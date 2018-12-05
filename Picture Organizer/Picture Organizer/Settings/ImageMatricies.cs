using System;
using System.Collections.Generic;
using System.Text;
using Trebuchet.Settings.ImageTypes;
using System.Drawing.Imaging;

namespace Trebuchet.Settings
{
	public class ImageMatricies : SettingsBase
	{
		private List<ImageColorMatrix> matricies = new List<ImageColorMatrix>();

		public ImageMatricies()
		{

		}

		public List<ImageColorMatrix> Matricies
		{
			get { return this.matricies; }
			set { this.matricies = value; }
		}

		public override void LoadDefaults()
		{

		}

		public override void LoadSettings(object obj)
		{
			ImageMatricies settings = obj as ImageMatricies;
			if (settings != null)
			{
				this.Matricies = settings.Matricies;
			}
		}

		public ImageColorMatrix GetImageMatrix(string matrixName)
		{
			foreach (ImageColorMatrix matrix in this.Matricies)
			{
				if (matrixName == matrix.Name)
					return matrix;
			}
			return null;
		}
	}
}
