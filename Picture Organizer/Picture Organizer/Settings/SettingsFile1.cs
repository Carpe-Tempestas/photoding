using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.IO;
using Trebuchet.Settings.UploadTypes;

namespace Trebuchet.Settings
{
    public class SettingsFile1 : SettingsBase
    {
        public enum ModeTypes
        {
            Scripted,
            Dialog
        };

        public event EventHandler RenameChanged;
        public event EventHandler ResizeChanged;
        public event EventHandler WatermarkChanged;
		public event EventHandler FolderSelectChanged;
		public event EventHandler ImageSettingsChanged;
		public event EventHandler CompressChanged;
		public event EventHandler UploadChanged;

        public const string Default = "Default";

        private string settingsLoaded;
		private string name;
		private string description;
        private int applicationMode;
        private bool compressMode;
        private bool watermarkMode;
        private bool resizeMode;
        private bool renameMode;
        private bool uploadMode;
        private FolderSelect folderSelectSettings;
        private Rename renameSettings;
        private Resize resizeSettings;
        private Watermark watermarkSettings;
        private Compress compressSettings;
        private ImageSettings imageAdjSettings;
        private UploadInfo uploadDetails;

        public SettingsFile1()
        {
            folderSelectSettings = new FolderSelect();
            renameSettings = new Rename();
            resizeSettings = new Resize();
            watermarkSettings = new Watermark();
            compressSettings = new Compress();
            imageAdjSettings = new ImageSettings();
            uploadDetails = new UploadInfo();
        }

        public int ApplicationMode
        {
            get { return this.applicationMode; }
            set { this.applicationMode = value; }
        }

        public string SettingsLoaded
        {
            get { return this.settingsLoaded; }
            set { this.settingsLoaded = value; }
		}

		public string Name
		{
			get { return this.name; }
			set { this.name = value; }
		}

		public string Description
		{
			get { return this.description; }
			set { this.description = value; }
		}

        public FolderSelect FolderSelectSettings
        {
            get { return this.folderSelectSettings; }
            set { this.folderSelectSettings = value; }
        }

        public bool RenameMode
        {
            get { return this.renameMode; }
            set { this.renameMode = value; }
        }

        public Rename RenameSettings
        {
            get { return this.renameSettings; }
            set { this.renameSettings = value; }
        }

        public bool ResizeMode
        {
            get { return this.resizeMode; }
            set
            { this.resizeMode = value; }
        }

        public Resize ResizeSettings
        {
            get { return this.resizeSettings; }
            set { this.resizeSettings = value; }
        }

        public bool WatermarkMode
        {
            get { return this.watermarkMode; }
            set { this.watermarkMode = value; }
        }

        public Watermark WatermarkSettings
        {
            get { return this.watermarkSettings; }
            set { this.watermarkSettings = value; }
		}

		public ImageSettings ImageAdjSettings
		{
			get { return this.imageAdjSettings; }
			set { this.imageAdjSettings = value; }
		}

        public bool CompressMode
        {
            get { return this.compressMode; }
            set { this.compressMode = value; }
        }

        public Compress CompressSettings
        {
            get { return this.compressSettings; }
            set { this.compressSettings = value; }
        }

        public bool UploadModeEnabled
        {
            get { return this.uploadMode; }
            set { this.uploadMode = value; }
        }

        public UploadInfo UploadDetails
        {
            get { return this.uploadDetails; }
            set { this.uploadDetails = value; }
		}

		public void FireAllChanged()
		{
			FireRename();
			FireResize();
			FireWatermark();
			FireFolderSelect();
			FireImageSettings();
			FireCompressSettings();
			FireUploadSettings();
		}

        public void FireRename()
        {
            if (this.RenameChanged != null)
                this.RenameChanged(this, new EventArgs());
        }

        public void FireResize()
        {
            if (this.ResizeChanged != null)
                this.ResizeChanged(this, new EventArgs());
        }

        public void FireWatermark()
        {
            if (this.WatermarkChanged != null)
                this.WatermarkChanged(this, new EventArgs());
		}

		public void FireFolderSelect()
		{
			if (this.FolderSelectChanged != null)
				this.FolderSelectChanged(this, new EventArgs());
		}

		public void FireImageSettings()
		{
			if (this.ImageSettingsChanged != null)
				this.ImageSettingsChanged(this, new EventArgs());
		}

		public void FireCompressSettings()
		{
			if (this.CompressChanged != null)
				this.CompressChanged(this, new EventArgs());
		}

		public void FireUploadSettings()
		{
			if (this.UploadChanged != null)
				this.UploadChanged(this, new EventArgs());
		}

        public override void LoadSettings(object obj)
        {
            SettingsFile1 settings = obj as SettingsFile1;
            if (settings != null)
            {                
                this.SettingsLoaded = settings.SettingsLoaded;
				this.Description = settings.Description;
                this.ApplicationMode = settings.ApplicationMode;
                this.CompressMode = settings.CompressMode;
                this.WatermarkMode = settings.WatermarkMode;
                this.ResizeMode = settings.ResizeMode;
                this.RenameMode = settings.RenameMode;
				this.UploadModeEnabled = settings.UploadModeEnabled;
                this.FolderSelectSettings.LoadSettings(settings.FolderSelectSettings);
                this.RenameSettings.LoadSettings(settings.RenameSettings);
                this.ResizeSettings.LoadSettings(settings.ResizeSettings);
                this.WatermarkSettings.LoadSettings(settings.WatermarkSettings);
                this.CompressSettings.LoadSettings(settings.CompressSettings);
                this.ImageAdjSettings.LoadSettings(settings.ImageAdjSettings);
				this.UploadDetails.LoadSettings(settings.UploadDetails);
            }
        }

        public void LoadAllDefaults()
        {
            LoadDefaults();
            this.FolderSelectSettings.LoadDefaults();
            this.RenameSettings.LoadDefaults();
            this.ResizeSettings.LoadDefaults();
            this.WatermarkSettings.LoadDefaults();
            this.CompressSettings.LoadDefaults();
            this.ImageAdjSettings.LoadDefaults();
			this.UploadDetails.LoadDefaults();
        }

        public override void LoadDefaults()
        {
            this.SettingsLoaded = "Default";
			this.Description = "This is a default settings file.";
            this.RenameMode = false;
            this.ResizeMode = false;
            this.WatermarkMode = false;
			this.UploadModeEnabled = false;
            this.CompressMode = false;
            this.ApplicationMode = -1;
        }

        public string ConvertToPointString(Point point)
        {
            return String.Format("{0},{1}", point.X, point.Y);
        }

        public Point ConvertFromPointString(string pointString)
        {
            Point point = Point.Empty;
            string[] array = pointString.Split(',');
            if (array.Length == 2)
            {
                int temp = 0;
                Int32.TryParse(array[0], out temp);
                point.X = temp;
                Int32.TryParse(array[1], out temp);
                point.Y = temp;
            }
            return point;
        }

        public string ConvertToColorString(Color color)
        {
            return String.Format("{0}:{1}:{2}:{3}", color.A, color.R, color.G, color.B);
        }

        public Color ConvertFromColorString(string color)
        {
            Color ret = Color.Green;
            string[] array = color.Split(':');
            if (array.Length == 4)
            {
                int tempA = 0;
                int tempR = 0;
                int tempG = 0;
                int tempB = 0;

                //A
                Int32.TryParse(array[0], out tempA);

                //R
                Int32.TryParse(array[1], out tempR);

                //G
                Int32.TryParse(array[2], out tempG);

                //B
                Int32.TryParse(array[3], out tempB);

                ret = Color.FromArgb(tempA, tempR, tempG, tempB);
            }
            return ret;
        }

		public override string ToString()
		{
			return this.Name;
		}
    }
}
