using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections.ObjectModel;
//using Facebook.Entity;
using Trebuchet.Settings.UploadTypes;
//using Facebook.Components;
using System.Web;

namespace Trebuchet.Controls.Uploading
{
	public partial class FaceBookControl : UserControl, IUpControl
	{
		//private Collection<Album> albums = null;
		private ToolTip tooltip = new ToolTip();
		private string customAlbum;
		private string customLocation;
		private string customDescription;
		private bool initializing = false;
		//FacebookService facebook = new FacebookService();

		public FaceBookControl()
		{
			InitializeComponent();
			FlagUnknown();
			App.CurAppSettings.FolderSelectChanged += new EventHandler(CurAppSettings_FolderSelectChanged);
		}

		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			Initialize();
		}

		public void Initialize()
		{
			this.initializing = true;
			
			//FaceBook facebookSettings = App.Uploaders.GetUpload(App.CurAppSettings.UploadDetails.Name) as FaceBook;
			//if (facebookSettings == null)
			//    facebookSettings = new FaceBook();
			//if(!String.IsNullOrEmpty(facebookSettings.SessionKey))
			//    this.facebook.SessionKey = facebookSettings.Request(facebookSettings.SessionKey);
			//this.txtLocation.Text = App.CurAppSettings.UploadDetails.Location;
			//this.txtDescription.Text = App.CurAppSettings.UploadDetails.Description;
			//this.customAlbum = App.CurAppSettings.UploadDetails.Album;
			//if (App.CurAppSettings.UploadDetails.UseDefaultAlbum)
			//    this.txtAlbum.Text = App.CurAppSettings.FolderSelectSettings.Album;
			//else
			//    this.txtAlbum.Text = this.customAlbum;
			//this.chkUseDefaultAlbum.Checked = App.CurAppSettings.UploadDetails.UseDefaultAlbum;
			//App.Uploaders.UpdateUpload(facebookSettings);

			this.initializing = false;
		}

		void CurAppSettings_FolderSelectChanged(object sender, EventArgs e)
		{
			this.txtAlbum.Text = App.CurAppSettings.FolderSelectSettings.Album;
		}

		private void btnGetAlbums_Click(object sender, EventArgs e)
		{
			//this.facebook.ApplicationKey = "9af06a1d94bb82cc78b3a3a5ffb56a46";
			//this.facebook.Secret = "0bb7fa5cf80eca9bd1eadae64f10e217";
			//UploadBase uploadSettings = App.Uploaders.GetUpload(App.CurAppSettings.UploadDetails.Name);
			//FaceBook facebookSettings = uploadSettings as FaceBook;
			//RemoveAllItems();

			//try
			//{
			//    this.facebook.CreateSession(authToken);
			//    if (!facebookSettings.SessionExpires && !String.IsNullOrEmpty(facebookSettings.SessionKey))
			//        facebook.SessionKey = facebookSettings.Request(facebookSettings.SessionKey);
			//    this.albums = facebook.GetPhotoAlbums();

			//    facebookSettings.SessionExpires = facebook.SessionExpires;
			//    facebookSettings.SessionKey = facebookSettings.Tell(facebook.SessionKey);
			//    App.Uploaders.UpdateUpload(facebookSettings);

			//    foreach (Album album in albums)
			//    {
			//        this.comboAlbums.Items.Add(album.Name);
			//    }
			//    AlbumExists(this.txtAlbum.Text);
			//    UpdateCurAppSettings();
			//}
			//catch (Exception ex)
			//{
			//    MessageBox.Show("There was an error communicating with Facebook.  Please visit https://github.com/Carpe-Tempestas/photoding.php if this continues.");
			//}
		}

		private void RemoveAllItems()
		{
			//if (this.albums != null && this.albums.Count > 0)
			//{
			//    this.albums.Clear();
			//    this.comboAlbums.Items.Clear();
			//}
		}

		private void comboAlbums_SelectedIndexChanged(object sender, EventArgs e)
		{
			//if (this.albums.Count == 0 || this.comboAlbums.SelectedIndex == -1)
			//    return;

			//string album = this.albums[this.comboAlbums.SelectedIndex].Name;
			//this.customAlbum = album;
			//this.txtAlbum.Text = album;
			//UpdateAlbumInfo();
		}

		private void UpdateAlbumInfo()
		{
			//Album album = GetAlbum(this.txtAlbum.Text);
			//if (album != null)
			//{
			//    this.txtLocation.Text = album.Location;
			//    this.txtDescription.Text = album.Description;
			//}
		}

		private void txtAlbum_TextChanged(object sender, EventArgs e)
		{
			if (this.txtAlbum.Text != App.CurAppSettings.FolderSelectSettings.Album)
			{
				this.customAlbum = this.txtAlbum.Text;
				this.chkUseDefaultAlbum.Checked = false;
			}

			if (!AlbumExists(this.txtAlbum.Text))
			{
				this.comboAlbums.SelectedIndex = -1;

				if (!this.txtDescription.Enabled && !this.txtLocation.Enabled)
				{
					this.txtLocation.Text = this.customLocation;
					this.txtDescription.Text = this.customDescription;
				}

				this.txtLocation.Enabled = true;
				this.txtDescription.Enabled = true;
			}
			else
			{
				if (this.txtLocation.Enabled && this.txtDescription.Enabled)
				{
					this.customLocation = this.txtLocation.Text;
					this.customDescription = this.txtDescription.Text;
				}
				UpdateAlbumInfo();
				this.txtLocation.Enabled = false;
				this.txtDescription.Enabled = false;
			}
			UpdateCurAppSettings();
		}

		public void UpdateCurAppSettings()
		{
			if (!this.initializing)
			{
				//FaceBook facebookSettings = App.Uploaders.GetUpload(App.CurAppSettings.UploadDetails.Name) as FaceBook;
				//if (facebookSettings == null)
				//    facebookSettings = new FaceBook();

				//if (!String.IsNullOrEmpty(this.facebook.SessionKey))
				//    this.facebook.SessionKey = facebookSettings.Tell(this.facebook.SessionKey);
				//facebookSettings.SessionExpires = this.facebook.SessionExpires;
				//App.CurAppSettings.UploadDetails.Location = this.txtLocation.Text;
				//App.CurAppSettings.UploadDetails.Description = this.txtDescription.Text;
				//App.CurAppSettings.UploadDetails.Album = this.customAlbum;
				//App.CurAppSettings.UploadDetails.UseDefaultAlbum = this.chkUseDefaultAlbum.Checked;
				//App.Uploaders.UpdateUpload(facebookSettings);
			}
		}

		//private Album GetAlbum(string albumName)
		//{
		//    foreach (Album album in this.albums)
		//    {
		//        if (album.Name == albumName)
		//        {
		//            return album;
		//        }
		//    }
		//    return null;
		//}

		private void btnValidate_Click(object sender, EventArgs e)
		{

		}

		private void txtLocation_TextChanged(object sender, EventArgs e)
		{
			UpdateCurAppSettings();
		}

		private void txtDescription_TextChanged(object sender, EventArgs e)
		{
			UpdateCurAppSettings();
		}

		private bool AlbumExists(string albumName)
		{
			//if (this.albums == null || this.albums.Count == 0)
			//{
			//    FlagUnknown();
			//    return false;
			//}

			//foreach (Album album in this.albums)
			//{
			//    if (album.Name == albumName)
			//    {
			//        FlagWarning();
			//        return true;
			//    }
			//}

			//FlagGood();
			return false;
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

		private void btnGoFacebook_Click(object sender, EventArgs e)
		{
			System.Diagnostics.Process.Start("http://www.facebook.com/");
		}
	}
}
