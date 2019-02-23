using GameExpress.Model.Structs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Windows.Foundation;

namespace GameExpress.Model.Item
{
    /// <summary>
    /// Objektinstanz
    /// </summary>
    [XmlType("story")]
    public class ItemStory : ItemVisual
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
            get { return m_loop; }
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
        /// Liefert das Icon des Items aus der FontFamily Segoe MDL2 Assets
        /// </summary>
        public override string Symbol { get { return "\uE77C"; } }

        /// <summary>
        /// Liefert oder setzt die Schlüselbilder
        /// </summary>
        [XmlElement("keyframe")]
        public ObservableCollection<ItemKeyFrame> KeyFrames { get; set; } = new ObservableCollection<ItemKeyFrame>();

        /// <summary>
        /// Liefert oder setzt den Verweis auf die übergeordnete Animation
        /// </summary>
        [XmlIgnore]
        public ItemAnimation Animation { get { return Parent as ItemAnimation; } }

        /// <summary>
        /// Die ID des mit der Instanz verbundenen Element
        /// </summary>
        [XmlIgnore]
        public Item Instance { get; private set; }

        /// <summary>
        /// Liefert einen Verweis auf die aktuelle Story
        /// </summary>
        public ItemStory Self { get { return this; } }

        /// <summary>
        /// Konstruktor
        /// </summary>
        public ItemStory()
        {

        }

        /// <summary>
        /// Initialisiert das Item
        /// </summary>
        public override void Init()
        {
            base.Init();

            KeyFrames.CollectionChanged += (s, e) =>
            {
                if (e.NewItems != null)
                {
                    foreach (ItemKeyFrame v in e.NewItems)
                    {
                        v.Parent = this;
                        v.PropertyChanged += OnKeyFramePropertyChanged;
                    }
                }

                if (e.OldItems != null)
                {
                    foreach (ItemKeyFrame v in e.OldItems)
                    {
                        v.Parent = null;
                        v.PropertyChanged -= OnKeyFramePropertyChanged;
                    }
                }
            };
        }

        /// <summary>
        /// Objekt aktualisieren
        /// </summary>
        /// <param name="uc">Der Updatekontext</param>
        public override void Update(UpdateContext uc)
        {
            base.Update(uc);

            if (Instance == null)
            {
                AttachedInstance(Item);
            }

        }

        /// <summary>
        /// Objekt darstllen
        /// </summary>
        /// <param name="pc">Der Präsentationskontext</param>
        public override void Presentation(PresentationContext pc)
        {
            if (pc.Level > 10 || !Enable) return;

            var newPC = new PresentationContext(pc) { Level = pc.Level, Time = LocalTime(pc.Time) };

            base.Presentation(newPC);

            if (Instance == null)
            {
                AttachedInstance(Item);
            }

            var currentKeyFrame = GetKeyFrame((ulong)newPC.Time);

            if (currentKeyFrame != null)
            {
                newPC.Matrix *= currentKeyFrame.Matrix;

                if (Instance is ItemGraphics)
                {
                    var graphics = Instance as ItemGraphics;
                    newPC.Matrix *= Matrix3D.Translation(graphics.Hotspot.X * -1, graphics.Hotspot.Y * -1);
                }

                Instance?.Presentation(newPC);
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
            if (Animation == null) return;

            // Suche Item
            var orgin = Animation.Root.FindItem(Item);

            if (orgin != null)
            {
                //Instance = orgin.Copy();
                //Instance.Name = Name + "_" + Instance.Name;
                //(Instance as Item).Parent = this;

                Instance = orgin;
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

                var mt = predecessorKeyFrame.Matrix.Invert * successorKeyFrame.Matrix;

                var t = (time - from) / (float)(till - from); //in %

                tweening.Matrix = predecessorKeyFrame.Matrix * new Matrix3D
                (
                    (mt.M11 - 1) * t + 1, mt.M12 * t, 0,
                    mt.M21 * t, (mt.M22 - 1) * t + 1, 0,
                    mt.M31 * t, mt.M32 * t, 1
                );

                // ToDo: Alpha, usw.

                return tweening;
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
                local.AddTick((ulong)time % (ulong)EndTime);
            }
            else if (EndTime < ulong.MaxValue && Loop == Loop.Oscillate)
            {
                // Schwingen
                var direction = ((ulong)time / (ulong)EndTime) % 2;
                switch (direction)
                {
                    case 0:
                        local.AddTick((ulong)time % (ulong)EndTime);
                        break;
                    default:
                        local.AddTick((ulong)EndTime - (ulong)time % (ulong)EndTime);
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
        public ulong EndTime
        {
            get
            {
                return (ulong)KeyFrames.Where(x => x is ItemKeyFrameAct).Select(x => x as ItemKeyFrameAct).Sum(x => (decimal)x.From + x.Duration);
            }
        }

        /// <summary>
        /// Liefert oder setzt das mit der Instanz verbundene Element
        /// </summary>
        [XmlAttribute("item")]
        public string Item
        {
            get { return m_item; }
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

        /// <summary>
        /// Liefert die Größe
        /// </summary>
        [XmlIgnore]
        public override Size Size
        {
            get
            {
                return new Size();
            }
        }
    }
}

