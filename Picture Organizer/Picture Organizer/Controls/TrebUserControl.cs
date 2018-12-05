using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Trebuchet.Controls
{
	public partial class TrebUserControl : UserControl
	{
		public TrebUserControl()
		{
			InitializeComponent();

			//if (!this.DesignMode)
			//{
			//    App.TheApp.Loading += new EventHandler(TheApp_Loading);
			//    App.TheApp.Saving += new EventHandler(TheApp_Saving);
			//}
		}

		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
		}

		void TheApp_Saving(object sender, EventArgs e)
		{
			UpdateCurAppSettings();
		}

		public virtual void UpdateCurAppSettings()
		{
			throw new Exception("The method or operation is not implemented.");
		}

		void TheApp_Loading(object sender, EventArgs e)
		{
			Initialize();
		}

		public virtual void Initialize()
		{
			throw new Exception("The method or operation is not implemented.");
		}

		protected ControlWrapper GetWrapper()
		{
			Control control = this;
			while (control != null && !(control is ControlWrapper))
			{
				control = control.Parent;
			}

			return control as ControlWrapper;
		}
	}
}
