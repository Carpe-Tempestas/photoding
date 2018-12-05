using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using Trebuchet.Settings.ImageTypes;
using System.Drawing.Imaging;

namespace Trebuchet.Settings
{
    public class ImageSettings : SettingsBase
    {
        private int imageQuality;
        private bool useDefaultResolution;
        private int resolutionDPI;
		private ImageColorMatrix currentEffect;
		private bool createDir = false;
		private bool useLowerCaseExtensions;
		private string format;

		public enum ExifModes : int
		{
			CopyAll,
			CopySome,
			CopyNone
		}
		private ExifModes exifMode;


		public ExifModes ExifMode
		{
			get { return this.exifMode; }
			set { this.exifMode = value; }
		}

		public bool CreateDir
		{
			get { return this.createDir; }
			set { this.createDir = value; }
		}

		public bool UseLowerCaseExtensions
		{
			get { return this.useLowerCaseExtensions; }
			set { this.useLowerCaseExtensions = value; }
		}

		public string Format
		{
			get { return this.format; }
			set { this.format = value; }
		}

        public int ImageQuality
        {
            get { return this.imageQuality; }
            set { this.imageQuality = value; }
        }

        public bool UseDefaultResolution
        {
            get { return this.useDefaultResolution; }
            set { this.useDefaultResolution = value; }
        }

        public int ResolutionDPI
        {
            get { return this.resolutionDPI; }
            set { this.resolutionDPI = value; }
        }

		public ImageColorMatrix CurrentEffect
		{
			get { return this.currentEffect; }
			set { this.currentEffect = value; }
		}

        public override void LoadSettings(object obj)
        {
            ImageSettings settings = obj as ImageSettings;
            if (settings != null)
            {
                this.ImageQuality = settings.ImageQuality;
                this.UseDefaultResolution = settings.UseDefaultResolution;
                this.ResolutionDPI = settings.ResolutionDPI;
				this.Format = settings.Format;
				this.CreateDir = settings.CreateDir;
				this.ExifMode = settings.ExifMode;
				if (settings.CurrentEffect == null)
					this.CurrentEffect = new ImageColorMatrix("Default", ImageColorMatrix.MatrixTypes.Custom, ImageColorMatrix.ValueModes.None);
				else
					this.CurrentEffect = settings.CurrentEffect.Clone() as ImageColorMatrix;
            }
        }

        public override void LoadDefaults()
        {
            this.ImageQuality = 100;
            this.UseDefaultResolution = true;
            this.ResolutionDPI = 300;
			this.CreateDir = false;
			this.Format = "Default";
			this.CurrentEffect = new ImageColorMatrix("Default", ImageColorMatrix.MatrixTypes.Custom, ImageColorMatrix.ValueModes.None);
			this.ExifMode = ExifModes.CopyAll;
        }

        public bool IsImageGettingAdjusted()
        {
            ImageSettings temp = new ImageSettings();
            temp.LoadDefaults();

            if (this.ImageQuality == temp.ImageQuality && this.UseDefaultResolution == temp.UseDefaultResolution
                && this.ResolutionDPI == temp.ResolutionDPI && this.CreateDir == temp.CreateDir
                && this.Format == temp.Format && this.CurrentEffect == temp.CurrentEffect
                && this.ExifMode == temp.ExifMode)
                return false;
            else
                return true;
        }
    }
}
