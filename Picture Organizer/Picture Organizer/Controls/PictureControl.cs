using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Trebuchet.Settings;
using System.IO;

namespace Trebuchet.Controls
{
    public partial class PictureControl : UserControl
    {
        private ThumbnailViewer thumbnailViewer = null;
        private Image selectedPicture = null;
        private Image resizePicture = null;
		private Image adjustedPicture = null;
		private Image pictureBoxImage = null;
		private FileStream selectedFile = null;
        private string picturePath = String.Empty;
		private string renameText = String.Empty;
        private bool subscribedToIdle = false;
        private bool loaded = false;
        private bool loadResized = false;
        private bool forceUpdate = false;
		private bool showOld = false;
		private bool wasInUse = false;
		private bool renameVisibility = false;
		private bool actualSizeVisibility = false;
        private int spacing = 5;
        private bool captureMouseEvents = false;
        private Rectangle watermarkBounds = Rectangle.Empty;
        private SizeF resizeRatios = SizeF.Empty;
        private SizeF reverseResizeRatios = SizeF.Empty;
        private Size originalOffset = Size.Empty;
		private BackgroundWorker picWorker = new BackgroundWorker();
		private Rectangle renameBounds = Rectangle.Empty;
		private Rectangle actualSizeBounds = Rectangle.Empty;
		private Rectangle autoUpdateBounds = Rectangle.Empty;
		private Rectangle refreshBounds = Rectangle.Empty;
		private Rectangle currentBounds = Rectangle.Empty;
		private Rectangle pictureBoxBounds = Rectangle.Empty;
		private bool loadingPicture = false;
		private Font loadingFontFore = new Font(FontFamily.GenericSansSerif, 18.0f);
		private SizeF loadingSize = SizeF.Empty;

		private int imageSet = 0;

        public PictureControl()
        {
            InitializeComponent();

            this.pictureBox1.BorderStyle = BorderStyle.FixedSingle;
            this.pictureBox1.Visible = false;

            App.CurAppSettings.RenameChanged += new EventHandler(CurAppSettings_RenameChanged);
            App.CurAppSettings.ResizeChanged += new EventHandler(CurAppSettings_ResizeChanged);
            App.CurAppSettings.WatermarkChanged += new EventHandler(CurAppSettings_WatermarkChanged);
			App.CurAppSettings.ImageSettingsChanged += new EventHandler(CurAppSettings_ImageSettingsChanged);

            this.pictureBox1.MouseDown += new MouseEventHandler(pictureBox1_MouseDown);
            this.pictureBox1.MouseMove += new MouseEventHandler(pictureBox1_MouseMove);
            this.pictureBox1.MouseUp += new MouseEventHandler(pictureBox1_MouseUp);
            this.pictureBox1.Paint += new PaintEventHandler(pictureBox1_Paint);

			this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer, true);

			this.picWorker.DoWork += new DoWorkEventHandler(picWorker_DoWork);
			this.picWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(picWorker_RunWorkerCompleted);

            this.loaded = true;
        }

		void CurAppSettings_ImageSettingsChanged(object sender, EventArgs e)
		{
			ActivateIdle();
		}

        void CurAppSettings_RenameChanged(object sender, EventArgs e)
        {
            ActivateIdle();
        }

        void CurAppSettings_ResizeChanged(object sender, EventArgs e)
        {
            this.loadResized = true;
            GetRidOfResizePicture();
            ActivateIdle();
        }

        void CurAppSettings_WatermarkChanged(object sender, EventArgs e)
        {
            ActivateIdle();
        }

        private void ActivateIdle()
        {
            if (this.loaded && !this.subscribedToIdle)
            {
                Application.Idle += new EventHandler(Application_Idle);
                this.subscribedToIdle = true;
            }
		}

        public ThumbnailViewer Viewer
        {
            set
            {
                UnsubscribeToViewerEvents();
                this.thumbnailViewer = value;
                SubscribeToViewerEvents();
            }
        }

        private void SubscribeToViewerEvents()
        {
            this.thumbnailViewer.SelectionChanged += new EventHandler(thumbnailViewer_SelectionChanged);
        }

        private void UnsubscribeToViewerEvents()
        {
            if(this.thumbnailViewer != null)
                this.thumbnailViewer.SelectionChanged -= thumbnailViewer_SelectionChanged;
        }

        void thumbnailViewer_SelectionChanged(object sender, EventArgs e)
        {
            string photoLocation = this.thumbnailViewer.GetSelectedPhotoLocation();
            App.TheCore.SelectedPhotoPath = photoLocation;
            LoadImage(photoLocation);
            ActivateIdle();
        }

        private void LoadImage(string path)
        {
            if (this.selectedPicture != null)
            {
                this.pictureBox1.Visible = false;
                this.selectedPicture.Dispose();
				CleanUpStream();
                this.picturePath = String.Empty;
            }

            if (!String.IsNullOrEmpty(path))
            {
				this.pictureBox1.Visible = false;
				try
				{
					CleanUpStream();
					this.loadingPicture = true;
					this.pictureBox1.Invalidate();
					this.selectedFile = new FileStream(path, FileMode.Open, FileAccess.Read);
					this.selectedPicture = Image.FromStream(this.selectedFile);
				}
				catch (Exception ex)
				{
					System.Diagnostics.Debug.WriteLine("Picture control error: " + ex.Message);
				}
                this.picturePath = path;
                Rectangle rect = GetAllowedPictureBounds();
                this.loadResized = true;
                GetRidOfResizePicture();
                ActivateIdle();
                this.pictureBox1.Visible = true;
            }
            else
            {
                this.pictureBox1.Visible = false;
				this.loadingPicture = false;
                this.pictureBox1.Image = null;
				IncrementImageSet();
                this.picturePath = String.Empty;
            }
		}

		private void CleanUpStream()
		{
			if (this.selectedFile != null)
			{
				this.selectedFile.Close();
				this.selectedFile.Dispose();
				this.selectedFile = null;
			}
		}

		private void IncrementImageSet()
		{
			System.Diagnostics.Debug.WriteLine("Image Set Count: " + this.imageSet.ToString());
			this.imageSet++;
		}

        private void GetRidOfResizePicture()
        {
            if (this.resizePicture != null)
            {
                this.resizePicture.Dispose();
                this.resizePicture = null;
            }
        }

        private string GetFileName()
        {
            if (this.picturePath.Length <= 0)
                return "";

            int lastSlash = -1;
            int period = -1;
            for (int x = 0; x < this.picturePath.Length; x++)
            {
                if (this.picturePath[x] == '\\')
                    lastSlash = x;
                else if (this.picturePath[x] == '.')
                    period = x;
            }

            return this.picturePath.Substring(lastSlash+1, period - lastSlash-1);
        }

        private void RepositionPicture()
        {
            Rectangle allowedRect = GetAllowedPictureBounds();
			if (this.pictureBoxImage == null)
			{
				this.pictureBoxBounds.Width = 0;
				this.pictureBoxBounds.Height = 0;
			}
			else
			{
				if (this.InvokeRequired)
				{
					this.pictureBoxBounds.Width = this.pictureBoxImage.Width;
					this.pictureBoxBounds.Height = this.pictureBoxImage.Height;
				}
				else
				{
				}
			}
			this.pictureBoxBounds.X = allowedRect.X + allowedRect.Width / 2 - this.pictureBoxBounds.Width / 2;
			this.pictureBoxBounds.Y = allowedRect.Y + allowedRect.Height / 2 - this.pictureBoxBounds.Height / 2;
			if (this.pictureBoxBounds.X < this.spacing)
				this.pictureBoxBounds.X = this.spacing;
			if (this.pictureBoxBounds.Y < this.spacing)
				this.pictureBoxBounds.Y = this.spacing;
        }

        private Rectangle GetAllowedPictureBounds()
        {
            Rectangle rect = this.Bounds;
            rect.X += this.spacing*2;
            rect.Y += this.spacing;
            rect.Width -= this.spacing*4;
            if (App.CurAppSettings.RenameMode)
            {
                rect.Height = rect.Height - this.renameBounds.Height - this.spacing*4;
                this.renameVisibility = true;
            }
            else
            {
                rect.Height -= this.spacing*2;
                this.renameVisibility = false;
            }
            return rect;
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            if (this.thumbnailViewer == null)
                return;

			PreparePositioning();
            if (this.chkAutoUpdate.Checked)
            {
                Rectangle allowedRect = GetAllowedPictureBounds();
                if (this.selectedPicture != null)
                {
                    Rectangle resizePic = GetResizedBounds();
                    if (resizePic.Width != this.pictureBox1.Width || resizePic.Height != this.pictureBox1.Height)
                    {
                        Image image = this.pictureBox1.Image;
						if (IsResizeNeeded(allowedRect))
						{
							this.loadingPicture = false;
							this.pictureBox1.Image = App.TheCore.AutoScaleByBound(ref image, allowedRect.Width, allowedRect.Height);
							this.pictureBoxImage = this.pictureBox1.Image;
							this.pictureBoxBounds.Width = this.pictureBoxImage.Width;
							this.pictureBoxBounds.Height = this.pictureBoxImage.Height;
							IncrementImageSet();
						}
                        if (!subscribedToIdle)
                        {
                            Application.Idle += new EventHandler(Application_Idle);
                            this.subscribedToIdle = true;
                        }
                    }
                    RepositionPicture();
                }

                RepositionEffects(false);
            }
            else
			{
				PreparePositioning();
				RepositionPicture();
            }
            RepositionBottomControls();
			if (!this.InvokeRequired)
			{
				FinalizePositioning();
			}

            if (App.CurAppSettings.WatermarkSettings.Location == Watermark.WatermarkLocation.Custom)
            {
                InitializeCustomRatios();
                Invalidate();
            }
        }

        void Application_Idle(object sender, EventArgs e)
        {
			if (!this.picWorker.IsBusy)
			{
				PreparePositioning();
				this.picWorker.RunWorkerAsync();
			}
		}

		void picWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			FinalizePositioning();
			Application.Idle -= Application_Idle;
			this.subscribedToIdle = false;
		}

		private void PreparePositioning()
		{
			this.renameBounds = this.lblRename.Bounds;
			this.actualSizeBounds = this.lblActualSize.Bounds;
			this.autoUpdateBounds = this.chkAutoUpdate.Bounds;
			this.refreshBounds = this.btnRefresh.Bounds;
			this.currentBounds = this.Bounds;
			this.pictureBoxImage = this.pictureBox1.Image;
			this.pictureBoxBounds = this.pictureBox1.Bounds;
			this.actualSizeVisibility = this.lblActualSize.Visible;
		}

		private void FinalizePositioning()
		{
			this.loadingPicture = false;
			this.pictureBox1.Image = this.pictureBoxImage;
			this.lblRename.Visible = this.renameVisibility;
			this.lblRename.Text = this.renameText;
			this.lblRename.Bounds = this.renameBounds;
			this.lblActualSize.Bounds = this.actualSizeBounds;
			this.chkAutoUpdate.Bounds = this.autoUpdateBounds;
			this.btnRefresh.Bounds = this.refreshBounds;
			this.pictureBox1.Bounds = this.pictureBoxBounds;
			this.lblActualSize.Visible = this.actualSizeVisibility;
		}

		void picWorker_DoWork(object sender, DoWorkEventArgs e)
		{
			if (this.selectedPicture != null)
			{
				Rectangle allowedRect = GetAllowedPictureBounds();
				Image image = GetAppropriatePicture(true);
				if (image != null)
					image = image.Clone() as Image;

				if (!App.CurAppSettings.WatermarkMode)
				{
					if (IsResizeNeeded(allowedRect))
					{
						this.pictureBoxImage = App.TheCore.AutoScaleByBound(ref image, allowedRect.Width, allowedRect.Height);
						IncrementImageSet();
					}
					else
					{
						this.pictureBoxImage = image;
						IncrementImageSet();
					}
				}
				RepositionBottomControls();
				RepositionEffects(true);
				RepositionPicture();
			}

			if (this.forceUpdate)
				this.forceUpdate = false;
		}

        private bool IsResizeNeeded(Rectangle allowedRect)
        {
            bool ret = false;
            Rectangle picture = Rectangle.Empty;
            if (App.CurAppSettings.ResizeMode && this.resizePicture != null)
            {
                picture = new Rectangle(0, 0, this.resizePicture.Width, this.resizePicture.Height);
            }
            else
            {
                picture = new Rectangle(0, 0, this.selectedPicture.Width, this.selectedPicture.Height);
            }

            if (picture.Width < allowedRect.Width && picture.Height < allowedRect.Height)
            {
                ret = false;
            }
            else
            {
                ret = true;
            }

            this.actualSizeVisibility = !ret;

            return ret;
        }

        private void RepositionBottomControls()
        {
            //Update rename text
            if ((this.forceUpdate || this.chkAutoUpdate.Checked) && App.CurAppSettings.RenameMode)
            {
                this.renameText = "Renaming: " + GetFileName() + " -> " + App.TheCore.GetFileName(this.thumbnailViewer.GetSelectedPhotoIndex());
            }

            //Reposition name
            Rectangle rect = this.renameBounds;
            rect.X = this.currentBounds.Width / 2 - rect.Width / 2;
			rect.Y = this.currentBounds.Height - rect.Height - this.spacing;
            this.renameBounds = rect;

            //Reposition Actual Size label
            rect = this.actualSizeBounds;
			rect.X = this.currentBounds.Width - rect.Width - this.spacing;
			rect.Y = this.currentBounds.Height - rect.Height - this.spacing;
            this.actualSizeBounds = rect;

            //Reposition Auto-update checkbox
            rect = this.autoUpdateBounds;
            rect.X = this.spacing*2;
			rect.Y = this.currentBounds.Height - rect.Height - this.spacing;
            this.autoUpdateBounds = rect;

            //Reposition Refresh Button
            rect = this.refreshBounds;
            rect.X = this.autoUpdateBounds.X + this.autoUpdateBounds.Width + this.spacing;
			rect.Y = this.currentBounds.Height - rect.Height + (rect.Height - this.autoUpdateBounds.Height) / 2 - this.spacing;
            this.refreshBounds = rect;
        }

        private Rectangle GetResizedBounds()
        {
            Rectangle allowedRect = GetAllowedPictureBounds();
            Rectangle rect = Rectangle.Empty;
            rect.Width = allowedRect.Width;
            rect.Height = allowedRect.Height;
			Image image = GetAppropriatePicture(false);

			if (image == null)
				return rect;

            float imagePercent = (float)image.Width / (float)image.Height;
            float boundPercent = (float)allowedRect.Width / (float)allowedRect.Height;

            if (imagePercent > boundPercent)
            {
                rect.Height = (int)((float)allowedRect.Width / (float)image.Width * (float)image.Height);
            }
            else
            {
				rect.Width = (int)((float)allowedRect.Height / (float)image.Height * (float)image.Width);
            }
            return rect;
        }

        private Image GetAppropriatePicture(bool getNewPicture)
        {
			Image image = null;
			if (!this.showOld && (this.wasInUse || App.CurAppSettings.ImageAdjSettings.CurrentEffect.InUse))
			{
				if (getNewPicture)
				{
					image = (Image)this.selectedPicture.Clone();
					image = App.TheCore.ApplyMatrix(image);
					if (App.CurAppSettings.ResizeMode)
					{
						this.adjustedPicture = App.TheCore.ResizePic(ref image, true);
						this.resizePicture = this.adjustedPicture;
					}
					else
					{
						this.adjustedPicture = image;
					}
				}
				if (App.CurAppSettings.ImageAdjSettings.CurrentEffect.InUse)
					this.wasInUse = true;
				else
					this.wasInUse = false;
				return this.adjustedPicture;
			}
            else if (App.CurAppSettings.ResizeMode)
            {
				if (getNewPicture && (this.resizePicture == null || this.showOld))
                {
                    image = this.selectedPicture;
					if (image != null)
						image = image.Clone() as Image;

                    this.resizePicture = App.TheCore.ResizePic(ref image, true);
                }
                return this.resizePicture;
            }
            else
                return this.selectedPicture;
        }

        private void RepositionEffects(bool idle)
        {
            if (this.selectedPicture == null)
                return;

            Rectangle allowedRect = GetAllowedPictureBounds();
            if (App.CurAppSettings.ResizeMode)
            {
                if (this.resizePicture == null)
                {
                    this.resizePicture = App.TheCore.ResizePic(ref this.selectedPicture, true);
                    Image image = (Image)GetAppropriatePicture(true).Clone();
                    if (IsResizeNeeded(allowedRect))
                        this.pictureBoxImage = App.TheCore.AutoScaleByBound(ref image, allowedRect.Width, allowedRect.Height);
                    else
						this.pictureBoxImage = image;
					IncrementImageSet();
                }
            }
            if (App.CurAppSettings.WatermarkMode)
            {
                if (idle)
                {
                    Image image = (Image)GetAppropriatePicture(true).Clone();
                    image = App.TheCore.WatermarkPic(image);
                    if (IsResizeNeeded(allowedRect))
                        this.pictureBoxImage = App.TheCore.AutoScaleByBound(ref image, allowedRect.Width, allowedRect.Height);
                    else
						this.pictureBoxImage = image;
					IncrementImageSet();
                }
                InitializeCustomRatios();
                this.pictureBox1.Invalidate();
            }
        }

        private void chkAutoUpdate_CheckedChanged(object sender, EventArgs e)
        {
            this.btnRefresh.Visible = !this.chkAutoUpdate.Checked;
            if (this.chkAutoUpdate.Checked)
            {
                App.CurAppSettings.RenameChanged += new EventHandler(CurAppSettings_RenameChanged);
                App.CurAppSettings.ResizeChanged += new EventHandler(CurAppSettings_ResizeChanged);
                App.CurAppSettings.WatermarkChanged += new EventHandler(CurAppSettings_WatermarkChanged);
                ActivateIdle();
            }
            else
            {
                App.CurAppSettings.RenameChanged -= CurAppSettings_RenameChanged;
                App.CurAppSettings.ResizeChanged -= CurAppSettings_ResizeChanged;
                App.CurAppSettings.WatermarkChanged -= CurAppSettings_WatermarkChanged;
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            this.forceUpdate = true;
            ActivateIdle();
        }

        void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
			if (App.CurAppSettings.WatermarkSettings.Location == Watermark.WatermarkLocation.Custom)
			{
				InitializeCustomRatios();
				Point currentPos = new Point(e.X, e.Y);
				if (this.watermarkBounds.Contains(currentPos))
				{
					this.captureMouseEvents = true;
					Point watermarkPos = ConvertWatermarkMousePosition(App.CurAppSettings.ConvertFromPointString(App.CurAppSettings.WatermarkSettings.CustomLocation));
					this.originalOffset.Width = currentPos.X - watermarkPos.X;
					this.originalOffset.Height = currentPos.Y - watermarkPos.Y;
				}
				else if (!this.showOld)
				{
					this.showOld = true;
					ActivateIdle();
					IncrementImageSet();
				}
			}
			else if (!this.showOld)
			{
				this.showOld = true;
				ActivateIdle();
				IncrementImageSet();
			}
        }

        void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (this.captureMouseEvents)
            {
                Point point = Point.Empty;
                point.X = e.X - this.originalOffset.Width;
                point.Y = e.Y - this.originalOffset.Height;

                if (point.X < 0)
                    point.X = 0;
                if (point.Y < 0)
                    point.Y = 0;
                if (point.X + this.watermarkBounds.Width > this.pictureBox1.Width)
                    point.X = this.pictureBox1.Width - this.watermarkBounds.Width;
                if (point.Y + this.watermarkBounds.Height > this.pictureBox1.Height)
                    point.Y = this.pictureBox1.Height - this.watermarkBounds.Height;

                this.watermarkBounds.X = point.X;
                this.watermarkBounds.Y = point.Y;
                App.CurAppSettings.WatermarkSettings.CustomLocation = App.CurAppSettings.ConvertToPointString(
                    new Point((int)(point.X * reverseResizeRatios.Width), (int)(point.Y * reverseResizeRatios.Height)));
                ActivateIdle();
                this.pictureBox1.Invalidate();
            }
        }

        void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            this.pictureBox1.Invalidate();
            this.captureMouseEvents = false;
			if (this.showOld)
			{
				this.showOld = false;
				this.forceUpdate = true;
				ActivateIdle();
			}
        }

        void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            if (App.CurAppSettings.WatermarkSettings.Location == Watermark.WatermarkLocation.Custom)
            {
                e.Graphics.DrawRectangle(Pens.LimeGreen, this.watermarkBounds);
            }

			if (this.loadingPicture)
			{
				if (this.loadingSize == SizeF.Empty)
					this.loadingSize = e.Graphics.MeasureString("Loading...", this.loadingFontFore);
				e.Graphics.DrawString("Loading...", this.loadingFontFore, Brushes.Black, new PointF(16.0f, this.pictureBox1.Height-this.loadingSize.Height-16.0f));
				e.Graphics.DrawString("Loading...", this.loadingFontFore, Brushes.LimeGreen, new PointF(15.0f, this.pictureBox1.Height - this.loadingSize.Height - 15.0f));
			}
        }

        private void InitializeCustomRatios()
        {
            Rectangle pictureBounds = this.pictureBox1.Bounds;
            Rectangle watermarkRect = App.TheCore.GetWatermarkBounds();
            if (this.resizePicture != null)
            {
                this.resizeRatios.Width = (float)this.pictureBox1.Bounds.Width / (float)this.resizePicture.Width;
                this.resizeRatios.Height = (float)this.pictureBox1.Bounds.Height / (float)this.resizePicture.Height;
                this.reverseResizeRatios.Width = (float)this.resizePicture.Width / (float)this.pictureBox1.Bounds.Width;
                this.reverseResizeRatios.Height = (float)this.resizePicture.Height / (float)this.pictureBox1.Bounds.Height;
            }
            else
            {
                this.resizeRatios.Width = (float)this.pictureBox1.Bounds.Width / (float)this.selectedPicture.Width;
                this.resizeRatios.Height = (float)this.pictureBox1.Bounds.Height / (float)this.selectedPicture.Height;
                this.reverseResizeRatios.Width = (float)this.selectedPicture.Width / (float)this.pictureBox1.Bounds.Width;
                this.reverseResizeRatios.Height = (float)this.selectedPicture.Height / (float)this.pictureBox1.Bounds.Height;
            }
            this.watermarkBounds.X = (int)(watermarkRect.X * resizeRatios.Width);
            this.watermarkBounds.Y = (int)(watermarkRect.Y * resizeRatios.Height);
            this.watermarkBounds.Width = (int)(watermarkRect.Width * resizeRatios.Width);
            this.watermarkBounds.Height = (int)(watermarkRect.Height * resizeRatios.Height);
        }

        private Point ConvertWatermarkMousePosition(Point originalPoint)
        {
            return new Point((int)(originalPoint.X * this.resizeRatios.Width), (int)(originalPoint.Y * this.resizeRatios.Height));  
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

        }
    }
}
