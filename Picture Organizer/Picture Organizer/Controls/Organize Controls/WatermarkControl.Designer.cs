namespace Trebuchet.Controls
{
    partial class WatermarkControl
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
			this.numTransparency = new System.Windows.Forms.NumericUpDown();
			this.label10 = new System.Windows.Forms.Label();
			this.chkCreateWater = new System.Windows.Forms.CheckBox();
			this.comboWaterLoc = new System.Windows.Forms.ComboBox();
			this.label6 = new System.Windows.Forms.Label();
			this.rbText = new System.Windows.Forms.RadioButton();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.watermarkTextControl1 = new Trebuchet.Controls.WatermarkTextControl();
			this.rbGraphic = new System.Windows.Forms.RadioButton();
			this.watermarkGraphicControl1 = new Trebuchet.Controls.WatermarkGraphicControl();
			this.lblFrequency = new System.Windows.Forms.Label();
			this.numSpacingTop = new System.Windows.Forms.NumericUpDown();
			this.trackFrequency = new System.Windows.Forms.TrackBar();
			this.grpSpacing = new System.Windows.Forms.GroupBox();
			this.button1 = new System.Windows.Forms.Button();
			this.label5 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.numSpacingWidth = new System.Windows.Forms.NumericUpDown();
			this.numSpacingLeft = new System.Windows.Forms.NumericUpDown();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.numSpacingHeight = new System.Windows.Forms.NumericUpDown();
			((System.ComponentModel.ISupportInitialize)(this.numTransparency)).BeginInit();
			this.groupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.numSpacingTop)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.trackFrequency)).BeginInit();
			this.grpSpacing.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.numSpacingWidth)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numSpacingLeft)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numSpacingHeight)).BeginInit();
			this.SuspendLayout();
			// 
			// numTransparency
			// 
			this.numTransparency.Location = new System.Drawing.Point(333, 197);
			this.numTransparency.Maximum = new decimal(new int[] {
            254,
            0,
            0,
            0});
			this.numTransparency.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.numTransparency.Name = "numTransparency";
			this.numTransparency.Size = new System.Drawing.Size(50, 20);
			this.numTransparency.TabIndex = 41;
			this.numTransparency.Value = new decimal(new int[] {
            60,
            0,
            0,
            0});
			this.numTransparency.ValueChanged += new System.EventHandler(this.numTransparency_ValueChanged);
			// 
			// label10
			// 
			this.label10.AutoSize = true;
			this.label10.Location = new System.Drawing.Point(255, 199);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(72, 13);
			this.label10.TabIndex = 40;
			this.label10.Text = "Transparency";
			// 
			// chkCreateWater
			// 
			this.chkCreateWater.AutoSize = true;
			this.chkCreateWater.Location = new System.Drawing.Point(217, 282);
			this.chkCreateWater.Name = "chkCreateWater";
			this.chkCreateWater.Size = new System.Drawing.Size(152, 17);
			this.chkCreateWater.TabIndex = 35;
			this.chkCreateWater.Text = "Create watermark directory";
			this.chkCreateWater.UseVisualStyleBackColor = true;
			this.chkCreateWater.CheckedChanged += new System.EventHandler(this.chkCreateWater_CheckedChanged);
			// 
			// comboWaterLoc
			// 
			this.comboWaterLoc.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboWaterLoc.FormattingEnabled = true;
			this.comboWaterLoc.Items.AddRange(new object[] {
            "Top-Left",
            "Top-Center",
            "Top-Right",
            "Center-Left",
            "Center-Center",
            "Center-Right",
            "Bottom-Left",
            "Bottom-Center",
            "Bottom-Right",
            "Tile",
            "Custom"});
			this.comboWaterLoc.Location = new System.Drawing.Point(59, 196);
			this.comboWaterLoc.Name = "comboWaterLoc";
			this.comboWaterLoc.Size = new System.Drawing.Size(100, 21);
			this.comboWaterLoc.TabIndex = 34;
			this.comboWaterLoc.SelectedIndexChanged += new System.EventHandler(this.comboWaterLoc_SelectedIndexChanged);
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(2, 199);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(48, 13);
			this.label6.TabIndex = 33;
			this.label6.Text = "Location";
			// 
			// rbText
			// 
			this.rbText.AutoSize = true;
			this.rbText.Checked = true;
			this.rbText.Location = new System.Drawing.Point(119, 17);
			this.rbText.Name = "rbText";
			this.rbText.Size = new System.Drawing.Size(46, 17);
			this.rbText.TabIndex = 46;
			this.rbText.TabStop = true;
			this.rbText.Text = "Text";
			this.rbText.UseVisualStyleBackColor = true;
			this.rbText.CheckedChanged += new System.EventHandler(this.rbText_CheckedChanged);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.watermarkTextControl1);
			this.groupBox1.Controls.Add(this.rbGraphic);
			this.groupBox1.Controls.Add(this.rbText);
			this.groupBox1.Controls.Add(this.watermarkGraphicControl1);
			this.groupBox1.Location = new System.Drawing.Point(3, 3);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(378, 189);
			this.groupBox1.TabIndex = 51;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Type of Watermark";
			// 
			// watermarkTextControl1
			// 
			this.watermarkTextControl1.Location = new System.Drawing.Point(4, 35);
			this.watermarkTextControl1.Name = "watermarkTextControl1";
			this.watermarkTextControl1.Size = new System.Drawing.Size(371, 149);
			this.watermarkTextControl1.TabIndex = 48;
			// 
			// rbGraphic
			// 
			this.rbGraphic.AutoSize = true;
			this.rbGraphic.Location = new System.Drawing.Point(197, 17);
			this.rbGraphic.Name = "rbGraphic";
			this.rbGraphic.Size = new System.Drawing.Size(62, 17);
			this.rbGraphic.TabIndex = 47;
			this.rbGraphic.Text = "Graphic";
			this.rbGraphic.UseVisualStyleBackColor = true;
			this.rbGraphic.CheckedChanged += new System.EventHandler(this.rbGraphic_CheckedChanged);
			// 
			// watermarkGraphicControl1
			// 
			this.watermarkGraphicControl1.Location = new System.Drawing.Point(4, 35);
			this.watermarkGraphicControl1.Name = "watermarkGraphicControl1";
			this.watermarkGraphicControl1.Size = new System.Drawing.Size(368, 149);
			this.watermarkGraphicControl1.TabIndex = 49;
			// 
			// lblFrequency
			// 
			this.lblFrequency.AutoSize = true;
			this.lblFrequency.Location = new System.Drawing.Point(2, 283);
			this.lblFrequency.Name = "lblFrequency";
			this.lblFrequency.Size = new System.Drawing.Size(57, 13);
			this.lblFrequency.TabIndex = 60;
			this.lblFrequency.Text = "Size";
			// 
			// numSpacingTop
			// 
			this.numSpacingTop.Location = new System.Drawing.Point(37, 19);
			this.numSpacingTop.Name = "numSpacingTop";
			this.numSpacingTop.Size = new System.Drawing.Size(40, 20);
			this.numSpacingTop.TabIndex = 52;
			this.numSpacingTop.ValueChanged += new System.EventHandler(this.numSpacingTop_ValueChanged);
			// 
			// trackFrequency
			// 
			this.trackFrequency.Location = new System.Drawing.Point(30, 281);
			this.trackFrequency.Maximum = 150;
			this.trackFrequency.Minimum = 1;
			this.trackFrequency.Name = "trackFrequency";
			this.trackFrequency.Size = new System.Drawing.Size(170, 45);
			this.trackFrequency.TabIndex = 59;
			this.trackFrequency.TickStyle = System.Windows.Forms.TickStyle.None;
			this.trackFrequency.Value = 10;
			this.trackFrequency.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OnFrequencyMouseDown);
			this.trackFrequency.ValueChanged += new System.EventHandler(this.OnFrequencyChanged);
			this.trackFrequency.MouseUp += new System.Windows.Forms.MouseEventHandler(this.OnFrequencyMouseUp);
			// 
			// grpSpacing
			// 
			this.grpSpacing.Controls.Add(this.button1);
			this.grpSpacing.Controls.Add(this.label5);
			this.grpSpacing.Controls.Add(this.label1);
			this.grpSpacing.Controls.Add(this.numSpacingTop);
			this.grpSpacing.Controls.Add(this.numSpacingWidth);
			this.grpSpacing.Controls.Add(this.numSpacingLeft);
			this.grpSpacing.Controls.Add(this.label2);
			this.grpSpacing.Controls.Add(this.label3);
			this.grpSpacing.Controls.Add(this.numSpacingHeight);
			this.grpSpacing.Location = new System.Drawing.Point(5, 223);
			this.grpSpacing.Name = "grpSpacing";
			this.grpSpacing.Size = new System.Drawing.Size(378, 52);
			this.grpSpacing.TabIndex = 71;
			this.grpSpacing.TabStop = false;
			this.grpSpacing.Text = "Spacing";
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(349, 16);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(23, 23);
			this.button1.TabIndex = 75;
			this.button1.Text = "Reset";
			this.button1.UseVisualStyleBackColor = true;
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(83, 21);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(25, 13);
			this.label5.TabIndex = 74;
			this.label5.Text = "Left";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(252, 21);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(35, 13);
			this.label1.TabIndex = 73;
			this.label1.Text = "Width";
			// 
			// numSpacingWidth
			// 
			this.numSpacingWidth.Location = new System.Drawing.Point(290, 19);
			this.numSpacingWidth.Name = "numSpacingWidth";
			this.numSpacingWidth.Size = new System.Drawing.Size(40, 20);
			this.numSpacingWidth.TabIndex = 72;
			this.numSpacingWidth.ValueChanged += new System.EventHandler(this.numSpacingWidth_ValueChanged);
			// 
			// numSpacingLeft
			// 
			this.numSpacingLeft.Location = new System.Drawing.Point(114, 19);
			this.numSpacingLeft.Name = "numSpacingLeft";
			this.numSpacingLeft.Size = new System.Drawing.Size(40, 20);
			this.numSpacingLeft.TabIndex = 71;
			this.numSpacingLeft.ValueChanged += new System.EventHandler(this.numSpacingLeft_ValueChanged);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(5, 21);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(26, 13);
			this.label2.TabIndex = 70;
			this.label2.Text = "Top";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(160, 21);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(38, 13);
			this.label3.TabIndex = 69;
			this.label3.Text = "Height";
			// 
			// numSpacingHeight
			// 
			this.numSpacingHeight.Location = new System.Drawing.Point(206, 19);
			this.numSpacingHeight.Name = "numSpacingHeight";
			this.numSpacingHeight.Size = new System.Drawing.Size(40, 20);
			this.numSpacingHeight.TabIndex = 68;
			this.numSpacingHeight.ValueChanged += new System.EventHandler(this.numSpacingHeight_ValueChanged);
			// 
			// WatermarkControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.BackColor = System.Drawing.Color.White;
			this.Controls.Add(this.grpSpacing);
			this.Controls.Add(this.lblFrequency);
			this.Controls.Add(this.trackFrequency);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.numTransparency);
			this.Controls.Add(this.label10);
			this.Controls.Add(this.chkCreateWater);
			this.Controls.Add(this.comboWaterLoc);
			this.Controls.Add(this.label6);
			this.MinimumSize = new System.Drawing.Size(390, 309);
			this.Name = "WatermarkControl";
			this.Size = new System.Drawing.Size(390, 309);
			((System.ComponentModel.ISupportInitialize)(this.numTransparency)).EndInit();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.numSpacingTop)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.trackFrequency)).EndInit();
			this.grpSpacing.ResumeLayout(false);
			this.grpSpacing.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.numSpacingWidth)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numSpacingLeft)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numSpacingHeight)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown numTransparency;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.CheckBox chkCreateWater;
        private System.Windows.Forms.ComboBox comboWaterLoc;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.RadioButton rbText;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.NumericUpDown numSpacingTop;
        private System.Windows.Forms.Label lblFrequency;
        private System.Windows.Forms.TrackBar trackFrequency;
        private System.Windows.Forms.RadioButton rbGraphic;
        private System.Windows.Forms.GroupBox grpSpacing;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numSpacingWidth;
        private System.Windows.Forms.NumericUpDown numSpacingLeft;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown numSpacingHeight;
        private WatermarkTextControl watermarkTextControl1;
        private WatermarkGraphicControl watermarkGraphicControl1;
    }
}
