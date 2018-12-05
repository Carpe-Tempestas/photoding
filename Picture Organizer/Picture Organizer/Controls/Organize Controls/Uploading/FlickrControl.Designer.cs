using System.Security;

namespace Trebuchet.Controls.Uploading
{
    partial class FlickrControl
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
			this.btnValidate = new System.Windows.Forms.Button();
			this.comboAlbums = new System.Windows.Forms.ComboBox();
			this.txtDescription = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.chkUseDefaultAlbum = new System.Windows.Forms.CheckBox();
			this.txtAlbum = new System.Windows.Forms.TextBox();
			this.btnGetAlbums = new System.Windows.Forms.Button();
			this.grpAlbum = new System.Windows.Forms.GroupBox();
			this.label1 = new System.Windows.Forms.Label();
			this.btnGoFlickr = new System.Windows.Forms.Button();
			this.rbPrivate = new System.Windows.Forms.RadioButton();
			this.rbPublic = new System.Windows.Forms.RadioButton();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.chkFamily = new System.Windows.Forms.CheckBox();
			this.chkFriends = new System.Windows.Forms.CheckBox();
			this.grpAlbum.SuspendLayout();
			this.SuspendLayout();
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
			// txtDescription
			// 
			this.txtDescription.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.txtDescription.Location = new System.Drawing.Point(6, 121);
			this.txtDescription.Multiline = true;
			this.txtDescription.Name = "txtDescription";
			this.txtDescription.Size = new System.Drawing.Size(348, 133);
			this.txtDescription.TabIndex = 28;
			this.txtDescription.TextChanged += new System.EventHandler(this.txtDescription_TextChanged);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(3, 105);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(60, 13);
			this.label2.TabIndex = 27;
			this.label2.Text = "Description";
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
			// 
			// txtAlbum
			// 
			this.txtAlbum.Location = new System.Drawing.Point(123, 17);
			this.txtAlbum.Name = "txtAlbum";
			this.txtAlbum.Size = new System.Drawing.Size(108, 20);
			this.txtAlbum.TabIndex = 16;
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
			// grpAlbum
			// 
			this.grpAlbum.Controls.Add(this.btnValidate);
			this.grpAlbum.Controls.Add(this.chkUseDefaultAlbum);
			this.grpAlbum.Controls.Add(this.comboAlbums);
			this.grpAlbum.Controls.Add(this.txtAlbum);
			this.grpAlbum.Controls.Add(this.btnGetAlbums);
			this.grpAlbum.Location = new System.Drawing.Point(4, 3);
			this.grpAlbum.Name = "grpAlbum";
			this.grpAlbum.Size = new System.Drawing.Size(274, 76);
			this.grpAlbum.TabIndex = 24;
			this.grpAlbum.TabStop = false;
			this.grpAlbum.Text = "Album (Photoset on Flickr)";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.label1.Location = new System.Drawing.Point(0, 254);
			this.label1.Name = "label1";
			this.label1.Padding = new System.Windows.Forms.Padding(0, 0, 0, 6);
			this.label1.Size = new System.Drawing.Size(347, 19);
			this.label1.TabIndex = 29;
			this.label1.Text = "This product uses the Flickr API but is not endorsed or certified by Flickr.";
			// 
			// btnGoFlickr
			// 
			this.btnGoFlickr.Location = new System.Drawing.Point(286, 12);
			this.btnGoFlickr.Name = "btnGoFlickr";
			this.btnGoFlickr.Size = new System.Drawing.Size(67, 66);
			this.btnGoFlickr.TabIndex = 30;
			this.btnGoFlickr.Text = "Go to Flickr website!";
			this.btnGoFlickr.UseVisualStyleBackColor = true;
			this.btnGoFlickr.Click += new System.EventHandler(this.btnGoFlickr_Click);
			// 
			// rbPrivate
			// 
			this.rbPrivate.AutoSize = true;
			this.rbPrivate.Checked = true;
			this.rbPrivate.Location = new System.Drawing.Point(112, 85);
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
			this.rbPublic.Location = new System.Drawing.Point(52, 85);
			this.rbPublic.Name = "rbPublic";
			this.rbPublic.Size = new System.Drawing.Size(54, 17);
			this.rbPublic.TabIndex = 0;
			this.rbPublic.Text = "Public";
			this.rbPublic.UseVisualStyleBackColor = true;
			this.rbPublic.CheckedChanged += new System.EventHandler(this.rbPublic_CheckedChanged);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(3, 87);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(42, 13);
			this.label3.TabIndex = 31;
			this.label3.Text = "Privacy";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(176, 87);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(62, 13);
			this.label4.TabIndex = 32;
			this.label4.Text = "Viewable to";
			// 
			// chkFamily
			// 
			this.chkFamily.AutoSize = true;
			this.chkFamily.Location = new System.Drawing.Point(244, 86);
			this.chkFamily.Name = "chkFamily";
			this.chkFamily.Size = new System.Drawing.Size(55, 17);
			this.chkFamily.TabIndex = 33;
			this.chkFamily.Text = "Family";
			this.chkFamily.UseVisualStyleBackColor = true;
			this.chkFamily.CheckedChanged += new System.EventHandler(this.chkFamily_CheckedChanged);
			// 
			// chkFriends
			// 
			this.chkFriends.AutoSize = true;
			this.chkFriends.Location = new System.Drawing.Point(298, 86);
			this.chkFriends.Name = "chkFriends";
			this.chkFriends.Size = new System.Drawing.Size(60, 17);
			this.chkFriends.TabIndex = 34;
			this.chkFriends.Text = "Friends";
			this.chkFriends.UseVisualStyleBackColor = true;
			this.chkFriends.CheckedChanged += new System.EventHandler(this.chkFriends_CheckedChanged);
			// 
			// FlickrControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.chkFriends);
			this.Controls.Add(this.chkFamily);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.rbPrivate);
			this.Controls.Add(this.rbPublic);
			this.Controls.Add(this.btnGoFlickr);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.txtDescription);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.grpAlbum);
			this.Name = "FlickrControl";
			this.Size = new System.Drawing.Size(359, 273);
			this.grpAlbum.ResumeLayout(false);
			this.grpAlbum.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

		private System.Windows.Forms.Button btnValidate;
		private System.Windows.Forms.ComboBox comboAlbums;
		private System.Windows.Forms.TextBox txtDescription;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.CheckBox chkUseDefaultAlbum;
		private System.Windows.Forms.TextBox txtAlbum;
		private System.Windows.Forms.Button btnGetAlbums;
		private System.Windows.Forms.GroupBox grpAlbum;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button btnGoFlickr;
		private System.Windows.Forms.RadioButton rbPrivate;
		private System.Windows.Forms.RadioButton rbPublic;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.CheckBox chkFamily;
		private System.Windows.Forms.CheckBox chkFriends;
	}
}
