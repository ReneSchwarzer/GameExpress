using GameExpress.Model.Structs;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Xml.Serialization;
using Windows.Foundation;
using Windows.UI.Xaml;

namespace GameExpress.Model.Item
{
    [XmlType("animation")]
    public class ItemAnimation : ItemGraphics
    {
        /// <summary>
        /// Liefert oder setzt die Instanzen
        /// </summary>
        [XmlElement("story")]
        public ObservableCollection<ItemStory> StoryBoard { get; set; } = new ObservableCollection<ItemStory>();

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

            foreach (var v in StoryBoard.Reverse())
            {
                v.Presentation(new PresentationContext(pc) { });
            }
        }

        /// <summary>
        /// Liefert eine Tiefernkopie des Items
        /// </summary>
        /// <returns>Die Tiefenkopie</returns>
        public override T Copy<T>()
        {
            var copy = base.Copy<T>() as ItemAnimation;

            foreach (var i in StoryBoard)
            {
                copy.StoryBoard.Add(i.Copy<ItemStory>());
            }

            return copy as T;
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

        /// <summary>
        /// Liefert das Icon des Items aus der FontFamily Segoe MDL2 Assets
        /// </summary>
        public override string Symbol { get { return "\uE173"; } }
    }
}
