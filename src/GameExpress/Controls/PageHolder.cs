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
    /// <summary>
    /// Platzhalter für Pages
    /// </summary>
    public partial class PageHolder : Page
    {
        private Stack<Page> LastStack { get; set; }
        private Stack<Page> NextStack { get; set; }

        /// <summary>
        /// Ermittelt die aktive Page oder null
        /// </summary>
        public Page CurrentPage { get; private set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        public PageHolder()
        {
            InitializeComponent();

            LastStack = new Stack<Page>();
            NextStack = new Stack<Page>();
        }

        /// <summary>
        /// Ändert die Ansicht
        /// </summary>
        /// <param name="page">Seite, zu der gewechselt werden soll</param>
        /// <param name="savePage">Seite in Historie aufnehmen</param>
        public void ChangePage(Page page, bool savePage = false)
        {
            if (CurrentPage == page) return;

            if (CurrentPage != null)
            {
                CurrentPage.Visible = false;
            }

            // Page aufnehmen
            SuspendLayout();
            Controls.Clear();

            // Alte Page retten
            if (CurrentPage != null && savePage)
            {
                LastStack.Push(CurrentPage);
                NextStack.Clear();
            }

            if (page != null)
            {
                page.Dock = DockStyle.Fill;
                Controls.Add(page);

                Image = page.Image;
                Title = page.Title;

                page.Visible = true;
            }

            CurrentPage = page;

            ResumeLayout();
        }

        /// <summary>
        /// Navigiert zur letzten Seite
        /// </summary>
        public void NavigateToLastPage()
        {

            // Aktuelle Seite retten
            if (CurrentPage != null)
            {
                NextStack.Push(CurrentPage);
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
            // Aktuelle Seite retten
            if (CurrentPage != null)
            {
                LastStack.Push(CurrentPage);
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
        /// löscht die Navigationshistorie
        /// </summary>
        public void ClearNavigationHitory()
        {
            NextStack.Clear();
            LastStack.Clear();
        }
    }
}
