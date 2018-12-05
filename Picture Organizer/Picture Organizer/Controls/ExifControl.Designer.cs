namespace Trebuchet.Controls
{
	partial class ExifControl
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
			this.comboMode = new System.Windows.Forms.ComboBox();
			this.dataGridExif = new System.Windows.Forms.DataGridView();
			this.columnCopy = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			this.columnProperty = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.columnData = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.panel1 = new System.Windows.Forms.Panel();
			this.lblMode = new System.Windows.Forms.Label();
			this.dataSet1 = new System.Data.DataSet();
			((System.ComponentModel.ISupportInitialize)(this.dataGridExif)).BeginInit();
			this.panel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dataSet1)).BeginInit();
			this.SuspendLayout();
			// 
			// comboMode
			// 
			this.comboMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboMode.FormattingEnabled = true;
			this.comboMode.Items.AddRange(new object[] {
            "Copy all",
            "Copy none"});
			this.comboMode.Location = new System.Drawing.Point(43, 3);
			this.comboMode.Name = "comboMode";
			this.comboMode.Size = new System.Drawing.Size(121, 21);
			this.comboMode.TabIndex = 0;
			this.comboMode.SelectedIndexChanged += new System.EventHandler(this.comboMode_SelectedIndexChanged);
			// 
			// dataGridExif
			// 
			this.dataGridExif.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridExif.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.columnCopy,
            this.columnProperty,
            this.columnData});
			this.dataGridExif.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dataGridExif.Location = new System.Drawing.Point(0, 27);
			this.dataGridExif.Name = "dataGridExif";
			this.dataGridExif.RowHeadersVisible = false;
			this.dataGridExif.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dataGridExif.Size = new System.Drawing.Size(400, 306);
			this.dataGridExif.TabIndex = 1;
			// 
			// columnCopy
			// 
			this.columnCopy.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
			this.columnCopy.HeaderText = "Copy";
			this.columnCopy.Name = "columnCopy";
			this.columnCopy.Width = 37;
			// 
			// columnProperty
			// 
			this.columnProperty.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
			this.columnProperty.HeaderText = "Property";
			this.columnProperty.Name = "columnProperty";
			this.columnProperty.ReadOnly = true;
			this.columnProperty.Width = 71;
			// 
			// columnData
			// 
			this.columnData.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
			this.columnData.HeaderText = "Data";
			this.columnData.Name = "columnData";
			this.columnData.Width = 55;
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.lblMode);
			this.panel1.Controls.Add(this.comboMode);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(400, 27);
			this.panel1.TabIndex = 2;
			// 
			// lblMode
			// 
			this.lblMode.AutoSize = true;
			this.lblMode.Location = new System.Drawing.Point(3, 6);
			this.lblMode.Name = "lblMode";
			this.lblMode.Size = new System.Drawing.Size(34, 13);
			this.lblMode.TabIndex = 1;
			this.lblMode.Text = "Mode";
			// 
			// dataSet1
			// 
			this.dataSet1.DataSetName = "NewDataSet";
			// 
			// ExifControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.dataGridExif);
			this.Controls.Add(this.panel1);
			this.Name = "ExifControl";
			this.Size = new System.Drawing.Size(400, 333);
			((System.ComponentModel.ISupportInitialize)(this.dataGridExif)).EndInit();
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.dataSet1)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ComboBox comboMode;
		private System.Windows.Forms.DataGridView dataGridExif;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Label lblMode;
		private System.Windows.Forms.DataGridViewCheckBoxColumn columnCopy;
		private System.Windows.Forms.DataGridViewTextBoxColumn columnProperty;
		private System.Windows.Forms.DataGridViewTextBoxColumn columnData;
		private System.Data.DataSet dataSet1;
	}
}
