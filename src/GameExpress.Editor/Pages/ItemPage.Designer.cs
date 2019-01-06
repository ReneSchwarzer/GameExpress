namespace GameExpress.Editor.Pages
{
    partial class ItemPage
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
            this.m_panel = new GameExpress.Editor.Pages.ItemPanel();
            this.m_tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.m_zoomComboBox = new System.Windows.Forms.ComboBox();
            this.m_tableLayoutPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_panel
            // 
            this.m_tableLayoutPanel.SetColumnSpan(this.m_panel, 2);
            this.m_panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_panel.Location = new System.Drawing.Point(9, 12);
            this.m_panel.Margin = new System.Windows.Forms.Padding(9, 12, 9, 12);
            this.m_panel.Name = "m_panel";
            this.m_panel.Size = new System.Drawing.Size(1674, 814);
            this.m_panel.TabIndex = 3;
            this.m_panel.Zoom = 1F;
            this.m_panel.MouseClick += new System.Windows.Forms.MouseEventHandler(this.OnMouseClickItem);
            this.m_panel.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.OnMouseDoubleClickItem);
            this.m_panel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OnMouseDownItem);
            this.m_panel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.OnMouseMoveItem);
            this.m_panel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.OnMouseUpItem);
            // 
            // m_tableLayoutPanel
            // 
            this.m_tableLayoutPanel.ColumnCount = 2;
            this.m_tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.m_tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.m_tableLayoutPanel.Controls.Add(this.m_panel, 0, 0);
            this.m_tableLayoutPanel.Controls.Add(this.m_zoomComboBox, 0, 1);
            this.m_tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_tableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.m_tableLayoutPanel.Name = "m_tableLayoutPanel";
            this.m_tableLayoutPanel.RowCount = 2;
            this.m_tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.m_tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.m_tableLayoutPanel.Size = new System.Drawing.Size(1692, 881);
            this.m_tableLayoutPanel.TabIndex = 4;
            // 
            // m_zoomComboBox
            // 
            this.m_zoomComboBox.FormatString = "{0} %";
            this.m_zoomComboBox.FormattingEnabled = true;
            this.m_zoomComboBox.Location = new System.Drawing.Point(3, 841);
            this.m_zoomComboBox.MinimumSize = new System.Drawing.Size(100, 0);
            this.m_zoomComboBox.Name = "m_zoomComboBox";
            this.m_zoomComboBox.Size = new System.Drawing.Size(121, 37);
            this.m_zoomComboBox.TabIndex = 5;
            this.m_zoomComboBox.TextChanged += new System.EventHandler(this.OnZoomValueChanged);
            // 
            // ItemPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(14F, 29F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.m_tableLayoutPanel);
            this.DoubleBuffered = true;
            this.Margin = new System.Windows.Forms.Padding(7);
            this.Name = "ItemPage";
            this.Size = new System.Drawing.Size(1692, 881);
            this.m_tableLayoutPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private ItemPanel m_panel;
        private System.Windows.Forms.TableLayoutPanel m_tableLayoutPanel;
        private System.Windows.Forms.ComboBox m_zoomComboBox;
    }
}
