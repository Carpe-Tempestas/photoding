namespace Trebuchet.Controls.Uploading
{
	partial class EmailControl
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
			this.txtTo = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.comboFrom = new System.Windows.Forms.ComboBox();
			this.txtSubject = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.chkUseAlbum = new System.Windows.Forms.CheckBox();
			this.txtBody = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.comboAttachMode = new System.Windows.Forms.ComboBox();
			this.label5 = new System.Windows.Forms.Label();
			this.chkPromptSend = new System.Windows.Forms.CheckBox();
			this.btnTo = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// txtTo
			// 
			this.txtTo.Location = new System.Drawing.Point(67, 42);
			this.txtTo.Name = "txtTo";
			this.txtTo.Size = new System.Drawing.Size(322, 20);
			this.txtTo.TabIndex = 1;
			this.txtTo.Visible = false;
			this.txtTo.TextChanged += new System.EventHandler(this.txtTo_TextChanged);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(3, 75);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(30, 13);
			this.label2.TabIndex = 2;
			this.label2.Text = "From";
			this.label2.Visible = false;
			// 
			// comboFrom
			// 
			this.comboFrom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboFrom.FormattingEnabled = true;
			this.comboFrom.Location = new System.Drawing.Point(39, 72);
			this.comboFrom.Name = "comboFrom";
			this.comboFrom.Size = new System.Drawing.Size(199, 21);
			this.comboFrom.TabIndex = 3;
			this.comboFrom.Visible = false;
			this.comboFrom.SelectedIndexChanged += new System.EventHandler(this.comboFrom_SelectedIndexChanged);
			// 
			// txtSubject
			// 
			this.txtSubject.Location = new System.Drawing.Point(52, 42);
			this.txtSubject.Name = "txtSubject";
			this.txtSubject.Size = new System.Drawing.Size(186, 20);
			this.txtSubject.TabIndex = 5;
			this.txtSubject.TextChanged += new System.EventHandler(this.txtSubject_TextChanged);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(3, 45);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(43, 13);
			this.label3.TabIndex = 4;
			this.label3.Text = "Subject";
			// 
			// chkUseAlbum
			// 
			this.chkUseAlbum.AutoSize = true;
			this.chkUseAlbum.Location = new System.Drawing.Point(244, 44);
			this.chkUseAlbum.Name = "chkUseAlbum";
			this.chkUseAlbum.Size = new System.Drawing.Size(105, 17);
			this.chkUseAlbum.TabIndex = 6;
			this.chkUseAlbum.Text = "Use album name";
			this.chkUseAlbum.UseVisualStyleBackColor = true;
			this.chkUseAlbum.CheckedChanged += new System.EventHandler(this.checkUseAlbum_CheckedChanged);
			// 
			// txtBody
			// 
			this.txtBody.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.txtBody.Location = new System.Drawing.Point(6, 97);
			this.txtBody.Multiline = true;
			this.txtBody.Name = "txtBody";
			this.txtBody.Size = new System.Drawing.Size(383, 130);
			this.txtBody.TabIndex = 8;
			this.txtBody.TextChanged += new System.EventHandler(this.txtText_TextChanged);
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(5, 75);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(31, 13);
			this.label4.TabIndex = 7;
			this.label4.Text = "Body";
			// 
			// comboAttachMode
			// 
			this.comboAttachMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboAttachMode.FormattingEnabled = true;
			this.comboAttachMode.Items.AddRange(new object[] {
            "All Pictures",
            "Compressed File"});
			this.comboAttachMode.Location = new System.Drawing.Point(143, 3);
			this.comboAttachMode.Name = "comboAttachMode";
			this.comboAttachMode.Size = new System.Drawing.Size(161, 21);
			this.comboAttachMode.TabIndex = 10;
			this.comboAttachMode.SelectedIndexChanged += new System.EventHandler(this.comboAttachMode_SelectedIndexChanged);
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(69, 6);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(68, 13);
			this.label5.TabIndex = 9;
			this.label5.Text = "Attach Mode";
			// 
			// chkPromptSend
			// 
			this.chkPromptSend.AutoSize = true;
			this.chkPromptSend.Location = new System.Drawing.Point(244, 74);
			this.chkPromptSend.Name = "chkPromptSend";
			this.chkPromptSend.Size = new System.Drawing.Size(97, 17);
			this.chkPromptSend.TabIndex = 11;
			this.chkPromptSend.Text = "Prompt to send";
			this.chkPromptSend.UseVisualStyleBackColor = true;
			this.chkPromptSend.Visible = false;
			this.chkPromptSend.CheckedChanged += new System.EventHandler(this.checkPromptSend_CheckedChanged);
			// 
			// btnTo
			// 
			this.btnTo.Location = new System.Drawing.Point(3, 40);
			this.btnTo.Name = "btnTo";
			this.btnTo.Size = new System.Drawing.Size(58, 23);
			this.btnTo.TabIndex = 12;
			this.btnTo.Text = "To";
			this.btnTo.UseVisualStyleBackColor = true;
			this.btnTo.Visible = false;
			this.btnTo.Click += new System.EventHandler(this.btnTo_Click);
			// 
			// EmailControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.btnTo);
			this.Controls.Add(this.chkPromptSend);
			this.Controls.Add(this.comboAttachMode);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.txtBody);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.chkUseAlbum);
			this.Controls.Add(this.txtSubject);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.comboFrom);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.txtTo);
			this.Name = "EmailControl";
			this.Size = new System.Drawing.Size(392, 230);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox txtTo;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ComboBox comboFrom;
		private System.Windows.Forms.TextBox txtSubject;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.CheckBox chkUseAlbum;
		private System.Windows.Forms.TextBox txtBody;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.ComboBox comboAttachMode;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.CheckBox chkPromptSend;
		private System.Windows.Forms.Button btnTo;
	}
}
