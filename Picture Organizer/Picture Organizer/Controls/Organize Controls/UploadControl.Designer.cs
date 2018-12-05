namespace Trebuchet.Controls
{
    partial class UploadControl
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UploadControl));
			this.btnAddProvider = new System.Windows.Forms.Button();
			this.comboProviders = new System.Windows.Forms.ComboBox();
			this.panelProviderControl = new System.Windows.Forms.Panel();
			this.panelChooseProvider = new System.Windows.Forms.Panel();
			this.btnDeleteProvider = new System.Windows.Forms.Button();
			this.toolTipAddProvider = new System.Windows.Forms.ToolTip(this.components);
			this.toolTipDeleteProvider = new System.Windows.Forms.ToolTip(this.components);
			this.panelChooseProvider.SuspendLayout();
			this.SuspendLayout();
			// 
			// btnAddProvider
			// 
			this.btnAddProvider.Image = ((System.Drawing.Image)(resources.GetObject("btnAddProvider.Image")));
			this.btnAddProvider.Location = new System.Drawing.Point(166, 4);
			this.btnAddProvider.Name = "btnAddProvider";
			this.btnAddProvider.Size = new System.Drawing.Size(63, 23);
			this.btnAddProvider.TabIndex = 3;
			this.btnAddProvider.UseVisualStyleBackColor = true;
			this.btnAddProvider.Click += new System.EventHandler(this.btnAddProvider_Click);
			// 
			// comboProviders
			// 
			this.comboProviders.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboProviders.FormattingEnabled = true;
			this.comboProviders.Location = new System.Drawing.Point(3, 5);
			this.comboProviders.Name = "comboProviders";
			this.comboProviders.Size = new System.Drawing.Size(121, 21);
			this.comboProviders.TabIndex = 2;
			this.comboProviders.SelectedIndexChanged += new System.EventHandler(this.comboProviders_SelectedIndexChanged);
			// 
			// panelProviderControl
			// 
			this.panelProviderControl.AutoScroll = true;
			this.panelProviderControl.Location = new System.Drawing.Point(0, 31);
			this.panelProviderControl.Name = "panelProviderControl";
			this.panelProviderControl.Size = new System.Drawing.Size(371, 319);
			this.panelProviderControl.TabIndex = 4;
			// 
			// panelChooseProvider
			// 
			this.panelChooseProvider.Controls.Add(this.btnDeleteProvider);
			this.panelChooseProvider.Controls.Add(this.comboProviders);
			this.panelChooseProvider.Controls.Add(this.btnAddProvider);
			this.panelChooseProvider.Dock = System.Windows.Forms.DockStyle.Top;
			this.panelChooseProvider.Location = new System.Drawing.Point(0, 0);
			this.panelChooseProvider.Name = "panelChooseProvider";
			this.panelChooseProvider.Size = new System.Drawing.Size(371, 31);
			this.panelChooseProvider.TabIndex = 0;
			// 
			// btnDeleteProvider
			// 
			this.btnDeleteProvider.Image = ((System.Drawing.Image)(resources.GetObject("btnDeleteProvider.Image")));
			this.btnDeleteProvider.Location = new System.Drawing.Point(130, 4);
			this.btnDeleteProvider.Name = "btnDeleteProvider";
			this.btnDeleteProvider.Size = new System.Drawing.Size(30, 23);
			this.btnDeleteProvider.TabIndex = 4;
			this.btnDeleteProvider.UseVisualStyleBackColor = true;
			this.btnDeleteProvider.Click += new System.EventHandler(this.btnDeleteProvider_Click);
			// 
			// toolTipAddProvider
			// 
			this.toolTipAddProvider.ToolTipTitle = "Add Provider";
			// 
			// toolTipDeleteProvider
			// 
			this.toolTipDeleteProvider.ToolTipTitle = "Delete Provider";
			// 
			// UploadControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.panelProviderControl);
			this.Controls.Add(this.panelChooseProvider);
			this.MinimumSize = new System.Drawing.Size(371, 350);
			this.Name = "UploadControl";
			this.Size = new System.Drawing.Size(371, 350);
			this.panelChooseProvider.ResumeLayout(false);
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnAddProvider;
        private System.Windows.Forms.ComboBox comboProviders;
        private System.Windows.Forms.Panel panelProviderControl;
        private System.Windows.Forms.Panel panelChooseProvider;
		private System.Windows.Forms.Button btnDeleteProvider;
		private System.Windows.Forms.ToolTip toolTipAddProvider;
		private System.Windows.Forms.ToolTip toolTipDeleteProvider;
    }
}
