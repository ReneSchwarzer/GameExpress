using GameExpress.Model.Structs;
using System.Collections.ObjectModel;
using System.Xml.Serialization;
using Windows.Foundation;

namespace GameExpress.Model.Item
{
    [XmlType("object")]
    public class ItemObject : ItemVisual
    {
        /// <summary>
        /// Liefert oder setzt die Objektzustände
        /// </summary>
        [XmlElement("state")]
        public ObservableCollection<ItemAnimation> States { get; set; } = new ObservableCollection<ItemAnimation>();

        /// <summary>
        /// Konstruktor
        /// </summary>
        public ItemObject()
        {
            
        }

        /// <summary>
        /// Objekt aktualisieren
        /// </summary>
        /// <param name="uc">Der Updatekontext</param>
        public override void Update(UpdateContext uc)
        {
            base.Update(uc);

            
        }

        /// <summary>
        /// Objekt darstllen
        /// </summary>
        /// <param name="pc"></param>
        public override void Presentation(PresentationContext pc)
        {
            if (!Enable) return;

            var transform = pc.Graphics.Transform;

            base.Presentation(pc);

            pc.Graphics.Transform = transform;
        }

        /// <summary>
        /// Liefert eine Tiefernkopie des Items
        /// </summary>
        /// <returns>Die Tiefenkopie</returns>
        public override T Copy<T>()
        {
            var copy = base.Copy<T>() as ItemObject;

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
        public override string Symbol { get { return "\uEBD2"; } }
    }
}
