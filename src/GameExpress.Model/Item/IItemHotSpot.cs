using GameExpress.Model.Structs;
using System.Xml.Serialization;
using Windows.Foundation;

namespace GameExpress.Model.Item
{
    /// <summary>
    /// Kennzeichnung, dass das Item einen Hotspot besitzt
    /// </summary>
    public interface IItemHotSpot
    {
        /// <summary>
        /// Liefert oder setzt den HotSpot
        /// </summary>
        Hotspot Hotspot { get; set; }
    }
}
