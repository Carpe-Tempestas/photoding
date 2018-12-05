using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Trebuchet.Settings.WatermarkTypes;
using Trebuchet.Settings;

namespace Trebuchet.Controls
{
	public partial class WatermarkGraphicControl : TrebUserControl
    {
        private bool initializing = false;
        private bool preventSizeUpdate = false;
        private bool preventCropUpdate = false;
        private SizeF lastGraphicRatio = Size.Empty;
        private ToolTip keyColorTip = new ToolTip();
        private Size frequencySize = Size.Empty;

        public WatermarkGraphicControl()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!this.DesignMode)
            {
                Initialize();
                this.keyColorTip.SetToolTip(this.btnGraphicKey, "Click to select a background color to make transparent.");
            }
		}

		private WatermarkControl GetParentWatermarkControl()
		{
			Control control = this;
			WatermarkControl watControl = control as WatermarkControl;
			while (watControl == null)
			{
				control = control.Parent;
				watControl = control as WatermarkControl;
			}
			return watControl;
		}

        public bool CanContinue()
        {
            if (String.IsNullOrEmpty(this.txtGraphicLocation.Text))
                return false;
            else
                return true;
        }

        private void BrowseForGraphic()
        {
            OpenFileDialog dlgOpen = new OpenFileDialog();
            dlgOpen.InitialDirectory = this.txtGraphicLocation.Text;
            dlgOpen.Filter = "Image files|*.jpg;*.png;*.gif;*.bmp;*.tiff;*.tif;*.ico|All files (*.*)|*.*";
            if (dlgOpen.ShowDialog() == DialogResult.OK)
            {
                this.txtGraphicLocation.Text = dlgOpen.FileName;
                LoadGraphic();
                UpdateCurAppSettings();
				WatermarkControl parent = GetParentWatermarkControl();
				if (parent != null)
				{
					parent.UpdateFrequencyBounds();
				}
            }
        }

        private void LoadGraphic()
        {
            Bitmap bmp = new Bitmap(this.txtGraphicLocation.Text);
            this.picBoxWatermark.Image = ResizeBitmap(bmp, this.picBoxWatermark.Width, this.picBoxWatermark.Height);
			if (bmp.Height > this.numWatermarkHeight.Maximum)
				this.numWatermarkHeight.Maximum = bmp.Height * 3;
			if (bmp.Width > this.numWatermarkWidth.Maximum)
				this.numWatermarkWidth.Maximum = bmp.Width * 3;
            this.numWatermarkHeight.Value = bmp.Height;
            this.numWatermarkWidth.Value = bmp.Width;
        }

        private void CalculateCropMaximums()
        {
            if (!this.preventCropUpdate)
            {
                this.preventCropUpdate = true;
                NormalizeCurrentCrops();

                this.numCropTop.Maximum = this.numWatermarkHeight.Value - this.numCropBottom.Value-1;
                this.numCropBottom.Maximum = this.numWatermarkHeight.Value - this.numCropTop.Value-1;
                this.numCropLeft.Maximum = this.numWatermarkWidth.Value - this.numCropRight.Value-1;
                this.numCropRight.Maximum = this.numWatermarkWidth.Value - this.numCropLeft.Value-1;
                this.preventCropUpdate = false;
            }
        }

        private void NormalizeCurrentCrops()
        {
            decimal top = this.numCropTop.Value;
            decimal bottom = this.numCropBottom.Value;
            decimal left = this.numCropLeft.Value;
            decimal right = this.numCropRight.Value;

            while (top + bottom >= this.numWatermarkHeight.Value)
            {
                if (top > 0)
                    top--;
                else
                    bottom--;
            }

            while (left + right >= this.numWatermarkWidth.Value)
            {
                if (left > 0)
                    left--;
                else
                    right--;
            }

            SetValueWithCare(this.numCropTop, top);
            SetValueWithCare(this.numCropBottom, bottom);
            SetValueWithCare(this.numCropLeft, left);
            SetValueWithCare(this.numCropRight, right);
        }

        private void SetValueWithCare(NumericUpDown numericUpDown, decimal value)
        {
            if (value < numericUpDown.Minimum)
                numericUpDown.Value = numericUpDown.Minimum;
            else if (value > numericUpDown.Maximum)
                numericUpDown.Value = numericUpDown.Maximum;
            else
                numericUpDown.Value = value;
        }

        public Bitmap ResizeBitmap(Bitmap bmp, int nWidth, int nHeight)
        {
            Bitmap result = new Bitmap(nWidth, nHeight);
            using (Graphics g = Graphics.FromImage((Image)result))
                g.DrawImage(bmp, 0, 0, nWidth, nHeight);
            return result;
        }

        private void btnSelectGraphic_Click(object sender, EventArgs e)
        {
            BrowseForGraphic();
        }

        private void btnGraphicKey_Click(object sender, EventArgs e)
        {
            ColorDialog dialog = new ColorDialog();
            dialog.Color = App.CurAppSettings.ConvertFromColorString(((Graphic)App.CurAppSettings.WatermarkSettings.Setting).Key);
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                ((Graphic)App.CurAppSettings.WatermarkSettings.Setting).Key = App.CurAppSettings.ConvertToColorString(dialog.Color);
				LoadBackgroundColor(this.btnGraphicKey, dialog.Color);
				UpdateCurAppSettings();
            }
        }

        private void LoadBackgroundColor(Button button, Color color)
        {
            button.BackColor = color;
            if (((color.R + color.G + color.B) / 3.0f) < (255.0f / 2.0f))
            {
                button.ForeColor = Color.White;
            }
            else
            {
                button.ForeColor = Color.Black;
            }
        }

        private void numWatermarkWidth_ValueChanged(object sender, EventArgs e)
        {
            if (!this.preventSizeUpdate)
            {
                if (this.chkLockAspect.Checked)
                {
                    this.preventSizeUpdate = true;
                    decimal heightResult = (decimal)((float)this.numWatermarkWidth.Value * this.lastGraphicRatio.Height);
                    decimal widthResult = (decimal)((float)this.numWatermarkHeight.Value * this.lastGraphicRatio.Width);
                    if (heightResult >= this.numWatermarkHeight.Minimum && heightResult <= this.numWatermarkHeight.Maximum)
                    {
                        this.numWatermarkHeight.Value = heightResult;
                    }
                    else
					{
						if (widthResult >= this.numWatermarkWidth.Minimum && widthResult <= this.numWatermarkWidth.Maximum)
							this.numWatermarkWidth.Value = widthResult;
                    }
                }
                CalculateCropMaximums();
                UpdateCurAppSettings();
                this.preventSizeUpdate = false;
            }
        }

        private void numWatermarkHeight_ValueChanged(object sender, EventArgs e)
        {
            if (!this.preventSizeUpdate)
            {
                if (this.chkLockAspect.Checked)
                {
                    this.preventSizeUpdate = true;
                    decimal heightResult = (decimal)((float)this.numWatermarkWidth.Value * this.lastGraphicRatio.Height);
                    decimal widthResult = (decimal)((float)this.numWatermarkHeight.Value * this.lastGraphicRatio.Width);
                    if (widthResult >= this.numWatermarkWidth.Minimum && widthResult <= this.numWatermarkWidth.Maximum)
                    {
                        this.numWatermarkWidth.Value = widthResult;
                    }
                    else
                    {
						if(heightResult >= this.numWatermarkHeight.Minimum && heightResult <= this.numWatermarkHeight.Maximum)
							this.numWatermarkHeight.Value = heightResult;
                    }
                }
                CalculateCropMaximums();
                UpdateCurAppSettings();
                this.preventSizeUpdate = false;
            }
        }

        private void UpdateRatios()
        {
            this.lastGraphicRatio.Width = (float)this.numWatermarkWidth.Value / (float)this.numWatermarkHeight.Value;
            this.lastGraphicRatio.Height = (float)this.numWatermarkHeight.Value / (float)this.numWatermarkWidth.Value;
        }

		public override void Initialize()
        {
            if (App.CurAppSettings.WatermarkSettings.WatType != Watermark.WatermarkType.GraphicWatermark)
                return;

            this.initializing = true;
            Graphic settings = (Graphic)App.CurAppSettings.WatermarkSettings.Setting;
            this.txtGraphicLocation.Text = settings.Location;
            if (!String.IsNullOrEmpty(this.txtGraphicLocation.Text))
                LoadGraphic();
            LoadBackgroundColor(this.btnGraphicKey, App.CurAppSettings.ConvertFromColorString(settings.Key));
            this.numWatermarkHeight.Value = settings.Height;
            this.numWatermarkWidth.Value = settings.Width;
            this.numCropTop.Value = (decimal)settings.CropTop;
            this.numCropLeft.Value = (decimal)settings.CropLeft;
            this.numCropBottom.Value = (decimal)settings.CropBottom;
            this.numCropRight.Value = (decimal)settings.CropRight;
            this.numRemapPercent.Value = (decimal)settings.RemapPercent;
            this.initializing = false;
        }

		public override void UpdateCurAppSettings()
        {
            if (!this.initializing && App.CurAppSettings.WatermarkSettings.Setting is Graphic)
            {
                Graphic settings = (Graphic)App.CurAppSettings.WatermarkSettings.Setting;
                settings.Location = this.txtGraphicLocation.Text;
                settings.Height = (int)this.numWatermarkHeight.Value;
                settings.Width = (int)this.numWatermarkWidth.Value;
                settings.CropTop = (int)this.numCropTop.Value;
                settings.CropLeft = (int)this.numCropLeft.Value;
                settings.CropBottom = (int)this.numCropBottom.Value;
                settings.CropRight = (int)this.numCropRight.Value;
                settings.RemapPercent = (int)this.numRemapPercent.Value;
                App.CurAppSettings.FireWatermark();
            }
        }

        void Application_Idle(object sender, EventArgs e)
        {
            GetParentWatermarkControl().UpdateFrequencyBounds();
            Application.Idle -= new EventHandler(Application_Idle);
        }

        private void btnResetStretch_Click(object sender, EventArgs e)
        {
            LoadGraphic();
            Application.Idle += new EventHandler(Application_Idle);
        }

        private void btnResetCrop_Click(object sender, EventArgs e)
        {
            this.numCropBottom.Value = 0;
            this.numCropLeft.Value = 0;
            this.numCropRight.Value = 0;
            this.numCropTop.Value = 0;
            CalculateCropMaximums();
            UpdateCurAppSettings();
        }

        private void numCropTop_ValueChanged(object sender, EventArgs e)
        {
            CalculateCropMaximums();
            UpdateCurAppSettings();
        }

        private void numCropLeft_ValueChanged(object sender, EventArgs e)
        {
            CalculateCropMaximums();
            UpdateCurAppSettings();
        }

        private void numCropBottom_ValueChanged(object sender, EventArgs e)
        {
            CalculateCropMaximums();
            UpdateCurAppSettings();
        }

        private void numCropRight_ValueChanged(object sender, EventArgs e)
        {
            CalculateCropMaximums();
            UpdateCurAppSettings();
        }

        private void numRemapPercent_ValueChanged(object sender, EventArgs e)
        {
            UpdateCurAppSettings();
        }

        private void chkLockAspect_CheckedChanged(object sender, EventArgs e)
        {
            if(this.chkLockAspect.Checked)
                UpdateRatios();
            Application.Idle += new EventHandler(Application_Idle);
        }

        public void SetFrequency(int frequency, int maximum)
        {
            if (this.frequencySize != Size.Empty)
            {
                float percent = (float)frequency / 100.0f;

                SetValueWithCare(this.numWatermarkHeight, (decimal)((float)this.frequencySize.Height * percent));
                SetValueWithCare(this.numWatermarkWidth, (decimal)((float)this.frequencySize.Width * percent));
            }
        }

        internal void ActivateFrequency()
        {
            this.frequencySize = new Size((int)this.numWatermarkWidth.Value, (int)this.numWatermarkHeight.Value);
        }

        internal void DeactivateFrequency()
        {
            this.frequencySize = Size.Empty;
        }

        private void OnCropTopLeave(object sender, EventArgs e)
        {
            Application.Idle+=new EventHandler(Application_Idle);
        }

        private void OnCropLeftLeave(object sender, EventArgs e)
        {
            Application.Idle += new EventHandler(Application_Idle);
        }

        private void OnCropBottomLeave(object sender, EventArgs e)
        {
            Application.Idle += new EventHandler(Application_Idle);
        }

        private void OnCropRightLeave(object sender, EventArgs e)
        {
            Application.Idle += new EventHandler(Application_Idle);
        }

        private void OnHeightLeave(object sender, EventArgs e)
        {
            Application.Idle += new EventHandler(Application_Idle);
        }

        private void OnWidthLeave(object sender, EventArgs e)
        {
            Application.Idle += new EventHandler(Application_Idle);
        }
    }
}
