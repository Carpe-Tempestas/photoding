using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Trebuchet.Controls.Uploading;
using Trebuchet.Settings.UploadTypes;

namespace Trebuchet.Controls.Uploading
{
	public partial class EmailControl : UserControl, IUpControl
	{
		private bool initializing = false;
		private string customAlbum = String.Empty;

		public EmailControl()
		{
			InitializeComponent();
		}

		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			Initialize();
			App.CurAppSettings.FolderSelectChanged += new EventHandler(CurAppSettings_FolderSelectChanged);
		}

		void CurAppSettings_FolderSelectChanged(object sender, EventArgs e)
		{
			if (this.chkUseAlbum.Checked)
			{
				this.txtSubject.Text = App.CurAppSettings.FolderSelectSettings.Album;
			}
		}

		private void btnTo_Click(object sender, EventArgs e)
		{
			UpdateCurAppSettings();
		}

		private void txtTo_TextChanged(object sender, EventArgs e)
		{
			UpdateCurAppSettings();
		}

		private void comboFrom_SelectedIndexChanged(object sender, EventArgs e)
		{
			UpdateCurAppSettings();
		}

		private void checkPromptSend_CheckedChanged(object sender, EventArgs e)
		{
			UpdateCurAppSettings();
		}

		private void txtSubject_TextChanged(object sender, EventArgs e)
		{
			UpdateCurAppSettings();
		}

		private void comboAttachMode_SelectedIndexChanged(object sender, EventArgs e)
		{
			UpdateCurAppSettings();
		}

		private void txtText_TextChanged(object sender, EventArgs e)
		{
			UpdateCurAppSettings();
		}

		private void checkUseAlbum_CheckedChanged(object sender, EventArgs e)
		{
			UpdateCurAppSettings();
		}

		#region IUpControl Members

		public void Initialize()
		{
			this.initializing = true;
			Email emailSettings = App.Uploaders.GetUpload(App.CurAppSettings.UploadDetails.Name) as Email;
			if (emailSettings == null)
				emailSettings = new Email();

			if (emailSettings.AttachMode == Email.AttachModes.Compressed)
				this.comboAttachMode.SelectedIndex = 1;
			else
				this.comboAttachMode.SelectedIndex = 0;
			App.Uploaders.UpdateUpload(emailSettings);

			this.chkUseAlbum.Checked = App.CurAppSettings.UploadDetails.UseDefaultAlbum;
			if (this.chkUseAlbum.Checked)
				this.txtSubject.Text = App.CurAppSettings.FolderSelectSettings.Album;
			else
				this.txtSubject.Text = App.CurAppSettings.UploadDetails.Album;
			this.txtBody.Text = App.CurAppSettings.UploadDetails.Description;
			this.customAlbum = App.CurAppSettings.UploadDetails.Album;
			this.initializing = false;
		}

		public void UpdateCurAppSettings()
		{
			if (this.initializing)
				return;

			Email emailSettings = App.Uploaders.GetUpload(App.CurAppSettings.UploadDetails.Name) as Email;
			if (emailSettings == null)
				emailSettings = new Trebuchet.Settings.UploadTypes.Email();

			if (this.comboAttachMode.SelectedIndex == 1)
				emailSettings.AttachMode = Email.AttachModes.Compressed;
			else
				emailSettings.AttachMode = Email.AttachModes.All;

			App.CurAppSettings.UploadDetails.Description = this.txtBody.Text;
			App.CurAppSettings.UploadDetails.UseDefaultAlbum = this.chkUseAlbum.Checked;
			if (!this.chkUseAlbum.Checked)
				this.customAlbum = this.txtSubject.Text;
			App.CurAppSettings.UploadDetails.Album = this.customAlbum;
			App.Uploaders.UpdateUpload(emailSettings);
		}

		#endregion
	}
}
