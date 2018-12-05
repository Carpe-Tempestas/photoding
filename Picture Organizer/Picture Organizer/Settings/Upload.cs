using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using Pikizzi.Settings.UploadTypes;

namespace Pikizzi.Settings
{
    public class Upload : UploadBase
    {

        private Pikizzi.Settings.UploadSettings.UploadType uploadMethod = UploadType.Ftp;
        private UploadInfo uploadDetails = null;

        public override void LoadSettings(object obj)
        {
            Upload settings = obj as Upload;
            if (settings != null)
            {
                this.UploadMethod = settings.UploadMethod;
            }
        }
        public override void LoadDefaults()
        {
            this.UploadMethod = UploadType.Ftp;
        }

        public Pikizzi.Settings.UploadSettings.UploadType UploadMethod
        {
            get
            {
                return this.uploadMethod;
            }
            set
            {
                this.uploadMethod = value;
            }
        }

        public UploadInfo UploaderDetails
        {
            get
            {
                return this.uploadDetails;
            }
            set
            {
                this.uploadDetails = value;
            }
        }

    }
}
