namespace GameExpress.View
{
    partial class FormMain
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.m_menuStrip = new System.Windows.Forms.MenuStrip();
            this.dateiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.m_newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.m_openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.m_saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.speichernUnterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.m_printToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.seitenansichtToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.m_exportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.einstellungenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.beendenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ansichtToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.m_currentPagesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aktuallisierenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.m_nextToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.m_lastToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.extrasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hilfeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.m_userManualToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.überToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.m_toolStrip = new System.Windows.Forms.ToolStrip();
            this.m_lastToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.m_nextToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.m_printToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.m_refreshToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.m_exportToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.m_statusStrip = new System.Windows.Forms.StatusStrip();
            this.m_toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.m_pageTreeFrame = new GameExpress.Controls.PageTreeFrame();
            this.m_timer = new System.Windows.Forms.Timer(this.components);
            this.m_splitContainer = new System.Windows.Forms.SplitContainer();
            this.m_propertyGrid = new GameExpress.Controls.PropertyGrid();
            this.m_menuStrip.SuspendLayout();
            this.m_toolStrip.SuspendLayout();
            this.m_statusStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_splitContainer)).BeginInit();
            this.m_splitContainer.Panel1.SuspendLayout();
            this.m_splitContainer.Panel2.SuspendLayout();
            this.m_splitContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_menuStrip
            // 
            this.m_menuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.m_menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dateiToolStripMenuItem,
            this.ansichtToolStripMenuItem,
            this.extrasToolStripMenuItem,
            this.hilfeToolStripMenuItem});
            this.m_menuStrip.Location = new System.Drawing.Point(0, 0);
            this.m_menuStrip.Name = "m_menuStrip";
            this.m_menuStrip.Padding = new System.Windows.Forms.Padding(14, 3, 0, 3);
            this.m_menuStrip.Size = new System.Drawing.Size(2005, 49);
            this.m_menuStrip.TabIndex = 1;
            this.m_menuStrip.Text = "menuStrip1";
            // 
            // dateiToolStripMenuItem
            // 
            this.dateiToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_newToolStripMenuItem,
            this.m_openToolStripMenuItem,
            this.m_saveToolStripMenuItem,
            this.speichernUnterToolStripMenuItem,
            this.toolStripSeparator3,
            this.m_printToolStripMenuItem,
            this.seitenansichtToolStripMenuItem,
            this.toolStripSeparator4,
            this.m_exportToolStripMenuItem,
            this.einstellungenToolStripMenuItem,
            this.toolStripMenuItem1,
            this.beendenToolStripMenuItem});
            this.dateiToolStripMenuItem.Name = "dateiToolStripMenuItem";
            this.dateiToolStripMenuItem.Size = new System.Drawing.Size(92, 43);
            this.dateiToolStripMenuItem.Text = "Datei";
            // 
            // m_newToolStripMenuItem
            // 
            this.m_newToolStripMenuItem.Image = global::GameExpress.Properties.Resources.page;
            this.m_newToolStripMenuItem.Name = "m_newToolStripMenuItem";
            this.m_newToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.m_newToolStripMenuItem.Size = new System.Drawing.Size(580, 42);
            this.m_newToolStripMenuItem.Text = "Neu";
            // 
            // m_openToolStripMenuItem
            // 
            this.m_openToolStripMenuItem.Image = global::GameExpress.Properties.Resources.folder_page;
            this.m_openToolStripMenuItem.Name = "m_openToolStripMenuItem";
            this.m_openToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.m_openToolStripMenuItem.Size = new System.Drawing.Size(580, 42);
            this.m_openToolStripMenuItem.Text = "Öffnen...";
            this.m_openToolStripMenuItem.Click += new System.EventHandler(this.OnOpenDokument);
            // 
            // m_saveToolStripMenuItem
            // 
            this.m_saveToolStripMenuItem.Image = global::GameExpress.Properties.Resources.disk;
            this.m_saveToolStripMenuItem.Name = "m_saveToolStripMenuItem";
            this.m_saveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.m_saveToolStripMenuItem.Size = new System.Drawing.Size(580, 42);
            this.m_saveToolStripMenuItem.Text = "Speichen";
            this.m_saveToolStripMenuItem.Click += new System.EventHandler(this.OnSaveDokument);
            // 
            // speichernUnterToolStripMenuItem
            // 
            this.speichernUnterToolStripMenuItem.Name = "speichernUnterToolStripMenuItem";
            this.speichernUnterToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Alt) 
            | System.Windows.Forms.Keys.S)));
            this.speichernUnterToolStripMenuItem.Size = new System.Drawing.Size(580, 42);
            this.speichernUnterToolStripMenuItem.Text = "Speichern unter ...";
            this.speichernUnterToolStripMenuItem.Click += new System.EventHandler(this.OnSaveAsDokument);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(577, 6);
            // 
            // m_printToolStripMenuItem
            // 
            this.m_printToolStripMenuItem.Image = global::GameExpress.Properties.Resources.printer;
            this.m_printToolStripMenuItem.Name = "m_printToolStripMenuItem";
            this.m_printToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.P)));
            this.m_printToolStripMenuItem.Size = new System.Drawing.Size(580, 42);
            this.m_printToolStripMenuItem.Text = "Drucken...";
            this.m_printToolStripMenuItem.Click += new System.EventHandler(this.OnPrint);
            // 
            // seitenansichtToolStripMenuItem
            // 
            this.seitenansichtToolStripMenuItem.Image = global::GameExpress.Properties.Resources.page_white_magnify;
            this.seitenansichtToolStripMenuItem.Name = "seitenansichtToolStripMenuItem";
            this.seitenansichtToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.P)));
            this.seitenansichtToolStripMenuItem.Size = new System.Drawing.Size(580, 42);
            this.seitenansichtToolStripMenuItem.Text = "Seitenansicht...";
            this.seitenansichtToolStripMenuItem.Click += new System.EventHandler(this.OnPreviewPrint);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(577, 6);
            // 
            // m_exportToolStripMenuItem
            // 
            this.m_exportToolStripMenuItem.Image = global::GameExpress.Properties.Resources.table_save;
            this.m_exportToolStripMenuItem.Name = "m_exportToolStripMenuItem";
            this.m_exportToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.E)));
            this.m_exportToolStripMenuItem.Size = new System.Drawing.Size(580, 42);
            this.m_exportToolStripMenuItem.Text = "Exportieren...";
            this.m_exportToolStripMenuItem.Click += new System.EventHandler(this.OnExport);
            // 
            // einstellungenToolStripMenuItem
            // 
            this.einstellungenToolStripMenuItem.Image = global::GameExpress.Properties.Resources.cog;
            this.einstellungenToolStripMenuItem.Name = "einstellungenToolStripMenuItem";
            this.einstellungenToolStripMenuItem.Size = new System.Drawing.Size(580, 42);
            this.einstellungenToolStripMenuItem.Text = "Einstellungen...";
            this.einstellungenToolStripMenuItem.Click += new System.EventHandler(this.OnProperty);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(577, 6);
            // 
            // beendenToolStripMenuItem
            // 
            this.beendenToolStripMenuItem.Image = global::GameExpress.Properties.Resources.cancel;
            this.beendenToolStripMenuItem.Name = "beendenToolStripMenuItem";
            this.beendenToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.beendenToolStripMenuItem.Size = new System.Drawing.Size(580, 42);
            this.beendenToolStripMenuItem.Text = "Beenden";
            this.beendenToolStripMenuItem.Click += new System.EventHandler(this.OnExit);
            // 
            // ansichtToolStripMenuItem
            // 
            this.ansichtToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_currentPagesToolStripMenuItem,
            this.aktuallisierenToolStripMenuItem,
            this.toolStripSeparator8,
            this.m_nextToolStripMenuItem,
            this.m_lastToolStripMenuItem});
            this.ansichtToolStripMenuItem.Name = "ansichtToolStripMenuItem";
            this.ansichtToolStripMenuItem.Size = new System.Drawing.Size(115, 43);
            this.ansichtToolStripMenuItem.Text = "Ansicht";
            // 
            // m_currentPagesToolStripMenuItem
            // 
            this.m_currentPagesToolStripMenuItem.Name = "m_currentPagesToolStripMenuItem";
            this.m_currentPagesToolStripMenuItem.Size = new System.Drawing.Size(512, 42);
            this.m_currentPagesToolStripMenuItem.Text = "Aktuelle Ansicht";
            // 
            // aktuallisierenToolStripMenuItem
            // 
            this.aktuallisierenToolStripMenuItem.Image = global::GameExpress.Properties.Resources.arrow_refresh_small;
            this.aktuallisierenToolStripMenuItem.Name = "aktuallisierenToolStripMenuItem";
            this.aktuallisierenToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.aktuallisierenToolStripMenuItem.Size = new System.Drawing.Size(512, 42);
            this.aktuallisierenToolStripMenuItem.Text = "&Aktuallisieren";
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(509, 6);
            // 
            // m_nextToolStripMenuItem
            // 
            this.m_nextToolStripMenuItem.Image = global::GameExpress.Properties.Resources.next;
            this.m_nextToolStripMenuItem.Name = "m_nextToolStripMenuItem";
            this.m_nextToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.Up)));
            this.m_nextToolStripMenuItem.Size = new System.Drawing.Size(512, 42);
            this.m_nextToolStripMenuItem.Text = "Vorwärts navigieren";
            // 
            // m_lastToolStripMenuItem
            // 
            this.m_lastToolStripMenuItem.Image = global::GameExpress.Properties.Resources.last;
            this.m_lastToolStripMenuItem.Name = "m_lastToolStripMenuItem";
            this.m_lastToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.Down)));
            this.m_lastToolStripMenuItem.Size = new System.Drawing.Size(512, 42);
            this.m_lastToolStripMenuItem.Text = "Rückwärts navigieren";
            // 
            // extrasToolStripMenuItem
            // 
            this.extrasToolStripMenuItem.Name = "extrasToolStripMenuItem";
            this.extrasToolStripMenuItem.Size = new System.Drawing.Size(98, 43);
            this.extrasToolStripMenuItem.Text = "Extras";
            // 
            // hilfeToolStripMenuItem
            // 
            this.hilfeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_userManualToolStripMenuItem,
            this.toolStripSeparator6,
            this.überToolStripMenuItem});
            this.hilfeToolStripMenuItem.Name = "hilfeToolStripMenuItem";
            this.hilfeToolStripMenuItem.Size = new System.Drawing.Size(84, 43);
            this.hilfeToolStripMenuItem.Text = "Hilfe";
            // 
            // m_userManualToolStripMenuItem
            // 
            this.m_userManualToolStripMenuItem.Image = global::GameExpress.Properties.Resources.page_white_acrobat;
            this.m_userManualToolStripMenuItem.Name = "m_userManualToolStripMenuItem";
            this.m_userManualToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F1;
            this.m_userManualToolStripMenuItem.Size = new System.Drawing.Size(390, 42);
            this.m_userManualToolStripMenuItem.Text = "Benutzerhandbuch";
            this.m_userManualToolStripMenuItem.Click += new System.EventHandler(this.OnUserManual);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(387, 6);
            // 
            // überToolStripMenuItem
            // 
            this.überToolStripMenuItem.Image = global::GameExpress.Properties.Resources.help;
            this.überToolStripMenuItem.Name = "überToolStripMenuItem";
            this.überToolStripMenuItem.Size = new System.Drawing.Size(390, 42);
            this.überToolStripMenuItem.Text = "Über...";
            this.überToolStripMenuItem.Click += new System.EventHandler(this.OnAbout);
            // 
            // m_toolStrip
            // 
            this.m_toolStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.m_toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_lastToolStripButton,
            this.m_nextToolStripButton,
            this.toolStripSeparator7,
            this.m_printToolStripButton,
            this.toolStripSeparator1,
            this.m_refreshToolStripButton,
            this.toolStripSeparator2,
            this.m_exportToolStripButton});
            this.m_toolStrip.Location = new System.Drawing.Point(0, 49);
            this.m_toolStrip.Name = "m_toolStrip";
            this.m_toolStrip.Padding = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this.m_toolStrip.Size = new System.Drawing.Size(2005, 44);
            this.m_toolStrip.TabIndex = 2;
            this.m_toolStrip.Text = "toolStrip1";
            // 
            // m_lastToolStripButton
            // 
            this.m_lastToolStripButton.Image = global::GameExpress.Properties.Resources.last;
            this.m_lastToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.m_lastToolStripButton.Name = "m_lastToolStripButton";
            this.m_lastToolStripButton.Size = new System.Drawing.Size(120, 41);
            this.m_lastToolStripButton.Text = "Zurück";
            this.m_lastToolStripButton.ToolTipText = "Rückwärts navigieren";
            // 
            // m_nextToolStripButton
            // 
            this.m_nextToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.m_nextToolStripButton.Image = global::GameExpress.Properties.Resources.next;
            this.m_nextToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.m_nextToolStripButton.Name = "m_nextToolStripButton";
            this.m_nextToolStripButton.Size = new System.Drawing.Size(24, 41);
            this.m_nextToolStripButton.Text = "Weiter";
            this.m_nextToolStripButton.ToolTipText = "Vorwärts navigieren";
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(6, 44);
            // 
            // m_printToolStripButton
            // 
            this.m_printToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.m_printToolStripButton.Image = global::GameExpress.Properties.Resources.printer;
            this.m_printToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.m_printToolStripButton.Name = "m_printToolStripButton";
            this.m_printToolStripButton.Size = new System.Drawing.Size(24, 41);
            this.m_printToolStripButton.Text = "Drucken";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 44);
            // 
            // m_refreshToolStripButton
            // 
            this.m_refreshToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.m_refreshToolStripButton.Image = global::GameExpress.Properties.Resources.arrow_refresh_small;
            this.m_refreshToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.m_refreshToolStripButton.Name = "m_refreshToolStripButton";
            this.m_refreshToolStripButton.Size = new System.Drawing.Size(24, 41);
            this.m_refreshToolStripButton.Text = "Aktualisieren";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 44);
            // 
            // m_exportToolStripButton
            // 
            this.m_exportToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.m_exportToolStripButton.Image = global::GameExpress.Properties.Resources.table_save;
            this.m_exportToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.m_exportToolStripButton.Name = "m_exportToolStripButton";
            this.m_exportToolStripButton.Size = new System.Drawing.Size(24, 41);
            this.m_exportToolStripButton.Text = "Exportieren";
            // 
            // m_statusStrip
            // 
            this.m_statusStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.m_statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_toolStripStatusLabel});
            this.m_statusStrip.Location = new System.Drawing.Point(0, 1127);
            this.m_statusStrip.Name = "m_statusStrip";
            this.m_statusStrip.Padding = new System.Windows.Forms.Padding(2, 0, 33, 0);
            this.m_statusStrip.Size = new System.Drawing.Size(2005, 42);
            this.m_statusStrip.TabIndex = 3;
            this.m_statusStrip.Text = "statusStrip1";
            // 
            // m_toolStripStatusLabel
            // 
            this.m_toolStripStatusLabel.Name = "m_toolStripStatusLabel";
            this.m_toolStripStatusLabel.Size = new System.Drawing.Size(1970, 37);
            this.m_toolStripStatusLabel.Spring = true;
            this.m_toolStripStatusLabel.Text = "Bereit";
            this.m_toolStripStatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // m_pageTreeFrame
            // 
            this.m_pageTreeFrame.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_pageTreeFrame.Location = new System.Drawing.Point(0, 0);
            this.m_pageTreeFrame.Margin = new System.Windows.Forms.Padding(5, 7, 5, 7);
            this.m_pageTreeFrame.Name = "m_pageTreeFrame";
            this.m_pageTreeFrame.SelectedPath = null;
            this.m_pageTreeFrame.Size = new System.Drawing.Size(1686, 1034);
            this.m_pageTreeFrame.TabIndex = 4;
            this.m_pageTreeFrame.TreeContextMenuStrip = null;
            // 
            // m_timer
            // 
            this.m_timer.Enabled = true;
            // 
            // m_splitContainer
            // 
            this.m_splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_splitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.m_splitContainer.Location = new System.Drawing.Point(0, 93);
            this.m_splitContainer.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.m_splitContainer.Name = "m_splitContainer";
            // 
            // m_splitContainer.Panel1
            // 
            this.m_splitContainer.Panel1.Controls.Add(this.m_pageTreeFrame);
            // 
            // m_splitContainer.Panel2
            // 
            this.m_splitContainer.Panel2.Controls.Add(this.m_propertyGrid);
            this.m_splitContainer.Size = new System.Drawing.Size(2005, 1034);
            this.m_splitContainer.SplitterDistance = 1686;
            this.m_splitContainer.SplitterWidth = 6;
            this.m_splitContainer.TabIndex = 5;
            // 
            // m_propertyGrid
            // 
            this.m_propertyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_propertyGrid.Location = new System.Drawing.Point(0, 0);
            this.m_propertyGrid.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.m_propertyGrid.Name = "m_propertyGrid";
            this.m_propertyGrid.Size = new System.Drawing.Size(313, 1034);
            this.m_propertyGrid.TabIndex = 0;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(14F, 29F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(2005, 1169);
            this.Controls.Add(this.m_splitContainer);
            this.Controls.Add(this.m_statusStrip);
            this.Controls.Add(this.m_toolStrip);
            this.Controls.Add(this.m_menuStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.Name = "FormMain";
            this.Text = "GameExpress";
            this.m_menuStrip.ResumeLayout(false);
            this.m_menuStrip.PerformLayout();
            this.m_toolStrip.ResumeLayout(false);
            this.m_toolStrip.PerformLayout();
            this.m_statusStrip.ResumeLayout(false);
            this.m_statusStrip.PerformLayout();
            this.m_splitContainer.Panel1.ResumeLayout(false);
            this.m_splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.m_splitContainer)).EndInit();
            this.m_splitContainer.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip m_menuStrip;
        private System.Windows.Forms.ToolStripMenuItem dateiToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem m_newToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem m_printToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem seitenansichtToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem m_exportToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem einstellungenToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem beendenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ansichtToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem m_currentPagesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aktuallisierenToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.ToolStripMenuItem m_nextToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem m_lastToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem extrasToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hilfeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem m_userManualToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripMenuItem überToolStripMenuItem;
        private System.Windows.Forms.ToolStrip m_toolStrip;
        private System.Windows.Forms.ToolStripButton m_lastToolStripButton;
        private System.Windows.Forms.ToolStripButton m_nextToolStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripButton m_printToolStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton m_refreshToolStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton m_exportToolStripButton;
        private System.Windows.Forms.StatusStrip m_statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel m_toolStripStatusLabel;
        private System.Windows.Forms.ToolStripMenuItem m_openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem m_saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem speichernUnterToolStripMenuItem;
        private Controls.PageTreeFrame m_pageTreeFrame;
        private System.Windows.Forms.Timer m_timer;
        private System.Windows.Forms.SplitContainer m_splitContainer;
        private Controls.PropertyGrid m_propertyGrid;
    }
}