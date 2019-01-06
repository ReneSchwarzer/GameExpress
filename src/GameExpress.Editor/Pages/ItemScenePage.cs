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
    public partial class ItemScenePage : ItemAnimatedPage
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="item">Das zugehörige Item</param>
        public ItemScenePage(IItem item)
            :base(item)
        {
            InitializeComponent();
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

            var objectState = Item as ItemVisualScene;
            if (objectState == null) return;
        }

        /// <summary>
        /// Wird aufgerufen, wenn die Größe des Items ermittelt werden muss
        /// </summary>
        /// <param name="e">Das Eventargument</param>
        protected override void OnRetrieveItemSize(RetrieveItemSizeEventArgs e)
        {
            // base.OnRetrieveItemSize(e);

            var item = Item as ItemVisualScene;
            if (item == null) return;

            e.Size = item.Size;
        }

    }
}
