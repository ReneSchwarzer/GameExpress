using GameExpress.Core.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameExpress.Editor.Pages
{
    /// <summary>
    /// Eventargument für das ChangedTime-Event
    /// </summary>
    public class ChangedTimeEventArgs : EventArgs
    {
        /// <summary>
        /// Liefert oder setzt die Zeit
        /// </summary>
        public ulong Time { get; set; }

        /// <summary>
        /// Liefert oder setzt die neue Auswahl
        /// </summary>
        public ItemVisualInstance Item { get; set; }

        /// <summary>
        /// Liefert oder setzt die neue Auswahl
        /// </summary>
        public ItemVisualKeyFrame KeyFrame { get; set; }
    }
}
