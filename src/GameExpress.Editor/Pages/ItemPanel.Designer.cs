namespace GameExpress.Editor.Pages
{
    partial class ItemPanel
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.m_verticalScrollBar = new System.Windows.Forms.VScrollBar();
            this.m_horizontalScrollBar = new System.Windows.Forms.HScrollBar();
            this.m_panel = new GameExpress.Editor.Pages.Panel();
            this.m_horizontalRuler = new GameExpress.Editor.Pages.Panel();
            this.m_verticalRuler = new GameExpress.Editor.Pages.Panel();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.m_verticalScrollBar, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.m_horizontalScrollBar, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.m_panel, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.m_horizontalRuler, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.m_verticalRuler, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(725, 395);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // m_verticalScrollBar
            // 
            this.m_verticalScrollBar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_verticalScrollBar.Location = new System.Drawing.Point(708, 20);
            this.m_verticalScrollBar.Name = "m_verticalScrollBar";
            this.m_verticalScrollBar.Size = new System.Drawing.Size(17, 358);
            this.m_verticalScrollBar.TabIndex = 1;
            this.m_verticalScrollBar.Scroll += new System.Windows.Forms.ScrollEventHandler(this.OnVerticalScroll);
            // 
            // m_horizontalScrollBar
            // 
            this.m_horizontalScrollBar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_horizontalScrollBar.Location = new System.Drawing.Point(20, 378);
            this.m_horizontalScrollBar.Name = "m_horizontalScrollBar";
            this.m_horizontalScrollBar.Size = new System.Drawing.Size(688, 17);
            this.m_horizontalScrollBar.TabIndex = 2;
            this.m_horizontalScrollBar.Scroll += new System.Windows.Forms.ScrollEventHandler(this.OnHorizontalScroll);
            // 
            // m_panel
            // 
            this.m_panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_panel.Location = new System.Drawing.Point(20, 20);
            this.m_panel.Margin = new System.Windows.Forms.Padding(0);
            this.m_panel.Name = "m_panel";
            this.m_panel.Size = new System.Drawing.Size(688, 358);
            this.m_panel.TabIndex = 5;
            this.m_panel.Paint += new System.Windows.Forms.PaintEventHandler(this.OnPaint);
            this.m_panel.MouseClick += new System.Windows.Forms.MouseEventHandler(this.OnMouseClick);
            this.m_panel.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.OnMouseDoubleClick);
            this.m_panel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OnMouseDown);
            this.m_panel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.OnMouseMove);
            this.m_panel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.OnMouseUp);
            // 
            // m_horizontalRuler
            // 
            this.m_horizontalRuler.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_horizontalRuler.Location = new System.Drawing.Point(20, 0);
            this.m_horizontalRuler.Margin = new System.Windows.Forms.Padding(0);
            this.m_horizontalRuler.Name = "m_horizontalRuler";
            this.m_horizontalRuler.Size = new System.Drawing.Size(688, 20);
            this.m_horizontalRuler.TabIndex = 6;
            this.m_horizontalRuler.Paint += new System.Windows.Forms.PaintEventHandler(this.OnPaintHorizontalRuler);
            // 
            // m_verticalRuler
            // 
            this.m_verticalRuler.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_verticalRuler.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.m_verticalRuler.Location = new System.Drawing.Point(0, 20);
            this.m_verticalRuler.Margin = new System.Windows.Forms.Padding(0);
            this.m_verticalRuler.Name = "m_verticalRuler";
            this.m_verticalRuler.Size = new System.Drawing.Size(20, 358);
            this.m_verticalRuler.TabIndex = 7;
            this.m_verticalRuler.Paint += new System.Windows.Forms.PaintEventHandler(this.OnPaintVerticalRuler);
            // 
            // ItemPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "ItemPanel";
            this.Size = new System.Drawing.Size(725, 395);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.VScrollBar m_verticalScrollBar;
        private System.Windows.Forms.HScrollBar m_horizontalScrollBar;
        private Panel m_panel;
        private Panel m_horizontalRuler;
        private Panel m_verticalRuler;
    }
}
