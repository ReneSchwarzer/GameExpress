using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using GameExpress.Core.Items;

namespace GameExpress.Editor.Pages
{
    public partial class ItemDirectoryPage : ItemPage
    {
        protected ImageList ImageList { get; set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="item">Das Item</param>
        public ItemDirectoryPage(IItem item)
            : base(item)
        {
            InitializeComponent();
            
            ImageList = new System.Windows.Forms.ImageList();
            ImageList.ImageSize = new System.Drawing.Size(16, 16);

            m_toolStrip.ImageScalingSize = new Size(16, 16);

            m_listView.SmallImageList = ImageList;
            m_listView.LargeImageList = ImageList;

            OnList(this, new EventArgs());

            Refresh();
        }

        /// <summary>
        /// Aktusiaiert die Daten der Page
        /// </summary>
        public override void Refresh()
        {
            base.Refresh();

            //m_listView.Items.Clear();

            //ImageList = new ImageList();

            //if (Item == null) return;

            //var n = (Item as ITree).LeftChild;

            //// Liste alle Kinder auf
            //while (n != null)
            //{
            //    var item = n as IItem;
            //    if (item != null)
            //    {
            //        if (!ImageList.Images.ContainsKey(item.Context.ImageID.ToString()))
            //        {
            //            ImageList.Images.Add(item.Context.ImageID.ToString(), item.Context.Image);
            //        }

            //        var i = new ListViewItem(item.Name);
            //        i.ImageIndex = ImageList.Images.IndexOfKey(item.Context.ImageID.ToString());
            //        i.Tag = item;

            //        if (m_listView != null) m_listView.Items.Add(i);
            //        n = n.RightSibling;
            //    }
            //}

            //m_listView.SmallImageList = ImageList;
            //m_listView.LargeImageList = ImageList;
        }

        /// <summary>
        /// Wird aufgerufen, wenn die Detailansicht erfolgen soll
        /// </summary>
        /// <param name="sender">Der Auslöser</param>
        /// <param name="e">Das Eventargument</param>
        private void OnDetails(object sender, EventArgs e)
        {
            m_listView.View = View.Details;

            m_detailsToolStripButton.Checked = true;
            m_listToolStripButton.Checked = false;
        }

        /// <summary>
        /// Wird aufgerufen, wenn die Ansicht tabellarisch erfolgen soll
        /// </summary>
        /// <param name="sender">Der Auslöser</param>
        /// <param name="e">Das Eventargument</param>
        private void OnList(object sender, EventArgs e)
        {
            m_listView.View = View.List;

            m_detailsToolStripButton.Checked = false;
            m_listToolStripButton.Checked = true;
        }

        /// <summary>
        /// Wird aufgerufen, wenn der Nutzer doppelt auf ein Item klickt 
        /// </summary>
        /// <param name="sender">Der Auslöser</param>
        /// <param name="e">Das Eventargument</param>
        private void OnDoubleClick(object sender, EventArgs e)
        {
            if (m_listView.SelectedItems == null) return;

            var item = m_listView.SelectedItems[0].Tag as IItem;

            if (item != null)
            {
                //item.RaiseOpenItemEvent();
                //Project.Current.ExplorerCtrl.ItemOpen(item);
            }
        }
    }
}
