using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Xml.Serialization;

namespace GameExpress.Core.Structs
{
    /// <summary>
    /// Gamma
    /// </summary>
    [Serializable(),
    TypeConverterAttribute(typeof(Converter.GammaTypeConverter))]
    [XmlType("gamma")]
    public struct Gamma
    {
        /// <summary>
        /// Gamma-Wert
        /// </summary>
        float m_gamma;
        
        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="g">Der Gammmawert</param>
        public Gamma(float g)
        {
            m_gamma = g;
        }

        /// <summary>
        /// Implizite benutzerdefinierte Typkonvertierungsoperation 
        /// </summary>
        /// <param name="gamma">Der Gammawert</param>
        static public implicit operator float(Gamma gamma)
        {
            return gamma.m_gamma;
        }

        /// <summary>
        /// Implizite benutzerdefinierte Typkonvertierungsoperation 
        /// </summary>
        /// <param name="gamma">Der Gammawert</param>
        static public implicit operator Gamma(float alpha)
        {
            return new Gamma(alpha);
        }

        /// <summary>
        /// In String umwandeln
        /// </summary>
        /// <returns>Das Objet in seiner Stringrepräsentation</returns>
        public override string ToString()
        {
            return m_gamma.ToString();
        }

        /// <summary>
        /// Die Value-Eigenschaft
        /// </summary>
        [XmlAttribute("value")]
        public float Value
        {
            get { return m_gamma; }
            set { m_gamma = value; }
        }

    }
}
