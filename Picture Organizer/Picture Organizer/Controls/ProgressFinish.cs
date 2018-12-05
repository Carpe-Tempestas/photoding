using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Media;
using System.Reflection;
using System.IO;

namespace Trebuchet.Controls
{
    public partial class ProgressFinish : UserControl
    {
        private ToolTip tooltip = new ToolTip();
        public event EventHandler ImagesLoaded;
		private bool initializing = false;
		private string dirLoaded = String.Empty;

        public ProgressFinish()
        {
            InitializeComponent();
        }

		public ThumbnailViewer Viewer
		{
			get { return this.thumbnailViewer1; }
		}

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.tooltip.SetToolTip(this.btnOrganize, "Click here to organize your photos.");
			this.initializing = true;
			this.chkAlwaysLoadThumbs.Checked = App.IntSettings.AutoLoadPhotos;
			this.initializing = false;
			App.CurAppSettingChanging += new EventHandler(App_CurAppSettingChanging);
			App.CurAppSettingChanged += new EventHandler(App_CurAppSettingChanged);
			App.CurAppSettings.FolderSelectChanged += new EventHandler(CurAppSettings_FolderSelectChanged);
			App.CurAppSettings.FolderSelectSettings.SourceChanged += new EventHandler(FolderSelectSettings_SourceChanged);

			if (this.chkAlwaysLoadThumbs.Checked)
				btnLoadThumbnails_Click(this, new EventArgs());
        }

		void App_CurAppSettingChanged(object sender, EventArgs e)
		{
			App.CurAppSettings.FolderSelectChanged += new EventHandler(CurAppSettings_FolderSelectChanged);
			App.CurAppSettings.FolderSelectSettings.SourceChanged += new EventHandler(FolderSelectSettings_SourceChanged);
			CurAppSettings_FolderSelectChanged(sender, e);
		}

		void App_CurAppSettingChanging(object sender, EventArgs e)
		{
			App.CurAppSettings.FolderSelectChanged -= new EventHandler(CurAppSettings_FolderSelectChanged);
			App.CurAppSettings.FolderSelectSettings.SourceChanged -= new EventHandler(FolderSelectSettings_SourceChanged);
		}

		void FolderSelectSettings_SourceChanged(object sender, EventArgs e)
		{
			if (this.chkAlwaysLoadThumbs.Checked && this.dirLoaded != App.CurAppSettings.FolderSelectSettings.SourceFolder)
				btnLoadThumbnails_Click(this, new EventArgs());
			else if (this.dirLoaded != App.CurAppSettings.FolderSelectSettings.SourceFolder)
				this.thumbnailViewer1.UnloadImages();
		}

		void CurAppSettings_FolderSelectChanged(object sender, EventArgs e)
		{
			if (this.chkAlwaysLoadThumbs.Checked && this.dirLoaded != App.CurAppSettings.FolderSelectSettings.SourceFolder)
				btnLoadThumbnails_Click(this, new EventArgs());
			else if (this.dirLoaded != App.CurAppSettings.FolderSelectSettings.SourceFolder)
				this.thumbnailViewer1.UnloadImages();
		}

        private void btnOrganize_Click(object sender, EventArgs e)
        {
			if (this.btnOrganize.Text == "Organize")
			{
				App.TheCore.CalculateDryRun();
				this.progressControl1.Bar.Minimum = 0;
				this.progressControl1.Bar.Maximum = App.TheCore.FileTotal;
				App.TheCore.ProgressMade += new EventHandler(TheCore_ProgressMade);
				App.TheCore.Finished += new EventHandler(TheCore_Finished);
				this.progressControl1.Visible = true;
				this.thumbnailViewer1.Visible = false;
				this.progressControl1.BringToFront();
				this.btnOrganize.Text = "Cancel";
				this.tooltip.SetToolTip(this.btnOrganize, "Click here to cancel organization.");
				App.TheCore.Organize();
			}
			else if (this.btnOrganize.Text == "Cancel" && this.btnOrganize.Enabled == true)
			{
				this.btnOrganize.Enabled = false;
				App.TheCore.Cancelling += new EventHandler(TheCore_Cancelling);
				App.TheCore.CancelOrganizing();
			}
        }

		void TheCore_Cancelling(object sender, EventArgs e)
		{
			this.btnOrganize.Text = "Cancelling";
			this.tooltip.SetToolTip(this.btnOrganize, "The organization is being cancelled, please wait until this is finished.");
		}

		void TheCore_Finished(object sender, EventArgs e)
		{
			this.btnOrganize.Text = "Organize";
			this.tooltip.SetToolTip(this.btnOrganize, "Click here to organize your photos.");
			this.btnOrganize.Enabled = true;
			this.progressControl1.Visible = false;
			this.progressControl1.SendToBack();
			this.thumbnailViewer1.Visible = true;
			string path = Application.ExecutablePath;
			path = Path.GetDirectoryName(path);
			path = Path.Combine(path, "ding.wav");
			if(File.Exists(path))
			{
				SoundPlayer player = new SoundPlayer(path);
				player.Play();
			}
		}

		public void TheCore_ProgressMade(object sender, EventArgs e)
		{
			if (this.InvokeRequired)
				this.BeginInvoke((MethodInvoker)delegate{this.TheCore_ProgressMade(this, e);});
			else
			{
				if (this.progressControl1.ProgressTitle != App.TheCore.ProgressTitle)
				{
					this.progressControl1.Bar.Value = 0;
					this.progressControl1.ProgressTitle = App.TheCore.ProgressTitle;
				}

				if (this.progressControl1.Details != App.TheCore.ProgressDetails)
				{
					this.progressControl1.Details = App.TheCore.ProgressDetails;
					try
					{
						this.progressControl1.Bar.Value = App.TheCore.CurrentFile;
					}
					catch (ArgumentOutOfRangeException ex)
					{
						this.progressControl1.Bar.Value = this.progressControl1.Bar.Maximum;
					}
				}
			}
		}

        private void OnSizeChanged(object sender, EventArgs e)
        {
            Point organizeBtnPos = this.btnOrganize.Location;
            organizeBtnPos.X = this.Width - this.btnOrganize.Width - 3;
            this.btnOrganize.Location = organizeBtnPos;
        }

        private void btnLoadThumbnails_Click(object sender, EventArgs e)
        {
			this.btnLoadThumbnails.Enabled = false;
            this.thumbnailViewer1.Visible = true;
            this.thumbnailViewer1.BringToFront();
            this.btnOrganize.BringToFront();
            this.progressControl1.Hide();
            this.progressControl1.BringToFront();
            this.thumbnailViewer1.ImagesLoaded += new EventHandler(thumbnailViewer1_ImagesLoaded);
            this.thumbnailViewer1.LoadImages(this.progressControl1);
        }

        void thumbnailViewer1_ImagesLoaded(object sender, EventArgs e)
        {
            if (this.ImagesLoaded != null)
                this.ImagesLoaded(this.thumbnailViewer1, new EventArgs());
			this.dirLoaded = App.CurAppSettings.FolderSelectSettings.SourceFolder;
			this.btnLoadThumbnails.Enabled = true;
        }

		private void chkAlwaysLoadThumbs_CheckedChanged(object sender, EventArgs e)
		{
			if (!this.initializing)
			{
				App.IntSettings.AutoLoadPhotos = this.chkAlwaysLoadThumbs.Checked;
			}
		}
    }
}
