using GameExpress.Core.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameExpress.Editor.Pages
{
    /// <summary>
    /// Eventargument für das ChanegedSelectetItem-Event
    /// </summary>
    public class ChanegedSelectetItemArgs : EventArgs
    {
        /// <summary>
        /// Liefert oder setzt die neue Auswahl
        /// </summary>
        public ItemVisualInstance Item { get; set; }
    }
}
