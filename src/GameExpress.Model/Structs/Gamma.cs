using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace GameExpress.Model.Structs
{
    /// <summary>
    /// Gamma
    /// </summary>
    [XmlType("gamma")]
    public struct Gamma
    {
        /// <summary>
        /// Die Value-Eigenschaft
        /// </summary>
        [XmlAttribute("value")]
        public byte Value { get; set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="gamma">Der Gammmawert</param>
        public Gamma(byte gamma)
        {
            Value = gamma;
        }

        /// <summary>
        /// Implizite benutzerdefinierte Typkonvertierungsoperation 
        /// </summary>
        /// <param name="gamma">Der Gammawert</param>
        static public implicit operator float(Gamma gamma)
        {
            return gamma.Value;
        }

        /// <summary>
        /// Implizite benutzerdefinierte Typkonvertierungsoperation 
        /// </summary>
        /// <param name="gamma">Der Gammawert</param>
        static public implicit operator Gamma(byte alpha)
        {
            return new Gamma(alpha);
        }

        /// <summary>
        /// Alphawert hinzufügen
        /// </summary>
        /// <param name="gamma">Der Unschärfewert</param>
        public void Add(Gamma gamma)
        {
            float f = Value + ((255.0f - (float)this) * ((float)gamma / 255.0f));
            if (f > 255) f = 255;

            Value = (byte)f;
        }

        /// <summary>
        /// In String umwandeln
        /// </summary>
        /// <returns>Das Objet in seiner Stringrepräsentation</returns>
        public override string ToString()
        {
            return Value.ToString();
        }
    }
}
