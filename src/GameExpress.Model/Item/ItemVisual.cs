using GameExpress.Model.Structs;
using System.Xml.Serialization;
using Windows.Foundation;
using Windows.UI;

namespace GameExpress.Model.Item
{
    [XmlInclude(typeof(ItemGraphics))]
    [XmlInclude(typeof(ItemGame))]
    public abstract class ItemVisual : ItemTreeNode
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public ItemVisual()
        {
        }

        /// <summary>
        /// Initialisiert das Item
        /// </summary>
        public override void Init()
        {
            base.Init();
        }

        /// <summary>
        /// Objekt aktualisieren
        /// </summary>
        /// <param name="uc">Der Updatekontext</param>
        public override void Update(UpdateContext uc)
        {

        }

        /// <summary>
        /// Objekt darstllen
        /// </summary>
        /// <param name="pc">Der Präsentationskontext</param>
        public override void Presentation(PresentationContext pc)
        {

        }

        /// <summary>
        /// Liefert eine Tiefernkopie des Items
        /// </summary>
        /// <returns>Die Tiefenkopie</returns>
        public override T Copy<T>()
        {
            var copy = base.Copy<T>() as ItemVisual;

            return copy as T;
        }

        /// <summary>
        /// Liefert die Größe
        /// </summary>
        [XmlIgnore]
        public abstract Size Size { get; }
    }
}
