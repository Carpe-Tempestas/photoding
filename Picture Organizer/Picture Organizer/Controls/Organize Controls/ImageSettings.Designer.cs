namespace Trebuchet.Controls
{
    partial class ImageSettings
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
			this.chkUseDefaultRes = new System.Windows.Forms.CheckBox();
			this.numUpDownResDPI = new System.Windows.Forms.NumericUpDown();
			this.numImageQuality = new System.Windows.Forms.NumericUpDown();
			this.comboFormat = new System.Windows.Forms.ComboBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.chkUseLowerCase = new System.Windows.Forms.CheckBox();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.comboImageQuality = new System.Windows.Forms.ComboBox();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.groupBox4 = new System.Windows.Forms.GroupBox();
			this.tabOtherAdjustments = new System.Windows.Forms.TabControl();
			this.tabStandardEffects = new System.Windows.Forms.TabPage();
			this.basicMatrixControl1 = new Trebuchet.Controls.BasicMatrixControl();
			this.tabAdvancedEffects = new System.Windows.Forms.TabPage();
			this.matrixControl1 = new Trebuchet.Controls.MatrixControl();
			this.tabEXIF = new System.Windows.Forms.TabPage();
			this.exifControl1 = new Trebuchet.Controls.ExifControl();
			((System.ComponentModel.ISupportInitialize)(this.numUpDownResDPI)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numImageQuality)).BeginInit();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.groupBox4.SuspendLayout();
			this.tabOtherAdjustments.SuspendLayout();
			this.tabStandardEffects.SuspendLayout();
			this.tabAdvancedEffects.SuspendLayout();
			this.tabEXIF.SuspendLayout();
			this.SuspendLayout();
			// 
			// chkUseDefaultRes
			// 
			this.chkUseDefaultRes.AutoSize = true;
			this.chkUseDefaultRes.Checked = true;
			this.chkUseDefaultRes.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkUseDefaultRes.Location = new System.Drawing.Point(6, 19);
			this.chkUseDefaultRes.Name = "chkUseDefaultRes";
			this.chkUseDefaultRes.Size = new System.Drawing.Size(80, 17);
			this.chkUseDefaultRes.TabIndex = 41;
			this.chkUseDefaultRes.Text = "Use default";
			this.chkUseDefaultRes.UseVisualStyleBackColor = true;
			this.chkUseDefaultRes.CheckedChanged += new System.EventHandler(this.chkOverRes_CheckedChanged);
			// 
			// numUpDownResDPI
			// 
			this.numUpDownResDPI.Enabled = false;
			this.numUpDownResDPI.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.numUpDownResDPI.Location = new System.Drawing.Point(92, 18);
			this.numUpDownResDPI.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
			this.numUpDownResDPI.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.numUpDownResDPI.Name = "numUpDownResDPI";
			this.numUpDownResDPI.Size = new System.Drawing.Size(50, 20);
			this.numUpDownResDPI.TabIndex = 40;
			this.numUpDownResDPI.Value = new decimal(new int[] {
            300,
            0,
            0,
            0});
			this.numUpDownResDPI.ValueChanged += new System.EventHandler(this.numUpDownResDPI_ValueChanged);
			// 
			// numImageQuality
			// 
			this.numImageQuality.Location = new System.Drawing.Point(117, 19);
			this.numImageQuality.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.numImageQuality.Name = "numImageQuality";
			this.numImageQuality.Size = new System.Drawing.Size(50, 20);
			this.numImageQuality.TabIndex = 39;
			this.numImageQuality.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
			this.numImageQuality.ValueChanged += new System.EventHandler(this.numImageQuality_ValueChanged);
			// 
			// comboFormat
			// 
			this.comboFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboFormat.FormattingEnabled = true;
			this.comboFormat.Location = new System.Drawing.Point(36, 18);
			this.comboFormat.Name = "comboFormat";
			this.comboFormat.Size = new System.Drawing.Size(121, 21);
			this.comboFormat.TabIndex = 43;
			this.comboFormat.SelectedIndexChanged += new System.EventHandler(this.comboFormat_SelectedIndexChanged);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.chkUseLowerCase);
			this.groupBox1.Location = new System.Drawing.Point(202, 59);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(183, 46);
			this.groupBox1.TabIndex = 44;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "File Extensions";
			// 
			// chkUseLowerCase
			// 
			this.chkUseLowerCase.AutoSize = true;
			this.chkUseLowerCase.Location = new System.Drawing.Point(36, 18);
			this.chkUseLowerCase.Name = "chkUseLowerCase";
			this.chkUseLowerCase.Size = new System.Drawing.Size(96, 17);
			this.chkUseLowerCase.TabIndex = 0;
			this.chkUseLowerCase.Text = "Use lowercase";
			this.chkUseLowerCase.UseVisualStyleBackColor = true;
			this.chkUseLowerCase.CheckedChanged += new System.EventHandler(this.chkUseLowerCase_CheckedChanged);
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.comboImageQuality);
			this.groupBox2.Controls.Add(this.numImageQuality);
			this.groupBox2.Location = new System.Drawing.Point(5, 5);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(176, 50);
			this.groupBox2.TabIndex = 45;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Image Quality";
			// 
			// comboImageQuality
			// 
			this.comboImageQuality.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboImageQuality.FormattingEnabled = true;
			this.comboImageQuality.Items.AddRange(new object[] {
            "High",
            "Medium",
            "Low",
            "Advanced"});
			this.comboImageQuality.Location = new System.Drawing.Point(6, 18);
			this.comboImageQuality.Name = "comboImageQuality";
			this.comboImageQuality.Size = new System.Drawing.Size(105, 21);
			this.comboImageQuality.TabIndex = 45;
			this.comboImageQuality.SelectedIndexChanged += new System.EventHandler(this.comboImageQuality_SelectedIndexChanged);
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.comboFormat);
			this.groupBox3.Location = new System.Drawing.Point(202, 5);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(183, 49);
			this.groupBox3.TabIndex = 46;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Destination Format";
			// 
			// groupBox4
			// 
			this.groupBox4.Controls.Add(this.chkUseDefaultRes);
			this.groupBox4.Controls.Add(this.numUpDownResDPI);
			this.groupBox4.Location = new System.Drawing.Point(5, 59);
			this.groupBox4.Name = "groupBox4";
			this.groupBox4.Size = new System.Drawing.Size(175, 46);
			this.groupBox4.TabIndex = 47;
			this.groupBox4.TabStop = false;
			this.groupBox4.Text = "Resolution(DPI)";
			// 
			// tabOtherAdjustments
			// 
			this.tabOtherAdjustments.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)));
			this.tabOtherAdjustments.Controls.Add(this.tabStandardEffects);
			this.tabOtherAdjustments.Controls.Add(this.tabAdvancedEffects);
			this.tabOtherAdjustments.Location = new System.Drawing.Point(5, 111);
			this.tabOtherAdjustments.Name = "tabOtherAdjustments";
			this.tabOtherAdjustments.SelectedIndex = 0;
			this.tabOtherAdjustments.Size = new System.Drawing.Size(473, 298);
			this.tabOtherAdjustments.TabIndex = 49;
			this.tabOtherAdjustments.SelectedIndexChanged += new System.EventHandler(this.tabOtherAdjustments_SelectedIndexChanged);
			// 
			// tabStandardEffects
			// 
			this.tabStandardEffects.Controls.Add(this.basicMatrixControl1);
			this.tabStandardEffects.Location = new System.Drawing.Point(4, 22);
			this.tabStandardEffects.Name = "tabStandardEffects";
			this.tabStandardEffects.Padding = new System.Windows.Forms.Padding(3);
			this.tabStandardEffects.Size = new System.Drawing.Size(465, 272);
			this.tabStandardEffects.TabIndex = 0;
			this.tabStandardEffects.Text = "Standard Effects";
			this.tabStandardEffects.UseVisualStyleBackColor = true;
			// 
			// basicMatrixControl1
			// 
			this.basicMatrixControl1.Location = new System.Drawing.Point(6, 6);
			this.basicMatrixControl1.Name = "basicMatrixControl1";
			this.basicMatrixControl1.Size = new System.Drawing.Size(406, 170);
			this.basicMatrixControl1.TabIndex = 0;
			// 
			// tabAdvancedEffects
			// 
			this.tabAdvancedEffects.Controls.Add(this.matrixControl1);
			this.tabAdvancedEffects.Location = new System.Drawing.Point(4, 22);
			this.tabAdvancedEffects.Name = "tabAdvancedEffects";
			this.tabAdvancedEffects.Padding = new System.Windows.Forms.Padding(3);
			this.tabAdvancedEffects.Size = new System.Drawing.Size(465, 272);
			this.tabAdvancedEffects.TabIndex = 1;
			this.tabAdvancedEffects.Text = "Advanced Effects";
			this.tabAdvancedEffects.UseVisualStyleBackColor = true;
			// 
			// matrixControl1
			// 
			this.matrixControl1.Location = new System.Drawing.Point(2, 0);
			this.matrixControl1.Name = "matrixControl1";
			this.matrixControl1.Size = new System.Drawing.Size(466, 271);
			this.matrixControl1.TabIndex = 0;
			// 
			// tabEXIF
			// 
			this.tabEXIF.Controls.Add(this.exifControl1);
			this.tabEXIF.Location = new System.Drawing.Point(4, 22);
			this.tabEXIF.Name = "tabEXIF";
			this.tabEXIF.Size = new System.Drawing.Size(465, 272);
			this.tabEXIF.TabIndex = 2;
			this.tabEXIF.Text = "EXIF Information";
			this.tabEXIF.UseVisualStyleBackColor = true;
			// 
			// exifControl1
			// 
			this.exifControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.exifControl1.Location = new System.Drawing.Point(0, 0);
			this.exifControl1.Name = "exifControl1";
			this.exifControl1.Size = new System.Drawing.Size(465, 272);
			this.exifControl1.TabIndex = 0;
			this.exifControl1.Viewer = null;
			// 
			// ImageSettings
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.White;
			this.Controls.Add(this.tabOtherAdjustments);
			this.Controls.Add(this.groupBox4);
			this.Controls.Add(this.groupBox3);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.MinimumSize = new System.Drawing.Size(482, 416);
			this.Name = "ImageSettings";
			this.Size = new System.Drawing.Size(482, 416);
			((System.ComponentModel.ISupportInitialize)(this.numUpDownResDPI)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numImageQuality)).EndInit();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.groupBox3.ResumeLayout(false);
			this.groupBox4.ResumeLayout(false);
			this.groupBox4.PerformLayout();
			this.tabOtherAdjustments.ResumeLayout(false);
			this.tabStandardEffects.ResumeLayout(false);
			this.tabAdvancedEffects.ResumeLayout(false);
			this.tabEXIF.ResumeLayout(false);
			this.ResumeLayout(false);

        }

        #endregion

		private System.Windows.Forms.CheckBox chkUseDefaultRes;
        private System.Windows.Forms.NumericUpDown numUpDownResDPI;
		private System.Windows.Forms.NumericUpDown numImageQuality;
		private System.Windows.Forms.ComboBox comboFormat;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.CheckBox chkUseLowerCase;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.ComboBox comboImageQuality;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.GroupBox groupBox4;
		private System.Windows.Forms.TabControl tabOtherAdjustments;
		private System.Windows.Forms.TabPage tabStandardEffects;
		private System.Windows.Forms.TabPage tabAdvancedEffects;
		private MatrixControl matrixControl1;
		private BasicMatrixControl basicMatrixControl1;
		private System.Windows.Forms.TabPage tabEXIF;
		private ExifControl exifControl1;
    }
}
