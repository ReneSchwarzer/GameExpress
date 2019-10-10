using GameExpress.Model.Structs;
using System.Xml.Serialization;
using Windows.Foundation;
using Windows.UI;

namespace GameExpress.Model.Item
{
    /// <summary>
    ///  Eck- bzw. Scheitelpunkt eines Primitivs (Dreiecks)
    /// </summary>
    [XmlType("vertex")]
    public class ItemMapVertext : Item, IItemClickable
    {
        /// <summary>
        /// Die Koordinaten
        /// </summary>
        private Vector m_point = new Vector();

        /// <summary>
        /// Der Gammawert von 0-1
        /// </summary>
        private Gamma m_gamma;

        /// <summary>
        /// Der Alphawert von 0-255
        /// </summary>
        private Alpha m_alpha;

        /// <summary>
        /// Der Farbton
        /// </summary>
        private Hue m_hue;

        /// <summary>
        /// Die Matrix
        /// </summary>
        [XmlElement("matrix")]
        private Matrix3D m_matrix = Matrix3D.Identity;

        /// <summary>
        /// Liefert oder setzt die Map
        /// </summary>
        [XmlIgnore]
        public ItemMap Map { get; set; }

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
                //var p = pc.Transform(Vector);

                //var black = Color.FromArgb(200, 0, 0, 0);
                //var white = Color.FromArgb(200, 255, 255, 255);

                // Create a Pen object.
                //pc.Graphics.FillEllipse((float)p.X - 4, (float)p.Y - 4, 8, 8, white);
                //pc.Graphics.DrawEllipse((float)p.X - 4, (float)p.Y - 4, 8, 8, black);
            }
        }

        /// <summary>
        /// Liefert eine Tiefernkopie des Items
        /// </summary>
        /// <returns>Die Tiefenkopie</returns>
        public override Item Copy()
        {
            return Copy<ItemMapVertext>();
        }

        // <summary>
        /// Liefert eine Tiefernkopie des Items
        /// </summary>
        /// <returns>Die Tiefenkopie</returns>
        protected override T Copy<T>()
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
        /// Prüft ob der Punkt innerhalb eines Items liegt und gibt das Item zurück
        /// </summary>
        /// <param name="hc">Der Kontext</param>
        /// <param name="point">Der zu überprüfende Punkt</param>
        /// <returns>Das erste Item, welches gefunden wurde oder null</returns>
        public virtual Item HitTest(HitTestContext hc, Vector point)
        {
            var invert = hc.Matrix.Invert;
            var p = invert.Transform(point);

            var rect = new Rect(new Point(), new Size(8, 8));

            return rect.Contains(p) ? this : null;
        }

        /// <summary>
        /// Liefert oder setzt die Koordinaten
        /// </summary>
        [XmlElement("vector")]
        public Vector Vector
        {
            get => m_point;
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
            get => m_gamma;
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
            get => m_alpha;
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
            get => m_hue;
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
            get => m_matrix;
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
