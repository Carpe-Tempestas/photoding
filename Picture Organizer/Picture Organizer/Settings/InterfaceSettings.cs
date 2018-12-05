using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using System.Drawing;

namespace Trebuchet.Settings
{
	public class InterfaceSettings : SettingsBase
	{
		private bool useAutoPlay;
		private bool maximized;
		private bool autoLoadPhotos;
		private int topSplitterOffsetMaximized;
		private int bottomSplitterOffsetMaximized;
		private int topSplitterOffsetMinimized;
		private int bottomSplitterOffsetMinimized;
		private Size windowSize;

		public InterfaceSettings()
		{

		}

		public bool UseAutoPlay
		{
			get { return this.useAutoPlay; }
			set { this.useAutoPlay = value; }
		}

		public bool Maximized
		{
			get { return this.maximized; }
			set { this.maximized = value; }
		}

		public bool AutoLoadPhotos
		{
			get { return this.autoLoadPhotos; }
			set { this.autoLoadPhotos = value; }
		}

		public int TopSplitterOffsetMaximized
		{
			get { return this.topSplitterOffsetMaximized; }
			set { this.topSplitterOffsetMaximized = value; }
		}

		public int BottomSplitterOffsetMaximized
		{
			get { return this.bottomSplitterOffsetMaximized; }
			set { this.bottomSplitterOffsetMaximized = value; }
		}

		public int TopSplitterOffsetMinimized
		{
			get { return this.topSplitterOffsetMinimized; }
			set { this.topSplitterOffsetMinimized = value; }
		}

		public int BottomSplitterOffsetMinimized
		{
			get { return this.bottomSplitterOffsetMinimized; }
			set { this.bottomSplitterOffsetMinimized = value; }
		}

		public Size WindowSize
		{
			get { return this.windowSize; }
			set { this.windowSize = value; }
		}

		public override void LoadSettings(object obj)
		{
			InterfaceSettings settings = obj as InterfaceSettings;
			if(settings == null)
				return;

			this.UseAutoPlay = settings.UseAutoPlay;
			this.AutoLoadPhotos = settings.AutoLoadPhotos;
			this.Maximized = settings.Maximized;
			this.TopSplitterOffsetMaximized = settings.TopSplitterOffsetMaximized;
			this.BottomSplitterOffsetMaximized = settings.BottomSplitterOffsetMaximized;
			this.TopSplitterOffsetMinimized = settings.TopSplitterOffsetMinimized;
			this.BottomSplitterOffsetMinimized = settings.BottomSplitterOffsetMinimized;
			this.WindowSize = settings.WindowSize;
		}

		public override void LoadDefaults()
		{
			this.UseAutoPlay = true;
			this.AutoLoadPhotos = false;
			this.Maximized = false;
			this.TopSplitterOffsetMaximized = 500;
			this.BottomSplitterOffsetMaximized = 100;
			this.TopSplitterOffsetMinimized = 500;
			this.BottomSplitterOffsetMinimized = 100;
			this.WindowSize = new Size(800, 600);
		}
	}
}
