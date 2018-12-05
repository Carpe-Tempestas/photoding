using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Trebuchet
{
    public partial class About : Form
    {
        public About()
        {
            InitializeComponent();
        }

        public string Version
        {
            get
            {
                string ret = this.lblVersion.Text;
                ret = ret.Remove(0, 12);
                ret = ret.Remove(ret.Length - 2, 2);
                return ret;
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.BringToFront();
            this.Focus();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("IExplore", "https://github.com/Carpe-Tempestas/photoding");

            linkLabel1.LinkVisited = true;
        }
    }
}