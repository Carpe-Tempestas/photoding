namespace Trebuchet.Controls
{
    partial class ThumbnailControl
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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ThumbnailControl));
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.rotateToRightToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.rotateToLeftToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.contextMenuStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// pictureBox1
			// 
			this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
			this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
			this.pictureBox1.Location = new System.Drawing.Point(10, 10);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(90, 90);
			this.pictureBox1.TabIndex = 0;
			this.pictureBox1.TabStop = false;
			this.pictureBox1.Click += new System.EventHandler(this.OnPictureClicked);
			// 
			// contextMenuStrip1
			// 
			this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.rotateToRightToolStripMenuItem,
            this.rotateToLeftToolStripMenuItem,
            this.deleteToolStripMenuItem});
			this.contextMenuStrip1.Name = "contextMenuStrip1";
			this.contextMenuStrip1.Size = new System.Drawing.Size(206, 92);
			// 
			// rotateToRightToolStripMenuItem
			// 
			this.rotateToRightToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("rotateToRightToolStripMenuItem.Image")));
			this.rotateToRightToolStripMenuItem.Name = "rotateToRightToolStripMenuItem";
			this.rotateToRightToolStripMenuItem.Size = new System.Drawing.Size(205, 22);
			this.rotateToRightToolStripMenuItem.Text = "Rotate Clockwise";
			this.rotateToRightToolStripMenuItem.Click += new System.EventHandler(this.rotateToRightToolStripMenuItem_Click);
			// 
			// rotateToLeftToolStripMenuItem
			// 
			this.rotateToLeftToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("rotateToLeftToolStripMenuItem.Image")));
			this.rotateToLeftToolStripMenuItem.Name = "rotateToLeftToolStripMenuItem";
			this.rotateToLeftToolStripMenuItem.Size = new System.Drawing.Size(205, 22);
			this.rotateToLeftToolStripMenuItem.Text = "Rotate Counterclockwise";
			this.rotateToLeftToolStripMenuItem.Click += new System.EventHandler(this.rotateToLeftToolStripMenuItem_Click);
			// 
			// deleteToolStripMenuItem
			// 
			this.deleteToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("deleteToolStripMenuItem.Image")));
			this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
			this.deleteToolStripMenuItem.Size = new System.Drawing.Size(205, 22);
			this.deleteToolStripMenuItem.Text = "Delete";
			this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
			// 
			// ThumbnailControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.pictureBox1);
			this.Name = "ThumbnailControl";
			this.Size = new System.Drawing.Size(110, 110);
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.contextMenuStrip1.ResumeLayout(false);
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
		private System.Windows.Forms.ToolStripMenuItem rotateToRightToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem rotateToLeftToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
    }
}
