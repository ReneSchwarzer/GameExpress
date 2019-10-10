using GameExpress.Model.Structs;
using System.Xml.Serialization;
using Windows.Foundation;

namespace GameExpress.Model.Item
{
    /// <summary>
    /// Kennzeichnung als sichtbares Item
    /// </summary>
    public interface IItemClipping
    {
        /// <summary>
        /// Abschneiden der Ausgabe auf Größe des Objektes
        /// </summary>
        [XmlAttribute("clipping")]
        bool Clipping { get; set; }
    }
}
