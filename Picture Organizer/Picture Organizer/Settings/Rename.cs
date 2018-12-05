using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Trebuchet.Settings
{
    public class Rename : SettingsBase
    {
        private string fileMask;
        private int numericIDLength;
        private bool iDBeforeMask;
        private string dateFormat;
        private bool renameOriginals;
        private bool createDir;
		private bool useAlbumName;

        public string FileMask
        {
            get { return this.fileMask; }
            set { this.fileMask = value; }
        }

        public string DateFormat
        {
            get { return this.dateFormat; }
            set { this.dateFormat = value; }
        }

        public int NumericIDLength
        {
            get { return this.numericIDLength; }
            set { this.numericIDLength = value; }
        }

        public bool IDBeforeMask
        {
            get { return this.iDBeforeMask; }
            set { this.iDBeforeMask = value; }
        }

        public bool RenameOriginals
        {
            get { return this.renameOriginals; }
            set { this.renameOriginals = value; }
        }

        public bool CreateDir
        {
            get { return this.createDir; }
            set { this.createDir = value; }
		}

		public bool UseAlbumName
		{
			get { return this.useAlbumName; }
			set { this.useAlbumName = value; }
		}

        public override void LoadSettings(object obj)
        {
            Rename settings = obj as Rename;
            if (settings != null)
            {
                this.FileMask = settings.FileMask;
                this.NumericIDLength = settings.NumericIDLength;
                this.IDBeforeMask = settings.IDBeforeMask;
                this.DateFormat = settings.DateFormat;
                this.RenameOriginals = settings.RenameOriginals;
                this.CreateDir = settings.CreateDir;
				this.UseAlbumName = settings.UseAlbumName;
            }
        }

        public override void LoadDefaults()
        {
            this.FileMask = "myPicture";
            this.DateFormat = "MM-dd-yy";
            this.NumericIDLength = 2;
            this.IDBeforeMask = false;
            this.RenameOriginals = false;
            this.CreateDir = false;
			this.UseAlbumName = false;
        }
    }
}
