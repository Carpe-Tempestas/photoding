namespace Trebuchet.Controls
{
    partial class CompressControl
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
			this.txtFile = new System.Windows.Forms.TextBox();
			this.btnSelectFile = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.mtxtPassword = new System.Windows.Forms.MaskedTextBox();
			this.btnClear = new System.Windows.Forms.Button();
			this.chkUseFolderName = new System.Windows.Forms.CheckBox();
			this.label3 = new System.Windows.Forms.Label();
			this.numZipLevel = new System.Windows.Forms.NumericUpDown();
			((System.ComponentModel.ISupportInitialize)(this.numZipLevel)).BeginInit();
			this.SuspendLayout();
			// 
			// txtFile
			// 
			this.txtFile.Location = new System.Drawing.Point(57, 36);
			this.txtFile.Name = "txtFile";
			this.txtFile.Size = new System.Drawing.Size(254, 20);
			this.txtFile.TabIndex = 0;
			this.txtFile.TextChanged += new System.EventHandler(this.txtFile_TextChanged);
			// 
			// btnSelectFile
			// 
			this.btnSelectFile.Location = new System.Drawing.Point(317, 34);
			this.btnSelectFile.Name = "btnSelectFile";
			this.btnSelectFile.Size = new System.Drawing.Size(26, 23);
			this.btnSelectFile.TabIndex = 1;
			this.btnSelectFile.Text = "...";
			this.btnSelectFile.UseVisualStyleBackColor = true;
			this.btnSelectFile.Click += new System.EventHandler(this.btnSelectFile_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(-2, 39);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(41, 13);
			this.label1.TabIndex = 2;
			this.label1.Text = "Zip File";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(-2, 80);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(53, 13);
			this.label2.TabIndex = 3;
			this.label2.Text = "Password";
			// 
			// mtxtPassword
			// 
			this.mtxtPassword.Location = new System.Drawing.Point(57, 77);
			this.mtxtPassword.Name = "mtxtPassword";
			this.mtxtPassword.Size = new System.Drawing.Size(100, 20);
			this.mtxtPassword.TabIndex = 4;
			this.mtxtPassword.UseSystemPasswordChar = true;
			this.mtxtPassword.TextChanged += new System.EventHandler(this.OnPasswordChanged);
			// 
			// btnClear
			// 
			this.btnClear.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnClear.ForeColor = System.Drawing.Color.Red;
			this.btnClear.Location = new System.Drawing.Point(163, 75);
			this.btnClear.Name = "btnClear";
			this.btnClear.Size = new System.Drawing.Size(26, 23);
			this.btnClear.TabIndex = 5;
			this.btnClear.Text = "X";
			this.btnClear.UseVisualStyleBackColor = true;
			this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
			// 
			// chkUseFolderName
			// 
			this.chkUseFolderName.AutoSize = true;
			this.chkUseFolderName.Location = new System.Drawing.Point(3, 3);
			this.chkUseFolderName.Name = "chkUseFolderName";
			this.chkUseFolderName.Size = new System.Drawing.Size(111, 17);
			this.chkUseFolderName.TabIndex = 6;
			this.chkUseFolderName.Text = "Use default album";
			this.chkUseFolderName.UseVisualStyleBackColor = true;
			this.chkUseFolderName.CheckedChanged += new System.EventHandler(this.chkUseFolderName_CheckedChanged);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(254, 79);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(51, 13);
			this.label3.TabIndex = 7;
			this.label3.Text = "Zip Level";
			// 
			// numZipLevel
			// 
			this.numZipLevel.Location = new System.Drawing.Point(311, 77);
			this.numZipLevel.Maximum = new decimal(new int[] {
            9,
            0,
            0,
            0});
			this.numZipLevel.Name = "numZipLevel";
			this.numZipLevel.Size = new System.Drawing.Size(32, 20);
			this.numZipLevel.TabIndex = 8;
			this.numZipLevel.Value = new decimal(new int[] {
            9,
            0,
            0,
            0});
			this.numZipLevel.ValueChanged += new System.EventHandler(this.numZipLevel_ValueChanged);
			// 
			// CompressControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.Transparent;
			this.Controls.Add(this.numZipLevel);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.chkUseFolderName);
			this.Controls.Add(this.btnClear);
			this.Controls.Add(this.mtxtPassword);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.btnSelectFile);
			this.Controls.Add(this.txtFile);
			this.MinimumSize = new System.Drawing.Size(358, 109);
			this.Name = "CompressControl";
			this.Size = new System.Drawing.Size(358, 109);
			((System.ComponentModel.ISupportInitialize)(this.numZipLevel)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtFile;
        private System.Windows.Forms.Button btnSelectFile;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.MaskedTextBox mtxtPassword;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.CheckBox chkUseFolderName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown numZipLevel;
    }
}
