using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Trebuchet.Settings;
using System.Threading;
using System.Xml.Serialization;

namespace Trebuchet.Controls
{
    public partial class FolderSelectControl : UserControl, IPikControl
    {
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        public event EventHandler useDefaultChanged;
        public event EventHandler enabling;
        public event EventHandler directionsChanging;
		public event EventHandler sourceChanged;
        private bool initializing = false;
		private string oldAlbum;
		private bool updating = false;
		private bool updatingSelection = false;
		private bool subscribedToRename = false;
		private string oldName;

        public FolderSelectControl()
        {
            InitializeComponent();
            
        }

        public string Source
        {
            get { return this.txtSource.Text; }
            set { this.txtSource.Text = value; }
        }

        public string Destination
        {
            get { return this.txtDestination.Text; }
            set { this.txtDestination.Text = value; }
        }

        public bool UseEnabled
        {
            get
            {
                return false;
            }
        }

        public bool ControlEnabled
        {
            get
            {
                return true;
            }
            set
            {
                //Do nothing
            }
        }

        public string Directions
        {
            get
            {
                if (this.chkUseDefault.Checked)
                    return this.BasicDirections;
                else
                    return this.AdvancedDirections;
            }
        }

        private string AdvancedDirections
        {
            get
            {
                string directions;
                directions = "Directions:" + Environment.NewLine;
                directions = directions + "The source directory is the folder that contains the" +
                    " photos you would like to organize.  The destination directory is the folder" +
                    " where you would like your organized photos to go once photoding is finished." +
                    "  The action describes whether the original photos will be moved from the source " +
                    "directory to the destination, be copied to the destination directory, OR " +
                    "do nothing - which would place the organized photos in the destination directory." +
                    Environment.NewLine + Environment.NewLine + "Check the \"Use Default Destination\"" +
                    " box to go back to using the default destination.";

                return directions;
            }
        }

        private string BasicDirections
        {
            get
            {
                string directions;
                directions = "Directions:" + Environment.NewLine;
                directions = directions + "The source directory is the folder that contains the" +
                    " photos you would like to organize.  The default destination is an \"organized\"" +
                    " directory below the source directory." + Environment.NewLine +
                    Environment.NewLine + "Uncheck the \"Use Default Directory\" box to specify " +
                    "a different destination";
                
                return directions;
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!this.DesignMode)
            {
				App.SettingLoaded += new EventHandler(App_SettingLoaded);
				if (App.SettingsList.Count >= 1)
					InitializeSettingsListView(this.listSettings);
				App.CurAppSettingChanging += new EventHandler(App_CurAppSettingChanging);
				App.CurAppSettingChanged += new EventHandler(App_CurAppSettingChanged);
            }
        }

		void App_SettingLoaded(object sender, EventArgs e)
		{
			if (this.InvokeRequired)
			{
				this.BeginInvoke((MethodInvoker)delegate { App_SettingLoaded(this, e); });
			}
			else
			{
				InitializeSettingsListView(this.listSettings);
			}
		}

		void App_CurAppSettingChanged(object sender, EventArgs e)
		{
			Initialize();
			App.CurAppSettings.FolderSelectChanged += new EventHandler(CurAppSettings_FolderSelectChanged);
			App.CurAppSettings.RenameChanged += new EventHandler(CurAppSettings_RenameChanged);
			App.CurAppSettings.ResizeChanged += new EventHandler(CurAppSettings_ResizeChanged);
			App.CurAppSettings.WatermarkChanged += new EventHandler(CurAppSettings_WatermarkChanged);
			App.CurAppSettings.ImageSettingsChanged += new EventHandler(CurAppSettings_ImageSettingsChanged);
		}

		void App_CurAppSettingChanging(object sender, EventArgs e)
		{
			App.CurAppSettings.FolderSelectChanged -= new EventHandler(CurAppSettings_FolderSelectChanged);
			App.CurAppSettings.RenameChanged -= new EventHandler(CurAppSettings_RenameChanged);
			App.CurAppSettings.ResizeChanged -= new EventHandler(CurAppSettings_ResizeChanged);
			App.CurAppSettings.WatermarkChanged -= new EventHandler(CurAppSettings_WatermarkChanged);
			App.CurAppSettings.ImageSettingsChanged -= new EventHandler(CurAppSettings_ImageSettingsChanged);
		}

		void CurAppSettings_FolderSelectChanged(object sender, EventArgs e)
		{
			TriggerSaveNeeded();
		}

		void CurAppSettings_RenameChanged(object sender, EventArgs e)
		{
			TriggerSaveNeeded();
		}

		void CurAppSettings_ResizeChanged(object sender, EventArgs e)
		{
			TriggerSaveNeeded();
		}

		void CurAppSettings_WatermarkChanged(object sender, EventArgs e)
		{
			TriggerSaveNeeded();
		}

		void CurAppSettings_ImageSettingsChanged(object sender, EventArgs e)
		{
			TriggerSaveNeeded();
		}

		private void TriggerSaveNeeded()
		{
			if (this.listSettings.SelectedItems.Count > 0)
			{
				this.listSettings.SelectedItems[0].ForeColor = Color.Red;
				this.btnSave.Enabled = true;
				this.btnSaveAll.Enabled = true;
				Application.DoEvents();
			}
		}

		void bw_DoWork(object sender, DoWorkEventArgs e)
		{
			if (this.InvokeRequired)
			{
				while (App.SettingsList.Count == 0)
					Thread.Sleep(200);
				this.BeginInvoke((MethodInvoker)delegate { bw_DoWork(this, e); });
			}
			else
			{
				InitializeSettingsListView(this.listSettings);
			}
		}

        public void Initialize()
        {
            this.initializing = true;
            this.txtSource.Text = App.AppSettings.FolderSelectSettings.SourceFolder;
            this.chkUseDefault.Checked = App.AppSettings.FolderSelectSettings.UseDefaultDestination;
            this.txtDestination.Text = App.AppSettings.FolderSelectSettings.DestinationFolder;
			this.txtAlbum.Text = App.AppSettings.FolderSelectSettings.Album;
			this.chkAppendAlbum.Checked = App.AppSettings.FolderSelectSettings.AppendAlbum;
			this.comboAction.SelectedIndex = App.AppSettings.FolderSelectSettings.FolderAction;
			if ((this.chkUseDefault.Checked && this.txtDestination.Visible) || (!this.chkUseDefault.Checked && !this.txtDestination.Visible))
			{
				this.chkUseDefault.Checked = !this.chkUseDefault.Checked;
				this.chkUseDefault.Checked = !this.chkUseDefault.Checked;
			}
            this.initializing = false;            
        }

        private void btnSourceFolderSelect_Click(object sender, EventArgs e)
        {
            try
            {
                if (Directory.Exists(this.txtSource.Text))
                    this.folderBrowserDialog1.SelectedPath = this.txtSource.Text;
                this.folderBrowserDialog1.ShowDialog();
                this.txtSource.Text = this.folderBrowserDialog1.SelectedPath;
            }
            catch(Exception error)
            {
                System.Diagnostics.Debug.WriteLine(error.Message);
            }
        }

        private void btnDestinationFolderSelect_Click(object sender, EventArgs e)
        {
            this.folderBrowserDialog1.SelectedPath = RemoveAppendedAlbum();
            this.folderBrowserDialog1.ShowDialog();
            this.txtDestination.Text = this.folderBrowserDialog1.SelectedPath;
            UpdateCurAppSettings();
        }

		private string GetAppropriatePath(string path)
		{
			if (!String.IsNullOrEmpty(path) && path[path.Length - 1] == '\\')
			{
				path = path.Remove(path.Length - 1);
			}
			return path;
		}

		private void UpdateLocationText()
		{
			if (this.chkAppendAlbum.Checked)
			{
				if (String.IsNullOrEmpty(this.txtAlbum.Text))
					this.txtDestination.Text = RemoveAppendedAlbum();
				else if (IsAlbumAppended())
					this.txtDestination.Text = GetAppropriatePath(RemoveAppendedAlbum()) + "\\" + this.txtAlbum.Text;
				else
					this.txtDestination.Text = GetAppropriatePath(this.txtDestination.Text) + "\\" + this.txtAlbum.Text;
			}
			else if (IsAlbumAppended())
			{
				this.txtDestination.Text = RemoveAppendedAlbum();
			}
		}

		private string RemoveAppendedAlbum()
		{
			if (String.IsNullOrEmpty(this.oldAlbum))
				return this.txtDestination.Text;

			string locationText = this.txtDestination.Text;
			string albumText = String.Empty;
			if (!String.IsNullOrEmpty(this.oldAlbum))
				albumText = "\\" + this.oldAlbum;

			if (!locationText.Contains(albumText))
				return locationText;

			if (albumText.Length > locationText.Length)
				return "";
			string ret = locationText.Remove(locationText.Length - albumText.Length, albumText.Length);
			return ret;
		}

		private bool IsAlbumAppended()
		{
			string albumText = "\\" + this.oldAlbum;
			albumText = albumText.ToLower();
			string location = this.txtDestination.Text;
			location = location.ToLower();
			int count = albumText.Length - 1;
			int search = location.Length - 1;
			bool ret = false;

			if (search < 0)
				return false;

			for (int x = count; x >= 0; x--)
			{
				if (location[search--] != albumText[count--])
				{
					ret = false;
					break;
				}
				else if (count == 0)
				{
					ret = true;
				}
			}
			return ret;
		}

        private void chkUseDefault_CheckedChanged(object sender, EventArgs e)
        {
            Rectangle checkbox = this.chkUseDefault.Bounds;
            Rectangle move = Rectangle.Empty;

            if (this.chkUseDefault.Checked)
            {
                move = this.txtDestination.Bounds;
                this.txtDestination.Visible = false;
                this.lblDestination.Visible = false;
                this.lblAction.Visible = false;
                this.comboAction.Visible = false;
                this.btnDestinationFolderSelect.Visible = false;
            }
            else
            {
                move = this.lblAction.Bounds;
                this.txtDestination.Visible = true;
                this.lblDestination.Visible = true;
                this.lblAction.Visible = true;
                this.comboAction.Visible = true;
                this.btnDestinationFolderSelect.Visible = true;

				if (String.IsNullOrEmpty(RemoveAppendedAlbum()))
				{
					this.txtDestination.Text = this.txtSource.Text;
					UpdateLocationText();
				}
            }

            checkbox.Y = move.Y;
            this.chkUseDefault.Bounds = checkbox;

            if (this.useDefaultChanged != null)
                this.useDefaultChanged.Invoke(this, new EventArgs());
            if (this.directionsChanging != null)
                this.directionsChanging.Invoke(this, new EventArgs());
            UpdateCurAppSettings();
        }

        private void comboAction_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateCurAppSettings(); 
        }

        public void UpdateCurAppSettings()
        {
            if (!this.initializing)
            {
                App.CurAppSettings.FolderSelectSettings.SourceFolder = this.txtSource.Text;
                App.CurAppSettings.FolderSelectSettings.DestinationFolder = this.txtDestination.Text;
                App.CurAppSettings.FolderSelectSettings.UseDefaultDestination = this.chkUseDefault.Checked;
				App.CurAppSettings.FolderSelectSettings.Album = this.txtAlbum.Text;
				App.CurAppSettings.FolderSelectSettings.AppendAlbum = this.chkAppendAlbum.Checked;
				App.CurAppSettings.FolderSelectSettings.FolderAction = this.comboAction.SelectedIndex;
				App.CurAppSettings.FireFolderSelect();
            }
        }

        public bool CanContinue()
        {
            if (!String.IsNullOrEmpty(this.txtSource.Text) && (this.chkUseDefault.Checked ||
                (!String.IsNullOrEmpty(this.txtDestination.Text) && this.comboAction.SelectedIndex >= 0)))
            {
                return true;
            }
            else
                return false;
        }

        private void txtSource_TextChanged(object sender, EventArgs e)
        {
            UpdateCurAppSettings();
        }

        private void txtDestination_TextChanged(object sender, EventArgs e)
        {
			if (!this.updating && this.chkAppendAlbum.Checked && !IsAlbumAppended())
			{
				this.updating = true;
				UpdateLocationText();
				this.updating = false;
			}

            UpdateCurAppSettings();
        }

        private void txtAlbum_TextChanged(object sender, EventArgs e)
        {
			this.updating = true;
			UpdateLocationText();
			this.oldAlbum = this.txtAlbum.Text;
            UpdateCurAppSettings();
			this.updating = false;
        }

		private void chkAppendAlbum_CheckedChanged(object sender, EventArgs e)
		{
			UpdateLocationText();
			UpdateCurAppSettings();
		}

		private void InitializeSettingsListView(ListView view)
		{
			ListViewItem item = null;
			view.Items.Clear();
			string selectedItem = "Default";
			for (int x = 0; x < App.SettingsList.Count;x++ )
			{
				if (App.SettingsList[x].Name != "Default" && App.SettingsList[x].Name != "settings")
				{
					if (App.SettingsList[x].Name == String.Empty)
						throw new Exception();

					item = new ListViewItem(App.SettingsList[x].ToString());
					item.ToolTipText = App.SettingsList[x].Name;
					item.Tag = App.SettingsList[x];
					view.Items.Add(item);
					if (App.AppSettings.SettingsLoaded == App.SettingsList[x].Name)
					{
						item.Selected = true;
						Initialize();
					}
				}
				else
				{
					selectedItem = App.SettingsList[x].SettingsLoaded;
					if (App.AppSettings.SettingsLoaded == App.SettingsList[x].Name)
					{
						Initialize();
					}
				}

			}

			if (this.listSettings.SelectedItems.Count == 0)
			{
				foreach (ListViewItem temp in this.listSettings.Items)
				{
					if (temp.Text == selectedItem)
						temp.Selected = true;
				}
			}
		}

		private void listSettings_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.updatingSelection = true;
			if (this.listSettings.SelectedItems.Count > 0)
			{
				SettingsFile1 file = this.listSettings.SelectedItems[0].Tag as SettingsFile1;
				this.updating = true;
				this.txtRenameSetting.Text = this.listSettings.SelectedItems[0].Text;
				this.txtSettingDescription.Text = file.Description;
				this.updating = false;
				if (this.txtRenameSetting.Text == "Default")
				{
					this.btnCopySetting.Enabled = true;
					this.txtRenameSetting.Enabled = false;
					this.txtSettingDescription.Enabled = false;
					this.btnRemoveSetting.Enabled = false;
					App.AppSettings.SettingsLoaded = "settings";
					App.AppSettings.LoadSettings(this.listSettings.SelectedItems[0].Tag as SettingsFile1);
					App.TheApp.SaveSettings("settings");
					App.SetToDefaultIndex();
				}
				else
				{
					this.btnCopySetting.Enabled = true;
					this.txtRenameSetting.Enabled = true;
					this.btnRemoveSetting.Enabled = true;
					this.txtSettingDescription.Enabled = true;
					App.AppSettings.SettingsLoaded = file.Name;
					App.AppSettings.LoadSettings(App.SettingsList.IndexOf(file));
					App.TheApp.SaveSettings("settings");
					App.CurAppSettingsIndex = App.SettingsList.IndexOf(file);
				}
				if (this.listSettings.SelectedItems[0].ForeColor == Color.Red)
				{
					this.btnSave.Enabled = true;
					this.btnSaveAll.Enabled = true;
				}
				else
				{
					this.btnSave.Enabled = false;
					foreach (ListViewItem item in this.listSettings.Items)
					{
						if (item.ForeColor == Color.Red)
						{
							this.btnSaveAll.Enabled = true;
							break;
						}
					}
				}
			}
			else
			{
				App.SetToDefaultIndex();
				this.btnSave.Enabled = false;
				this.txtRenameSetting.Enabled = false;
				this.txtRenameSetting.Text = String.Empty;
				this.btnRemoveSetting.Enabled = false;
				this.btnCopySetting.Enabled = false;
				this.txtSettingDescription.Enabled = false;
				this.txtSettingDescription.Text = String.Empty;
			}
			this.updatingSelection = false;
		}

		private void btnAddSetting_Click(object sender, EventArgs e)
		{
			SettingsFile1 file = new SettingsFile1();
			file.LoadAllDefaults();
			file.Description = "Edit the description of your new dingSet here.";
			string fileName = "myDingSet";
			CreateNewSetting(file, fileName);
		}

		private void CreateNewSetting(SettingsFile1 file, string fileName)
		{
			bool nameFound = true;
			int count = 1;
			while (nameFound)
			{
				file.Name = fileName + count.ToString();
				foreach (SettingsFile1 check in App.SettingsList)
				{
					if (check.Name == file.Name)
						count++;
				}
				if(fileName+count.ToString() == file.Name)
					nameFound = false;
			}
			App.SettingsList.Add(file);
			App.CurAppSettingsIndex = App.SettingsList.Count - 1;
			App.CurAppSettings.SettingsLoaded = file.Name;
			ListViewItem item = new ListViewItem(file.Name);
			item.ForeColor = Color.Red;
			item.ToolTipText = file.Name;
			item.Tag = file;
			this.listSettings.Items.Add(item);
			this.listSettings.SelectedItems.Clear();
			this.listSettings.Focus();
			item.Selected = true;
			SaveCurrentlySelected();
		}

		private bool SaveCurrentlySelected()
		{
			if (this.listSettings.SelectedItems.Count == 0)
				return false;

			SettingsFile1 file = this.listSettings.SelectedItems[0].Tag as SettingsFile1;
			bool ret = App.TheApp.SaveCurrentSettings(file.Name);
			if (ret)
			{
				App.AppSettings.LoadSettings(App.CurAppSettings);
				this.listSettings.SelectedItems[0].ForeColor = Color.Black;
				this.btnSave.Enabled = false;
			}

			bool allSaved = true;
			foreach (ListViewItem item in this.listSettings.Items)
			{
				if (item.ForeColor == Color.Red)
				{
					allSaved = false;
					break;
				}
			}
			if (allSaved)
				this.btnSaveAll.Enabled = false;

			return ret;
		}

		private void btnCopySetting_Click(object sender, EventArgs e)
		{
			if (this.listSettings.SelectedItems.Count == 0)
				return;

			SettingsFile1 file = this.listSettings.SelectedItems[0].Tag as SettingsFile1;
			SettingsFile1 fileCopy = new SettingsFile1();
			fileCopy.LoadSettings(file);
			if (file.Name.StartsWith("copy"))
				CreateNewSetting(fileCopy, file.Name);
			else
				CreateNewSetting(fileCopy, "copy" + file.Name);
		}

		private void btnRemoveSetting_Click(object sender, EventArgs e)
		{
			if (this.listSettings.SelectedItems.Count == 0)
				return;

			SettingsFile1 file = this.listSettings.SelectedItems[0].Tag as SettingsFile1;
			int index = this.listSettings.SelectedIndices[0];
			this.listSettings.SelectedItems.Clear();
			if (file.Name != "Default" && App.TheApp.DeleteSettings(file.Name))
			{
				App.SettingsList.Remove(file);
				this.listSettings.Items.Remove(this.listSettings.Items[index]);
			}
			if (index <= this.listSettings.Items.Count - 1)
			{
				this.listSettings.Items[index].Selected = true;
			}
			else
			{
				if (index-1 > 0)
					this.listSettings.Items[index-1].Selected = true;
				else
					this.listSettings.SelectedItems.Clear();
			}
		}

		private void txtRenameSetting_TextChanged(object sender, EventArgs e)
		{
			if (this.listSettings.SelectedItems.Count == 0 || this.updatingSelection)
				return;

			this.listSettings.SelectedItems[0].Text = this.txtRenameSetting.Text;
			this.listSettings.SelectedItems[0].ForeColor = Color.Red;
			if (!this.subscribedToRename)
			{
				this.subscribedToRename = true;
				this.txtRenameSetting.LostFocus += new EventHandler(txtRenameSetting_LostFocus);
			}
		}

		void txtRenameSetting_LostFocus(object sender, EventArgs e)
		{
			this.txtRenameSetting.LostFocus -= new EventHandler(txtRenameSetting_LostFocus);
			this.subscribedToRename = false;
			SettingsFile1 file = this.listSettings.SelectedItems[0].Tag as SettingsFile1;
			if (this.listSettings.SelectedItems[0].Text == String.Empty || SettingAlreadyExists(this.listSettings.SelectedItems[0].Text))
			{
				this.listSettings.SelectedItems[0].Text = file.Name;
				this.listSettings.SelectedItems[0].ForeColor = Color.Black;
			}
			else
			{
				string temp = file.Name;
				file.Name = this.listSettings.SelectedItems[0].Text;
				file.SettingsLoaded = file.Name;
				App.AppSettings.SettingsLoaded = file.Name;
				if (SaveCurrentlySelected())
				{
					App.TheApp.DeleteSettings(temp);
				}
				else
				{
					App.AppSettings.SettingsLoaded = temp;
				}
			}
		}

		private bool SettingAlreadyExists(string fileName)
		{
			foreach (SettingsFile1 file in App.SettingsList)
			{
				if (file.Name == fileName)
					return true;
			}
			return false;
		}

		private void txtSettingDescription_TextChanged(object sender, EventArgs e)
		{
			if (this.listSettings.SelectedItems.Count == 0 || this.updatingSelection)
				return;

			SettingsFile1 file = this.listSettings.SelectedItems[0].Tag as SettingsFile1;
			file.Description = this.txtSettingDescription.Text;
			this.listSettings.SelectedItems[0].ForeColor = Color.Red;
		}

		private void listSettings_AfterLabelEdit(object sender, LabelEditEventArgs e)
		{
			ListViewItem oldItem = null;
			foreach (ListViewItem item in this.listSettings.Items)
			{
				if (item.Text == e.Label || e.Label == "Default" || e.Label == "settings")
				{
					e.CancelEdit = true;
					return;
				}
				else if (item.Text == this.oldName)
				{
					oldItem = item;
				}
			}

			if (oldItem != null)
			{
				SettingsFile1 file = oldItem.Tag as SettingsFile1;
				int index = App.SettingsList.IndexOf(file);
				file.Name = e.Label;
				App.SettingsList[index].Name = e.Label;
				App.AppSettings.LoadSettings(App.CurAppSettings);
				if (App.TheApp.SaveSettings(e.Label))
				{
					App.TheApp.SaveSettings("settings");
					App.TheApp.DeleteSettings(this.oldName);
				}
			}
		}

		private void listSettings_BeforeLabelEdit(object sender, LabelEditEventArgs e)
		{
			if (e.Item.ToString() == "Default")
				e.CancelEdit = true;
			else
				this.oldName = e.Item.ToString();
		}

		private void btnSave_Click(object sender, EventArgs e)
		{
			SaveCurrentlySelected();
		}

		private void btnSaveAll_Click(object sender, EventArgs e)
		{
			int currentIndex = App.CurAppSettingsIndex;
			int temp = 0;
			
			SettingsFile1 file = null;
			for (int x = 0; x < this.listSettings.Items.Count; x++)
			{
				if (this.listSettings.Items[x].ForeColor == Color.Red)
				{
					file = this.listSettings.Items[x].Tag as SettingsFile1;
					temp = App.SettingsList.IndexOf(file);
					App.CurAppSettingsIndex = temp;
					if (App.TheApp.SaveCurrentSettings(file.Name))
						this.listSettings.Items[x].ForeColor = Color.Black;
				}
			}
			this.btnSaveAll.Enabled = false;
		}

		private void listSettings_DragDrop(object sender, DragEventArgs e)
		{
			if (e.Data.GetDataPresent(DataFormats.FileDrop))
			{
				string[] array = (string[])e.Data.GetData(DataFormats.FileDrop);
				XmlSerializer serializer = new XmlSerializer(typeof(SettingsFile1));
				FileStream stream = null;
				SettingsFile1 file = null;
				foreach(string path in array)
				{
					if (Path.GetExtension(path) == App.UserSettingExt)
					{
						if (!SettingAlreadyExists(Path.GetFileNameWithoutExtension(path)))
						{
							string dest = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\" + Application.ProductName + "\\";
							dest += Path.GetFileName(path);
							File.Copy(path, dest);
							stream = File.Open(dest, FileMode.Open);
							file = serializer.Deserialize(stream) as SettingsFile1;
							if (file != null)
								App.SettingsList.Add(file);
						}
					}
				}
				InitializeSettingsListView(this.listSettings);
			}
		}

		private void listSettings_DragEnter(object sender, DragEventArgs e)
		{
			if (e.Data.GetDataPresent(DataFormats.FileDrop))
			{
				string[] array = (string[])e.Data.GetData(DataFormats.FileDrop);
				bool oneBadFileFound = false;
				foreach (string path in array)
				{
					if (Path.GetExtension(path) == App.UserSettingExt)
					{
						if (SettingAlreadyExists(Path.GetFileNameWithoutExtension(path)))
						{
							oneBadFileFound = true;
							break;
						}
					}
					else
					{
						oneBadFileFound = true;
						break;
					}
				}
				if (!oneBadFileFound)
					e.Effect = DragDropEffects.Copy;
			}
		}

		private void listSettings_ItemDrag(object sender, ItemDragEventArgs e)
		{
			System.Diagnostics.Debug.WriteLine("test");
			string path = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\" + Application.ProductName + "\\";
			ListViewItem item = e.Item as ListViewItem;
			path += item.Text;
			path += App.UserSettingExt;
			
			string[] array = { path };
			DataObject data = new DataObject(DataFormats.FileDrop, array);
			DoDragDrop(data, DragDropEffects.Copy);
		}
	}
}
