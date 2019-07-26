namespace Key_Manager
{
    partial class mainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(mainForm));
            this.listKeys = new System.Windows.Forms.ListView();
            this.colRoomSpace = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colKeyCodeRm = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colKeyCodeApt = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colStudentId = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colNameLast = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colNameFirst = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colDateStamp = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newEntryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importCSVToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportCSVToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.refreshToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setServerLocationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createDatabaseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.colorsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.changeDefaultColorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.changeAPTOutColorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.changeRMOutColorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.changeBothOutColorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuStripItemEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStripItemDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStripItemMissingRm = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStripItemMissingApt = new System.Windows.Forms.ToolStripMenuItem();
            this.generateEmailToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // listKeys
            // 
            this.listKeys.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colRoomSpace,
            this.colKeyCodeRm,
            this.colKeyCodeApt,
            this.colStudentId,
            this.colNameLast,
            this.colNameFirst,
            this.colDateStamp});
            this.listKeys.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listKeys.FullRowSelect = true;
            this.listKeys.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.listKeys.Location = new System.Drawing.Point(0, 24);
            this.listKeys.MultiSelect = false;
            this.listKeys.Name = "listKeys";
            this.listKeys.ShowItemToolTips = true;
            this.listKeys.Size = new System.Drawing.Size(762, 505);
            this.listKeys.TabIndex = 1;
            this.listKeys.UseCompatibleStateImageBehavior = false;
            this.listKeys.View = System.Windows.Forms.View.Details;
            // 
            // colRoomSpace
            // 
            this.colRoomSpace.Text = "Room Space";
            this.colRoomSpace.Width = 109;
            // 
            // colKeyCodeRm
            // 
            this.colKeyCodeRm.Text = "Key Code (Room)";
            this.colKeyCodeRm.Width = 111;
            // 
            // colKeyCodeApt
            // 
            this.colKeyCodeApt.Text = "Key Code (APT)";
            this.colKeyCodeApt.Width = 92;
            // 
            // colStudentId
            // 
            this.colStudentId.Text = "Student ID";
            this.colStudentId.Width = 99;
            // 
            // colNameLast
            // 
            this.colNameLast.Text = "Last Name";
            this.colNameLast.Width = 102;
            // 
            // colNameFirst
            // 
            this.colNameFirst.Text = "First Name";
            this.colNameFirst.Width = 96;
            // 
            // colDateStamp
            // 
            this.colDateStamp.Text = "Date Last Updated";
            this.colDateStamp.Width = 105;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.settingsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(762, 24);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newEntryToolStripMenuItem,
            this.importCSVToolStripMenuItem,
            this.exportCSVToolStripMenuItem,
            this.refreshToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // newEntryToolStripMenuItem
            // 
            this.newEntryToolStripMenuItem.Name = "newEntryToolStripMenuItem";
            this.newEntryToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.newEntryToolStripMenuItem.Text = "New Entry";
            this.newEntryToolStripMenuItem.Click += new System.EventHandler(this.NewEntryToolStripMenuItem_Click);
            // 
            // importCSVToolStripMenuItem
            // 
            this.importCSVToolStripMenuItem.Name = "importCSVToolStripMenuItem";
            this.importCSVToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.importCSVToolStripMenuItem.Text = "Import Keys";
            this.importCSVToolStripMenuItem.Click += new System.EventHandler(this.ImportCSVToolStripMenuItem_Click);
            // 
            // exportCSVToolStripMenuItem
            // 
            this.exportCSVToolStripMenuItem.Name = "exportCSVToolStripMenuItem";
            this.exportCSVToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.exportCSVToolStripMenuItem.Text = "Export Keys";
            this.exportCSVToolStripMenuItem.Click += new System.EventHandler(this.ExportCSVToolStripMenuItem_Click);
            // 
            // refreshToolStripMenuItem
            // 
            this.refreshToolStripMenuItem.Name = "refreshToolStripMenuItem";
            this.refreshToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.refreshToolStripMenuItem.Text = "Refresh";
            this.refreshToolStripMenuItem.Click += new System.EventHandler(this.RefreshToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.ExitToolStripMenuItem_Click);
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.setServerLocationToolStripMenuItem,
            this.createDatabaseToolStripMenuItem,
            this.colorsToolStripMenuItem});
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.settingsToolStripMenuItem.Text = "Settings";
            // 
            // setServerLocationToolStripMenuItem
            // 
            this.setServerLocationToolStripMenuItem.Name = "setServerLocationToolStripMenuItem";
            this.setServerLocationToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.setServerLocationToolStripMenuItem.Text = "Select Database";
            this.setServerLocationToolStripMenuItem.Click += new System.EventHandler(this.SetServerLocationToolStripMenuItem_Click);
            // 
            // createDatabaseToolStripMenuItem
            // 
            this.createDatabaseToolStripMenuItem.Name = "createDatabaseToolStripMenuItem";
            this.createDatabaseToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.createDatabaseToolStripMenuItem.Text = "Create Database";
            this.createDatabaseToolStripMenuItem.Click += new System.EventHandler(this.CreateDatabaseToolStripMenuItem_Click);
            // 
            // colorsToolStripMenuItem
            // 
            this.colorsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.changeDefaultColorToolStripMenuItem,
            this.changeAPTOutColorToolStripMenuItem,
            this.changeRMOutColorToolStripMenuItem,
            this.changeBothOutColorToolStripMenuItem});
            this.colorsToolStripMenuItem.Name = "colorsToolStripMenuItem";
            this.colorsToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.colorsToolStripMenuItem.Text = "Colors";
            this.colorsToolStripMenuItem.Click += new System.EventHandler(this.ColorsToolStripMenuItem_Click);
            // 
            // changeDefaultColorToolStripMenuItem
            // 
            this.changeDefaultColorToolStripMenuItem.Name = "changeDefaultColorToolStripMenuItem";
            this.changeDefaultColorToolStripMenuItem.Size = new System.Drawing.Size(198, 22);
            this.changeDefaultColorToolStripMenuItem.Text = "Change Default Color";
            this.changeDefaultColorToolStripMenuItem.Click += new System.EventHandler(this.ChangeDefaultColorToolStripMenuItem_Click);
            // 
            // changeAPTOutColorToolStripMenuItem
            // 
            this.changeAPTOutColorToolStripMenuItem.Name = "changeAPTOutColorToolStripMenuItem";
            this.changeAPTOutColorToolStripMenuItem.Size = new System.Drawing.Size(198, 22);
            this.changeAPTOutColorToolStripMenuItem.Text = "Change APT Out Color";
            this.changeAPTOutColorToolStripMenuItem.Click += new System.EventHandler(this.ChangeAPTOutColorToolStripMenuItem_Click);
            // 
            // changeRMOutColorToolStripMenuItem
            // 
            this.changeRMOutColorToolStripMenuItem.Name = "changeRMOutColorToolStripMenuItem";
            this.changeRMOutColorToolStripMenuItem.Size = new System.Drawing.Size(198, 22);
            this.changeRMOutColorToolStripMenuItem.Text = "Change RM Out Color";
            this.changeRMOutColorToolStripMenuItem.Click += new System.EventHandler(this.ChangeRMOutColorToolStripMenuItem_Click);
            // 
            // changeBothOutColorToolStripMenuItem
            // 
            this.changeBothOutColorToolStripMenuItem.Name = "changeBothOutColorToolStripMenuItem";
            this.changeBothOutColorToolStripMenuItem.Size = new System.Drawing.Size(198, 22);
            this.changeBothOutColorToolStripMenuItem.Text = "Change Both Out Color";
            this.changeBothOutColorToolStripMenuItem.Click += new System.EventHandler(this.ChangeBothOutColorToolStripMenuItem_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.progressBar1.Location = new System.Drawing.Point(0, 506);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(762, 23);
            this.progressBar1.TabIndex = 3;
            this.progressBar1.Visible = false;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuStripItemEdit,
            this.menuStripItemDelete,
            this.menuStripItemMissingRm,
            this.menuStripItemMissingApt,
            this.generateEmailToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(215, 114);
            // 
            // menuStripItemEdit
            // 
            this.menuStripItemEdit.Name = "menuStripItemEdit";
            this.menuStripItemEdit.Size = new System.Drawing.Size(214, 22);
            this.menuStripItemEdit.Text = "Edit Entry";
            this.menuStripItemEdit.Click += new System.EventHandler(this.MenuStripItemEdit_Click);
            // 
            // menuStripItemDelete
            // 
            this.menuStripItemDelete.Name = "menuStripItemDelete";
            this.menuStripItemDelete.Size = new System.Drawing.Size(214, 22);
            this.menuStripItemDelete.Text = "Delete Entry";
            this.menuStripItemDelete.Click += new System.EventHandler(this.MenuStripItemDelete_Click);
            // 
            // menuStripItemMissingRm
            // 
            this.menuStripItemMissingRm.Name = "menuStripItemMissingRm";
            this.menuStripItemMissingRm.Size = new System.Drawing.Size(214, 22);
            this.menuStripItemMissingRm.Text = "Toggle Missing Room";
            this.menuStripItemMissingRm.Click += new System.EventHandler(this.MenuStripItemMissingRm_Click);
            // 
            // menuStripItemMissingApt
            // 
            this.menuStripItemMissingApt.Name = "menuStripItemMissingApt";
            this.menuStripItemMissingApt.Size = new System.Drawing.Size(214, 22);
            this.menuStripItemMissingApt.Text = "Toggle Missing Apartment";
            this.menuStripItemMissingApt.Click += new System.EventHandler(this.MenuStripItemMissingApt_Click);
            // 
            // generateEmailToolStripMenuItem
            // 
            this.generateEmailToolStripMenuItem.Name = "generateEmailToolStripMenuItem";
            this.generateEmailToolStripMenuItem.Size = new System.Drawing.Size(214, 22);
            this.generateEmailToolStripMenuItem.Text = "Generate Email";
            this.generateEmailToolStripMenuItem.Click += new System.EventHandler(this.GenerateEmailToolStripMenuItem_Click);
            // 
            // mainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(762, 529);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.listKeys);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "mainForm";
            this.Text = "Spare Key Manager";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView listKeys;
        private System.Windows.Forms.ColumnHeader colRoomSpace;
        private System.Windows.Forms.ColumnHeader colKeyCodeRm;
        private System.Windows.Forms.ColumnHeader colKeyCodeApt;
        private System.Windows.Forms.ColumnHeader colStudentId;
        private System.Windows.Forms.ColumnHeader colNameFirst;
        private System.Windows.Forms.ColumnHeader colDateStamp;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newEntryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importCSVToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportCSVToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem setServerLocationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem colorsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ColumnHeader colNameLast;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.ToolStripMenuItem createDatabaseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem changeDefaultColorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem changeAPTOutColorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem changeRMOutColorToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem menuStripItemEdit;
        private System.Windows.Forms.ToolStripMenuItem menuStripItemDelete;
        private System.Windows.Forms.ToolStripMenuItem menuStripItemMissingRm;
        private System.Windows.Forms.ToolStripMenuItem menuStripItemMissingApt;
        private System.Windows.Forms.ToolStripMenuItem changeBothOutColorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem refreshToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem generateEmailToolStripMenuItem;
    }
}

