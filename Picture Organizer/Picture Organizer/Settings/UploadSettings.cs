using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using Trebuchet.Settings.UploadTypes;

namespace Trebuchet.Settings
{
	[Serializable]
    public class UploadSettings : SettingsBase
	{
        List<UploadBase> uploaders = new List<UploadBase>();

        public enum UploadType
        {
            [XmlEnum(Name = "FTP")]
            Ftp,
            [XmlEnum(Name = "Facebook")]
            Facebook,
            [XmlEnum(Name = "Flickr")]
            Flickr,
            [XmlEnum(Name = "Picasa")]
            Picasa,
			[XmlEnum(Name = "Email")]
			Email
            //Add new upload types here!!
        };

		public UploadSettings()
		{

		}

        public List<UploadBase> Uploaders
        {
            get { return this.uploaders; }
            set { this.uploaders = value; }
        }

        public override void LoadDefaults()
        {
            
        }

        public override void LoadSettings(object obj)
        {
            UploadSettings settings = obj as UploadSettings;
            if (settings != null)
            {
                this.Uploaders = settings.Uploaders;
            }
        }

        public UploadBase GetUpload(string name)
        {
            foreach (UploadBase uploader in this.uploaders)
            {
                if (uploader.Name == name)
                    return uploader;
            }
            return null;
        }

        public void UpdateUpload(UploadBase uploadBase)
        {
            bool found = false;
            for(int x = 0; x < this.uploaders.Count; x++)
            {
                if (this.uploaders[x].Name == uploadBase.Name)
                {
                    this.uploaders[x] = uploadBase;
                    found = true;
                    break;
                }
            }
            if (!found)
                this.uploaders.Add(uploadBase);
        }

		public void DeleteUpload(string name)
		{
			UploadBase uploaderToDelete = null;
			foreach (UploadBase uploader in this.uploaders)
			{
				if (uploader.Name == name)
				{
					uploaderToDelete = uploader;
					break;
				}
			}
			this.uploaders.Remove(uploaderToDelete);
		}
    }
}
