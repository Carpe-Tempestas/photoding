using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Trebuchet.Controls
{
    public partial class ProgressControl : UserControl
    {
		private const string DetailsStarter = "Details: ";

        public ProgressControl()
        {
            InitializeComponent();
        }

        public ProgressBar Bar
        {
            get
            {
                return this.progressBar;
            }
        }

        public string ProgressTitle
        {
            get
            {
                return this.lblProgressTitle.Text;
            }
            set
            {
                this.lblProgressTitle.Text = value;
            }
        }

        public string Details
        {
            get
            {
                return this.txtDetails.Text;
            }
            set
            {
				this.txtDetails.Text = "Details: " + value;
            }
        }


    }
}
