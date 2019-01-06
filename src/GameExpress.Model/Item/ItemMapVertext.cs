using GameExpress.Model.Structs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Windows.UI;

namespace GameExpress.Model.Item
{
    /// <summary>
    ///  Eck- bzw. Scheitelpunkt eines Primitivs (Dreiecks)
    /// </summary>
    [XmlType("vertex")]
    public class ItemMapVertext : Item
    {
        /// <summary>
        /// Die Koordinaten
        /// </summary>
        private Vector m_point = new Vector();

        /// <summary>
        /// Der Gammawert von 0-1
        /// </summary>
        private Structs.Gamma m_gamma;

        /// <summary>
        /// Der Alphawert von 0-255
        /// </summary>
        private Structs.Alpha m_alpha;

        /// <summary>
        /// Der Farbton
        /// </summary>
        private Structs.Hue m_hue;

        /// <summary>
        /// Die Matrix
        /// </summary>
        [XmlElement("matrix")]
        private Matrix3D m_matrix = Matrix3D.Identity;

        /// <summary>
        /// Konstruktor
        /// </summary>
        public ItemMapVertext()
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
            // Der Pfad ist ein nicht sichtbares Element, nur im Designer 
            // wird er visualisiert
            if (pc.Designer)
            {
                // Vertex im Weltkoordinaten transformieren
                var p = pc.Transform(Vector);

                var black = Color.FromArgb(200, 0, 0, 0);
                var white = Color.FromArgb(200, 255, 255, 255);

                // Create a Pen object.
                pc.Graphics.FillEllipse(p.X - 4, p.Y - 4, 8, 8, white);
                pc.Graphics.DrawEllipse(p.X - 4, p.Y - 4, 8, 8, black);
            }
        }

        // <summary>
        /// Liefert eine Tiefernkopie des Items
        /// </summary>
        /// <returns>Die Tiefenkopie</returns>
        public override T Copy<T>()
        {
            var copy = base.Copy<T>() as ItemMapVertext;
            copy.Vector = Vector;
            copy.Gamma = Gamma;
            copy.Alpha = Alpha;
            copy.Hue = Hue;
            copy.Matrix = new Matrix3D(Matrix);

            return copy as T;
        }

        /// <summary>
        /// Liefert oder setzt die Koordinaten
        /// </summary>
        [XmlElement("vector")]
        public Vector Vector
        {
            get { return m_point; }
            set
            {
                if (!m_point.Equals(value))
                {
                    m_point = value;

                    RaisePropertyChanged();
                }
            }
        }

        /// <summary>
        /// Liefert oder setzt den Gamma-Wert
        /// </summary>
        [XmlElement("gamma")]
        public Gamma Gamma
        {
            get { return m_gamma; }
            set
            {
                if (!m_gamma.Equals(value))
                {
                    m_gamma = value;

                    RaisePropertyChanged();
                }
            }
        }

        /// <summary>
        /// Liefert oder setzt den Alpha-Wert
        /// </summary>
        [XmlElement("alpha")]
        public Alpha Alpha
        {
            get { return m_alpha; }
            set
            {
                if (!m_alpha.Equals(value))
                {
                    m_alpha = value;

                    RaisePropertyChanged();
                }
            }
        }

        /// <summary>
        /// Liefert oder setzt die Farbton-Eigenschaft
        /// </summary>
        [XmlIgnore]
        public Hue Hue
        {
            get { return m_hue; }
            set
            {
                if (!m_hue.Equals(value))
                {
                    m_hue = value;

                    RaisePropertyChanged();
                }
            }
        }

        /// <summary>
        /// Liefert oder setzt die Matrix
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
    }
}
