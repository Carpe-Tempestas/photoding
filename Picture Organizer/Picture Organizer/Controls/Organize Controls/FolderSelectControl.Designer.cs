namespace Trebuchet.Controls
{
    partial class FolderSelectControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FolderSelectControl));
			this.btnSourceFolderSelect = new System.Windows.Forms.Button();
			this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
			this.label1 = new System.Windows.Forms.Label();
			this.txtSource = new System.Windows.Forms.TextBox();
			this.btnDestinationFolderSelect = new System.Windows.Forms.Button();
			this.lblDestination = new System.Windows.Forms.Label();
			this.txtDestination = new System.Windows.Forms.TextBox();
			this.chkUseDefault = new System.Windows.Forms.CheckBox();
			this.lblAction = new System.Windows.Forms.Label();
			this.comboAction = new System.Windows.Forms.ComboBox();
			this.label2 = new System.Windows.Forms.Label();
			this.txtAlbum = new System.Windows.Forms.TextBox();
			this.chkAppendAlbum = new System.Windows.Forms.CheckBox();
			this.grpAlbum = new System.Windows.Forms.GroupBox();
			this.panel1 = new System.Windows.Forms.Panel();
			this.panel2 = new System.Windows.Forms.Panel();
			this.txtSettingDescription = new System.Windows.Forms.TextBox();
			this.panel4 = new System.Windows.Forms.Panel();
			this.label3 = new System.Windows.Forms.Label();
			this.splitter1 = new System.Windows.Forms.Splitter();
			this.panel3 = new System.Windows.Forms.Panel();
			this.listSettings = new System.Windows.Forms.ListView();
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.btnAddSetting = new System.Windows.Forms.ToolStripButton();
			this.btnCopySetting = new System.Windows.Forms.ToolStripButton();
			this.btnRemoveSetting = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.btnSave = new System.Windows.Forms.ToolStripButton();
			this.btnSaveAll = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
			this.txtRenameSetting = new System.Windows.Forms.ToolStripTextBox();
			this.grpAlbum.SuspendLayout();
			this.panel1.SuspendLayout();
			this.panel2.SuspendLayout();
			this.panel4.SuspendLayout();
			this.panel3.SuspendLayout();
			this.toolStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// btnSourceFolderSelect
			// 
			this.btnSourceFolderSelect.Location = new System.Drawing.Point(282, 5);
			this.btnSourceFolderSelect.Name = "btnSourceFolderSelect";
			this.btnSourceFolderSelect.Size = new System.Drawing.Size(24, 23);
			this.btnSourceFolderSelect.TabIndex = 5;
			this.btnSourceFolderSelect.Text = "...";
			this.btnSourceFolderSelect.UseVisualStyleBackColor = true;
			this.btnSourceFolderSelect.Click += new System.EventHandler(this.btnSourceFolderSelect_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(3, 10);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(41, 13);
			this.label1.TabIndex = 4;
			this.label1.Text = "Source";
			// 
			// txtSource
			// 
			this.txtSource.Location = new System.Drawing.Point(50, 7);
			this.txtSource.Name = "txtSource";
			this.txtSource.Size = new System.Drawing.Size(226, 20);
			this.txtSource.TabIndex = 3;
			this.txtSource.TextChanged += new System.EventHandler(this.txtSource_TextChanged);
			// 
			// btnDestinationFolderSelect
			// 
			this.btnDestinationFolderSelect.Location = new System.Drawing.Point(282, 34);
			this.btnDestinationFolderSelect.Name = "btnDestinationFolderSelect";
			this.btnDestinationFolderSelect.Size = new System.Drawing.Size(24, 23);
			this.btnDestinationFolderSelect.TabIndex = 8;
			this.btnDestinationFolderSelect.Text = "...";
			this.btnDestinationFolderSelect.UseVisualStyleBackColor = true;
			this.btnDestinationFolderSelect.Click += new System.EventHandler(this.btnDestinationFolderSelect_Click);
			// 
			// lblDestination
			// 
			this.lblDestination.AutoSize = true;
			this.lblDestination.Location = new System.Drawing.Point(2, 39);
			this.lblDestination.Name = "lblDestination";
			this.lblDestination.Size = new System.Drawing.Size(60, 13);
			this.lblDestination.TabIndex = 7;
			this.lblDestination.Text = "Destination";
			// 
			// txtDestination
			// 
			this.txtDestination.Location = new System.Drawing.Point(68, 36);
			this.txtDestination.Name = "txtDestination";
			this.txtDestination.Size = new System.Drawing.Size(208, 20);
			this.txtDestination.TabIndex = 6;
			this.txtDestination.TextChanged += new System.EventHandler(this.txtDestination_TextChanged);
			// 
			// chkUseDefault
			// 
			this.chkUseDefault.AutoSize = true;
			this.chkUseDefault.Checked = true;
			this.chkUseDefault.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkUseDefault.Location = new System.Drawing.Point(168, 64);
			this.chkUseDefault.Name = "chkUseDefault";
			this.chkUseDefault.Size = new System.Drawing.Size(138, 17);
			this.chkUseDefault.TabIndex = 11;
			this.chkUseDefault.Text = "Use Default Destination";
			this.chkUseDefault.UseVisualStyleBackColor = true;
			this.chkUseDefault.CheckedChanged += new System.EventHandler(this.chkUseDefault_CheckedChanged);
			// 
			// lblAction
			// 
			this.lblAction.AutoSize = true;
			this.lblAction.Location = new System.Drawing.Point(3, 65);
			this.lblAction.Name = "lblAction";
			this.lblAction.Size = new System.Drawing.Size(37, 13);
			this.lblAction.TabIndex = 12;
			this.lblAction.Text = "Action";
			// 
			// comboAction
			// 
			this.comboAction.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboAction.FormattingEnabled = true;
			this.comboAction.Items.AddRange(new object[] {
            "Move",
            "Copy",
            "Do Nothing"});
			this.comboAction.Location = new System.Drawing.Point(50, 62);
			this.comboAction.MaxDropDownItems = 3;
			this.comboAction.Name = "comboAction";
			this.comboAction.Size = new System.Drawing.Size(87, 21);
			this.comboAction.TabIndex = 13;
			this.comboAction.SelectedIndexChanged += new System.EventHandler(this.comboAction_SelectedIndexChanged);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(6, 33);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(35, 13);
			this.label2.TabIndex = 15;
			this.label2.Text = "Name";
			// 
			// txtAlbum
			// 
			this.txtAlbum.Location = new System.Drawing.Point(47, 30);
			this.txtAlbum.Name = "txtAlbum";
			this.txtAlbum.Size = new System.Drawing.Size(95, 20);
			this.txtAlbum.TabIndex = 14;
			this.txtAlbum.TextChanged += new System.EventHandler(this.txtAlbum_TextChanged);
			// 
			// chkAppendAlbum
			// 
			this.chkAppendAlbum.AutoSize = true;
			this.chkAppendAlbum.Checked = true;
			this.chkAppendAlbum.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkAppendAlbum.Location = new System.Drawing.Point(161, 32);
			this.chkAppendAlbum.Name = "chkAppendAlbum";
			this.chkAppendAlbum.Size = new System.Drawing.Size(129, 17);
			this.chkAppendAlbum.TabIndex = 16;
			this.chkAppendAlbum.Text = "Append to destination";
			this.chkAppendAlbum.UseVisualStyleBackColor = true;
			this.chkAppendAlbum.CheckedChanged += new System.EventHandler(this.chkAppendAlbum_CheckedChanged);
			// 
			// grpAlbum
			// 
			this.grpAlbum.Controls.Add(this.chkAppendAlbum);
			this.grpAlbum.Controls.Add(this.label2);
			this.grpAlbum.Controls.Add(this.txtAlbum);
			this.grpAlbum.Location = new System.Drawing.Point(9, 89);
			this.grpAlbum.Name = "grpAlbum";
			this.grpAlbum.Size = new System.Drawing.Size(296, 60);
			this.grpAlbum.TabIndex = 17;
			this.grpAlbum.TabStop = false;
			this.grpAlbum.Text = "Album";
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.label1);
			this.panel1.Controls.Add(this.grpAlbum);
			this.panel1.Controls.Add(this.txtSource);
			this.panel1.Controls.Add(this.comboAction);
			this.panel1.Controls.Add(this.btnSourceFolderSelect);
			this.panel1.Controls.Add(this.lblAction);
			this.panel1.Controls.Add(this.txtDestination);
			this.panel1.Controls.Add(this.chkUseDefault);
			this.panel1.Controls.Add(this.lblDestination);
			this.panel1.Controls.Add(this.btnDestinationFolderSelect);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel1.Location = new System.Drawing.Point(10, 10);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(312, 152);
			this.panel1.TabIndex = 18;
			// 
			// panel2
			// 
			this.panel2.Controls.Add(this.txtSettingDescription);
			this.panel2.Controls.Add(this.panel4);
			this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel2.Location = new System.Drawing.Point(10, 212);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(312, 65);
			this.panel2.TabIndex = 19;
			// 
			// txtSettingDescription
			// 
			this.txtSettingDescription.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtSettingDescription.Location = new System.Drawing.Point(0, 19);
			this.txtSettingDescription.Multiline = true;
			this.txtSettingDescription.Name = "txtSettingDescription";
			this.txtSettingDescription.Size = new System.Drawing.Size(312, 46);
			this.txtSettingDescription.TabIndex = 4;
			this.txtSettingDescription.TextChanged += new System.EventHandler(this.txtSettingDescription_TextChanged);
			// 
			// panel4
			// 
			this.panel4.Controls.Add(this.label3);
			this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel4.Location = new System.Drawing.Point(0, 0);
			this.panel4.Name = "panel4";
			this.panel4.Size = new System.Drawing.Size(312, 19);
			this.panel4.TabIndex = 0;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(3, 3);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(60, 13);
			this.label3.TabIndex = 15;
			this.label3.Text = "Description";
			// 
			// splitter1
			// 
			this.splitter1.BackColor = System.Drawing.Color.SteelBlue;
			this.splitter1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.splitter1.Location = new System.Drawing.Point(10, 210);
			this.splitter1.Name = "splitter1";
			this.splitter1.Size = new System.Drawing.Size(312, 2);
			this.splitter1.TabIndex = 20;
			this.splitter1.TabStop = false;
			// 
			// panel3
			// 
			this.panel3.Controls.Add(this.listSettings);
			this.panel3.Controls.Add(this.toolStrip1);
			this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel3.Location = new System.Drawing.Point(10, 162);
			this.panel3.Name = "panel3";
			this.panel3.Size = new System.Drawing.Size(312, 48);
			this.panel3.TabIndex = 21;
			// 
			// listSettings
			// 
			this.listSettings.Alignment = System.Windows.Forms.ListViewAlignment.Left;
			this.listSettings.AllowDrop = true;
			this.listSettings.Dock = System.Windows.Forms.DockStyle.Fill;
			this.listSettings.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
			this.listSettings.HideSelection = false;
			this.listSettings.LabelEdit = true;
			this.listSettings.LabelWrap = false;
			this.listSettings.Location = new System.Drawing.Point(0, 25);
			this.listSettings.MultiSelect = false;
			this.listSettings.Name = "listSettings";
			this.listSettings.Size = new System.Drawing.Size(312, 23);
			this.listSettings.TabIndex = 3;
			this.listSettings.UseCompatibleStateImageBehavior = false;
			this.listSettings.View = System.Windows.Forms.View.List;
			this.listSettings.DragEnter += new System.Windows.Forms.DragEventHandler(this.listSettings_DragEnter);
			this.listSettings.DragDrop += new System.Windows.Forms.DragEventHandler(this.listSettings_DragDrop);
			this.listSettings.SelectedIndexChanged += new System.EventHandler(this.listSettings_SelectedIndexChanged);
			this.listSettings.AfterLabelEdit += new System.Windows.Forms.LabelEditEventHandler(this.listSettings_AfterLabelEdit);
			this.listSettings.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.listSettings_ItemDrag);
			this.listSettings.BeforeLabelEdit += new System.Windows.Forms.LabelEditEventHandler(this.listSettings_BeforeLabelEdit);
			// 
			// toolStrip1
			// 
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnAddSetting,
            this.btnCopySetting,
            this.btnRemoveSetting,
            this.toolStripSeparator1,
            this.btnSave,
            this.btnSaveAll,
            this.toolStripSeparator2,
            this.toolStripLabel1,
            this.txtRenameSetting});
			this.toolStrip1.Location = new System.Drawing.Point(0, 0);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new System.Drawing.Size(312, 25);
			this.toolStrip1.TabIndex = 2;
			this.toolStrip1.Text = "toolStrip1";
			// 
			// btnAddSetting
			// 
			this.btnAddSetting.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.btnAddSetting.Image = ((System.Drawing.Image)(resources.GetObject("btnAddSetting.Image")));
			this.btnAddSetting.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.btnAddSetting.Name = "btnAddSetting";
			this.btnAddSetting.Size = new System.Drawing.Size(23, 22);
			this.btnAddSetting.Text = "New Setting";
			this.btnAddSetting.ToolTipText = "This will create a new setting.";
			this.btnAddSetting.Click += new System.EventHandler(this.btnAddSetting_Click);
			// 
			// btnCopySetting
			// 
			this.btnCopySetting.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.btnCopySetting.Enabled = false;
			this.btnCopySetting.Image = ((System.Drawing.Image)(resources.GetObject("btnCopySetting.Image")));
			this.btnCopySetting.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.btnCopySetting.Name = "btnCopySetting";
			this.btnCopySetting.Size = new System.Drawing.Size(23, 22);
			this.btnCopySetting.Text = "Copy Setting";
			this.btnCopySetting.ToolTipText = "This will create a copy of the currently selected setting.";
			this.btnCopySetting.Click += new System.EventHandler(this.btnCopySetting_Click);
			// 
			// btnRemoveSetting
			// 
			this.btnRemoveSetting.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.btnRemoveSetting.Enabled = false;
			this.btnRemoveSetting.Image = ((System.Drawing.Image)(resources.GetObject("btnRemoveSetting.Image")));
			this.btnRemoveSetting.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.btnRemoveSetting.Name = "btnRemoveSetting";
			this.btnRemoveSetting.Size = new System.Drawing.Size(23, 22);
			this.btnRemoveSetting.Text = "Delete Setting";
			this.btnRemoveSetting.ToolTipText = "This will delete the currently selected setting.";
			this.btnRemoveSetting.Click += new System.EventHandler(this.btnRemoveSetting_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
			// 
			// btnSave
			// 
			this.btnSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.btnSave.Enabled = false;
			this.btnSave.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.Image")));
			this.btnSave.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.btnSave.Name = "btnSave";
			this.btnSave.Size = new System.Drawing.Size(23, 22);
			this.btnSave.Text = "Save";
			this.btnSave.ToolTipText = "Save the currently selected setting.";
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			// 
			// btnSaveAll
			// 
			this.btnSaveAll.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.btnSaveAll.Enabled = false;
			this.btnSaveAll.Image = ((System.Drawing.Image)(resources.GetObject("btnSaveAll.Image")));
			this.btnSaveAll.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.btnSaveAll.Name = "btnSaveAll";
			this.btnSaveAll.Size = new System.Drawing.Size(23, 22);
			this.btnSaveAll.Text = "Save All";
			this.btnSaveAll.ToolTipText = "Saves all settings that have been changed.";
			this.btnSaveAll.Click += new System.EventHandler(this.btnSaveAll_Click);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
			// 
			// toolStripLabel1
			// 
			this.toolStripLabel1.Name = "toolStripLabel1";
			this.toolStripLabel1.Size = new System.Drawing.Size(50, 22);
			this.toolStripLabel1.Text = "Current:";
			// 
			// txtRenameSetting
			// 
			this.txtRenameSetting.Enabled = false;
			this.txtRenameSetting.Name = "txtRenameSetting";
			this.txtRenameSetting.Size = new System.Drawing.Size(100, 25);
			this.txtRenameSetting.TextChanged += new System.EventHandler(this.txtRenameSetting_TextChanged);
			// 
			// FolderSelectControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.White;
			this.Controls.Add(this.panel3);
			this.Controls.Add(this.splitter1);
			this.Controls.Add(this.panel2);
			this.Controls.Add(this.panel1);
			this.MinimumSize = new System.Drawing.Size(332, 287);
			this.Name = "FolderSelectControl";
			this.Padding = new System.Windows.Forms.Padding(10);
			this.Size = new System.Drawing.Size(332, 287);
			this.grpAlbum.ResumeLayout(false);
			this.grpAlbum.PerformLayout();
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.panel2.ResumeLayout(false);
			this.panel2.PerformLayout();
			this.panel4.ResumeLayout(false);
			this.panel4.PerformLayout();
			this.panel3.ResumeLayout(false);
			this.panel3.PerformLayout();
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnSourceFolderSelect;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtSource;
        private System.Windows.Forms.Button btnDestinationFolderSelect;
        private System.Windows.Forms.Label lblDestination;
        private System.Windows.Forms.TextBox txtDestination;
        private System.Windows.Forms.CheckBox chkUseDefault;
        private System.Windows.Forms.Label lblAction;
        private System.Windows.Forms.ComboBox comboAction;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtAlbum;
		private System.Windows.Forms.CheckBox chkAppendAlbum;
		private System.Windows.Forms.GroupBox grpAlbum;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Splitter splitter1;
		private System.Windows.Forms.Panel panel3;
		private System.Windows.Forms.ListView listSettings;
		private System.Windows.Forms.ToolStrip toolStrip1;
		private System.Windows.Forms.ToolStripButton btnAddSetting;
		private System.Windows.Forms.ToolStripButton btnCopySetting;
		private System.Windows.Forms.ToolStripButton btnRemoveSetting;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripButton btnSave;
		private System.Windows.Forms.ToolStripButton btnSaveAll;
		private System.Windows.Forms.Panel panel4;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox txtSettingDescription;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripTextBox txtRenameSetting;
		private System.Windows.Forms.ToolStripLabel toolStripLabel1;
    }
}
