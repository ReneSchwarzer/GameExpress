using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameExpress.Model.Item
{
    /// <summary>
    /// Kennzeichnet das Item als verschiebbar
    /// </summary>
    public interface IItemTranslation
    {
        /// <summary>
        /// Liefert oder setzt die Verschiebung der x-Achse entlang
        /// </summary>
        short TranslationX { get; set; }

        /// <summary>
        /// Liefert oder setzt die Verschiebung der y-Achse entlang
        /// </summary>
        short TranslationY { get; set; }
    }
}
