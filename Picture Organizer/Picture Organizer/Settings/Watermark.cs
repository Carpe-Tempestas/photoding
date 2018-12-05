using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using Trebuchet.Settings.WatermarkTypes;

namespace Trebuchet.Settings
{
    public class Watermark : SettingsBase
    {
        public enum WatermarkLocation
        {
            [XmlEnum(Name = "TopLeft")]
            TopLeft,
            [XmlEnum(Name = "TopCenter")]
            TopCenter,
            [XmlEnum(Name = "TopRight")]
            TopRight,
            [XmlEnum(Name = "CenterLeft")]
            CenterLeft,
            [XmlEnum(Name = "CenterCenter")]
            CenterCenter,
            [XmlEnum(Name = "CenterRight")]
            CenterRight,
            [XmlEnum(Name = "BottomLeft")]
            BottomLeft,
            [XmlEnum(Name = "BottomCenter")]
            BottomCenter,
            [XmlEnum(Name = "BottomRight")]
            BottomRight,
            [XmlEnum(Name = "Tile")]
            Tile,
            [XmlEnum(Name = "Custom")]
            Custom
        };

        public enum WatermarkType
        {
            [XmlEnum(Name = "TextWatermark")]
            TextWatermark,
            [XmlEnum(Name = "GraphicWatermark")]
            GraphicWatermark
        };

        private WatermarkType watType;
        private WatermarkLocation location;
        private string customLocation;
        private int transparency;
        private int spacingTop;
        private int spacingLeft;
        private int spacingHeight;
        private int spacingWidth;
        private bool createDir;
        private WatermarkBase watSettings = null;
		private WatText textWatermark = new WatText();
		private Graphic graphicWatermark = new Graphic();


        public WatermarkLocation Location
        {
            get { return this.location; }
            set { this.location = value; }
        }

        public WatermarkType WatType
        {
            get { return this.watType; }
            set
            {
                if (this.watType != value)
                {
                    this.watType = value;
                    ManageTypes();
                }
            }
        }

        public string CustomLocation
        {
            get { return this.customLocation; }
            set { this.customLocation = value; }
        }

        public int Transparency
        {
            get { return this.transparency; }
            set { this.transparency = value; }
        }

        public int SpacingTop
        {
            get { return this.spacingTop; }
            set { this.spacingTop = value; }
        }

        public int SpacingLeft
        {
            get { return this.spacingLeft; }
            set { this.spacingLeft = value; }
        }

        public int SpacingHeight
        {
            get { return this.spacingHeight; }
            set { this.spacingHeight = value; }
        }

        public int SpacingWidth
        {
            get { return this.spacingWidth; }
            set { this.spacingWidth = value; }
        }

        public WatermarkBase Setting
        {
            get { return this.watSettings; }
            set { this.watSettings = value; }
        }

        public bool CreateDir
        {
            get { return this.createDir; }
            set { this.createDir = value; }
        }

        public override void LoadSettings(object obj)
        {
            Watermark settings = obj as Watermark;
            if (settings != null)
            {
                this.WatType = settings.WatType;
                this.Location = settings.Location;
                this.CustomLocation = settings.CustomLocation;
                this.Transparency = settings.Transparency;
                this.SpacingTop = settings.SpacingTop;
                this.SpacingLeft = settings.SpacingLeft;
                this.SpacingHeight = settings.SpacingHeight;
                this.SpacingWidth = settings.SpacingWidth;
                if(this.watSettings == null)
                    ManageTypes();
                this.watSettings.LoadSettings(settings.watSettings);
                this.CreateDir = settings.CreateDir;
            }
        }
        public override void LoadDefaults()
        {
            this.WatType = WatermarkType.TextWatermark;
            this.Location = WatermarkLocation.CenterCenter;
            this.Transparency = 53;
            this.CustomLocation = "0,0";
            this.SpacingTop = 0;
            this.SpacingLeft = 0;
            this.SpacingHeight = 0;
            this.SpacingWidth = 0;
            this.CreateDir = false;
        }

        private void ManageTypes()
        {
            if (this.watType == WatermarkType.TextWatermark)
            {
                this.watSettings = this.textWatermark;
            }
            else if (this.watType == WatermarkType.GraphicWatermark)
            {
                this.watSettings = this.graphicWatermark;
            }
        }
    }
}
