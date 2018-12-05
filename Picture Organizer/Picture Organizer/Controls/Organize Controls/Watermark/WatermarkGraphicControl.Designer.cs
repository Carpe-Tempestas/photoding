namespace Trebuchet.Controls
{
    partial class WatermarkGraphicControl
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
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.numWatermarkWidth = new System.Windows.Forms.NumericUpDown();
            this.numWatermarkHeight = new System.Windows.Forms.NumericUpDown();
            this.btnGraphicKey = new System.Windows.Forms.Button();
            this.txtGraphicLocation = new System.Windows.Forms.TextBox();
            this.picBoxWatermark = new System.Windows.Forms.PictureBox();
            this.btnSelectGraphic = new System.Windows.Forms.Button();
            this.lblLocation = new System.Windows.Forms.Label();
            this.chkLockAspect = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnResetStretch = new System.Windows.Forms.Button();
            this.grpCrop = new System.Windows.Forms.GroupBox();
            this.btnResetTrim = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.numCropRight = new System.Windows.Forms.NumericUpDown();
            this.numCropLeft = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.numCropBottom = new System.Windows.Forms.NumericUpDown();
            this.numCropTop = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.numRemapPercent = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.numWatermarkWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numWatermarkHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxWatermark)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.grpCrop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numCropRight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCropLeft)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCropBottom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCropTop)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numRemapPercent)).BeginInit();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 19);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 13);
            this.label3.TabIndex = 66;
            this.label3.Text = "Height";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(104, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 65;
            this.label2.Text = "Width";
            // 
            // numWatermarkWidth
            // 
            this.numWatermarkWidth.Location = new System.Drawing.Point(148, 17);
            this.numWatermarkWidth.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numWatermarkWidth.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numWatermarkWidth.Name = "numWatermarkWidth";
            this.numWatermarkWidth.Size = new System.Drawing.Size(46, 20);
            this.numWatermarkWidth.TabIndex = 64;
            this.numWatermarkWidth.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numWatermarkWidth.ValueChanged += new System.EventHandler(this.numWatermarkWidth_ValueChanged);
            this.numWatermarkWidth.Leave += new System.EventHandler(this.OnWidthLeave);
            // 
            // numWatermarkHeight
            // 
            this.numWatermarkHeight.Location = new System.Drawing.Point(51, 17);
            this.numWatermarkHeight.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numWatermarkHeight.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numWatermarkHeight.Name = "numWatermarkHeight";
            this.numWatermarkHeight.Size = new System.Drawing.Size(46, 20);
            this.numWatermarkHeight.TabIndex = 63;
            this.numWatermarkHeight.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numWatermarkHeight.ValueChanged += new System.EventHandler(this.numWatermarkHeight_ValueChanged);
            this.numWatermarkHeight.Leave += new System.EventHandler(this.OnHeightLeave);
            // 
            // btnGraphicKey
            // 
            this.btnGraphicKey.Location = new System.Drawing.Point(308, 64);
            this.btnGraphicKey.Name = "btnGraphicKey";
            this.btnGraphicKey.Size = new System.Drawing.Size(60, 23);
            this.btnGraphicKey.TabIndex = 62;
            this.btnGraphicKey.Text = "Key Color";
            this.btnGraphicKey.UseVisualStyleBackColor = true;
            this.btnGraphicKey.Click += new System.EventHandler(this.btnGraphicKey_Click);
            // 
            // txtGraphicLocation
            // 
            this.txtGraphicLocation.Location = new System.Drawing.Point(53, 3);
            this.txtGraphicLocation.Name = "txtGraphicLocation";
            this.txtGraphicLocation.Size = new System.Drawing.Size(214, 20);
            this.txtGraphicLocation.TabIndex = 61;
            // 
            // picBoxWatermark
            // 
            this.picBoxWatermark.Location = new System.Drawing.Point(308, 3);
            this.picBoxWatermark.Name = "picBoxWatermark";
            this.picBoxWatermark.Size = new System.Drawing.Size(60, 55);
            this.picBoxWatermark.TabIndex = 60;
            this.picBoxWatermark.TabStop = false;
            // 
            // btnSelectGraphic
            // 
            this.btnSelectGraphic.Location = new System.Drawing.Point(275, 1);
            this.btnSelectGraphic.Name = "btnSelectGraphic";
            this.btnSelectGraphic.Size = new System.Drawing.Size(27, 23);
            this.btnSelectGraphic.TabIndex = 59;
            this.btnSelectGraphic.Text = "...";
            this.btnSelectGraphic.UseVisualStyleBackColor = true;
            this.btnSelectGraphic.Click += new System.EventHandler(this.btnSelectGraphic_Click);
            // 
            // lblLocation
            // 
            this.lblLocation.AutoSize = true;
            this.lblLocation.Location = new System.Drawing.Point(0, 6);
            this.lblLocation.Name = "lblLocation";
            this.lblLocation.Size = new System.Drawing.Size(48, 13);
            this.lblLocation.TabIndex = 67;
            this.lblLocation.Text = "Location";
            // 
            // chkLockAspect
            // 
            this.chkLockAspect.AutoSize = true;
            this.chkLockAspect.Location = new System.Drawing.Point(61, 45);
            this.chkLockAspect.Name = "chkLockAspect";
            this.chkLockAspect.Size = new System.Drawing.Size(108, 17);
            this.chkLockAspect.TabIndex = 68;
            this.chkLockAspect.Text = "Lock aspect ratio";
            this.chkLockAspect.UseVisualStyleBackColor = true;
			this.chkLockAspect.Checked = true;
            this.chkLockAspect.CheckedChanged += new System.EventHandler(this.chkLockAspect_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnResetStretch);
            this.groupBox1.Controls.Add(this.chkLockAspect);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.numWatermarkWidth);
            this.groupBox1.Controls.Add(this.numWatermarkHeight);
            this.groupBox1.Location = new System.Drawing.Point(3, 25);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(234, 66);
            this.groupBox1.TabIndex = 69;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Stretch";
            // 
            // btnResetStretch
            // 
            this.btnResetStretch.Location = new System.Drawing.Point(204, 14);
            this.btnResetStretch.Name = "btnResetStretch";
            this.btnResetStretch.Size = new System.Drawing.Size(23, 23);
            this.btnResetStretch.TabIndex = 69;
            this.btnResetStretch.Text = "Reset";
            this.btnResetStretch.UseVisualStyleBackColor = true;
            this.btnResetStretch.Click += new System.EventHandler(this.btnResetStretch_Click);
            // 
            // grpCrop
            // 
            this.grpCrop.Controls.Add(this.btnResetTrim);
            this.grpCrop.Controls.Add(this.label5);
            this.grpCrop.Controls.Add(this.label1);
            this.grpCrop.Controls.Add(this.numCropRight);
            this.grpCrop.Controls.Add(this.numCropLeft);
            this.grpCrop.Controls.Add(this.label4);
            this.grpCrop.Controls.Add(this.label6);
            this.grpCrop.Controls.Add(this.numCropBottom);
            this.grpCrop.Controls.Add(this.numCropTop);
            this.grpCrop.Location = new System.Drawing.Point(3, 93);
            this.grpCrop.Name = "grpCrop";
            this.grpCrop.Size = new System.Drawing.Size(365, 52);
            this.grpCrop.TabIndex = 72;
            this.grpCrop.TabStop = false;
            this.grpCrop.Text = "Crop";
            // 
            // btnResetTrim
            // 
            this.btnResetTrim.Location = new System.Drawing.Point(336, 17);
            this.btnResetTrim.Name = "btnResetTrim";
            this.btnResetTrim.Size = new System.Drawing.Size(23, 23);
            this.btnResetTrim.TabIndex = 75;
            this.btnResetTrim.Text = "Reset";
            this.btnResetTrim.UseVisualStyleBackColor = true;
            this.btnResetTrim.Click += new System.EventHandler(this.btnResetCrop_Click);
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
            this.label1.Size = new System.Drawing.Size(32, 13);
            this.label1.TabIndex = 73;
            this.label1.Text = "Right";
            // 
            // numCropRight
            // 
            this.numCropRight.Location = new System.Drawing.Point(290, 19);
            this.numCropRight.Name = "numCropRight";
            this.numCropRight.Size = new System.Drawing.Size(40, 20);
            this.numCropRight.TabIndex = 72;
            this.numCropRight.ValueChanged += new System.EventHandler(this.numCropRight_ValueChanged);
            this.numCropRight.Leave += new System.EventHandler(this.OnCropRightLeave);
            // 
            // numCropLeft
            // 
            this.numCropLeft.Location = new System.Drawing.Point(114, 19);
            this.numCropLeft.Name = "numCropLeft";
            this.numCropLeft.Size = new System.Drawing.Size(40, 20);
            this.numCropLeft.TabIndex = 71;
            this.numCropLeft.ValueChanged += new System.EventHandler(this.numCropLeft_ValueChanged);
            this.numCropLeft.Leave += new System.EventHandler(this.OnCropLeftLeave);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(5, 21);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(26, 13);
            this.label4.TabIndex = 70;
            this.label4.Text = "Top";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(160, 21);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(40, 13);
            this.label6.TabIndex = 69;
            this.label6.Text = "Bottom";
            // 
            // numCropBottom
            // 
            this.numCropBottom.Location = new System.Drawing.Point(206, 19);
            this.numCropBottom.Name = "numCropBottom";
            this.numCropBottom.Size = new System.Drawing.Size(40, 20);
            this.numCropBottom.TabIndex = 68;
            this.numCropBottom.ValueChanged += new System.EventHandler(this.numCropBottom_ValueChanged);
            this.numCropBottom.Leave += new System.EventHandler(this.OnCropBottomLeave);
            // 
            // numCropTop
            // 
            this.numCropTop.Location = new System.Drawing.Point(37, 19);
            this.numCropTop.Name = "numCropTop";
            this.numCropTop.Size = new System.Drawing.Size(40, 20);
            this.numCropTop.TabIndex = 67;
            this.numCropTop.ValueChanged += new System.EventHandler(this.numCropTop_ValueChanged);
            this.numCropTop.Leave += new System.EventHandler(this.OnCropTopLeave);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(246, 49);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(52, 13);
            this.label7.TabIndex = 70;
            this.label7.Text = "Remap %";
            // 
            // numRemapPercent
            // 
            this.numRemapPercent.Location = new System.Drawing.Point(250, 67);
            this.numRemapPercent.Name = "numRemapPercent";
            this.numRemapPercent.Size = new System.Drawing.Size(46, 20);
            this.numRemapPercent.TabIndex = 70;
            this.numRemapPercent.ValueChanged += new System.EventHandler(this.numRemapPercent_ValueChanged);
            // 
            // WatermarkGraphicControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.numRemapPercent);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.grpCrop);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lblLocation);
            this.Controls.Add(this.btnGraphicKey);
            this.Controls.Add(this.txtGraphicLocation);
            this.Controls.Add(this.picBoxWatermark);
            this.Controls.Add(this.btnSelectGraphic);
            this.Name = "WatermarkGraphicControl";
            this.Size = new System.Drawing.Size(371, 149);
            ((System.ComponentModel.ISupportInitialize)(this.numWatermarkWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numWatermarkHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxWatermark)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.grpCrop.ResumeLayout(false);
            this.grpCrop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numCropRight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCropLeft)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCropBottom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCropTop)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numRemapPercent)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numWatermarkWidth;
        private System.Windows.Forms.NumericUpDown numWatermarkHeight;
        private System.Windows.Forms.Button btnGraphicKey;
        private System.Windows.Forms.TextBox txtGraphicLocation;
        private System.Windows.Forms.PictureBox picBoxWatermark;
        private System.Windows.Forms.Button btnSelectGraphic;
        private System.Windows.Forms.Label lblLocation;
        private System.Windows.Forms.CheckBox chkLockAspect;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnResetStretch;
        private System.Windows.Forms.GroupBox grpCrop;
        private System.Windows.Forms.Button btnResetTrim;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numCropRight;
        private System.Windows.Forms.NumericUpDown numCropLeft;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown numCropBottom;
        private System.Windows.Forms.NumericUpDown numCropTop;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown numRemapPercent;
    }
}
