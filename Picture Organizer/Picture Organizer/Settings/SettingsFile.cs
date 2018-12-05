using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.IO;

namespace Trebuchet.Settings
{
    public class SettingsFile
    {
        public enum ModeTypes
        {
            Scripted,
            Dialog
        };

        public event EventHandler RenameChanged;
        public event EventHandler ResizeChanged;
        public event EventHandler WatermarkChanged;

        public const string Default = "Default";

        private string source;
        private string settings;
        private bool useDefaultDestination;
        private string destination;
        private string fileMask;
        private bool renameMode; 
        private int numericIDLength; 
        private bool iDBeforeMask;
        private string dateFormat;
        private bool renameOriginals; 
        private bool createRenameDir; 
        private bool resizeMode; 
        private int resizeChoice; 
        private int resizePercent; 
        private bool resizeBoundWidthEnabled; 
        private int resizeBoundWidth; 
        private bool resizeBoundHeightEnabled;
        private int resizeBoundHeight; 
        private bool createResizeDir;
        private bool watermarkMode;
        private int watermarkType;
        private string watermarkText;
        private string watermarkTextBackground;
        private string watermarkTextForeground;
        private int watermarkLocation;
        private string watermarkFont;
        private string watermarkCustomLocation;
        private string watermarkGraphicLocation;
        private string watermarkGraphicKey;
        private int watermarkGraphicHeight;
        private int watermarkGraphicWidth;
        private int watermarkGraphicCropTop;
        private int watermarkGraphicCropLeft;
        private int watermarkGraphicCropBottom;
        private int watermarkGraphicCropRight;
        private int watermarkGraphicRemapPercent;
        private int watermarkTransparency;
        private int watermarkSpacingTop;
        private int watermarkSpacingLeft;
        private int watermarkSpacingHeight;
        private int watermarkSpacingWidth;
        private bool createWatermarkDir; 
        private int imageQuality;
        private bool overrideResolutionEnabled;
        private int resolutionDPI;
        private int applicationMode;
        private bool imageSettingsCollapsed;
        private bool zipMode;
        private bool zipUseDefault;
        private string zipFile;
        private string zipPassword;
        private int zipLevel;

        public SettingsFile()
        {

        }

        public int ApplicationMode
        {
            get { return this.applicationMode; }
            set { this.applicationMode = value; }
        }

        public string SettingsLoaded
        {
            get { return this.settings; }
            set { this.settings = value; }
        }

        public string Source
        {
            get { return this.source; }
            set { this.source = value; }
        }

        public bool UseDefaultDestination
        {
            get { return this.useDefaultDestination; }
            set { this.useDefaultDestination = value; }
        }

        public string Destination
        {
            get { return this.destination; }
            set { this.destination = value; }
        }

        public bool ImageSettingsCollapsed
        {
            get { return this.imageSettingsCollapsed; }
            set { this.imageSettingsCollapsed = value; }
        }

        public string FileMask
        {
            get { return this.fileMask; }
            set { this.fileMask = value; }
        }

        public string DateFormat
        {
            get { return this.dateFormat; }
            set { this.dateFormat = value; }
        }

        public bool RenameMode
        {
            get { return this.renameMode; }
            set { this.renameMode = value; }
        }

        public int NumericIDLength
        {
            get { return this.numericIDLength; }
            set { this.numericIDLength = value; }
        }

        public bool IDBeforeMask
        {
            get { return this.iDBeforeMask; }
            set { this.iDBeforeMask = value; }
        }

        public bool RenameOriginals
        {
            get { return this.renameOriginals; }
            set { this.renameOriginals = value; }
        }

        public bool CreateRenameDir
        {
            get { return this.createRenameDir; }
            set { this.createRenameDir = value; }
        }

        public bool ResizeMode
        {
            get { return this.resizeMode; }
            set
            { this.resizeMode = value; }
        }

        public int ResizeChoice
        {
            get { return this.resizeChoice; }
            set { this.resizeChoice = value; }
        }

        public int ResizePercent
        {
            get { return this.resizePercent; }
            set { this.resizePercent = value; }
        }

        public bool ResizeBoundWidthEnabled
        {
            get { return this.resizeBoundWidthEnabled; }
            set { this.resizeBoundWidthEnabled = value; }
        }

        public int ResizeBoundWidth
        {
            get { return this.resizeBoundWidth; }
            set { this.resizeBoundWidth = value; }
        }

        public bool ResizeBoundHeightEnabled
        {
            get { return this.resizeBoundHeightEnabled; }
            set { this.resizeBoundHeightEnabled = value; }
        }

        public int ResizeBoundHeight
        {
            get { return this.resizeBoundHeight; }
            set { this.resizeBoundHeight = value; }
        }

        public bool CreateResizeDir
        {
            get { return this.createResizeDir; }
            set { this.createResizeDir = value; }
        }

        public bool WatermarkMode
        {
            get { return this.watermarkMode; }
            set { this.watermarkMode = value; }
        }

        public string WatermarkTextBackground
        {
            get { return this.watermarkTextBackground; }
            set { this.watermarkTextBackground = value; }
        }

        public string WatermarkTextForeground
        {
            get { return this.watermarkTextForeground; }
            set { this.watermarkTextForeground = value; }
        }

        public string WatermarkText
        {
            get { return this.watermarkText; }
            set { this.watermarkText = value; }
        }

        public int WatermarkLocation
        {
            get { return this.watermarkLocation; }
            set { this.watermarkLocation = value; }
        }

        public string WatermarkFont
        {
            get { return this.watermarkFont; }
            set { this.watermarkFont = value; }
        }

        public int WatermarkType
        {
            get { return this.watermarkType; }
            set { this.watermarkType = value; }
        }

        public string WatermarkCustomLocation
        {
            get { return this.watermarkCustomLocation; }
            set { this.watermarkCustomLocation = value; }
        }

        public string WatermarkGraphicLocation
        {
            get { return this.watermarkGraphicLocation; }
            set { this.watermarkGraphicLocation = value; }
        }

        public string WatermarkGraphicKey
        {
            get { return this.watermarkGraphicKey; }
            set { this.watermarkGraphicKey = value; }
        }

        public int WatermarkGraphicHeight
        {
            get { return this.watermarkGraphicHeight; }
            set { this.watermarkGraphicHeight = value; }
        }

        public int WatermarkGraphicWidth
        {
            get { return this.watermarkGraphicWidth; }
            set { this.watermarkGraphicWidth = value; }
        }

        public int WatermarkGraphicCropTop
        {
            get { return this.watermarkGraphicCropTop; }
            set { this.watermarkGraphicCropTop = value; }
        }

        public int WatermarkGraphicCropLeft
        {
            get { return this.watermarkGraphicCropLeft; }
            set { this.watermarkGraphicCropLeft = value; }
        }

        public int WatermarkGraphicCropBottom
        {
            get { return this.watermarkGraphicCropBottom; }
            set { this.watermarkGraphicCropBottom = value; }
        }

        public int WatermarkGraphicCropRight
        {
            get { return this.watermarkGraphicCropRight; }
            set { this.watermarkGraphicCropRight = value; }
        }

        public int WatermarkGraphicRemapPercent
        {
            get { return this.watermarkGraphicRemapPercent; }
            set { this.watermarkGraphicRemapPercent = value; }
        }

        public int WatermarkTransparency
        {
            get { return this.watermarkTransparency; }
            set { this.watermarkTransparency = value; }
        }

        public int WatermarkSpacingTop
        {
            get { return this.watermarkSpacingTop; }
            set { this.watermarkSpacingTop = value; }
        }

        public int WatermarkSpacingLeft
        {
            get { return this.watermarkSpacingLeft; }
            set { this.watermarkSpacingLeft = value; }
        }

        public int WatermarkSpacingHeight
        {
            get { return this.watermarkSpacingHeight; }
            set { this.watermarkSpacingHeight = value; }
        }

        public int WatermarkSpacingWidth
        {
            get { return this.watermarkSpacingWidth; }
            set { this.watermarkSpacingWidth = value; }
        }

        public bool CreateWatermarkDir
        {
            get { return this.createWatermarkDir; }
            set { this.createWatermarkDir = value; }
        }

        public int ImageQuality
        {
            get { return this.imageQuality; }
            set { this.imageQuality = value; }
        }

        public bool OverrideResolutionEnabled
        {
            get { return this.overrideResolutionEnabled; }
            set { this.overrideResolutionEnabled = value; }
        }

        public int ResolutionDPI
        {
            get { return this.resolutionDPI; }
            set { this.resolutionDPI = value; }
        }

        public bool ZipMode
        {
            get { return this.zipMode; }
            set { this.zipMode = value; }
        }

        public bool ZipUseDefault
        {
            get { return this.zipUseDefault; }
            set { this.zipUseDefault = value; }
        }

        public string ZipFile
        {
            get { return this.zipFile; }
            set { this.zipFile = value; }
        }

        public string ZipPassword
        {
            get { return this.zipPassword; }
            set { this.zipPassword = value; }
        }

        public int ZipLevel
        {
            get { return this.zipLevel; }
            set { this.zipLevel = value; }
        }

        public void FireRename()
        {
            if (this.RenameChanged != null)
                this.RenameChanged(this, new EventArgs());
        }

        public void FireResize()
        {
            if (this.ResizeChanged != null)
                this.ResizeChanged(this, new EventArgs());
        }

        public void FireWatermark()
        {
            if (this.WatermarkChanged != null)
                this.WatermarkChanged(this, new EventArgs());
        }

        public void LoadSettings(SettingsFile source)
        {
            this.Source = source.Source;
            this.UseDefaultDestination = source.UseDefaultDestination;
            this.Destination = source.Destination;
            this.FileMask = source.FileMask;
            this.DateFormat = source.DateFormat;
            this.RenameMode = source.RenameMode;
            this.NumericIDLength = source.NumericIDLength;
            this.IDBeforeMask = source.IDBeforeMask;
            this.RenameOriginals = source.RenameOriginals;
            this.CreateRenameDir = source.CreateRenameDir;
            this.ResizeMode = source.ResizeMode;
            this.ResizeChoice = source.ResizeChoice;
            this.ResizePercent = source.ResizePercent;
            this.ResizeBoundWidthEnabled = source.ResizeBoundWidthEnabled;
            this.ResizeBoundWidth = source.ResizeBoundWidth;
            this.ResizeBoundHeightEnabled = source.ResizeBoundHeightEnabled;
            this.ResizeBoundHeight = source.ResizeBoundHeight;
            this.CreateResizeDir = source.CreateResizeDir;
            this.WatermarkMode = source.WatermarkMode;
            this.WatermarkType = source.WatermarkType;
            this.WatermarkText = source.WatermarkText;
            this.WatermarkLocation = source.WatermarkLocation;
            this.WatermarkFont = source.WatermarkFont;
            this.WatermarkTextBackground = source.WatermarkTextBackground;
            this.WatermarkTextForeground = source.WatermarkTextForeground;
            this.watermarkGraphicLocation = source.watermarkGraphicLocation;
            this.watermarkCustomLocation = source.WatermarkCustomLocation;
            this.WatermarkGraphicKey = source.WatermarkGraphicKey;
            this.WatermarkGraphicHeight = source.WatermarkGraphicHeight;
            this.WatermarkGraphicWidth = source.WatermarkGraphicWidth;
            this.WatermarkGraphicCropTop = source.WatermarkGraphicCropTop;
            this.WatermarkGraphicCropLeft = source.WatermarkGraphicCropLeft;
            this.WatermarkGraphicCropBottom = source.WatermarkGraphicCropBottom;
            this.WatermarkGraphicCropRight = source.WatermarkGraphicCropRight;
            this.WatermarkGraphicRemapPercent = source.WatermarkGraphicRemapPercent;
            this.WatermarkTransparency = source.WatermarkTransparency;
            this.WatermarkSpacingTop = source.WatermarkSpacingTop;
            this.WatermarkSpacingLeft = source.WatermarkSpacingLeft;
            this.WatermarkSpacingHeight = source.WatermarkSpacingHeight;
            this.WatermarkSpacingWidth = source.WatermarkSpacingWidth;
            this.CreateWatermarkDir = source.CreateWatermarkDir;
            this.ImageQuality = source.ImageQuality;
            this.OverrideResolutionEnabled = source.OverrideResolutionEnabled;
            this.ResolutionDPI = source.ResolutionDPI;
            this.ApplicationMode = source.ApplicationMode;
            this.ZipMode = source.ZipMode;
            this.ZipUseDefault = source.ZipUseDefault;
            this.ZipFile = source.ZipFile;
            this.ZipPassword = source.ZipPassword;
            this.ZipLevel = source.ZipLevel;            
        }

        public string ConvertToPointString(Point point)
        {
            return String.Format("{0},{1}", point.X, point.Y);
        }

        public Point ConvertFromPointString(string pointString)
        {
            Point point = Point.Empty;
            string[] array = pointString.Split(',');
            if (array.Length == 2)
            {
                int temp = 0;
                Int32.TryParse(array[0], out temp);
                point.X = temp;
                Int32.TryParse(array[1], out temp);
                point.Y = temp;
            }
            return point;
        }

        public string ConvertToColorString(Color color)
        {
            return String.Format("{0}:{1}:{2}:{3}", color.A, color.R, color.G, color.B);
        }

        public Color ConvertFromColorString(string color)
        {
            Color ret = Color.Green;
            string[] array = color.Split(':');
            if (array.Length == 4)
            {
                int tempA = 0;
                int tempR = 0;
                int tempG = 0;
                int tempB = 0;

                //A
                Int32.TryParse(array[0], out tempA);

                //R
                Int32.TryParse(array[1], out tempR);

                //G
                Int32.TryParse(array[2], out tempG);

                //B
                Int32.TryParse(array[3], out tempB);

                ret = Color.FromArgb(tempA, tempR, tempG, tempB);
            }
            return ret;
        }
    }
}
