using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.ComponentModel;
using System.Drawing.Imaging;
using System.Xml.Serialization;

namespace GameExpress.Core.Items
{
    [XmlType("object")]
    public class ItemVisualObject : ItemVisual
    {
        /// <summary>
        /// Das Bild
        /// </summary>
        private Image m_image = null;

        /// <summary>
        /// Konstruktor
        /// </summary>
        public ItemVisualObject()
            : this(Project.ItemContextList.GetItemContext(typeof(ItemVisualObject)))
        {
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="context">Das zugehörige Kontextobjekt</param>
        public ItemVisualObject(ItemContext context)
            :base(context, true)
        {
        }

        /// <summary>
        /// Objekt darstllen
        /// </summary>
        /// <param name="pc">Präsentationskontext</param>
        public override void Presentation(Structs.PresentationContext pc)
        {
            //pe.Graphics.DrawImage(item.Image, new Point());

            // Nichts zum zeichnen vorhanden
	        if (m_image == null) return;

            ImageAttributes imageAtt = new ImageAttributes();	
	        Point[] destPoints = { new Point(0, 0), new Point(m_image.Width, 0), new Point(0, m_image.Height) };

	        //pc.AddAlpha(m_alpha);
	        //pc.AddGamma(m_gamma);
	        //pc.AddHue(m_hue);
	        //pc.AddTransparency(m_transparency);
	
	        // Punkte transformieren
	        pc.Transform(destPoints);
	    
            // Bildattribute bestimmen
	        pc.SetImageArrtibut(imageAtt);
	
	        // Bild zeichnen
            pc.Graphics.DrawImage(m_image, destPoints, new Rectangle(0, 0, m_image.Width, m_image.Height), GraphicsUnit.Pixel, imageAtt);

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
                        pc.Graphics.DrawLine(blackPen, p.X-5, p.Y, p.X+6, p.Y);
                        pc.Graphics.DrawLine(blackPen, p.X, p.Y-5, p.X, p.Y+6);

                        pc.Graphics.DrawLine(whitePen, p.X-5, p.Y, p.X+6, p.Y);
                        pc.Graphics.DrawLine(whitePen, p.X, p.Y-5, p.X, p.Y+6);
                    } 
                    else if (pc.Level == 2) 
                    {
                        blackPen.Color = Color.FromArgb(125, 0, 0, 0);
                        pc.Graphics.DrawEllipse(blackPen, p.X-3, p.Y-3, 6, 6);
                        whitePen.Color = Color.FromArgb(255, 255, 255, 255);
                        pc.Graphics.DrawEllipse(whitePen, p.X-2, p.Y-2, 4, 4);
                    }
                }
            }           
        }

        /// <summary>
        /// Liefert oder setzt die Bildeigenschaft
        /// </summary>
        [Category("Darstellung"), Description("Geben Sie hier das Bild an.")]
        public Image Image
        {
            get { return m_image; }
            set
            {
                if (m_image != value)
                {
                    m_image = value;

                    NotifyPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Liefert die Größe
        /// </summary>
        [Category("Darstellung"), Description("Liefert die Größe des Objektes")]
        public override Size Size
        {
            get
            {
                return new Size();
            }
        }
    }
}
