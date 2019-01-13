using GameExpress.Model.Structs;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Xml.Serialization;
using Windows.Foundation;
using Windows.UI.Xaml;

namespace GameExpress.Model.Item
{
    [XmlType("animation")]
    public abstract class ItemAnimation : ItemGraphics
    {
        /// <summary>
        /// Liefert oder setzt die Instanzen
        /// </summary>
        [XmlElement("story")]
        public ObservableCollection<ItemStory> StoryBoard { get; set; } = new ObservableCollection<ItemStory>();

        /// <summary>
        /// Liefert oder setzt die Zeit, bei dem die Annimation beendet wird
        /// </summary>
        ulong m_endTime;
        [XmlAttribute("endtime")]
        public ulong EndTime
        {
            get { return m_endTime; }
            set
            {
                if (m_endTime != value)
                {
                    m_endTime = value;

                    RaisePropertyChanged();
                }
            }
        }

        /// <summary>
        /// Liefert oder setzt ob die Annimation in einer Schleife wiederholt werden soll
        /// </summary>
        [XmlAttribute("loop")]
        public Loop Loop { get; set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        public ItemAnimation()
        {
            StoryBoard.CollectionChanged += (s, e) =>
            {
                if (e.NewItems != null)
                {
                    foreach (ItemStory v in e.NewItems)
                    {
                        v.Parent = this;
                        v.PropertyChanged += OnStoryPropertyChanged;
                    }
                }

                if (e.OldItems != null)
                {
                    foreach (ItemStory v in e.OldItems)
                    {
                        v.Parent = null;
                        v.PropertyChanged -= OnStoryPropertyChanged;
                    }
                }
            };
        }

        /// <summary>
        /// Wird aufgerufen, wenn sich eine Story geändert hat
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="args">Das Eventargument</param>
        private void OnStoryPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            RaisePropertyChanged("StoryBoard");
        }

        /// <summary>
        /// Objekt aktualisieren
        /// </summary>
        /// <param name="uc">Der Updatekontext</param>
        public override void Update(UpdateContext uc)
        {
            base.Update(uc);

            foreach (var v in StoryBoard)
            {
                v.Update(new UpdateContext(uc));
            }
        }

        /// <summary>
        /// Objekt darstllen
        /// </summary>
        /// <param name="pc"></param>
        public override void Presentation(PresentationContext pc)
        {
            base.Presentation(pc);

            if (StoryBoard == null) return;

            foreach (var v in StoryBoard)
            {
                v.Presentation(new PresentationContext(pc) { Time = LocalTime(pc.Time) });
            }
        }

        /// <summary>
        /// Liefert eine Tiefernkopie des Items
        /// </summary>
        /// <returns>Die Tiefenkopie</returns>
        public override T Copy<T>()
        {
            var copy = base.Copy<T>() as ItemObject;

            foreach (var i in StoryBoard)
            {
                copy.StoryBoard.Add(i.Copy<ItemStory>());
            }

            return copy as T;
        }

        /// <summary>
        /// Berechnet die lokale Zeit
        /// </summary>
        /// <param name="time">Die globale Zeit</param>
        /// <returns>Die interne Zeit</returns>
        public Time LocalTime(Time time)
        {
            var local = new Time();

            if (EndTime < ulong.MaxValue && Loop == Loop.None && (ulong)time > EndTime)
            {
                // Zeit begrenzen
                local.AddTick(EndTime);
            }
            else if (EndTime < ulong.MaxValue && Loop == Loop.Default)
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
    }
}
