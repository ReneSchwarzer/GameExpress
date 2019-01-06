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
    public partial class ItemGeometryPage : ItemPage
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="item">Das zugehörige Item</param>
        public ItemGeometryPage(IItem item)
            :base(item)
        {
            InitializeComponent();
        }

        /// <summary>
        /// Wird aufgerufen, wenn das Item visuell dargestellt werden soll
        /// </summary>
        /// <param name="e">Das Eventargument</param>
        protected override void OnPaintItem(PaintViewEventArgs e)
        {
            base.OnPaintItem(e);

            var item = Item as ItemVisualGeometry;
            if (item == null) return;

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

            var item = Item as ItemVisualGeometry;
            if (item == null) return;

            e.Size = item.Size;
        }
    }
}
