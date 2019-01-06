using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameExpress.Editor.Pages
{
    public class RetrieveItemSizeEventArgs : EventArgs
    {
        /// <summary>
        /// Liefert oder setzt die Größe des Items
        /// </summary>
        public Size Size { get; set; }
    }
}
