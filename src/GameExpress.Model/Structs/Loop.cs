using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace GameExpress.Model.Structs
{
    [XmlType("loop")]
    public enum Loop
    {
        /// <summary>
        /// Keine Schleife
        /// </summary>
        [XmlEnum("none")]
        None,

        /// <summary>
        /// Standard
        /// </summary>
        [XmlEnum("default")]
        Default,

        /// <summary>
        /// Schwingen
        /// </summary>
        [XmlEnum("oscillate")]
        Oscillate,
    }
}
