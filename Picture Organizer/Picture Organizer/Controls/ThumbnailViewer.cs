using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace Trebuchet.Controls
{
    public partial class ThumbnailViewer : UserControl
    {
        private List<ThumbnailControl> thumbControls = new List<ThumbnailControl>();
        private List<ThumbnailControl> selectedThumbs = new List<ThumbnailControl>();
        public event EventHandler SelectionChanged;
        public event EventHandler ImagesLoaded;
        private int selectedThumbIndex = -1;
		private bool stopLoading = false;
        private Size thumbSize = new Size(90, 90);
        private int spacing = 10;
        private int xCount = 0;
        private int yCount = 0;
        private int maxVisibleRows = 0;
        private bool scrolled = false;
        private Point originalSelectionPoint = Point.Empty;
        private Point selectionPoint = Point.Empty;
        private bool mouseDown = false;
        private SelectionPanel selectionPanel;
        private bool disregardSelectionEvents = false;
        private Bitmap contentBitmap = null;
        private Rectangle targetRect;
        private Point originScreen;
        private Rectangle usableRect;
        private Brush selectionBrush;
        private const int ScrollStops = 2;
        private int thumbHeight = -1;
        private int thumbWidth = -1;
        private int widthCount = -1;
        private int heightCount = -1;

        public ThumbnailViewer()
        {
            InitializeComponent();

            this.contentBitmap = new Bitmap(Bounds.Width, Bounds.Height);
            this.selectionBrush = new SolidBrush(Color.FromArgb(200, 0xE8, 0xED, 0xF5));
            this.DoubleBuffered = true;

        }

		public List<ThumbnailControl> SelectedThumbs
		{
			get
			{
				return this.selectedThumbs;
			}
		}

		public bool StopLoading
		{
			get { return this.stopLoading; }
		}

        public string GetSelectedPhotoLocation()
        {
			if (this.selectedThumbIndex == -1)
				return "";

			return this.thumbControls[this.selectedThumbIndex].Path;
        }

		private int GetOffsetIncludingUndesiredFiles(FileInfo[] rgFiles, int potentialLocation)
		{
			for (int x = 0; x < rgFiles.Length; x++)
			{
				if (!Core.IsValidExtension(rgFiles[x].Extension))
					potentialLocation++;
			}
			return potentialLocation;
		}

		public int GetSelectedPhotoIndex()
        {
            return this.selectedThumbIndex;
        }

		public void UnloadImages()
		{
			foreach (ThumbnailControl thumb in this.thumbControls)
			{
				thumb.Deleting -= new EventHandler(thumb_Deleting);
				this.Controls.Remove(thumb);
			}
			this.thumbControls.Clear();
			if (this.selectedThumbIndex != -1)
			{
				this.selectedThumbIndex = -1;
				if (this.SelectionChanged != null)
					this.SelectionChanged(this, new EventArgs());
			}
			this.stopLoading = true;
		}

        public void LoadImages(ProgressControl progress)
        {
			if (String.IsNullOrEmpty(App.CurAppSettings.FolderSelectSettings.SourceFolder) 
				|| !Directory.Exists(App.CurAppSettings.FolderSelectSettings.SourceFolder))
				return;

			UnloadImages();
			this.stopLoading = false;

            progress.Show();
            progress.ProgressTitle = "Loading thumbnails";
            DirectoryInfo di = new DirectoryInfo(App.CurAppSettings.FolderSelectSettings.SourceFolder);
            FileInfo[] rgFiles = di.GetFiles("*.*");
            progress.Bar.Value = 0;
            progress.Bar.Maximum = rgFiles.Length;

            Image temp = null;
            foreach (FileInfo info in rgFiles)
            {
				if (this.stopLoading)
					break;

                if (Core.IsValidExtension(info.Extension))
                {
                    progress.Details = GetFileName(info.FullName);
                    Application.DoEvents();
                    this.thumbControls.Add(new ThumbnailControl(info.FullName, thumbSize, this));
                }
                progress.Bar.Increment(1);
				Application.DoEvents();
            }

            progress.Bar.Value = progress.Bar.Maximum;

            foreach (ThumbnailControl thumb in this.thumbControls)
            {
                this.Controls.Add(thumb);
                thumb.StartSelecting += new SelectingEventHandler(thumb_StartSelecting);
                thumb.MultipleSelecting += new SelectingEventHandler(thumb_MultipleSelecting);
                thumb.EndSelecting += new SelectingEventHandler(thumb_EndSelecting);
                thumb.SingleSelecting += new SelectingEventHandler(thumb_SingleSelecting);
				thumb.Deleting += new EventHandler(thumb_Deleting);
				thumb.RotatingToLeft += new EventHandler(thumb_RotatingToLeft);
				thumb.RotatingToRight += new EventHandler(thumb_RotatingToRight);
            }
			this.scrolled = true;
            RepositionThumbs();

            progress.Hide();
            this.Focus();

            if (this.ImagesLoaded != null)
                this.ImagesLoaded(this, new EventArgs());
        }

        public bool IsKeyDown(Keys key)
        {
            bool ret = GetAsyncKeyState((int)key) < 0;
            if (ret)
                System.Diagnostics.Debug.WriteLine("Key found");
            else
                System.Diagnostics.Debug.WriteLine("Key not found");
            return ret;
        }
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern short GetAsyncKeyState(int vkey);

		protected override void OnClick(EventArgs e)
		{
			base.OnClick(e);
			ClearSelection(null);
		}

		#region Thumb Events
		void thumb_RotatingToRight(object sender, EventArgs e)
		{
			RotatePhotos(90);
		}

		void thumb_RotatingToLeft(object sender, EventArgs e)
		{
			RotatePhotos(270);
		}

		private void RotatePhotos(int degrees)
		{
			int selectedIndex = this.selectedThumbIndex;
			this.selectedThumbIndex = -1;
			this.SelectionChanged(this, new EventArgs());

			foreach (ThumbnailControl thumb in this.SelectedThumbs)
			{
				thumb.PrepareForDirectEdit();
				Rotate(thumb.Path, degrees);
				thumb.ReloadImage();
			}

			this.selectedThumbIndex = selectedIndex;
			this.SelectionChanged(this, new EventArgs());
		}

		public void Rotate(string fileName, int degrees)
		{
			Image Pic;
			string FileNameTemp;
			System.Drawing.Imaging.Encoder Enc = System.Drawing.Imaging.Encoder.Transformation;
			EncoderParameters EncParms = new EncoderParameters(1);
			EncoderParameter EncParm;
			ImageCodecInfo CodecInfo = GetEncoderInfo("image/jpeg");

			// load the image to change 
			Pic = Image.FromFile(fileName);

			// we cannot store in the same image, so use a temporary image instead 
			FileNameTemp = fileName + ".temp";

			if (degrees == 90)
			{
				// for rewriting without recompression we must rotate the image 90 degrees
				EncParm = new EncoderParameter(Enc, (long)EncoderValue.TransformRotate90);
				EncParms.Param[0] = EncParm;
			}
			else if (degrees == 270 || degrees == -90)
			{
				// for rewriting without recompression we must rotate the image 90 degrees
				EncParm = new EncoderParameter(Enc, (long)EncoderValue.TransformRotate270);
				EncParms.Param[0] = EncParm;
			}

			// now write the rotated image with new description 
			Pic.Save(FileNameTemp, CodecInfo, EncParms);
			Pic.Dispose();
			Pic = null;
			GC.Collect();

			// delete the original file, will be replaced later 
			System.IO.File.Delete(fileName);
			System.IO.File.Move(FileNameTemp, fileName);
		}

		private static ImageCodecInfo GetEncoderInfo(String mimeType)
		{
			int j;
			ImageCodecInfo[] encoders;
			encoders = ImageCodecInfo.GetImageEncoders();
			for (j = 0; j < encoders.Length; ++j)
			{
				if (encoders[j].MimeType == mimeType)
					return encoders[j];
			}
			return null;
		}

		void thumb_Deleting(object sender, EventArgs e)
		{
			if (MessageBox.Show("Are you sure you want to delete the image(s)?", "Delete Images?", MessageBoxButtons.YesNo) == DialogResult.Yes)
			{
				try
				{
					this.selectedThumbIndex = -1;
					this.SelectionChanged(this, new EventArgs());
					foreach (ThumbnailControl thumb in this.SelectedThumbs)
					{
						thumb.PrepareForDirectEdit();
						Microsoft.VisualBasic.FileIO.FileSystem.DeleteFile(thumb.Path, Microsoft.VisualBasic.FileIO.UIOption.OnlyErrorDialogs,
							Microsoft.VisualBasic.FileIO.RecycleOption.SendToRecycleBin);
						this.Controls.Remove(thumb);
						this.thumbControls.Remove(thumb);
					}
					this.SelectedThumbs.Clear();
					RepositionThumbs();
				}
				catch (Exception ex)
				{
					System.Diagnostics.Debug.WriteLine(ex.Message);
				}
			}
		}

        void thumb_SingleSelecting(object sender, SelectingEventArgs e)
        {
            if (this.disregardSelectionEvents)
                return;

			bool rightClickAvoidClear = false;

            ThumbnailControl thumb = sender as ThumbnailControl;
            if (thumb != null && thumb.Selected)
            {
                if (this.selectedThumbIndex == -1 && this.thumbControls.IndexOf(thumb) != this.selectedThumbIndex)
                {
                    this.selectedThumbIndex = this.thumbControls.IndexOf(thumb);
                    FireSelectionChanged();
                    System.Diagnostics.Debug.WriteLine(String.Format("1 Selected: {0}", this.selectedThumbIndex));
                }

                //Only add the thumb if it's not already in our list
				if (!this.selectedThumbs.Contains(thumb))
				{
					this.selectedThumbs.Add(thumb);
				}
				else
				{
					if (thumb.RightMouseButton)
					{
						System.Diagnostics.Debug.WriteLine("Right-click down!!");
						rightClickAvoidClear = true;
					}
				}

                //Just a standard selection click, clear selection and set selected index
				if (!IsKeyDown(Keys.ControlKey) && !IsKeyDown(Keys.ShiftKey) && !rightClickAvoidClear)
                {
                    ClearSelection(thumb);
                    this.selectedThumbIndex = this.thumbControls.IndexOf(thumb);
                    FireSelectionChanged();
                    System.Diagnostics.Debug.WriteLine(String.Format("2 Selected: {0}", this.selectedThumbIndex));
                }
                else if (IsKeyDown(Keys.ControlKey))  //Control selecting, just make sure to update the last clicked index
                {
                    this.selectedThumbIndex = this.thumbControls.IndexOf(thumb);
                }
                else if (IsKeyDown(Keys.ShiftKey))  //Shift selecting, don't update last clicked index, select from low to high
                {
                    if (this.selectedThumbIndex != -1)
                    {
                        //Currently selected is different from last selected index
                        int currentlySelectedIndex = this.thumbControls.IndexOf(thumb);

                        System.Diagnostics.Debug.WriteLine(String.Format("Cur Selected: {0}", currentlySelectedIndex));
                        
                        int low = -1;
                        int high = -1;

                        //Do main work
                        GetLowHigh(currentlySelectedIndex, this.selectedThumbIndex, ref low, ref high);
                        SelectRangeOfThumbs(low, high);
                        RemoveSelectedThumbsOutOfRange(low, high);
                    }
                }
            }
            else //We're deselecting something
            {
                if (!IsKeyDown(Keys.ShiftKey))
                {
                    //We're not shift-selecting, but we might be using the control key;
                    //if we're not using the control key, then clear the selection and reset
                    //selected index
                    if (!IsKeyDown(Keys.ControlKey))
                    {
                        this.selectedThumbIndex = -1;
                        ClearSelection(null);
                    }
                }
                else if(!IsKeyDown(Keys.ControlKey))
                {
                    //The shift key is down, so we need to make sure that item is still selected
                    thumb.Selected = true;
                }
            }
		}

		void thumb_EndSelecting(object sender, SelectingEventArgs e)
		{
			if (this.mouseDown)
			{
				this.originalSelectionPoint = Point.Empty;
				Controls.Remove(selectionPanel);
				this.selectionPanel = null;
				this.mouseDown = false;
			}

		}

		void thumb_MultipleSelecting(object sender, SelectingEventArgs e)
		{
			if (this.selectionPanel != null && this.mouseDown)
			{
				int before = this.selectedThumbs.Count;
				SelectThumbs(this.selectionPanel.SelectRect);
				if (before != this.selectedThumbs.Count)
				{
					this.selectionPanel.Visible = false;
					this.selectionPanel.BackColor = Color.Azure;
					DrawToBitmap(this.contentBitmap, this.targetRect);
					this.selectionPanel.BackgroundBmp = this.contentBitmap;
					this.selectionPanel.Visible = true;
				}
				this.selectionPanel.Invalidate();
			}
			else if (this.mouseDown)
			{
				this.targetRect = Bounds;
				this.targetRect.X = 0;
				this.targetRect.Y = 0;
				DrawToBitmap(this.contentBitmap, this.targetRect);

				this.originScreen = PointToScreen(new Point(0, 0));

				selectionPanel = new SelectionPanel(this.contentBitmap, ClientRectangle, PointToClient(this.originalSelectionPoint), originScreen);
				Controls.Add(selectionPanel);
				selectionPanel.BringToFront();
				this.selectionPanel.Invalidate();
			}

		}

		void thumb_StartSelecting(object sender, SelectingEventArgs e)
		{
			this.originalSelectionPoint = e.SelectPoint;
			this.mouseDown = true;
		}
		#endregion

		private void FireSelectionChanged()
        {
            if (this.SelectionChanged != null)
                this.SelectionChanged(this, new EventArgs());
        }

        private void GetLowHigh(int index1, int index2, ref int low, ref int high)
        {
            if (index1 < index2)
            {
                low = index1;
                high = index2;
            }
            else
            {
                low = index2;
                high = index1;
            }
        }

        private void SelectRangeOfThumbs(int low, int high)
        {
            this.disregardSelectionEvents = true;
            //Select range of thumbs
            ThumbnailControl temp = null;
            for (int x = low; x <= high; x++)
            {
                if (x > this.thumbControls.Count)
                    break;

                temp = this.thumbControls[x];

                if (!this.selectedThumbs.Contains(temp))
                {
                    temp.Selected = true;
                    this.selectedThumbs.Add(temp);
                }
            }
            this.disregardSelectionEvents = false;
        }

        private void RemoveSelectedThumbsOutOfRange(int low, int high)
        {
            this.disregardSelectionEvents = true;
            //Remove the surplus selected thumbs
            int count = 0;
            while (high - low < this.selectedThumbs.Count && count < this.selectedThumbs.Count)
            {
                int index = this.thumbControls.IndexOf(this.selectedThumbs[count]);
                if (index < low || index > high)
                {
                    this.selectedThumbs[count].Selected = false;
                    this.selectedThumbs.RemoveAt(count);
                }
                else
                    count++;
            }
            this.disregardSelectionEvents = false;
        }

        private void ClearSelection(ThumbnailControl thumbnailControl)
        {
            this.disregardSelectionEvents = true;
            foreach (ThumbnailControl thumb in this.selectedThumbs)
            {
                if(thumbnailControl != thumb)
                    thumb.Selected = false;
            }

            if (thumbnailControl == null)
                this.selectedThumbs.Clear();
            else
            {
                int skipCount = 0;
                while (this.selectedThumbs.Count > 1 && skipCount < this.selectedThumbs.Count)
                {
                    if (this.selectedThumbs[skipCount] != thumbnailControl)
                        this.selectedThumbs.RemoveAt(skipCount);
                    else
                        skipCount++;
                }
            }
            this.disregardSelectionEvents = false;
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            this.Focus();
        }
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            thumb_StartSelecting(this, new SelectingEventArgs(PointToScreen(e.Location)));
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            thumb_MultipleSelecting(this, new SelectingEventArgs(PointToScreen(e.Location)));
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            thumb_EndSelecting(this, new SelectingEventArgs(PointToScreen(e.Location)));
        }        

        private void SelectThumbs(Rectangle rectangle)
        {
            this.disregardSelectionEvents = true;
			Rectangle thumbBounds = Rectangle.Empty;
            foreach (ThumbnailControl thumb in this.thumbControls)
            {
				thumbBounds = new Rectangle(
					thumb.Bounds.X + this.spacing / 2, 
					thumb.Bounds.Y + this.spacing / 2,
					thumb.Bounds.Width - this.spacing / 2, 
					thumb.Bounds.Height - this.spacing / 2);

				// Old code - it used to just check the corners
				//if (rectangle.Contains(thumb.Bounds.X + this.spacing / 2, thumb.Bounds.Y + this.spacing / 2) ||  //Top-Left
				//    rectangle.Contains(thumb.Bounds.X + thumb.Bounds.Width - this.spacing, thumb.Bounds.Y + this.spacing / 2) || //Top-Right
				//    rectangle.Contains(thumb.Bounds.X + this.spacing / 2, thumb.Bounds.Y + thumb.Bounds.Height - this.spacing / 2) ||  //Bottom-Left
				//    rectangle.Contains(thumb.Bounds.X + thumb.Bounds.Width - this.spacing / 2, thumb.Bounds.Y + thumb.Bounds.Height - this.spacing / 2)) //Bottom-Right

				if(rectangle.IntersectsWith(thumbBounds))
                {
                    if (!this.selectedThumbs.Contains(thumb))
                    {
                        thumb.Selected = true;
                        this.selectedThumbs.Add(thumb);
                    }
                }
                else
                {
                    if (this.selectedThumbs.Contains(thumb) && !IsKeyDown(Keys.ControlKey) && !IsKeyDown(Keys.ShiftKey))
                    {
                        thumb.Selected = false;
                        this.selectedThumbs.Remove(thumb);
                    }
                }
            }
            this.disregardSelectionEvents = false;
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            this.contentBitmap.Dispose();
            this.contentBitmap = new Bitmap(this.Bounds.Width, this.Bounds.Height);
        }

        private bool CountsInitialized()
        {
            if (this.thumbWidth != -1 && this.thumbHeight != -1 && this.widthCount != -1 && this.heightCount != -1)
                return true;
            else
                return false;
        }

        private void InitializeCounts()
        {
            if (!CountsInitialized())
            {
                this.thumbHeight = this.thumbSize.Height + this.spacing * 2;
                this.thumbWidth = this.thumbSize.Width + this.spacing * 2;
                this.widthCount = (int)((float)(this.Width - this.spacing) / ((float)thumbWidth));
                this.heightCount = (int)(Math.Ceiling((double)this.thumbControls.Count / (double)widthCount) * ThumbnailViewer.ScrollStops);
            }
        }

        private void RepositionThumbs()
        {

            //If there are no thumbnails, don't reposition any
            if (this.thumbControls.Count == 0)
                return;

            InitializeCounts();

            //Return if the number of pictures by width hasn't changed
            if (this.xCount != widthCount || this.maxVisibleRows != heightCount)
                this.xCount = widthCount;
            else if (!this.scrolled)
                return;

            int rowCount = 0;
            int offset = 0;
            if (this.vScrollBar1.Value > 0)
                offset = this.vScrollBar1.Value;
            else
                offset = this.vScrollBar1.Value;
            this.maxVisibleRows = this.Height / this.thumbHeight;
            Rectangle rect;
            rect = new Rectangle(this.spacing / 2,
                (int)((-((float)offset) * (float)this.thumbHeight) / (float)ScrollStops),
                this.thumbWidth, this.thumbHeight);
            this.yCount = 1;

            foreach (Control control in this.Controls)
            {
                if (control is ThumbnailControl)
                {
                    control.Bounds = rect;
                    rowCount++;
                    if (rowCount >= this.widthCount)
                    {
                        rect.X = this.spacing / 2;
                        rect.Y += this.thumbHeight;
                        rowCount = 0;
                        this.yCount++;
                    }
                    else
                    {
                        rect.X += this.thumbWidth;
                    }
                }
            }
            UpdateScrollBar();
        }

        private void UpdateScrollBar()
        {
            InitializeCounts();

            this.maxVisibleRows = (int)Math.Floor((double)this.Height / (double)thumbHeight);
            if (this.heightCount / ThumbnailViewer.ScrollStops > this.maxVisibleRows)
            {
                this.vScrollBar1.Visible = true;
                this.vScrollBar1.Maximum = heightCount-this.maxVisibleRows*ThumbnailViewer.ScrollStops;
                this.vScrollBar1.LargeChange = 1;
            }
            else
            {
                this.vScrollBar1.Visible = false;
            }
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            float oldVerticalScrollPercent = this.vScrollBar1.Value/this.vScrollBar1.Maximum;
            this.thumbWidth = -1;
            this.thumbHeight = -1;
            this.widthCount = -1;
            this.heightCount = -1;
            RepositionThumbs();
            Rectangle rect = this.vScrollBar1.Bounds;
            rect.X = this.Width - rect.Width;
            rect.Y = 60;
            rect.Height = this.Height - 60;
            this.vScrollBar1.Bounds = rect;
            this.vScrollBar1.Value = (int)(this.vScrollBar1.Maximum * oldVerticalScrollPercent);
        }

        private string GetFileName(string filename)
        {
            int right = FindPeriod(filename);
            int left = GetLastDirectoryCharacter(filename, right);
            return filename.Substring(left, right - left);
        }

        private int GetLastDirectoryCharacter(string path, int right)
        {
            int temp = path.Length - 1;
            while (path[temp] != '\\' && temp > 0)
                temp--;
            return temp;
        }

        private int FindPeriod(string path)
        {
            int temp = path.Length - 1;
            while (path[temp] != '.' && temp > 0)
                temp--;
            return temp;
        }

        private void OnScroll(object sender, ScrollEventArgs e)
        {
            this.scrolled = true;
            RepositionThumbs();
            this.scrolled = false;
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            base.OnMouseWheel(e);
            if (this.vScrollBar1.Visible)
            {
                if (e.Delta < 0 && this.vScrollBar1.Value < this.vScrollBar1.Maximum)
                    this.vScrollBar1.Value++;
                else if (e.Delta > 0 && this.vScrollBar1.Value > this.vScrollBar1.Minimum)
                    this.vScrollBar1.Value--;

                this.scrolled = true;
                RepositionThumbs();
                this.scrolled = false;
            }
        }
    }

    public class SelectionPanel : Panel 
    {   
         public SelectionPanel(Bitmap backgroundBitmap, Rectangle usableRect, Point mouseDownPos, Point offset) 
         {   
             DoubleBuffered = true;   
        
             this.backgroundBitmap = backgroundBitmap;   
             this.mouseDownPos = mouseDownPos;   
             this.usableRect = usableRect;
             this.offsetPos = offset;
        
             this.Size = usableRect.Size;   
             this.Location = new Point(0, 0);   
        
             selectionColor = Color.FromArgb(200, 0xE8, 0xED, 0xF5);   
             selectionBrush = new SolidBrush(selectionColor);   
             frameColor = Color.FromArgb(0x33, 0x5E, 0xA8);   
             framePen = new Pen(frameColor);   
         }

        public Bitmap BackgroundBmp
        {
            set
            {
                this.backgroundBitmap = value;
            }
        }

        public Rectangle SelectRect
        {
            get
            {
                return this.selectedRect;
            }
        }
        
         Bitmap backgroundBitmap;   
         Point mouseDownPos;
         Point offsetPos;
         Color selectionColor;   
         Brush selectionBrush;   
         Color frameColor;   
         Pen framePen;   
         Rectangle usableRect;
        Point mousePos;
        Rectangle selectedRect;
        
         protected override void OnPaint(PaintEventArgs e) 
		 {   
             // copy the backgroundImage   
             e.Graphics.DrawImage(backgroundBitmap, ClientRectangle, usableRect, GraphicsUnit.Pixel);   
        
             // Now, if needed, draw the selection rectangle   
             if (mouseDownPos != Point.Empty) {   
                 this.mousePos = PointToClient(MousePosition);
                 this.selectedRect = new Rectangle(   
                     Math.Min(mouseDownPos.X, mousePos.X),   
                     Math.Min(mouseDownPos.Y, mousePos.Y),   
                     Math.Abs(mousePos.X - mouseDownPos.X),   
                     Math.Abs(mousePos.Y - mouseDownPos.Y));   
                 e.Graphics.FillRectangle(selectionBrush, selectedRect);   
                 e.Graphics.DrawRectangle(framePen, selectedRect);

                 System.Diagnostics.Debug.WriteLine(String.Format(
                     "Orig: ({0},{1}) - Curr: ({2},{3})",
                     this.mouseDownPos.X, this.mouseDownPos.Y,
                     MousePosition.X, MousePosition.Y));
             }   
         }

		protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
		{
			return base.ProcessCmdKey(ref msg, keyData);
		}
     }   

}
