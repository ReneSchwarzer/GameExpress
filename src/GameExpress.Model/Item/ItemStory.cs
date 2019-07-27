using GameExpress.Model.Structs;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Xml.Serialization;

namespace GameExpress.Model.Item
{
    /// <summary>
    /// Objektinstanz
    /// </summary>
    [XmlType("story")]
    public class ItemStory : ItemTreeNode
    {
        /// <summary>
        /// Das Item
        /// </summary>
        private string m_item;

        /// <summary>
        /// Bestimmt die Animationnach dem letzten KeyFrame  
        /// </summary>
        private Loop m_loop;

        /// <summary>
        /// Liefert oder setzt ob die Annimation in einer Schleife wiederholt werden soll
        /// </summary>
        [XmlAttribute("loop")]
        public Loop Loop
        {
            get => m_loop;
            set
            {
                if (m_loop != value)
                {
                    m_loop = value;

                    RaisePropertyChanged();
                }
            }
        }

        /// <summary>
        /// Liefert oder setzt die Schlüselbilder
        /// </summary>
        [XmlElement("keyframe")]
        public ObservableCollection<ItemKeyFrame> KeyFrames { get; set; } = new ObservableCollection<ItemKeyFrame>();

        /// <summary>
        /// Liefert oder setzt den Verweis auf die übergeordnete Animation
        /// </summary>
        [XmlIgnore]
        public ItemAnimation Animation => Parent as ItemAnimation;

        /// <summary>
        /// Die ID des mit der Instanz verbundenen Element
        /// </summary>
        [XmlIgnore]
        public Item Instance { get; private set; }

        /// <summary>
        /// Liefert einen Verweis auf die aktuelle Story
        /// </summary>
        [XmlIgnore]
        public ItemStory Self => this;

        /// <summary>
        /// Konstruktor
        /// </summary>
        public ItemStory()
        {
            KeyFrames.CollectionChanged += (s, e) =>
            {
                if (e.NewItems != null)
                {
                    foreach (ItemKeyFrame v in e.NewItems)
                    {
                        v.Story = this;
                        v.PropertyChanged += OnKeyFramePropertyChanged;
                    }
                }

                if (e.OldItems != null)
                {
                    foreach (ItemKeyFrame v in e.OldItems)
                    {
                        v.Story = null;
                        v.PropertyChanged -= OnKeyFramePropertyChanged;
                    }
                }
            };
        }

        /// <summary>
        /// Initialisiert das Item
        /// </summary>
        public override void Init()
        {
            base.Init();

            AttachedInstance(Item);
        }

        /// <summary>
        /// Objekt aktualisieren
        /// </summary>
        /// <param name="uc">Der Updatekontext</param>
        public override void Update(UpdateContext uc)
        {
            if (Instance == null)
            {
                AttachedInstance(Item);
            }

            var newUC = new UpdateContext(uc) { Time = LocalTime(uc.Time) };

            if (GetKeyFrame((ulong)newUC.Time) is ItemKeyFrame frame)
            {
                frame.Update(newUC);
            }
        }

        /// <summary>
        /// Objekt darstllen
        /// </summary>
        /// <param name="pc">Der Präsentationskontext</param>
        public override void Presentation(PresentationContext pc)
        {
            if (pc.Level > 10 || !Enable)
            {
                return;
            }

            var newPC = new PresentationContext(pc)
            {
                Time = LocalTime(pc.Time)
            };

            if (GetKeyFrame((ulong)newPC.Time) is ItemKeyFrame frame)
            {
                frame.Presentation(newPC);
            }
        }

        /// <summary>
        /// Liefert eine Tiefernkopie des Items
        /// </summary>
        /// <returns>Die Tiefenkopie</returns>
        public override T Copy<T>()
        {
            var copy = base.Copy<T>() as ItemStory;

            return copy as T;
        }

        /// <summary>
        /// Ermittelt und fügt die Instanz hinzu
        /// </summary>
        /// <param name="item">Das Item</param>
        protected void AttachedInstance(string item)
        {
            if (Animation == null)
            {
                return;
            }

            // Suche Item
            var origin = Animation.Root.FindItem(Item);

            if (origin != null)
            {
                //Instance = orgin.Copy();
                //Instance.Name = Name + "_" + Instance.Name;
                //(Instance as Item).Parent = this;

                Instance = origin;
            }
        }

        /// <summary>
        /// Liefert ein Schlüselbild zu der gegebenen Zeit
        /// </summary>
        /// <param name="time">Die Zeit</param>
        /// <returns>Das Schlüsselbid oder null</returns>
        public ItemKeyFrame GetKeyFrame(ulong time)
        {
            ItemKeyFrameAct predecessorKeyFrame = null;
            ulong absolutePredecessorEndTime = 0;

            ItemKeyFrameAct successorKeyFrame = null;
            ulong absoluteSuccessorStartTime = 0;

            ItemKeyFrameTweening tweening = null;

            ulong absoluteTime = 0;

            foreach (var k in KeyFrames)
            {
                if (k is ItemKeyFrameAct act)
                {
                    absoluteTime += act.From;

                    if (absoluteTime <= time && time <= absoluteTime + act.Duration)
                    {
                        // Schlüsselbild 
                        return act;
                    }
                    else if (absoluteTime + act.Duration < time)
                    {
                        predecessorKeyFrame = act;
                        absolutePredecessorEndTime = absoluteTime + act.Duration;
                    }
                    else if (time < absoluteTime + act.Duration)
                    {
                        successorKeyFrame = act;
                        absoluteSuccessorStartTime = absoluteTime;

                        break;
                    }

                    absoluteTime += act.Duration;
                }
                else if (k is ItemKeyFrameTweening tween)
                {
                    tweening = tween;
                }
            }

            // Tweening
            if (absolutePredecessorEndTime < time && time < absoluteSuccessorStartTime && tweening != null)
            {
                var from = absolutePredecessorEndTime;
                var till = absoluteSuccessorStartTime;
                var progress = (time - from) / (float)(till - from); // in %

                var snapShot = tweening.Copy<ItemKeyFrameTweening>();
                snapShot.Init(predecessorKeyFrame, successorKeyFrame, progress);

                // ToDo: Alpha, usw.

                return snapShot;
            }

            return null;
        }

        /// <summary>
        /// Berechnet die lokale Zeit
        /// </summary>
        /// <param name="time">Die globale Zeit</param>
        /// <returns>Die interne Zeit</returns>
        public Time LocalTime(Time time)
        {
            var local = new Time();

            if (KeyFrames.Count == 0 || EndTime == 0)
            {

            }
            else if (EndTime < ulong.MaxValue && Loop == Loop.Freeze && (ulong)time > EndTime)
            {
                // Zeit begrenzen
                local.AddTick(EndTime);
            }
            else if (EndTime < ulong.MaxValue && Loop == Loop.Repeat)
            {
                // Schleife
                local.AddTick((ulong)time % EndTime);
            }
            else if (EndTime < ulong.MaxValue && Loop == Loop.Oscillate)
            {
                // Schwingen
                var direction = ((ulong)time / EndTime) % 2;
                switch (direction)
                {
                    case 0:
                        local.AddTick((ulong)time % EndTime);
                        break;
                    default:
                        local.AddTick(EndTime - (ulong)time % EndTime);
                        break;
                }

            }
            else
            {
                local.AddTick((ulong)time);
            }

            return local;
        }

        /// <summary>
        /// Wird aufgerufen, wenn sich eine Story geändert hat
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="args">Das Eventargument</param>
        private void OnKeyFramePropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            RaisePropertyChanged("KeyFrames");
        }

        /// <summary>
        /// Liefert oder setzt die Zeit, bei dem die Annimation beendet wird
        /// </summary>
        [XmlIgnore]
        public ulong EndTime => (ulong)KeyFrames.Where(x => x is ItemKeyFrameAct).Select(x => x as ItemKeyFrameAct).Sum(x => (decimal)x.From + x.Duration);

        /// <summary>
        /// Liefert oder setzt das mit der Instanz verbundene Element
        /// </summary>
        [XmlAttribute("item")]
        public string Item
        {
            get => m_item;
            set
            {
                if (m_item == null ? true : !m_item.Equals(value))
                {
                    m_item = value;
                    AttachedInstance(Item);

                    RaisePropertyChanged();
                }
            }
        }
    }
}

