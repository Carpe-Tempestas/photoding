using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using EnterpriseDT.Net.Ftp;
using Trebuchet.Settings.UploadTypes;
using System.Security;
using System.Runtime.InteropServices;
using FlickrNet;
using System.Threading;

namespace Trebuchet.Controls.Uploading
{
	public partial class FlickrControl : UserControl, IUpControl
    {
        private bool initializing = false;
		private string oldAlbum;
		private string customAlbum;
		private string customDescription;
		private ToolTip locationTip;
		private bool updating = false;
		private FlickrNet.Flickr flickrService = new FlickrNet.Flickr("aefe86f96a84c4b2c40542593f1329d5", "09ea41f118201c0b");
		private Photosets photosets = null;
		private Auth token = null;
		private string frob = "";
		private ToolTip tooltip = new ToolTip();
		private ThreadStart flickrCommsStart = null;
		private Thread flickrComms = null;
		private bool requestCommsStop = false;
		private const int InitialWait = 15;
		private const int TotalWait = 18-InitialWait; //3 minutes minus the initial wait
		private string progressText = "";
		private EventHandler AddItems;

		public FlickrControl()
        {
			InitializeComponent(); 
			this.flickrCommsStart = new ThreadStart(GetAlbums);
			this.AddItems = new EventHandler(OnAddItems);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            Initialize();
			App.CurAppSettings.FolderSelectChanged += new EventHandler(CurAppSettings_FolderSelectChanged);
        }

		void CurAppSettings_FolderSelectChanged(object sender, EventArgs e)
		{
			if (this.chkUseDefaultAlbum.Checked)
			{
				this.txtAlbum.Text = App.CurAppSettings.FolderSelectSettings.Album;
			}
		}

        public void Initialize()
        {
			this.initializing = true;
			Trebuchet.Settings.UploadTypes.Flickr flickrSettings = App.Uploaders.GetUpload(App.CurAppSettings.UploadDetails.Name) as Trebuchet.Settings.UploadTypes.Flickr;
			if (flickrSettings == null)
				flickrSettings = new Trebuchet.Settings.UploadTypes.Flickr();

			if (flickrSettings.Privacy == Trebuchet.Settings.UploadTypes.Flickr.FlickrPrivacyType.Private)
				this.rbPrivate.Checked = true;
			else if (flickrSettings.Privacy == Trebuchet.Settings.UploadTypes.Flickr.FlickrPrivacyType.Public)
				this.rbPublic.Checked = true;

			if (flickrSettings.AllowFamily)
				this.chkFamily.Checked = true;
			else
				this.chkFamily.Checked = false;

			if (flickrSettings.AllowFriends)
				this.chkFriends.Checked = true;
			else
				this.chkFriends.Checked = false;

			this.chkUseDefaultAlbum.Checked = App.CurAppSettings.UploadDetails.UseDefaultAlbum;
			if (this.chkUseDefaultAlbum.Checked)
				this.txtAlbum.Text = App.CurAppSettings.FolderSelectSettings.Album;
			else
				this.txtAlbum.Text = App.CurAppSettings.UploadDetails.Album;
			this.customAlbum = App.CurAppSettings.UploadDetails.Album;
			this.txtDescription.Text = App.CurAppSettings.UploadDetails.Description;

            this.initializing = false;
        }

		public void UpdateCurAppSettings()
        {
            if (this.initializing)
                return;

			Trebuchet.Settings.UploadTypes.Flickr flickrSettings = App.Uploaders.GetUpload(App.CurAppSettings.UploadDetails.Name) as Trebuchet.Settings.UploadTypes.Flickr;
			if (flickrSettings == null)
				flickrSettings = new Trebuchet.Settings.UploadTypes.Flickr();

			flickrSettings.AllowFamily = this.chkFamily.Checked;
			flickrSettings.AllowFriends = this.chkFriends.Checked;
			if (this.rbPrivate.Checked)
				flickrSettings.Privacy = Trebuchet.Settings.UploadTypes.Flickr.FlickrPrivacyType.Private;
			else if (this.rbPublic.Checked)
				flickrSettings.Privacy = Trebuchet.Settings.UploadTypes.Flickr.FlickrPrivacyType.Public;
			App.CurAppSettings.UploadDetails.UseDefaultAlbum = this.chkUseDefaultAlbum.Checked;
			App.CurAppSettings.UploadDetails.Album = this.customAlbum;
			App.CurAppSettings.UploadDetails.Description = this.txtDescription.Text;
			App.Uploaders.UpdateUpload(flickrSettings);
        }

		private void btnGetAlbums_Click(object sender, EventArgs e)
		{
			if (this.flickrComms != null && this.flickrComms.IsAlive)
			{
				this.requestCommsStop = true;
			}
			else
			{
				RemoveAllItems();

				this.flickrComms = new Thread(this.flickrCommsStart);
				this.flickrComms.Name = "Flickr Comms Thread";
				this.flickrComms.Start();
			}
		}

		private void OnAddItems(object sender, EventArgs e)
		{
			if (this.InvokeRequired)
			{
				this.Invoke(AddItems, new object[] { sender, e });
			}
			else
			{
				foreach (Photoset album in this.photosets.PhotosetCollection)
				{
					this.comboAlbums.Items.Add(album.Title);
				}
				AlbumExists(this.txtAlbum.Text);
				UpdateCurAppSettings();
			}
		}

		private void GetAlbums()
		{
			if (!EnsureCommunications())
				return;
			
			try
			{
				this.photosets = this.flickrService.PhotosetsGetList();
				if (this.photosets == null)
					return;

				this.AddItems.Invoke(this, new EventArgs());
			}
			catch (Exception exception)
			{
				System.Diagnostics.Debug.WriteLine("Exception: " + exception.Message);
			}
		}

		private bool EnsureCommunications()
		{
			Trebuchet.Settings.UploadTypes.Flickr flickrSettings = App.Uploaders.GetUpload(App.CurAppSettings.UploadDetails.Name) as Trebuchet.Settings.UploadTypes.Flickr;
			if (String.IsNullOrEmpty(flickrSettings.Token) || String.IsNullOrEmpty(this.frob))
			{
				this.frob = this.flickrService.AuthGetFrob();
				string url = flickrService.AuthCalcUrl(frob, AuthLevel.Write);

				System.Diagnostics.Process.Start(url);
			}

			if (String.IsNullOrEmpty(flickrSettings.Token) && String.IsNullOrEmpty(this.frob))
			{
				//Throw connection exception?
				return false;
			}
			else if(!String.IsNullOrEmpty(flickrSettings.Token))
			{
				this.flickrService.AuthToken = flickrSettings.Request(flickrSettings.Token);
			}

			if (MessageBox.Show("Check your internet browser to allow photoding access to your flickr account.  Return to photoding when you have allowed access."
				+ System.Environment.NewLine + System.Environment.NewLine +
				"Did you allow photoding access to your flickr account?", "Confirm", MessageBoxButtons.YesNo) == DialogResult.No)
				return false;


			bool tokenValidated = false;
			try
			{
				if(!String.IsNullOrEmpty(flickrSettings.Token))
					tokenValidated = ValidateToken(flickrService.AuthCheckToken(flickrSettings.Request(flickrSettings.Token)), flickrSettings.Request(flickrSettings.Token));
			}
			catch(Exception e)
			{
				System.Diagnostics.Debug.WriteLine(e.Message);
				return false;
			}

			if (String.IsNullOrEmpty(flickrSettings.Token) || !tokenValidated)
			{
				Auth auth = null;
				if (GetAuthetication(ref auth) && auth != null)
				{
					flickrSettings.Token = flickrSettings.Tell(auth.Token);
					flickrSettings.UserID = auth.User.UserId;
					if (auth.Permissions == AuthLevel.Write)
						flickrSettings.WriteAccess = true;
					else
						flickrSettings.WriteAccess = false;
					App.Uploaders.UpdateUpload(flickrSettings);
				}
			}

			if (!String.IsNullOrEmpty(flickrSettings.Token) && !String.IsNullOrEmpty(flickrSettings.UserID))
				return true;
			else
				return false;
		}

		private bool GetAuthetication(ref Auth auth)
		{
			int tries = 0;
			string working = "Working";
			int invalidFrob = 0;
			List<string>progress = new List<string>();
			progress.Add(".");
			progress.Add("..");
			progress.Add("...");

			for (int x = 0; x < FlickrControl.InitialWait && !this.requestCommsStop; x++)
			{
				if (invalidFrob > 3)
					this.frob = this.flickrService.AuthGetFrob();

				try
				{
					System.Diagnostics.Debug.WriteLine("Trying!");
					auth = this.flickrService.AuthGetToken(this.frob);
					if (this.flickrService.IsAuthenticated)
					{
						this.flickrService.AuthToken = auth.Token;
						return true;
					}
				}
				catch (Exception e)
				{
					if (e is FlickrApiException && e.Message.Contains("Invalid frob (108)"))
						invalidFrob++;

					Thread.Sleep(2000);
					this.progressText = working + progress[tries % progress.Count];
					Application.Idle += new EventHandler(Application_Idle);
					System.Diagnostics.Debug.WriteLine("Still looking: " + e.Message);
				}
			}

			if (this.requestCommsStop)
				return false;
			

			for (int y = 0; y < FlickrControl.TotalWait && !this.requestCommsStop; y++)
			{
				Thread.Sleep(10000);
				try
				{
					System.Diagnostics.Debug.WriteLine("Trying - 2!");
					auth = this.flickrService.AuthGetToken(this.frob);
					if (this.flickrService.IsAuthenticated)
						break;
				}
				catch (Exception ex)
				{
					System.Diagnostics.Debug.WriteLine("Still looking: " + ex.Message);
					if (auth != null && y == 9)
					{
						MessageBox.Show("Unable to connect to Flickr services.  Ensure you've allowed access and try again.");
						return false;
					}
				}
			}
			return true;
		}

		void Application_Idle(object sender, EventArgs e)
		{
			if (!this.InvokeRequired)
			{
				Application.Idle -= new EventHandler(Application_Idle);
				this.btnGetAlbums.Text = this.progressText;
			}
		}

		private bool ValidateToken(Auth auth, string token)
		{
			if (auth == null)
				return false;
			else if (auth.Token == token)
				return true;
			else
				return false;
		}

		private void RemoveAllItems()
		{
			if (this.photosets != null && this.photosets.PhotosetCollection.Length > 0)
			{
				this.photosets = null;
				this.comboAlbums.Items.Clear();
			}
		}

		private void comboAlbums_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (this.photosets.PhotosetCollection.Length == 0 || this.comboAlbums.SelectedIndex == -1)
				return;

			string album = this.photosets.PhotosetCollection[this.comboAlbums.SelectedIndex].Title;
			this.customAlbum = album;
			this.txtAlbum.Text = album;
			UpdateAlbumInfo();
		}

		private void UpdateAlbumInfo()
		{
			Photoset photoset = GetAlbum(this.txtAlbum.Text);
			if (photoset != null)
			{
				this.txtDescription.Text = photoset.Description;
			}
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

		private Photoset GetAlbum(string albumName)
		{
			foreach (Photoset photoset in this.photosets.PhotosetCollection)
			{
				if (photoset.Title == albumName)
				{
					return photoset;
				}
			}
			return null;
		}

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
			if (this.photosets == null || this.photosets.PhotosetCollection.Length == 0)
			{
				FlagUnknown();
				return false;
			}

			foreach (Photoset photoset in this.photosets.PhotosetCollection)
			{
				if (photoset.Title == albumName)
				{
					FlagWarning();
					return true;
				}
			}

			FlagGood();
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

		private void btnGoFlickr_Click(object sender, EventArgs e)
		{
			System.Diagnostics.Process.Start("http://www.flickr.com/");
		}

		private void rbPublic_CheckedChanged(object sender, EventArgs e)
		{
			UpdateCurAppSettings();
		}

		private void rbPrivate_CheckedChanged(object sender, EventArgs e)
		{
			UpdateCurAppSettings();
		}

		private void chkFamily_CheckedChanged(object sender, EventArgs e)
		{
			UpdateCurAppSettings();
		}

		private void chkFriends_CheckedChanged(object sender, EventArgs e)
		{
			UpdateCurAppSettings();
		}
    }
}
