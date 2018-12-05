using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Trebuchet.TrebuchetDialogs
{
    public partial class DateDialog : Form
    {
        public DateDialog()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.dateTimePicker1.Format = DateTimePickerFormat.Custom;
        }

        private void txtDateFormat_TextChanged(object sender, EventArgs e)
        {
            this.dateTimePicker1.CustomFormat = ProcessFormat();
        }

        private string ProcessFormat()
        {
            StringBuilder sb = new StringBuilder();
            string format = this.txtDateFormat.Text;

            for (int x = 0; x < format.Length; x++)
            {
                if (format[x] == 'm')
                    sb.Append('M');
                else if (format[x] == '/')
                {
                    format = format.Remove(x, 1);
                    format = format.Insert(x, "/");
                    sb.Append('-');
                }
                else
                    sb.Append(format[x]);
            }

            return sb.ToString();
        }

        public void SetFormat()
        {
            this.dateTimePicker1.Format = DateTimePickerFormat.Custom;
            this.txtDateFormat.Text = App.CurAppSettings.RenameSettings.DateFormat;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            App.CurAppSettings.RenameSettings.DateFormat = this.txtDateFormat.Text;
            App.DateText = this.dateTimePicker1.Text;
            this.Hide();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}