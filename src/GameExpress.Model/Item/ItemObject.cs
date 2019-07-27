using GameExpress.Model.Structs;
using System.Collections.ObjectModel;
using System.Xml.Serialization;
using Windows.Foundation;

namespace GameExpress.Model.Item
{
    [XmlType("object")]
    public class ItemObject : ItemTreeNode, IItemVisual, IItemClickable
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
            //base.Update(uc);


        }

        /// <summary>
        /// Objekt darstllen
        /// </summary>
        /// <param name="pc"></param>
        public override void Presentation(PresentationContext pc)
        {
            if (!Enable)
            {
                return;
            }

            var transform = pc.Graphics.Transform;

            //base.Presentation(pc);

            pc.Graphics.Transform = transform;
        }

        /// <summary>
        /// Liefert die Anzeigematrix des Items
        /// </summary>
        /// <returns>Die Matrix mit allen Transformationen des Items</returns>
        public virtual Matrix3D GetMatrix()
        {
            return Matrix3D.Identity;
        }

        /// <summary>
        /// Prüft ob der Punkt innerhalb eines Items liegt und gibt das Item zurück
        /// </summary>
        /// <param name="hc">Der Kontext</param>
        /// <param name="point">Der zu überprüfende Punkt</param>
        /// <returns>Das erste Item, welches gefunden wurde oder null</returns>
        public virtual Item HitTest(HitTestContext hc, Vector point)
        {
            return null;
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
        public virtual Size Size => new Size();
    }
}
