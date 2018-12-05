using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using System.Drawing;

namespace Trebuchet.Settings.WatermarkTypes
{
    public class WatText : WatermarkBase
    {
        private string watText;
        private string textBackground;
        private string textForeground;
        private string watFont;

        public WatText()
        {
            LoadDefaults();
        }

        public string TextBackground
        {
            get 
            { 
                if(this.textBackground == null)
                    return ConvertToColorString(Color.Black); 
                
                return this.textBackground;
            }
            set { this.textBackground = value; }
        }

        public string TextForeground
        {
            get
            {
                if (this.textForeground == null)
                    return ConvertToColorString(Color.White);

                return this.textForeground;
            }
            set { this.textForeground = value; }
        }

        public string WatermarkText
        {
            get { return this.watText; }
            set { this.watText = value; }
        }

        public string WatFont
        {
            get
            {
                if (this.watFont == null)
                    return "Microsoft Sans Serif, 8.25pt"; 
                
                return this.watFont;
            }
            set { this.watFont = value; }
        }

        public override void LoadSettings(object obj)
        {
            WatText settings = obj as WatText;
            if (settings != null)
            {
                this.WatermarkText = settings.WatermarkText;
                this.TextBackground = settings.TextBackground;
                this.TextForeground = settings.TextForeground;
                this.WatFont = settings.WatFont;
            }
        }

        public override void LoadDefaults()
        {
            this.WatermarkText = "My Watermark";
            this.TextBackground = ConvertToColorString(Color.Black);
            this.TextForeground = ConvertToColorString(Color.White);
            this.WatFont = "Microsoft Sans Serif, 8.25pt";
        }

        public string ConvertToColorString(Color color)
        {
            return String.Format("{0}:{1}:{2}:{3}", color.A, color.R, color.G, color.B);
        }
    }
}
