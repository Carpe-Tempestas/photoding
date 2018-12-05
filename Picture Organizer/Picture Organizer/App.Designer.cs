namespace Trebuchet
{
    partial class App
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(App));
			this.lblMode = new System.Windows.Forms.Label();
			this.btnContinue = new System.Windows.Forms.Button();
			this.menuAppStrip1 = new System.Windows.Forms.MenuStrip();
			this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.checkForUpdatesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.txtUsername = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.mtxtPassword = new System.Windows.Forms.MaskedTextBox();
			this.btnActivate = new System.Windows.Forms.Button();
			this.lblTrialRemaining = new System.Windows.Forms.Label();
			this.lblLicense = new System.Windows.Forms.LinkLabel();
			this.panelActivate = new System.Windows.Forms.Panel();
			this.panelOrganize = new System.Windows.Forms.Panel();
			this.panelSettings = new System.Windows.Forms.Panel();
			this.panel1 = new System.Windows.Forms.Panel();
			this.panelProgress = new System.Windows.Forms.Panel();
			this.pictureBox2 = new System.Windows.Forms.PictureBox();
			this.btnOrganize = new System.Windows.Forms.Button();
			this.folderSelectControl1 = new Trebuchet.Controls.FolderSelectControl();
			this.progressControl1 = new Trebuchet.Controls.ProgressControl();
			this.menuAppStrip1.SuspendLayout();
			this.panelActivate.SuspendLayout();
			this.panelOrganize.SuspendLayout();
			this.panelSettings.SuspendLayout();
			this.panel1.SuspendLayout();
			this.panelProgress.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
			this.SuspendLayout();
			// 
			// lblMode
			// 
			this.lblMode.AutoSize = true;
			this.lblMode.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblMode.Location = new System.Drawing.Point(10, 4);
			this.lblMode.Name = "lblMode";
			this.lblMode.Size = new System.Drawing.Size(122, 16);
			this.lblMode.TabIndex = 0;
			this.lblMode.Text = "Activate License";
			// 
			// btnContinue
			// 
			this.btnContinue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnContinue.Location = new System.Drawing.Point(21, 137);
			this.btnContinue.Name = "btnContinue";
			this.btnContinue.Size = new System.Drawing.Size(127, 23);
			this.btnContinue.TabIndex = 6;
			this.btnContinue.Text = "Edit Current dingSet...";
			this.btnContinue.UseVisualStyleBackColor = true;
			this.btnContinue.Click += new System.EventHandler(this.btnContinue_Click);
			// 
			// menuAppStrip1
			// 
			this.menuAppStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.settingsToolStripMenuItem,
            this.helpToolStripMenuItem});
			this.menuAppStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuAppStrip1.Name = "menuAppStrip1";
			this.menuAppStrip1.Size = new System.Drawing.Size(329, 24);
			this.menuAppStrip1.TabIndex = 7;
			this.menuAppStrip1.Text = "menuStrip1";
			// 
			// settingsToolStripMenuItem
			// 
			this.settingsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveToolStripMenuItem});
			this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
			this.settingsToolStripMenuItem.Size = new System.Drawing.Size(59, 20);
			this.settingsToolStripMenuItem.Text = "dingSet";
			// 
			// saveToolStripMenuItem
			// 
			this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
			this.saveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
			this.saveToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
			this.saveToolStripMenuItem.Text = "&Save";
			this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
			// 
			// helpToolStripMenuItem
			// 
			this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem,
            this.checkForUpdatesToolStripMenuItem});
			this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
			this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
			this.helpToolStripMenuItem.Text = "Help";
			// 
			// aboutToolStripMenuItem
			// 
			this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
			this.aboutToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
			this.aboutToolStripMenuItem.Text = "About...";
			this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
			// 
			// checkForUpdatesToolStripMenuItem
			// 
			this.checkForUpdatesToolStripMenuItem.Name = "checkForUpdatesToolStripMenuItem";
			this.checkForUpdatesToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
			this.checkForUpdatesToolStripMenuItem.Text = "Check for updates...";
			this.checkForUpdatesToolStripMenuItem.Click += new System.EventHandler(this.checkForUpdatesToolStripMenuItem_Click);
			// 
			// txtUsername
			// 
			this.txtUsername.Location = new System.Drawing.Point(84, 34);
			this.txtUsername.Name = "txtUsername";
			this.txtUsername.Size = new System.Drawing.Size(100, 20);
			this.txtUsername.TabIndex = 8;
			this.txtUsername.TextChanged += new System.EventHandler(this.txtUsername_TextChanged);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(23, 36);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(55, 13);
			this.label1.TabIndex = 9;
			this.label1.Text = "Username";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(23, 63);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(53, 13);
			this.label2.TabIndex = 10;
			this.label2.Text = "Password";
			// 
			// mtxtPassword
			// 
			this.mtxtPassword.Location = new System.Drawing.Point(84, 60);
			this.mtxtPassword.Name = "mtxtPassword";
			this.mtxtPassword.Size = new System.Drawing.Size(100, 20);
			this.mtxtPassword.TabIndex = 11;
			this.mtxtPassword.UseSystemPasswordChar = true;
			this.mtxtPassword.TextChanged += new System.EventHandler(this.OnMaskTextChanged);
			// 
			// btnActivate
			// 
			this.btnActivate.Enabled = false;
			this.btnActivate.Location = new System.Drawing.Point(210, 60);
			this.btnActivate.Name = "btnActivate";
			this.btnActivate.Size = new System.Drawing.Size(84, 23);
			this.btnActivate.TabIndex = 12;
			this.btnActivate.Text = "Activate License";
			this.btnActivate.UseVisualStyleBackColor = true;
			// 
			// lblTrialRemaining
			// 
			this.lblTrialRemaining.AutoSize = true;
			this.lblTrialRemaining.Location = new System.Drawing.Point(207, 17);
			this.lblTrialRemaining.Name = "lblTrialRemaining";
			this.lblTrialRemaining.Size = new System.Drawing.Size(194, 13);
			this.lblTrialRemaining.TabIndex = 13;
			this.lblTrialRemaining.Text = "There are x days remaining on your trial.";
			// 
			// lblLicense
			// 
			this.lblLicense.AutoSize = true;
			this.lblLicense.Location = new System.Drawing.Point(207, 40);
			this.lblLicense.Name = "lblLicense";
			this.lblLicense.Size = new System.Drawing.Size(205, 13);
			this.lblLicense.TabIndex = 14;
			this.lblLicense.TabStop = true;
			this.lblLicense.Text = "Click here to become a fully licensed user.";
			this.lblLicense.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lblLicense_LinkClicked);
			// 
			// panelActivate
			// 
			this.panelActivate.Controls.Add(this.lblMode);
			this.panelActivate.Controls.Add(this.lblLicense);
			this.panelActivate.Controls.Add(this.txtUsername);
			this.panelActivate.Controls.Add(this.lblTrialRemaining);
			this.panelActivate.Controls.Add(this.label1);
			this.panelActivate.Controls.Add(this.btnActivate);
			this.panelActivate.Controls.Add(this.label2);
			this.panelActivate.Controls.Add(this.mtxtPassword);
			this.panelActivate.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panelActivate.Location = new System.Drawing.Point(0, 307);
			this.panelActivate.Name = "panelActivate";
			this.panelActivate.Size = new System.Drawing.Size(489, 95);
			this.panelActivate.TabIndex = 16;
			// 
			// panelOrganize
			// 
			this.panelOrganize.Controls.Add(this.pictureBox2);
			this.panelOrganize.Controls.Add(this.btnOrganize);
			this.panelOrganize.Controls.Add(this.btnContinue);
			this.panelOrganize.Dock = System.Windows.Forms.DockStyle.Right;
			this.panelOrganize.Location = new System.Drawing.Point(329, 0);
			this.panelOrganize.Name = "panelOrganize";
			this.panelOrganize.Size = new System.Drawing.Size(160, 212);
			this.panelOrganize.TabIndex = 22;
			// 
			// panelSettings
			// 
			this.panelSettings.Controls.Add(this.panel1);
			this.panelSettings.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelSettings.Location = new System.Drawing.Point(0, 24);
			this.panelSettings.Margin = new System.Windows.Forms.Padding(10);
			this.panelSettings.Name = "panelSettings";
			this.panelSettings.Size = new System.Drawing.Size(329, 188);
			this.panelSettings.TabIndex = 23;
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.folderSelectControl1);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(329, 188);
			this.panel1.TabIndex = 0;
			// 
			// panelProgress
			// 
			this.panelProgress.Controls.Add(this.progressControl1);
			this.panelProgress.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panelProgress.Location = new System.Drawing.Point(0, 212);
			this.panelProgress.Name = "panelProgress";
			this.panelProgress.Size = new System.Drawing.Size(489, 95);
			this.panelProgress.TabIndex = 17;
			this.panelProgress.Visible = false;
			// 
			// pictureBox2
			// 
			this.pictureBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
			this.pictureBox2.Location = new System.Drawing.Point(21, 3);
			this.pictureBox2.Name = "pictureBox2";
			this.pictureBox2.Size = new System.Drawing.Size(128, 128);
			this.pictureBox2.TabIndex = 17;
			this.pictureBox2.TabStop = false;
			// 
			// btnOrganize
			// 
			this.btnOrganize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnOrganize.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.btnOrganize.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnOrganize.Image = ((System.Drawing.Image)(resources.GetObject("btnOrganize.Image")));
			this.btnOrganize.Location = new System.Drawing.Point(21, 166);
			this.btnOrganize.Name = "btnOrganize";
			this.btnOrganize.Size = new System.Drawing.Size(127, 76);
			this.btnOrganize.TabIndex = 21;
			this.btnOrganize.Text = "Organize";
			this.btnOrganize.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
			this.btnOrganize.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			this.btnOrganize.UseVisualStyleBackColor = true;
			this.btnOrganize.Click += new System.EventHandler(this.btnOrganize_Click);
			// 
			// folderSelectControl1
			// 
			this.folderSelectControl1.BackColor = System.Drawing.Color.White;
			this.folderSelectControl1.ControlEnabled = true;
			this.folderSelectControl1.Destination = "";
			this.folderSelectControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.folderSelectControl1.Location = new System.Drawing.Point(0, 0);
			this.folderSelectControl1.MinimumSize = new System.Drawing.Size(332, 287);
			this.folderSelectControl1.Name = "folderSelectControl1";
			this.folderSelectControl1.Padding = new System.Windows.Forms.Padding(5);
			this.folderSelectControl1.Size = new System.Drawing.Size(332, 287);
			this.folderSelectControl1.Source = "";
			this.folderSelectControl1.TabIndex = 0;
			// 
			// progressControl1
			// 
			this.progressControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.progressControl1.BackColor = System.Drawing.Color.White;
			this.progressControl1.Location = new System.Drawing.Point(99, 6);
			this.progressControl1.Name = "progressControl1";
			this.progressControl1.ProgressTitle = "Progress";
			this.progressControl1.Size = new System.Drawing.Size(309, 85);
			this.progressControl1.TabIndex = 1;
			// 
			// App
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.White;
			this.ClientSize = new System.Drawing.Size(489, 402);
			this.Controls.Add(this.panelSettings);
			this.Controls.Add(this.menuAppStrip1);
			this.Controls.Add(this.panelOrganize);
			this.Controls.Add(this.panelProgress);
			this.Controls.Add(this.panelActivate);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MinimumSize = new System.Drawing.Size(505, 438);
			this.Name = "App";
			this.Text = "photoding";
			this.menuAppStrip1.ResumeLayout(false);
			this.menuAppStrip1.PerformLayout();
			this.panelActivate.ResumeLayout(false);
			this.panelActivate.PerformLayout();
			this.panelOrganize.ResumeLayout(false);
			this.panelSettings.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			this.panelProgress.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblMode;
        private System.Windows.Forms.Button btnContinue;
        private System.Windows.Forms.MenuStrip menuAppStrip1;
		private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem checkForUpdatesToolStripMenuItem;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.MaskedTextBox mtxtPassword;
        private System.Windows.Forms.Button btnActivate;
        private System.Windows.Forms.Label lblTrialRemaining;
		private System.Windows.Forms.LinkLabel lblLicense;
		private System.Windows.Forms.Panel panelActivate;
		private System.Windows.Forms.PictureBox pictureBox2;
		private System.Windows.Forms.Button btnOrganize;
		private System.Windows.Forms.Panel panelOrganize;
		private System.Windows.Forms.Panel panelSettings;
		private System.Windows.Forms.Panel panel1;
		private Trebuchet.Controls.FolderSelectControl folderSelectControl1;
		private System.Windows.Forms.Panel panelProgress;
		private Trebuchet.Controls.ProgressControl progressControl1;
		private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
    }
}