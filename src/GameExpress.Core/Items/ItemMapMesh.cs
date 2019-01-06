using GameExpress.Core.Structs;
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
        [Browsable(false)]
        private ItemMapVertext VertextItem1 { get; set; }

        /// <summary>
        /// Der Name des zweiten Vertext
        /// </summary>
        private string m_vertext2;

        /// <summary>
        /// Der zweite Vertext
        /// </summary>
        [Browsable(false)]
        private ItemMapVertext VertextItem2 { get; set; }

        /// <summary>
        /// Der Name des dritten Vertext
        /// </summary>
        private string m_vertext3;

        /// <summary>
        /// Der dritte Vertext
        /// </summary>
        [Browsable(false)]
        private ItemMapVertext VertextItem3 { get; set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        public ItemMapMesh()
            : this(Project.ItemContextList.GetItemContext(typeof(ItemMapMesh)))
        {
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="context">Das zugehörige Kontextobjekt</param>
        public ItemMapMesh(ItemContext context)
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
        /// Objekt aktualisieren
        /// </summary>
        /// <param name="uc">Der Updatekontext</param>
        public override void Update(UpdateContext uc)
        {
            base.Update(uc);

            if (VertextItem1 == null)
            {
                VertextItem1 = Parent.FindItem<ItemMapVertext>(Vertext1, oneLevel: true).FirstOrDefault();
            }

            if (VertextItem2 == null)
            {
                VertextItem2 = Parent.FindItem<ItemMapVertext>(Vertext2, oneLevel: true).FirstOrDefault();
            }

            if (VertextItem3 == null)
            {
                VertextItem3 = Parent.FindItem<ItemMapVertext>(Vertext3, oneLevel: true).FirstOrDefault();
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
                using (Pen blackPen = new Pen(Color.Black, 3))
                {
                    using (var whitePen = new Pen(Color.White, 1))
                    {
                        blackPen.StartCap = System.Drawing.Drawing2D.LineCap.Flat;
                        blackPen.EndCap = System.Drawing.Drawing2D.LineCap.Flat;
                        whitePen.StartCap = System.Drawing.Drawing2D.LineCap.Flat;
                        whitePen.EndCap = System.Drawing.Drawing2D.LineCap.Flat;
                        blackPen.LineJoin = System.Drawing.Drawing2D.LineJoin.Miter;
                        blackPen.MiterLimit = 0;
                        whitePen.LineJoin = System.Drawing.Drawing2D.LineJoin.Miter;

                        // Vertex im Weltkoordinaten transformieren
                        var p1 = pc.Transform(VertextItem1 != null ? VertextItem1.Point : new Point());
                        var p2 = pc.Transform(VertextItem2 != null ? VertextItem2.Point : new Point());
                        var p3 = pc.Transform(VertextItem3 != null ? VertextItem3.Point : new Point());

                        pc.Graphics.DrawLine(blackPen, p1, p2);
                        pc.Graphics.DrawLine(whitePen, p1, p2);
                        
                        pc.Graphics.DrawLine(blackPen, p2, p3);
                        pc.Graphics.DrawLine(whitePen, p2, p3);
                        
                        pc.Graphics.DrawLine(blackPen, p3, p1);
                        pc.Graphics.DrawLine(whitePen, p3, p1);

                        var centroid = Centroid;
                        var points = new PointF[]
                        {
                            new Point(centroid.X, centroid.Y - 3),
                            new Point(centroid.X + 4, centroid.Y + 4),
                            new Point(centroid.X - 4, centroid.Y + 4)
                        };

                        using (var whiteBrush = new SolidBrush(Color.White))
                        {
                            pc.Graphics.FillPolygon(whiteBrush, points);
                        }

                        blackPen.Width = 1;
                        pc.Graphics.DrawPolygon(blackPen, points);
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
            

            return copy;
        }

        /// <summary>
        /// Liefert oder setzt den Namen des ersten Vertext
        /// </summary>
        [XmlAttribute("vertext1")]
        public string Vertext1
        {
            get { return m_vertext1; }
            set
            {
                if (m_vertext1 == null && value != null || m_vertext1 != null && value == null  || !m_vertext1.Equals(value))
                {
                    m_vertext1 = value;

                    NotifyPropertyChanged();
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

                    NotifyPropertyChanged();
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

                    NotifyPropertyChanged();
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

                var x = (VertextItem1.Point.X + VertextItem2.Point.X + VertextItem3.Point.X) / 3f;
                var y = (VertextItem1.Point.Y + VertextItem2.Point.Y + VertextItem3.Point.Y) / 3f;

                return new Point((int)x, (int)y);
            }
        }
    }
}
