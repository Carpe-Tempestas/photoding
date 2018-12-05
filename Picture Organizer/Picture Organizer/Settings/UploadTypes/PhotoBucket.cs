using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Trebuchet.Settings.UploadTypes
{
    public class PhotoBucket : SettingsBase
    {


        public override void LoadSettings(object obj)
        {
            PhotoBucket settings = obj as PhotoBucket;
            if (settings != null)
            {

            }
        }
        public override void LoadDefaults()
        {
        }
    }
}
