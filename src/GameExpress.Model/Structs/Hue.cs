using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace GameExpress.Model.Structs
{
    /// <summary>
    /// Farbton
    /// </summary>
    [XmlType("hue")]
    public struct Hue
    {
        /// <summary>
        /// Farbton
        /// </summary>
        private Color m_color;

        /// <summary>
        /// Farbtonalpha
        /// </summary>
        private Alpha m_alpha;

        /// <summary>
        /// Farbton ein / aus
        /// </summary>
        private bool m_enable;

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="c">Die Farbe</param>
        /// <param name="a">Der Alpha-Wert</param>
        /// <param name="enable">Schaltet den Fabton ein oder aus</param>
        public Hue(Color c, byte a, bool enable)
        {
            m_color = c;
            m_alpha = a;
            m_enable = enable;
        }

        /// <summary>
        /// Die Farbegenschaft
        /// </summary>
        [XmlAttribute("color")]
        public Color Color
        {
            get { return m_color; }
            set { m_color = value; }
        }

        /// <summary>
        /// Die Transparenz ein/aus-Eigenschaft
        /// </summary>
        [XmlAttribute("enable")]
        public bool Enable
        {
            get { return m_enable; }
            set { m_enable = value; }
        }

        /// <summary>
        /// Die Alpha-Eigenschaft
        /// </summary>
        [XmlAttribute("alpha")]
        public Alpha Alpha
        {
            get { return m_alpha; }
            set { m_alpha = value; }
        }
    }
}
