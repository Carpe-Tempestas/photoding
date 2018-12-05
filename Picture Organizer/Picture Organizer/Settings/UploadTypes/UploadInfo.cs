using System;
using System.Collections.Generic;
using System.Text;

namespace Trebuchet.Settings.UploadTypes
{
    public class UploadInfo : SettingsBase
    {
        private string uploadPath;
        private string name;
        private string album;
        private bool useDefaultAlbum;
        private bool appendAlbum;
		private string description;
		private string location;

        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }

        public string UploadPath
        {
            get { return this.uploadPath; }
            set { this.uploadPath = value; }
        }

        public string Album
        {
            get { return this.album; }
            set { this.album = value; }
        }

        public bool UseDefaultAlbum
        {
            get { return this.useDefaultAlbum; }
            set { this.useDefaultAlbum = value; }
        }

        public bool AppendAlbum
        {
            get { return this.appendAlbum; }
            set { this.appendAlbum = value; }
		}

		public string Description
		{
			get { return this.description; }
			set { this.description = value; }
		}

		public string Location
		{
			get { return this.location; }
			set { this.location = value; }
		}

		public override void LoadSettings(object obj)
		{
			UploadInfo settings = obj as UploadInfo;
			if (settings != null)
			{
				this.Name = settings.Name;
				this.UploadPath = settings.UploadPath;
				this.Album = settings.Album;
				this.UseDefaultAlbum = settings.UseDefaultAlbum;
				this.AppendAlbum = settings.AppendAlbum;
				this.Description = settings.Description;
				this.Location = settings.Location;
			}
		}

		public override void LoadDefaults()
		{
			this.Name = "";
			this.UploadPath = "";
			this.Album = "";
			this.UseDefaultAlbum = true;
			this.AppendAlbum = true;
			this.Location = "";
			this.Description = "";
		}

    }
}
