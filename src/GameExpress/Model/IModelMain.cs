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
    public interface IModelMain : IModel
    {
        /// <summary>
        /// Liefert oder setzt die letzte Position des Hauptfensters
        /// </summary>
        Point LastWindowsPos { get; set; }

        /// <summary>
        /// Liefert oder setzt die letzte Größe des Hauptfensters
        /// </summary>
        Size LastWindowSize { get; set; }

        /// <summary>
        /// Liefert oder setzt den letzten Status des Hauptfensters
        /// </summary>
        FormWindowState LastWindowState { get; set; }

        /// <summary>
        /// Liefert oder setzt das Projekt
        /// </summary>
        IProject Project { get; set; }
    }
}
