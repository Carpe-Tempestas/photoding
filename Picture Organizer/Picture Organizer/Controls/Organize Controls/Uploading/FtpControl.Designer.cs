using System.Security;
namespace Trebuchet.Controls
{
    partial class FtpControl
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
			this.btnConnect = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.txtAddress = new System.Windows.Forms.TextBox();
			this.txtUsername = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.numPort = new System.Windows.Forms.NumericUpDown();
			this.txtLocation = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.panel1 = new System.Windows.Forms.Panel();
			this.txtAlbum = new System.Windows.Forms.TextBox();
			this.chkUseDefaultAlbum = new System.Windows.Forms.CheckBox();
			this.chkAppend = new System.Windows.Forms.CheckBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.chkRememberPassword = new System.Windows.Forms.CheckBox();
			this.txtPassword = new System.Windows.Forms.MaskedTextBox();
			this.panel2 = new System.Windows.Forms.Panel();
			this.treeFolders = new System.Windows.Forms.TreeView();
			((System.ComponentModel.ISupportInitialize)(this.numPort)).BeginInit();
			this.panel1.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.panel2.SuspendLayout();
			this.SuspendLayout();
			// 
			// btnConnect
			// 
			this.btnConnect.Location = new System.Drawing.Point(228, 47);
			this.btnConnect.Name = "btnConnect";
			this.btnConnect.Size = new System.Drawing.Size(85, 60);
			this.btnConnect.TabIndex = 0;
			this.btnConnect.Text = "Connect";
			this.btnConnect.UseVisualStyleBackColor = true;
			this.btnConnect.Click += new System.EventHandler(this.btnTestConnection_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 19);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(45, 13);
			this.label1.TabIndex = 1;
			this.label1.Text = "Address";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(12, 47);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(55, 13);
			this.label2.TabIndex = 2;
			this.label2.Text = "Username";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(12, 73);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(53, 13);
			this.label3.TabIndex = 3;
			this.label3.Text = "Password";
			// 
			// txtAddress
			// 
			this.txtAddress.Location = new System.Drawing.Point(73, 18);
			this.txtAddress.Name = "txtAddress";
			this.txtAddress.Size = new System.Drawing.Size(135, 20);
			this.txtAddress.TabIndex = 4;
			this.txtAddress.TextChanged += new System.EventHandler(this.txtAddress_TextChanged);
			// 
			// txtUsername
			// 
			this.txtUsername.Location = new System.Drawing.Point(73, 44);
			this.txtUsername.Name = "txtUsername";
			this.txtUsername.Size = new System.Drawing.Size(135, 20);
			this.txtUsername.TabIndex = 5;
			this.txtUsername.TextChanged += new System.EventHandler(this.txtUsername_TextChanged);
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(214, 21);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(26, 13);
			this.label4.TabIndex = 7;
			this.label4.Text = "Port";
			// 
			// numPort
			// 
			this.numPort.Location = new System.Drawing.Point(246, 19);
			this.numPort.Name = "numPort";
			this.numPort.Size = new System.Drawing.Size(56, 20);
			this.numPort.TabIndex = 8;
			this.numPort.Value = new decimal(new int[] {
            21,
            0,
            0,
            0});
			// 
			// txtLocation
			// 
			this.txtLocation.Location = new System.Drawing.Point(60, 146);
			this.txtLocation.Name = "txtLocation";
			this.txtLocation.Size = new System.Drawing.Size(264, 20);
			this.txtLocation.TabIndex = 10;
			this.txtLocation.MouseHover += new System.EventHandler(this.txtLocation_MouseHover);
			this.txtLocation.TextChanged += new System.EventHandler(this.txtLocation_TextChanged);
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(6, 149);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(48, 13);
			this.label5.TabIndex = 9;
			this.label5.Text = "Location";
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.txtAlbum);
			this.panel1.Controls.Add(this.chkUseDefaultAlbum);
			this.panel1.Controls.Add(this.chkAppend);
			this.panel1.Controls.Add(this.groupBox1);
			this.panel1.Controls.Add(this.txtLocation);
			this.panel1.Controls.Add(this.label5);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(331, 173);
			this.panel1.TabIndex = 11;
			// 
			// txtAlbum
			// 
			this.txtAlbum.Enabled = false;
			this.txtAlbum.Location = new System.Drawing.Point(127, 120);
			this.txtAlbum.Name = "txtAlbum";
			this.txtAlbum.Size = new System.Drawing.Size(108, 20);
			this.txtAlbum.TabIndex = 14;
			this.txtAlbum.TextChanged += new System.EventHandler(this.txtAlbum_TextChanged);
			// 
			// chkUseDefaultAlbum
			// 
			this.chkUseDefaultAlbum.AutoSize = true;
			this.chkUseDefaultAlbum.Checked = true;
			this.chkUseDefaultAlbum.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkUseDefaultAlbum.Location = new System.Drawing.Point(10, 122);
			this.chkUseDefaultAlbum.Name = "chkUseDefaultAlbum";
			this.chkUseDefaultAlbum.Size = new System.Drawing.Size(111, 17);
			this.chkUseDefaultAlbum.TabIndex = 13;
			this.chkUseDefaultAlbum.Text = "Use default album";
			this.chkUseDefaultAlbum.UseVisualStyleBackColor = true;
			this.chkUseDefaultAlbum.CheckedChanged += new System.EventHandler(this.chkUseDefaultAlbum_CheckedChanged);
			// 
			// chkAppend
			// 
			this.chkAppend.AutoSize = true;
			this.chkAppend.Checked = true;
			this.chkAppend.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkAppend.Location = new System.Drawing.Point(251, 122);
			this.chkAppend.Name = "chkAppend";
			this.chkAppend.Size = new System.Drawing.Size(63, 17);
			this.chkAppend.TabIndex = 12;
			this.chkAppend.Text = "Append";
			this.chkAppend.UseVisualStyleBackColor = true;
			this.chkAppend.CheckedChanged += new System.EventHandler(this.chkAppend_CheckedChanged);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.chkRememberPassword);
			this.groupBox1.Controls.Add(this.txtAddress);
			this.groupBox1.Controls.Add(this.btnConnect);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.numPort);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.label4);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.txtPassword);
			this.groupBox1.Controls.Add(this.txtUsername);
			this.groupBox1.Location = new System.Drawing.Point(5, 3);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(319, 113);
			this.groupBox1.TabIndex = 11;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Connection Settings";
			// 
			// chkRememberPassword
			// 
			this.chkRememberPassword.AutoSize = true;
			this.chkRememberPassword.Location = new System.Drawing.Point(73, 90);
			this.chkRememberPassword.Name = "chkRememberPassword";
			this.chkRememberPassword.Size = new System.Drawing.Size(125, 17);
			this.chkRememberPassword.TabIndex = 17;
			this.chkRememberPassword.Text = "Remember password";
			this.chkRememberPassword.UseVisualStyleBackColor = true;
			this.chkRememberPassword.CheckedChanged += new System.EventHandler(this.chkRememberPassword_CheckedChanged);
			// 
			// txtPassword
			// 
			this.txtPassword.Location = new System.Drawing.Point(73, 70);
			this.txtPassword.Name = "txtPassword";
			this.txtPassword.PasswordChar = '*';
			this.txtPassword.Size = new System.Drawing.Size(100, 20);
			this.txtPassword.TabIndex = 6;
			this.txtPassword.UseSystemPasswordChar = true;
			this.txtPassword.TextChanged += new System.EventHandler(this.txtPassword_TextChanged);
			// 
			// panel2
			// 
			this.panel2.Controls.Add(this.treeFolders);
			this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel2.Location = new System.Drawing.Point(0, 173);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(331, 126);
			this.panel2.TabIndex = 12;
			// 
			// treeFolders
			// 
			this.treeFolders.Dock = System.Windows.Forms.DockStyle.Fill;
			this.treeFolders.Location = new System.Drawing.Point(0, 0);
			this.treeFolders.Margin = new System.Windows.Forms.Padding(0);
			this.treeFolders.Name = "treeFolders";
			this.treeFolders.Size = new System.Drawing.Size(331, 126);
			this.treeFolders.TabIndex = 0;
			this.treeFolders.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeFolders_AfterSelect);
			this.treeFolders.AfterExpand += new System.Windows.Forms.TreeViewEventHandler(this.treeFolders_AfterExpand);
			// 
			// FtpControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.panel2);
			this.Controls.Add(this.panel1);
			this.Name = "FtpControl";
			this.Size = new System.Drawing.Size(331, 299);
			((System.ComponentModel.ISupportInitialize)(this.numPort)).EndInit();
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.panel2.ResumeLayout(false);
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtAddress;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.MaskedTextBox txtPassword;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown numPort;
        private System.Windows.Forms.TextBox txtLocation;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TreeView treeFolders;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox chkUseDefaultAlbum;
        private System.Windows.Forms.CheckBox chkAppend;
        private System.Windows.Forms.TextBox txtAlbum;
		private System.Windows.Forms.CheckBox chkRememberPassword;
    }
}
