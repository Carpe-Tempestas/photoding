/*****************************************************************************
  Copyright © 2004 - 2005 by Martin Cook. All rights are reserved. If you like
  this code then feel free to go ahead and use it. The only thing I ask is 
  that you don't remove or alter my copyright notice. Your use of this 
  software is entirely at your own risk. I make no claims about the 
  reliability or fitness of this code for any particular purpose. If you 
  make changes or additions to this code then please clearly mark your code 
  as yours. If you have questions or comments then please contact me at: 
  martin@codegator.com
  
  Have Fun! :o)
*****************************************************************************/

#region Using directives

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Collections;
using System.Runtime.InteropServices;

using CG.Core.Resource;

#endregion

namespace CG.FontCombo
{
	
	/// <summary>
	/// A control that produces a selectable list of fonts.
	/// </summary>
	[ToolboxBitmap(typeof(GCFontCombo), "CGFontCombo.bmp")]
	public class GCFontCombo : Control
	{

		// ******************************************************************
		// Events.
		// ******************************************************************

		#region Events

		/// <summary>
		/// Occurs whenever the 'SelectedIndex' property for this control changes.
		/// </summary>
		[CGDescription(typeof(GCFontCombo), "selected_index_changed")]
		[CGCategory(typeof(GCFontCombo), "behavior")]
		public event EventHandler SelectedIndexChanged;

		#endregion

		// ******************************************************************
		// Attributes.
		// ******************************************************************

		#region Attributes

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		/// <summary>
		/// The actual combobox.
		/// </summary>
		private ComboBox m_comboBox;

		#endregion

		// ******************************************************************
		// Properties.
		// ******************************************************************

		#region Properties

		/// <summary>
		/// Gets and sets the background color.
		/// </summary>
		[DefaultValue("Window")]
		public override Color BackColor
		{

			get {return m_comboBox.BackColor;}
			
			set
			{

				// If nothing has changed then simply exit.
				if (m_comboBox.BackColor == value)
					return;

				// Save the value.
				m_comboBox.BackColor = value;

			} // End set

		} // End BackColor

		// ******************************************************************

		/// <summary>
		/// Gets and sets the foreground color.
		/// </summary>
		[DefaultValue("WindowText")]
		public override Color ForeColor
		{

			get {return m_comboBox.ForeColor;}
			
			set
			{

				// If nothing has changed then simply exit.
				if (m_comboBox.ForeColor == value)
					return;

				// Save the value.
				m_comboBox.ForeColor = value;

			} // End set

		} // End ForeColor

		// ******************************************************************

		/// <summary>
		/// Gets and sets the selected font family.
		/// </summary>
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public FontFamily SelectedFontFamily
		{

			get
			{

				// Sanity check the combobox before attempting to use it.
				if (m_comboBox == null)
					return null;

				// Get the index of the currently selected item.
				int index = m_comboBox.SelectedIndex;

				// Sanity check the index before attempting to use it.
				if (index == -1)
					return null;

				// Return the corresponding font family.
				return ((Font)m_comboBox.SelectedItem).FontFamily;

			} // End get

			set
			{

				// Sanity check the combobox before attempting to use it.
				if (m_comboBox == null)
					return;

				int index = -1;

				// Should we look for a matching item?
				if (value != null)
					index = m_comboBox.FindString(value.Name, 0);

				// Select the item.
				m_comboBox.SelectedIndex = index;
				
			} // End set

		} // End SelectedFontFamily

		// ******************************************************************

		/// <summary>
		/// Gets and sets the index of the currently selected item.
		/// </summary>
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public int SelectedIndex
		{
			get {return m_comboBox.SelectedIndex;}
			set {m_comboBox.SelectedIndex = value;}
		} // End SelectedIndex

		#endregion

		// ******************************************************************
		// Constructors.
		// ******************************************************************

		#region Constructors

		/// <summary>
		/// Creates a new instance of the CGFontCombo class.
		/// </summary>
		public GCFontCombo()
		{
		
			// Required by the designer.
			InitializeComponent();

			// Create the combobox.
			m_comboBox = new ComboBox();
			m_comboBox.Dock = DockStyle.Fill;
			m_comboBox.DrawMode = DrawMode.OwnerDrawFixed;
			m_comboBox.BackColor = SystemColors.Window;
			m_comboBox.ForeColor = SystemColors.WindowText;
			m_comboBox.DisplayMember = "Name";
			m_comboBox.DrawItem += new DrawItemEventHandler(
				m_comboBox_DrawItem
            	);
			m_comboBox.SelectedIndexChanged += new EventHandler(
				m_comboBox_SelectedIndexChanged
				);
			m_comboBox.FontChanged += new EventHandler(
				m_comboBox_FontChanged
				);
			Controls.Add(m_comboBox);
            
			// Fixup the styles.
			SetStyle(ControlStyles.StandardClick, false);
			SetStyle(ControlStyles.Selectable, false);
			SetStyle(ControlStyles.UserPaint, false);
			SetStyle(ControlStyles.ResizeRedraw, true);
			SetStyle(ControlStyles.SupportsTransparentBackColor, false);

		} // End CGFontCombo()

		#endregion

		// ******************************************************************
		// Overrides.
		// ******************************************************************

		#region Overrides

		/// <summary>
		/// Raises the System.Windows.Forms.Control.HandleCreated event.  
		/// </summary>
		/// <param name="e">An System.EventArgs that contains the event data.</param>
		protected override void OnHandleCreated(EventArgs e)
		{
			
			base.OnHandleCreated(e);

			// Populate the combobox.
			CreateSampleFonts();
                				
		} // End OnHandleCreated()

		// ******************************************************************

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose(bool disposing)
		{

			// Should we dispose of the container?
			if (disposing && components != null)
				components.Dispose();

			base.Dispose(disposing);

		} // End Dispose()

		#endregion

		// ******************************************************************
		// Component Designer generated code.
		// ******************************************************************

		#region Component Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{

		}
		#endregion

		// ******************************************************************
		// Combobox event handlers.
		// ******************************************************************

		#region Combobox event handlers

		/// <summary>
		/// Called whenever the combobox needs to draw an item.
		/// </summary>
		/// <param name="sender">The event sender.</param>
		/// <param name="e">The event arguments.</param>
		private void m_comboBox_DrawItem(object sender, DrawItemEventArgs e)
		{

			// If the index is invalid then simply exit.
			if (e.Index == -1 || e.Index >= m_comboBox.Items.Count)
				return;

			// Draw the background of the item.
			e.DrawBackground();

			// Should we draw the focus rectangle?
			if ((e.State & DrawItemState.Focus) != 0)
				e.DrawFocusRectangle();
			
			Brush b = null;
			
			try
			{

				// Create a new background brush.
				b = new SolidBrush(e.ForeColor);

				// Draw the item.
				e.Graphics.DrawString(
					((Font)m_comboBox.Items[e.Index]).Name, 
					((Font)m_comboBox.Items[e.Index]),
					b,
					e.Bounds
					);

			} // End try

			finally
			{

                // Should we cleanup the brush?
				if (b != null)
					b.Dispose();    

				b = null;

			} // End finally
		
		} // End m_comboBox_DrawItem()

		// ******************************************************************

		/// <summary>
		/// Called whenever the selected item in the combobox is changed.
		/// </summary>
		/// <param name="sender">The event sender.</param>
		/// <param name="e">The event arguments.</param>
		private void m_comboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			OnSelectedIndexChanged();
		} // End m_comboBox_SelectedIndexChanged()

		// ******************************************************************

		/// <summary>
		/// Called whenever the combobox font is changed.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void m_comboBox_FontChanged(object sender, EventArgs e)
		{
			CreateSampleFonts();
		} // End m_comboBox_FontChanged()

		#endregion

		// ******************************************************************
		// Protected.
		// ******************************************************************

		#region Protected methods

		/// <summary>
		/// Raises the SelectedIndexChanged event.
		/// </summary>
		protected virtual void OnSelectedIndexChanged()
		{

			// Should we fire the event?
			if (SelectedIndexChanged != null)
				SelectedIndexChanged(this, EventArgs.Empty);

		} // End OnSelectedIndexChanged()

		#endregion

		// ******************************************************************
		// Private methods.
		// ******************************************************************

		#region Private methods.

		/// <summary>
		/// Creates the array of sample fonts.
		/// </summary>
		private void CreateSampleFonts()
		{

			// Should we simply exit?
			if (!IsHandleCreated || DesignMode)
				return;

			// Should we destroy any existing sample fonts?
			if (m_comboBox.Items.Count > 0)
			{

				// Loop and cleanup the fonts.
				for (int x = 0; x < m_comboBox.Items.Count; x++)
					((Font)m_comboBox.Items[x]).Dispose();

				m_comboBox.Items.Clear();

			} // End if we should cleanup the sample fonts.

			// Gets the list of installed fonts.
			FontFamily[] ff = FontFamily.Families;

			// Loop and create a sample of each font.
			for (int x = 0; x < ff.Length; x++)
			{

				System.Drawing.Font font = null;

				// Create the font - based on the styles available.
				if (ff[x].IsStyleAvailable(FontStyle.Regular))
					font = new System.Drawing.Font(
						ff[x].Name, 
						m_comboBox.Font.Size
						);
				else if (ff[x].IsStyleAvailable(FontStyle.Bold))
					font = new System.Drawing.Font(
						ff[x].Name, 
						m_comboBox.Font.Size,
						FontStyle.Bold
						);
				else if (ff[x].IsStyleAvailable(FontStyle.Italic))
					font = new System.Drawing.Font(
						ff[x].Name, 
						m_comboBox.Font.Size,
						FontStyle.Italic
						);
				else if (ff[x].IsStyleAvailable(FontStyle.Strikeout))
					font = new System.Drawing.Font(
						ff[x].Name, 
						m_comboBox.Font.Size,
						FontStyle.Strikeout
						);
				else if (ff[x].IsStyleAvailable(FontStyle.Underline))
					font = new System.Drawing.Font(
						ff[x].Name, 
						m_comboBox.Font.Size,
						FontStyle.Underline
						);

				// Should we add the item?
				if (font != null)
					m_comboBox.Items.Add(font);

			} // End for all the fonts.

		} // End CreateSampleFonts()

		#endregion

	} // End class CGFontCombo

} // End namespace CG.FontCombo
