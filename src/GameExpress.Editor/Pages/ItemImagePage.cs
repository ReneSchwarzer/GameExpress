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
    public partial class ItemImagePage : ItemPage
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="item">Das zugehörige Item</param>
        public ItemImagePage(IItem item)
            :base(item)
        {
            InitializeComponent();
        }

        /// <summary>
        /// Wird bei ersten Mal anzeigen aufgerufen
        /// </summary>
        /// <param name="e">Das Eventargument</param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            m_toolStrip.ImageScalingSize = new Size(16, 16);
        }

        /// <summary>
        /// Wird aufgerufen, wenn das Item visuell dargestellt werden soll
        /// </summary>
        /// <param name="e">Das Eventargument</param>
        protected override void OnPaintItem(PaintViewEventArgs e)
        {
            base.OnPaintItem(e);

            var item = Item as ItemVisualImage;
            if (item == null || item.Image == null) return;

            var pc = new PresentationContext(e.Graphics);
            pc.Matrix *= Matrix3D.Translation(e.ViewArea.Location);
            pc.Matrix *= Matrix3D.Scaling(e.Zoom, e.Zoom);
            pc.Designer = true;

            item.Presentation(pc);
        }

        /// <summary>
        /// Wird aufgerufen, wenn die Größe des Items ermittelt werden muss
        /// </summary>
        /// <param name="e">Das Eventargument</param>
        protected override void OnRetrieveItemSize(RetrieveItemSizeEventArgs e)
        {
            base.OnRetrieveItemSize(e);

            var item = Item as ItemVisualImage;
            if (item == null || item.Image == null) return;

            e.Size = item.Image.Size;
        }

        /// <summary>
        /// Wird aufgerufen, wenn der Benutzer das Bild vergrößern möchte
        /// </summary>
        /// <param name="sender">Sender der Nachricht</param>
        /// <param name="e">Das Eventargument</param>
        private void OnZoomIn(object sender, EventArgs e)
        {
            //m_panel.ZoomIn();
        }

        /// <summary>
        /// Wird aufgerufen, wenn der Benutzer das Bild verkleinern möchte
        /// </summary>
        /// <param name="sender">Sender der Nachricht</param>
        /// <param name="e">Das Eventargument</param>
        private void OnZoomOut(object sender, EventArgs e)
        {
            //m_panel.ZoomOut();
        }
    }
}
