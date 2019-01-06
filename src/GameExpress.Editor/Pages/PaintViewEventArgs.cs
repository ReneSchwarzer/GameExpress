using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameExpress.Editor.Pages
{
    public class PaintViewEventArgs : EventArgs
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public PaintViewEventArgs()
        {
            ViewArea = new Rectangle();
        }

        /// <summary>
        /// Größe der ViewArea
        /// </summary>
        public Rectangle ViewArea { get; set; }

        /// <summary>
        /// Weite der ViewArea ermitteln
        /// </summary>
        public int Width { get { return ViewArea.Width; } }

        /// <summary>
        /// Höhe der ViewArea ermitteln
        /// </summary>
        public int Height { get { return ViewArea.Height; } }

        /// <summary>
        /// Liefert oder setzt die Grafikeinheit
        /// </summary>
        public Graphics Graphics { get; set; }

        /// <summary>
        /// Liefert oder setzt den Zoom
        /// </summary>
        public float Zoom { get; set; }
    }
}
