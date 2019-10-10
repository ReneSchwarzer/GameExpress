using GameExpress.Model.Structs;
using System.Collections.ObjectModel;
using System.Xml.Serialization;
using Windows.Foundation;

namespace GameExpress.Model.Item
{
    /// <summary>
    /// Kennzeichnung als Item mit verschiedenen Zuständen
    /// </summary>
    public interface IItemStates
    {
        /// <summary>
        /// Liefert oder setzt die Objektzustände
        /// </summary>
        [XmlIgnore]
        ObservableCollection<IItemState> States { get; set; }

    }
}
