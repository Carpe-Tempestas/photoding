namespace Trebuchet
{
    partial class Dialog
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
			Trebuchet.Settings.Compress compress1 = new Trebuchet.Settings.Compress();
			Trebuchet.Settings.FolderSelect folderSelect1 = new Trebuchet.Settings.FolderSelect();
			Trebuchet.Settings.ImageSettings imageSettings3 = new Trebuchet.Settings.ImageSettings();
			Trebuchet.Settings.Rename rename1 = new Trebuchet.Settings.Rename();
			Trebuchet.Settings.Resize resize1 = new Trebuchet.Settings.Resize();
			Trebuchet.Settings.UploadTypes.UploadInfo uploadInfo1 = new Trebuchet.Settings.UploadTypes.UploadInfo();
			Trebuchet.Settings.Watermark watermark1 = new Trebuchet.Settings.Watermark();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Dialog));
			this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
			this.fontDialog1 = new System.Windows.Forms.FontDialog();
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.watermarkControl = new Trebuchet.Controls.WatermarkControl();
			this.resizeControl = new Trebuchet.Controls.ResizeControl();
			this.renameControl = new Trebuchet.Controls.RenameControl();
			this.folderSelectControl = new Trebuchet.Controls.FolderSelectControl();
			this.compressControl = new Trebuchet.Controls.CompressControl();
			this.uploadControl = new Trebuchet.Controls.UploadControl();
			this.imageSettings2 = new Trebuchet.Controls.ImageSettings();
			this.watermarkControl1 = new Trebuchet.Controls.ControlWrapper(this.watermarkControl);
			this.resizeControl1 = new Trebuchet.Controls.ControlWrapper(this.resizeControl);
			this.renameControl1 = new Trebuchet.Controls.ControlWrapper(this.renameControl);
			this.folderSelectControl1 = new Trebuchet.Controls.ControlWrapper(this.folderSelectControl);
			this.imageSettings1 = new Trebuchet.Controls.ControlWrapper(this.imageSettings2);
			this.compressControl1 = new Trebuchet.Controls.ControlWrapper(this.compressControl);
			this.uploadControl1 = new Trebuchet.Controls.ControlWrapper(this.uploadControl);
			this.tabStrip1 = new Messir.Windows.Forms.TabStrip();
			this.tabStripFolderSelect = new Messir.Windows.Forms.TabStripButton();
			this.tabStripRename = new Messir.Windows.Forms.TabStripButton();
			this.tabStripResize = new Messir.Windows.Forms.TabStripButton();
			this.tabStripWatermark = new Messir.Windows.Forms.TabStripButton();
			this.tabStripImageSettings = new Messir.Windows.Forms.TabStripButton();
			this.tabStripCompress = new Messir.Windows.Forms.TabStripButton();
			this.tabStripUpload = new Messir.Windows.Forms.TabStripButton();
			this.progressFinish1 = new Trebuchet.Controls.ProgressFinish();
			this.panel1 = new System.Windows.Forms.Panel();
			this.panel2 = new System.Windows.Forms.Panel();
			this.pictureControl1 = new Trebuchet.Controls.PictureControl();
			this.scriptControl1 = new Trebuchet.Controls.ScriptControl();
			this.topSplitter = new System.Windows.Forms.Splitter();
			this.bottomSplitter = new System.Windows.Forms.Splitter();
			this.panel3 = new System.Windows.Forms.Panel();
			this.tabStrip1.SuspendLayout();
			this.panel1.SuspendLayout();
			this.panel2.SuspendLayout();
			this.panel3.SuspendLayout();
			this.SuspendLayout();
			// 
			// menuStrip1
			// 
			this.menuStrip1.BackColor = System.Drawing.Color.White;
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(988, 24);
			this.menuStrip1.TabIndex = 41;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// watermarkControl
			// 
			this.watermarkControl.BackColor = System.Drawing.Color.White;
			this.watermarkControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.watermarkControl.Enabled = false;
			this.watermarkControl.Location = new System.Drawing.Point(0, 0);
			this.watermarkControl.Name = "watermarkControl";
			this.watermarkControl.Size = new System.Drawing.Size(430, 710);
			this.watermarkControl.TabIndex = 48;
			this.watermarkControl.Viewer = this.progressFinish1.Viewer;
			// 
			// resizeControl
			// 
			this.resizeControl.BackColor = System.Drawing.Color.White;
			this.resizeControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.resizeControl.Enabled = false;
			this.resizeControl.Location = new System.Drawing.Point(0, 0);
			this.resizeControl.Name = "resizeControl";
			this.resizeControl.Size = new System.Drawing.Size(430, 742);
			this.resizeControl.TabIndex = 47;
			// 
			// renameControl
			// 
			this.renameControl.BackColor = System.Drawing.Color.White;
			this.renameControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.renameControl.Enabled = false;
			this.renameControl.Location = new System.Drawing.Point(0, 0);
			this.renameControl.Name = "renameControl";
			this.renameControl.Size = new System.Drawing.Size(430, 742);
			this.renameControl.TabIndex = 46;
			// 
			// folderSelectControl
			// 
			this.folderSelectControl.BackColor = System.Drawing.Color.White;
			this.folderSelectControl.ControlEnabled = true;
			this.folderSelectControl.Destination = "";
			this.folderSelectControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.folderSelectControl.Location = new System.Drawing.Point(0, 0);
			this.folderSelectControl.Name = "folderSelectControl";
			this.folderSelectControl.Size = new System.Drawing.Size(458, 890);
			this.folderSelectControl.Source = "";
			this.folderSelectControl.TabIndex = 45;
			// 
			// compressControl
			// 
			this.compressControl.BackColor = System.Drawing.Color.White;
			this.compressControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.compressControl.Enabled = false;
			this.compressControl.Location = new System.Drawing.Point(0, 0);
			this.compressControl.Name = "compressControl";
			this.compressControl.Size = new System.Drawing.Size(430, 742);
			this.compressControl.TabIndex = 46;
			// 
			// uploadControl
			// 
			this.uploadControl.BackColor = System.Drawing.Color.White;
			this.uploadControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.uploadControl.Location = new System.Drawing.Point(0, 0);
			this.uploadControl.Name = "uploadControl";
			this.uploadControl.Size = new System.Drawing.Size(430, 890);
			this.uploadControl.TabIndex = 46;
			// 
			// imageSettings2
			// 
			this.imageSettings2.BackColor = System.Drawing.Color.White;
			this.imageSettings2.ControlEnabled = true;
			this.imageSettings2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.imageSettings2.Location = new System.Drawing.Point(0, 0);
			this.imageSettings2.Name = "imageSettings2";
			this.imageSettings2.Size = new System.Drawing.Size(475, 424);
			this.imageSettings2.TabIndex = 49;
			// 
			// watermarkControl1
			// 
			this.watermarkControl1.AutoScroll = true;
			this.watermarkControl1.BackColor = System.Drawing.Color.White;
			this.watermarkControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.watermarkControl1.Location = new System.Drawing.Point(0, 0);
			this.watermarkControl1.MinPanelSize = new System.Drawing.Size(430, 330);
			this.watermarkControl1.Name = "watermarkControl1";
			this.watermarkControl1.Size = new System.Drawing.Size(475, 421);
			this.watermarkControl1.TabIndex = 48;
			this.watermarkControl1.UseButton = true;
			// 
			// resizeControl1
			// 
			this.resizeControl1.AutoScroll = true;
			this.resizeControl1.BackColor = System.Drawing.Color.White;
			this.resizeControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.resizeControl1.Location = new System.Drawing.Point(0, 0);
			this.resizeControl1.MinPanelSize = new System.Drawing.Size(430, 280);
			this.resizeControl1.Name = "resizeControl1";
			this.resizeControl1.Size = new System.Drawing.Size(475, 421);
			this.resizeControl1.TabIndex = 47;
			this.resizeControl1.UseButton = true;
			// 
			// renameControl1
			// 
			this.renameControl1.AutoScroll = true;
			this.renameControl1.BackColor = System.Drawing.Color.White;
			this.renameControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.renameControl1.Location = new System.Drawing.Point(0, 0);
			this.renameControl1.MinPanelSize = new System.Drawing.Size(430, 280);
			this.renameControl1.Name = "renameControl1";
			this.renameControl1.Size = new System.Drawing.Size(475, 421);
			this.renameControl1.TabIndex = 46;
			this.renameControl1.UseButton = true;
			// 
			// folderSelectControl1
			// 
			this.folderSelectControl1.AutoScroll = true;
			this.folderSelectControl1.BackColor = System.Drawing.Color.White;
			this.folderSelectControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.folderSelectControl1.Location = new System.Drawing.Point(0, 0);
			this.folderSelectControl1.MinPanelSize = new System.Drawing.Size(430, 280);
			this.folderSelectControl1.Name = "folderSelectControl1";
			this.folderSelectControl1.Size = new System.Drawing.Size(475, 421);
			this.folderSelectControl1.TabIndex = 45;
			this.folderSelectControl1.UseButton = false;
			// 
			// imageSettings1
			// 
			this.imageSettings1.AutoScroll = true;
			this.imageSettings1.BackColor = System.Drawing.Color.White;
			this.imageSettings1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.imageSettings1.Location = new System.Drawing.Point(0, 0);
			this.imageSettings1.MinPanelSize = new System.Drawing.Size(430, 400);
			this.imageSettings1.Name = "imageSettings1";
			this.imageSettings1.Size = new System.Drawing.Size(475, 421);
			this.imageSettings1.TabIndex = 49;
			this.imageSettings1.UseButton = false;
			// 
			// compressControl1
			// 
			this.compressControl1.AutoScroll = true;
			this.compressControl1.BackColor = System.Drawing.Color.White;
			this.compressControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.compressControl1.Location = new System.Drawing.Point(0, 0);
			this.compressControl1.MinPanelSize = new System.Drawing.Size(430, 280);
			this.compressControl1.Name = "compressControl1";
			this.compressControl1.Size = new System.Drawing.Size(475, 421);
			this.compressControl1.TabIndex = 46;
			this.compressControl1.UseButton = true;
			// 
			// uploadControl1
			// 
			this.uploadControl1.AutoScroll = true;
			this.uploadControl1.BackColor = System.Drawing.Color.White;
			this.uploadControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.uploadControl1.Location = new System.Drawing.Point(0, 0);
			this.uploadControl1.MinPanelSize = new System.Drawing.Size(430, 400);
			this.uploadControl1.Name = "uploadControl1";
			this.uploadControl1.Size = new System.Drawing.Size(475, 421);
			this.uploadControl1.TabIndex = 46;
			this.uploadControl1.UseButton = true;
			// 
			// tabStrip1
			// 
			this.tabStrip1.BackColor = System.Drawing.Color.White;
			this.tabStrip1.FlipButtons = false;
			this.tabStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tabStripFolderSelect,
            this.tabStripRename,
            this.tabStripResize,
            this.tabStripWatermark,
            this.tabStripImageSettings,
            this.tabStripCompress,
            this.tabStripUpload});
			this.tabStrip1.Location = new System.Drawing.Point(0, 0);
			this.tabStrip1.Name = "tabStrip1";
			this.tabStrip1.RenderStyle = System.Windows.Forms.ToolStripRenderMode.Custom;
			this.tabStrip1.SelectedTab = this.tabStripUpload;
			this.tabStrip1.Size = new System.Drawing.Size(475, 35);
			this.tabStrip1.TabIndex = 43;
			this.tabStrip1.Text = "tabStrip1";
			this.tabStrip1.UseVisualStyles = true;
			this.tabStrip1.SelectedTabChanged += new System.EventHandler<Messir.Windows.Forms.SelectedTabChangedEventArgs>(this.OnSelectedTabChanged);
			// 
			// tabStripFolderSelect
			// 
			this.tabStripFolderSelect.HotTextColor = System.Drawing.SystemColors.ControlText;
			this.tabStripFolderSelect.Image = ((System.Drawing.Image)(resources.GetObject("tabStripFolderSelect.Image")));
			this.tabStripFolderSelect.ImageTransparentColor = System.Drawing.Color.White;
			this.tabStripFolderSelect.IsSelected = false;
			this.tabStripFolderSelect.Margin = new System.Windows.Forms.Padding(0);
			this.tabStripFolderSelect.Name = "tabStripFolderSelect";
			this.tabStripFolderSelect.Padding = new System.Windows.Forms.Padding(0);
			this.tabStripFolderSelect.SelectedFont = new System.Drawing.Font("Tahoma", 8.25F);
			this.tabStripFolderSelect.SelectedTextColor = System.Drawing.SystemColors.ControlText;
			this.tabStripFolderSelect.Size = new System.Drawing.Size(95, 35);
			this.tabStripFolderSelect.Text = "Start Here";
			this.tabStripFolderSelect.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			this.tabStripFolderSelect.ToolTipText = "The source and destination directories can be specified here.";
			// 
			// tabStripRename
			// 
			this.tabStripRename.HotTextColor = System.Drawing.SystemColors.ControlText;
			this.tabStripRename.Image = ((System.Drawing.Image)(resources.GetObject("tabStripRename.Image")));
			this.tabStripRename.ImageTransparentColor = System.Drawing.Color.White;
			this.tabStripRename.IsSelected = false;
			this.tabStripRename.Margin = new System.Windows.Forms.Padding(0);
			this.tabStripRename.Name = "tabStripRename";
			this.tabStripRename.Padding = new System.Windows.Forms.Padding(0);
			this.tabStripRename.SelectedFont = new System.Drawing.Font("Tahoma", 8.25F);
			this.tabStripRename.SelectedTextColor = System.Drawing.SystemColors.ControlText;
			this.tabStripRename.Size = new System.Drawing.Size(54, 35);
			this.tabStripRename.Text = "Rename";
			this.tabStripRename.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			this.tabStripRename.ToolTipText = "New naming conventions for photos can be specified here.";
			// 
			// tabStripResize
			// 
			this.tabStripResize.HotTextColor = System.Drawing.SystemColors.ControlText;
			this.tabStripResize.Image = ((System.Drawing.Image)(resources.GetObject("tabStripResize.Image")));
			this.tabStripResize.ImageTransparentColor = System.Drawing.Color.White;
			this.tabStripResize.IsSelected = false;
			this.tabStripResize.Margin = new System.Windows.Forms.Padding(0);
			this.tabStripResize.Name = "tabStripResize";
			this.tabStripResize.Padding = new System.Windows.Forms.Padding(0);
			this.tabStripResize.SelectedFont = new System.Drawing.Font("Tahoma", 8.25F);
			this.tabStripResize.SelectedTextColor = System.Drawing.SystemColors.ControlText;
			this.tabStripResize.Size = new System.Drawing.Size(43, 35);
			this.tabStripResize.Text = "Resize";
			this.tabStripResize.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			this.tabStripResize.ToolTipText = "Photos can be resized here.";
			// 
			// tabStripWatermark
			// 
			this.tabStripWatermark.HotTextColor = System.Drawing.SystemColors.ControlText;
			this.tabStripWatermark.Image = ((System.Drawing.Image)(resources.GetObject("tabStripWatermark.Image")));
			this.tabStripWatermark.ImageTransparentColor = System.Drawing.Color.White;
			this.tabStripWatermark.IsSelected = false;
			this.tabStripWatermark.Margin = new System.Windows.Forms.Padding(0);
			this.tabStripWatermark.Name = "tabStripWatermark";
			this.tabStripWatermark.Padding = new System.Windows.Forms.Padding(0);
			this.tabStripWatermark.SelectedFont = new System.Drawing.Font("Tahoma", 8.25F);
			this.tabStripWatermark.SelectedTextColor = System.Drawing.SystemColors.ControlText;
			this.tabStripWatermark.Size = new System.Drawing.Size(69, 35);
			this.tabStripWatermark.Text = "Watermark";
			this.tabStripWatermark.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			this.tabStripWatermark.ToolTipText = "Apply a watermark to protect photos here.";
			// 
			// tabStripImageSettings
			// 
			this.tabStripImageSettings.HotTextColor = System.Drawing.SystemColors.ControlText;
			this.tabStripImageSettings.Image = ((System.Drawing.Image)(resources.GetObject("tabStripImageSettings.Image")));
			this.tabStripImageSettings.ImageTransparentColor = System.Drawing.Color.White;
			this.tabStripImageSettings.IsSelected = false;
			this.tabStripImageSettings.Margin = new System.Windows.Forms.Padding(0);
			this.tabStripImageSettings.Name = "tabStripImageSettings";
			this.tabStripImageSettings.Padding = new System.Windows.Forms.Padding(0);
			this.tabStripImageSettings.SelectedFont = new System.Drawing.Font("Segoe UI", 9F);
			this.tabStripImageSettings.SelectedTextColor = System.Drawing.SystemColors.ControlText;
			this.tabStripImageSettings.Size = new System.Drawing.Size(89, 35);
			this.tabStripImageSettings.Text = "Image Settings";
			this.tabStripImageSettings.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			this.tabStripImageSettings.ToolTipText = "Various image settings can be adjusted here.";
			// 
			// tabStripCompress
			// 
			this.tabStripCompress.HotTextColor = System.Drawing.SystemColors.ControlText;
			this.tabStripCompress.Image = ((System.Drawing.Image)(resources.GetObject("tabStripCompress.Image")));
			this.tabStripCompress.ImageTransparentColor = System.Drawing.Color.White;
			this.tabStripCompress.IsSelected = false;
			this.tabStripCompress.Margin = new System.Windows.Forms.Padding(0);
			this.tabStripCompress.Name = "tabStripCompress";
			this.tabStripCompress.Padding = new System.Windows.Forms.Padding(0);
			this.tabStripCompress.SelectedFont = new System.Drawing.Font("Tahoma", 8.25F);
			this.tabStripCompress.SelectedTextColor = System.Drawing.SystemColors.ControlText;
			this.tabStripCompress.Size = new System.Drawing.Size(64, 35);
			this.tabStripCompress.Text = "Compress";
			this.tabStripCompress.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			this.tabStripCompress.ToolTipText = "Compress photos to save space here.";
			// 
			// tabStripUpload
			// 
			this.tabStripUpload.Checked = true;
			this.tabStripUpload.HotTextColor = System.Drawing.SystemColors.ControlText;
			this.tabStripUpload.Image = ((System.Drawing.Image)(resources.GetObject("tabStripUpload.Image")));
			this.tabStripUpload.ImageTransparentColor = System.Drawing.Color.White;
			this.tabStripUpload.IsSelected = true;
			this.tabStripUpload.Margin = new System.Windows.Forms.Padding(0);
			this.tabStripUpload.Name = "tabStripUpload";
			this.tabStripUpload.Padding = new System.Windows.Forms.Padding(0);
			this.tabStripUpload.SelectedFont = new System.Drawing.Font("Tahoma", 8.25F);
			this.tabStripUpload.SelectedTextColor = System.Drawing.SystemColors.ControlText;
			this.tabStripUpload.Size = new System.Drawing.Size(49, 35);
			this.tabStripUpload.Text = "Upload";
			this.tabStripUpload.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			this.tabStripUpload.ToolTipText = "Use this to upload photos.";
			// 
			// progressFinish1
			// 
			this.progressFinish1.BackColor = System.Drawing.Color.White;
			this.progressFinish1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.progressFinish1.Location = new System.Drawing.Point(0, 0);
			this.progressFinish1.Name = "progressFinish1";
			this.progressFinish1.Size = new System.Drawing.Size(988, 150);
			this.progressFinish1.TabIndex = 42;
			// 
			// panel1
			// 
			this.panel1.BackColor = System.Drawing.Color.White;
			this.panel1.Controls.Add(this.tabStrip1);
			this.panel1.Controls.Add(this.resizeControl1);
			this.panel1.Controls.Add(this.uploadControl1);
			this.panel1.Controls.Add(this.renameControl1);
			this.panel1.Controls.Add(this.compressControl1);
			this.panel1.Controls.Add(this.folderSelectControl1);
			this.panel1.Controls.Add(this.watermarkControl1);
			this.panel1.Controls.Add(this.imageSettings1);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
			this.panel1.Location = new System.Drawing.Point(0, 24);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(475, 421);
			this.panel1.TabIndex = 49;
			// 
			// panel2
			// 
			this.panel2.BackColor = System.Drawing.Color.White;
			this.panel2.Controls.Add(this.pictureControl1);
			this.panel2.Controls.Add(this.scriptControl1);
			this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel2.Location = new System.Drawing.Point(475, 24);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(513, 421);
			this.panel2.TabIndex = 50;
			// 
			// pictureControl1
			// 
			this.pictureControl1.BackColor = System.Drawing.Color.White;
			this.pictureControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pictureControl1.Location = new System.Drawing.Point(0, 0);
			this.pictureControl1.Name = "pictureControl1";
			this.pictureControl1.Size = new System.Drawing.Size(513, 421);
			this.pictureControl1.TabIndex = 53;
			// 
			// scriptControl1
			// 
			this.scriptControl1.BackColor = System.Drawing.Color.White;
			this.scriptControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.scriptControl1.Location = new System.Drawing.Point(0, 0);
			this.scriptControl1.Name = "scriptControl1";
			this.scriptControl1.Size = new System.Drawing.Size(513, 421);
			this.scriptControl1.TabIndex = 45;
			// 
			// topSplitter
			// 
			this.topSplitter.BackColor = System.Drawing.Color.SteelBlue;
			this.topSplitter.Location = new System.Drawing.Point(475, 24);
			this.topSplitter.MinSize = 475;
			this.topSplitter.Name = "topSplitter";
			this.topSplitter.Size = new System.Drawing.Size(5, 421);
			this.topSplitter.TabIndex = 51;
			this.topSplitter.TabStop = false;
			this.topSplitter.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.topSplitter_SplitterMoved);
			// 
			// bottomSplitter
			// 
			this.bottomSplitter.BackColor = System.Drawing.Color.SteelBlue;
			this.bottomSplitter.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.bottomSplitter.Location = new System.Drawing.Point(0, 445);
			this.bottomSplitter.Name = "bottomSplitter";
			this.bottomSplitter.Size = new System.Drawing.Size(988, 5);
			this.bottomSplitter.TabIndex = 44;
			this.bottomSplitter.TabStop = false;
			this.bottomSplitter.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.OnBottomSplitterMoved);
			// 
			// panel3
			// 
			this.panel3.Controls.Add(this.progressFinish1);
			this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel3.Location = new System.Drawing.Point(0, 450);
			this.panel3.MinimumSize = new System.Drawing.Size(100, 150);
			this.panel3.Name = "panel3";
			this.panel3.Size = new System.Drawing.Size(988, 150);
			this.panel3.TabIndex = 52;
			// 
			// Dialog
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(988, 600);
			this.Controls.Add(this.topSplitter);
			this.Controls.Add(this.panel2);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.bottomSplitter);
			this.Controls.Add(this.panel3);
			this.Controls.Add(this.menuStrip1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MinimumSize = new System.Drawing.Size(475, 600);
			this.Name = "Dialog";
			this.Text = "photoding";
			this.tabStrip1.ResumeLayout(false);
			this.tabStrip1.PerformLayout();
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.panel2.ResumeLayout(false);
			this.panel3.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.FontDialog fontDialog1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private Trebuchet.Controls.ProgressFinish progressFinish1;
        private Messir.Windows.Forms.TabStrip tabStrip1;
        private Messir.Windows.Forms.TabStripButton tabStripFolderSelect;
        private Messir.Windows.Forms.TabStripButton tabStripRename;
        private Messir.Windows.Forms.TabStripButton tabStripResize;
        private Messir.Windows.Forms.TabStripButton tabStripCompress;
        private Trebuchet.Controls.FolderSelectControl folderSelectControl;
        private Trebuchet.Controls.RenameControl renameControl;
        private Trebuchet.Controls.ResizeControl resizeControl;
        private Trebuchet.Controls.WatermarkControl watermarkControl;
        private Trebuchet.Controls.CompressControl compressControl;
        private Trebuchet.Controls.UploadControl uploadControl;
        private Trebuchet.Controls.ControlWrapper folderSelectControl1;
        private Trebuchet.Controls.ControlWrapper renameControl1;
        private Trebuchet.Controls.ControlWrapper resizeControl1;
		private Trebuchet.Controls.ControlWrapper watermarkControl1;
		private Trebuchet.Controls.ControlWrapper imageSettings1;
        private Trebuchet.Controls.ControlWrapper compressControl1;
        private Trebuchet.Controls.ControlWrapper uploadControl1;
        private Messir.Windows.Forms.TabStripButton tabStripUpload;
        private Messir.Windows.Forms.TabStripButton tabStripWatermark;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private Trebuchet.Controls.ScriptControl scriptControl1;
        private System.Windows.Forms.Splitter topSplitter;
        private System.Windows.Forms.Splitter bottomSplitter;
        private System.Windows.Forms.Panel panel3;
        private Trebuchet.Controls.PictureControl pictureControl1;
		private Messir.Windows.Forms.TabStripButton tabStripImageSettings;
		private Trebuchet.Controls.ImageSettings imageSettings2;
    }
}

