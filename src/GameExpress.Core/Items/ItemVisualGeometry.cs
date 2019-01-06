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
    public abstract class ItemVisualGeometry : ItemVisual
    {
        uint m_width;
        uint m_heigt;
        Color m_fontColor;
        Color m_backColor;
        float m_strokeWidth;
        Brush m_backgroundBrush;

        /// <summary>
        /// Konstruktor
        /// </summary>
        public ItemVisualGeometry()
            : this(Project.ItemContextList.GetItemContext(typeof(ItemVisualGeometry)))
        {
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="context">Das zugehörige Kontextobjekt</param>
        public ItemVisualGeometry(ItemContext context)
            : base(context, true)
        {
        }

        /// <summary>
        /// Objekt darstllen
        /// </summary>
        /// <param name="pc"></param>
        public override void Presentation(Structs.PresentationContext pc)
        {
            base.Presentation(pc);

            DrawGeometry(pc);

            // Hotspot zeichnen
            DrawHotspot(pc);
        }

        /// <summary>
        /// Objekt darstllen
        /// </summary>
        /// <param name="pc"></param>
        public abstract void DrawGeometry(Structs.PresentationContext pc);

        /// <summary>
        /// Liefert eine Tiefernkopie des Items
        /// </summary>
        /// <returns>Die Tiefenkopie</returns>
        public override IItem Copy()
        {
            var copy = base.Copy() as ItemVisualGeometry;
            copy.FrontColor = FrontColor;
            copy.BackColor = BackColor;
            copy.Width = Width;
            copy.Heigt = Heigt;
            copy.StrokeWidth = StrokeWidth;
            copy.Background = Background;

            return copy;
        }

        /// <summary>
        /// Liefert oder setzt die Vordergrundfarbe
        /// </summary>
        [Category("Darstellung"), Description("Geben Sie hier die Vordergrundfarbe an.")]
        [XmlAttribute("frontcolor")]
        public Color FrontColor
        {
            get { return m_fontColor; }
            set
            {
                if (!m_fontColor.Equals(value))
                {
                    m_fontColor = value;

                    NotifyPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Liefert oder setzt die Hintergrundfarbe
        /// </summary>
        [Category("Darstellung"), Description("Geben Sie hier die Hintergrundfarbe an.")]
        [XmlAttribute("backcolor")]
        public Color BackColor
        {
            get { return m_backColor; }
            set
            {
                if (!m_backColor.Equals(value))
                {
                    m_backColor = value;

                    NotifyPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Liefert oder setzt die Weite des Objektes
        /// </summary>
        [Category("Darstellung"), Description("Liefert die Weite des Objektes")]
        [XmlAttribute("width")]
        public virtual uint Width
        {
            get { return m_width; }
            set
            {
                if (!m_width.Equals(value))
                {
                    m_width = value;

                    NotifyPropertyChanged();
                }
            }
        }
        
        /// <summary>
        /// Liefert óder setzt die Höhe des Objekts
        /// </summary>
        [Category("Darstellung"), Description("Liefert oder setzt die Höhe des Objektes")]
        [XmlAttribute("heigt")]
        public virtual uint Heigt
        {
            get { return m_heigt; }
            set
            {
                if (!m_heigt.Equals(value))
                {
                    m_heigt = value;

                    NotifyPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Liefert oder setzt die Strichstärke 
        /// </summary>
        [Category("Darstellung"), DisplayName("Strichstärke"), Description("Liefert oder setzt die Strichstärke")]
        [XmlAttribute("strokewidth")]
        public virtual float StrokeWidth
        {
            get { return m_strokeWidth; }
            set
            {
                if (!m_strokeWidth.Equals(value))
                {
                    m_strokeWidth = value;

                    NotifyPropertyChanged();
                }
            }
        }
        
        /// <summary>
        /// Liefert oder setzt die Hintergrund
        /// </summary>
        [Category("Darstellung"), DisplayName("Hintergrund"), Description("Liefert oder setzt den Hintergrund")]
        [Editor(typeof(UIEditor.BrushUITypeEditor), typeof(System.Drawing.Design.UITypeEditor))]
        [XmlElement("background")]
        public virtual Brush Background
        {
            get { return m_backgroundBrush; }
            set
            {
                if (m_backgroundBrush == null || !m_backgroundBrush.Equals(value))
                {
                    m_backgroundBrush = value;

                    NotifyPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Liefert die Größe
        /// </summary>
        [Category("Darstellung"), Description("Liefert die Größe des Objektes")]
        [XmlIgnore]
        public override Size Size { get { return new Size((int)Width, (int)Heigt); } }
    }
}
