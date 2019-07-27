using GameExpress.Model.Structs;
using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.Geometry;
using System;
using System.Linq;
using System.Numerics;
using System.Xml.Serialization;
using Windows.Foundation;
using Windows.UI;
using Vector = GameExpress.Model.Structs.Vector;

namespace GameExpress.Model.Item
{
    /// <summary>
    ///  Dreieck eines Netzes
    /// </summary>
    [XmlType("mesh")]
    public class ItemMapMesh : Item, IItemClickable
    {
        /// <summary>
        /// Der Name des ersten Vertext
        /// </summary>
        private string m_vertext1;

        /// <summary>
        /// Der erste Vertext
        /// </summary>
        [XmlIgnore]
        private ItemMapVertext VertextItem1 { get; set; }

        /// <summary>
        /// Der Name des zweiten Vertext
        /// </summary>
        private string m_vertext2;

        /// <summary>
        /// Der zweite Vertext
        /// </summary>
        [XmlIgnore]
        private ItemMapVertext VertextItem2 { get; set; }

        /// <summary>
        /// Der Name des dritten Vertext
        /// </summary>
        private string m_vertext3;

        /// <summary>
        /// Der dritte Vertext
        /// </summary>
        [XmlIgnore]
        private ItemMapVertext VertextItem3 { get; set; }

        /// <summary>
        /// Liefert oder setzt die Map
        /// </summary>
        [XmlIgnore]
        public ItemMap Map { get; set; } 

        /// <summary>
        /// Liefert oder setzt den Namen des ersten Vertext
        /// </summary>
        [XmlAttribute("vertext1")]
        public string Vertext1
        {
            get => m_vertext1;
            set
            {
                if (m_vertext1 == null && value != null || m_vertext1 != null && value == null || !m_vertext1.Equals(value))
                {
                    m_vertext1 = value;

                    RaisePropertyChanged();
                }
            }
        }

        /// <summary>
        /// Liefert oder setzt den Namen des zweiten Vertext
        /// </summary>
        [XmlAttribute("vertext2")]
        public string Vertext2
        {
            get => m_vertext2;
            set
            {
                if (m_vertext2 == null && value != null || m_vertext2 != null && value == null || !m_vertext2.Equals(value))
                {
                    m_vertext2 = value;

                    RaisePropertyChanged();
                }
            }
        }

        /// <summary>
        /// Liefert oder setzt den Namen des dritten Vertext
        /// </summary>
        [XmlAttribute("vertext3")]
        public string Vertext3
        {
            get => m_vertext3;
            set
            {
                if (m_vertext3 == null && value != null || m_vertext3 != null && value == null || !m_vertext3.Equals(value))
                {
                    m_vertext3 = value;

                    RaisePropertyChanged();
                }
            }
        }

        /// <summary>
        /// Liefert den Schwerpunkt des Dreiecks
        /// </summary>
        [XmlIgnore]
        public Structs.Vector Centroid
        {
            get
            {
                if (VertextItem1 == null || VertextItem2 == null || VertextItem3 == null)
                {
                    return new Point();
                }

                var x = (VertextItem1.Vector.X + VertextItem2.Vector.X + VertextItem3.Vector.X) / 3f;
                var y = (VertextItem1.Vector.Y + VertextItem2.Vector.Y + VertextItem3.Vector.Y) / 3f;

                return new Structs.Vector((int)x, (int)y);
            }
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        public ItemMapMesh()
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
            if (VertextItem1 == null)
            {
                VertextItem1 = Map.Vertices.Where(x => x.ID.Equals(Vertext1, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
            }

            if (VertextItem2 == null)
            {
                VertextItem2 = Map.Vertices.Where(x => x.ID.Equals(Vertext2, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
            }

            if (VertextItem3 == null)
            {
                VertextItem3 = Map.Vertices.Where(x => x.ID.Equals(Vertext3, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
            }
        }

        /// <summary>
        /// Objekt darstllen
        /// </summary>
        /// <param name="pc">Der Präsentationskontext</param>
        public override void Presentation(PresentationContext pc)
        {
            // Der Pfad ist ein nicht sichtbares Element, nur im Designer 
            // wird er visualisiert
            if (pc.Designer && VertextItem1 != null && VertextItem2 != null && VertextItem3 != null)
            {
                var black = Color.FromArgb(200, 0, 0, 0);
                var white = Color.FromArgb(200, 255, 255, 255);

                // Vertex im Weltkoordinaten transformieren
                var p1 = pc.Transform(VertextItem1 != null ? VertextItem1.Vector : new Structs.Vector());
                var p2 = pc.Transform(VertextItem2 != null ? VertextItem2.Vector : new Structs.Vector());
                var p3 = pc.Transform(VertextItem3 != null ? VertextItem3.Vector : new Structs.Vector());

                pc.Graphics.DrawLine((float)p1.X, (float)p1.Y, (float)p2.X, (float)p2.Y, black, 3);
                pc.Graphics.DrawLine((float)p1.X, (float)p1.Y, (float)p2.X, (float)p2.Y, white);

                pc.Graphics.DrawLine((float)p2.X, (float)p2.Y, (float)p3.X, (float)p3.Y, black, 2);
                pc.Graphics.DrawLine((float)p2.X, (float)p2.Y, (float)p3.X, (float)p3.Y, white);

                pc.Graphics.DrawLine((float)p3.X, (float)p3.Y, (float)p1.X, (float)p1.Y, black);
                pc.Graphics.DrawLine((float)p3.X, (float)p3.Y, (float)p1.X, (float)p1.Y, white);

                var centroid = pc.Transform(Centroid);
                var triangle = new Vector2[]
                {
                    new Vector2((float)centroid.X, (float)centroid.Y - 3),
                    new Vector2((float)centroid.X + 4, (float)centroid.Y + 4),
                    new Vector2((float)centroid.X - 4, (float)centroid.Y + 4)
                };

                using (var geometry = CanvasGeometry.CreatePolygon(pc.Graphics, triangle))
                {
                    pc.Graphics.FillGeometry(geometry, white);
                    pc.Graphics.DrawGeometry(geometry, black);
                }
            }
        }

        /// <summary>
        /// Liefert eine Tiefernkopie des Items
        /// </summary>
        /// <returns>Die Tiefenkopie</returns>
        public override T Copy<T>()
        {
            var copy = base.Copy<T>() as ItemMapMesh;

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
        /// Lädt das Bild aus der gegebenen Quelle
        /// </summary>
        /// <param name="g">Der Zeichenkontext</param>
        public override void CreateResources(ICanvasResourceCreator g)
        {
        }
    }
}
