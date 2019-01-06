using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using GameExpress.Core.Structs;
using GameExpress.Core.Items;
using System.Linq;

namespace GameExpress.Editor.Pages
{
    public partial class ItemMapPage : ItemPage
    {
        /// <summary>
        /// Liefert oder setzt das ausgewählete Item
        /// </summary>
        private ItemMapVertext SelectedItem { get; set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="item">Das zugehörige Item</param>
        public ItemMapPage(IItem item)
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

            var item = Item as ItemMap;
            if (item == null) return;

            var path = item.GetPath().Reverse().Where(x => x is ItemVisualScene).Select(x => x as ItemVisualScene).FirstOrDefault();
            if (path == null) return;

            var uc = new UpdateContext();

            Item.Update(uc);

            var pc = new PresentationContext(e.Graphics);
            pc.Matrix *= Matrix3D.Translation(e.ViewArea.Location);
            pc.Matrix *= Matrix3D.Scaling(e.Zoom, e.Zoom);
            pc.Designer = true;

            path.Presentation(pc);

            item.Presentation(pc);
        }

        /// <summary>
        /// Wird aufgerufen, wenn die Größe des Items ermittelt werden muss
        /// </summary>
        /// <param name="e">Das Eventargument</param>
        protected override void OnRetrieveItemSize(RetrieveItemSizeEventArgs e)
        {
            base.OnRetrieveItemSize(e);

            var item = Item as ItemMap;
            if (item == null) return;

            var path = item.GetPath().Reverse().Where(x => x is ItemVisualScene).Select(x => x as ItemVisualScene).FirstOrDefault();
            if (path == null) return;

            e.Size = path.Size;
        }

        /// <summary>
        /// Wird aufgerufen, wenn der Benutzer die Maus über die Ansicht bewegt
        /// </summary>
        /// <param name="e">Das Eventargument</param>
        protected override void OnMouseMoveItem(MouseEventArgs e)
        {
            base.OnMouseMoveItem(e);

            if (SelectedItem != null)
            {
                SelectedItem.Point = e.Location;

                RefreshItem();
            }
        }

        /// <summary>
        /// Wird beim drücken einer Maustaste aufgerufen
        /// </summary>
        /// <param name="e">Das Eventargument</param>
        protected override void OnMouseDownItem(MouseEventArgs e)
        {
            base.OnMouseDownItem(e);

            var item = Item as ItemMap;
            if (item == null) return;

            // Suche Vertices
            foreach (var v in item?.Vertices)
            {
                var rect = new Rectangle() { Location = v.Point };
                rect.Inflate(5, 5);

                if (rect.Contains(e.Location))
                {
                    SelectedItem = v;
                    
                    return;
                }
            }

            SelectedItem = null;
            
        }

        /// <summary>
        /// Wird beim loslassen der Maustaste gedrückt
        /// </summary>
        /// <param name="e">Das Eventargument</param>
        protected override void OnMouseUpItem(MouseEventArgs e)
        {
            base.OnMouseUpItem(e);

            SelectedItem = null;
        }

        /// <summary>
        /// Wird beim doppelklicken ausgelöst
        /// </summary>
        /// <param name="e">Das Eventargument</param>
        protected override void OnMouseClickItem(MouseEventArgs e)
        {
            base.OnMouseClickItem(e);

            var item = Item as ItemMap;
            if (item == null) return;

            // Suche Vertices
            foreach (var v in item?.Vertices)
            {
                var rect = new Rectangle() { Location = v.Point };
                rect.Inflate(5, 5);

                if (rect.Contains(e.Location))
                {
                    OnChangeActiveItem(new ChangeActiveItemEventArgs() { Item = v });

                    return;
                }
            }

            // Suche Dreiecke
            foreach (var v in item?.Mesh)
            {
                var rect = new Rectangle() { Location = v.Centroid };
                rect.Inflate(5, 5);

                if (rect.Contains(e.Location))
                {
                    OnChangeActiveItem(new ChangeActiveItemEventArgs() { Item = v });

                    return;
                }
            }

            OnChangeActiveItem(new ChangeActiveItemEventArgs() { Item = item });
        }

        /// <summary>
        /// Wird beim doppelklicken ausgelöst
        /// </summary>
        /// <param name="e">Das Eventargument</param>
        protected override void OnMouseDoubleClickItem(MouseEventArgs e)
        {
            base.OnMouseDoubleClickItem(e);

            
        }
    }
}
