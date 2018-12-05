using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Trebuchet.TrebuchetDialogs;

namespace Trebuchet.Controls
{
	public partial class RenameControl : TrebUserControl, IPikControl
    {
        private DateDialog date = new DateDialog();
        private const string DateTag = "{date}";
        private bool initializing = false;
        public event EventHandler enabling;
        public event EventHandler directionsChanging;
		private string oldName;

        public RenameControl()
        {
            InitializeComponent();
        }

        public string Directions
        {
            get
            {
                return "Directions:" + System.Environment.NewLine + 
                    "The file mask is the text that will appear in every file name.  You may insert a date anywhere in the name.  A mandatory numeric ID will be inserted either before the mask or after it.  Depending on the number of photos in the source directory, the numeric ID length may need to be altered.  Check rename originals to rename your original files to the newly specified name.  Finally, if you desire an individual rename directory that just contains the photos after they've been renamed, check the box to do so.";
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
                return App.CurAppSettings.RenameMode;
            }
            set
            {
                App.CurAppSettings.RenameMode = value;
                this.Enabled = value;
                App.CurAppSettings.FireRename();
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.date.SetFormat();

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
            this.txtMask.Text = App.CurAppSettings.RenameSettings.FileMask;
            this.numIDLength.Value = App.CurAppSettings.RenameSettings.NumericIDLength;
            this.chkFlipNumeric.Checked = App.CurAppSettings.RenameSettings.IDBeforeMask;
            this.chkRenameOriginals.Checked = App.CurAppSettings.RenameSettings.RenameOriginals;
            this.chkCreateRename.Checked = App.CurAppSettings.RenameSettings.CreateDir;
            if (this.txtMask.Text.Contains(RenameControl.DateTag))
				this.chkInsertDate.Checked = true;

			if (App.CurAppSettings.RenameMode != this.Enabled)
				GetWrapper().EnableControls(App.CurAppSettings.RenameMode);

			if (App.CurAppSettings.RenameSettings.UseAlbumName)
			{
				this.chkUseAlbum.Checked = true;
			}
			else
			{
				this.chkUseAlbum.Checked = false;
			}

            this.initializing = false;
        }

        private void btnDate_Click(object sender, EventArgs e)
        {
            this.date.ShowDialog();
        }

        private void chkInsertDate_CheckedChanged(object sender, EventArgs e)
        {
            if (!this.initializing)
            {
                string mask = this.txtMask.Text;
                if (this.chkInsertDate.Checked)
                {
                    if (!mask.Contains(RenameControl.DateTag))
                        mask += RenameControl.DateTag;
                }
                else
                {
                    if (mask.Contains(RenameControl.DateTag))
                        mask = mask.Remove(mask.IndexOf(RenameControl.DateTag), RenameControl.DateTag.Length);
                }
                this.txtMask.Text = mask;
                this.txtMask.Focus();
                this.txtMask.SelectionStart = this.txtMask.Text.Length;
                UpdateCurAppSettings();
            }
        }

		public override void UpdateCurAppSettings()
        {
            if (!initializing)
            {
                App.CurAppSettings.RenameSettings.FileMask = this.txtMask.Text;
                App.CurAppSettings.RenameSettings.NumericIDLength = (int)this.numIDLength.Value;
                App.CurAppSettings.RenameSettings.IDBeforeMask = this.chkFlipNumeric.Checked;
                App.CurAppSettings.RenameSettings.RenameOriginals = this.chkRenameOriginals.Checked;
                App.CurAppSettings.RenameSettings.CreateDir = this.chkCreateRename.Checked;
                App.CurAppSettings.FireRename();
            }
        }

        public bool CanContinue()
        {
            if (this.txtMask.Text.Length > 0)
                return true;
            else
                return false;
        }

        private void txtMask_TextChanged(object sender, EventArgs e)
        {
            UpdateCurAppSettings();
        }

        private void chkRenameOriginals_CheckedChanged(object sender, EventArgs e)
        {
            UpdateCurAppSettings();
        }

        private void chkFlipNumeric_CheckedChanged(object sender, EventArgs e)
        {
            UpdateCurAppSettings();
        }

        private void chkCreateRename_CheckedChanged(object sender, EventArgs e)
        {
            UpdateCurAppSettings();
        }

        private void numIDLength_ValueChanged(object sender, EventArgs e)
        {
            UpdateCurAppSettings();
        }

		private void chkUseAlbum_CheckedChanged(object sender, EventArgs e)
		{
			if (this.chkUseAlbum.Checked)
			{
				this.oldName = this.txtMask.Text;
				this.txtMask.Text = App.CurAppSettings.FolderSelectSettings.Album;
				this.txtMask.Enabled = false;
				UpdateCurAppSettings();
			}
			else
			{
				this.txtMask.Text = this.oldName;
				this.txtMask.Enabled = true;
				UpdateCurAppSettings();
			}
		}
    }
}
