using GameExpress.Core.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameExpress.Editor.Pages
{
    /// <summary>
    /// Eventargumente
    /// </summary>
    public class ChangeActiveItemEventArgs : System.EventArgs
    {
        /// <summary>
        /// Liefert das Item
        /// </summary>
        public IItem Item { get; set; }
    }
}
