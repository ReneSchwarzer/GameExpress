using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.ComponentModel;
using System.Drawing.Imaging;
using System.Xml.Serialization;

namespace GameExpress.Core.Items
{
    public abstract class ItemVisual : Item
    {
        /// <summary>
        /// Der Hotspot
        /// </summary>
        private Point m_hotspot = new Point();

        /// <summary>
        /// Die transparente Farbe
        /// </summary>
        private Structs.Transparency m_transparency; 

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
        /// Konstruktor
        /// </summary>
        /// <param name="context">Das zugehörige Kontextobjekt</param>
        public ItemVisual(ItemContext context, bool autoGUID)
            :base(context, autoGUID)
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
	        pc.AddAlpha(m_alpha);
	        pc.AddGamma(m_gamma);
	        pc.AddHue(m_hue);
	        pc.AddTransparency(m_transparency);
        }

        /// <summary>
        /// Zeichnet den Hotspot
        /// </summary>
        /// <param name="pc">Der Präsentationskontext</param>
        protected void DrawHotspot(Structs.PresentationContext pc)
        {
            if (!pc.Designer)
            {
                return;
            }

            // Hotspot zeichnen
            Point p = pc.Transform(new Point(Hotspot.X, Hotspot.Y));

            // Create a Pen object.
            using (Pen blackPen = new Pen(Color.FromArgb(200, 0, 0, 0), 3))
            {
                using (Pen whitePen = new Pen(Color.FromArgb(200, 255, 255, 255), 1))
                {
                    // Linienübergänge
                    blackPen.StartCap = System.Drawing.Drawing2D.LineCap.Flat;
                    blackPen.EndCap = System.Drawing.Drawing2D.LineCap.Flat;
                    whitePen.StartCap = System.Drawing.Drawing2D.LineCap.Flat;
                    whitePen.EndCap = System.Drawing.Drawing2D.LineCap.Flat;

                    if (pc.Level == 1)
                    {
                        pc.Graphics.DrawLine(blackPen, p.X - 5, p.Y, p.X + 6, p.Y);
                        pc.Graphics.DrawLine(blackPen, p.X, p.Y - 5, p.X, p.Y + 6);

                        pc.Graphics.DrawLine(whitePen, p.X - 5, p.Y, p.X + 6, p.Y);
                        pc.Graphics.DrawLine(whitePen, p.X, p.Y - 5, p.X, p.Y + 6);
                    }
                    else if (pc.Level == 2)
                    {
                        blackPen.Color = Color.FromArgb(125, 0, 0, 0);
                        pc.Graphics.DrawEllipse(blackPen, p.X - 3, p.Y - 3, 6, 6);
                        whitePen.Color = Color.FromArgb(255, 255, 255, 255);
                        pc.Graphics.DrawEllipse(whitePen, p.X - 2, p.Y - 2, 4, 4);
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
            var copy = base.Copy() as ItemVisual;
            copy.Hotspot = Hotspot;
            copy.Transparency = Transparency;
            copy.Gamma = Gamma;
            copy.Alpha = Alpha;
            copy.Hue = Hue;
            
            return copy;
        }

        /// <summary>
        /// Liefert oder setzt den HotSpot
        /// </summary>
        [Category("Darstellung"), Description("Geben Sie hier den HotSpot an.")]
        [XmlElement("hotspot")]
        public Point Hotspot
        {
            get { return m_hotspot; }
            set 
            {
                if (!m_hotspot.Equals(value))
                {
                    m_hotspot = value;

                    NotifyPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Liefert oder setzt die Transparenz-Eigenschaft
        /// </summary>
        [Category("Darstellung"), DisplayName("Transparenz"), Description("Geben Sie hier den transparenten Farbschlüssel an.")]
        [XmlElement("transparency")]
        public Structs.Transparency Transparency
        {
            get { return m_transparency; }
            set 
            {
                if (!m_transparency.Equals(value))
                {
                    m_transparency = value;

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
        /// Liefert die Größe
        /// </summary>
        [Category("Darstellung"), Description("Liefert die Größe des Objektes")]
        [XmlIgnore]
        public abstract Size Size { get; }
        
    }
}
