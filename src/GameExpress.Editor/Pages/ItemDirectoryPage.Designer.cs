namespace GameExpress.Editor.Pages
{
    partial class ItemDirectoryPage
    {
        /// <summary> 
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Komponenten-Designer generierter Code

        /// <summary> 
        /// Erforderliche Methode für die Designerunterstützung. 
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.m_toolStrip = new System.Windows.Forms.ToolStrip();
            this.m_detailsToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.m_listToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.m_listView = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.m_toolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_toolStrip
            // 
            this.m_toolStrip.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.m_toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_detailsToolStripButton,
            this.m_listToolStripButton});
            this.m_toolStrip.Location = new System.Drawing.Point(0, 0);
            this.m_toolStrip.Name = "m_toolStrip";
            this.m_toolStrip.Padding = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this.m_toolStrip.Size = new System.Drawing.Size(1154, 31);
            this.m_toolStrip.TabIndex = 1;
            this.m_toolStrip.Text = "toolStrip1";
            // 
            // m_detailsToolStripButton
            // 
            this.m_detailsToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.m_detailsToolStripButton.Image = global::GameExpress.Editor.Properties.Resources.application_view_detail;
            this.m_detailsToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.m_detailsToolStripButton.Name = "m_detailsToolStripButton";
            this.m_detailsToolStripButton.Size = new System.Drawing.Size(28, 28);
            this.m_detailsToolStripButton.Text = "Details";
            this.m_detailsToolStripButton.Click += new System.EventHandler(this.OnDetails);
            // 
            // m_listToolStripButton
            // 
            this.m_listToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.m_listToolStripButton.Image = global::GameExpress.Editor.Properties.Resources.application_view_tile;
            this.m_listToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.m_listToolStripButton.Name = "m_listToolStripButton";
            this.m_listToolStripButton.Size = new System.Drawing.Size(28, 28);
            this.m_listToolStripButton.Text = "Liste";
            this.m_listToolStripButton.Click += new System.EventHandler(this.OnList);
            // 
            // m_listView
            // 
            this.m_listView.BackgroundImage = global::GameExpress.Editor.Properties.Resources.hintergrund;
            this.m_listView.BackgroundImageTiled = true;
            this.m_listView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.m_listView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.m_listView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_listView.Location = new System.Drawing.Point(0, 31);
            this.m_listView.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.m_listView.Name = "m_listView";
            this.m_listView.Size = new System.Drawing.Size(1154, 1034);
            this.m_listView.TabIndex = 2;
            this.m_listView.UseCompatibleStateImageBehavior = false;
            this.m_listView.View = System.Windows.Forms.View.SmallIcon;
            this.m_listView.DoubleClick += new System.EventHandler(this.OnDoubleClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Name";
            this.columnHeader1.Width = 120;
            // 
            // ItemDirectoryPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.m_listView);
            this.Controls.Add(this.m_toolStrip);
            this.Margin = new System.Windows.Forms.Padding(6, 8, 6, 8);
            this.Name = "ItemDirectoryPage";
            this.Size = new System.Drawing.Size(1154, 1065);
            this.m_toolStrip.ResumeLayout(false);
            this.m_toolStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip m_toolStrip;
        private System.Windows.Forms.ListView m_listView;
        private System.Windows.Forms.ToolStripButton m_detailsToolStripButton;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ToolStripButton m_listToolStripButton;
    }
}
