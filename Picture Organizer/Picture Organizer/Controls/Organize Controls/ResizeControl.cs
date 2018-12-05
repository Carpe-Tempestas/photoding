using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Trebuchet.Controls
{
	public partial class ResizeControl : TrebUserControl, IPikControl
    {
        private bool initializing = false;
        public event EventHandler enabling;
        public event EventHandler directionsChanging;

        public ResizeControl()
        {
            InitializeComponent();
        }

        public string Directions
        {
            get
            {
                return "Directions:" + System.Environment.NewLine + 
                    "To resize your photos, use select the mode you would like to use.  Using a percentage will reduce the size of the image while maintaining aspect ratio.  If you choose to bound by a particular width and/or height, then the photo will be resized to fit within the specified bounds.  The aspect ratio will be maintained unless you select bounding by both height AND width.";
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
                return App.CurAppSettings.ResizeMode;
            }
            set
            {
                App.CurAppSettings.ResizeMode = value;
                this.Enabled = value;
                App.CurAppSettings.FireResize();
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
            if (App.CurAppSettings.ResizeSettings.Choice == 0)
                this.rbResPercent.Checked = true;
            else
                this.rbResBound.Checked = true;

            this.numResizePercent.Value = App.CurAppSettings.ResizeSettings.Percent;
            this.chkBoundWidth.Checked = App.CurAppSettings.ResizeSettings.BoundWidthEnabled;
            this.numUpDownBoundWidth.Value = App.CurAppSettings.ResizeSettings.BoundWidth;
            this.chkBoundHeight.Checked = App.CurAppSettings.ResizeSettings.BoundHeightEnabled;
			this.numUpDownBoundHeight.Value = App.CurAppSettings.ResizeSettings.BoundHeight;
            this.chkCreateResize.Checked = App.CurAppSettings.ResizeSettings.CreateDir;

			if (App.CurAppSettings.ResizeMode != this.Enabled)
				GetWrapper().EnableControls(App.CurAppSettings.ResizeMode);

            this.initializing = false;

            EnableProperControls();
        }

        private void rbResPercent_CheckedChanged(object sender, EventArgs e)
        {
            EnableProperControls();
            UpdateCurAppSettings();
        }

        private void rbResBound_CheckedChanged(object sender, EventArgs e)
        {
            EnableProperControls();
            UpdateCurAppSettings();
        }

        private void EnableProperControls()
        {
            this.numResizePercent.Enabled = this.rbResPercent.Checked;
            this.chkBoundWidth.Enabled = this.rbResBound.Checked;
            if (this.rbResBound.Checked)
                this.numUpDownBoundWidth.Enabled = this.chkBoundWidth.Checked;
            else
                this.numUpDownBoundWidth.Enabled = false;
            this.chkBoundHeight.Enabled = this.rbResBound.Checked;

            if (this.rbResBound.Checked)
                this.numUpDownBoundHeight.Enabled = this.chkBoundHeight.Checked;
            else
                this.numUpDownBoundHeight.Enabled = false;
        }

        private void chkBoundWidth_CheckedChanged(object sender, EventArgs e)
        {
            EnableProperControls();
            UpdateCurAppSettings();
        }

        private void chkBoundHeight_CheckedChanged(object sender, EventArgs e)
        {
            EnableProperControls();
            UpdateCurAppSettings();
        }

        private void numResizePercent_ValueChanged(object sender, EventArgs e)
        {
            UpdateCurAppSettings();
        }

        private void numUpDownBoundWidth_ValueChanged(object sender, EventArgs e)
        {
            UpdateCurAppSettings();
        }

        private void numUpDownBoundHeight_ValueChanged(object sender, EventArgs e)
        {
            UpdateCurAppSettings();
        }

        private void chkCreateResize_CheckedChanged(object sender, EventArgs e)
        {
            UpdateCurAppSettings();
        }

        public bool CanContinue()
        {
            if (this.rbResPercent.Checked || this.rbResBound.Checked)
                return true;
            else
                return false;
        }

        public override void UpdateCurAppSettings()
        {
            if (!this.initializing)
            {
                if (this.rbResPercent.Checked)
                    App.CurAppSettings.ResizeSettings.Choice = 0;
                else
                    App.CurAppSettings.ResizeSettings.Choice = 1;

                App.CurAppSettings.ResizeSettings.Percent = (int)this.numResizePercent.Value;
                App.CurAppSettings.ResizeSettings.BoundWidthEnabled = this.chkBoundWidth.Checked;
                App.CurAppSettings.ResizeSettings.BoundWidth = (int)this.numUpDownBoundWidth.Value;
				App.CurAppSettings.ResizeSettings.BoundHeightEnabled = this.chkBoundHeight.Checked;
				App.CurAppSettings.ResizeSettings.BoundHeight = (int)this.numUpDownBoundHeight.Value;
                App.CurAppSettings.ResizeSettings.CreateDir = this.chkCreateResize.Checked;
                App.CurAppSettings.FireResize();
            }
        }
    }
}
