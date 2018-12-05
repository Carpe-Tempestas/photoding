using System;
using System.Collections.Generic;
using System.Text;

namespace Trebuchet.Settings
{
    public partial class FolderSelect : SettingsBase
    {
        private string source;
        private bool useDefaultDestination;
        private string destination;
        private string album;
		private bool appendAlbum;
		private int folderAction = 1;
		public event EventHandler SourceChanged;

        public FolderSelect()
        {

        }

        public string SourceFolder
        {
            get { return this.source; }
            set 
			{
				this.source = value;

				if(this.SourceChanged != null)
					this.SourceChanged(this, new EventArgs());
			}
        }

        public bool UseDefaultDestination
        {
            get { return this.useDefaultDestination; }
            set { this.useDefaultDestination = value; }
        }

		public int FolderAction
		{
			get { return this.folderAction; }
			set { this.folderAction = value; }
		}

        public string DestinationFolder
        {
            get { return this.destination; }
            set { this.destination = value; }
        }

        public string Album
        {
            get { return this.album; }
            set { this.album = value; }
        }

        public bool AppendAlbum
        {
            get { return this.appendAlbum; }
            set { this.appendAlbum = value; }
        }

        public override void LoadSettings(object obj)
        {
            FolderSelect settings = obj as FolderSelect;
            if (settings != null)
            {
                this.SourceFolder = settings.SourceFolder;
                this.UseDefaultDestination = settings.UseDefaultDestination;
                this.DestinationFolder = settings.DestinationFolder;
                this.Album = settings.Album;
                this.AppendAlbum = settings.AppendAlbum;
				this.FolderAction = settings.FolderAction;
            }
        }

        public override void LoadDefaults()
        {
            this.SourceFolder = "";
            this.UseDefaultDestination = true;
            this.DestinationFolder = "";
            this.Album = "MyAlbum";
            this.AppendAlbum = true;
			this.FolderAction = 1;
        }
    }
}
