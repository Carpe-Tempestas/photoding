namespace Trebuchet.Controls.Uploading
{
	partial class PicasaControl
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
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.chkRememberPassword = new System.Windows.Forms.CheckBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.txtPassword = new System.Windows.Forms.MaskedTextBox();
			this.txtUsername = new System.Windows.Forms.TextBox();
			this.btnGoPicasa = new System.Windows.Forms.Button();
			this.lblDisclaimer = new System.Windows.Forms.Label();
			this.txtDescription = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.grpAlbum = new System.Windows.Forms.GroupBox();
			this.btnValidate = new System.Windows.Forms.Button();
			this.chkUseDefaultAlbum = new System.Windows.Forms.CheckBox();
			this.comboAlbums = new System.Windows.Forms.ComboBox();
			this.txtAlbum = new System.Windows.Forms.TextBox();
			this.btnGetAlbums = new System.Windows.Forms.Button();
			this.txtLocation = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.grpPrivacy = new System.Windows.Forms.GroupBox();
			this.rbPrivate = new System.Windows.Forms.RadioButton();
			this.rbPublic = new System.Windows.Forms.RadioButton();
			this.groupBox1.SuspendLayout();
			this.grpAlbum.SuspendLayout();
			this.grpPrivacy.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.chkRememberPassword);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.txtPassword);
			this.groupBox1.Controls.Add(this.txtUsername);
			this.groupBox1.Location = new System.Drawing.Point(3, 3);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(210, 98);
			this.groupBox1.TabIndex = 12;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Connection Settings";
			// 
			// chkRememberPassword
			// 
			this.chkRememberPassword.AutoSize = true;
			this.chkRememberPassword.Location = new System.Drawing.Point(67, 76);
			this.chkRememberPassword.Name = "chkRememberPassword";
			this.chkRememberPassword.Size = new System.Drawing.Size(125, 17);
			this.chkRememberPassword.TabIndex = 16;
			this.chkRememberPassword.Text = "Remember password";
			this.chkRememberPassword.UseVisualStyleBackColor = true;
			this.chkRememberPassword.CheckedChanged += new System.EventHandler(this.chkRememberPassword_CheckedChanged);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(6, 25);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(55, 13);
			this.label2.TabIndex = 2;
			this.label2.Text = "Username";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(6, 53);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(53, 13);
			this.label3.TabIndex = 3;
			this.label3.Text = "Password";
			// 
			// txtPassword
			// 
			this.txtPassword.Location = new System.Drawing.Point(67, 50);
			this.txtPassword.Name = "txtPassword";
			this.txtPassword.PasswordChar = '*';
			this.txtPassword.Size = new System.Drawing.Size(100, 20);
			this.txtPassword.TabIndex = 6;
			this.txtPassword.UseSystemPasswordChar = true;
			this.txtPassword.TextChanged += new System.EventHandler(this.txtPassword_TextChanged);
			// 
			// txtUsername
			// 
			this.txtUsername.Location = new System.Drawing.Point(67, 22);
			this.txtUsername.Name = "txtUsername";
			this.txtUsername.Size = new System.Drawing.Size(135, 20);
			this.txtUsername.TabIndex = 5;
			this.txtUsername.TextChanged += new System.EventHandler(this.txtUsername_TextChanged);
			// 
			// btnGoPicasa
			// 
			this.btnGoPicasa.Location = new System.Drawing.Point(283, 117);
			this.btnGoPicasa.Name = "btnGoPicasa";
			this.btnGoPicasa.Size = new System.Drawing.Size(67, 66);
			this.btnGoPicasa.TabIndex = 32;
			this.btnGoPicasa.Text = "Go to Picasa website!";
			this.btnGoPicasa.UseVisualStyleBackColor = true;
			this.btnGoPicasa.Click += new System.EventHandler(this.btnGoPicasa_Click);
			// 
			// lblDisclaimer
			// 
			this.lblDisclaimer.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.lblDisclaimer.Location = new System.Drawing.Point(0, 231);
			this.lblDisclaimer.Name = "lblDisclaimer";
			this.lblDisclaimer.Padding = new System.Windows.Forms.Padding(0, 0, 0, 6);
			this.lblDisclaimer.Size = new System.Drawing.Size(368, 19);
			this.lblDisclaimer.TabIndex = 36;
			this.lblDisclaimer.Text = "This product uses the Picasa API but is not endorsed or certified by Google.";
			// 
			// txtDescription
			// 
			this.txtDescription.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.txtDescription.Location = new System.Drawing.Point(5, 208);
			this.txtDescription.MinimumSize = new System.Drawing.Size(353, 20);
			this.txtDescription.Multiline = true;
			this.txtDescription.Name = "txtDescription";
			this.txtDescription.Size = new System.Drawing.Size(353, 20);
			this.txtDescription.TabIndex = 35;
			this.txtDescription.TextChanged += new System.EventHandler(this.txtDescription_TextChanged);
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(4, 192);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(60, 13);
			this.label6.TabIndex = 34;
			this.label6.Text = "Description";
			// 
			// grpAlbum
			// 
			this.grpAlbum.Controls.Add(this.btnValidate);
			this.grpAlbum.Controls.Add(this.chkUseDefaultAlbum);
			this.grpAlbum.Controls.Add(this.comboAlbums);
			this.grpAlbum.Controls.Add(this.txtAlbum);
			this.grpAlbum.Controls.Add(this.btnGetAlbums);
			this.grpAlbum.Location = new System.Drawing.Point(3, 107);
			this.grpAlbum.Name = "grpAlbum";
			this.grpAlbum.Size = new System.Drawing.Size(274, 76);
			this.grpAlbum.TabIndex = 33;
			this.grpAlbum.TabStop = false;
			this.grpAlbum.Text = "Album";
			// 
			// btnValidate
			// 
			this.btnValidate.Location = new System.Drawing.Point(237, 15);
			this.btnValidate.Name = "btnValidate";
			this.btnValidate.Size = new System.Drawing.Size(29, 23);
			this.btnValidate.TabIndex = 19;
			this.btnValidate.Text = "?";
			this.btnValidate.UseVisualStyleBackColor = true;
			this.btnValidate.Click += new System.EventHandler(this.btnValidate_Click);
			// 
			// chkUseDefaultAlbum
			// 
			this.chkUseDefaultAlbum.AutoSize = true;
			this.chkUseDefaultAlbum.Checked = true;
			this.chkUseDefaultAlbum.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkUseDefaultAlbum.Location = new System.Drawing.Point(6, 19);
			this.chkUseDefaultAlbum.Name = "chkUseDefaultAlbum";
			this.chkUseDefaultAlbum.Size = new System.Drawing.Size(111, 17);
			this.chkUseDefaultAlbum.TabIndex = 15;
			this.chkUseDefaultAlbum.Text = "Use default album";
			this.chkUseDefaultAlbum.UseVisualStyleBackColor = true;
			this.chkUseDefaultAlbum.CheckedChanged += new System.EventHandler(this.chkUseDefaultAlbum_CheckedChanged);
			// 
			// comboAlbums
			// 
			this.comboAlbums.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboAlbums.FormattingEnabled = true;
			this.comboAlbums.Location = new System.Drawing.Point(35, 45);
			this.comboAlbums.Name = "comboAlbums";
			this.comboAlbums.Size = new System.Drawing.Size(121, 21);
			this.comboAlbums.TabIndex = 18;
			this.comboAlbums.SelectedIndexChanged += new System.EventHandler(this.comboAlbums_SelectedIndexChanged);
			// 
			// txtAlbum
			// 
			this.txtAlbum.Location = new System.Drawing.Point(123, 17);
			this.txtAlbum.Name = "txtAlbum";
			this.txtAlbum.Size = new System.Drawing.Size(108, 20);
			this.txtAlbum.TabIndex = 16;
			this.txtAlbum.TextChanged += new System.EventHandler(this.txtAlbum_TextChanged);
			// 
			// btnGetAlbums
			// 
			this.btnGetAlbums.Location = new System.Drawing.Point(162, 43);
			this.btnGetAlbums.Name = "btnGetAlbums";
			this.btnGetAlbums.Size = new System.Drawing.Size(69, 23);
			this.btnGetAlbums.TabIndex = 17;
			this.btnGetAlbums.Text = "Get Albums";
			this.btnGetAlbums.UseVisualStyleBackColor = true;
			this.btnGetAlbums.Click += new System.EventHandler(this.btnGetAlbums_Click);
			// 
			// txtLocation
			// 
			this.txtLocation.Location = new System.Drawing.Point(57, 189);
			this.txtLocation.Name = "txtLocation";
			this.txtLocation.Size = new System.Drawing.Size(301, 20);
			this.txtLocation.TabIndex = 38;
			this.txtLocation.Visible = false;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(3, 192);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(48, 13);
			this.label1.TabIndex = 37;
			this.label1.Text = "Location";
			this.label1.Visible = false;
			// 
			// grpPrivacy
			// 
			this.grpPrivacy.Controls.Add(this.rbPrivate);
			this.grpPrivacy.Controls.Add(this.rbPublic);
			this.grpPrivacy.Location = new System.Drawing.Point(219, 3);
			this.grpPrivacy.Name = "grpPrivacy";
			this.grpPrivacy.Size = new System.Drawing.Size(138, 98);
			this.grpPrivacy.TabIndex = 39;
			this.grpPrivacy.TabStop = false;
			this.grpPrivacy.Text = "Privacy";
			// 
			// rbPrivate
			// 
			this.rbPrivate.AutoSize = true;
			this.rbPrivate.Checked = true;
			this.rbPrivate.Location = new System.Drawing.Point(45, 62);
			this.rbPrivate.Name = "rbPrivate";
			this.rbPrivate.Size = new System.Drawing.Size(58, 17);
			this.rbPrivate.TabIndex = 1;
			this.rbPrivate.TabStop = true;
			this.rbPrivate.Text = "Private";
			this.rbPrivate.UseVisualStyleBackColor = true;
			this.rbPrivate.CheckedChanged += new System.EventHandler(this.rbPrivate_CheckedChanged);
			// 
			// rbPublic
			// 
			this.rbPublic.AutoSize = true;
			this.rbPublic.Location = new System.Drawing.Point(44, 26);
			this.rbPublic.Name = "rbPublic";
			this.rbPublic.Size = new System.Drawing.Size(54, 17);
			this.rbPublic.TabIndex = 0;
			this.rbPublic.Text = "Public";
			this.rbPublic.UseVisualStyleBackColor = true;
			this.rbPublic.CheckedChanged += new System.EventHandler(this.rbPublic_CheckedChanged);
			// 
			// PicasaControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.grpPrivacy);
			this.Controls.Add(this.txtLocation);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.lblDisclaimer);
			this.Controls.Add(this.txtDescription);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.grpAlbum);
			this.Controls.Add(this.btnGoPicasa);
			this.Controls.Add(this.groupBox1);
			this.MinimumSize = new System.Drawing.Size(368, 250);
			this.Name = "PicasaControl";
			this.Size = new System.Drawing.Size(368, 250);
			this.Resize += new System.EventHandler(this.PicasaControl_Resize);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.grpAlbum.ResumeLayout(false);
			this.grpAlbum.PerformLayout();
			this.grpPrivacy.ResumeLayout(false);
			this.grpPrivacy.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.MaskedTextBox txtPassword;
		private System.Windows.Forms.TextBox txtUsername;
		private System.Windows.Forms.Button btnGoPicasa;
		private System.Windows.Forms.Label lblDisclaimer;
		private System.Windows.Forms.TextBox txtDescription;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.GroupBox grpAlbum;
		private System.Windows.Forms.Button btnValidate;
		private System.Windows.Forms.CheckBox chkUseDefaultAlbum;
		private System.Windows.Forms.ComboBox comboAlbums;
		private System.Windows.Forms.TextBox txtAlbum;
		private System.Windows.Forms.Button btnGetAlbums;
		private System.Windows.Forms.CheckBox chkRememberPassword;
		private System.Windows.Forms.TextBox txtLocation;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.GroupBox grpPrivacy;
		private System.Windows.Forms.RadioButton rbPrivate;
		private System.Windows.Forms.RadioButton rbPublic;
	}
}
