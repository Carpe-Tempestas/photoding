using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Trebuchet.Helper_Classes;
using System.Drawing.Imaging;
using Trebuchet.Settings.ImageTypes;
using BaseMatrix = Trebuchet.Settings.ImageTypes;

namespace Trebuchet.Controls
{
	public partial class MatrixControl : UserControl
	{
		private Stack<ImageTransaction> stackUndo = new Stack<ImageTransaction>();
		private Stack<ImageTransaction> stackRedo = new Stack<ImageTransaction>();
		private FastColorMatrix matrix;
		private bool updatingNumValues = false;
		private bool updatingValuesUsedForDefaults = false;
		private bool preventAdd = false;
		private string final = "Combined Effect";
		private ImageTransaction lastTransaction;

		private enum TransactionMode : int
		{
			Single,
			All
		};

		private enum TransactionType : int
		{
			Value,
			Index
		};

		private struct ImageTransaction
		{
			public TransactionMode mode;
			public TransactionType type;
			public ImageColorMatrix matrix;
			public int index;
		}

		public MatrixControl()
		{
			InitializeComponent();
		}


		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			if (!this.DesignMode)
			{
				Initialize();
			}
		}

		public void Initialize()
		{
			this.matrix = FastColorMatrix.GetIdentityMatrix();
			ImageColorMatrix temp = null;
			this.listEffects.Items.Clear();
			this.listEffects.Items.Add(new ImageColorMatrix("Combined Effect", ImageColorMatrix.MatrixTypes.MakeYourOwn, ImageColorMatrix.ValueModes.None));
			foreach (ImageColorMatrix matrix in App.CurAppSettings.ImageAdjSettings.CurrentEffect.Matricies)
			{
				int index = this.listEffects.Items.Count - 1;
				temp = App.ImageMatricies.GetImageMatrix(matrix.Name);
				if (temp == null || String.IsNullOrEmpty(temp.Name))
					this.listEffects.Items.Insert(index, matrix);
				else
					this.listEffects.Items.Insert(index, temp);
				temp = null;
			}
			LoadCustomMatricies();
			UpdateFinal();
			this.listEffects.SelectedIndex = this.listEffects.Items.Count - 1;
		}

		private void btnDeleteCustom_Click(object sender, EventArgs e)
		{
			if (this.comboCustom.SelectedIndex != -1)
			{
				App.ImageMatricies.Matricies.Remove(this.comboCustom.SelectedItem as ImageColorMatrix);
				for (int x = 0; x < this.listEffects.Items.Count; x++)
				{
					if (this.listEffects.Items[x].ToString() == this.comboCustom.SelectedItem.ToString())
					{
						this.listEffects.Items.RemoveAt(x--);
					}
				}
				this.comboCustom.Items.RemoveAt(this.comboCustom.SelectedIndex);
			}
			ApplyMatrix();
			UpdateMatrix();
			if (this.listEffects.SelectedIndex == -1)
			{
				this.listEffects.SelectedIndex = this.listEffects.Items.Count - 1;
			}
			App.TheApp.SaveCustomEffects();
		}

		private string GetMatrixString(ColorMatrix matrix)
		{
			string newline = System.Environment.NewLine;
			return String.Format("{0},{1},{2},{3},{4}" + newline +
				"{5},{6},{7},{8},{9}" + newline +
				"{10},{11},{12},{13},{14}" + newline +
				"{15},{16},{17},{18},{19}" + newline +
				"{20},{21},{22},{23},{24}",
				matrix.Matrix00,
				matrix.Matrix01,
				matrix.Matrix02,
				matrix.Matrix03,
				matrix.Matrix04,
				matrix.Matrix10,
				matrix.Matrix11,
				matrix.Matrix12,
				matrix.Matrix13,
				matrix.Matrix14,
				matrix.Matrix20,
				matrix.Matrix21,
				matrix.Matrix22,
				matrix.Matrix23,
				matrix.Matrix24,
				matrix.Matrix30,
				matrix.Matrix31,
				matrix.Matrix32,
				matrix.Matrix33,
				matrix.Matrix34,
				matrix.Matrix40,
				matrix.Matrix41,
				matrix.Matrix42,
				matrix.Matrix43,
				matrix.Matrix44);
		}

		/// <summary>
		/// Updates the how the matrix is painted on the picture
		/// </summary>
		private void UpdateMatrix()
		{
			UpdateFinal();
			UpdateCurrentMultipleMatrix();
			App.CurAppSettings.FireImageSettings();
		}

		private void UpdateCurrentMultipleMatrix()
		{
			App.CurAppSettings.ImageAdjSettings.CurrentEffect.Matricies.Clear();
			for (int x = 0; x < this.listEffects.Items.Count - 1; x++)
			{
				App.CurAppSettings.ImageAdjSettings.CurrentEffect.Matricies.Add(this.listEffects.Items[x] as ImageColorMatrix);
			}
		}

		/// <summary>
		/// Applies the matrix to the numeric up-downs
		/// </summary>
		private void ApplyMatrix()
		{
			this.numRedRed.Value = (decimal)this.matrix.RR;
			this.numRedGreen.Value = (decimal)this.matrix.RG;
			this.numRedBlue.Value = (decimal)this.matrix.RB;
			this.numRedAlpha.Value = (decimal)this.matrix.RA;
			this.numRedBase.Value = (decimal)this.matrix.RW;
			this.numGreenRed.Value = (decimal)this.matrix.GR;
			this.numGreenGreen.Value = (decimal)this.matrix.GG;
			this.numGreenBlue.Value = (decimal)this.matrix.GB;
			this.numGreenAlpha.Value = (decimal)this.matrix.GA;
			this.numGreenBase.Value = (decimal)this.matrix.GW;
			this.numBlueRed.Value = (decimal)this.matrix.BR;
			this.numBlueGreen.Value = (decimal)this.matrix.BG;
			this.numBlueBlue.Value = (decimal)this.matrix.BB;
			this.numBlueAlpha.Value = (decimal)this.matrix.BA;
			this.numBlueBase.Value = (decimal)this.matrix.BW;
			this.numAlphaRed.Value = (decimal)this.matrix.AR;
			this.numAlphaGreen.Value = (decimal)this.matrix.AG;
			this.numAlphaBlue.Value = (decimal)this.matrix.AB;
			this.numAlphaAlpha.Value = (decimal)this.matrix.AA;
			this.numAlphaBase.Value = (decimal)this.matrix.AW;
			this.numBaseRed.Value = (decimal)this.matrix.WR;
			this.numBaseGreen.Value = (decimal)this.matrix.WG;
			this.numBaseBlue.Value = (decimal)this.matrix.WB;
			this.numBaseAlpha.Value = (decimal)this.matrix.WA;
			this.numBaseBase.Value = (decimal)this.matrix.WW;
		}

		//private void btnRemoveEffect_Click(object sender, EventArgs e)
		//{
		//    FastColorMatrix fastM1 = FastColorMatrix.GetRandomMatrix();
		//    FastColorMatrix fastM2 = FastColorMatrix.GetRandomMatrix();
		//    ColorMatrix colorM1 = FastColorMatrix.ConvertToColorMatrix(fastM1);
		//    ColorMatrix colorM2 = FastColorMatrix.ConvertToColorMatrix(fastM2);

		//    System.Diagnostics.Debug.WriteLine(fastM1);
		//    System.Diagnostics.Debug.WriteLine(fastM2);

		//    DateTime fastMultiplicationBefore = DateTime.Now;
		//    FastColorMatrix fastMultipliedMatrix = fastM1 * fastM2;
		//    DateTime fastMultiplicationAfter = DateTime.Now;


		//    DateTime colorMultiplicationBefore = DateTime.Now;
		//    ColorMatrix colorMultipliedMatrix = App.MultiplyMatrices(colorM1, colorM2);
		//    DateTime colorMultiplicationAfter = DateTime.Now;

		//    TimeSpan fastTime = fastMultiplicationAfter - fastMultiplicationBefore;
		//    TimeSpan colorTime = colorMultiplicationAfter - colorMultiplicationBefore;

		//    FastColorMatrix afterColor = new FastColorMatrix(colorMultipliedMatrix);
		//    if (afterColor == fastMultipliedMatrix)
		//    {
		//        System.Diagnostics.Debug.WriteLine("Matricies are equal!");
		//    }
		//    else
		//    {
		//        System.Diagnostics.Debug.WriteLine(afterColor);
		//        System.Diagnostics.Debug.WriteLine(fastMultipliedMatrix);
		//    }

		//    TimeSpan difference = new TimeSpan();
		//    if (fastTime < colorTime)
		//    {
		//        difference = colorTime - fastTime;
		//        System.Diagnostics.Debug.WriteLine(String.Format("Fast time was faster than color time by: {0} milliseconds", difference.Milliseconds));
		//    }
		//    else
		//    {
		//        difference = fastTime - colorTime;
		//        System.Diagnostics.Debug.WriteLine(String.Format("Color time was faster than fast time by: {0} milliseconds", difference.Milliseconds));
		//    }

		//}

		private void Push(ImageTransaction transaction)
		{
			this.stackRedo.Clear();
			this.lastTransaction = transaction;
			this.stackUndo.Push(transaction);

			SetStackEnabled();
		}

		private ImageTransaction CreateSingleTransaction()
		{
			ImageTransaction transaction = new ImageTransaction();
			transaction.mode = TransactionMode.Single;
			transaction.index = this.listEffects.SelectedIndex;
			transaction.matrix = ((ImageColorMatrix)this.listEffects.SelectedItem).Clone() as ImageColorMatrix;
			return transaction;
		}

		private ImageTransaction CreateValueTransaction()
		{
			ImageTransaction transaction = CreateSingleTransaction();
			transaction.type = TransactionType.Value;
			return transaction;
		}

		private ImageTransaction CreateIndexTransaction()
		{
			ImageTransaction transaction = CreateSingleTransaction();
			transaction.type = TransactionType.Index;
			return transaction;
		}
		
		#region Num Color Matrix Changed Events
		private void numRedRed_ValueChanged(object sender, EventArgs e)
		{
			if (this.updatingNumValues)
				return;

			Push(CreateValueTransaction());
			this.matrix.RR = (float)this.numRedRed.Value;
			UpdateMatrix();
		}

		private void numRedGreen_ValueChanged(object sender, EventArgs e)
		{
			if (this.updatingNumValues)
				return;

			Push(CreateValueTransaction());
			this.matrix.RG = (float)this.numRedGreen.Value;
			UpdateMatrix();
		}

		private void numRedBlue_ValueChanged(object sender, EventArgs e)
		{
			if (this.updatingNumValues)
				return;

			Push(CreateValueTransaction());
			this.matrix.RB = (float)this.numRedBlue.Value;
			UpdateMatrix();
		}

		private void numRedAlpha_ValueChanged(object sender, EventArgs e)
		{
			if (this.updatingNumValues)
				return;

			Push(CreateValueTransaction());
			this.matrix.RA = (float)this.numRedAlpha.Value;
			UpdateMatrix();
		}

		private void numRedBase_ValueChanged(object sender, EventArgs e)
		{
			if (this.updatingNumValues)
				return;

			Push(CreateValueTransaction());
			this.matrix.RW = (float)this.numRedBase.Value;
			UpdateMatrix();
		}

		private void numGreenRed_ValueChanged(object sender, EventArgs e)
		{
			if (this.updatingNumValues)
				return;

			Push(CreateValueTransaction());
			this.matrix.GR = (float)this.numGreenRed.Value;
			UpdateMatrix();
		}

		private void numGreenGreen_ValueChanged(object sender, EventArgs e)
		{
			if (this.updatingNumValues)
				return;

			this.matrix.GG = (float)this.numGreenGreen.Value;
			UpdateMatrix();
		}

		private void numGreenBlue_ValueChanged(object sender, EventArgs e)
		{
			if (this.updatingNumValues)
				return;

			Push(CreateValueTransaction());
			this.matrix.GB = (float)this.numGreenBlue.Value;
			UpdateMatrix();
		}

		private void numGreenAlpha_ValueChanged(object sender, EventArgs e)
		{
			if (this.updatingNumValues)
				return;

			Push(CreateValueTransaction());
			this.matrix.GA = (float)this.numGreenAlpha.Value;
			UpdateMatrix();
		}

		private void numGreenBase_ValueChanged(object sender, EventArgs e)
		{
			if (this.updatingNumValues)
				return;

			Push(CreateValueTransaction());
			this.matrix.GW = (float)this.numGreenBase.Value;
			UpdateMatrix();
		}

		private void numBlueRed_ValueChanged(object sender, EventArgs e)
		{
			if (this.updatingNumValues)
				return;

			Push(CreateValueTransaction());
			this.matrix.BR = (float)this.numBlueRed.Value;
			UpdateMatrix();
		}

		private void numBlueGreen_ValueChanged(object sender, EventArgs e)
		{
			if (this.updatingNumValues)
				return;

			Push(CreateValueTransaction());
			this.matrix.BG = (float)this.numBlueGreen.Value;
			UpdateMatrix();
		}

		private void numBlueBlue_ValueChanged(object sender, EventArgs e)
		{
			if (this.updatingNumValues)
				return;

			Push(CreateValueTransaction());
			this.matrix.BB = (float)this.numBlueBlue.Value;
			UpdateMatrix();
		}

		private void numBlueAlpha_ValueChanged(object sender, EventArgs e)
		{
			if (this.updatingNumValues)
				return;

			Push(CreateValueTransaction());
			this.matrix.BA = (float)this.numBlueAlpha.Value;
			UpdateMatrix();
		}

		private void numBlueBase_ValueChanged(object sender, EventArgs e)
		{
			if (this.updatingNumValues)
				return;

			Push(CreateValueTransaction());
			this.matrix.BW = (float)this.numBlueBase.Value;
			UpdateMatrix();
		}

		private void numAlphaRed_ValueChanged(object sender, EventArgs e)
		{
			if (this.updatingNumValues)
				return;

			Push(CreateValueTransaction());
			this.matrix.AR = (float)this.numAlphaRed.Value;
			UpdateMatrix();
		}

		private void numAlphaGreen_ValueChanged(object sender, EventArgs e)
		{
			if (this.updatingNumValues)
				return;

			Push(CreateValueTransaction());
			this.matrix.AG = (float)this.numAlphaGreen.Value;
			UpdateMatrix();
		}

		private void numAlphaBlue_ValueChanged(object sender, EventArgs e)
		{
			if (this.updatingNumValues)
				return;

			Push(CreateValueTransaction());
			this.matrix.AB = (float)this.numAlphaBlue.Value;
			UpdateMatrix();
		}

		private void numAlphaAlpha_ValueChanged(object sender, EventArgs e)
		{
			if (this.updatingNumValues)
				return;

			Push(CreateValueTransaction());
			this.matrix.AA = (float)this.numAlphaAlpha.Value;
			UpdateMatrix();
		}

		private void numAlphaBase_ValueChanged(object sender, EventArgs e)
		{
			if (this.updatingNumValues)
				return;

			Push(CreateValueTransaction());
			this.matrix.AW = (float)this.numAlphaBase.Value;
			UpdateMatrix();
		}

		private void numBaseRed_ValueChanged(object sender, EventArgs e)
		{
			if (this.updatingNumValues)
				return;

			Push(CreateValueTransaction());
			this.matrix.WR = (float)this.numBaseRed.Value;
			UpdateMatrix();
		}

		private void numBaseGreen_ValueChanged(object sender, EventArgs e)
		{
			if (this.updatingNumValues)
				return;

			Push(CreateValueTransaction());
			this.matrix.WG = (float)this.numBaseGreen.Value;
			UpdateMatrix();
		}

		private void numBaseBlue_ValueChanged(object sender, EventArgs e)
		{
			if (this.updatingNumValues)
				return;

			Push(CreateValueTransaction());
			this.matrix.WB = (float)this.numBaseBlue.Value;
			UpdateMatrix();
		}

		private void numBaseAlpha_ValueChanged(object sender, EventArgs e)
		{
			if (this.updatingNumValues)
				return;

			Push(CreateValueTransaction());
			this.matrix.WA = (float)this.numBaseAlpha.Value;
			UpdateMatrix();
		}

		private void numBaseBase_ValueChanged(object sender, EventArgs e)
		{
			if (this.updatingNumValues)
				return;

			Push(CreateValueTransaction());
			this.matrix.WW = (float)this.numBaseBase.Value;
			UpdateMatrix();
		}
		#endregion Num Color Matrix Changed Events

		private void btnUndo_Click(object sender, EventArgs e)
		{
			if (this.stackUndo.Count == 0)
				return;

			if (this.stackRedo.Count == 0)
				this.stackRedo.Push(CreateGiantTransaction());
			else
				this.stackRedo.Push(this.lastTransaction);
			this.lastTransaction = this.stackUndo.Pop();
			ApplyTransaction(this.lastTransaction);
			SetStackEnabled();
		}

		private void UnravelSingleTransaction(ImageTransaction transaction)
		{
			if (transaction.type == TransactionType.Value)
			{
				UnravelValueSingleTransaction(transaction);
			}
			else if (transaction.type == TransactionType.Index)
			{
				UnravelIndexSingleTransaction(transaction);
			}
			UpdateFinal();
			this.listEffects.SelectedIndex = transaction.index;
		}

		private void UnravelIndexSingleTransaction(ImageTransaction transaction)
		{
			ImageColorMatrix matrix;
			int oldIndex = -1;
			for (int x = 0; x < this.listEffects.Items.Count; x++)
			{
				matrix = this.listEffects.Items[x] as ImageColorMatrix;
				if (matrix.Name == transaction.matrix.Name && matrix.ImageGuid == transaction.matrix.ImageGuid)
				{
					oldIndex = x;
					break;
				}
			}
			if (oldIndex != -1)
			{
				this.listEffects.Items.RemoveAt(oldIndex);
			}
			int modifiedIndex = transaction.index;
			if(modifiedIndex >= this.listEffects.Items.Count-1)
				modifiedIndex = this.listEffects.Items.Count-1;
			this.listEffects.Items.Insert(modifiedIndex, transaction.matrix.Clone());
		}

		private void UnravelValueSingleTransaction(ImageTransaction transaction)
		{
			if (transaction.index < this.listEffects.Items.Count 
				&& this.listEffects.Items[transaction.index].ToString() == transaction.matrix.Name
				&& ((ImageColorMatrix)this.listEffects.Items[transaction.index]).ImageGuid == transaction.matrix.ImageGuid)
			{
				this.listEffects.Items[transaction.index] = transaction.matrix.Clone();
			}
		}

		private void UnravelGiantTransaction(ImageTransaction transaction)
		{
			RemoveCurrentEffects();
			foreach (ImageColorMatrix matrix in transaction.matrix.Matricies)
			{
				if (DoesDefaultExist(matrix.Name) || DoesCustomExist(matrix.Name))
					this.listEffects.Items.Insert(this.listEffects.Items.Count-1, matrix.Clone());
			}
			UpdateFinal();
			this.listEffects.SelectedIndex = transaction.index;
		}

		private ImageTransaction CreateGiantTransaction()
		{
			ImageTransaction transaction = new ImageTransaction();
			transaction.mode = TransactionMode.All;
			transaction.matrix = new ImageColorMatrix();
			transaction.index = this.listEffects.SelectedIndex;
			foreach (object obj in this.listEffects.Items)
			{
				if(obj.ToString() != this.final)
					transaction.matrix.Matricies.Add(((ImageColorMatrix)obj).Clone() as ImageColorMatrix);
			}
			return transaction;
		}

		private void btnRedo_Click(object sender, EventArgs e)
		{
			if (this.stackRedo.Count == 0)
				return;

			this.stackUndo.Push(this.lastTransaction);
			this.lastTransaction = this.stackRedo.Pop();
			ApplyTransaction(this.lastTransaction);
			SetStackEnabled();
		}

		private void ApplyTransaction(ImageTransaction transaction)
		{
			if (transaction.mode == TransactionMode.All)
				UnravelGiantTransaction(transaction);
			else
				UnravelSingleTransaction(transaction);
		}

		private void SetStackEnabled()
		{
			if (this.stackRedo.Count == 0)
				this.btnRedo.Enabled = false;
			else
				this.btnRedo.Enabled = true;

			if (this.stackUndo.Count == 0)
				this.btnUndo.Enabled = false;
			else
				this.btnUndo.Enabled = true;
		}

		private void comboDefaults_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (this.comboDefaults.SelectedIndex == -1)
			{
				UpdateValueControls();
				return;
			}
			else if (this.preventAdd)
			{
				return;
			}

			Push(CreateGiantTransaction());
			int index = this.listEffects.Items.Count - 1;
			this.listEffects.Items.Insert(index, GetNewDefaultMatrix());
			UpdateMatrix();
			this.listEffects.SelectedIndex = index;
			UpdateValueControls();
		}

		private object GetNewDefaultMatrix()
		{
			ImageColorMatrix newMatrix = null;
			if (this.comboDefaults.Text == this.comboDefaults.Items[0].ToString())
			{
				newMatrix = new ImageColorMatrix(this.comboDefaults.Items[0].ToString(),
					ImageColorMatrix.MatrixTypes.Brightness, ImageColorMatrix.ValueModes.Single);
			}
			else if(this.comboDefaults.Text == this.comboDefaults.Items[1].ToString())
			{
				newMatrix = new ImageColorMatrix(this.comboDefaults.Items[1].ToString(),
					ImageColorMatrix.MatrixTypes.BrightnessEx, ImageColorMatrix.ValueModes.Triple);
			}
			else if(this.comboDefaults.Text == this.comboDefaults.Items[2].ToString())
			{
				newMatrix = new ImageColorMatrix(this.comboDefaults.Items[2].ToString(),
					ImageColorMatrix.MatrixTypes.Contrast, ImageColorMatrix.ValueModes.Single);
			}
			else if(this.comboDefaults.Text == this.comboDefaults.Items[3].ToString())
			{
				newMatrix = new ImageColorMatrix(this.comboDefaults.Items[3].ToString(),
					ImageColorMatrix.MatrixTypes.ContrastEx, ImageColorMatrix.ValueModes.Triple);
			}
			else if(this.comboDefaults.Text == this.comboDefaults.Items[4].ToString())
			{
				newMatrix = new ImageColorMatrix(this.comboDefaults.Items[4].ToString(),
					ImageColorMatrix.MatrixTypes.Saturation, ImageColorMatrix.ValueModes.Single);
			}
			else if(this.comboDefaults.Text == this.comboDefaults.Items[5].ToString())
			{
				newMatrix = new ImageColorMatrix(this.comboDefaults.Items[5].ToString(), 
					ImageColorMatrix.MatrixTypes.SaturationEx, ImageColorMatrix.ValueModes.Triple);
			}
			else if(this.comboDefaults.Text == this.comboDefaults.Items[6].ToString())
			{
				newMatrix = new ImageColorMatrix(this.comboDefaults.Items[6].ToString(), 
					ImageColorMatrix.MatrixTypes.Sepia, ImageColorMatrix.ValueModes.None);
			}
			else if(this.comboDefaults.Text == this.comboDefaults.Items[7].ToString())
			{
				newMatrix = new ImageColorMatrix(this.comboDefaults.Items[7].ToString(),
					ImageColorMatrix.MatrixTypes.Greyscale, ImageColorMatrix.ValueModes.None);
			}
			else if(this.comboDefaults.Text == this.comboDefaults.Items[8].ToString())
			{
				newMatrix = new ImageColorMatrix(this.comboDefaults.Items[8].ToString(),
					ImageColorMatrix.MatrixTypes.Negative, ImageColorMatrix.ValueModes.None);
			}
			else if (this.comboDefaults.Text == this.comboDefaults.Items[9].ToString())
			{
				newMatrix = new ImageColorMatrix(this.comboDefaults.Items[9].ToString(),
					ImageColorMatrix.MatrixTypes.Invert, ImageColorMatrix.ValueModes.All);
            }
            else if (this.comboDefaults.Text == this.comboDefaults.Items[10].ToString())
            {
                newMatrix = new ImageColorMatrix(this.comboDefaults.Items[10].ToString(),
                    ImageColorMatrix.MatrixTypes.MakeYourOwn, ImageColorMatrix.ValueModes.All);
            }
			else
			{
				throw new NotImplementedException("This default effect has not been implemented");
			}
			return newMatrix;
		}

		private void UpdateValueControls()
		{
			if (this.listEffects.SelectedItem is ISingleValue
				&& ((ImageColorMatrix)this.listEffects.SelectedItem).ValueMode == ImageColorMatrix.ValueModes.Single)
			{
				this.updatingValuesUsedForDefaults = true;
				PrepareNUDForChange(this.numSingleValue, (ISingleValue)this.listEffects.SelectedItem);
				this.numSingleValue.Value = ((ISingleValue)this.listEffects.SelectedItem).Value;
				this.numSingleValue.Minimum = ((ISingleValue)this.listEffects.SelectedItem).Minimum;
				this.numSingleValue.Maximum = ((ISingleValue)this.listEffects.SelectedItem).Maximum;
				this.updatingValuesUsedForDefaults = false;

				this.numSingleValue.Visible = true;
				this.numTripleValueR.Visible = false;
				this.numTripleValueG.Visible = false;
				this.numTripleValueB.Visible = false;
			}
			else if (this.listEffects.SelectedItem is ITripleValue
				&& ((ImageColorMatrix)this.listEffects.SelectedItem).ValueMode == ImageColorMatrix.ValueModes.Triple)
			{
				this.updatingValuesUsedForDefaults = true;
				PrepareNUDForChange(this.numTripleValueR, this.numTripleValueG, this.numTripleValueB,
					(ITripleValue)this.listEffects.SelectedItem);
				this.numTripleValueR.Value = ((ITripleValue)this.listEffects.SelectedItem).ValueR;
				this.numTripleValueG.Value = ((ITripleValue)this.listEffects.SelectedItem).ValueG;
				this.numTripleValueB.Value = ((ITripleValue)this.listEffects.SelectedItem).ValueB;
				this.numTripleValueR.Minimum = ((ITripleValue)this.listEffects.SelectedItem).Minimum;
				this.numTripleValueR.Maximum = ((ITripleValue)this.listEffects.SelectedItem).Maximum;
				this.numTripleValueG.Minimum = ((ITripleValue)this.listEffects.SelectedItem).Minimum;
				this.numTripleValueG.Maximum = ((ITripleValue)this.listEffects.SelectedItem).Maximum;
				this.numTripleValueB.Minimum = ((ITripleValue)this.listEffects.SelectedItem).Minimum;
				this.numTripleValueB.Maximum = ((ITripleValue)this.listEffects.SelectedItem).Maximum;
				this.updatingValuesUsedForDefaults = false;

				this.numSingleValue.Visible = false;
				this.numTripleValueR.Visible = true;
				this.numTripleValueG.Visible = true;
				this.numTripleValueB.Visible = true;
			}
			else
			{
				this.numSingleValue.Visible = false;
				this.numTripleValueR.Visible = false;
				this.numTripleValueG.Visible = false;
				this.numTripleValueB.Visible = false;
			}
		}

		private void PrepareNUDForChange(NumericUpDown numR, NumericUpDown numG, NumericUpDown numB, ITripleValue tripValue)
		{
			if (numR.Minimum > tripValue.Minimum)
			{
				numR.Minimum = tripValue.Minimum;
				numG.Minimum = tripValue.Minimum;
				numB.Minimum = tripValue.Minimum;
			}


			if (numR.Maximum < tripValue.Maximum)
			{
				numR.Maximum = tripValue.Maximum;
				numG.Maximum = tripValue.Maximum;
				numB.Maximum = tripValue.Maximum;
			}
		}

		private void PrepareNUDForChange(NumericUpDown num, ISingleValue singleValue)
		{
			if (num.Minimum > singleValue.Minimum)
			{
				num.Minimum = singleValue.Minimum;
			}


			if (num.Maximum < singleValue.Maximum)
			{
				num.Maximum = singleValue.Maximum;
			}
		}

		private void comboCustom_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (this.comboCustom.SelectedIndex == -1)
				return;

			if (this.preventAdd)
			{
				return;
			}

			Push(CreateGiantTransaction());
			int index = this.listEffects.Items.Count - 1;
			this.listEffects.Items.Insert(index, App.ImageMatricies.GetImageMatrix(this.comboCustom.SelectedItem.ToString()).Clone());
			UpdateMatrix();
			this.listEffects.SelectedIndex = index;
		}

		private void listEffects_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (this.listEffects.SelectedItem == null)
				return;
			else if (this.comboDefaults.Items.Contains(this.listEffects.SelectedItem.ToString()))
			{
				this.preventAdd = true;
				this.comboDefaults.SelectedIndex = this.comboDefaults.Items.IndexOf(this.listEffects.SelectedItem.ToString());
				this.comboCustom.SelectedIndex = -1;
				this.preventAdd = false;
				UpdateValueControls();
			}
			else if (this.comboCustom.Items.Contains(this.listEffects.SelectedItem.ToString()))
			{
				this.preventAdd = true;
				this.comboCustom.SelectedIndex = this.comboCustom.Items.IndexOf(this.listEffects.SelectedItem.ToString());
				this.comboDefaults.SelectedIndex = -1;
				this.preventAdd = false;
				UpdateValueControls();
			}
			else
			{
				this.comboDefaults.SelectedIndex = -1;
				this.comboCustom.SelectedIndex = -1;
				UpdateValueControls();
			}

			this.matrix = ((ImageColorMatrix)this.listEffects.SelectedItem).ColMatrix;
			this.updatingNumValues = true;
			ApplyMatrix();
			this.updatingNumValues = false;

			SetEffectsButtonsEnabled();
			SetEffectsMatrixEnabled(((ImageColorMatrix)this.listEffects.SelectedItem).GetEnabledStatus());
		}

		private void SetEffectsMatrixEnabled(bool[] enabledStatus)
		{
			int index = 0;

			this.numRedRed.Enabled = enabledStatus[index++];
			this.numRedGreen.Enabled = enabledStatus[index++];
			this.numRedBlue.Enabled = enabledStatus[index++];
			this.numRedAlpha.Enabled = enabledStatus[index++];
			this.numRedBase.Enabled = enabledStatus[index++];
			this.numGreenRed.Enabled = enabledStatus[index++];
			this.numGreenGreen.Enabled = enabledStatus[index++];
			this.numGreenBlue.Enabled = enabledStatus[index++];
			this.numGreenAlpha.Enabled = enabledStatus[index++];
			this.numGreenBase.Enabled = enabledStatus[index++];
			this.numBlueRed.Enabled = enabledStatus[index++];
			this.numBlueGreen.Enabled = enabledStatus[index++];
			this.numBlueBlue.Enabled = enabledStatus[index++];
			this.numBlueAlpha.Enabled = enabledStatus[index++];
			this.numBlueBase.Enabled = enabledStatus[index++];
			this.numAlphaRed.Enabled = enabledStatus[index++];
			this.numAlphaGreen.Enabled = enabledStatus[index++];
			this.numAlphaBlue.Enabled = enabledStatus[index++];
			this.numAlphaAlpha.Enabled = enabledStatus[index++];
			this.numAlphaBase.Enabled = enabledStatus[index++];
			this.numBaseRed.Enabled = enabledStatus[index++];
			this.numBaseGreen.Enabled = enabledStatus[index++];
			this.numBaseBlue.Enabled = enabledStatus[index++];
			this.numBaseAlpha.Enabled = enabledStatus[index++];
			this.numBaseBase.Enabled = enabledStatus[index++];
		}

		private void SetEffectsButtonsEnabled()
		{
			if (this.listEffects.SelectedItem.ToString() == this.final)
			{
				this.btnUp.Enabled = false;
				this.btnDown.Enabled = false;
				this.btnRemoveEffect.Enabled = false;
			}
			else
			{
				if (this.listEffects.SelectedIndex - 1 >= 0)
					this.btnUp.Enabled = true;
				else
					this.btnUp.Enabled = false;

				if (this.listEffects.SelectedIndex + 1 < this.listEffects.Items.Count - 1)
					this.btnDown.Enabled = true;
				else
					this.btnDown.Enabled = false;
				this.btnRemoveEffect.Enabled = true;
			}
		}

		private void UpdateFinal()
		{
			FastColorMatrix matrix = FastColorMatrix.GetIdentityMatrix();
			ImageColorMatrix temp = null;
			for(int x = 0; x < this.listEffects.Items.Count-1; x++)
			{
				temp = this.listEffects.Items[x] as ImageColorMatrix;
				if (temp != null)
				{
					matrix = matrix * temp.ColMatrix;
				}
			}
			((ImageColorMatrix)this.listEffects.Items[this.listEffects.Items.Count - 1]).ColMatrix = matrix;

			if (this.listEffects.SelectedItem != null && this.listEffects.SelectedItem.ToString() == this.final)
				ApplyMatrix();
		}

		private void btnRemoveEffect_Click(object sender, EventArgs e)
		{
			int index = this.listEffects.SelectedIndex;
			if (this.listEffects.SelectedItem != null && this.listEffects.SelectedItem.ToString() != this.final)
			{
				Push(CreateIndexTransaction());
				this.listEffects.Items.Remove(this.listEffects.SelectedItem);
				UpdateMatrix();
				if (index <= this.listEffects.Items.Count - 1)
					this.listEffects.SelectedIndex = index;
				else
					this.listEffects.SelectedIndex = 0;
			}
		}

		private void btnUp_Click(object sender, EventArgs e)
		{
			int index = this.listEffects.SelectedIndex;
			if (this.listEffects.SelectedItem != null && this.listEffects.SelectedItem.ToString() != this.final && index - 1 >= 0)
			{
				Push(CreateIndexTransaction());
				object obj = this.listEffects.SelectedItem;
				this.listEffects.Items.Remove(obj);
				this.listEffects.Items.Insert(index - 1, obj);
				UpdateMatrix();
				this.listEffects.SelectedIndex = index - 1;
			}
		}

		private void btnDown_Click(object sender, EventArgs e)
		{
			int index = this.listEffects.SelectedIndex;
			if (this.listEffects.SelectedItem != null && this.listEffects.SelectedItem.ToString() != this.final && index + 1 < this.listEffects.Items.Count - 1)
			{
				Push(CreateIndexTransaction());
				object obj = this.listEffects.SelectedItem;
				this.listEffects.Items.Remove(obj);
				this.listEffects.Items.Insert(index + 1, obj);
				UpdateMatrix();
				this.listEffects.SelectedIndex = index + 1;
			}
		}

		private void txtCustomName_TextChanged(object sender, EventArgs e)
		{
			if (String.IsNullOrEmpty(this.txtCustomName.Text) || DoesDefaultExist(this.txtCustomName.Text) 
				|| DoesCustomExist(this.txtCustomName.Text) || this.txtCustomName.Text == this.final || this.txtCustomName.Text == "Default")
				this.btnSaveCustom.Enabled = false;
			else
				this.btnSaveCustom.Enabled = true;
		}

		private bool DoesCustomExist(string name)
		{
			foreach (object obj in this.comboCustom.Items)
			{
				if (obj.ToString() == name)
					return true;
			}
			return false;
		}

		private bool DoesDefaultExist(string name)
		{
			foreach (object obj in this.comboDefaults.Items)
			{
				if (obj.ToString() == name)
					return true;
			}
			return false;
		}

		private void numSingleValue_ValueChanged(object sender, EventArgs e)
		{
			if (updatingValuesUsedForDefaults)
				return;

			ISingleValue single = this.listEffects.SelectedItem as ISingleValue;
			if (single == null)
				return;

			Push(CreateValueTransaction());
			this.updatingNumValues = true;
			single.Value = this.numSingleValue.Value;
			this.matrix.ColorMatrix = single.ColMatrix.ColorMatrix;
			ApplyMatrix();
			this.updatingNumValues = false;
			UpdateMatrix();
		}

		private void numTripleValueR_ValueChanged(object sender, EventArgs e)
		{
			SetUsingTriple();
		}

		private void numTripleValueG_ValueChanged(object sender, EventArgs e)
		{
			SetUsingTriple();
		}

		private void numTripleValueB_ValueChanged(object sender, EventArgs e)
		{
			SetUsingTriple();
		}

		private void SetUsingTriple()
		{
			if (updatingValuesUsedForDefaults)
				return;

			ITripleValue triple = this.listEffects.SelectedItem as ITripleValue;
			if (triple == null)
				return;

			Push(CreateValueTransaction());
			this.updatingNumValues = true;
			triple.ValueR = this.numTripleValueR.Value;
			triple.ValueG = this.numTripleValueG.Value;
			triple.ValueB = this.numTripleValueB.Value;
			this.matrix.ColorMatrix = triple.ColMatrix.ColorMatrix;
			ApplyMatrix();
			this.updatingNumValues = false;
			UpdateMatrix();
		}

		private void btnSaveCustom_Click(object sender, EventArgs e)
		{
			ImageColorMatrix matrix = CreateNewCustomMatrix();
			App.ImageMatricies.Matricies.Add(matrix);
			LoadCustomMatricies();
			RemoveCurrentEffects();
			ApplyMatrix();
			UpdateMatrix();
			this.listEffects.Items.Insert(this.listEffects.Items.Count - 1, App.ImageMatricies.Matricies[App.ImageMatricies.Matricies.Count - 1]);
			this.listEffects.SelectedIndex = this.listEffects.Items.IndexOf(App.ImageMatricies.Matricies[App.ImageMatricies.Matricies.Count - 1]);
			this.txtCustomName.Text = "";
			App.TheApp.SaveCustomEffects();
		}

		private void RemoveCurrentEffects()
		{
			while (this.listEffects.Items.Count != 1)
			{
				this.listEffects.Items.RemoveAt(0);
			}
		}

		private ImageColorMatrix CreateNewCustomMatrix()
		{
			ImageColorMatrix custom = new ImageColorMatrix(this.txtCustomName.Text, ImageColorMatrix.MatrixTypes.Custom, ImageColorMatrix.ValueModes.None);
			for (int x = 0; x < this.listEffects.Items.Count - 1; x++)
			{
				custom.Matricies.Add((ImageColorMatrix)this.listEffects.Items[x]);
			}
			return custom;
		}

		private void LoadCustomMatricies()
		{
			this.comboCustom.Items.Clear();
			foreach (ImageColorMatrix matrix in App.ImageMatricies.Matricies)
			{
				this.comboCustom.Items.Add(matrix.Name);
			}
		}
	}
}
