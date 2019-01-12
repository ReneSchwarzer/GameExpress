using GameExpress.Model.Structs;
using System.Xml.Serialization;
using Windows.Foundation;

namespace GameExpress.Model.Item
{
    [XmlType("keyframebase")]
    public abstract class ItemKeyFrameBase : ItemVisual
    {
        /// <summary>ItemKeyFrameBase
        /// Konstruktor
        /// </summary>
        public ItemKeyFrameBase()
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
            base.Presentation(pc);
        }

        /// <summary>
        /// Liefert eine Tiefernkopie des Items
        /// </summary>
        /// <returns>Die Tiefenkopie</returns>
        public override T Copy<T>()
        {
            var copy = base.Copy<T>() as ItemKeyFrameBase;
            return copy as T;
        }


        /// <summary>
        /// Liefert oder setzt die Transformationsmatrix
        /// </summary>
        [XmlIgnore]
        public abstract Matrix3D Matrix { get; set; }

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