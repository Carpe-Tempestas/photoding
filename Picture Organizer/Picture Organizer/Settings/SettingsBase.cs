using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using Trebuchet.Settings.WatermarkTypes;

namespace Trebuchet.Settings
{
    public abstract class SettingsBase
    {
        public abstract void LoadSettings(object obj);
        public abstract void LoadDefaults();
    }
}
