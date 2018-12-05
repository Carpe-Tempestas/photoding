using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using System.Drawing;

namespace Trebuchet.Settings.WatermarkTypes
{
    public class Graphic : WatermarkBase
    {
        private string location;
        private string key;
        private int height;
        private int width;
        private int cropTop;
        private int cropLeft;
        private int cropBottom;
        private int cropRight;
        private int remapPercent;

        public Graphic()
        {
            LoadDefaults();
        }

        public string Location
        {
            get { return this.location; }
            set { this.location = value; }
        }

        public string Key
        {
            get { return this.key; }
            set { this.key = value; }
        }

        public int Height
        {
            get { return this.height; }
            set { this.height = value; }
        }

        public int Width
        {
            get { return this.width; }
            set { this.width = value; }
        }

        public int CropTop
        {
            get { return this.cropTop; }
            set { this.cropTop = value; }
        }

        public int CropLeft
        {
            get { return this.cropLeft; }
            set { this.cropLeft = value; }
        }

        public int CropBottom
        {
            get { return this.cropBottom; }
            set { this.cropBottom = value; }
        }

        public int CropRight
        {
            get { return this.cropRight; }
            set { this.cropRight = value; }
        }

        public int RemapPercent
        {
            get { return this.remapPercent; }
            set { this.remapPercent = value; }
        }

        public override void LoadSettings(object obj)
        {
            Graphic settings = obj as Graphic;
            if (settings != null)
            {
                this.Location = settings.Location;
                this.Key = settings.Key;
                this.Height = settings.Height;
                this.Width = settings.Width;
                this.CropTop = settings.CropTop;
                this.CropLeft = settings.CropLeft;
                this.CropRight = settings.CropRight;
                this.CropBottom = settings.CropBottom;
                this.RemapPercent = settings.RemapPercent;
            }
        }
        public override void LoadDefaults()
        {
            this.Location = "0,0";
            this.Key = ConvertToColorString(Color.Green);
            this.Height = 10;
            this.Width = 10;
            this.CropTop = 0;
            this.CropLeft = 0;
            this.CropRight = 0;
            this.CropBottom = 0;
            this.RemapPercent = 0;
        }

        public string ConvertToColorString(Color color)
        {
            return String.Format("{0}:{1}:{2}:{3}", color.A, color.R, color.G, color.B);
        }
    }
}
