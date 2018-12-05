using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Trebuchet.Settings.WatermarkTypes
{
    [XmlInclude(typeof(WatText)), XmlInclude(typeof(Graphic))]
    public abstract class WatermarkBase : SettingsBase
    {
    }
}
