using GameExpress.Model.Structs;
using System.Xml.Serialization;
using Windows.Foundation;

namespace GameExpress.Model.Item
{
    [XmlType("keyframe")]
    public class ItemKeyFrame : ItemGraphics
    {
        /// <summary>
        /// Beginn
        /// </summary>
        private ulong m_from;

        /// <summary>
        /// Dauer
        /// </summary>
        private ulong m_duration;

        /// <summary>
        /// Matrix
        /// </summary>
        private Matrix3D m_matrix = Matrix3D.Identity;

        /// <summary>
        /// Übergang
        /// </summary>
        private bool m_tweening;

        /// <summary>
        /// Konstruktor
        /// </summary>
        public ItemKeyFrame()
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
            var copy = base.Copy<T>() as ItemKeyFrame;
            copy.From = From;
            copy.Duration = Duration;
            copy.Matrix = Matrix;
            copy.Tweening = Tweening;

            return copy as T;
        }

        /// <summary>
        /// Liefert oder setzt die Startzeit
        /// </summary>
        [XmlAttribute("from")]
        public ulong From
        {
            get { return m_from; }
            set
            {
                if (m_from != value)
                {
                    m_from = value;

                    RaisePropertyChanged();
                }
            }
        }

        /// <summary>
        /// Liefert oder setzt die Dauer in 1/40 Sekunden
        /// </summary>
        [XmlAttribute("duration")]
        public ulong Duration
        {
            get { return m_duration; }
            set
            {
                if (m_duration != value)
                {
                    m_duration = value;

                    RaisePropertyChanged();
                }
            }
        }

        /// <summary>
        /// Liefert oder setzt die Transformationsmatrix
        /// </summary>
        [XmlElement("matrix")]
        public Matrix3D Matrix
        {
            get { return m_matrix; }
            set
            {
                if (!m_matrix.Equals(value))
                {
                    m_matrix = value;

                    RaisePropertyChanged();
                }
            }
        }

        /// <summary>
        /// Liefert oder setzt die Tweening-Eigenschaft
        /// </summary>
        [XmlAttribute("tweening")]
        public bool Tweening
        {
            get { return m_tweening; }
            set
            {
                if (!m_tweening.Equals(value))
                {
                    m_tweening = value;

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