namespace GameExpress.Controls
{
    partial class PageTreeFrame
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
            this.components = new System.ComponentModel.Container();
            this.m_splitContainer = new System.Windows.Forms.SplitContainer();
            this.m_treeView = new GameExpress.Controls.TreeViewPath();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.m_pageTitle = new GameExpress.Controls.PageTitle();
            this.m_pageHolder = new GameExpress.Controls.PageHolder();
            ((System.ComponentModel.ISupportInitialize)(this.m_splitContainer)).BeginInit();
            this.m_splitContainer.Panel1.SuspendLayout();
            this.m_splitContainer.Panel2.SuspendLayout();
            this.m_splitContainer.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_splitContainer
            // 
            this.m_splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_splitContainer.Location = new System.Drawing.Point(0, 0);
            this.m_splitContainer.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.m_splitContainer.Name = "m_splitContainer";
            // 
            // m_splitContainer.Panel1
            // 
            this.m_splitContainer.Panel1.Controls.Add(this.m_treeView);
            // 
            // m_splitContainer.Panel2
            // 
            this.m_splitContainer.Panel2.Controls.Add(this.tableLayoutPanel1);
            this.m_splitContainer.Size = new System.Drawing.Size(1012, 802);
            this.m_splitContainer.SplitterDistance = 174;
            this.m_splitContainer.SplitterWidth = 6;
            this.m_splitContainer.TabIndex = 5;
            // 
            // m_treeView
            // 
            this.m_treeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_treeView.ImageIndex = 0;
            this.m_treeView.Location = new System.Drawing.Point(0, 0);
            this.m_treeView.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.m_treeView.Name = "m_treeView";
            this.m_treeView.SelectedImageIndex = 0;
            this.m_treeView.SelectedPath = null;
            this.m_treeView.Size = new System.Drawing.Size(174, 802);
            this.m_treeView.TabIndex = 0;
            this.m_treeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.OnAfterSelect);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.m_pageTitle, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.m_pageHolder, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 38F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(832, 802);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // m_pageTitle
            // 
            this.m_pageTitle.Data = null;
            this.m_pageTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_pageTitle.Image = null;
            this.m_pageTitle.Location = new System.Drawing.Point(3, 5);
            this.m_pageTitle.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.m_pageTitle.Name = "m_pageTitle";
            this.m_pageTitle.Size = new System.Drawing.Size(826, 28);
            this.m_pageTitle.TabIndex = 0;
            this.m_pageTitle.Title = "";
            // 
            // m_pageHolder
            // 
            this.m_pageHolder.Data = null;
            this.m_pageHolder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_pageHolder.Image = null;
            this.m_pageHolder.Location = new System.Drawing.Point(3, 42);
            this.m_pageHolder.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.m_pageHolder.Name = "m_pageHolder";
            this.m_pageHolder.Size = new System.Drawing.Size(826, 756);
            this.m_pageHolder.TabIndex = 1;
            this.m_pageHolder.Title = "";
            // 
            // PageTreeFrame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.m_splitContainer);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "PageTreeFrame";
            this.Size = new System.Drawing.Size(1012, 802);
            this.m_splitContainer.Panel1.ResumeLayout(false);
            this.m_splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.m_splitContainer)).EndInit();
            this.m_splitContainer.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer m_splitContainer;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private TreeViewPath m_treeView;
        private PageTitle m_pageTitle;
        private PageHolder m_pageHolder;


    }
}
