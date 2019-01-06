using GameExpress.Model.Structs;
using Microsoft.Graphics.Canvas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Windows.Foundation;
using Windows.UI;

namespace GameExpress.Model.Item
{
    /// <summary>
    ///  Dreieck eines Netzes
    /// </summary>
    [XmlType("mesh")]
    public class ItemMapMesh : Item
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
        /// Liefert oder setzt den Namen des ersten Vertext
        /// </summary>
        [XmlAttribute("vertext1")]
        public string Vertext1
        {
            get { return m_vertext1; }
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
            get { return m_vertext2; }
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
            get { return m_vertext3; }
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
        public Point Centroid
        {
            get
            {
                if (VertextItem1 == null || VertextItem2 == null || VertextItem3 == null)
                {
                    return new Point();
                }

                //var x = (VertextItem1.Point.X + VertextItem2.Point.X + VertextItem3.Point.X) / 3f;
                //var y = (VertextItem1.Point.Y + VertextItem2.Point.Y + VertextItem3.Point.Y) / 3f;

                //return new Point((int)x, (int)y);

                return new Point();
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
            //if (VertextItem1 == null)
            //{
            //    VertextItem1 = Parent.FindItem<ItemMapVertext>(Vertext1, oneLevel: true).FirstOrDefault();
            //}

            //if (VertextItem2 == null)
            //{
            //    VertextItem2 = Parent.FindItem<ItemMapVertext>(Vertext2, oneLevel: true).FirstOrDefault();
            //}

            //if (VertextItem3 == null)
            //{
            //    VertextItem3 = Parent.FindItem<ItemMapVertext>(Vertext3, oneLevel: true).FirstOrDefault();
            //}
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
                //var black = Color.FromArgb(200, 0, 0, 0);
                //var white = Color.FromArgb(200, 255, 255, 255);

                //// Vertex im Weltkoordinaten transformieren
                //var p1 = pc.Transform(VertextItem1 != null ? VertextItem1.Point : new Point());
                //var p2 = pc.Transform(VertextItem2 != null ? VertextItem2.Point : new Point());
                //var p3 = pc.Transform(VertextItem3 != null ? VertextItem3.Point : new Point());

                //pc.Graphics.DrawLine(p1, p2, black);
                //pc.Graphics.DrawLine(p1, p2, white);

                //pc.Graphics.DrawLine(p2, p3, black);
                //pc.Graphics.DrawLine(p2, p3, white);

                //pc.Graphics.DrawLine(p3, p1, black);
                //pc.Graphics.DrawLine(p3, p1, white);

                //var centroid = Centroid;
                //var points = new PointF[]
                //{
                //    new Point(centroid.X, centroid.Y - 3),
                //    new Point(centroid.X + 4, centroid.Y + 4),
                //    new Point(centroid.X - 4, centroid.Y + 4)
                //};

                //using (var whiteBrush = new SolidBrush(Color.White))
                //{
                //    pc.Graphics.FillPolygon(whiteBrush, points);
                //}

                //blackPen.Width = 1;
                //pc.Graphics.DrawPolygon(blackPen, points);
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
        /// Lädt das Bild aus der gegebenen Quelle
        /// </summary>
        /// <param name="g">Der Zeichenkontext</param>
        public override void CreateResources(ICanvasResourceCreator g)
        {
        }     
    }
}
