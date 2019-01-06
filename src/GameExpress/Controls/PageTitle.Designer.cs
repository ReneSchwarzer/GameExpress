namespace GameExpress.Controls
{
    partial class PageTitle
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PageTitle));
            this.m_titleLabel = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.m_inageLabel = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_titleLabel
            // 
            this.m_titleLabel.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.m_titleLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_titleLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_titleLabel.Location = new System.Drawing.Point(31, 0);
            this.m_titleLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.m_titleLabel.Name = "m_titleLabel";
            this.m_titleLabel.Size = new System.Drawing.Size(560, 37);
            this.m_titleLabel.TabIndex = 8;
            this.m_titleLabel.Text = "Tabelle";
            this.m_titleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.m_titleLabel, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.m_inageLabel, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(4);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(595, 37);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // m_inageLabel
            // 
            this.m_inageLabel.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.m_inageLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_inageLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_inageLabel.Image = ((System.Drawing.Image)(resources.GetObject("m_inageLabel.Image")));
            this.m_inageLabel.Location = new System.Drawing.Point(4, 0);
            this.m_inageLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.m_inageLabel.Name = "m_inageLabel";
            this.m_inageLabel.Size = new System.Drawing.Size(19, 37);
            this.m_inageLabel.TabIndex = 7;
            this.m_inageLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // PageTitle
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "PageTitle";
            this.Size = new System.Drawing.Size(595, 37);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label m_titleLabel;
        private System.Windows.Forms.Label m_inageLabel;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    }
}
