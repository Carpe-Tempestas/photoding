using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Google.GData.Photos;
using Trebuchet.Settings.UploadTypes;
using Google.GData.Client;

namespace Trebuchet.Controls.Uploading
{
	public partial class PicasaControl : UserControl, IUpControl
	{
		private List<PicasaEntry> albums = new List<PicasaEntry>();
		private string oldAlbum;
		private string customAlbum;
		private string customDescription;
		private ToolTip tooltip = new ToolTip();
		private bool initializing = false;

		public PicasaControl()
		{
			InitializeComponent();
			Initialize();
		}

		public void Initialize()
		{
			this.initializing = true;

			Picasa picasaSettings = App.Uploaders.GetUpload(App.CurAppSettings.UploadDetails.Name) as Picasa;
			if (picasaSettings == null)
				picasaSettings = new Picasa();

			if (picasaSettings.Privacy == Picasa.PicasaPrivacyType.Private)
				this.rbPrivate.Checked = true;
			else if (picasaSettings.Privacy == Picasa.PicasaPrivacyType.Public)
				this.rbPublic.Checked = true;

			this.txtUsername.Text = picasaSettings.Username;
			this.chkRememberPassword.Checked = picasaSettings.RememberPass;
			if (picasaSettings.RememberPass)
				this.txtPassword.Text = picasaSettings.Request(picasaSettings.Password);
			else
				this.txtPassword.Text = String.Empty;

			this.chkUseDefaultAlbum.Checked = App.CurAppSettings.UploadDetails.UseDefaultAlbum;
			if (this.chkUseDefaultAlbum.Checked)
				this.txtAlbum.Text = App.CurAppSettings.FolderSelectSettings.Album;
			else
				this.txtAlbum.Text = App.CurAppSettings.UploadDetails.Album;
			this.customAlbum = App.CurAppSettings.UploadDetails.Album;
			this.txtDescription.Text = App.CurAppSettings.UploadDetails.Description;
			this.txtLocation.Text = App.CurAppSettings.UploadDetails.Location;
			this.initializing = false;
		}

		private void btnGetAlbums_Click(object sender, EventArgs e)
		{
			if (this.txtUsername.Text == String.Empty || this.txtPassword.Text == String.Empty)
			{
				MessageBox.Show("You must fill in your username and/or password in order to connect");
				return;
			}

			AlbumQuery query = new AlbumQuery();
			query.Uri = new Uri(AlbumQuery.CreatePicasaUri(this.txtUsername.Text));
			PicasaService service = new PicasaService("photoding");
			((GDataRequestFactory)service.RequestFactory).KeepAlive = false;
			service.setUserCredentials(this.txtUsername.Text, this.txtPassword.Text);
			PicasaFeed feed = (PicasaFeed)service.Query(query);
			foreach (PicasaEntry pEntry in feed.Entries)
			{
				if (pEntry.IsAlbum)
				{
					this.albums.Add(pEntry);
				}
			}

			foreach (PicasaEntry album in this.albums)
			{
				this.comboAlbums.Items.Add(GetAlbumName(album));
			}
			AlbumExists(this.txtAlbum.Text);
		}

		private bool AlbumExists(string albumName)
		{
			if (this.albums == null || this.albums.Count == 0)
			{
				FlagUnknown();
				return false;
			}

			foreach (PicasaEntry album in this.albums)
			{
				if (GetAlbumName(album) == albumName)
				{
					FlagWarning();
					return true;
				}
			}

			FlagGood();
			return false;
		}

		private static string GetAlbumName(PicasaEntry album)
		{
			if (album != null && album.Title != null && album.Title.Text.Length > 0)
				return album.Title.Text;
			else
				return "";
		}

		private void FlagGood()
		{
			this.btnValidate.Text = "$";
			this.tooltip.SetToolTip(this.btnValidate, "There is no album using this name.");
		}

		private void FlagUnknown()
		{
			this.btnValidate.Text = "?";
			this.tooltip.SetToolTip(this.btnValidate, "This album may already be in use, click to check.");
		}

		private void FlagWarning()
		{
			this.btnValidate.Text = "!";
			this.tooltip.SetToolTip(this.btnValidate, "This album is already in use - photos will be added to this album.");
		}


		private void txtUsername_TextChanged(object sender, EventArgs e)
		{
			UpdateCurAppSettings();
		}

		private void btnGoPicasa_Click(object sender, EventArgs e)
		{
			System.Diagnostics.Process.Start("http://picasaweb.google.com/");
		}

		private void txtAlbum_TextChanged(object sender, EventArgs e)
		{
			if (this.txtAlbum.Text != App.CurAppSettings.FolderSelectSettings.Album)
			{
				this.customAlbum = this.txtAlbum.Text;
				this.chkUseDefaultAlbum.Checked = false;
			}
			else if (!this.chkUseDefaultAlbum.Checked)
			{
				this.customAlbum = this.txtAlbum.Text;
			}

			if (!AlbumExists(this.txtAlbum.Text))
			{
				this.comboAlbums.SelectedIndex = -1;

				if (!this.txtDescription.Enabled)
				{
					this.txtDescription.Text = this.customDescription;
				}

				this.txtDescription.Enabled = true;
			}
			else
			{
				if (this.txtDescription.Enabled)
				{
					this.customDescription = this.txtDescription.Text;
				}
				UpdateAlbumInfo();
				this.txtDescription.Enabled = false;
			}
			UpdateCurAppSettings();
		}

		private void UpdateAlbumInfo()
		{
			PicasaEntry album = GetAlbum(this.txtAlbum.Text);
			if (album != null)
			{
				this.txtDescription.Text = album.Summary.Text;
			}
		}

		private PicasaEntry GetAlbum(string albumName)
		{
			foreach (PicasaEntry album in this.albums)
			{
				if (GetAlbumName(album) == albumName)
				{
					return album;
				}
			}
			return null;
		}

		public void UpdateCurAppSettings()
		{
			if (this.initializing)
				return;

			Picasa picasaSettings = App.Uploaders.GetUpload(App.CurAppSettings.UploadDetails.Name) as Picasa;
			if (picasaSettings == null)
				picasaSettings = new Picasa();

			picasaSettings.Username = this.txtUsername.Text;
			picasaSettings.RememberPass = this.chkRememberPassword.Checked;
			if (this.chkRememberPassword.Checked)
				picasaSettings.Password = picasaSettings.Tell(this.txtPassword.Text);
			App.CurAppSettings.UploadDetails.Location = this.txtLocation.Text;
			App.CurAppSettings.UploadDetails.Description = this.txtDescription.Text;
			App.CurAppSettings.UploadDetails.Album = this.customAlbum;
			App.CurAppSettings.UploadDetails.UseDefaultAlbum = this.chkUseDefaultAlbum.Checked;
			if (this.rbPublic.Checked)
				picasaSettings.Privacy = Picasa.PicasaPrivacyType.Public;
			else if (this.rbPrivate.Checked)
				picasaSettings.Privacy = Picasa.PicasaPrivacyType.Private;
			App.Uploaders.UpdateUpload(picasaSettings);
		}

		private void btnValidate_Click(object sender, EventArgs e)
		{

		}

		private void comboAlbums_SelectedIndexChanged(object sender, EventArgs e)
		{

			UpdateCurAppSettings();
		}

		private void txtDescription_TextChanged(object sender, EventArgs e)
		{
			UpdateCurAppSettings();
		}

		private void chkUseDefaultAlbum_CheckedChanged(object sender, EventArgs e)
		{
			if (this.chkUseDefaultAlbum.Checked)
			{
				this.customAlbum = this.txtAlbum.Text;
				this.txtAlbum.Text = App.CurAppSettings.FolderSelectSettings.Album;
			}
			else
			{
				this.txtAlbum.Text = this.customAlbum;
			}
		}

		private void txtPassword_TextChanged(object sender, EventArgs e)
		{
			UpdateCurAppSettings();
		}

		private void chkRememberPassword_CheckedChanged(object sender, EventArgs e)
		{
			UpdateCurAppSettings();
		}

		private void PicasaControl_Resize(object sender, EventArgs e)
		{
			//Rectangle bounds = this.lblDisclaimer.Bounds;
			//bounds.Y = this.txtDescription.Bounds.Y + this.txtDescription.Bounds.Height + 5;
			//this.lblDisclaimer.Bounds = bounds;
		}

		private void rbPublic_CheckedChanged(object sender, EventArgs e)
		{
			UpdateCurAppSettings();
		}

		private void rbPrivate_CheckedChanged(object sender, EventArgs e)
		{
			UpdateCurAppSettings();
		}
	}
}
