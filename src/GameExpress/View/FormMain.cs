using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using GameExpress.Adventure;
using GameExpress.Controller;
using GameExpress.Controls;
using GameExpress.Core.Items;
using GameExpress.Editor;
using GameExpress.Editor.Pages;
using GameExpress.Model;

namespace GameExpress.View
{
    public partial class FormMain : Form, IView<IControllerMain>
    {
        /// <summary>
        /// Struktur zum kurzeitigen Zwischenspeichern der Statusnachrichten
        /// </summary>
        private Stack<StatusChangeEventArgs> StatusText { get; set; }

        /// <summary>
        /// Liefert oder setzt den Controller
        /// </summary>
        private IControllerMain Controller { get; set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        public FormMain()
        {
            InitializeComponent();

            StatusText = new Stack<StatusChangeEventArgs>();
        }

        /// <summary>
        /// Threadsicherer Zugriff
        /// </summary>
        /// <param name="a">Die auszuführende Aktion</param>
        protected void ExecuteSecure(Action a)
        {
            if (InvokeRequired)
            {
                BeginInvoke(a);
            }
            else
            {
                a();
            }
        }

        /// <summary>
        /// Verknüfpft den Controller mit der View
        /// </summary>
        /// <param name="controller">Der zugehörige Controller</param>
        public void SetController(IControllerMain controller)
        {
            Controller = controller;
        }

        /// <summary>
        /// Wird aufgerufen, wenn die Anwendung beendet werden soll
        /// </summary>
        /// <param name="sender">Der Auslöser der Nachricht</param>
        /// <param name="e">Das Eventargument</param>
        private void OnExit(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Wird aufgerufen, wenn das Formular geschlossen wird
        /// </summary>
        /// <param name="e"></param>
        protected override void OnClosing(CancelEventArgs e)
        {
            // Fenstereigenschaften merken
            Controller.LastWindowsPos = Location;
            Controller.LastWindowSize = Size;
            Controller.LastWindowState = WindowState;

            base.OnClosing(e);
        }

        /// <summary>
        /// Tritt ein, wenn der Benutzer das Formular lädt
        /// </summary>
        /// <param name="e">Das Eventargument</param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            UpdateNavButtons();
                        
            Location = Controller.LastWindowsPos;
            Size = Controller.LastWindowSize;
            WindowState = Controller.LastWindowState;

            Controller.ChangedProjectTree += (s, a) => { UpdatePage(a); };
            Controller.ChangeActiveItemEvent += OnChangeActiveItemEvent;

            Controller.CreateProject();
#if DEBUG
            //Controller.LoadProject(@"C:\Users\rene_\OneDrive\Desktop\Sandbox\Adventure.gx");
            //Controller.SaveProject(@"C:\Users\rene_\OneDrive\Desktop\Sandbox\Adventure.gx");

            foreach (var path in Controller.ProjectTree)
            {
                AddPage(path);
            }
#endif

            m_pageTreeFrame.ExpandAll();
        }

        /// <summary>
        /// Fügt eine Funktion dem Baum hinzu
        /// </summary>
        /// <param name="path">TreeViewPath</param>
        /// <param name="hotkey">Die Hotkey</param>
        private void AddPage(TreeViewPathCollection path, Keys hotkey = Keys.None)
        {
            if (path.Page != null)
            {
                path.Page.StatusChange += new EventHandler<StatusChangeEventArgs>(OnStatusChange);
            }

            m_pageTreeFrame.AddOrRefreshPage(path);

            if (path.Page != null)
            {
                var item = new ToolStripMenuItem(path.Page.Text, path.Page.Image) { Tag = path };
                item.Click += new System.EventHandler(OnCurrentPageClick);
                item.ShowShortcutKeys = true;
                if (hotkey != Keys.None)
                {
                    item.ShortcutKeys = Keys.Alt | hotkey;
                }
                m_currentPagesToolStripMenuItem.DropDownItems.Add(item);
            }
        }

        /// <summary>
        /// Aktualisiert eine Funktion im Baum
        /// </summary>
        /// <param name="path">TreeViewPath</param>
        private void UpdatePage(TreeViewPathCollection path)
        {
            m_pageTreeFrame.AddOrRefreshPage(path);
        }

        /// <summary>
        /// Aktualisiere die Ansicht der Navigationsschaltflächen
        /// </summary>
        protected void UpdateNavButtons()
        {
            m_lastToolStripButton.Enabled = m_pageTreeFrame.CanNavigateToLastPage();
            m_nextToolStripButton.Enabled = m_pageTreeFrame.CanNavigateToNextPage();
            m_nextToolStripMenuItem.Enabled = m_nextToolStripButton.Enabled;
            m_lastToolStripMenuItem.Enabled = m_lastToolStripButton.Enabled;
        }

        /// <summary>
        /// Ändert die Ansicht
        /// </summary>
        /// <param name="name">Der Name der Seite</param>
        public void ChangePage(string name)
        {
            foreach (ToolStripItem v in m_currentPagesToolStripMenuItem.DropDownItems)
            {
                if (v.Text.Equals(name, StringComparison.OrdinalIgnoreCase))
                {
                    ChangePage(v.Tag as TreeViewPathCollection, true);
                }
            }
        }

        /// <summary>
        /// Ändert die Ansicht
        /// </summary>
        /// <param name="page">Die anzuzeigene Seite</param>
        /// <param name="savePage">Gibt an, ob die Seite gesichert werden soll</param>
        private void ChangePage(TreeViewPathCollection path, bool savePage)
        {
            m_pageTreeFrame.ShowPage(path);
        }

        /// <summary>
        /// Öffnet den Dialog zum Laden eines Dokumentes
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="e">Das Eventargument</param>
        private void OnOpenDokument(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "GameExpress-Dateien (*.gx)|*.gx|Alle Dateien (*.*)|*.*";

            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                // XML laden
            }
        }

        /// <summary>
        /// Wird aufgerufen, wenn das Dokument gespeichert werden soll
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="e">Das Eventargument</param>
        private void OnSaveDokument(object sender, EventArgs e)
        {
            OnSaveAsDokument(sender, e);

            
        }

        /// <summary>
        /// Wird aufgerufen, wenn gespeichert werden soll
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="e">Das Eventargument</param>
        private void OnSaveAsDokument(object sender, EventArgs e)
        {
            FileDialog dlg = new SaveFileDialog();
            dlg.Filter = "GameExpress-Dateien (*.gx)|*.gx|Alle Dateien (*.*)|*.*";
            if (!string.IsNullOrWhiteSpace(global::GameExpress.Properties.Settings.Default.LastFile1))
            {
                dlg.FileName = global::GameExpress.Properties.Settings.Default.LastFile1;
            }

            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                using (var statusText = new StatusChangeEventArgs("Zuletzt gespeichert um " + DateTime.Now.ToShortTimeString()))
                {
                    ExecuteSecure(() =>
                    {
                        OnStatusChange(this, statusText);
                    });

                    Controller.SaveProject(dlg.FileName);
                }
            }
        }

        /// <summary>
        /// Wird aufgerufen, wenn der Statustext geändert werden soll
        /// </summary>
        /// <param name="sender">Der Sender des Events</param>
        /// <param name="e">Das Eventargument</param>
        private void OnStatusChange(object sender, StatusChangeEventArgs e)
        {
            if (!e.IsClosed)
            {
                StatusText.Push(e);

                m_toolStripStatusLabel.Text = e.Text;
            }
        }

        /// <summary>
        /// Wird aufgerufen, wenn das aktive Item wechselt
        /// </summary>
        /// <param name="sender">Der Sender des Events</param>
        /// <param name="e">Das Eventargument</param>
        void OnChangeActiveItemEvent(object sender, ChangeActiveItemEventArgs e)
        {
            m_propertyGrid.SelectedObject = e.Item;
        }

        /// <summary>
        /// Der Benutzer hat auf die Schaltfläche "Aktualisieren" gedrückt
        /// </summary>
        /// <param name="sender">Der Sender des Events</param>
        /// <param name="e">Das Eventargument</param>
        private void OnRefreshPanel(object sender, EventArgs e)
        {
            var page = m_pageTreeFrame.CurrentPage;

            page.Refresh();
        }

        /// <summary>
        /// Wird aufgerufen, wenn der Nutzer drucken möchte
        /// </summary>
        /// <param name="sender">Der Sender des Events</param>
        /// <param name="e">Das Eventargument</param>
        private void OnPrint(object sender, EventArgs e)
        {
            PrintDialog dlg = new PrintDialog();
            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                PrintDocument doc = null;

                var currentPage = m_pageTreeFrame.CurrentPage;
                if (currentPage != null)
                {
                    doc = currentPage.Print();
                }

                if (doc != null)
                {
                    doc.PrinterSettings = dlg.PrinterSettings;
                    doc.Print();
                }
            }
        }

        /// <summary>
        /// Wird aufgerufen, um eine Druckvorschau anzuzeigen
        /// </summary>
        /// <param name="sender">Der Sender des Events</param>
        /// <param name="e">Das Eventargument</param>
        private void OnPreviewPrint(object sender, EventArgs e)
        {
            PrintPreviewDialog dlg = new PrintPreviewDialog();
            PrintDocument doc = null;

            Page currentPage = m_pageTreeFrame.CurrentPage;
            if (currentPage != null)
            {
                doc = currentPage.Print();
            }

            if (doc != null)
            {
                dlg.Document = doc;
            }

            dlg.ShowDialog();
        }

        /// <summary>
        /// Wird aufgerufen, wenn der Nutzer Einstellungen am Programm vornehmen möchte
        /// </summary>
        /// <param name="sender">Der Sender des Events</param>
        /// <param name="e">Die Event-Argumente</param>
        private void OnProperty(object sender, EventArgs e)
        {
            //DlgProperty dlg = new DlgProperty();

            //var ppGeneral = new PagePropertyGeneral();
           
            //var model = new ModelProperty();

            //new ControllerProperty(ppGeneral, model);
           

            //dlg.AddPropertyPage(ppGeneral);
            

            //Action ReadData = () =>
            //{
            //    // Allgemeine Daten
            //    ppGeneral.StartView = Controller.StartView;
            //};

            //dlg.ExtendedButton.Click += (s, e1) =>
            //{
            //    // Standardwerte übernehmen
            //    Controller.ResetConfig();

            //    ReadData();
            //};

            //// Daten lesen
            //ReadData();

            //if (dlg.ShowDialog() == DialogResult.OK)
            //{
            //    // Allgemeine Daten schreiben
            //    Controller.StartView = ppGeneral.StartView;

                
            //    Controller.SaveConfig();
            //    MessageBox.Show("Die Änderungen werden erst nach einem Neustart der Anwendung wirksam.", "Hinweis", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //}
        }

        /// <summary>
        /// Wird aufgerufen, wenn der Benutzer das ÜberFormular aufruft
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="e">Das Eventargument</param>
        private void OnAbout(object sender, EventArgs e)
        {
            var dlg = new DlgAboutBox();
            var m = new ModelAbout();
            var c = new ControllerAbout(dlg, m);

            dlg.ShowDialog();
        }

        /// <summary>
        /// Wird aufgerufen, wenn das Benutzerhandbuch angezeigt werden soll
        /// </summary>
        /// <param name="sender">Der Auslöser des Events</param>
        /// <param name="e">Das Eventargument</param>
        private void OnUserManual(object sender, EventArgs e)
        {
            try
            {
                //string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "UserPortal", "Benutzerhandbuch.pdf");

                //File.WriteAllBytes(path, global::UserPortal.Properties.Resources.Benutzerhandbuch);

                //System.Diagnostics.Process.Start(path);
            }
            catch (Exception /*ex*/)
            {

            }
        }

        /// <summary>
        /// Wird aufgerufen, wenn der Benutzer Daten Exportieren möchte
        /// </summary>
        /// <param name="sender">Der Sender des Events</param>
        /// <param name="e">Das Eventargument</param>
        private void OnExport(object sender, EventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "Alle Dateien (*.*)|*.*|Csv Dateien (*.csv)|*.csv";
            dlg.FilterIndex = 2;

            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                using (var statusText = new StatusChangeEventArgs("Exportiere Daten in " + dlg.FileName))
                {
                    ExecuteSecure(() =>
                    {
                        OnStatusChange(this, statusText);
                    });

                    Task taskV = Task.Factory.StartNew(() =>
                    {
                        List<List<string>> csv = new List<List<string>>();

                        try
                        {
                            File.WriteAllLines(dlg.FileName, from x in csv select string.Join(";", from i in x select i.Replace(Environment.NewLine, " ")), UTF8Encoding.UTF8);
                        }
                        catch (Exception /*ex*/)
                        {
                        }
                    });
                }
            }
        }

        /// <summary>
        /// Wird aufgerufen, wenn der Timer ausgelöst wird
        /// </summary>
        /// <param name="sender">Der Sender des Events</param>
        /// <param name="e">Das Eventargument</param>
        private void OnTimer(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Wird aufgerufen, wenn der Nutzer auf die Zurück-Schaltfläche klickt
        /// </summary>
        /// <param name="sender">Der Sender des Events</param>
        /// <param name="e">Das Eventargument</param>
        private void OnLastClick(object sender, EventArgs e)
        {
            m_pageTreeFrame.NavigateToLastPage();
        }

        /// <summary>
        /// Wird aufgerufen, wenn der Nutzer auf die Weiter-Schaltfläche klickt
        /// </summary>
        /// <param name="sender">Der Sender des Events</param>
        /// <param name="e">Das Eventargument</param>
        private void OnNextClick(object sender, EventArgs e)
        {
            m_pageTreeFrame.NavigateToNextPage();
        }

        /// <summary>
        /// Wird aufgerufen, wenn die Auswahl der aktuellen Ansicht sich ändert
        /// </summary>
        /// <param name="sender">Der Sender des Events</param>
        /// <param name="e">Das Eventargument</param>
        private void OnCurrentPageClick(object sender, EventArgs e)
        {
            if (sender is ToolStripMenuItem)
            {
                ChangePage((sender as ToolStripMenuItem).Tag as TreeViewPathCollection, true);
            }
        }

        /// <summary>
        /// Ändert die Ansicht
        /// </summary>
        /// <param name="sender">Der Sender des Events</param>
        /// <param name="e">Das Eventargument</param>
        private void OnChangedPage(object sender, EventArgsChangedPage e)
        {
            var page = e.NewPath.Last().Page;

            // Drucken und Exportieren an-/abstellen 
            m_printToolStripButton.Enabled = page != null ? page.IsPrintable : false;
            m_printToolStripMenuItem.Enabled = page != null ? page.IsPrintable : false;
            m_exportToolStripMenuItem.Enabled = page != null ? page.IsExportable : false;
            m_exportToolStripButton.Enabled = page != null ? page.IsExportable : false;

            // Zum Aktualisieren der Daten auffordern
            if (page != null)
            {
                page.Refresh();
            }

            UpdateNavButtons();
        }
    }
}
