namespace Trebuchet.Controls
{
    partial class RenameControl
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
			this.chkRenameOriginals = new System.Windows.Forms.CheckBox();
			this.chkCreateRename = new System.Windows.Forms.CheckBox();
			this.chkFlipNumeric = new System.Windows.Forms.CheckBox();
			this.label3 = new System.Windows.Forms.Label();
			this.numIDLength = new System.Windows.Forms.NumericUpDown();
			this.label2 = new System.Windows.Forms.Label();
			this.txtMask = new System.Windows.Forms.TextBox();
			this.btnDate = new System.Windows.Forms.Button();
			this.chkInsertDate = new System.Windows.Forms.CheckBox();
			this.chkUseAlbum = new System.Windows.Forms.CheckBox();
			((System.ComponentModel.ISupportInitialize)(this.numIDLength)).BeginInit();
			this.SuspendLayout();
			// 
			// chkRenameOriginals
			// 
			this.chkRenameOriginals.AutoSize = true;
			this.chkRenameOriginals.Location = new System.Drawing.Point(6, 66);
			this.chkRenameOriginals.Name = "chkRenameOriginals";
			this.chkRenameOriginals.Size = new System.Drawing.Size(107, 17);
			this.chkRenameOriginals.TabIndex = 31;
			this.chkRenameOriginals.Text = "Rename originals";
			this.chkRenameOriginals.UseVisualStyleBackColor = true;
			this.chkRenameOriginals.CheckedChanged += new System.EventHandler(this.chkRenameOriginals_CheckedChanged);
			// 
			// chkCreateRename
			// 
			this.chkCreateRename.AutoSize = true;
			this.chkCreateRename.Location = new System.Drawing.Point(221, 66);
			this.chkCreateRename.Name = "chkCreateRename";
			this.chkCreateRename.Size = new System.Drawing.Size(138, 17);
			this.chkCreateRename.TabIndex = 30;
			this.chkCreateRename.Text = "Create rename directory";
			this.chkCreateRename.UseVisualStyleBackColor = true;
			this.chkCreateRename.CheckedChanged += new System.EventHandler(this.chkCreateRename_CheckedChanged);
			// 
			// chkFlipNumeric
			// 
			this.chkFlipNumeric.AutoSize = true;
			this.chkFlipNumeric.Location = new System.Drawing.Point(188, 31);
			this.chkFlipNumeric.Name = "chkFlipNumeric";
			this.chkFlipNumeric.Size = new System.Drawing.Size(157, 17);
			this.chkFlipNumeric.TabIndex = 29;
			this.chkFlipNumeric.Text = "Put numeric ID before mask";
			this.chkFlipNumeric.UseVisualStyleBackColor = true;
			this.chkFlipNumeric.CheckedChanged += new System.EventHandler(this.chkFlipNumeric_CheckedChanged);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(185, 4);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(99, 13);
			this.label3.TabIndex = 28;
			this.label3.Text = "Numeric ID Length:";
			// 
			// numIDLength
			// 
			this.numIDLength.Location = new System.Drawing.Point(290, 1);
			this.numIDLength.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.numIDLength.Name = "numIDLength";
			this.numIDLength.Size = new System.Drawing.Size(50, 20);
			this.numIDLength.TabIndex = 27;
			this.numIDLength.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.numIDLength.ValueChanged += new System.EventHandler(this.numIDLength_ValueChanged);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(3, 4);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(54, 13);
			this.label2.TabIndex = 26;
			this.label2.Text = "File mask:";
			// 
			// txtMask
			// 
			this.txtMask.Location = new System.Drawing.Point(63, 1);
			this.txtMask.Name = "txtMask";
			this.txtMask.Size = new System.Drawing.Size(100, 20);
			this.txtMask.TabIndex = 25;
			this.txtMask.TextChanged += new System.EventHandler(this.txtMask_TextChanged);
			// 
			// btnDate
			// 
			this.btnDate.Location = new System.Drawing.Point(88, 27);
			this.btnDate.Name = "btnDate";
			this.btnDate.Size = new System.Drawing.Size(75, 23);
			this.btnDate.TabIndex = 32;
			this.btnDate.Text = "Define Date";
			this.btnDate.UseVisualStyleBackColor = true;
			this.btnDate.Click += new System.EventHandler(this.btnDate_Click);
			// 
			// chkInsertDate
			// 
			this.chkInsertDate.AutoSize = true;
			this.chkInsertDate.Location = new System.Drawing.Point(6, 31);
			this.chkInsertDate.Name = "chkInsertDate";
			this.chkInsertDate.Size = new System.Drawing.Size(76, 17);
			this.chkInsertDate.TabIndex = 33;
			this.chkInsertDate.Text = "Insert date";
			this.chkInsertDate.UseVisualStyleBackColor = true;
			this.chkInsertDate.CheckedChanged += new System.EventHandler(this.chkInsertDate_CheckedChanged);
			// 
			// chkUseAlbum
			// 
			this.chkUseAlbum.AutoSize = true;
			this.chkUseAlbum.Location = new System.Drawing.Point(6, 89);
			this.chkUseAlbum.Name = "chkUseAlbum";
			this.chkUseAlbum.Size = new System.Drawing.Size(163, 17);
			this.chkUseAlbum.TabIndex = 34;
			this.chkUseAlbum.Text = "Use album name as file mask";
			this.chkUseAlbum.UseVisualStyleBackColor = true;
			this.chkUseAlbum.CheckedChanged += new System.EventHandler(this.chkUseAlbum_CheckedChanged);
			// 
			// RenameControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.White;
			this.Controls.Add(this.chkUseAlbum);
			this.Controls.Add(this.chkInsertDate);
			this.Controls.Add(this.btnDate);
			this.Controls.Add(this.chkRenameOriginals);
			this.Controls.Add(this.chkCreateRename);
			this.Controls.Add(this.chkFlipNumeric);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.numIDLength);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.txtMask);
			this.MinimumSize = new System.Drawing.Size(371, 94);
			this.Name = "RenameControl";
			this.Size = new System.Drawing.Size(371, 115);
			((System.ComponentModel.ISupportInitialize)(this.numIDLength)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox chkRenameOriginals;
        private System.Windows.Forms.CheckBox chkCreateRename;
        private System.Windows.Forms.CheckBox chkFlipNumeric;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown numIDLength;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtMask;
        private System.Windows.Forms.Button btnDate;
        private System.Windows.Forms.CheckBox chkInsertDate;
		private System.Windows.Forms.CheckBox chkUseAlbum;
    }
}
