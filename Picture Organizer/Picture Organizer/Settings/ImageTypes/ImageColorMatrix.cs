using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using System.Drawing.Imaging;
using Trebuchet.Helper_Classes;
using System.Xml;
using System.Windows.Forms;

namespace Trebuchet.Settings.ImageTypes
{
	public class ImageColorMatrix : IXmlSerializable, ICloneable, ISingleValue, ITripleValue, IMultipleMatricies
	{
		private FastColorMatrix matrix = new FastColorMatrix();
		private string name;
		private MatrixTypes matrixType;
		private ValueModes valueMode;
		private List<ImageColorMatrix> matricies = new List<ImageColorMatrix>();
		private Guid guid = Guid.NewGuid();

		public enum MatrixTypes : int
		{
			Custom,
			MakeYourOwn,
			Brightness,
			BrightnessEx,
			Contrast,
			ContrastEx,
			Saturation,
			SaturationEx,
			Sepia,
			Greyscale,
			Negative,
            Invert
		};

		public enum ValueModes : int
		{
			None,
			Single,
			Triple,
			All
		};

		public ImageColorMatrix()
		{
			this.ColMatrix = FastColorMatrix.GetIdentityMatrix();
		}

		public ImageColorMatrix(string name, MatrixTypes type, ValueModes mode)
		{
			this.Name = name;
			this.MatrixType = type;
			this.ValueMode = mode;

			this.ColMatrix = FastColorMatrix.GetIdentityMatrix();

			InitializeMatrixValues();
		}

		private void InitializeMatrixValues()
		{
			if (this.MatrixType == MatrixTypes.Brightness || this.MatrixType == MatrixTypes.BrightnessEx)
			{
				this.Value = (decimal)((0.000f + this.Modifier) / this.Scale * (float)this.Maximum);
				this.ValueR = (decimal)((0.000f + this.Modifier) / this.Scale * (float)this.Maximum);
				this.ValueG = (decimal)((0.000f + this.Modifier) / this.Scale * (float)this.Maximum);
				this.ValueB = (decimal)((0.000f + this.Modifier) / this.Scale * (float)this.Maximum);
			}
			else if (this.MatrixType == MatrixTypes.Contrast || this.MatrixType == MatrixTypes.ContrastEx)
			{
				this.Value = (decimal)((1.000f + this.Modifier) / this.Scale * (float)this.Maximum);
				this.ValueR = (decimal)((1.000f + this.Modifier) / this.Scale * (float)this.Maximum);
				this.ValueG = (decimal)((1.000f + this.Modifier) / this.Scale * (float)this.Maximum);
				this.ValueB = (decimal)((1.000f + this.Modifier) / this.Scale * (float)this.Maximum);
			}
			else if (this.MatrixType == MatrixTypes.Saturation || this.MatrixType == MatrixTypes.SaturationEx)
			{
				this.Value = 255;
				this.ValueR = 255;
				this.ValueG = 255;
				this.ValueB = 255;
			}
		}

		public bool InUse
		{
			get
			{
				if (this.Matricies.Count > 0)
					return true;
				else
					return false;
			}
		}

		public Guid ImageGuid
		{
			get { return this.guid; }
			set { this.guid = value; }
		}

		public List<ImageColorMatrix> Matricies
		{
			get { return this.matricies; }
			set { this.matricies = value; }
		}

		public override string ToString()
		{
			return this.Name;
		}

		public MatrixTypes MatrixType
		{
			get { return this.matrixType; }
			set { this.matrixType = value; }
		}

		public ValueModes ValueMode
		{
			get { return this.valueMode; }
			set { this.valueMode = value; }
		}

		[XmlIgnore()]
		public FastColorMatrix ColMatrix
		{
			get 
			{
				if (this.MatrixType == MatrixTypes.Custom)
				{
					return ((IMultipleMatricies)this).MultiplyMultipleMatricies();
				}
				else if (this.MatrixType == MatrixTypes.Greyscale)
				{
					return new FastColorMatrix(
						new float[5, 5]
						{
							{0.3f, 0.3f, 0.3f, 0.0f, 0.0f},
							{0.59f, 0.59f, 0.59f, 0.0f, 0.0f},
							{0.11f, 0.11f, 0.11f, 0.0f, 0.0f},
							{0.0f, 0.0f, 0.0f, 1.0f, 0.0f},
							{0.0f, 0.0f, 0.0f, 0.0f, 1.0f}
						});
				}
				else if (this.MatrixType == MatrixTypes.Sepia)
				{
					return new FastColorMatrix(
						new float[5, 5]
						{
							{0.393f, 0.349f, 0.272f, 0.0f, 0.0f},
							{0.769f, 0.686f, 0.543f, 0.0f, 0.0f},
							{0.189f, 0.168f, 0.131f, 0.0f, 0.0f},
							{0.0f, 0.0f, 0.0f, 1.0f, 0.0f},
							{0.0f, 0.0f, 0.0f, 0.0f, 1.0f}
						});
				}
				else if (this.MatrixType == MatrixTypes.Negative)
				{
					return new FastColorMatrix(
						   new float[5, 5]
						{
							{-1.0f, 0.0f, 0.0f, 0.0f, 0.0f},
							{0.0f, -1.0f, 0.0f, 0.0f, 0.0f},
							{0.0f, 0.0f, -1.0f, 0.0f, 0.0f},
							{0.0f, 0.0f, 0.0f, 1.0f, 0.0f},
							{0.0f, 0.0f, 0.0f, 0.0f, 1.0f}
						});
                }
                else if (this.MatrixType == MatrixTypes.Invert)
                {
                    return new FastColorMatrix(
                           new float[5, 5]
						{
							{-1.0f, 0.0f, 0.0f, 0.0f, 0.0f},
							{0.0f, -1.0f, 0.0f, 0.0f, 0.0f},
							{0.0f, 0.0f, -1.0f, 0.0f, 0.0f},
							{0.0f, 0.0f, 0.0f, 1.0f, 0.0f},
							{1.0f, 1.0f, 1.0f, 0.0f, 1.0f}
						});
                }
				return this.matrix; 
			}
			set 
			{
				this.matrix = value; // FastColorMatrix.CopyMatrix(value, this.matrix); 
			}
		}

		public string Name
		{
			get 
			{
				//if (this.MatrixType == MatrixTypes.Custom)
				//    return this.name;
				//else if (this.MatrixType == MatrixTypes.Brightness)
				//    return "Brightness";
				//else if (this.MatrixType == MatrixTypes.BrightnessEx)
				//    return "BrightnessEx";
				//else if (this.MatrixType == MatrixTypes.Contrast)
				//    return "Contrast";
				//else if (this.MatrixType == MatrixTypes.ContrastEx)
				//    return "ContrastEx";
				//else if (this.MatrixType == MatrixTypes.Greyscale)
				//    return "Greyscale";
				//else if (this.MatrixType == MatrixTypes.MakeYourOwn)
				//    return "MakeYourOwn";
				//else if (this.MatrixType == MatrixTypes.Negative)
                //    return "Negative";
                //else if (this.MatrixType == MatrixTypes.Invert)
                //    return "Invert";
				//else if (this.MatrixType == MatrixTypes.Saturation)
				//    return "Saturation";
				//else if (this.MatrixType == MatrixTypes.SaturationEx)
				//    return "SaturationEx";
				//else if (this.MatrixType == MatrixTypes.Sepia)
				//    return "Sepia";
				//else
					return this.name; 
			}
			set { this.name = value; }
		}

		public virtual bool[] GetEnabledStatus()
		{
			if (this.ValueMode == ValueModes.All)
			{
				return new bool[25]{
					true, true, true, true, true,
					true, true, true, true, true,
					true, true, true, true, true,
					true, true, true, true, true,
					true, true, true, true, true};
			}
			else if (this.ValueMode == ValueModes.None || this.MatrixType == MatrixTypes.Custom)
			{
				return new bool[25]{
					false, false, false, false, false,
					false, false, false, false, false,
					false, false, false, false, false,
					false, false, false, false, false,
					false, false, false, false, false};
			}

			if (this.MatrixType == MatrixTypes.Brightness || this.MatrixType == MatrixTypes.BrightnessEx)
			{
				return new bool[25]{
					false, false, false, false, false,
					false, false, false, false, false,
					false, false, false, false, false,
					false, false, false, false, false,
					true, true, true, false, false};
			}
			else if (this.MatrixType == MatrixTypes.Contrast || this.MatrixType == MatrixTypes.ContrastEx)
			{
				return new bool[25]{
					true, false, false, false, false,
					false, true, false, false, false,
					false, false, true, false, false,
					false, false, false, false, false,
					false, false, false, false, false};
			}
			else if (this.MatrixType == MatrixTypes.Saturation || this.MatrixType == MatrixTypes.SaturationEx)
			{
				return new bool[25]{
					true, true, true, false, false,
					true, true, true, false, false,
					true, true, true, false, false,
					false, false, false, false, false,
					false, false, false, false, false};
			}
			else
			{
				bool[] ret = new bool[25];
				int index = 0;
				ret[index++] = ConfigureEnable(this.ColMatrix.RR);
				ret[index++] = ConfigureEnable(this.ColMatrix.RG);
				ret[index++] = ConfigureEnable(this.ColMatrix.RB);
				ret[index++] = ConfigureEnable(this.ColMatrix.RA);
				ret[index++] = ConfigureEnable(this.ColMatrix.RW);
				ret[index++] = ConfigureEnable(this.ColMatrix.GR);
				ret[index++] = ConfigureEnable(this.ColMatrix.GG);
				ret[index++] = ConfigureEnable(this.ColMatrix.GB);
				ret[index++] = ConfigureEnable(this.ColMatrix.GA);
				ret[index++] = ConfigureEnable(this.ColMatrix.GW);
				ret[index++] = ConfigureEnable(this.ColMatrix.BR);
				ret[index++] = ConfigureEnable(this.ColMatrix.BG);
				ret[index++] = ConfigureEnable(this.ColMatrix.BB);
				ret[index++] = ConfigureEnable(this.ColMatrix.BA);
				ret[index++] = ConfigureEnable(this.ColMatrix.BW);
				ret[index++] = ConfigureEnable(this.ColMatrix.AR);
				ret[index++] = ConfigureEnable(this.ColMatrix.AG);
				ret[index++] = ConfigureEnable(this.ColMatrix.AB);
				ret[index++] = ConfigureEnable(this.ColMatrix.AA);
				ret[index++] = ConfigureEnable(this.ColMatrix.AW);
				ret[index++] = ConfigureEnable(this.ColMatrix.WR);
				ret[index++] = ConfigureEnable(this.ColMatrix.WG);
				ret[index++] = ConfigureEnable(this.ColMatrix.WB);
				ret[index++] = ConfigureEnable(this.ColMatrix.WA);
				ret[index++] = ConfigureEnable(this.ColMatrix.WW);
				return ret;
			}
		}

		public virtual bool ConfigureEnable(float value)
		{
			if (value != 0.0f && value != 1.0f)
				return true;
			else
				return false;
		}

		#region Colors
		public float RR
		{
			get { return this.ColMatrix.RR; }
			set { this.ColMatrix.RR = value; }
		}

		public float RG
		{
			get { return this.ColMatrix.RG; }
			set { this.ColMatrix.RG = value; }
		}

		public float RB
		{
			get { return this.ColMatrix.RB; }
			set { this.ColMatrix.RB = value; }
		}

		public float RA
		{
			get { return this.ColMatrix.RA; }
			set { this.ColMatrix.RA = value; }
		}

		public float RW
		{
			get { return this.ColMatrix.RW; }
			set { this.ColMatrix.RW = value; }
		}

		public float GR
		{
			get { return this.ColMatrix.GR; }
			set { this.ColMatrix.GR = value; }
		}

		public float GG
		{
			get { return this.ColMatrix.GG; }
			set { this.ColMatrix.GG = value; }
		}

		public float GB
		{
			get { return this.ColMatrix.GB; }
			set { this.ColMatrix.GB = value; }
		}

		public float GA
		{
			get { return this.ColMatrix.GA; }
			set { this.ColMatrix.GA = value; }
		}

		public float GW
		{
			get { return this.ColMatrix.GW; }
			set { this.ColMatrix.GW = value; }
		}

		public float BR
		{
			get { return this.ColMatrix.BR; }
			set { this.ColMatrix.BR = value; }
		}

		public float BG
		{
			get { return this.ColMatrix.BG; }
			set { this.ColMatrix.BG = value; }
		}

		public float BB
		{
			get { return this.ColMatrix.BB; }
			set { this.ColMatrix.BB = value; }
		}

		public float BA
		{
			get { return this.ColMatrix.BA; }
			set { this.ColMatrix.BA = value; }
		}

		public float BW
		{
			get { return this.ColMatrix.BW; }
			set { this.ColMatrix.BW = value; }
		}

		public float AR
		{
			get { return this.ColMatrix.AR; }
			set { this.ColMatrix.AR = value; }
		}

		public float AG
		{
			get { return this.ColMatrix.AG; }
			set { this.ColMatrix.AG = value; }
		}

		public float AB
		{
			get { return this.ColMatrix.AB; }
			set { this.ColMatrix.AB = value; }
		}

		public float AA
		{
			get { return this.ColMatrix.AA; }
			set { this.ColMatrix.AA = value; }
		}

		public float AW
		{
			get { return this.ColMatrix.AW; }
			set { this.ColMatrix.AW = value; }
		}

		public float WR
		{
			get { return this.ColMatrix.WR; }
			set { this.ColMatrix.WR = value; }
		}

		public float WG
		{
			get { return this.ColMatrix.WG; }
			set { this.ColMatrix.WG = value; }
		}

		public float WB
		{
			get { return this.ColMatrix.WB; }
			set { this.ColMatrix.WB = value; }
		}

		public float WA
		{
			get { return this.ColMatrix.WA; }
			set { this.ColMatrix.WA = value; }
		}
		public float WW
		{
			get { return this.ColMatrix.WW; }
			set { this.ColMatrix.WW = value; }
		}
		#endregion Colors

		#region IXmlSerializable Members

		public System.Xml.Schema.XmlSchema GetSchema()
		{
			return null;
		}

		public void ReadXml(System.Xml.XmlReader reader)
		{
			try
			{
				this.Name = reader.GetAttribute("Name");
				this.ValueMode = (ValueModes)Int32.Parse(reader.GetAttribute("ValueMode"));
				this.MatrixType = (MatrixTypes)Int32.Parse(reader.GetAttribute("Type"));

				if (this.MatrixType == MatrixTypes.Custom)
				{
					ReadMultipleMatrix(reader, this);
				}
				else if (this.ValueMode == ValueModes.Single)
				{
					ReadSingleValue(reader, this);
				}
				else if (this.ValueMode == ValueModes.Triple)
				{
					ReadTripleValues(reader, this);
				}
				else if (this.ValueMode == ValueModes.All)
				{
					ReadAllValues(reader, this);
				}
				while (reader.NodeType == XmlNodeType.EndElement && reader.Name != "ImageAdjSettings")
					reader.Read();
				App.RunningMatrixList.Add(this);
			}
			catch (Exception e)
			{
				System.Diagnostics.Debug.WriteLine(e.Message);
			}
		}

		private void ReadAllValues(System.Xml.XmlReader reader, ImageColorMatrix matrix)
		{
			reader.ReadStartElement();
			//Red
			matrix.RR = reader.ReadElementContentAsFloat();//"Red Red");
			reader.Read();
			matrix.RG = reader.ReadElementContentAsFloat();//"Red Green");
			reader.Read();
			matrix.RB = reader.ReadElementContentAsFloat();//"Red Blue");
			reader.Read();
			matrix.RA = reader.ReadElementContentAsFloat();//"Red Alpha");
			reader.Read();
			matrix.RW = reader.ReadElementContentAsFloat();//"Red Base");
			reader.Read();
			//Green
			matrix.RR = reader.ReadElementContentAsFloat();//"Green Red");
			reader.Read();
			matrix.RG = reader.ReadElementContentAsFloat();//"Green Green");
			reader.Read();
			matrix.RB = reader.ReadElementContentAsFloat();//"Green Blue");
			reader.Read();
			matrix.RA = reader.ReadElementContentAsFloat();//"Green Alpha");
			reader.Read();
			matrix.RW = reader.ReadElementContentAsFloat();//"Green Base");
			reader.Read();
			//Blue
			matrix.RR = reader.ReadElementContentAsFloat();//"Blue Red");
			reader.Read();
			matrix.RG = reader.ReadElementContentAsFloat();//"Blue Green");
			reader.Read();
			matrix.RB = reader.ReadElementContentAsFloat();//"Blue Blue");
			reader.Read();
			matrix.RA = reader.ReadElementContentAsFloat();//"Blue Alpha");
			reader.Read();
			matrix.RW = reader.ReadElementContentAsFloat();//"Blue Base");
			reader.Read();
			//Alpha
			matrix.RR = reader.ReadElementContentAsFloat();//"Alpha Red");
			reader.Read();
			matrix.RG = reader.ReadElementContentAsFloat();//"Alpha Green");
			reader.Read();
			matrix.RB = reader.ReadElementContentAsFloat();//"Alpha Blue");
			reader.Read();
			matrix.RA = reader.ReadElementContentAsFloat();//"Alpha Alpha");
			reader.Read();
			matrix.RW = reader.ReadElementContentAsFloat();//"Alpha Base");
			reader.Read();
			//Base
			matrix.RR = reader.ReadElementContentAsFloat();//"Base Red");
			reader.Read();
			matrix.RG = reader.ReadElementContentAsFloat();//"Base Green");
			reader.Read();
			matrix.RB = reader.ReadElementContentAsFloat();//"Base Blue");
			reader.Read();
			matrix.RA = reader.ReadElementContentAsFloat();//"Base Alpha");
			reader.Read();
			matrix.RW = reader.ReadElementContentAsFloat();//"Base Base");
			reader.ReadEndElement();
		}

		private void ReadTripleValues(XmlReader reader, ImageColorMatrix matrix)
		{
			ITripleValue iTriple = matrix as ITripleValue;
			if (iTriple != null)
			{
				reader.Read();
				reader.MoveToFirstAttribute();
				iTriple.ValueR = Int32.Parse(reader.Value);
				reader.MoveToNextAttribute();
				iTriple.ValueG = Int32.Parse(reader.Value);
				reader.MoveToNextAttribute();
				iTriple.ValueB = Int32.Parse(reader.Value);
				reader.Read();
			}
		}

		private void ReadSingleValue(XmlReader reader, ImageColorMatrix matrix)
		{
			ISingleValue iSingle = matrix as ISingleValue;
			if (iSingle != null)
			{
				reader.Read();
				iSingle.Value = reader.ReadElementContentAsInt();
				reader.Read();
			}
		}

		private void ReadMultipleMatrix(XmlReader reader, ImageColorMatrix matrix)
		{
			IMultipleMatricies iMatrix = matrix as IMultipleMatricies;
			if (iMatrix == null || reader.Depth == 0)
				return;
			while (reader.Name != "Effect" && reader.NodeType != XmlNodeType.EndElement)
				reader.Read();

			while (reader.Name == "Effect")
			{
				ImageColorMatrix newMatrix = new ImageColorMatrix();
				reader.MoveToFirstAttribute();
				newMatrix.Name = reader.Value;
				reader.MoveToNextAttribute();
				newMatrix.ValueMode = (ValueModes)Int32.Parse(reader.Value);
				reader.MoveToNextAttribute();
				newMatrix.MatrixType = (MatrixTypes)Int32.Parse(reader.Value);
				//if (newMatrix.MatrixType == MatrixTypes.Custom)
				//{
				//    ImageColorMatrix savedMatrix = GetSavedMatrix(newMatrix.Name);
				//    if (savedMatrix != null)
				//    {
				//        this.matricies = savedMatrix.Matricies;
				//    }
				//    else
				//    {
				//        if (MessageBox.Show("The " + newMatrix.Name + " effect cannot be found (referenced in " + matrix.Name + ").  "
				//            + "Would you like to remove the missing effect?", "Effect not found", MessageBoxButtons.YesNo) == DialogResult.Yes)
				//        {
				//            continue;
				//        }
				//    }
				//}
				//else 
					if (newMatrix.ValueMode == ValueModes.Single)
				{
					ReadSingleValue(reader, newMatrix);
				}
				else if (newMatrix.ValueMode == ValueModes.Triple)
				{
					ReadTripleValues(reader, newMatrix);
				}
				else if (newMatrix.ValueMode == ValueModes.All)
				{
					ReadAllValues(reader, newMatrix);
				}
				matrix.Matricies.Add(newMatrix.Clone() as ImageColorMatrix);
				while (reader.NodeType == XmlNodeType.EndElement || reader.NodeType == XmlNodeType.Attribute)
					reader.Read();
			}
		}

		private ImageColorMatrix GetSavedMatrix(string name)
		{
			foreach (ImageColorMatrix matrix in App.RunningMatrixList)
			{
				if (name == matrix.Name)
					return matrix;
			}
			return null;
		}

		public void WriteXml(System.Xml.XmlWriter writer)
		{
			writer.WriteAttributeString("Name", this.Name);
			writer.WriteAttributeString("ValueMode", ((int)this.ValueMode).ToString());
			writer.WriteAttributeString("Type", ((int)this.MatrixType).ToString());
			if (this is IMultipleMatricies)
			{
				WriteMultipleMatrix(writer);
			}
			else if (this.ValueMode == ValueModes.Single)
			{
				WriteSingleValue(writer, this);
			}
			else if (this.ValueMode == ValueModes.Triple)
			{
				WriteTripleValue(writer, this);
			}
			else if (this.ValueMode == ValueModes.All)
			{
				WriteAllValues(writer, this);
			}
		}

		private void WriteMultipleMatrix(XmlWriter writer)
		{
			IMultipleMatricies iMatrix = this as IMultipleMatricies;
			if (iMatrix == null || !iMatrix.InUse)
				return;

			writer.WriteStartElement("EffectsList");
			foreach (ImageColorMatrix matrix in iMatrix.Matricies)
			{
				writer.WriteStartElement("Effect");
				writer.WriteAttributeString("Name", matrix.Name);
				writer.WriteAttributeString("ValueMode", ((int)matrix.ValueMode).ToString());
				writer.WriteAttributeString("Type", ((int)matrix.MatrixType).ToString());
				if (matrix.ValueMode == ValueModes.Single)
				{
					WriteSingleValue(writer, matrix);
				}
				else if (matrix.ValueMode == ValueModes.Triple)
				{
					WriteTripleValue(writer, matrix);
				}
				else if (matrix.ValueMode == ValueModes.All)
				{
					WriteAllValues(writer, matrix);
				}
					
				writer.WriteEndElement();
			}
			writer.WriteEndElement();
		}

		private void WriteAllValues(XmlWriter writer, ImageColorMatrix matrix)
		{
			writer.WriteStartElement("ColorMatrixValues");
			#region Write Color Values
			//Red
			writer.WriteElementString("RedRed", matrix.RR.ToString());
			writer.WriteElementString("RedGreen", matrix.RG.ToString());
			writer.WriteElementString("RedBlue", matrix.RB.ToString());
			writer.WriteElementString("RedAlpha", matrix.RA.ToString());
			writer.WriteElementString("RedBase", matrix.RW.ToString());
			//Green
			writer.WriteElementString("GreenRed", matrix.GR.ToString());
			writer.WriteElementString("GreenGreen", matrix.GG.ToString());
			writer.WriteElementString("GreenBlue", matrix.GB.ToString());
			writer.WriteElementString("GreenAlpha", matrix.GA.ToString());
			writer.WriteElementString("GreenBase", matrix.GW.ToString());
			//Blue
			writer.WriteElementString("BlueRed", matrix.BR.ToString());
			writer.WriteElementString("BlueGreen", matrix.BG.ToString());
			writer.WriteElementString("BlueBlue", matrix.BB.ToString());
			writer.WriteElementString("BlueAlpha", matrix.BA.ToString());
			writer.WriteElementString("BlueBase", matrix.BW.ToString());
			//Alpha
			writer.WriteElementString("AlphaRed", matrix.AR.ToString());
			writer.WriteElementString("AlphaGreen", matrix.AG.ToString());
			writer.WriteElementString("AlphaBlue", matrix.AB.ToString());
			writer.WriteElementString("AlphaAlpha", matrix.AA.ToString());
			writer.WriteElementString("AlphaBase", matrix.AW.ToString());
			//Base
			writer.WriteElementString("BaseRed", matrix.WR.ToString());
			writer.WriteElementString("BaseGreen", matrix.WG.ToString());
			writer.WriteElementString("BaseBlue", matrix.WB.ToString());
			writer.WriteElementString("BaseAlpha", matrix.WA.ToString());
			writer.WriteElementString("BaseBase", matrix.WW.ToString());
			#endregion Write Color Values
			writer.WriteEndElement();
		}

		private void WriteTripleValue(XmlWriter writer, ImageColorMatrix matrix)
		{
			ITripleValue iTriple = matrix as ITripleValue;
			if (iTriple != null)
			{
				writer.WriteStartElement("TripleValue");
				writer.WriteAttributeString("RedValue", iTriple.ValueR.ToString());
				writer.WriteAttributeString("GreenValue", iTriple.ValueG.ToString());
				writer.WriteAttributeString("BlueValue", iTriple.ValueB.ToString());
				writer.WriteEndElement();
			}
		}

		private void WriteSingleValue(XmlWriter writer, ImageColorMatrix matrix)
		{
			ISingleValue iSingle = matrix as ISingleValue;
			if (iSingle != null)
			{
				writer.WriteElementString("SingleValue", iSingle.Value.ToString());
			}
		}

		#endregion

		#region ICloneable Members

		public virtual object Clone()
		{
			ImageColorMatrix matrix = new ImageColorMatrix(this.Name, this.MatrixType, this.ValueMode);
			matrix.Value = this.Value;
			matrix.ValueR = this.ValueR;
			matrix.ValueG = this.ValueG;
			matrix.ValueB = this.ValueB;
			matrix.ColMatrix = this.ColMatrix.Clone() as FastColorMatrix;
			matrix.ImageGuid = this.ImageGuid;
			foreach (ImageColorMatrix usedMatrix in this.Matricies)
			{
				matrix.Matricies.Add((ImageColorMatrix)usedMatrix.Clone());
			}
			return matrix;
		}

		#endregion

		#region ISingleValue Members

		public decimal Minimum
		{
			get
			{
				return 0;
			}
		}

		public decimal Maximum
		{
			get 
			{ 
				if (this.MatrixType == MatrixTypes.Contrast || this.MatrixType == MatrixTypes.ContrastEx)
				{
					return 255;
				}
				else if (this.MatrixType == MatrixTypes.Brightness || this.MatrixType == MatrixTypes.BrightnessEx ||
					this.MatrixType == MatrixTypes.Saturation || this.MatrixType == MatrixTypes.SaturationEx)
				{
					return 512;
				}				 
				else
				{
					return 512;
				}
			}
		}
		private decimal value;
		public decimal Value
		{
			get
			{
				return this.value;
			}
			set
			{
				this.value = value;
				if (this.ValueMode == ValueModes.Single)
					this.ColMatrix = CalculateMatrixOffValue(this.value);
			}
		}

		private FastColorMatrix CalculateMatrixOffValue(decimal value)
		{
			FastColorMatrix matrix = FastColorMatrix.GetIdentityMatrix();
			if (this.MatrixType == MatrixTypes.Brightness || this.MatrixType == MatrixTypes.BrightnessEx)
			{
				matrix.WR = (float)(value / this.Maximum) * this.Scale - this.Modifier;
				matrix.WG = (float)(value / this.Maximum) * this.Scale - this.Modifier;
				matrix.WB = (float)(value / this.Maximum) * this.Scale - this.Modifier;
			}
			else if (this.MatrixType == MatrixTypes.Contrast || this.MatrixType == MatrixTypes.ContrastEx)
			{
				matrix.RR = (float)(value / this.Maximum) * this.Scale - this.Modifier;
				matrix.GG = (float)(value / this.Maximum) * this.Scale - this.Modifier;
				matrix.BB = (float)(value / this.Maximum) * this.Scale - this.Modifier;
				matrix.WR = 0.001f;
				matrix.WG = 0.001f;
				matrix.WB = 0.001f;
			}
			else if(this.MatrixType == MatrixTypes.Saturation || this.MatrixType == MatrixTypes.SaturationEx)
			{

				float firstStep = 1.0f / 255.0f * (float)this.Value;
				float saturation = 1.0f - firstStep;
				float saturationComplementR = 0.3086f * saturation;
				float saturationComplementG = 0.6094f * saturation;
				float saturationComplementB = 0.0820f * saturation;

				matrix.RR = saturationComplementR + firstStep;
				matrix.RG = saturationComplementR;
				matrix.RB = saturationComplementR;
				matrix.GR = saturationComplementG;
				matrix.GG = saturationComplementG + firstStep;
				matrix.GB = saturationComplementG;
				matrix.BR = saturationComplementB;
				matrix.BG = saturationComplementB;
				matrix.BB = saturationComplementB + firstStep;
			}
			return matrix;
		}

		#endregion

		#region ITripleValue Members

		private decimal valueR;
		public decimal ValueR
		{
			get
			{
				return this.valueR;
			}
			set
			{
				this.valueR = value;
			}
		}

		private decimal valueG;
		public decimal ValueG
		{
			get
			{
				return this.valueG;
			}
			set
			{
				this.valueG = value;
			}
		}

		private decimal valueB;
		public decimal ValueB
		{
			get
			{
				return this.valueB;
			}
			set
			{
				this.valueB = value;
				if(this.ValueMode == ValueModes.Triple)
					this.ColMatrix = CalculateMatrixOffValue(this.ValueR, this.ValueG, this.ValueB);
			}
		}

		private FastColorMatrix CalculateMatrixOffValue(decimal valueR, decimal valueG, decimal valueB)
		{
			FastColorMatrix matrix = FastColorMatrix.GetIdentityMatrix();
			if (this.MatrixType == MatrixTypes.Brightness || this.MatrixType == MatrixTypes.BrightnessEx)
			{
				matrix.WR = (float)(valueR / this.Maximum) * this.Scale - this.Modifier;
				matrix.WG = (float)(valueG / this.Maximum) * this.Scale - this.Modifier;
				matrix.WB = (float)(valueB / this.Maximum) * this.Scale - this.Modifier;
			}
			else if (this.MatrixType == MatrixTypes.Contrast || this.MatrixType == MatrixTypes.ContrastEx)
			{
				matrix.RR = (float)(valueR / this.Maximum) * this.Scale - this.Modifier;
				matrix.GG = (float)(valueG / this.Maximum) * this.Scale - this.Modifier;
				matrix.BB = (float)(valueB / this.Maximum) * this.Scale - this.Modifier;
				matrix.WR = 0.001f;
				matrix.WG = 0.001f;
				matrix.WB = 0.001f;
			}
			else if (this.MatrixType == MatrixTypes.Saturation || this.MatrixType == MatrixTypes.SaturationEx)
			{
				float firstStepR = 1.0f / 255.0f * (float)this.ValueR;
				float firstStepG = 1.0f / 255.0f * (float)this.ValueG;
				float firstStepB = 1.0f / 255.0f * (float)this.ValueB;
				float saturationR = 1.0f - firstStepR;
				float saturationG = 1.0f - firstStepG;
				float saturationB = 1.0f - firstStepB;
				float saturationComplementR = 0.3086f * saturationR;
				float saturationComplementG = 0.6094f * saturationG;
				float saturationComplementB = 0.0820f * saturationB;

				matrix.RR = saturationComplementR + firstStepR;
				matrix.RG = saturationComplementR;
				matrix.RB = saturationComplementR;
				matrix.GR = saturationComplementG;
				matrix.GG = saturationComplementG + firstStepG;
				matrix.GB = saturationComplementG;
				matrix.BR = saturationComplementB;
				matrix.BG = saturationComplementB;
				matrix.BB = saturationComplementB + firstStepB;
			}
			return matrix;
		}

		#endregion

		private float Scale
		{
			get
			{
				if (this.MatrixType == MatrixTypes.Brightness || this.MatrixType == MatrixTypes.BrightnessEx)
				{
					return 2.0f;
				}
				else if (this.MatrixType == MatrixTypes.Contrast || this.MatrixType == MatrixTypes.ContrastEx)
				{
					return 5.0f;
				}
				else if (this.MatrixType == MatrixTypes.Saturation || this.MatrixType == MatrixTypes.SaturationEx)
				{
					return 2.0f;
				}
				else
				{
					return 1.0f;
				}
			}
		}

		private float Modifier
		{
			get
			{
				if (this.MatrixType == MatrixTypes.Brightness || this.MatrixType == MatrixTypes.BrightnessEx)
				{
					return 1.0f;
				}
				else if (this.MatrixType == MatrixTypes.Contrast || this.MatrixType == MatrixTypes.ContrastEx)
				{
					return 0.0f;
				}
				else if (this.MatrixType == MatrixTypes.Saturation || this.MatrixType == MatrixTypes.SaturationEx)
				{
					return 1.0f;
				}
				else
				{
					return 1.0f;
				}
			}
		}

		public FastColorMatrix MultiplyMultipleMatricies()
		{
			FastColorMatrix answer = FastColorMatrix.GetIdentityMatrix();
			try
			{
				foreach (ImageColorMatrix matrix in this.Matricies)
				{
					if (matrix.MatrixType == MatrixTypes.Custom)
					{
						ImageColorMatrix customMatrix = App.ImageMatricies.GetImageMatrix(matrix.Name);
						answer = answer * customMatrix.ColMatrix;
					}
					else
						answer = answer * matrix.ColMatrix;
				}
			}
			catch (InvalidOperationException e)
			{
				System.Diagnostics.Debug.WriteLine(e.Message);
			}
			return answer;
		}
	}
}
