using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameExpress.Model.Item
{
    /// <summary>
    /// Kennzeichnet das Item als skalierbar
    /// </summary>
    public interface IItemScale
    {
        /// <summary>
        /// Liefert oder setzt die Skalierung der x-Achse
        /// </summary>
        double ScaleX { get; set; }

        /// <summary>
        /// Liefert oder setzt die Skalierung der y-Achse
        /// </summary>
        double ScaleY { get; set; }
    }
}
