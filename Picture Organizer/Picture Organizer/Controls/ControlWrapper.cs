using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Trebuchet.Controls
{
    public partial class ControlWrapper : UserControl
    {
        IPikControl innerControl = null;
		private int cacheHeight = 0;

        public ControlWrapper(IPikControl control)
        {
            InitializeComponent();
            this.innerControl = control;
            this.MinPanelSize = new Size(((Control)control).MinimumSize.Width, ((Control)control).MinimumSize.Height);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            AddControl(this.innerControl);
        }

        private void btnDisabled_Click(object sender, EventArgs e)
        {
            if (this.btnDisabled.Text == "Enabled - click here to disable!")
            {
                EnableControls(false);
            }
            else
            {
                EnableControls(true);
            }
        }

        public void EnableControls(bool enable)
        {
            this.innerControl.ControlEnabled = enable;

            if (!enable)
            {
                foreach (Control control in this.Controls)
                {
					if (control != this.panelMode && control != this.btnDisabled)
						control.Hide();
                }
                this.btnDisabled.Text = "Disabled - click here to enable!";
                this.btnDisabled.ForeColor = Color.Red;
            }
            else
            {
                foreach (Control control in this.Controls)
                {
					control.Show();
                }
                this.btnDisabled.Text = "Enabled - click here to disable!";
                this.btnDisabled.ForeColor = Color.Green;
            }
        }

        public void AddControl(IPikControl pikControl)
        {
            this.innerControl = pikControl;
            Control control = this.innerControl as UserControl;
            control.Dock = DockStyle.Fill;
            this.panelControl.Width = control.Width;
            this.panelControl.Controls.Add(control);
            this.UseButton = this.innerControl.UseEnabled;
            this.innerControl.enabling += new EventHandler(innerControl_enabling);
            EnableControls(this.innerControl.ControlEnabled);
            this.txtDirections.Text = this.innerControl.Directions;
            this.innerControl.directionsChanging += new EventHandler(innerControl_directionsChanging);
        }

        void innerControl_directionsChanging(object sender, EventArgs e)
        {
            this.txtDirections.Text = this.innerControl.Directions;
        }

        void innerControl_enabling(object sender, EventArgs e)
        {
            EnableControls(this.innerControl.ControlEnabled);
        }

        public bool UseButton
        {
            get
            {
				return this.btnDisabled.Visible;
            }
            set
            {
                this.btnDisabled.Visible = value;
            }
        }

		public Size MinPanelSize
		{
			get { return this.panelControl.AutoScrollMinSize; }
			set	{ this.panelControl.AutoScrollMinSize = value; }
		}
    }
}
