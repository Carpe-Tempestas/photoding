using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Trebuchet.Controls
{
	public partial class ExifControl : UserControl
	{
		private ThumbnailViewer thumbnailViewer = null;

		public ExifControl()
		{
			InitializeComponent();
		}

		public ThumbnailViewer Viewer
		{
			get
			{
				return this.thumbnailViewer;
			}
			set
			{
				UnsubscribeToViewerEvents();
				this.thumbnailViewer = value;
				SubscribeToViewerEvents();
			}
		}

		private void SubscribeToViewerEvents()
		{
			if (this.thumbnailViewer != null)
				this.thumbnailViewer.SelectionChanged += new EventHandler(thumbnailViewer_SelectionChanged);
		}

		private void UnsubscribeToViewerEvents()
		{
			if (this.thumbnailViewer != null)
				this.thumbnailViewer.SelectionChanged -= thumbnailViewer_SelectionChanged;
		}

		void thumbnailViewer_SelectionChanged(object sender, EventArgs e)
		{
			string photoLocation = this.thumbnailViewer.GetSelectedPhotoLocation();
			LoadImageInfo(photoLocation);
		}

		private void LoadImageInfo(string photoLocation)
		{
			InitializeInfo();
		}

		private void InitializeInfo()
		{
			DataGridViewCheckBoxCell copyCell = null;
			DataGridViewTextBoxCell propertyCell = null;
			DataGridViewTextBoxCell valueCell = null;
			this.dataGridExif.Rows.Clear();
			int index = 0;
		}

		private void comboMode_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (this.comboMode.SelectedIndex == 0)
				App.CurAppSettings.ImageAdjSettings.ExifMode = Trebuchet.Settings.ImageSettings.ExifModes.CopyAll;
			//else if (this.comboMode.SelectedIndex == 1)
			//    App.CurAppSettings.ImageAdjSettings.ExifMode = Trebuchet.Settings.ImageSettings.ExifModes.CopySome;
			//else if (this.comboMode.SelectedIndex == 2)
			//    App.CurAppSettings.ImageAdjSettings.ExifMode = Trebuchet.Settings.ImageSettings.ExifModes.CopyNone;
			else
				App.CurAppSettings.ImageAdjSettings.ExifMode = Trebuchet.Settings.ImageSettings.ExifModes.CopyNone;
		}
	}
}
