using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Xml.Serialization;

namespace GameExpress.Core.Structs
{
    /// <summary>
    /// Alpha
    /// </summary>
    [Serializable(),
    TypeConverterAttribute(typeof(Converter.AlphaTypeConverter))]
    [XmlType("alpha")]
    public struct Alpha
    {
        /// <summary>
        /// Alpha
        /// </summary>
        private byte m_alpha;
        
        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="a">Der Alphawert</param>
        public Alpha(byte a)
        {
            m_alpha = a;
        }

        /// <summary>
        /// Implizite benutzerdefinierte Typkonvertierungsoperation
        /// </summary>
        /// <param name="alpha">Der Alphawert</param>
        /// <returns>Der umgewandelte Alphawert</returns>
        static public implicit operator byte(Alpha alpha)
        {
            return alpha.m_alpha;
        }

        /// <summary>
        /// Implizite benutzerdefinierte Typkonvertierungsoperation 
        /// </summary>
        /// <param name="alpha">Der Alphawert</param>
        /// <returns>Der umgewandelte Alphawert</returns>
        static public implicit operator Alpha(byte alpha)
        {
            return new Alpha(alpha);
        }

        /// <summary>
        /// Alphawert hinzufügen
        /// </summary>
        /// <param name="alpha">Der Alphawert</param>
        public void Add(Alpha a) 
        {
            float f = this.m_alpha + ((255.0f - (float)this) * ((float)a / 255.0f));
            if (f > 255) f = 255;

            m_alpha = (byte)f;
        }

        /// <summary>
        /// In String umwandeln
        /// </summary>
        /// <returns>ein String</returns>
        public override string ToString()
        {
            return m_alpha.ToString();
        }

        /// <summary>
        /// Die Value-Eigenschaft
        /// </summary>
        [XmlAttribute("value")]
        private byte Value
        {
            get { return m_alpha; }
            set { m_alpha = value; }
        }
    }
}
