using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Runtime.InteropServices;
using System.ComponentModel;
using System.Xml.Serialization;

namespace GameExpress.Core.Structs
{
    /// <summary>
    /// Transparenz
    /// </summary>
    [Serializable(), StructLayout(LayoutKind.Sequential), ComVisible(true),
    Editor(typeof(UIEditor.TransparencyUITypeEditor), typeof(System.Drawing.Design.UITypeEditor)),
    TypeConverterAttribute(typeof(Converter.TransparencyTypeConverter))]
    [XmlType("map")]
    public struct Transparency
    {
        /// <summary>
        /// Transparente Farbe
        /// </summary>
        private Color m_color;

        /// <summary>
        /// Transparenz ein / aus
        /// </summary>
	    private bool  m_enable;
	
        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="c">Die transparente Farbe</param>
        /// <param name="enable">Tranzparenz wird verwendet</param>
	    public Transparency(Color c, bool enable)
        {
            m_color = c;
            m_enable = enable;
        }

        /// <summary>
        /// Kopier-Konstruktor
        /// </summary>
        /// <param name="t">Das zu kopierende Objekt</param>
        public Transparency(Transparency t)
        {
            m_color = t.m_color;
            m_enable = t.m_enable;
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

    }
}
