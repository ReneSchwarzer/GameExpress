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
        /// Am Ende einfrieren
        /// </summary>
        [XmlEnum("freeze")]
        Freeze,

        /// <summary>
        /// Wiederholung
        /// </summary>
        [XmlEnum("repeat")]
        Repeat,

        /// <summary>
        /// Schwingen
        /// </summary>
        [XmlEnum("oscillate")]
        Oscillate,
    }
}
