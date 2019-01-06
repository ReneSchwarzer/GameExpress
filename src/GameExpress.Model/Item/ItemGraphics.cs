using GameExpress.Model.Structs;
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
    /// Basisklasse von Grafiken
    /// </summary>
    [XmlInclude(typeof(ItemScene))]
    [XmlInclude(typeof(ItemObject))]
    [XmlInclude(typeof(ItemImage))]
    [XmlType("graphics")]
    public abstract class ItemGraphics : ItemVisual
    {
        /// <summary>
        /// Der Hotspot
        /// </summary>
        private Hotspot m_hotspot = new Hotspot(0, 0);

        /// <summary>
        /// Der Gammawert von 0-1
        /// </summary>
        private Gamma m_gamma;

        /// <summary>
        /// Der Alphawert von 0-255
        /// </summary>
        private Alpha m_alpha;

        /// <summary>
        /// Die Unschärfe von 0-255
        /// </summary>
        private Blur m_blur;

        /// <summary>
        /// Der Farbton
        /// </summary>
        private Hue m_hue;

        /// <summary>
        /// Konstruktor
        /// </summary>
        public ItemGraphics()
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

        }

        /// <summary>
        /// Objekt darstllen
        /// </summary>
        /// <param name="pc">Der Präsentationskontext</param>
        public override void Presentation(PresentationContext pc)
        {
            pc.AddAlpha(m_alpha);
            pc.AddBlur(m_blur);
            pc.AddGamma(m_gamma);
            pc.AddHue(m_hue);
        }

        /// <summary>
        /// Zeichnet den Hotspot
        /// </summary>
        /// <param name="pc">Der Präsentationskontext</param>
        protected void DrawHotspot(PresentationContext pc)
        {
            if (!pc.Designer)
            {
                return;
            }

            // Hotspot zeichnen
            var p = pc.Transform(new Point(Hotspot.X, Hotspot.Y));

            var black = Color.FromArgb(200, 0, 0, 0);
            var white = Color.FromArgb(200, 255, 255, 255);

            if (pc.Level == 1)
            {
                pc.Graphics.DrawLine((float)p.X - 5, (float)p.Y, (float)p.X + 6, (float)p.Y, black, 3);
                pc.Graphics.DrawLine((float)p.X, (float)p.Y - 5, (float)p.X, (float)p.Y + 6, black, 3);
                pc.Graphics.DrawLine((float)p.X - 5, (float)p.Y, (float)p.X + 6, (float)p.Y, white, 1);
                pc.Graphics.DrawLine((float)p.X, (float)p.Y - 5, (float)p.X, (float)p.Y + 6, white, 1);
            }
            else if (pc.Level == 2)
            {
                pc.Graphics.DrawEllipse((float)p.X - 3, (float)p.Y - 3, 6, 6, black);
                pc.Graphics.DrawEllipse((float)p.X - 2, (float)p.Y - 2, 4, 4, white);
            }
        }

        /// <summary>
        /// Liefert eine Tiefernkopie des Items
        /// </summary>
        /// <returns>Die Tiefenkopie</returns>
        public override T Copy<T>()
        {
            var copy = base.Copy<T>() as ItemGraphics;
            copy.Hotspot = Hotspot;
            copy.Gamma = Gamma;
            copy.Alpha = Alpha;
            copy.Hue = Hue;

            return copy as T;
        }

        /// <summary>
        /// Liefert oder setzt den HotSpot
        /// </summary>
        [XmlElement("hotspot")]
        public Hotspot Hotspot
        {
            get { return m_hotspot; }
            set
            {
                if (!m_hotspot.Equals(value))
                {
                    m_hotspot = value;

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
        /// Liefert oder setzt den Alphawert
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
        /// Liefert oder setzt den Unschärfewert
        /// </summary>
        [XmlElement("blur")]
        public Blur Blur
        {
            get { return m_blur; }
            set
            {
                if (!m_blur.Equals(value))
                {
                    m_blur = value;

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

    }
}
