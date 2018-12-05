namespace Trebuchet.Controls
{
    partial class WatermarkTextControl
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
            this.btnForeColor = new System.Windows.Forms.Button();
            this.btnBackColor = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnInsertNewline = new System.Windows.Forms.Button();
            this.btnInsertCopyright = new System.Windows.Forms.Button();
            this.txtWaterText = new System.Windows.Forms.RichTextBox();
            this.btnSelectFont = new System.Windows.Forms.Button();
            this.fontDialog1 = new System.Windows.Forms.FontDialog();
            this.SuspendLayout();
            // 
            // btnForeColor
            // 
            this.btnForeColor.Location = new System.Drawing.Point(279, 30);
            this.btnForeColor.Name = "btnForeColor";
            this.btnForeColor.Size = new System.Drawing.Size(30, 23);
            this.btnForeColor.TabIndex = 60;
            this.btnForeColor.Text = "F";
            this.btnForeColor.UseVisualStyleBackColor = true;
            this.btnForeColor.Click += new System.EventHandler(this.btnForeColor_Click);
            // 
            // btnBackColor
            // 
            this.btnBackColor.Location = new System.Drawing.Point(235, 30);
            this.btnBackColor.Name = "btnBackColor";
            this.btnBackColor.Size = new System.Drawing.Size(30, 23);
            this.btnBackColor.TabIndex = 59;
            this.btnBackColor.Text = "S";
            this.btnBackColor.UseVisualStyleBackColor = true;
            this.btnBackColor.Click += new System.EventHandler(this.btnBackColor_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 127);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 13);
            this.label1.TabIndex = 58;
            this.label1.Text = "Quick Picks";
            // 
            // btnInsertNewline
            // 
            this.btnInsertNewline.Location = new System.Drawing.Point(153, 122);
            this.btnInsertNewline.Name = "btnInsertNewline";
            this.btnInsertNewline.Size = new System.Drawing.Size(60, 23);
            this.btnInsertNewline.TabIndex = 57;
            this.btnInsertNewline.Text = "New Line";
            this.btnInsertNewline.UseVisualStyleBackColor = true;
            this.btnInsertNewline.Click += new System.EventHandler(this.btnInsertNewline_Click);
            // 
            // btnInsertCopyright
            // 
            this.btnInsertCopyright.Location = new System.Drawing.Point(73, 122);
            this.btnInsertCopyright.Name = "btnInsertCopyright";
            this.btnInsertCopyright.Size = new System.Drawing.Size(66, 23);
            this.btnInsertCopyright.TabIndex = 56;
            this.btnInsertCopyright.Text = "Copyright";
            this.btnInsertCopyright.UseVisualStyleBackColor = true;
            this.btnInsertCopyright.Click += new System.EventHandler(this.btnInsertCopyright_Click);
            // 
            // txtWaterText
            // 
            this.txtWaterText.Location = new System.Drawing.Point(6, 3);
            this.txtWaterText.Name = "txtWaterText";
            this.txtWaterText.Size = new System.Drawing.Size(207, 113);
            this.txtWaterText.TabIndex = 55;
            this.txtWaterText.Text = "";
            this.txtWaterText.Leave += new System.EventHandler(this.OnTxtWatermarkLeave);
            this.txtWaterText.TextChanged += new System.EventHandler(this.txtWaterText_TextChanged);
            // 
            // btnSelectFont
            // 
            this.btnSelectFont.Location = new System.Drawing.Point(235, 1);
            this.btnSelectFont.Name = "btnSelectFont";
            this.btnSelectFont.Size = new System.Drawing.Size(75, 23);
            this.btnSelectFont.TabIndex = 54;
            this.btnSelectFont.Text = "Select Font";
            this.btnSelectFont.UseVisualStyleBackColor = true;
            this.btnSelectFont.Click += new System.EventHandler(this.btnSelectFont_Click);
            // 
            // WatermarkTextControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnForeColor);
            this.Controls.Add(this.btnBackColor);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnInsertNewline);
            this.Controls.Add(this.btnInsertCopyright);
            this.Controls.Add(this.txtWaterText);
            this.Controls.Add(this.btnSelectFont);
            this.Name = "WatermarkTextControl";
            this.Size = new System.Drawing.Size(371, 149);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnForeColor;
        private System.Windows.Forms.Button btnBackColor;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnInsertNewline;
        private System.Windows.Forms.Button btnInsertCopyright;
        private System.Windows.Forms.RichTextBox txtWaterText;
        private System.Windows.Forms.Button btnSelectFont;
        private System.Windows.Forms.FontDialog fontDialog1;
    }
}
