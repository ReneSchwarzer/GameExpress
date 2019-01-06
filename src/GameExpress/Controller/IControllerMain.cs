using GameExpress.Controls;
using GameExpress.Core;
using GameExpress.Editor.Pages;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static GameExpress.Editor.Pages.ItemPage;

namespace GameExpress.Controller
{
    public interface IControllerMain : IController
    {
        /// <summary>
        /// Event zum Mitteilen, dass sich Namenseigenschaften geändert haben
        /// </summary>
        event EventHandler<TreeViewPathCollection> ChangedProjectTree;

        /// <summary>
        /// Event zum Mitteilen, dass sich das aktive Item geändert hat
        /// </summary>
        event EventHandler<ChangeActiveItemEventArgs> ChangeActiveItemEvent;

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
        /// Erstellt ein Projekt
        /// </summary>
        IProject CreateProject();

        /// <summary>
        /// Liefert den Projektbaum
        /// </summary>
        ICollection<TreeViewPathCollection> ProjectTree { get; }

        /// <summary>
        /// Lädt das Projekt
        /// </summary>
        /// <param name="file">Der Dateiname inklusive Pfad</param>
        void LoadProject(string file);

        /// <summary>
        /// Speichert das Projekt
        /// </summary>
        /// <param name="file">Der Dateiname inklusive Pfad</param>
        void SaveProject(string file);
    }
}
