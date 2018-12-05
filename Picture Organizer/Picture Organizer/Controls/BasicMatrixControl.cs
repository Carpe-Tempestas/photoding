using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Trebuchet.Settings.ImageTypes;

namespace Trebuchet.Controls
{
	public partial class BasicMatrixControl : UserControl
	{
		private ImageColorMatrix matrixBrightness = null;
		private ImageColorMatrix matrixContrast = null;
		private ImageColorMatrix matrixSaturation = null;
		private ImageColorMatrix matrixSepia = null;
		private ImageColorMatrix matrixGrayscale = null;
		private ImageColorMatrix matrixNegative = null;
		private bool preventChanges = false;

		public BasicMatrixControl()
		{
			InitializeComponent();

			ResetEffects();
		}

		private void ResetEffects()
		{
			this.preventChanges = true;
			this.matrixBrightness = new ImageColorMatrix("Brightness", ImageColorMatrix.MatrixTypes.Brightness, ImageColorMatrix.ValueModes.Single);
			this.trackBrightness.Minimum = (int)(this.matrixBrightness.Minimum * 1000);
			this.trackBrightness.Maximum = (int)(this.matrixBrightness.Maximum * 1000);
			this.trackBrightness.Value = (int)(this.matrixBrightness.Value * 1000);
			this.numBrightness.Minimum = this.matrixBrightness.Minimum;
			this.numBrightness.Maximum = this.matrixBrightness.Maximum;
			this.numBrightness.Value = this.matrixBrightness.Value;
			this.matrixContrast = new ImageColorMatrix("Contrast", ImageColorMatrix.MatrixTypes.Contrast, ImageColorMatrix.ValueModes.Single);
			this.trackContrast.Minimum = (int)(this.matrixContrast.Minimum * 1000);
			this.trackContrast.Maximum = (int)(this.matrixContrast.Maximum * 1000);
			this.trackContrast.Value = (int)(this.matrixContrast.Value * 1000);
			this.numContrast.Minimum = this.matrixContrast.Minimum;
			this.numContrast.Maximum = this.matrixContrast.Maximum;
			this.numContrast.Value = this.matrixContrast.Value;
			this.matrixSaturation = new ImageColorMatrix("Saturation", ImageColorMatrix.MatrixTypes.Saturation, ImageColorMatrix.ValueModes.Single);
			this.trackSaturation.Minimum = (int)(this.matrixSaturation.Minimum * 1000);
			this.trackSaturation.Maximum = (int)(this.matrixSaturation.Maximum * 1000);
			this.trackSaturation.Value = (int)(this.matrixSaturation.Value * 1000);
			this.numSaturation.Minimum = this.matrixSaturation.Minimum;
			this.numSaturation.Maximum = this.matrixSaturation.Maximum;
			this.numSaturation.Value = this.matrixSaturation.Value;
			this.matrixSepia = new ImageColorMatrix("Sepia", ImageColorMatrix.MatrixTypes.Sepia, ImageColorMatrix.ValueModes.None);
			this.chkSepia.Checked = false;
			this.matrixGrayscale = new ImageColorMatrix("Grayscale", ImageColorMatrix.MatrixTypes.Greyscale, ImageColorMatrix.ValueModes.None);
			this.chkGrayscale.Checked = false;
			this.matrixNegative = new ImageColorMatrix("Negative", ImageColorMatrix.MatrixTypes.Negative, ImageColorMatrix.ValueModes.None);
			this.chkNegative.Checked = false;
			this.preventChanges = false;
		}

		public void Initialize()
		{
			ResetEffects();

			this.preventChanges = true;
			foreach (ImageColorMatrix matrix in App.CurAppSettings.ImageAdjSettings.CurrentEffect.Matricies)
			{
				if (matrix.MatrixType == ImageColorMatrix.MatrixTypes.Brightness)
				{
					this.matrixBrightness.Value = matrix.Value;
					this.trackBrightness.Value = (int)(this.matrixBrightness.Value * 1000);
				}
				else if (matrix.MatrixType == ImageColorMatrix.MatrixTypes.Contrast)
				{
					this.matrixContrast.Value = matrix.Value;
					this.trackContrast.Value = (int)(this.matrixContrast.Value * 1000);
				}
				else if (matrix.MatrixType == ImageColorMatrix.MatrixTypes.Saturation)
				{
					this.matrixSaturation.Value = matrix.Value;
					this.trackSaturation.Value = (int)(this.matrixSaturation.Value * 1000);
				}
				else if (matrix.MatrixType == ImageColorMatrix.MatrixTypes.Sepia)
					this.chkSepia.Checked = true;
				else if (matrix.MatrixType == ImageColorMatrix.MatrixTypes.Greyscale)
					this.chkGrayscale.Checked = true;
				else if (matrix.MatrixType == ImageColorMatrix.MatrixTypes.Negative)
					this.chkNegative.Checked = true;
			}
			this.preventChanges = false;
		}

		private void trackBrightness_Scroll(object sender, EventArgs e)
		{
			this.numBrightness.Value = (decimal)this.trackBrightness.Value / (decimal)1000.0;
		}

		private void trackContrast_Scroll(object sender, EventArgs e)
		{
			this.numContrast.Value = (decimal)this.trackContrast.Value / (decimal)1000.0;
		}

		private void trackSaturation_Scroll(object sender, EventArgs e)
		{
			this.numSaturation.Value = (decimal)this.trackSaturation.Value / (decimal)1000.0;
		}

		private void numBrightness_ValueChanged(object sender, EventArgs e)
		{
			if (!this.preventChanges)
			{
				UpdateMatrix(ImageColorMatrix.MatrixTypes.Brightness, this.numBrightness.Value);
				App.CurAppSettings.FireImageSettings();
			}
		}

		private void numContrast_ValueChanged(object sender, EventArgs e)
		{
			if (!this.preventChanges)
			{
				UpdateMatrix(ImageColorMatrix.MatrixTypes.Contrast, this.numContrast.Value);
				App.CurAppSettings.FireImageSettings();
			}
		}

		private void numSaturation_ValueChanged(object sender, EventArgs e)
		{
			if (!this.preventChanges)
			{
				UpdateMatrix(ImageColorMatrix.MatrixTypes.Saturation, this.numSaturation.Value);
				App.CurAppSettings.FireImageSettings();
			}
		}

		private void UpdateMatrix(ImageColorMatrix.MatrixTypes matrixType, decimal newValue)
		{
			ImageColorMatrix target = null;
			foreach (ImageColorMatrix matrix in App.CurAppSettings.ImageAdjSettings.CurrentEffect.Matricies)
			{
				if (matrix.MatrixType == matrixType)
				{
					target = matrix;
					break;
				}
			}

			if (target == null)
			{
				target = new ImageColorMatrix(matrixType.ToString(), matrixType, ImageColorMatrix.ValueModes.Single);
				target.Value = newValue;
				App.CurAppSettings.ImageAdjSettings.CurrentEffect.Matricies.Add(target);
			}
			else
			{
				target.Value = newValue;
			}
		}

		private void btnResetBrightness_Click(object sender, EventArgs e)
		{
			this.matrixBrightness = new ImageColorMatrix("Brightness", ImageColorMatrix.MatrixTypes.Brightness, ImageColorMatrix.ValueModes.Single);
			this.trackBrightness.Value = (int)(this.matrixBrightness.Value * 1000);
			this.numBrightness.Value = this.matrixBrightness.Value;
			RemoveEffect(ImageColorMatrix.MatrixTypes.Brightness);
			App.CurAppSettings.FireImageSettings();
		}

		private void btnResetContrast_Click(object sender, EventArgs e)
		{
			this.matrixContrast = new ImageColorMatrix("Contrast", ImageColorMatrix.MatrixTypes.Contrast, ImageColorMatrix.ValueModes.Single);
			this.trackContrast.Value = (int)(this.matrixContrast.Value * 1000);
			this.numContrast.Value = this.matrixContrast.Value;
			RemoveEffect(ImageColorMatrix.MatrixTypes.Contrast);
			App.CurAppSettings.FireImageSettings();
		}

		private void btnResetSaturation_Click(object sender, EventArgs e)
		{
			this.matrixSaturation = new ImageColorMatrix("Saturation", ImageColorMatrix.MatrixTypes.Saturation, ImageColorMatrix.ValueModes.Single);
			this.trackSaturation.Value = (int)(this.matrixSaturation.Value * 1000);
			this.numSaturation.Value = this.matrixSaturation.Value;
			RemoveEffect(ImageColorMatrix.MatrixTypes.Saturation);
			App.CurAppSettings.FireImageSettings();
		}

		private void chkSepia_CheckedChanged(object sender, EventArgs e)
		{
			if (this.preventChanges)
				return;

			if (this.chkSepia.Checked)
			{
				ImageColorMatrix matrix = new ImageColorMatrix("Sepia", ImageColorMatrix.MatrixTypes.Sepia, ImageColorMatrix.ValueModes.None);
				App.CurAppSettings.ImageAdjSettings.CurrentEffect.Matricies.Add(matrix);
			}
			else
			{
				RemoveEffect(ImageColorMatrix.MatrixTypes.Sepia);
			}
			App.CurAppSettings.FireImageSettings();
		}

		private void chkGrayscale_CheckedChanged(object sender, EventArgs e)
		{			
			if (this.preventChanges)
				return;

			if (this.chkGrayscale.Checked)
			{
				ImageColorMatrix matrix = new ImageColorMatrix("Grayscale", ImageColorMatrix.MatrixTypes.Greyscale, ImageColorMatrix.ValueModes.None);
				App.CurAppSettings.ImageAdjSettings.CurrentEffect.Matricies.Add(matrix);
			}
			else
			{
				RemoveEffect(ImageColorMatrix.MatrixTypes.Greyscale);
			}
			App.CurAppSettings.FireImageSettings();
		}

		private void chkNegative_CheckedChanged(object sender, EventArgs e)
		{
			if (this.preventChanges)
				return;

			if (this.chkNegative.Checked)
			{
				ImageColorMatrix matrix = new ImageColorMatrix("Negative", ImageColorMatrix.MatrixTypes.Negative, ImageColorMatrix.ValueModes.None);
				App.CurAppSettings.ImageAdjSettings.CurrentEffect.Matricies.Add(matrix);
			}
			else
			{
				RemoveEffect(ImageColorMatrix.MatrixTypes.Negative);
			}
			App.CurAppSettings.FireImageSettings();
		}

		private void RemoveEffect(ImageColorMatrix.MatrixTypes matrixType)
		{
			for (int x = 0; x < App.CurAppSettings.ImageAdjSettings.CurrentEffect.Matricies.Count; x++)
			{
				if (App.CurAppSettings.ImageAdjSettings.CurrentEffect.Matricies[x].MatrixType == matrixType)
				{
					App.CurAppSettings.ImageAdjSettings.CurrentEffect.Matricies.Remove(App.CurAppSettings.ImageAdjSettings.CurrentEffect.Matricies[x]);
					break;
				}
			}
		}

        private void chkInvert_CheckedChanged(object sender, EventArgs e)
        {
            if (this.preventChanges)
                return;

            if (this.chkInvert.Checked)
            {
                ImageColorMatrix matrix = new ImageColorMatrix("Invert", ImageColorMatrix.MatrixTypes.Invert, ImageColorMatrix.ValueModes.None);
                App.CurAppSettings.ImageAdjSettings.CurrentEffect.Matricies.Add(matrix);
            }
            else
            {
                RemoveEffect(ImageColorMatrix.MatrixTypes.Invert);
            }
            App.CurAppSettings.FireImageSettings();
        }
	}
}
