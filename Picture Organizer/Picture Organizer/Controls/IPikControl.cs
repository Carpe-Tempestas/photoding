using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Trebuchet.Controls
{
    public interface IPikControl
    {
        event EventHandler enabling;
        event EventHandler directionsChanging;
        string Directions
        {
            get;
        }
        bool UseEnabled
        {
            get;
        }
        bool ControlEnabled
        {
            get;
            set;
        }

		void Initialize();
		void UpdateCurAppSettings();
    }
}
