using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace GameExpress.Core.Items
{
    /// <summary>
    ///  Eck- bzw. Scheitelpunkt eines Primitivs (Dreiecks)
    /// </summary>
    [XmlType("vertex")]
    public class ItemMapVertext : Item
    {
        /// <summary>
        /// Der Hotspot
        /// </summary>
        private Point m_point = new Point();

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
        private Structs.Matrix3D m_matrix = Structs.Matrix3D.Identity;

        /// <summary>
        /// Konstruktor
        /// </summary>
        public ItemMapVertext()
            : this(Project.ItemContextList.GetItemContext(typeof(ItemMapVertext)))
        {
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="context">Das zugehörige Kontextobjekt</param>
        public ItemMapVertext(ItemContext context)
            : base(context, true)
        {
        }

        /// <summary>
        /// Initialisiert das Item
        /// </summary>
        public override void Init()
        {

        }

        /// <summary>
        /// Objekt darstllen
        /// </summary>
        /// <param name="pc">Der Präsentationskontext</param>
        public override void Presentation(Structs.PresentationContext pc)
        {
            // Der Pfad ist ein nicht sichtbares Element, nur im Designer 
            // wird er visualisiert
            if (pc.Designer)
            {
                // Vertex im Weltkoordinaten transformieren
                var p = pc.Transform(Point);

                // Create a Pen object.
                using (Pen blackPen = new Pen(Color.Black, 1))
                {
                    using (var whitePen = new SolidBrush(Color.White))
                    {
                        pc.Graphics.FillEllipse(whitePen, p.X - 4, p.Y - 4, 8, 8);
                        pc.Graphics.DrawEllipse(blackPen, p.X - 4, p.Y - 4, 8, 8);
                    }
                }
            }
        }

        /// <summary>
        /// Liefert eine Tiefernkopie des Items
        /// </summary>
        /// <returns>Die Tiefenkopie</returns>
        public override IItem Copy()
        {
            var copy = base.Copy() as ItemMapVertext;
            copy.Point = Point;
            copy.Gamma = Gamma;
            copy.Alpha = Alpha;
            copy.Hue = Hue;
            copy.Matrix = new Structs.Matrix3D(Matrix);

            return copy;
        }

        /// <summary>
        /// Liefert oder setzt die Koordinaten
        /// </summary>
        [Category("Darstellung"), Description("Geben Sie hier die Koordinaten an.")]
        [XmlElement("point")]
        public Point Point
        {
            get { return m_point; }
            set
            {
                if (!m_point.Equals(value))
                {
                    m_point = value;

                    NotifyPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Liefert oder setzt den Gamma-Wert
        /// </summary>
        [Category("Darstellung"), DisplayName("Gamma"), Description("Geben Sie hier den Gammawert an. Übliche Werte liegen zwischen 2.0 und 5.0.")]
        [XmlElement("gamma")]
        public Structs.Gamma Gamma
        {
            get { return m_gamma; }
            set
            {
                if (!m_gamma.Equals(value))
                {
                    m_gamma = value;

                    NotifyPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Liefert oder setzt den Alpha-Wert
        /// </summary>
        [Category("Darstellung"), DisplayName("Alpha"), Description("Geben Sie hier den Alphawert in % an.")]
        [XmlElement("alpha")]
        public Structs.Alpha Alpha
        {
            get { return m_alpha; }
            set
            {
                if (!m_alpha.Equals(value))
                {
                    m_alpha = value;

                    NotifyPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Liefert oder setzt die Farbton-Eigenschaft
        /// </summary>
        [Category("Darstellung"), DisplayName("Farbton"), Description("Geben Sie hier eine mögliche Farbkorektur an.")]
        [XmlElement("hue")]
        public Structs.Hue Hue
        {
            get { return m_hue; }
            set
            {
                if (!m_hue.Equals(value))
                {
                    m_hue = value;

                    NotifyPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Liefert oder setzt die Matrix
        /// </summary>
        [Category("Darstellung"), DisplayName("Matrix"), Description("Geben Sie hier eine Tranformationsmatrix an.")]
        [XmlElement("matrix")]
        public Structs.Matrix3D Matrix
        {
            get { return m_matrix; }
            set
            {
                if (!m_matrix.Equals(value))
                {
                    m_matrix = value;

                    NotifyPropertyChanged();
                }
            }
        }
    }
}
