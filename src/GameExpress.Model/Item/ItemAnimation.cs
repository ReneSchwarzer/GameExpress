using GameExpress.Model.Structs;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Xml.Serialization;
using Windows.Foundation;

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
        /// Initialisierung
        /// </summary>
        public override void Init()
        {
            base.Init();

            foreach (var story in StoryBoard)
            {
                story.Init();
            }
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

            foreach (var story in StoryBoard)
            {
                var newUC = new UpdateContext(uc);
                story.Update(new UpdateContext(newUC));
            }
        }

        /// <summary>
        /// Objekt darstllen
        /// </summary>
        /// <param name="pc">Der Präsentationskontext</param>
        public override void Presentation(PresentationContext pc)
        {
            base.Presentation(pc);

            if (StoryBoard == null)
            {
                return;
            }

            foreach (var v in StoryBoard.Reverse())
            {
                var newPC = new PresentationContext(pc);
                v.Presentation(new PresentationContext(newPC) { });
            }
        }

        /// <summary>
        /// Liefert die Anzeigematrix des Items
        /// </summary>
        /// <returns>Die Matrix mit allen Transformationen des Items</returns>
        public override Matrix3D GetMatrix()
        {
            return Matrix3D.Identity;
        }

        /// <summary>
        /// Prüft ob der Punkt innerhalb eines Items liegt und gibt das Item zurück
        /// </summary>
        /// <param name="hc">Der Kontext</param>
        /// <param name="point">Der zu überprüfende Punkt</param>
        /// <returns>Das erste Item, welches gefunden wurde oder null</returns>
        public override Item HitTest(HitTestContext hc, Vector point)
        {
            foreach (var story in StoryBoard)
            {
                if (story.GetKeyFrame((ulong)hc.Time) is IItemClickable frame)
                {
                    var newHC = new HitTestContext(hc);
                    var item = frame.HitTest(newHC, point);

                    if (item != null)
                    {
                        return item;
                    }
                }
                
            }

            return null;
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
        public override Size Size => new Size();
    }
}
