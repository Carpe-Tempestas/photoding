namespace Trebuchet.Controls
{
    partial class ResizeControl
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
			this.numUpDownBoundHeight = new System.Windows.Forms.NumericUpDown();
			this.chkBoundHeight = new System.Windows.Forms.CheckBox();
			this.numUpDownBoundWidth = new System.Windows.Forms.NumericUpDown();
			this.chkBoundWidth = new System.Windows.Forms.CheckBox();
			this.rbResBound = new System.Windows.Forms.RadioButton();
			this.rbResPercent = new System.Windows.Forms.RadioButton();
			this.chkCreateResize = new System.Windows.Forms.CheckBox();
			this.label5 = new System.Windows.Forms.Label();
			this.numResizePercent = new System.Windows.Forms.NumericUpDown();
			((System.ComponentModel.ISupportInitialize)(this.numUpDownBoundHeight)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numUpDownBoundWidth)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numResizePercent)).BeginInit();
			this.SuspendLayout();
			// 
			// numUpDownBoundHeight
			// 
			this.numUpDownBoundHeight.Location = new System.Drawing.Point(269, 33);
			this.numUpDownBoundHeight.Maximum = new decimal(new int[] {
            999999,
            0,
            0,
            0});
			this.numUpDownBoundHeight.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.numUpDownBoundHeight.Name = "numUpDownBoundHeight";
			this.numUpDownBoundHeight.Size = new System.Drawing.Size(50, 20);
			this.numUpDownBoundHeight.TabIndex = 52;
			this.numUpDownBoundHeight.Value = new decimal(new int[] {
            500,
            0,
            0,
            0});
			this.numUpDownBoundHeight.ValueChanged += new System.EventHandler(this.numUpDownBoundHeight_ValueChanged);
			// 
			// chkBoundHeight
			// 
			this.chkBoundHeight.AutoSize = true;
			this.chkBoundHeight.Location = new System.Drawing.Point(206, 34);
			this.chkBoundHeight.Name = "chkBoundHeight";
			this.chkBoundHeight.Size = new System.Drawing.Size(57, 17);
			this.chkBoundHeight.TabIndex = 51;
			this.chkBoundHeight.Text = "Height";
			this.chkBoundHeight.UseVisualStyleBackColor = true;
			this.chkBoundHeight.CheckedChanged += new System.EventHandler(this.chkBoundHeight_CheckedChanged);
			// 
			// numUpDownBoundWidth
			// 
			this.numUpDownBoundWidth.Location = new System.Drawing.Point(135, 33);
			this.numUpDownBoundWidth.Maximum = new decimal(new int[] {
            999999,
            0,
            0,
            0});
			this.numUpDownBoundWidth.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.numUpDownBoundWidth.Name = "numUpDownBoundWidth";
			this.numUpDownBoundWidth.Size = new System.Drawing.Size(50, 20);
			this.numUpDownBoundWidth.TabIndex = 50;
			this.numUpDownBoundWidth.Value = new decimal(new int[] {
            500,
            0,
            0,
            0});
			this.numUpDownBoundWidth.ValueChanged += new System.EventHandler(this.numUpDownBoundWidth_ValueChanged);
			// 
			// chkBoundWidth
			// 
			this.chkBoundWidth.AutoSize = true;
			this.chkBoundWidth.Location = new System.Drawing.Point(76, 34);
			this.chkBoundWidth.Name = "chkBoundWidth";
			this.chkBoundWidth.Size = new System.Drawing.Size(54, 17);
			this.chkBoundWidth.TabIndex = 49;
			this.chkBoundWidth.Text = "Width";
			this.chkBoundWidth.UseVisualStyleBackColor = true;
			this.chkBoundWidth.CheckedChanged += new System.EventHandler(this.chkBoundWidth_CheckedChanged);
			// 
			// rbResBound
			// 
			this.rbResBound.AutoSize = true;
			this.rbResBound.Location = new System.Drawing.Point(3, 33);
			this.rbResBound.Name = "rbResBound";
			this.rbResBound.Size = new System.Drawing.Size(73, 17);
			this.rbResBound.TabIndex = 48;
			this.rbResBound.TabStop = true;
			this.rbResBound.Text = "Bound by:";
			this.rbResBound.UseVisualStyleBackColor = true;
			this.rbResBound.CheckedChanged += new System.EventHandler(this.rbResBound_CheckedChanged);
			// 
			// rbResPercent
			// 
			this.rbResPercent.AutoSize = true;
			this.rbResPercent.Location = new System.Drawing.Point(3, 3);
			this.rbResPercent.Name = "rbResPercent";
			this.rbResPercent.Size = new System.Drawing.Size(72, 17);
			this.rbResPercent.TabIndex = 47;
			this.rbResPercent.TabStop = true;
			this.rbResPercent.Text = "Resize to:";
			this.rbResPercent.UseVisualStyleBackColor = true;
			this.rbResPercent.CheckedChanged += new System.EventHandler(this.rbResPercent_CheckedChanged);
			// 
			// chkCreateResize
			// 
			this.chkCreateResize.AutoSize = true;
			this.chkCreateResize.Location = new System.Drawing.Point(236, 71);
			this.chkCreateResize.Name = "chkCreateResize";
			this.chkCreateResize.Size = new System.Drawing.Size(130, 17);
			this.chkCreateResize.TabIndex = 46;
			this.chkCreateResize.Text = "Create resize directory";
			this.chkCreateResize.UseVisualStyleBackColor = true;
			this.chkCreateResize.CheckedChanged += new System.EventHandler(this.chkCreateResize_CheckedChanged);
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(132, 5);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(84, 13);
			this.label5.TabIndex = 45;
			this.label5.Text = "% of original size";
			// 
			// numResizePercent
			// 
			this.numResizePercent.Location = new System.Drawing.Point(76, 3);
			this.numResizePercent.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.numResizePercent.Name = "numResizePercent";
			this.numResizePercent.Size = new System.Drawing.Size(50, 20);
			this.numResizePercent.TabIndex = 44;
			this.numResizePercent.Value = new decimal(new int[] {
            25,
            0,
            0,
            0});
			this.numResizePercent.ValueChanged += new System.EventHandler(this.numResizePercent_ValueChanged);
			// 
			// ResizeControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.White;
			this.Controls.Add(this.numUpDownBoundHeight);
			this.Controls.Add(this.chkBoundHeight);
			this.Controls.Add(this.numUpDownBoundWidth);
			this.Controls.Add(this.chkBoundWidth);
			this.Controls.Add(this.rbResBound);
			this.Controls.Add(this.rbResPercent);
			this.Controls.Add(this.chkCreateResize);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.numResizePercent);
			this.MinimumSize = new System.Drawing.Size(369, 97);
			this.Name = "ResizeControl";
			this.Size = new System.Drawing.Size(369, 97);
			((System.ComponentModel.ISupportInitialize)(this.numUpDownBoundHeight)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numUpDownBoundWidth)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numResizePercent)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown numUpDownBoundHeight;
        private System.Windows.Forms.CheckBox chkBoundHeight;
        private System.Windows.Forms.NumericUpDown numUpDownBoundWidth;
        private System.Windows.Forms.CheckBox chkBoundWidth;
        private System.Windows.Forms.RadioButton rbResBound;
        private System.Windows.Forms.RadioButton rbResPercent;
        private System.Windows.Forms.CheckBox chkCreateResize;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown numResizePercent;
    }
}
