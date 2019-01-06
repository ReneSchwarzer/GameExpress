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
    public partial class ItemObjectPage : ItemPage
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="item">Das zugehörige Item</param>
        public ItemObjectPage(IItem item)
            :base(item)
        {
            InitializeComponent();

            //m_panel.Item = item;
            m_panel.PaintView += OnPaintView;
            m_panel.RetrieveItemSize += OnRetrieveItemSize;

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
        /// <param name="sender">Sender der Nachricht</param>
        /// <param name="e">Das Eventargument</param>
        private void OnPaintView(object sender, PaintViewEventArgs e)
        {
            var item = Item as ItemVisualObject;
            if (item == null || item.Image == null) return;


            //Rectangle rect = m_panel.ViewAreaRect;

            var pc = new PresentationContext(e.Graphics);
            //pc.Matrix *= Matrix3D.Translation(rect.Location);
            pc.Matrix *= Matrix3D.Scaling(m_panel.Zoom, m_panel.Zoom);

            //Point s = pc.Matrix.Transform(new Point(item.Image.Width, item.Image.Height));

            //pc.Matrix *= Matrix3D.Translation(new PointF((Width / 2) - (s.X / 2), (Height / 2) - (s.Y / 2)));


            item.Presentation(pc);
        }

        /// <summary>
        /// Wird aufgerufen, wenn die Größe des Items ermittelt werden muss
        /// </summary>
        /// <param name="sender">Sender der Nachricht</param>
        /// <param name="e">Das Eventargument</param>
        private void OnRetrieveItemSize(object sender, RetrieveItemSizeEventArgs e)
        {
            ItemVisualObject item = Item as ItemVisualObject;
            if (item == null) return;

            //e.Size = item.Image.Size;
        }
    }
}
