namespace GameExpress.Core.UIEditor
{
    partial class BrushEditor
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.m_propertyGrid = new System.Windows.Forms.PropertyGrid();
            this.m_previewPanel = new System.Windows.Forms.Panel();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.m_linearGradientRadioButton = new System.Windows.Forms.RadioButton();
            this.m_solidRadioButton = new System.Windows.Forms.RadioButton();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();;
            this.tableLayoutPanel3.SuspendLayout();
            this.tabControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_propertyGrid
            // 
            this.m_propertyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_propertyGrid.Location = new System.Drawing.Point(3, 21);
            this.m_propertyGrid.Name = "m_propertyGrid";
            this.m_propertyGrid.Size = new System.Drawing.Size(440, 15);
            this.m_propertyGrid.TabIndex = 1;
            this.m_propertyGrid.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.OnPropertyValueChanged);
            // 
            // m_previewPanel
            // 
            this.m_previewPanel.Location = new System.Drawing.Point(449, 10);
            this.m_previewPanel.Name = "m_previewPanel";
            this.m_previewPanel.Size = new System.Drawing.Size(154, 5);
            this.m_previewPanel.TabIndex = 2;
            this.m_previewPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.OnPreviewPaint);
            // 
            // radioButton3
            // 
            this.radioButton3.AutoSize = true;
            this.radioButton3.Location = new System.Drawing.Point(3, 10);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new System.Drawing.Size(42, 5);
            this.radioButton3.TabIndex = 0;
            this.radioButton3.TabStop = true;
            this.radioButton3.Text = "Bild";
            this.radioButton3.UseVisualStyleBackColor = true;
            // 
            // m_linearGradientRadioButton
            // 
            this.m_linearGradientRadioButton.AutoSize = true;
            this.m_linearGradientRadioButton.Location = new System.Drawing.Point(3, 9);
            this.m_linearGradientRadioButton.Name = "m_linearGradientRadioButton";
            this.m_linearGradientRadioButton.Size = new System.Drawing.Size(78, 1);
            this.m_linearGradientRadioButton.TabIndex = 0;
            this.m_linearGradientRadioButton.TabStop = true;
            this.m_linearGradientRadioButton.Text = "Farbverlauf";
            this.m_linearGradientRadioButton.UseVisualStyleBackColor = true;
            this.m_linearGradientRadioButton.CheckedChanged += new System.EventHandler(this.OnCheckdChanged);
            // 
            // m_solidRadioButton
            // 
            this.m_solidRadioButton.AutoSize = true;
            this.m_solidRadioButton.Location = new System.Drawing.Point(3, 3);
            this.m_solidRadioButton.Name = "m_solidRadioButton";
            this.m_solidRadioButton.Size = new System.Drawing.Size(52, 1);
            this.m_solidRadioButton.TabIndex = 0;
            this.m_solidRadioButton.TabStop = true;
            this.m_solidRadioButton.Text = "Farbe";
            this.m_solidRadioButton.UseVisualStyleBackColor = true;
            this.m_solidRadioButton.CheckedChanged += new System.EventHandler(this.OnCheckdChanged);
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.tableLayoutPanel3.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(213, 505);
            this.tableLayoutPanel3.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(207, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Vorschau:";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(682, 505);
            this.tabControl1.TabIndex = 2;
            // 
            // tabPage1
            // 
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(674, 479);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Gleichmäßige Füllung";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(674, 479);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Farbverlauf";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tableLayoutPanel3);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tabControl1);
            this.splitContainer1.Size = new System.Drawing.Size(899, 505);
            this.splitContainer1.SplitterDistance = 213;
            this.splitContainer1.TabIndex = 1;
            // 
            // BrushEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(899, 594);
            this.Name = "BrushEditor";
            this.ShowInTaskbar = false;
            this.Text = "Füllung bearbeiten";
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PropertyGrid m_propertyGrid;
        private System.Windows.Forms.Panel m_previewPanel;
        private System.Windows.Forms.RadioButton radioButton3;
        private System.Windows.Forms.RadioButton m_linearGradientRadioButton;
        private System.Windows.Forms.RadioButton m_solidRadioButton;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.SplitContainer splitContainer1;

    }
}