using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Trebuchet.Properties;
using Trebuchet.Controls;

/*

			this.watermarkControl = new Trebuchet.Controls.WatermarkControl();
			this.resizeControl = new Trebuchet.Controls.ResizeControl();
			this.renameControl = new Trebuchet.Controls.RenameControl();
			this.folderSelectControl = new Trebuchet.Controls.FolderSelectControl();
			this.compressControl = new Trebuchet.Controls.CompressControl();
			this.uploadControl = new Trebuchet.Controls.UploadControl();
			this.imageSettings2 = new Trebuchet.Controls.ImageSettings();
 * 
 *			INSERT WHAT'S BELOW, UNDER WHAT'S ABOVE
 * 
			this.watermarkControl1 = new Trebuchet.Controls.ControlWrapper(this.watermarkControl);
			this.resizeControl1 = new Trebuchet.Controls.ControlWrapper(this.resizeControl);
			this.renameControl1 = new Trebuchet.Controls.ControlWrapper(this.renameControl);
			this.folderSelectControl1 = new Trebuchet.Controls.ControlWrapper(this.folderSelectControl);
			this.imageSettings1 = new Trebuchet.Controls.ControlWrapper(this.imageSettings2);
			this.compressControl1 = new Trebuchet.Controls.ControlWrapper(this.compressControl);
			this.uploadControl1 = new Trebuchet.Controls.ControlWrapper(this.uploadControl);
*/

namespace Trebuchet
{
    public partial class Dialog : Form
    {
        public event EventHandler ModeClosing;
		private bool wasMaximized = false;

        public Dialog()
        {
			InitializeComponent();
			this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);

            this.bottomSplitter.Height = 5;
            this.topSplitter.Width = 5;

            this.progressFinish1.ImagesLoaded += new EventHandler(progressFinish1_ImagesLoaded);
        }

        void progressFinish1_ImagesLoaded(object sender, EventArgs e)
        {
            this.pictureControl1.Viewer = sender as ThumbnailViewer;
			this.imageSettings2.SetViewer(sender as ThumbnailViewer);
            
        }

        public MenuStrip BigMenu
        {
            get
            {
                return this.menuStrip1;
            }
            set
            {
                while (value.Items.Count > 0)
                    this.menuStrip1.Items.Add(value.Items[0]);
                value.Visible = false;
            }
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            if (this.ModeClosing != null)
                this.ModeClosing(this, new EventArgs());
        }

        protected override void OnLoad(EventArgs e)
        {                
            base.OnLoad(e);

            this.tabStrip1.SelectedTab = this.tabStripFolderSelect;
			if(this.WindowState == FormWindowState.Maximized)
				this.topSplitter.SplitPosition = App.IntSettings.TopSplitterOffsetMaximized;
			else
				this.topSplitter.SplitPosition = App.IntSettings.TopSplitterOffsetMinimized;

			if (this.WindowState == FormWindowState.Maximized)
				this.bottomSplitter.SplitPosition = App.IntSettings.BottomSplitterOffsetMaximized;
			else
				this.bottomSplitter.SplitPosition = App.IntSettings.BottomSplitterOffsetMinimized;
        }

		protected override void OnSizeChanged(EventArgs e)
		{
			base.OnSizeChanged(e);

			if (this.WindowState == FormWindowState.Maximized && !this.wasMaximized)
			{
				this.bottomSplitter.SplitPosition = App.IntSettings.BottomSplitterOffsetMaximized;
				this.topSplitter.SplitPosition = App.IntSettings.TopSplitterOffsetMaximized;
				this.wasMaximized = true;
			}
			else if (this.WindowState != FormWindowState.Maximized && this.wasMaximized)
			{
				this.bottomSplitter.SplitPosition = App.IntSettings.BottomSplitterOffsetMinimized;
				this.topSplitter.SplitPosition = App.IntSettings.TopSplitterOffsetMinimized;
				this.wasMaximized = false;
			}

			if (this.WindowState == FormWindowState.Normal)
			{
				App.IntSettings.WindowSize = this.Size;
				App.IntSettings.Maximized = false;
			}
			else if(this.WindowState == FormWindowState.Maximized)
			{
				App.IntSettings.Maximized = true;
			}
		}

        public void Initialize()
        {
        }

        public void UpdateControls()
        {
            Initialize();
        }

        #region Settings
        /*
        public void InitializeSettings()
        {
            //Source
            this.txtSource.Text = TrebuchetApp.AppSettings.Source;

            #region Modes
            //Rename
            this.chkRename.Checked = TrebuchetApp.AppSettings.RenameMode;

            //Resize
            this.chkResize.Checked = TrebuchetApp.AppSettings.ResizeMode;

            //Watermark
            this.chkWatermark.Checked = TrebuchetApp.AppSettings.WatermarkMode;
            #endregion Modes

            #region Rename
            //File Mask
            this.txtMask.Text = TrebuchetApp.AppSettings.FileMask;

            //Numeric ID Length
            this.numIDLength.Value = TrebuchetApp.AppSettings.NumericIDLength;

            //ID Before Mask
            this.chkFlipNumeric.Checked = TrebuchetApp.AppSettings.IDBeforeMask;

            //Create Rename Directory
            this.chkCreateRename.Checked = TrebuchetApp.AppSettings.CreateRenameDir;
            #endregion Rename

            #region Resize
            //Choice
            if (TrebuchetApp.AppSettings.ResizeChoice == 0)
                this.rbResPercent.Checked = true;
            else if (TrebuchetApp.AppSettings.ResizeChoice == 1)
                this.rbResBound.Checked = true;

            //Resize Percent
            this.numResizePercent.Value = TrebuchetApp.AppSettings.ResizePercent;

            //Bound Width Enabled
            this.chkBoundWidth.Checked = TrebuchetApp.AppSettings.ResizeBoundWidthEnabled;

            //Bound Width Value
            this.numUpDownBoundWidth.Value = TrebuchetApp.AppSettings.ResizeBoundWidth;

            //Bound Height Enabled
            this.chkBoundHeight.Checked = TrebuchetApp.AppSettings.ResizeBoundWidthEnabled;

            //Bound Height Value
            this.numUpDownBoundHeight.Value = TrebuchetApp.AppSettings.ResizeBoundHeight;

            //Create Resize Directory
            this.chkCreateResize.Checked = TrebuchetApp.AppSettings.CreateResizeDir;
            #endregion Resize

            #region Watermark
            //Watermark Location
            this.comboWaterLoc.SelectedIndex = TrebuchetApp.AppSettings.WatermarkLocation;

            //Watermark Font
            TypeConverter tc = TypeDescriptor.GetConverter(typeof(Font));
            this.fontDialog1.Font = (Font)tc.ConvertFromString(TrebuchetApp.AppSettings.WatermarkFont);

            //Watermark Text
            this.txtWaterText.Text = TrebuchetApp.AppSettings.WatermarkText;

            //Watermark Transparency
            this.numTransparency.Value = TrebuchetApp.AppSettings.WatermarkTransparency;

            #endregion Watermark

            //Image Quality
            this.numImageQuality.Value = TrebuchetApp.AppSettings.ImageQuality;

            //Resolution DPI Enabled
            this.chkOverRes.Checked = TrebuchetApp.AppSettings.OverrideResolutionEnabled;

            //Resolution DPI Value
            this.numUpDownResDPI.Value = TrebuchetApp.AppSettings.ResolutionDPI;
        }

        public void CollectSettings()
        {
            //Source
            TrebuchetApp.AppSettings.Source = this.txtSource.Text;

            #region Modes
            //Rename
            TrebuchetApp.AppSettings.RenameMode = this.chkRename.Checked;

            //Resize
            TrebuchetApp.AppSettings.ResizeMode = this.chkResize.Checked;

            //Watermark
            TrebuchetApp.AppSettings.WatermarkMode = this.chkWatermark.Checked;
            #endregion Modes

            #region Rename
            //File Mask
            TrebuchetApp.AppSettings.FileMask = this.txtMask.Text;

            //Numeric ID Length
            TrebuchetApp.AppSettings.NumericIDLength = (int)this.numIDLength.Value;

            //ID Before Mask
            TrebuchetApp.AppSettings.IDBeforeMask = this.chkFlipNumeric.Checked;

            //Create Rename Directory
            TrebuchetApp.AppSettings.CreateRenameDir = this.chkCreateRename.Checked;
            #endregion Rename

            #region Resize
            //Choice
            if (this.rbResPercent.Checked)
                TrebuchetApp.AppSettings.ResizeChoice = 0; //Percent
            else
                TrebuchetApp.AppSettings.ResizeChoice = 1; //Bound

            //Resize Percent
            TrebuchetApp.AppSettings.ResizePercent = (int)this.numResizePercent.Value;

            //Bound Width Enabled
            TrebuchetApp.AppSettings.ResizeBoundWidthEnabled = this.chkBoundWidth.Checked;

            //Bound Width Value
            TrebuchetApp.AppSettings.ResizeBoundWidth = (int)this.numUpDownBoundWidth.Value;

            //Bound Height Enabled
            TrebuchetApp.AppSettings.ResizeBoundWidthEnabled = this.chkBoundHeight.Checked;

            //Bound Height Value
            TrebuchetApp.AppSettings.ResizeBoundHeight = (int)this.numUpDownBoundHeight.Value;

            //Create Resize Directory
            TrebuchetApp.AppSettings.CreateResizeDir = this.chkCreateResize.Checked;
            #endregion Resize

            #region Watermark
            //Watermark Location
            TrebuchetApp.AppSettings.WatermarkLocation = this.comboWaterLoc.SelectedIndex;

            //Watermark Font
            TypeConverter tc = TypeDescriptor.GetConverter(typeof(Font));
            TrebuchetApp.AppSettings.WatermarkFont = tc.ConvertToString(this.fontDialog1.Font);

            //Watermark Text
            TrebuchetApp.AppSettings.WatermarkText = this.txtWaterText.Text;

            //Watermark Transparency
            TrebuchetApp.AppSettings.WatermarkTransparency = (int)this.numTransparency.Value;

            #endregion Watermark

            //Image Quality
            TrebuchetApp.AppSettings.ImageQuality = (int)this.numImageQuality.Value;

            //Resolution DPI Enabled
            TrebuchetApp.AppSettings.OverrideResolutionEnabled = this.chkOverRes.Checked;

            //Resolution DPI Value
            TrebuchetApp.AppSettings.ResolutionDPI = (int)this.numUpDownResDPI.Value;
        }

        */
        #endregion Settings


        private void OnSelectedTabChanged(object sender, Messir.Windows.Forms.SelectedTabChangedEventArgs e)
        {
            if (e.SelectedTab.Text == "Start Here")
            {
                this.folderSelectControl1.BringToFront();
            }
            else if (e.SelectedTab.Text == "Rename")
            {
                this.renameControl1.BringToFront();
            }
            else if (e.SelectedTab.Text == "Resize")
            {
                this.resizeControl1.BringToFront();
            }
            else if (e.SelectedTab.Text == "Watermark")
            {
                this.watermarkControl1.BringToFront();
			}
			else if (e.SelectedTab.Text == "Image Settings")
			{
				this.imageSettings1.BringToFront();
			}
            else if (e.SelectedTab.Text == "Compress")
            {
                this.compressControl1.BringToFront();
            }
            else if (e.SelectedTab.Text == "Upload")
            {
                this.uploadControl1.BringToFront();
            }
        }

        private void OnBottomSplitterMoved(object sender, SplitterEventArgs e)
        {
			if (this.WindowState == FormWindowState.Maximized)
				App.IntSettings.BottomSplitterOffsetMaximized = this.bottomSplitter.SplitPosition;
			else
				App.IntSettings.BottomSplitterOffsetMinimized = this.bottomSplitter.SplitPosition;
		}

		void topSplitter_SplitterMoved(object sender, System.Windows.Forms.SplitterEventArgs e)
		{
			if (this.WindowState == FormWindowState.Maximized)
				App.IntSettings.TopSplitterOffsetMaximized = this.topSplitter.SplitPosition;
			else
				App.IntSettings.TopSplitterOffsetMinimized = this.topSplitter.SplitPosition;

		}
    }
}