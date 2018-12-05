using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Trebuchet.Settings
{
    public class Compress : SettingsBase
    {
        private bool useDefault;
        private string file;
        private string password;
        private int level;

        public bool UseDefault
        {
            get { return this.useDefault; }
            set { this.useDefault = value; }
        }

        public string ZipFile
        {
            get { return this.file; }
            set { this.file = value; }
        }

        public string Password
        {
            get { return this.password; }
            set { this.password = value; }
        }

        public int Level
        {
            get { return this.level; }
            set { this.level = value; }
        }

        public override void LoadSettings(object obj)
        {
            Compress settings = obj as Compress;
            if (settings != null)
            {
                this.UseDefault = settings.UseDefault;
                this.ZipFile = settings.ZipFile;
                this.Password = settings.Password;
                this.Level = settings.Level;
            }
        }

        public override void LoadDefaults()
        {
            this.UseDefault = true;
            this.ZipFile = "MyZippedPictures.zip";
            this.Password = String.Empty;
            this.Level = 9;
        }
    }
}
