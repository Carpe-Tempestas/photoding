using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using Microsoft.VisualBasic;

namespace Trebuchet.Controls
{
    public partial class ThumbnailControl : UserControl
    {
        private int spacing = 10;
        private bool selected = false;
        private bool mouseOver = false;
        private bool selectionChanged = false;
        public event SelectingEventHandler SingleSelecting;
        public event SelectingEventHandler MultipleSelecting;
        public event SelectingEventHandler StartSelecting;
        public event SelectingEventHandler EndSelecting;
		public event EventHandler Deleting;
		public event EventHandler RotatingToLeft;
		public event EventHandler RotatingToRight;
        private GraphicsPath path = null;
        private PathGradientBrush selectedBrush = null;
        private PathGradientBrush mouseOverBrush = null;
        private Color[] selectedColors;
        private Color[] mouseOverColors;
        private ColorBlend selectedBlend;
        private ColorBlend mouseOverBlend;
        private float[] positions;
        private Rectangle rectBoxLightGray;
        private Rectangle rectBoxDarkGray;
        private bool sizeSet = false;
		private BackgroundWorker imageLoader = new BackgroundWorker();
		private Image pictureImage = null;
		private string imagePath = String.Empty;
		private Size thumbSize = Size.Empty;
		private bool rightMouseButton = false;
		private ThumbnailViewer parent = null;

        public ThumbnailControl(string path, Size thumb, ThumbnailViewer initParent)
        {
            InitializeComponent();
			this.imagePath = path;
			this.thumbSize = thumb;
			this.DoubleBuffered = true;
			this.parent = initParent;

			this.pictureBox1.MouseDown += new MouseEventHandler(pictureBox1_MouseDown);
			this.pictureBox1.MouseMove += new MouseEventHandler(pictureBox1_MouseMove);
			this.pictureBox1.MouseUp += new MouseEventHandler(pictureBox1_MouseUp);
			this.pictureBox1.MouseEnter += new EventHandler(pictureBox1_MouseEnter);
			this.pictureBox1.MouseLeave += new EventHandler(pictureBox1_MouseLeave);

			this.pictureImage = this.pictureBox1.Image;
			InitializeImage();

			this.imageLoader.DoWork += new DoWorkEventHandler(imageLoader_DoWork);
			this.imageLoader.RunWorkerCompleted += new RunWorkerCompletedEventHandler(imageLoader_RunWorkerCompleted);
			this.imageLoader.RunWorkerAsync();

        }

		public void ReloadImage()
		{
			this.imageLoader.RunWorkerAsync();
		}

		private void imageLoader_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			InitializeImage();
		}

		private void InitializeImage()
		{
			this.pictureBox1.Image = this.pictureImage;
			Rectangle origRect = this.pictureBox1.Bounds;
			Rectangle rect = this.pictureBox1.Bounds;
			rect.Width = this.pictureImage.Width;
			rect.Height = this.pictureImage.Height;
			rect.X = this.Width / 2 - rect.Width / 2;
			rect.Y = this.Height / 2 - rect.Height / 2;
			this.pictureBox1.Bounds = rect;
			this.pictureBox1.BorderStyle = BorderStyle.None;
			SetSize();
			this.rectBoxLightGray = rect;
			this.rectBoxLightGray.X -= this.spacing / 2;
			this.rectBoxLightGray.Y -= this.spacing / 2;
			this.rectBoxLightGray.Width += this.spacing;
			this.rectBoxLightGray.Height += this.spacing;
			this.rectBoxDarkGray = this.rectBoxLightGray;
			this.rectBoxDarkGray.Width += 1;
			this.rectBoxDarkGray.Height += 1;

			this.Invalidate();
			this.pictureBox1.Invalidate();
		}

		void imageLoader_DoWork(object sender, DoWorkEventArgs e)
		{
			if (!this.parent.StopLoading)
			{
				this.pictureImage = Image.FromFile(this.imagePath);
			}
			if (!this.parent.StopLoading)
			{
				this.pictureImage = App.TheCore.AutoScaleByBound(ref this.pictureImage, this.thumbSize.Width, this.thumbSize.Height);
			}
		}

        public Image Thumbnail
        {
            set
            {
                this.pictureBox1.Image = value;
            }
        }

        public int Spacing
        {
            set
            {
                this.spacing = value;
            }
        }

		public string Path
		{
			get
			{
				return this.imagePath;
			}
		}

        public bool Selected
        {
            get
            {
                return this.selected;
            }
            set
            {
                ChangeSelection(value);
                this.selected = value;
            }
        }

        public Size ThumbnailSize
        {
            get
            {
                return this.Size;
            }
            set
            {
                this.Size = value;
            }
        }

		public bool RightMouseButton
		{
			get
			{
				return this.rightMouseButton;
			}
		}

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            ChangeSelection();
        }

        private void ChangeSelection()
        {
            if (this.SingleSelecting != null)
            {
                this.selected = true;
                this.selectionChanged = true;
                Invalidate();
                SelectingEventArgs args = new SelectingEventArgs(this.selected);
                this.SingleSelecting(this, args);
            }
            else
            {
                this.selected = true;
                this.selectionChanged = true;
                Invalidate();
            }
        }

        private void ChangeSelection(bool selection)
        {
            if (selection == this.selected)
                return;

            ChangeSelection();
        }

        private void SetSelected()
        {
            this.selectedBrush = new PathGradientBrush(path);
            this.selectedBrush.CenterPoint = new PointF(this.Bounds.Width / 2.0f, this.Bounds.Height / 2.0f);
            this.selectedBrush.CenterColor = Color.Green;
            this.selectedColors = new Color[3]{
                   Color.Transparent,
                   Color.DodgerBlue,
                    Color.White};
            this.selectedBrush.SurroundColors = this.selectedColors;
            this.positions = new float[]{
                    0.0f, 
                    1-((float)this.spacing*2)/(float)this.Width, 
                    1.0f};
            this.selectedBlend = new ColorBlend(3);
            this.selectedBlend.Colors = this.selectedColors;
            this.selectedBlend.Positions = this.positions;
            this.selectedBrush.InterpolationColors = this.selectedBlend;
        }

        private void SetMouseOver()
        {
            this.mouseOverBrush = new PathGradientBrush(path);
            this.mouseOverBrush.CenterPoint = new PointF(this.Bounds.Width / 2.0f, this.Bounds.Height / 2.0f);
            this.mouseOverBrush.CenterColor = Color.Green;
            this.mouseOverColors = new Color[3]{
                   Color.Transparent,
                   Color.LightBlue,
                    Color.White};
            this.mouseOverBrush.SurroundColors = this.mouseOverColors;
            this.positions = new float[]{
                    0.0f, 
                    1-((float)this.spacing*2)/(float)this.Width, 
                    1.0f};
            this.mouseOverBlend = new ColorBlend(3);
            this.mouseOverBlend.Colors = this.mouseOverColors;
            this.mouseOverBlend.Positions = this.positions;
            this.mouseOverBrush.InterpolationColors = this.mouseOverBlend;
        }

        private void SetSize()
        {
            this.path = new GraphicsPath();
            Rectangle rect = this.Bounds;
            rect.X = 0;
            rect.Y = 0;
            float radius = 10;
            this.path.StartFigure();
            this.path.AddArc(rect.X, rect.Y, radius, radius, 180, 90);
            this.path.AddArc(rect.X + rect.Width - radius, rect.Y, radius, radius, 270, 90);
            this.path.AddArc(rect.X + rect.Width - radius, rect.Y + rect.Height - radius, radius, radius, 0, 90);
            this.path.AddArc(rect.X, rect.Y + rect.Height - radius, radius, radius, 90, 90);
            this.path.CloseFigure();
            SetSelected();
            SetMouseOver();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            //if (this.selectionChanged)
            //{
                DrawSelection(e.Graphics);
            //   this.selectionChanged = false;
            //}
        }

        private void DrawSelection(Graphics g)
        {
            if (this.selected)
            {
                g.FillPath(this.selectedBrush, this.path);
                DrawRoundedRectangle(g, rectBoxLightGray, 10, Pens.LightGray);
            }
            else if (this.mouseOver)
            {
                g.FillPath(this.mouseOverBrush, this.path);
                DrawRoundedRectangle(g, rectBoxLightGray, 10, Pens.LightGray);
            }
            else
            {
                g.FillRectangle(Brushes.White, this.Bounds);
                DrawRoundedRectangle(g, rectBoxLightGray, 10, Pens.LightGray);
            }
        }

        public static void DrawRoundedRectangle(Graphics g, Rectangle r, int d, Pen p)
        {

            System.Drawing.Drawing2D.GraphicsPath gp = new System.Drawing.Drawing2D.GraphicsPath();

            gp.AddArc(r.X, r.Y, d, d, 180, 90);
            gp.AddArc(r.X + r.Width - d, r.Y, d, d, 270, 90);
            gp.AddArc(r.X + r.Width - d, r.Y + r.Height - d, d, d, 0, 90);
            gp.AddArc(r.X, r.Y + r.Height - d, d, d, 90, 90);
            gp.AddLine(r.X, r.Y + r.Height - d, r.X, r.Y + d / 2);

            g.DrawPath(p, gp);
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            if (!this.sizeSet)
            {
                SetSize();
                this.sizeSet = true;
            }

            Rectangle rect = this.pictureBox1.Bounds;
            rect.X = this.spacing;
            rect.Y = this.spacing;
            rect.Width = this.Width - this.spacing*2;
            rect.Height = this.Height - this.spacing*2;
            this.pictureBox1.Bounds = rect;
            this.selectionChanged = true;
            Invalidate();
        }

        private void OnPictureClicked(object sender, EventArgs e)
        {
			MouseEventArgs mouseArgs = e as MouseEventArgs;
			if (mouseArgs != null && mouseArgs.Button == MouseButtons.Right)
			{
				this.rightMouseButton = true;
			}
			else
			{
				this.rightMouseButton = false;
			}
            ChangeSelection();
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            this.mouseOver = true;
            Invalidate();
            this.Parent.Focus();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            this.mouseOver = false;
            Invalidate();
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (this.StartSelecting != null)
                this.StartSelecting(this, new SelectingEventArgs(PointToScreen(e.Location)));

        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (this.MultipleSelecting != null)
                this.MultipleSelecting(this, new SelectingEventArgs(PointToScreen(e.Location)));
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            if (this.EndSelecting != null)
				this.EndSelecting(this, new SelectingEventArgs(PointToScreen(e.Location)));

			if (e.Button == MouseButtons.Right)
			{
				Rectangle menuBounds = this.contextMenuStrip1.Bounds;
				Point menuPoint = PointToScreen(new Point(e.X, e.Y));
				menuBounds.X = menuPoint.X;
				menuBounds.Y = menuPoint.Y;
				this.contextMenuStrip1.Show();
				this.contextMenuStrip1.Bounds = menuBounds;
			}
        }

        void pictureBox1_MouseEnter(object sender, EventArgs e)
        {
            this.mouseOver = true;
            Invalidate();
        }

        void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            this.mouseOver = false;
            Invalidate();
        }

        void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            Point point = e.Location;
			point.X += this.pictureBox1.Bounds.X;
			point.Y += this.pictureBox1.Bounds.Y;
			if (this.EndSelecting != null)
				this.EndSelecting(this, new SelectingEventArgs(PointToScreen(point)));

			if (e.Button == MouseButtons.Right)
			{
				Rectangle menuBounds = this.contextMenuStrip1.Bounds;
				Point menuPoint = PointToScreen(new Point(point.X, point.Y));
				menuBounds.X = menuPoint.X;
				menuBounds.Y = menuPoint.Y;
				this.contextMenuStrip1.Show();
				this.contextMenuStrip1.Bounds = menuBounds;
			}
        }

        void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            Point point = e.Location;
            point.X += this.pictureBox1.Bounds.X;
            point.Y += this.pictureBox1.Bounds.Y;
            if (this.MultipleSelecting != null)
                this.MultipleSelecting(this, new SelectingEventArgs(PointToScreen(point)));
        }

        void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            Point point = e.Location;
            point.X += this.pictureBox1.Bounds.X;
            point.Y += this.pictureBox1.Bounds.Y;
            if (this.StartSelecting != null)
                this.StartSelecting(this, new SelectingEventArgs(PointToScreen(point)));
        }

		private void rotateToRightToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (this.RotatingToRight != null)
				this.RotatingToRight(this, new EventArgs());
		}

		private void rotateToLeftToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (this.RotatingToLeft != null)
				this.RotatingToLeft(this, new EventArgs());
		}

		public void PrepareForDirectEdit()
		{
			this.pictureImage.Dispose();
			this.pictureBox1.Image.Dispose();
			GC.Collect();
		}

		private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (this.Deleting != null)
				this.Deleting(this, new EventArgs());
		}

		protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
		{
			if (keyData == Keys.Delete && this.Deleting != null)
				this.Deleting(this, new EventArgs());

			return base.ProcessCmdKey(ref msg, keyData);
		}
    }
    public partial class SelectingEventArgs : EventArgs
    {
        private Point point = Point.Empty;
        private bool status = false;
        public SelectingEventArgs(Point selectionPoint)
        {
            this.point = selectionPoint;
        }

        public SelectingEventArgs(bool selectionStatus)
        {
            this.status = selectionStatus;
        }

        public Point SelectPoint
        {
            get
            {
                return this.point;
            }
        }

        public bool SelectionStatus
        {
            get
            {
                return this.status;
            }
        }
    }

    public delegate void SelectingEventHandler(object sender, SelectingEventArgs e);

	class Win32ApiFunctions
	{
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto, Pack = 1)]
		public struct SHFILEOPSTRUCT
		{
			public IntPtr hwnd;
			[MarshalAs(UnmanagedType.U4)]
			public int wFunc;
			public string pFrom;
			public string pTo;
			public short fFlags;
			[MarshalAs(UnmanagedType.Bool)]
			public bool fAnyOperationsAborted;
			public IntPtr hNameMappings;
			public string lpszProgressTitle;
		}
		
		[DllImport("shell32.dll", CharSet = CharSet.Auto)]
		static extern int SHFileOperation(ref SHFILEOPSTRUCT FileOp);
		const int FO_DELETE = 3;
		const int FOF_ALLOWUNDO = 0x40;
		const int FOF_NOCONFIRMATION = 0x10; //Don't prompt the user.; 

		public static void DeleteFilesToRecycleBin(string filename)
		{
			SHFILEOPSTRUCT shf = new SHFILEOPSTRUCT();
			shf.wFunc = FO_DELETE;
			shf.fFlags = FOF_ALLOWUNDO | FOF_NOCONFIRMATION;
			shf.pFrom = filename + "\0";

			int result = SHFileOperation(ref shf);
			if (result != 0)
				Console.WriteLine(string.Format("error: {0} while moving file {1} to recycle bin", result, filename));
		}
	}
}
