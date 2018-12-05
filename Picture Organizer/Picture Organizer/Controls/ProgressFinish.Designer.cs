namespace Trebuchet.Controls
{
    partial class ProgressFinish
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProgressFinish));
			this.btnOrganize = new System.Windows.Forms.Button();
			this.chkAlwaysLoadThumbs = new System.Windows.Forms.CheckBox();
			this.btnLoadThumbnails = new System.Windows.Forms.Button();
			this.progressControl1 = new Trebuchet.Controls.ProgressControl();
			this.thumbnailViewer1 = new Trebuchet.Controls.ThumbnailViewer();
			this.SuspendLayout();
			// 
			// btnOrganize
			// 
			this.btnOrganize.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.btnOrganize.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnOrganize.Image = ((System.Drawing.Image)(resources.GetObject("btnOrganize.Image")));
			this.btnOrganize.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btnOrganize.Location = new System.Drawing.Point(318, 0);
			this.btnOrganize.Name = "btnOrganize";
			this.btnOrganize.Size = new System.Drawing.Size(102, 26);
			this.btnOrganize.TabIndex = 0;
			this.btnOrganize.Text = "Organize";
			this.btnOrganize.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.btnOrganize.UseVisualStyleBackColor = true;
			this.btnOrganize.Click += new System.EventHandler(this.btnOrganize_Click);
			// 
			// chkAlwaysLoadThumbs
			// 
			this.chkAlwaysLoadThumbs.AutoSize = true;
			this.chkAlwaysLoadThumbs.BackColor = System.Drawing.Color.Transparent;
			this.chkAlwaysLoadThumbs.Location = new System.Drawing.Point(117, 7);
			this.chkAlwaysLoadThumbs.Name = "chkAlwaysLoadThumbs";
			this.chkAlwaysLoadThumbs.Size = new System.Drawing.Size(138, 17);
			this.chkAlwaysLoadThumbs.TabIndex = 3;
			this.chkAlwaysLoadThumbs.Text = "Always load thumbnails ";
			this.chkAlwaysLoadThumbs.UseVisualStyleBackColor = false;
			this.chkAlwaysLoadThumbs.CheckedChanged += new System.EventHandler(this.chkAlwaysLoadThumbs_CheckedChanged);
			// 
			// btnLoadThumbnails
			// 
			this.btnLoadThumbnails.Location = new System.Drawing.Point(8, 3);
			this.btnLoadThumbnails.Name = "btnLoadThumbnails";
			this.btnLoadThumbnails.Size = new System.Drawing.Size(103, 23);
			this.btnLoadThumbnails.TabIndex = 4;
			this.btnLoadThumbnails.Text = "Load Thumbnails";
			this.btnLoadThumbnails.UseVisualStyleBackColor = true;
			this.btnLoadThumbnails.Click += new System.EventHandler(this.btnLoadThumbnails_Click);
			// 
			// progressControl1
			// 
			this.progressControl1.BackColor = System.Drawing.Color.Transparent;
			this.progressControl1.Details = resources.GetString("progressControl1.Details");
			this.progressControl1.Location = new System.Drawing.Point(0, 40);
			this.progressControl1.Name = "progressControl1";
			this.progressControl1.ProgressTitle = "Progress";
			this.progressControl1.Size = new System.Drawing.Size(302, 84);
			this.progressControl1.TabIndex = 1;
			this.progressControl1.Visible = false;
			// 
			// thumbnailViewer1
			// 
			this.thumbnailViewer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.thumbnailViewer1.BackColor = System.Drawing.Color.White;
			this.thumbnailViewer1.Location = new System.Drawing.Point(0, 30);
			this.thumbnailViewer1.Name = "thumbnailViewer1";
			this.thumbnailViewer1.Size = new System.Drawing.Size(541, 456);
			this.thumbnailViewer1.TabIndex = 5;
			this.thumbnailViewer1.Visible = false;
			// 
			// ProgressFinish
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.Transparent;
			this.Controls.Add(this.btnLoadThumbnails);
			this.Controls.Add(this.chkAlwaysLoadThumbs);
			this.Controls.Add(this.btnOrganize);
			this.Controls.Add(this.progressControl1);
			this.Controls.Add(this.thumbnailViewer1);
			this.Name = "ProgressFinish";
			this.Size = new System.Drawing.Size(541, 486);
			this.SizeChanged += new System.EventHandler(this.OnSizeChanged);
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOrganize;
        private Trebuchet.Controls.ProgressControl progressControl1;
        private System.Windows.Forms.CheckBox chkAlwaysLoadThumbs;
        private System.Windows.Forms.Button btnLoadThumbnails;
        private ThumbnailViewer thumbnailViewer1;
    }
}
