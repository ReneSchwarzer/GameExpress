namespace GameExpress.Editor.Pages
{
    partial class ItemAnimatedPage
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ItemAnimatedPage));
            this.m_timeLinePanel = new GameExpress.Editor.Pages.TimeLinePanel();
            this.m_splitter = new System.Windows.Forms.Splitter();
            this.m_playToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.m_toolStrip = new System.Windows.Forms.ToolStrip();
            this.m_toolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_timeLinePanel
            // 
            this.m_timeLinePanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_timeLinePanel.Location = new System.Drawing.Point(0, 31);
            this.m_timeLinePanel.Margin = new System.Windows.Forms.Padding(9, 12, 9, 12);
            this.m_timeLinePanel.Name = "m_timeLinePanel";
            this.m_timeLinePanel.SelectedItem = null;
            this.m_timeLinePanel.Size = new System.Drawing.Size(1795, 250);
            this.m_timeLinePanel.TabIndex = 3;
            this.m_timeLinePanel.Time = ((ulong)(0ul));
            // 
            // m_splitter
            // 
            this.m_splitter.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_splitter.Location = new System.Drawing.Point(0, 281);
            this.m_splitter.Name = "m_splitter";
            this.m_splitter.Size = new System.Drawing.Size(1795, 3);
            this.m_splitter.TabIndex = 4;
            this.m_splitter.TabStop = false;
            // 
            // m_playToolStripButton
            // 
            this.m_playToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.m_playToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("m_playToolStripButton.Image")));
            this.m_playToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.m_playToolStripButton.Name = "m_playToolStripButton";
            this.m_playToolStripButton.Size = new System.Drawing.Size(28, 28);
            this.m_playToolStripButton.Text = "Start";
            this.m_playToolStripButton.Click += new System.EventHandler(this.OnPlay);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 31);
            // 
            // m_toolStrip
            // 
            this.m_toolStrip.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.m_toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_playToolStripButton,
            this.toolStripSeparator1});
            this.m_toolStrip.Location = new System.Drawing.Point(0, 0);
            this.m_toolStrip.Name = "m_toolStrip";
            this.m_toolStrip.Padding = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.m_toolStrip.Size = new System.Drawing.Size(1795, 31);
            this.m_toolStrip.TabIndex = 1;
            this.m_toolStrip.Text = "toolStrip1";
            // 
            // ItemAnimatedPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(14F, 29F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.Controls.Add(this.m_splitter);
            this.Controls.Add(this.m_timeLinePanel);
            this.Controls.Add(this.m_toolStrip);
            this.Margin = new System.Windows.Forms.Padding(9, 12, 9, 12);
            this.Name = "ItemAnimatedPage";
            this.Size = new System.Drawing.Size(1795, 1544);
            this.Controls.SetChildIndex(this.m_toolStrip, 0);
            this.Controls.SetChildIndex(this.m_timeLinePanel, 0);
            this.Controls.SetChildIndex(this.m_splitter, 0);
            this.m_toolStrip.ResumeLayout(false);
            this.m_toolStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private TimeLinePanel m_timeLinePanel;
        private System.Windows.Forms.Splitter m_splitter;
        private System.Windows.Forms.ToolStripButton m_playToolStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStrip m_toolStrip;
    }
}
