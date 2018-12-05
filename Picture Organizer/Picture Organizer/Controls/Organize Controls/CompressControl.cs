using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Trebuchet.Controls
{
	public partial class CompressControl : TrebUserControl, IPikControl
    {
        private bool initializing = false;
        public event EventHandler enabling;
        public event EventHandler directionsChanging;
		private string oldPath = String.Empty;
		private bool wasDefault = false;

        public CompressControl()
        {
            InitializeComponent();
        }

        public string Directions
        {
            get
            {
                return "Directions:" + System.Environment.NewLine +
                    "This will do some file compression.";
            }
        }

        public bool UseEnabled
        {
            get
            {
                return true;
            }
        }

        public bool ControlEnabled
        {
            get
            {
                return App.CurAppSettings.CompressMode;
            }
            set
            {
                App.CurAppSettings.CompressMode = value;
				this.Enabled = true;
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!this.DesignMode)
            {
                Initialize();
				this.Enabled = this.ControlEnabled;
				App.CurAppSettingChanged += new EventHandler(App_CurAppSettingChanged);
            }
		}

		void App_CurAppSettingChanged(object sender, EventArgs e)
		{
			Initialize();
		}

		public override void Initialize()
        {
            this.initializing = true;
            this.chkUseFolderName.Checked = App.CurAppSettings.CompressSettings.UseDefault;
            this.txtFile.Text = App.CurAppSettings.CompressSettings.ZipFile;
            this.mtxtPassword.Text = App.CurAppSettings.CompressSettings.Password;
            this.numZipLevel.Value = App.CurAppSettings.CompressSettings.Level;
			App.CurAppSettings.FolderSelectChanged += new EventHandler(CurAppSettings_FolderSelectChanged);

			if (App.CurAppSettings.CompressMode != this.Enabled)
				GetWrapper().EnableControls(App.CurAppSettings.CompressMode);
            this.initializing = false;
        }

		void CurAppSettings_FolderSelectChanged(object sender, EventArgs e)
		{
			if (this.chkUseFolderName.Checked)
			{
				this.txtFile.Text = GetDefaultPath();
			}
		}

		public static string GetDefaultPath()
		{
			string path = String.Empty;
			if (App.CurAppSettings.FolderSelectSettings.UseDefaultDestination)
				path = App.CurAppSettings.FolderSelectSettings.SourceFolder;
			else
				path = App.CurAppSettings.FolderSelectSettings.DestinationFolder;

			if (App.CurAppSettings.FolderSelectSettings.AppendAlbum)
				path = Path.Combine(path, App.CurAppSettings.FolderSelectSettings.Album + ".zip");
			return path;
		}

        private void btnSelectFile_Click(object sender, EventArgs e)
        {
            SaveFileDialog dlgSave = new SaveFileDialog();
            dlgSave.InitialDirectory = this.txtFile.Text;
            dlgSave.Filter = "Compressed files|*.zip;";
            if (dlgSave.ShowDialog() == DialogResult.OK)
            {
                this.txtFile.Text = dlgSave.FileName;
                UpdateCurAppSettings();
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            this.mtxtPassword.Text = String.Empty;
        }

        private void txtFile_TextChanged(object sender, EventArgs e)
        {
			if (!this.chkUseFolderName.Checked)
			{
				this.oldPath = this.txtFile.Text;
			}
            UpdateCurAppSettings();
        }

		public override void UpdateCurAppSettings()
        {
            App.CurAppSettings.CompressSettings.ZipFile = this.txtFile.Text;
            App.CurAppSettings.CompressSettings.Password = this.mtxtPassword.Text;
            App.CurAppSettings.CompressSettings.UseDefault = this.chkUseFolderName.Checked;
            App.CurAppSettings.CompressSettings.Level = (int)this.numZipLevel.Value;
        }

        private void OnPasswordChanged(object sender, EventArgs e)
        {
            UpdateCurAppSettings();
        }

        private void chkUseFolderName_CheckedChanged(object sender, EventArgs e)
        {
            this.txtFile.Enabled = !this.chkUseFolderName.Checked;
            this.btnSelectFile.Enabled = !this.chkUseFolderName.Checked;
			if (this.chkUseFolderName.Checked)
			{
				this.txtFile.Text = GetDefaultPath();
				this.wasDefault = true;
			}
			else
			{
				if (this.wasDefault)
					this.txtFile.Text = this.oldPath;
				else
					this.oldPath = this.txtFile.Text;
				this.wasDefault = false;
			}
            UpdateCurAppSettings();
        }

        private void numZipLevel_ValueChanged(object sender, EventArgs e)
        {
            UpdateCurAppSettings();
        }

    }
}
