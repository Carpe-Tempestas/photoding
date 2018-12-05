namespace Trebuchet.Controls
{
    partial class ControlWrapper
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
			this.btnDisabled = new System.Windows.Forms.Button();
			this.txtDirections = new System.Windows.Forms.TextBox();
			this.panelControl = new System.Windows.Forms.Panel();
			this.btnAddScript = new System.Windows.Forms.Button();
			this.panelMode = new System.Windows.Forms.Panel();
			this.panelMode.SuspendLayout();
			this.SuspendLayout();
			// 
			// btnDisabled
			// 
			this.btnDisabled.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnDisabled.ForeColor = System.Drawing.Color.Red;
			this.btnDisabled.Location = new System.Drawing.Point(3, 3);
			this.btnDisabled.Name = "btnDisabled";
			this.btnDisabled.Size = new System.Drawing.Size(294, 26);
			this.btnDisabled.TabIndex = 0;
			this.btnDisabled.Text = "Disabled - click here to enable!";
			this.btnDisabled.UseVisualStyleBackColor = true;
			this.btnDisabled.Click += new System.EventHandler(this.btnDisabled_Click);
			// 
			// txtDirections
			// 
			this.txtDirections.BackColor = System.Drawing.Color.White;
			this.txtDirections.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.txtDirections.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.txtDirections.Location = new System.Drawing.Point(0, 441);
			this.txtDirections.Margin = new System.Windows.Forms.Padding(20, 3, 20, 3);
			this.txtDirections.Multiline = true;
			this.txtDirections.Name = "txtDirections";
			this.txtDirections.ReadOnly = true;
			this.txtDirections.Size = new System.Drawing.Size(586, 78);
			this.txtDirections.TabIndex = 1;
			// 
			// panelControl
			// 
			this.panelControl.AutoScroll = true;
			this.panelControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelControl.Location = new System.Drawing.Point(0, 33);
			this.panelControl.Name = "panelControl";
			this.panelControl.Size = new System.Drawing.Size(404, 263);
			this.panelControl.TabIndex = 3;
			// 
			// btnAddScript
			// 
			this.btnAddScript.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnAddScript.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnAddScript.ForeColor = System.Drawing.Color.Black;
			this.btnAddScript.Location = new System.Drawing.Point(269, 3);
			this.btnAddScript.Name = "btnAddScript";
			this.btnAddScript.Size = new System.Drawing.Size(132, 26);
			this.btnAddScript.TabIndex = 0;
			this.btnAddScript.Text = "Add to Script";
			this.btnAddScript.UseVisualStyleBackColor = true;
			this.btnAddScript.Visible = false;
			// 
			// panelMode
			// 
			this.panelMode.Controls.Add(this.btnAddScript);
			this.panelMode.Controls.Add(this.btnDisabled);
			this.panelMode.Dock = System.Windows.Forms.DockStyle.Top;
			this.panelMode.Location = new System.Drawing.Point(0, 0);
			this.panelMode.Name = "panelMode";
			this.panelMode.Size = new System.Drawing.Size(404, 33);
			this.panelMode.TabIndex = 1;
			// 
			// ControlWrapper
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoScroll = true;
			this.BackColor = System.Drawing.Color.White;
			this.Controls.Add(this.panelControl);
			this.Controls.Add(this.panelMode);
			this.Name = "ControlWrapper";
			this.Size = new System.Drawing.Size(404, 296);
			this.panelMode.ResumeLayout(false);
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnDisabled;
		private System.Windows.Forms.TextBox txtDirections;
        private System.Windows.Forms.Panel panelControl;
		private System.Windows.Forms.Button btnAddScript;
		private System.Windows.Forms.Panel panelMode;
    }
}
