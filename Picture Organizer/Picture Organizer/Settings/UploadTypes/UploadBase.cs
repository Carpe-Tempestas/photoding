using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Trebuchet.Settings.UploadTypes
{
	[Serializable]
    [XmlInclude(typeof(Ftp)), XmlInclude(typeof(Flickr)), XmlInclude(typeof(FaceBook)), XmlInclude(typeof(Picasa)), XmlInclude(typeof(Email))]
	//Add new upload types here!!
    public abstract class UploadBase : SettingsBase
    {
        private string name;
        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }

        public virtual Trebuchet.Settings.UploadSettings.UploadType GetMethod()
        {
            throw new NotImplementedException();
        }
    }
}
