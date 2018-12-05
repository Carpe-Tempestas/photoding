using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Trebuchet.Settings.WatermarkTypes;

namespace Trebuchet.Controls
{
	public partial class WatermarkTextControl : TrebUserControl
    {
        private bool initializing = false;
        private ToolTip dropShadowTip = new ToolTip();
        private ToolTip foreGroundTip = new ToolTip();

        public WatermarkTextControl()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!this.DesignMode)
            {
                Initialize();
                this.dropShadowTip.SetToolTip(this.btnBackColor, "Click to select the drop shadow color.");
                this.foreGroundTip.SetToolTip(this.btnForeColor, "Click to select the text color.");
            }
		}

		private WatermarkControl GetParentWatermarkControl(Control control)
		{
			if (control == null)
				return null;
			else if (control is WatermarkControl)
				return control as WatermarkControl;
			else
			{
				return GetParentWatermarkControl(control.Parent);
			}

		}

        public bool CanContinue()
        {
            if (String.IsNullOrEmpty(this.txtWaterText.Text))
                return false;
            else
                return true;
        }

		public override void Initialize()
        {
			this.initializing = true;
			if (App.CurAppSettings.WatermarkSettings.Setting != null && App.AppSettings.WatermarkSettings.Setting is WatText)
			{
				this.txtWaterText.Text = ((WatText)App.CurAppSettings.WatermarkSettings.Setting).WatermarkText;
				TypeConverter tc = TypeDescriptor.GetConverter(typeof(Font));
				this.fontDialog1.Font = (Font)tc.ConvertFromString(((WatText)App.AppSettings.WatermarkSettings.Setting).WatFont);
				WatermarkControl parent = GetParentWatermarkControl(this);
				if (parent != null)
				{
					parent.SetAndExpandFrequencyBounds((int)this.fontDialog1.Font.Size);
				}
				LoadBackgroundColor(this.btnBackColor, App.CurAppSettings.ConvertFromColorString(((WatText)App.CurAppSettings.WatermarkSettings.Setting).TextBackground));
				LoadBackgroundColor(this.btnForeColor, App.CurAppSettings.ConvertFromColorString(((WatText)App.CurAppSettings.WatermarkSettings.Setting).TextForeground));
			}
			this.initializing = false;
        }

		public override void UpdateCurAppSettings()
        {
			if (!this.initializing && App.CurAppSettings.WatermarkSettings.Setting is WatText)
            {
                ((WatText)App.CurAppSettings.WatermarkSettings.Setting).WatermarkText = this.txtWaterText.Text;
                TypeConverter tc = TypeDescriptor.GetConverter(typeof(Font));
                ((WatText)App.CurAppSettings.WatermarkSettings.Setting).WatFont = tc.ConvertToString(this.fontDialog1.Font);
                App.CurAppSettings.FireWatermark();
            }
        }

        void Application_Idle(object sender, EventArgs e)
        {
            GetParentWatermarkControl().UpdateFrequencyBounds();
            Application.Idle -= new EventHandler(Application_Idle);
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

        private void txtWaterText_TextChanged(object sender, EventArgs e)
        {
            UpdateCurAppSettings();
			WatermarkControl parent = this.Parent as WatermarkControl;
			if (parent != null)
			{
				parent.UpdateFrequencyBounds();
			}
        }

        private void btnInsertCopyright_Click(object sender, EventArgs e)
        {
            char copyright = (char)169;
            this.txtWaterText.Text = this.txtWaterText.Text + copyright;
            this.txtWaterText.Focus();
            this.txtWaterText.Select(this.txtWaterText.Text.Length, 1);
        }

        private void btnSelectFont_Click(object sender, EventArgs e)
        {
            if (this.fontDialog1.ShowDialog() == DialogResult.OK)
            {
                Application.Idle+=new EventHandler(Application_Idle);
				UpdateCurAppSettings();
				WatermarkControl parent = GetParentWatermarkControl(this);
				if (parent != null)
				{
					parent.SetAndExpandFrequencyBounds((int)this.fontDialog1.Font.Size);
				}
            }
        }

        private void btnInsertNewline_Click(object sender, EventArgs e)
        {
            this.txtWaterText.Text = this.txtWaterText.Text + Environment.NewLine;
            this.txtWaterText.Focus();
            this.txtWaterText.Select(this.txtWaterText.Text.Length, 1);
        }

        private void btnBackColor_Click(object sender, EventArgs e)
        {
            ColorDialog dialog = new ColorDialog();
            dialog.Color = App.CurAppSettings.ConvertFromColorString(((WatText)App.CurAppSettings.WatermarkSettings.Setting).TextBackground);
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                ((WatText)App.CurAppSettings.WatermarkSettings.Setting).TextBackground = App.CurAppSettings.ConvertToColorString(dialog.Color);
                LoadBackgroundColor(this.btnBackColor, dialog.Color);
                App.CurAppSettings.FireWatermark();
            }
        }

        private void btnForeColor_Click(object sender, EventArgs e)
        {
            ColorDialog dialog = new ColorDialog();
            dialog.Color = App.CurAppSettings.ConvertFromColorString(((WatText)App.CurAppSettings.WatermarkSettings.Setting).TextForeground);
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                ((WatText)App.CurAppSettings.WatermarkSettings.Setting).TextForeground = App.CurAppSettings.ConvertToColorString(dialog.Color);
                LoadBackgroundColor(this.btnForeColor, dialog.Color);
                App.CurAppSettings.FireWatermark();
            }
        }

        public void SetFrequency(int frequency)
        {
            Font font = new Font(this.fontDialog1.Font.FontFamily, (float)frequency, this.fontDialog1.Font.Style);
            this.fontDialog1.Font = font;
            UpdateCurAppSettings();
        }

        private void OnTxtWatermarkLeave(object sender, EventArgs e)
        {
            Application.Idle += new EventHandler(Application_Idle);
        }
    }
}
