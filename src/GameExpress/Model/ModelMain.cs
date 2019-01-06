using GameExpress.Core;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameExpress.Model
{
    public class ModelMain : IModelMain
    {
        /// <summary>
        /// Liefert oder setzt die letzte Position des Hauptfensters
        /// </summary>
        public Point LastWindowsPos
        {
            get { return global::GameExpress.Properties.Settings.Default.LastWindowsPos; }
            set
            {
                var pos = global::GameExpress.Properties.Settings.Default.LastWindowsPos;
                if (pos != value)
                {
                    global::GameExpress.Properties.Settings.Default.LastWindowsPos = value;
                    global::GameExpress.Properties.Settings.Default.Save();
                }
            }
        }

        /// <summary>
        /// Liefert oder setzt die letzte Größe des Hauptfensters
        /// </summary>
        public Size LastWindowSize
        {
            get { return global::GameExpress.Properties.Settings.Default.LastWindowSize; }
            set
            {
                var pos = global::GameExpress.Properties.Settings.Default.LastWindowSize;
                if (pos != value)
                {
                    global::GameExpress.Properties.Settings.Default.LastWindowSize = value;
                    global::GameExpress.Properties.Settings.Default.Save();
                }
            }
        }

        /// <summary>
        /// Liefert oder setzt den letzten Status des Hauptfensters
        /// </summary>
        public FormWindowState LastWindowState
        {
            get { return global::GameExpress.Properties.Settings.Default.LastWindowState; }
            set
            {
                var pos = global::GameExpress.Properties.Settings.Default.LastWindowState;
                if (pos != value)
                {
                    global::GameExpress.Properties.Settings.Default.LastWindowState = value;
                    global::GameExpress.Properties.Settings.Default.Save();
                }
            }
        }

        /// <summary>
        /// Liefert oder setzt das Projekt
        /// </summary>
        public IProject Project { get; set; }
    }
}
