using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.ComponentModel;
using System.Drawing.Imaging;
using System.Xml.Serialization;

namespace GameExpress.Core.Items
{
    [XmlType("image")]
    public class ItemVisualImage : ItemVisual
    {
        /// <summary>
        /// Das Bild
        /// </summary>
        private Image m_image = null;

        /// <summary>
        /// Die Bildquelle
        /// </summary>
        private string m_source = string.Empty;

        /// <summary>
        /// Konstruktor
        /// </summary>
        public ItemVisualImage()
            : this(Project.ItemContextList.GetItemContext(typeof(ItemVisualImage)))
        {
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="context">Das zugehörige Kontextobjekt</param>
        public ItemVisualImage(ItemContext context)
            :base(context, true)
        {
        }

        /// <summary>
        /// Objekt darstllen
        /// </summary>
        /// <param name="pc"></param>
        public override void Presentation(Structs.PresentationContext pc)
        {
            // Nichts zum zeichnen vorhanden
	        if (m_image == null) return;
            
            base.Presentation(pc);

            ImageAttributes imageAtt = new ImageAttributes();	
	        Point[] destPoints = { new Point(0, 0), new Point(m_image.Width, 0), new Point(0, m_image.Height) };

	        // Punkte transformieren
	        pc.Transform(destPoints);
	    
            // Bildattribute bestimmen
	        pc.SetImageArrtibut(imageAtt);
	
	        // Bild zeichnen
            pc.Graphics.DrawImage(m_image, destPoints, new Rectangle(0, 0, m_image.Width, m_image.Height), GraphicsUnit.Pixel, imageAtt);

	        // Hotspot zeichnen
            DrawHotspot(pc);
        }

        /// <summary>
        /// Liefert eine Tiefernkopie des Items
        /// </summary>
        /// <returns>Die Tiefenkopie</returns>
        public override IItem Copy()
        {
            var copy = base.Copy() as ItemVisualImage;
            copy.Image = Image;
            copy.Source = Source;

            return copy;
        }

        /// <summary>
        /// Liefert oder setzt das Bild
        /// </summary>
        [XmlIgnore]
        public Image Image
        {
            get { return m_image; }
            set 
            { 
                m_image = value;

                NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Liefert oder setzt die Bildquelle (Pfad+Dateiname)
        /// </summary>
        [Category("Darstellung"), DisplayName("Bild"), Description("Geben Sie hier das Bild an.")]
        [XmlAttribute("source")]
        public string Source
        {
            get { return m_source; }
            set
            {
                m_source = value;

                NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Liefert die Größe
        /// </summary>
        [Category("Darstellung"), DisplayName("Größe"), Description("Liefert die Größe des Objektes")]
        [XmlIgnore]
        public override Size Size 
        { 
            get
            {
                return m_image != null ? m_image.Size : new Size();
            } 
        }
    }
}
