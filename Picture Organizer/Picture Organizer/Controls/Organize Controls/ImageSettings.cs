using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Imaging;
using Trebuchet.Settings.ImageTypes;
using System.Collections;
using Trebuchet.Helper_Classes;

namespace Trebuchet.Controls
{
    public partial class ImageSettings : UserControl, IPikControl
    {
		private bool initializing = false;
		public event EventHandler enabling;
		public event EventHandler directionsChanging;

        public ImageSettings()
        {
            InitializeComponent();
			this.comboFormat.Items.Add("Default");
			this.comboFormat.Items.Add("jpeg");
			this.comboFormat.Items.Add("png");
			this.comboFormat.Items.Add("gif");
			this.comboFormat.Items.Add("bmp");
			this.comboFormat.Items.Add("tiff");
			this.comboFormat.Items.Add("exif");
			this.comboFormat.Items.Add("wmf");
			this.comboFormat.Items.Add("memorybmp");
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
				return false;
			}
		}

		public bool ControlEnabled
		{
			get
			{
				return true;
			}
			set
			{
				//Do nothing
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
            this.initializing = true;
            this.numImageQuality.Value = App.AppSettings.ImageAdjSettings.ImageQuality;
			InitializeImageQuality();
			InitializeFormat();
			this.chkUseLowerCase.Checked = App.AppSettings.ImageAdjSettings.UseLowerCaseExtensions;
            this.chkUseDefaultRes.Checked = App.AppSettings.ImageAdjSettings.UseDefaultResolution;
            this.numUpDownResDPI.Value = App.AppSettings.ImageAdjSettings.ResolutionDPI;
            this.initializing = false;
        }

		private void InitializeFormat()
		{
			if (App.AppSettings.ImageAdjSettings.Format == "Default")
				this.comboFormat.SelectedIndex = 0;
			else if (App.AppSettings.ImageAdjSettings.Format == "image/jpeg")
				this.comboFormat.SelectedIndex = 1;
			else if (App.AppSettings.ImageAdjSettings.Format == "image/png")
				this.comboFormat.SelectedIndex = 2;
			else if (App.AppSettings.ImageAdjSettings.Format == "image/gif")
				this.comboFormat.SelectedIndex = 3;
			else if (App.AppSettings.ImageAdjSettings.Format == "image/bmp")
				this.comboFormat.SelectedIndex = 4;
			else if (App.AppSettings.ImageAdjSettings.Format == "image/tiff")
				this.comboFormat.SelectedIndex = 5;
			else if (App.AppSettings.ImageAdjSettings.Format == "image/exif")
				this.comboFormat.SelectedIndex = 6;
			else if (App.AppSettings.ImageAdjSettings.Format == "image/wmf")
				this.comboFormat.SelectedIndex = 7;
			else if (App.AppSettings.ImageAdjSettings.Format == "image/memorybmp")
				this.comboFormat.SelectedIndex = 8;
			else
				this.comboFormat.SelectedIndex = 0;
		}

		private void InitializeImageQuality()
		{
			if (this.numImageQuality.Value == 100)
				this.comboImageQuality.SelectedIndex = 0;
			else if (this.numImageQuality.Value == 50)
				this.comboImageQuality.SelectedIndex = 1;
			else if (this.numImageQuality.Value == 25)
				this.comboImageQuality.SelectedIndex = 2;
			else
				this.comboImageQuality.SelectedIndex = 3;
		}

        public void UpdateCurAppSettings()
        {
            if (!this.initializing)
            {
                App.CurAppSettings.ImageAdjSettings.ImageQuality = (int)this.numImageQuality.Value;
                App.CurAppSettings.ImageAdjSettings.UseDefaultResolution = this.chkUseDefaultRes.Checked;
                App.CurAppSettings.ImageAdjSettings.ResolutionDPI = (int)this.numUpDownResDPI.Value;
				App.CurAppSettings.ImageAdjSettings.UseLowerCaseExtensions = this.chkUseLowerCase.Checked;
				if (this.comboFormat.SelectedIndex == 0)
					App.CurAppSettings.ImageAdjSettings.Format = this.comboFormat.Text;
				else
					App.CurAppSettings.ImageAdjSettings.Format = "image/" + this.comboFormat.Text;
            }
        }

        public bool CanContinue()
        {
            return true;
        }

        private void numImageQuality_ValueChanged(object sender, EventArgs e)
        {
            UpdateCurAppSettings();
        }

        private void chkOverRes_CheckedChanged(object sender, EventArgs e)
        {
            this.numUpDownResDPI.Enabled = !this.chkUseDefaultRes.Checked;
            UpdateCurAppSettings();
        }

        private void numUpDownResDPI_ValueChanged(object sender, EventArgs e)
        {
            UpdateCurAppSettings();
        }

		private void tabOtherAdjustments_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (this.tabOtherAdjustments.SelectedTab == this.tabStandardEffects)
				this.basicMatrixControl1.Initialize();
			else if (this.tabOtherAdjustments.SelectedTab == this.tabAdvancedEffects)
				this.matrixControl1.Initialize();
		}

		private void comboImageQuality_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (this.comboImageQuality.Text == "High")
			{
				this.numImageQuality.Value = 100;
				this.numImageQuality.Enabled = false;
			}
			else if (this.comboImageQuality.Text == "Medium")
			{
				this.numImageQuality.Value = 50;
				this.numImageQuality.Enabled = false;
			}
			else if (this.comboImageQuality.Text == "Low")
			{
				this.numImageQuality.Value = 25;
				this.numImageQuality.Enabled = false;
			}
			else if (this.comboImageQuality.Text == "Medium")
			{
				this.numImageQuality.Enabled = true;
			}
			else if (this.comboImageQuality.Text == "Advanced")
			{
				this.numImageQuality.Enabled = true;
			}

			UpdateCurAppSettings();
		}

		private void chkUseLowerCase_CheckedChanged(object sender, EventArgs e)
		{
			UpdateCurAppSettings();
		}

		private void comboFormat_SelectedIndexChanged(object sender, EventArgs e)
		{
			UpdateCurAppSettings();
		}

		public void SetViewer(ThumbnailViewer viewer)
		{
			this.exifControl1.Viewer = viewer;
		}
	}
}
