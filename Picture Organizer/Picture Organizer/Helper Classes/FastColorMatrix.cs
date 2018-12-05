using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing.Imaging;

namespace Trebuchet.Helper_Classes
{
	public class FastColorMatrix : ICloneable
	{
		float[,] colorMatrix = new float[5,5];

		public FastColorMatrix()
		{
			SetToIdentity(this);
		}

		public FastColorMatrix(ColorMatrix matrix)
		{
			InitializeMatrix(matrix);
		}

		public FastColorMatrix(float[,] matrix)
		{
			this.colorMatrix = matrix;
		}

		public override string ToString()
		{
			return String.Format("{0},{1},{2},{3},{4}," +
				"{5},{6},{7},{8},{9}," +
				"{10},{11},{12},{13},{14}," +
				"{15},{16},{17},{18},{19}," +
				"{20},{21},{22},{23},{24}",
				this.RR, this.RG, this.RB, this.RA, this.RW,
				this.GR, this.GG, this.GB, this.GA, this.GW,
				this.BR, this.BG, this.BB, this.BA, this.BW,
				this.AR, this.AG, this.AB, this.AA, this.AW,
				this.WR, this.WG, this.WB, this.WA, this.WW);
		}

		public static FastColorMatrix operator *(FastColorMatrix m1, FastColorMatrix m2)
		{
			int n = 5;
			float[,] multipliedMatrix = new float[n,n];
			float[,] m1matrix = m1.colorMatrix;
			float[,] m2matrix = m2.colorMatrix;
			for(int i=0;i<n;i++)
			{
				for(int j=0;j<n;j++)
				{
					for(int k=0;k<n;k++)
					{
						//System.Diagnostics.Debug.WriteLine(String.Format("I:{4}, J:{5}, K:{6}, - {0} = {1} + {2} * {3}",
						//multipliedMatrix[i, j] + m1matrix[i, k] * m2matrix[k, j], multipliedMatrix[i, j],
						//m1matrix[i, k], m2matrix[k, j], i,j,k));

						multipliedMatrix[i, j] = multipliedMatrix[i, j] + m2matrix[i, k] * m1matrix[k, j];
					}
				}
			}
			return new FastColorMatrix(multipliedMatrix);
		}

		public static bool operator ==(FastColorMatrix m1, FastColorMatrix m2)
		{
			// If both are null, or both are same instance, return true.
			if (System.Object.ReferenceEquals(m1, m2))
			{
				return true;
			}

			// If one is null, but not both, return false.
			if (((object)m1 == null) || ((object)m2 == null))
			{
				return false;
			}

			int n = 5;
			float[,] m1matrix = m1.colorMatrix;
			float[,] m2matrix = m2.colorMatrix;
			for (int x = 0; x < n; x++)
			{
				for (int y = 0; y < n; y++)
				{
					if (m1matrix[x, y] != m2matrix[x, y])
						return false;
				}
			}
			return true;
		}

		public static bool operator !=(FastColorMatrix m1, FastColorMatrix m2)
		{
			int n = 5;
			float[,] m1matrix = m1.colorMatrix;
			float[,] m2matrix = m2.colorMatrix;
			for (int x = 0; x < n; x++)
			{
				for (int y = 0; y < n; y++)
				{
					if (m1matrix[x, y] != m2matrix[x, y])
						return true;
				}
			}
			return false;
		}

		public float[,] ColorMatrix
		{
			get { return this.colorMatrix; }
			set { this.colorMatrix = value; }
		}

		public ColorMatrix ConvertToColorMatrix()
		{
			return ConvertToColorMatrix(this);
		}

		public static ColorMatrix ConvertToColorMatrix(FastColorMatrix matrix)
		{
			ColorMatrix colorMatrix = new ColorMatrix();
			colorMatrix.Matrix00 = matrix.RR;
			colorMatrix.Matrix01 = matrix.RG;
			colorMatrix.Matrix02 = matrix.RB;
			colorMatrix.Matrix03 = matrix.RA;
			colorMatrix.Matrix04 = matrix.RW;
			colorMatrix.Matrix10 = matrix.GR;
			colorMatrix.Matrix11 = matrix.GG;
			colorMatrix.Matrix12 = matrix.GB;
			colorMatrix.Matrix13 = matrix.GA;
			colorMatrix.Matrix14 = matrix.GW;
			colorMatrix.Matrix20 = matrix.BR;
			colorMatrix.Matrix21 = matrix.BG;
			colorMatrix.Matrix22 = matrix.BB;
			colorMatrix.Matrix23 = matrix.BA;
			colorMatrix.Matrix24 = matrix.BW;
			colorMatrix.Matrix30 = matrix.AR;
			colorMatrix.Matrix31 = matrix.AG;
			colorMatrix.Matrix32 = matrix.AB;
			colorMatrix.Matrix33 = matrix.AA;
			colorMatrix.Matrix34 = matrix.AW;
			colorMatrix.Matrix40 = matrix.WR;
			colorMatrix.Matrix41 = matrix.WG;
			colorMatrix.Matrix42 = matrix.WB;
			colorMatrix.Matrix43 = matrix.WA;
			colorMatrix.Matrix44 = matrix.WW;
			return colorMatrix;
		}

		public static FastColorMatrix GetRandomMatrix()
		{
			Random rand = new Random(DateTime.Now.Millisecond);
			List<float> numbs = new List<float>();
			int n = 5;
			float[,] matrix = new float[n, n];
			for (int x = 0; x < n; x++)
			{
				for (int y = 0; y < n; y++)
				{
					matrix[x, y] = (float)(rand.Next() % 90 + 10);
				}
			}
			return new FastColorMatrix(matrix);
		}

		public static FastColorMatrix GetIdentityMatrix()
		{
			FastColorMatrix newColorMatrix = new FastColorMatrix();
			newColorMatrix = FastColorMatrix.SetToIdentity(newColorMatrix);
			return newColorMatrix;
		}

		private static FastColorMatrix SetToIdentity(FastColorMatrix newColorMatrix)
		{
			newColorMatrix.RR = 1.0f;
			newColorMatrix.RG = 0.0f;
			newColorMatrix.RB = 0.0f;
			newColorMatrix.RA = 0.0f;
			newColorMatrix.RW = 0.0f;
			newColorMatrix.GR = 0.0f;
			newColorMatrix.GG = 1.0f;
			newColorMatrix.GB = 0.0f;
			newColorMatrix.GA = 0.0f;
			newColorMatrix.GW = 0.0f;
			newColorMatrix.BR = 0.0f;
			newColorMatrix.BG = 0.0f;
			newColorMatrix.BB = 1.0f;
			newColorMatrix.BA = 0.0f;
			newColorMatrix.BW = 0.0f;
			newColorMatrix.AR = 0.0f;
			newColorMatrix.AG = 0.0f;
			newColorMatrix.AB = 0.0f;
			newColorMatrix.AA = 1.0f;
			newColorMatrix.AW = 0.0f;
			newColorMatrix.WR = 0.0f;
			newColorMatrix.WG = 0.0f;
			newColorMatrix.WB = 0.0f;
			newColorMatrix.WA = 0.0f;
			newColorMatrix.WW = 1.0f;
			return newColorMatrix;
		}

		public void InitializeMatrix(ColorMatrix matrix)
		{
			this.RR = matrix.Matrix00;
			this.RG = matrix.Matrix01;
			this.RB = matrix.Matrix02;
			this.RA = matrix.Matrix03;
			this.RW = matrix.Matrix04;
			this.GR = matrix.Matrix10;
			this.GG = matrix.Matrix11;
			this.GB = matrix.Matrix12;
			this.GA = matrix.Matrix13;
			this.GW = matrix.Matrix14;
			this.BR = matrix.Matrix20;
			this.BG = matrix.Matrix21;
			this.BB = matrix.Matrix22;
			this.BA = matrix.Matrix23;
			this.BW = matrix.Matrix24;
			this.AR = matrix.Matrix30;
			this.AG = matrix.Matrix31;
			this.AB = matrix.Matrix32;
			this.AA = matrix.Matrix33;
			this.AW = matrix.Matrix34;
			this.WR = matrix.Matrix40;
			this.WG = matrix.Matrix41;
			this.WB = matrix.Matrix42;
			this.WA = matrix.Matrix43;
			this.WW = matrix.Matrix44;
		}

		public void InitializeMatrix(float rr, float rg, float rb, float ra, float rw,
			float gr, float gg, float gb, float ga, float gw,
			float br, float bg, float bb, float ba, float bw,
			float ar, float ag, float ab, float aa, float aw, 
			float wr, float wg, float wb, float wa, float ww)
		{
			this.RR = rr;
			this.RG = rg;
			this.RB = rb;
			this.RA = ra;
			this.RW = rw;
			this.GR = gr;
			this.GG = gg;
			this.GB = gb;
			this.GA = ga;
			this.GW = gw;
			this.BR = br;
			this.BG = bg;
			this.BB = bb;
			this.BA = ba;
			this.BW = bw;
			this.AR = ar;
			this.AG = ag;
			this.AB = ab;
			this.AA = aa;
			this.AW = aw;
			this.WR = wr;
			this.WG = wg;
			this.WB = wb;
			this.WA = wa;
			this.WW = ww;
		}

		public static void CopyMatrix(FastColorMatrix source, FastColorMatrix destination)
		{
			float[,] m1Matrix = source.ColorMatrix;
			float[,] m2Matrix = destination.ColorMatrix;
			int n = 5;

			for (int x = 0; x < n; x++)
			{
				for (int y = 0; y < n; y++)
				{
					m2Matrix[x, y] = m1Matrix[x, y];
				}
			}
		}

		#region Property Accessors for Colors
		public float RR
		{
			get { return this.ColorMatrix[0, 0]; }
			set { this.ColorMatrix[0, 0] = value; }
		}

		public float RG
		{
			get { return this.ColorMatrix[0, 1]; }
			set { this.ColorMatrix[0, 1] = value; }
		}

		public float RB
		{
			get { return this.ColorMatrix[0, 2]; }
			set { this.ColorMatrix[0, 2] = value; }
		}

		public float RA
		{
			get { return this.ColorMatrix[0, 3]; }
			set { this.ColorMatrix[0, 3] = value; }
		}

		public float RW
		{
			get { return this.ColorMatrix[0, 4]; }
			set { this.ColorMatrix[0, 4] = value; }
		}

		public float GR
		{
			get { return this.ColorMatrix[1, 0]; }
			set { this.ColorMatrix[1, 0] = value; }
		}

		public float GG
		{
			get { return this.ColorMatrix[1, 1]; }
			set { this.ColorMatrix[1, 1] = value; }
		}

		public float GB
		{
			get { return this.ColorMatrix[1, 2]; }
			set { this.ColorMatrix[1, 2] = value; }
		}

		public float GA
		{
			get { return this.ColorMatrix[1, 3]; }
			set { this.ColorMatrix[1, 3] = value; }
		}

		public float GW
		{
			get { return this.ColorMatrix[1, 4]; }
			set { this.ColorMatrix[1, 4] = value; }
		}

		public float BR
		{
			get { return this.ColorMatrix[2, 0]; }
			set { this.ColorMatrix[2, 0] = value; }
		}

		public float BG
		{
			get { return this.ColorMatrix[2, 1]; }
			set { this.ColorMatrix[2, 1] = value; }
		}

		public float BB
		{
			get { return this.ColorMatrix[2, 2]; }
			set { this.ColorMatrix[2, 2] = value; }
		}

		public float BA
		{
			get { return this.ColorMatrix[2, 3]; }
			set { this.ColorMatrix[2, 3] = value; }
		}

		public float BW
		{
			get { return this.ColorMatrix[2, 4]; }
			set { this.ColorMatrix[2, 4] = value; }
		}

		public float AR
		{
			get { return this.ColorMatrix[3, 0]; }
			set { this.ColorMatrix[3, 0] = value; }
		}

		public float AG
		{
			get { return this.ColorMatrix[3, 1]; }
			set { this.ColorMatrix[3, 1] = value; }
		}

		public float AB
		{
			get { return this.ColorMatrix[3, 2]; }
			set { this.ColorMatrix[3, 2] = value; }
		}

		public float AA
		{
			get { return this.ColorMatrix[3, 3]; }
			set { this.ColorMatrix[3, 3] = value; }
		}

		public float AW
		{
			get { return this.ColorMatrix[3, 4]; }
			set { this.ColorMatrix[3, 4] = value; }
		}

		public float WR
		{
			get { return this.ColorMatrix[4, 0]; }
			set { this.ColorMatrix[4, 0] = value; }
		}

		public float WG
		{
			get { return this.ColorMatrix[4, 1]; }
			set { this.ColorMatrix[4, 1] = value; }
		}

		public float WB
		{
			get { return this.ColorMatrix[4, 2]; }
			set { this.ColorMatrix[4, 2] = value; }
		}

		public float WA
		{
			get { return this.ColorMatrix[4, 3]; }
			set { this.ColorMatrix[4, 3] = value; }
		}

		public float WW
		{
			get { return this.ColorMatrix[4, 4]; }
			set { this.ColorMatrix[4, 4] = value; }
		}
		#endregion

		#region ICloneable Members

		public object Clone()
		{
			FastColorMatrix matrix = new FastColorMatrix();
			FastColorMatrix.CopyMatrix(this, matrix);
			return matrix;
		}

		#endregion
	}
}
