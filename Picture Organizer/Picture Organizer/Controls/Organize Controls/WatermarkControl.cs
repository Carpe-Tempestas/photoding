using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Trebuchet.Settings;

namespace Trebuchet.Controls
{
	public partial class WatermarkControl : TrebUserControl, IPikControl
    {
        private bool initializing = false;
        public event EventHandler enabling;
        public event EventHandler directionsChanging;
        private bool frequencyMouseDown = false;
        private bool frequencyActivated = false;
		private bool settingFrequency = false;
        private ThumbnailViewer thumbnailViewer = null;
        List<Watermark.WatermarkLocation> locations = new List<Watermark.WatermarkLocation>();

        public WatermarkControl()
        {
            InitializeComponent();

            AddLocations();
        }

        private void AddLocations()
        {
            this.locations.Add(Watermark.WatermarkLocation.TopLeft);
            this.locations.Add(Watermark.WatermarkLocation.TopCenter);
            this.locations.Add(Watermark.WatermarkLocation.TopRight);
            this.locations.Add(Watermark.WatermarkLocation.CenterLeft);
            this.locations.Add(Watermark.WatermarkLocation.CenterCenter);
            this.locations.Add(Watermark.WatermarkLocation.CenterRight);
            this.locations.Add(Watermark.WatermarkLocation.BottomLeft);
            this.locations.Add(Watermark.WatermarkLocation.BottomCenter);
            this.locations.Add(Watermark.WatermarkLocation.BottomRight);
            this.locations.Add(Watermark.WatermarkLocation.Tile);
            this.locations.Add(Watermark.WatermarkLocation.Custom);           
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
            if (this.thumbnailViewer != null)
                this.thumbnailViewer.SelectionChanged -= thumbnailViewer_SelectionChanged;
        }

        void thumbnailViewer_SelectionChanged(object sender, EventArgs e)
        {
            UpdateFrequencyBounds();
        }

		public void SetAndExpandFrequencyBounds(int frequency)
		{
			if (this.trackFrequency.Minimum <= frequency && frequency <= this.trackFrequency.Minimum)
			{
				this.settingFrequency = true;
				this.trackFrequency.Value = frequency;
				this.settingFrequency = false;
				return;
			}

			if (frequency < this.trackFrequency.Minimum && frequency >= 0)
			{
				this.trackFrequency.Minimum = frequency;
				this.settingFrequency = true;
				this.trackFrequency.Value = frequency;
				this.settingFrequency = false;
			}

			if (frequency > this.trackFrequency.Maximum)
			{
				this.trackFrequency.Maximum = frequency;
				this.settingFrequency = true;
				this.trackFrequency.Value = frequency;
				this.settingFrequency = false;
			}
		}

        public void UpdateFrequencyBounds()
        {
            int minimum = 0, maximum = 0;
            App.TheCore.GetFrequencyBounds(out minimum, out maximum);

            if (minimum == 0 && maximum == 0)
                this.trackFrequency.Visible = false;
            else 
                this.trackFrequency.Visible = true;

            if(minimum != 0)
                this.trackFrequency.Minimum = minimum;
            if(maximum != 0)
                this.trackFrequency.Maximum = maximum;

            if (this.trackFrequency.Value < this.trackFrequency.Minimum)
                this.trackFrequency.Value = this.trackFrequency.Minimum;
            if (this.trackFrequency.Value > this.trackFrequency.Maximum)
                this.trackFrequency.Value = this.trackFrequency.Maximum;
        }

        public string Directions
        {
            get
            {
                return "Directions:" + System.Environment.NewLine + 
                    "The watermark location specifies where the watermark will be placed on your photo.  Click the \"Select Font\" button to select a unique font.  Enter the text that the watermark will contain.  Use the \"Quick Picks\" buttons to insert special characters.  Choose the transparency of the watermark - the lower the number, the more transparent the watermark will be.  Finally, check the box if you want to create a watermark directory.";
            }
        }

        public bool UseEnabled
        {
            get
            {
                return true;
            }
        }

        public bool ControlEnabled
        {
            get
            {
				if (this.DesignMode)
					return false;

                return App.CurAppSettings.WatermarkMode;
            }
            set
            {
                App.CurAppSettings.WatermarkMode = value;
                this.Enabled = value;
                App.CurAppSettings.FireWatermark();
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!this.DesignMode)
            {
                Initialize();
				this.Enabled = this.ControlEnabled;
				App.CurAppSettingChanged += new EventHandler(App_CurAppSettingChanged);
            }
		}

		void App_CurAppSettingChanged(object sender, EventArgs e)
		{
			Initialize();
		}

		public override void Initialize()
        {
            this.initializing = true;
            this.comboWaterLoc.SelectedIndex = (int)App.CurAppSettings.WatermarkSettings.Location;
            this.numTransparency.Value = App.CurAppSettings.WatermarkSettings.Transparency;
            if (App.CurAppSettings.WatermarkSettings.WatType == Watermark.WatermarkType.TextWatermark)
            {
                this.rbText.Checked = true;
                this.rbGraphic.Checked = false;
            }
            else
            {
                this.rbText.Checked = false;
                this.rbGraphic.Checked = true;
            }
            ManageWatermarkType();
            this.numSpacingTop.Value = App.CurAppSettings.WatermarkSettings.SpacingTop;
            this.numSpacingLeft.Value = App.CurAppSettings.WatermarkSettings.SpacingLeft;
            this.numSpacingHeight.Value = App.CurAppSettings.WatermarkSettings.SpacingHeight;
            this.numSpacingWidth.Value = App.CurAppSettings.WatermarkSettings.SpacingWidth;
			this.chkCreateWater.Checked = App.CurAppSettings.WatermarkSettings.CreateDir;

			if (App.CurAppSettings.WatermarkMode != this.Enabled)
				GetWrapper().EnableControls(App.CurAppSettings.WatermarkMode);
            this.initializing = false;
        }

        private void comboWaterLoc_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateCurAppSettings();
            if (this.comboWaterLoc.SelectedIndex == (int)Watermark.WatermarkLocation.Tile)
            {
                this.grpSpacing.Visible = true;
                this.trackFrequency.Visible = true;
                this.lblFrequency.Visible = true;
            }
            else
            {
                this.grpSpacing.Visible = false;
                this.trackFrequency.Visible = true;
                this.lblFrequency.Visible = true;
            }
        }        

        private void numTransparency_ValueChanged(object sender, EventArgs e)
        {
            UpdateCurAppSettings();
        }

        private void chkCreateWater_CheckedChanged(object sender, EventArgs e)
        {
            UpdateCurAppSettings();
        }

        public bool CanContinue()
        {
            if ((this.rbText.Checked && this.watermarkTextControl1.CanContinue()) 
                ||(this.rbGraphic.Checked && this.watermarkGraphicControl1.CanContinue()))
                return true;
            else
                return false;
        }

		public override void UpdateCurAppSettings()
        {
            if (!this.initializing)
            {
                App.CurAppSettings.WatermarkSettings.Location = GetLocation(this.comboWaterLoc.SelectedIndex);
                App.CurAppSettings.WatermarkSettings.Transparency = (int)this.numTransparency.Value;
                if (this.rbText.Checked)
                    App.CurAppSettings.WatermarkSettings.WatType = Watermark.WatermarkType.TextWatermark;
                else
                    App.CurAppSettings.WatermarkSettings.WatType = Watermark.WatermarkType.GraphicWatermark;
                App.CurAppSettings.WatermarkSettings.SpacingTop = (int)this.numSpacingTop.Value;
                App.CurAppSettings.WatermarkSettings.SpacingLeft = (int)this.numSpacingLeft.Value;
                App.CurAppSettings.WatermarkSettings.SpacingHeight = (int)this.numSpacingHeight.Value;
                App.CurAppSettings.WatermarkSettings.SpacingWidth = (int)this.numSpacingWidth.Value;
                App.CurAppSettings.WatermarkSettings.CreateDir = this.chkCreateWater.Checked;
                App.CurAppSettings.FireWatermark();
            }
        }

        private Watermark.WatermarkLocation GetLocation(int index)
        {
            if (index > this.locations.Count)
                return 0;
            else
                return this.locations[index];
        }

        private void rbText_CheckedChanged(object sender, EventArgs e)
        {
            ManageWatermarkType();
        }

        private void ManageWatermarkType()
        {
            this.watermarkTextControl1.Visible = this.rbText.Checked;
            this.watermarkGraphicControl1.Visible = !this.rbText.Checked;
            UpdateCurAppSettings();
            UpdateFrequencyBounds();
        }

        private void rbGraphic_CheckedChanged(object sender, EventArgs e)
        {
            ManageWatermarkType();
        }

        private void numSpacingTop_ValueChanged(object sender, EventArgs e)
        {
            UpdateCurAppSettings();
        }

        private void numSpacingLeft_ValueChanged(object sender, EventArgs e)
        {
            UpdateCurAppSettings();
        }

        private void numSpacingHeight_ValueChanged(object sender, EventArgs e)
        {
            UpdateCurAppSettings();
        }

        private void numSpacingWidth_ValueChanged(object sender, EventArgs e)
        {
            UpdateCurAppSettings();
        }

        private void OnFrequencyChanged(object sender, EventArgs e)
        {
			if (this.settingFrequency)
				return;

            if (this.rbText.Checked)
            {
                this.watermarkTextControl1.SetFrequency((int)this.trackFrequency.Value);
            }
            else if (this.rbGraphic.Checked)
            {
                if (this.frequencyMouseDown)
                {
                    this.watermarkGraphicControl1.SetFrequency((int)this.trackFrequency.Value, (int)this.trackFrequency.Maximum);
                }
                else
                {
                    this.watermarkGraphicControl1.ActivateFrequency();
                    this.watermarkGraphicControl1.SetFrequency((int)this.trackFrequency.Value, (int)this.trackFrequency.Maximum);
                    this.watermarkGraphicControl1.DeactivateFrequency();
                }
            }
        }

        private void OnFrequencyMouseDown(object sender, MouseEventArgs e)
        {
            this.frequencyMouseDown = true;
            if (this.rbGraphic.Checked)
            {
                this.watermarkGraphicControl1.ActivateFrequency();
            }
        }

        private void OnFrequencyMouseUp(object sender, MouseEventArgs e)
        {
            this.frequencyMouseDown = false;
            if (this.rbGraphic.Checked)
            {
                this.watermarkGraphicControl1.DeactivateFrequency();
            }
        }
    }
}
