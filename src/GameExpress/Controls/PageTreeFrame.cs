using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GameExpress.Editor.Pages;

namespace GameExpress.Controls
{
    public partial class PageTreeFrame : UserControl
    {
        /// <summary>
        /// Wird aufgerufen, wenn sich die aktuelle Seiite ändert
        /// </summary>
        public event EventHandler<EventArgsChangedPage> ChangedPage;

        /// <summary>
        /// Liefert oder setzt den Stapel der letzten Seiten
        /// </summary>
        private Stack<TreeViewPathCollection> LastStack { get; set; }
        private Stack<TreeViewPathCollection> NextStack { get; set; }

        /// <summary>
        /// Liefert den aktuell ausgewählten Pfad
        /// </summary>
        public TreeViewPathCollection CurrentPath
        {
            get;
            protected set;
        }

        /// <summary>
        /// Ermittelt die aktive Page oder null
        /// </summary>
        public Page CurrentPage
        {
            get
            {
                return CurrentPath != null ? CurrentPath.Last().Page : null;
            }
        }

        /// <summary>
        /// Liefert oder setzt das Kontextmenü für die Baumansicht
        /// </summary>
        public ContextMenuStrip TreeContextMenuStrip
        {
            get { return m_treeView.ContextMenuStrip; }
            set { m_treeView.ContextMenuStrip = value; }
        }

        /// <summary>
        /// Liefert das aktuell ausgewählte Pfadelement
        /// </summary>
        public TreeViewPathCollection SelectedPath
        {
            get { return m_treeView.SelectedPath; }
            set { m_treeView.SelectedPath = value; }
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        public PageTreeFrame()
        {
            InitializeComponent();

            LastStack = new Stack<TreeViewPathCollection>();
            NextStack = new Stack<TreeViewPathCollection>();

            m_pageTitle.Dock = DockStyle.Top;
            m_pageTitle.Padding = new System.Windows.Forms.Padding(0);
            m_pageTitle.Margin = new System.Windows.Forms.Padding(0);
            m_pageTitle.Height = 24;

            m_pageHolder.Padding = new System.Windows.Forms.Padding(0);
            m_pageHolder.Margin = new System.Windows.Forms.Padding(0);
        }

        /// <summary>
        /// Tritt ein, wenn der Benutzer das Formular lädt
        /// </summary>
        /// <param name="e">Eventargumente</param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (m_treeView.Nodes.Count > 0)
            {
                m_treeView.ExpandAll();
                m_treeView.SelectedNode = m_treeView.Nodes[0];
            }
        }

        /// <summary>
        /// Löst das ChangedPage-Event aus
        /// </summary>
        /// <param name="path">der Pfad, zu dem gewechselt werden soll</param>
        protected virtual void OnChangedPage(EventArgsChangedPage path)
        {
            if (ChangedPage != null)
            {
                ChangedPage(this, path);
            }
        }

        /// <summary>
        /// Fügt eine Funktion dem Baum hinzu
        /// </summary>
        /// <param name="path">TreeViewPath</param>
        public void AddPage(TreeViewPathCollection path)
        {
            m_treeView.InsertNode(path);
        }

        /// <summary>
        /// Fügt eine Funktion dem Baum hinzu
        /// </summary>
        /// <param name="path">TreeViewPath</param>
        public void AddOrRefreshPage(TreeViewPathCollection path)
        {
            var n = m_treeView.FindNode(path);

            if (n == null)
            {
                AddPage(path);

                return;
            }

            // Update
            m_treeView.UpdateNode(path);
            var page = path.Page;
            if (page != null)
            {
                m_pageTitle.Image = page.Image;
                m_pageTitle.Title = page.Title;
            }
        }

        /// <summary>
        /// Fügt eine Funktion dem Baum hinzu
        /// </summary>
        /// <param name="path">TreeViewPath</param>
        public void RemovePage(TreeViewPathCollection path)
        {
            //m_treeView.RemoveAllNode();
        }

        /// <summary>
        /// Zeigt ein Pfadelement an
        /// </summary>
        /// <param name="path">TreeViewPath</param>
        public void ShowPage(TreeViewPathCollection path)
        {
            if (path != null)
            {
                ChangePage(path, true);
            }
        }

        /// <summary>
        /// Tritt ein, wenn die Auswahl des TreeViews geändert wurde
        /// </summary>
        /// <param name="sender">Der Auslöser der Nachricht</param>
        /// <param name="e">Eventargumente</param>
        private void OnAfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node != null)
            {
                var item = m_treeView.GetItem(e.Node);
                ChangePage(item, true);
            }
        }

        /// <summary>
        /// Ändert die Ansicht
        /// </summary>
        /// <param name="path"></param>
        /// <param name="savePage"></param>
        private void ChangePage(TreeViewPathCollection path, bool savePage)
        {
            var currentPath = CurrentPath;
            var currentPage = CurrentPage;
            var newPage = path.Page;

            if (currentPage != null && currentPage == newPage) return;

            // Alte Page retten
            if (currentPage != null && savePage)
            {
                LastStack.Push(CurrentPath);
                NextStack.Clear();
            }

            // Page aufnehmen
            m_pageHolder.ChangePage(newPage);

            m_pageTitle.Image = newPage != null ? newPage.Image : null;
            m_pageTitle.Title = newPage != null ? newPage.Title : string.Empty;

            CurrentPath = path;
            OnChangedPage(new EventArgsChangedPage() { OldPath = currentPath, NewPath = path });

            m_treeView.SelectedPath = path;
        }

        /// <summary>
        /// Entfernt alle Seiten aus der Ansicht
        /// </summary>
        public void ClearPages()
        {
            m_pageHolder.ChangePage(null);

            m_treeView.RemoveAllNode();
        }

        /// <summary>
        /// Entfernt alle Seiten aus der Navigationsliste
        /// </summary>
        public void ClearNavigatePages()
        {
            NextStack.Clear();
            LastStack.Clear();
        }

        /// <summary>
        /// Navigiert zur letzten Seite
        /// </summary>
        public void NavigateToLastPage()
        {
            var currentPath = CurrentPath;

            // Aktuelle Seite retten
            if (currentPath != null)
            {
                NextStack.Push(currentPath);
            }

            if (LastStack.Count == 0) return;

            var item = LastStack.Pop();

            ChangePage(item, false);
        }

        /// <summary>
        /// Ermittelt ob zur vorhergehenden Seite gewechselt werden kann
        /// </summary>
        /// <returns></returns>
        public bool CanNavigateToLastPage()
        {
            return LastStack.Count > 0;
        }

        /// <summary>
        /// Navigiert zur nächsten Seite
        /// </summary>
        public void NavigateToNextPage()
        {
            var currentPath = CurrentPath;

            // Aktuelle Seite retten
            if (currentPath != null)
            {
                LastStack.Push(currentPath);
            }

            if (NextStack.Count > 0)
            {
                var item = NextStack.Pop();

                ChangePage(item, false);
            }
        }

        /// <summary>
        /// Ermittelt ob zur nächsten Seite gewechselt werden kann
        /// </summary>
        /// <returns></returns>
        public bool CanNavigateToNextPage()
        {
            return NextStack.Count > 0;
        }

        /// <summary>
        /// Erweitert den Baum
        /// </summary>
        public void ExpandAll()
        {
            m_treeView.ExpandAll();
        }

        /// <summary>
        /// Wandelt den Baum als Liste um 
        /// </summary>
        /// <returns>Der Baum als Liste</returns>
        public List<TreeViewPathCollection> PreOrder()
        {
            return m_treeView.PreOrder();
        }
    }
}
