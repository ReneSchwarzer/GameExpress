using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using GameExpress.Core.Structs;
using GameExpress.Core.Items;

namespace GameExpress.Editor.Pages
{
    public partial class ItemObjectStatePage : ItemAnimatedPage
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="item">Das mit der Ansicht verbundene Item</param>
        public ItemObjectStatePage(IItem item)
            :base(item)
        {
            InitializeComponent();

            var i = Item as ItemVisualAnimatedObjectState;
            if (i == null) return;
        }

        /// <summary>
        /// Wird bei ersten mal anzeigen aufgerufen
        /// </summary>
        /// <param name="e">Das Eventargument</param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        }

        /// <summary>
        /// Wird aufgerufen, wenn das Item visuell dargestellt werden soll
        /// </summary>
        /// <param name="e">Das Eventargument</param>
        protected override void OnPaintItem(PaintViewEventArgs e)
        {
            base.OnPaintItem(e);

            var objectState = Item as ItemVisualAnimatedObjectState;
            if (objectState == null) return;
        }

        /// <summary>
        /// Diese Methode wird aufgerufen, wenn der Benutzer die Maustaste drückt.
        /// </summary>
        /// <param name="sender">Sender der Nachricht</param>
        /// <param name="e">Das Eventargument</param>
        private void OnMouseClick(object sender, MouseEventArgs e)
        {
        }

        /// <summary>
        /// Wird aufgerufen, wenn sich die Objektauswahl ändert
        /// </summary>
        /// <param name="sender">Sender der Nachricht</param>
        /// <param name="e">Das Eventargument</param>
        private void OnChangedSelectedItem(object sender, ChanegedSelectetItemArgs e)
        {
            m_panel.Refresh();
        }

        /// <summary>
        /// Wird aufgerufen, wenn der Benutzer die Maus über die Ansicht bewegt
        /// </summary>
        /// <param name="sender">Sender der Nachricht</param>
        /// <param name="e">Das Eventargument</param>
        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            
        }

        /// <summary>
        /// Wird beim drücken einer Maustaste aufgerufen
        /// </summary>
        /// <param name="sender">Sender der Nachricht</param>
        /// <param name="e">Das Eventargument</param>
        private void OnMouseDown(object sender, MouseEventArgs e)
        {
            
        }

        /// <summary>
        /// Wird beim loslassen der Maustaste gedrückt
        /// </summary>
        /// <param name="sender">Sender der Nachricht</param>
        /// <param name="e">Das Eventargument</param>
        private void OnMouseUp(object sender, MouseEventArgs e)
        {
            
        }

        /// <summary>
        /// Wird beim doppelklicken ausgelöst
        /// </summary>
        /// <param name="sender">Sender der Nachricht</param>
        /// <param name="e">Das Eventargument</param>
        private void OnMouseDoubleClick(object sender, MouseEventArgs e)
        {
        }

        /// <summary>
        /// Wird zum starten des Timers aufgerufen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">Das Eventargument</param>
        private void OnPlay(object sender, EventArgs e)
        {
            m_timer.Enabled = !m_timer.Enabled;

            m_playToolStripButton.Checked = m_timer.Enabled;
        }

        /// <summary>
        /// Wird aufgerufen, wenn der Timer auslöst
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">Das Eventargument</param>
        private void OnTick(object sender, EventArgs e)
        {
            timeLinePanel1.Time += 1;

            Refresh();
        }
    }
}
