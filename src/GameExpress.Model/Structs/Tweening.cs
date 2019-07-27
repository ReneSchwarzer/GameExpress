using System.Xml.Serialization;

namespace GameExpress.Model.Structs
{
    public class Tweening
    {
        /// <summary>
        /// Tweening ein / aus
        /// </summary>
        [XmlAttribute("enable")]
        public bool Enable { get; set; }
    }
}
