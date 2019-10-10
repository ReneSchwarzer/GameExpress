using GameExpress.Model.Structs;
using System.Xml.Serialization;
using Windows.Foundation;

namespace GameExpress.Model.Item
{
    /// <summary>
    /// Kennzeichnung als Item mit einer Größe
    /// </summary>
    public interface IItemSizing
    {
        /// <summary>
        /// Liefert die Größe
        /// </summary>
        Vector Size { get; }
    }
}
