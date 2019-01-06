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
    public partial class ItemContainerPage : ItemPage
    {
        private PullFrame m_pullFrame = new PullFrame();

        /// <summary>
        /// Liefert das PullFrame
        /// </summary>
        public PullFrame PullFrame { get { return m_pullFrame; } }

        /// <summary>
        /// Konstruktor
        /// </summary>
        public ItemContainerPage()
            : this(null)
        {
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="item"></param>
        public ItemContainerPage(IItem item)
            :base(item)
        {
            InitializeComponent();

            PullFrame.Enable = false;
        }

        /// <summary>
        /// Wird bei ersten Mal anzeigen aufgerufen
        /// </summary>
        /// <param name="e">Das Eventargument</param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            PullFrame.State = PullFrame.PullFrameState.None;
        }

        /// <summary>
        /// Wird aufgerufen, wenn das Item visuell dargestellt werden soll
        /// </summary>
        /// <param name="e">Das Eventargument</param>
        protected override void OnPaintItem(PaintViewEventArgs e)
        {
            base.OnPaintItem(e);

            if (PullFrame.Enable)
            {
                PullFrame.Draw(e.Graphics);
            }
        }

        /// <summary>
        /// Wird aufgerufen, wenn die Größe des Items ermittelt werden muss
        /// </summary>
        /// <param name="e">Das Eventargument</param>
        protected override void OnRetrieveItemSize(RetrieveItemSizeEventArgs e)
        {
            base.OnRetrieveItemSize(e);
        }

        /// <summary>
        /// Wird aufgerufen, wenn der Benutzer die Maus über die Ansicht bewegt
        /// </summary>
        /// <param name="e">Das Eventargument</param>
        protected override void OnMouseMoveItem(MouseEventArgs e)
        {
            base.OnMouseMoveItem(e);
        }

        /// <summary>
        /// Wird beim drücken einer Maustaste aufgerufen
        /// </summary>
        /// <param name="e">Das Eventargument</param>
        protected override void OnMouseDownItem(MouseEventArgs e)
        {
            base.OnMouseDownItem(e);
        }

        /// <summary>
        /// Wird beim loslassen der Maustaste gedrückt
        /// </summary>
        /// <param name="e">Das Eventargument</param>
        protected override void OnMouseUpItem(MouseEventArgs e)
        {
            base.OnMouseUpItem(e);
        }

        /// <summary>
        /// Wird beim doppelklicken ausgelöst
        /// </summary>
        /// <param name="e">Das Eventargument</param>
        protected override void OnMouseClickItem(MouseEventArgs e)
        {
            base.OnMouseClickItem(e);
        }

        /// <summary>
        /// Wird beim doppelklicken ausgelöst
        /// </summary>
        /// <param name="e">Das Eventargument</param>
        protected override void OnMouseDoubleClickItem(MouseEventArgs e)
        {
            base.OnMouseDoubleClickItem(e);

            // ToDo: Prüfe ob innerhalb des PullFrames
            PullFrame.NextHandleState();
        }
    }
}
