using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Trebuchet.Controls
{
	public partial class PassDialog : Form
	{
		public PassDialog(string service)
		{
			InitializeComponent();

			this.lblService.Text = service;
		}

		public string GetPass()
		{
			if (this.DialogResult == DialogResult.OK)
				return this.txtPassword.Text;
			else
				return String.Empty;
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.OK;
			this.Close();
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.Cancel;
			this.Close();
		}
	}
}