namespace Trebuchet.Controls.Uploading
{
	partial class FaceBookControl
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
			this.txtAlbum = new System.Windows.Forms.TextBox();
			this.chkUseDefaultAlbum = new System.Windows.Forms.CheckBox();
			this.btnGetAlbums = new System.Windows.Forms.Button();
			this.comboAlbums = new System.Windows.Forms.ComboBox();
			this.grpAlbum = new System.Windows.Forms.GroupBox();
			this.btnValidate = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.txtLocation = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.txtDescription = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.btnGoFacebook = new System.Windows.Forms.Button();
			this.grpAlbum.SuspendLayout();
			this.SuspendLayout();
			// 
			// txtAlbum
			// 
			this.txtAlbum.Location = new System.Drawing.Point(123, 17);
			this.txtAlbum.Name = "txtAlbum";
			this.txtAlbum.Size = new System.Drawing.Size(108, 20);
			this.txtAlbum.TabIndex = 16;
			this.txtAlbum.TextChanged += new System.EventHandler(this.txtAlbum_TextChanged);
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
			// grpAlbum
			// 
			this.grpAlbum.Controls.Add(this.btnValidate);
			this.grpAlbum.Controls.Add(this.chkUseDefaultAlbum);
			this.grpAlbum.Controls.Add(this.comboAlbums);
			this.grpAlbum.Controls.Add(this.txtAlbum);
			this.grpAlbum.Controls.Add(this.btnGetAlbums);
			this.grpAlbum.Location = new System.Drawing.Point(3, 3);
			this.grpAlbum.Name = "grpAlbum";
			this.grpAlbum.Size = new System.Drawing.Size(274, 76);
			this.grpAlbum.TabIndex = 19;
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
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(3, 88);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(48, 13);
			this.label1.TabIndex = 20;
			this.label1.Text = "Location";
			// 
			// txtLocation
			// 
			this.txtLocation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.txtLocation.Enabled = false;
			this.txtLocation.Location = new System.Drawing.Point(57, 85);
			this.txtLocation.Name = "txtLocation";
			this.txtLocation.Size = new System.Drawing.Size(331, 20);
			this.txtLocation.TabIndex = 21;
			this.txtLocation.TextChanged += new System.EventHandler(this.txtLocation_TextChanged);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(3, 108);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(60, 13);
			this.label2.TabIndex = 22;
			this.label2.Text = "Description";
			// 
			// txtDescription
			// 
			this.txtDescription.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.txtDescription.Location = new System.Drawing.Point(5, 124);
			this.txtDescription.Multiline = true;
			this.txtDescription.Name = "txtDescription";
			this.txtDescription.Size = new System.Drawing.Size(383, 100);
			this.txtDescription.TabIndex = 23;
			this.txtDescription.TextChanged += new System.EventHandler(this.txtDescription_TextChanged);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.label3.Location = new System.Drawing.Point(0, 227);
			this.label3.Name = "label3";
			this.label3.Padding = new System.Windows.Forms.Padding(0, 0, 0, 6);
			this.label3.Size = new System.Drawing.Size(393, 19);
			this.label3.TabIndex = 30;
			this.label3.Text = "This product uses the Facebook API but is not endorsed or certified by Facebook.";
			// 
			// btnGoFacebook
			// 
			this.btnGoFacebook.Location = new System.Drawing.Point(283, 13);
			this.btnGoFacebook.Name = "btnGoFacebook";
			this.btnGoFacebook.Size = new System.Drawing.Size(67, 66);
			this.btnGoFacebook.TabIndex = 31;
			this.btnGoFacebook.Text = "Go to Facebook website!";
			this.btnGoFacebook.UseVisualStyleBackColor = true;
			this.btnGoFacebook.Click += new System.EventHandler(this.btnGoFacebook_Click);
			// 
			// FaceBookControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.btnGoFacebook);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.txtDescription);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.txtLocation);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.grpAlbum);
			this.Name = "FaceBookControl";
			this.Size = new System.Drawing.Size(394, 246);
			this.grpAlbum.ResumeLayout(false);
			this.grpAlbum.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox txtAlbum;
		private System.Windows.Forms.CheckBox chkUseDefaultAlbum;
		private System.Windows.Forms.Button btnGetAlbums;
		private System.Windows.Forms.ComboBox comboAlbums;
		private System.Windows.Forms.GroupBox grpAlbum;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox txtLocation;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox txtDescription;
		private System.Windows.Forms.Button btnValidate;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Button btnGoFacebook;
	}
}
