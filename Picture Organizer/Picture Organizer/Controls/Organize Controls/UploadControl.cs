using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Trebuchet.Dialogs;
using MyUploadTypes = Trebuchet.Settings.UploadSettings.UploadType;
using Trebuchet.Settings.UploadTypes;
using Trebuchet.Controls.Uploading;

namespace Trebuchet.Controls
{
    public partial class UploadControl : TrebUserControl, IPikControl
    {
        public event EventHandler enabling;
        public event EventHandler directionsChanging;
		private List<Control> keeperControls = new List<Control>();
		private bool updating = false;

        public UploadControl()
        {
            InitializeComponent();
			Initialize();
			this.toolTipAddProvider.SetToolTip(this.btnAddProvider, "Click here to add a provider that will send your photos to the internet.");
			this.toolTipDeleteProvider.SetToolTip(this.btnDeleteProvider, "Click here to delete the selected provider.");
        }

        public string Directions
        {
            get
            {
                return "Directions:" + System.Environment.NewLine +
                    "This will do some file uploading";
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
				return App.CurAppSettings.UploadModeEnabled;
            }
            set
            {
				App.CurAppSettings.UploadModeEnabled = value;
				this.Enabled = value;
            }
        }

        private void btnAddProvider_Click(object sender, EventArgs e)
        {
            ProviderDialog provider = new ProviderDialog();
            if(provider.ShowDialog() == DialogResult.OK)
            {
                AddProvider(provider.UploadName, provider.GetProvider());
            }
        }

        private void AddProvider(string name, MyUploadTypes uploadType)
        {
			this.updating = true;
			UploadBase uploader = null;
            switch (uploadType)
            {
                case MyUploadTypes.Ftp:
					uploader = new Ftp();
					uploader.Name = name;
                    break;
                case MyUploadTypes.Facebook:
					uploader = new FaceBook();
					uploader.Name = name;
                    break;
                case MyUploadTypes.Flickr:
					uploader = new Flickr();
					uploader.Name = name;
                    break;
                case MyUploadTypes.Picasa:
					uploader = new Picasa();
					uploader.Name = name;
                    break;
				case MyUploadTypes.Email:
					uploader = new Email();
					uploader.Name = name;
					break;
                default:
                    break;

            }
			App.Uploaders.UpdateUpload(uploader);
			App.CurAppSettings.UploadDetails.Name = name;
			AddProviderControl(name, uploadType);
            this.comboProviders.Items.Add(name);
			this.updating = false;
            this.comboProviders.SelectedIndex = this.comboProviders.Items.Count - 1;
        }

		private void AddProviderControl(string name)
		{
			UploadBase test = App.Uploaders.GetUpload(name);
			Control control = null;
			if (test != null)
			{
				switch (test.GetMethod())
				{
					case MyUploadTypes.Ftp:
						control = new FtpControl();
						break;
					case MyUploadTypes.Facebook:
						control = new FaceBookControl();
						break;
					case MyUploadTypes.Flickr:
						control = new FlickrControl();
						break;
					case MyUploadTypes.Picasa:
						control = new PicasaControl();
						break;
					case MyUploadTypes.Email:
						control = new EmailControl();
						break;
					default:
						break;
				}
				if (control != null)
				{
					control.Dock = DockStyle.Fill;
					this.panelProviderControl.Dock = DockStyle.Fill;
					this.panelProviderControl.Controls.Add(control);
					control.BringToFront();

					if (this.Parent is ControlWrapper)
						((ControlWrapper)this.Parent).MinPanelSize = new Size(this.Bounds.Width, this.Bounds.Height);
				}
			}
		}

		private void AddProviderControl(string name, MyUploadTypes uploadType)
		{
			UploadBase test = App.Uploaders.GetUpload(name);
			Control control = null;
			switch (uploadType)
			{
				case MyUploadTypes.Ftp:
					control = new FtpControl();
					break;
				case MyUploadTypes.Facebook:
					control = new FaceBookControl();
					break;
				case MyUploadTypes.Flickr:
					control = new FlickrControl();
					break;
				case MyUploadTypes.Picasa:
					control = new PicasaControl();
					break;
				case MyUploadTypes.Email:
					control = new EmailControl();
					break;
				default:
					break;
			}
			if (control != null)
			{
				control.Dock = DockStyle.Fill;
				this.panelProviderControl.Dock = DockStyle.Fill;
				this.panelProviderControl.Controls.Add(control);
				control.BringToFront();

				if (this.Parent is ControlWrapper)
					((ControlWrapper)this.Parent).MinPanelSize = new Size(this.Bounds.Width, this.Bounds.Height);
			}
		}

		private void comboProviders_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (!this.updating)
			{
				App.CurAppSettings.UploadDetails.Name = this.comboProviders.SelectedItem.ToString();
				AddProviderControl(App.CurAppSettings.UploadDetails.Name);
				if (this.comboProviders.SelectedItem.ToString() != String.Empty)
					this.btnDeleteProvider.Enabled = true;
				else
					this.btnDeleteProvider.Enabled = false;
			}
		}

		private void RemoveUploadControl()
		{
			int count = 0;
			while (this.Controls.Count != this.keeperControls.Count)
			{
				if (count > this.Controls.Count-1)
					break;

				if (!this.keeperControls.Contains(this.Controls[count]))
				{
					this.Controls.Remove(this.Controls[count]);
				}
				count++;
			}
		}

		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			if (!this.DesignMode)
			{
				Initialize();
				App.CurAppSettingChanged += new EventHandler(App_CurAppSettingChanged);
			}
		}

		void App_CurAppSettingChanged(object sender, EventArgs e)
		{
			Initialize();
		}
	
		public void Initialize()
		{
			this.comboProviders.Items.Clear();

			//if (String.IsNullOrEmpty(App.CurAppSettings.UploadDetails.Name))
			//    return;

			bool uploaderFound = false;
			int index = 0;
			foreach (UploadBase uploadBase in App.Uploaders.Uploaders)
			{
				this.comboProviders.Items.Add(uploadBase.Name);
				if (!uploaderFound && uploadBase.Name != App.CurAppSettings.UploadDetails.Name)
					index++;
				else if(uploadBase.Name == App.CurAppSettings.UploadDetails.Name)
					uploaderFound = true;
			}

			if (uploaderFound)
			{
				this.comboProviders.SelectedIndex = index;
				this.btnDeleteProvider.Enabled = true;
			}
			else
			{
				this.btnDeleteProvider.Enabled = false;
			}


			foreach (Control control in this.Controls)
			{
				if (control is IUpControl)
				{
					((IUpControl)control).Initialize();
				}
			}

			if (App.CurAppSettings.UploadModeEnabled != this.Enabled)
			{
				ControlWrapper wrapper = GetWrapper();
				if(wrapper != null)
					wrapper.EnableControls(App.CurAppSettings.UploadModeEnabled);
			}
		}

		public void UpdateCurAppSettings()
		{
			foreach (Control control in this.Controls)
			{
				if (control is IUpControl)
				{
					((IUpControl)control).UpdateCurAppSettings();
				}
			}
		}

		private void btnDeleteProvider_Click(object sender, EventArgs e)
		{
			if (this.comboProviders.SelectedItem.ToString() != String.Empty &&
				MessageBox.Show("Are you sure you want to delete this provider: " 
				+ this.comboProviders.SelectedItem.ToString() + "?", "Delete Provider?",
				MessageBoxButtons.YesNo) == DialogResult.Yes)
			{
				App.Uploaders.DeleteUpload(this.comboProviders.SelectedItem.ToString());
				this.comboProviders.Items.RemoveAt(this.comboProviders.SelectedIndex);
				this.comboProviders.SelectedIndex = -1;
				this.btnDeleteProvider.Enabled = false;
				RemoveUploadControl();
				App.TheApp.SaveUploaders();
			}
				
		}
    }
}
