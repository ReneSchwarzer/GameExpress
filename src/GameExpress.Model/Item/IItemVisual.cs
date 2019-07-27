using GameExpress.Model.Structs;
using System.Xml.Serialization;
using Windows.Foundation;

namespace GameExpress.Model.Item
{
    /// <summary>
    /// Kennzeichnung als sichtbares Item
    /// </summary>
    public interface IItemVisual
    {
        /// <summary>
        /// Liefert die Größe
        /// </summary>
        Size Size { get; }

        /// <summary>
        /// Liefert die Anzeigematrix des Items
        /// </summary>
        /// <returns>Die Matrix mit allen Transformationen des Items</returns>
        Matrix3D GetMatrix();
    }
}
