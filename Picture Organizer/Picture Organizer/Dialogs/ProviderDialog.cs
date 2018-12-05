using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Utypes = Trebuchet.Settings.UploadSettings.UploadType;

namespace Trebuchet.Dialogs
{
    public partial class ProviderDialog : Form
    {
        List<Utypes> types = new List<Utypes>();

        public ProviderDialog()
        {
            InitializeComponent();
            this.listProviders.SelectedIndex = 0;

            this.types.Add(Utypes.Ftp);
            //this.types.Add(Utypes.Facebook);
            this.types.Add(Utypes.Flickr);
            this.types.Add(Utypes.Picasa);
			this.types.Add(Utypes.Email);
            //Add new upload types here!!
        }

        public string UploadName
        {
            get { return this.txtName.Text; }
            set { this.txtName.Text = value; }
        }

        public Utypes GetProvider()
        {
            return this.types[this.listProviders.SelectedIndex];
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
			if (App.Uploaders.GetUpload(this.txtName.Name) == null)
			{
				this.DialogResult = DialogResult.OK;
				this.Close();
			}
			else
			{
				MessageBox.Show("An uploader has already been created with that name, please choose a different name.");
			}
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            this.btnOk.Enabled = !String.IsNullOrEmpty(this.txtName.Text);
        }

        private void listProviders_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(this.txtName.Text) || this.txtName.Text.StartsWith("My"))
            {
				this.txtName.Text = GetNewDefaultName(); 
            }
        }

		private string GetNewDefaultName()
		{
			int count = 0;
			string name = "My" + this.listProviders.SelectedItem.ToString();
			while(App.Uploaders.GetUpload(name) != null)
			{
				count++;
				name = String.Format("My" + this.listProviders.SelectedItem.ToString() + "{0}", count);
			}
			return name;
		}
    }
}