using GameExpress.Model.Structs;
using System.Xml.Serialization;
using Windows.Foundation;

namespace GameExpress.Model.Item
{
    [XmlType("tweening")]
    public class ItemKeyFrameTweening : ItemKeyFrame
    {
        /// <summary>
        /// Die Matrix
        /// </summary>
        private Matrix3D m_matrix;

        /// <summary>
        /// Konstruktor
        /// </summary>
        public ItemKeyFrameTweening()
        {
        }

        /// <summary>
        /// Objekt darstllen
        /// </summary>
        /// <param name="pc">Der Präsentationskontext</param>
        public override void Presentation(PresentationContext pc)
        {
            base.Presentation(pc);
        }

        /// <summary>
        /// Liefert oder setzt die Transformationsmatrix
        /// </summary>
        [XmlIgnore]
        public override Matrix3D Matrix
        {
            get { return m_matrix; }
            set
            {
                m_matrix = value;
                RaisePropertyChanged();
            }
        }
    }
}