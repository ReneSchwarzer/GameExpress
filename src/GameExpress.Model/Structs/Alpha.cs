using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace GameExpress.Model.Structs
{
    /// <summary>
    /// Alpha
    /// </summary>
    [XmlType("alpha")]
    public struct Alpha
    {
        /// <summary>
        /// Die Value-Eigenschaft
        /// </summary>
        [XmlAttribute("value")]
        public byte Value { get; set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="value">Der Wert</param>
        public Alpha(string value)
        {
            Value = Convert.ToByte(value);
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="alpha">Der Alphawert</param>
        public Alpha(byte alpha)
        {
            Value = alpha;
        }

        /// <summary>
        /// Implizite benutzerdefinierte Typkonvertierungsoperation
        /// </summary>
        /// <param name="alpha">Der Alphawert</param>
        /// <returns>Der umgewandelte Alphawert</returns>
        static public implicit operator byte(Alpha alpha)
        {
            return alpha.Value;
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
            float f = this.Value + ((255.0f - (float)this) * ((float)a / 255.0f));
            if (f > 255) f = 255;

            Value = (byte)f;
        }

        /// <summary>
        /// In String umwandeln
        /// </summary>
        /// <returns>Die Stringrepräsentation</returns>
        public override string ToString()
        {
            return Value.ToString();
        }
    }
}
