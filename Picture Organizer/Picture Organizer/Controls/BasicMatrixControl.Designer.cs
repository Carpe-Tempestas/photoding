namespace Trebuchet.Controls
{
	partial class BasicMatrixControl
	{
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BasicMatrixControl));
            this.label1 = new System.Windows.Forms.Label();
            this.trackBrightness = new System.Windows.Forms.TrackBar();
            this.numBrightness = new System.Windows.Forms.NumericUpDown();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkNegative = new System.Windows.Forms.CheckBox();
            this.chkGrayscale = new System.Windows.Forms.CheckBox();
            this.chkSepia = new System.Windows.Forms.CheckBox();
            this.numContrast = new System.Windows.Forms.NumericUpDown();
            this.trackContrast = new System.Windows.Forms.TrackBar();
            this.label2 = new System.Windows.Forms.Label();
            this.numSaturation = new System.Windows.Forms.NumericUpDown();
            this.trackSaturation = new System.Windows.Forms.TrackBar();
            this.label3 = new System.Windows.Forms.Label();
            this.btnResetBrightness = new System.Windows.Forms.Button();
            this.btnResetContrast = new System.Windows.Forms.Button();
            this.btnResetSaturation = new System.Windows.Forms.Button();
            this.chkInvert = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.trackBrightness)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numBrightness)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numContrast)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackContrast)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSaturation)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackSaturation)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Brightness";
            // 
            // trackBrightness
            // 
            this.trackBrightness.BackColor = System.Drawing.Color.White;
            this.trackBrightness.Location = new System.Drawing.Point(10, 21);
            this.trackBrightness.Name = "trackBrightness";
            this.trackBrightness.Size = new System.Drawing.Size(157, 45);
            this.trackBrightness.TabIndex = 1;
            this.trackBrightness.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trackBrightness.Scroll += new System.EventHandler(this.trackBrightness_Scroll);
            // 
            // numBrightness
            // 
            this.numBrightness.Location = new System.Drawing.Point(173, 23);
            this.numBrightness.Name = "numBrightness";
            this.numBrightness.Size = new System.Drawing.Size(53, 20);
            this.numBrightness.TabIndex = 2;
            this.numBrightness.ValueChanged += new System.EventHandler(this.numBrightness_ValueChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkInvert);
            this.groupBox1.Controls.Add(this.chkNegative);
            this.groupBox1.Controls.Add(this.chkGrayscale);
            this.groupBox1.Controls.Add(this.chkSepia);
            this.groupBox1.Location = new System.Drawing.Point(291, 32);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(98, 112);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Effects";
            // 
            // chkNegative
            // 
            this.chkNegative.AutoSize = true;
            this.chkNegative.Location = new System.Drawing.Point(15, 63);
            this.chkNegative.Name = "chkNegative";
            this.chkNegative.Size = new System.Drawing.Size(69, 17);
            this.chkNegative.TabIndex = 2;
            this.chkNegative.Text = "Negative";
            this.chkNegative.UseVisualStyleBackColor = true;
            this.chkNegative.CheckedChanged += new System.EventHandler(this.chkNegative_CheckedChanged);
            // 
            // chkGrayscale
            // 
            this.chkGrayscale.AutoSize = true;
            this.chkGrayscale.Location = new System.Drawing.Point(15, 40);
            this.chkGrayscale.Name = "chkGrayscale";
            this.chkGrayscale.Size = new System.Drawing.Size(73, 17);
            this.chkGrayscale.TabIndex = 1;
            this.chkGrayscale.Text = "Grayscale";
            this.chkGrayscale.UseVisualStyleBackColor = true;
            this.chkGrayscale.CheckedChanged += new System.EventHandler(this.chkGrayscale_CheckedChanged);
            // 
            // chkSepia
            // 
            this.chkSepia.AutoSize = true;
            this.chkSepia.Location = new System.Drawing.Point(15, 17);
            this.chkSepia.Name = "chkSepia";
            this.chkSepia.Size = new System.Drawing.Size(53, 17);
            this.chkSepia.TabIndex = 0;
            this.chkSepia.Text = "Sepia";
            this.chkSepia.UseVisualStyleBackColor = true;
            this.chkSepia.CheckedChanged += new System.EventHandler(this.chkSepia_CheckedChanged);
            // 
            // numContrast
            // 
            this.numContrast.Location = new System.Drawing.Point(173, 73);
            this.numContrast.Name = "numContrast";
            this.numContrast.Size = new System.Drawing.Size(53, 20);
            this.numContrast.TabIndex = 6;
            this.numContrast.ValueChanged += new System.EventHandler(this.numContrast_ValueChanged);
            // 
            // trackContrast
            // 
            this.trackContrast.BackColor = System.Drawing.Color.White;
            this.trackContrast.Location = new System.Drawing.Point(10, 71);
            this.trackContrast.Name = "trackContrast";
            this.trackContrast.Size = new System.Drawing.Size(157, 45);
            this.trackContrast.TabIndex = 5;
            this.trackContrast.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trackContrast.Scroll += new System.EventHandler(this.trackContrast_Scroll);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Contrast";
            // 
            // numSaturation
            // 
            this.numSaturation.Location = new System.Drawing.Point(173, 124);
            this.numSaturation.Name = "numSaturation";
            this.numSaturation.Size = new System.Drawing.Size(53, 20);
            this.numSaturation.TabIndex = 9;
            this.numSaturation.ValueChanged += new System.EventHandler(this.numSaturation_ValueChanged);
            // 
            // trackSaturation
            // 
            this.trackSaturation.BackColor = System.Drawing.Color.White;
            this.trackSaturation.Location = new System.Drawing.Point(10, 122);
            this.trackSaturation.Name = "trackSaturation";
            this.trackSaturation.Size = new System.Drawing.Size(157, 45);
            this.trackSaturation.TabIndex = 8;
            this.trackSaturation.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trackSaturation.Scroll += new System.EventHandler(this.trackSaturation_Scroll);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 106);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Saturation";
            // 
            // btnResetBrightness
            // 
            this.btnResetBrightness.Image = ((System.Drawing.Image)(resources.GetObject("btnResetBrightness.Image")));
            this.btnResetBrightness.Location = new System.Drawing.Point(236, 20);
            this.btnResetBrightness.Name = "btnResetBrightness";
            this.btnResetBrightness.Size = new System.Drawing.Size(24, 23);
            this.btnResetBrightness.TabIndex = 96;
            this.btnResetBrightness.UseVisualStyleBackColor = true;
            this.btnResetBrightness.Click += new System.EventHandler(this.btnResetBrightness_Click);
            // 
            // btnResetContrast
            // 
            this.btnResetContrast.Image = ((System.Drawing.Image)(resources.GetObject("btnResetContrast.Image")));
            this.btnResetContrast.Location = new System.Drawing.Point(236, 71);
            this.btnResetContrast.Name = "btnResetContrast";
            this.btnResetContrast.Size = new System.Drawing.Size(24, 23);
            this.btnResetContrast.TabIndex = 97;
            this.btnResetContrast.UseVisualStyleBackColor = true;
            this.btnResetContrast.Click += new System.EventHandler(this.btnResetContrast_Click);
            // 
            // btnResetSaturation
            // 
            this.btnResetSaturation.Image = ((System.Drawing.Image)(resources.GetObject("btnResetSaturation.Image")));
            this.btnResetSaturation.Location = new System.Drawing.Point(236, 121);
            this.btnResetSaturation.Name = "btnResetSaturation";
            this.btnResetSaturation.Size = new System.Drawing.Size(24, 23);
            this.btnResetSaturation.TabIndex = 98;
            this.btnResetSaturation.UseVisualStyleBackColor = true;
            this.btnResetSaturation.Click += new System.EventHandler(this.btnResetSaturation_Click);
            // 
            // chkInvert
            // 
            this.chkInvert.AutoSize = true;
            this.chkInvert.Location = new System.Drawing.Point(15, 86);
            this.chkInvert.Name = "chkInvert";
            this.chkInvert.Size = new System.Drawing.Size(53, 17);
            this.chkInvert.TabIndex = 3;
            this.chkInvert.Text = "Invert";
            this.chkInvert.UseVisualStyleBackColor = true;
            this.chkInvert.CheckedChanged += new System.EventHandler(this.chkInvert_CheckedChanged);
            // 
            // BasicMatrixControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnResetSaturation);
            this.Controls.Add(this.btnResetContrast);
            this.Controls.Add(this.btnResetBrightness);
            this.Controls.Add(this.numSaturation);
            this.Controls.Add(this.trackSaturation);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.numContrast);
            this.Controls.Add(this.trackContrast);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.numBrightness);
            this.Controls.Add(this.trackBrightness);
            this.Controls.Add(this.label1);
            this.Name = "BasicMatrixControl";
            this.Size = new System.Drawing.Size(406, 170);
            ((System.ComponentModel.ISupportInitialize)(this.trackBrightness)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numBrightness)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numContrast)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackContrast)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSaturation)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackSaturation)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TrackBar trackBrightness;
		private System.Windows.Forms.NumericUpDown numBrightness;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.CheckBox chkNegative;
		private System.Windows.Forms.CheckBox chkGrayscale;
		private System.Windows.Forms.CheckBox chkSepia;
		private System.Windows.Forms.NumericUpDown numContrast;
		private System.Windows.Forms.TrackBar trackContrast;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.NumericUpDown numSaturation;
		private System.Windows.Forms.TrackBar trackSaturation;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Button btnResetBrightness;
		private System.Windows.Forms.Button btnResetContrast;
		private System.Windows.Forms.Button btnResetSaturation;
        private System.Windows.Forms.CheckBox chkInvert;
	}
}
