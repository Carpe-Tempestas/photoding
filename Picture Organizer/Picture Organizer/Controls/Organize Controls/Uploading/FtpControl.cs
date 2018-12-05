using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using EnterpriseDT.Net.Ftp;
using Trebuchet.Settings.UploadTypes;
using System.Security;
using System.Runtime.InteropServices;
using Trebuchet.Controls.Uploading;

namespace Trebuchet.Controls
{
	public partial class FtpControl : UserControl, IUpControl
    {
        private List<FTPFile> ftpDirectories = new List<FTPFile>();
        private FTPConnection ftpConnection;
        private TreeNode expandedNode = null;
        private bool initializing = false;
		private string oldAlbum;
		private string customAlbum;
		private ToolTip locationTip;
		private bool updating = false;

        public FtpControl()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            Initialize();
			App.CurAppSettings.FolderSelectChanged += new EventHandler(CurAppSettings_FolderSelectChanged);
        }

		void CurAppSettings_FolderSelectChanged(object sender, EventArgs e)
		{
			if (this.chkUseDefaultAlbum.Checked)
			{
				this.txtAlbum.Text = App.CurAppSettings.FolderSelectSettings.Album;
			}
		}

		public void Initialize()
        {
            this.initializing = true;
            Ftp ftpSettings = App.Uploaders.GetUpload(App.CurAppSettings.UploadDetails.Name) as Ftp;
            if (ftpSettings != null)
            {
                this.txtUsername.Text = ftpSettings.Username;
				try
				{
					this.txtPassword.Text = ftpSettings.Request(ftpSettings.Password);
				}
				catch (Exception ex)
				{
					System.Diagnostics.Debug.WriteLine(ex.Message);
				}
                this.txtAddress.Text = ftpSettings.ServerAddress;
                this.numPort.Value = ftpSettings.ServerPort;
				this.chkRememberPassword.Checked = ftpSettings.RememberPass;
				App.Uploaders.UpdateUpload(ftpSettings);
            }
			this.txtLocation.Text = App.CurAppSettings.UploadDetails.UploadPath;
			this.chkUseDefaultAlbum.Checked = App.CurAppSettings.UploadDetails.UseDefaultAlbum;
			this.chkAppend.Checked = App.CurAppSettings.UploadDetails.AppendAlbum;
			if (this.chkUseDefaultAlbum.Checked)
				this.txtAlbum.Text = App.CurAppSettings.FolderSelectSettings.Album;
			else
				this.txtAlbum.Text = App.CurAppSettings.UploadDetails.Album;
			this.customAlbum = App.CurAppSettings.UploadDetails.Album;
            this.initializing = false;
        }

        private void btnTestConnection_Click(object sender, EventArgs e)
		{
			if (this.txtUsername.Text == String.Empty || this.txtPassword.Text == String.Empty)
			{
				MessageBox.Show("You must fill in your username and/or password in order to connect");
				return;
			}

            this.treeFolders.Nodes.Clear();
            bool opened = false;
            try
			{
				if (this.ftpConnection == null)
				{
					this.ftpConnection = new FTPConnection();
					this.ftpConnection.ServerAddress = this.txtAddress.Text;
					this.ftpConnection.ServerPort = (int)this.numPort.Value;
					this.ftpConnection.UserName = this.txtUsername.Text;
					this.ftpConnection.Password = this.txtPassword.Text;
					this.ftpConnection.ConnectMode = FTPConnectMode.PASV;
					this.ftpConnection.Connect();
					opened = true;
				}
				InitializeTreeNodes();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
				this.ftpConnection.Dispose();
				this.ftpConnection = null;
            }
            finally
            {
                if (opened)
                {
                    this.btnConnect.Text = "Disconnect";
                }
                else
                {
                    this.btnConnect.Text = "Connect";
                }
            }
        }

        private void InitializeTreeNodes()
        {
            this.ftpConnection.ChangeWorkingDirectory("/");
            FTPFile[] tempFiles = ftpConnection.GetFileInfos();
            string name;
            foreach (FTPFile file in tempFiles)
            {
                if (file.Dir && file.Name != "." && file.Name != "..")
                {
                    this.ftpDirectories.Add(file);
                    TreeNode node = new TreeNode(file.Name);
                    node.Tag = file;
                    this.treeFolders.Nodes.Add(node);
                    Application.DoEvents();
                }
            }
        }

        private void AddChildren(FTPFile file)
        {
            file.Children = ftpConnection.GetFileInfos(file.Path);
        }

        private void RefreshTreeNode(TreeNode node)
        {
            string path = AssembleFolderPath(node);
            if (this.ftpConnection.ServerDirectory == path)
                return;

            this.ftpConnection.ChangeWorkingDirectory(path);
            FTPFile[] array = ftpConnection.GetFileInfos(path);
            int count = 0;
            foreach (FTPFile file in array)
            {
                if (!NodeExists(file.Name, node) && file.Dir)
                {
                    node.Nodes.Insert(count, file.Name);
                }
            }
            Application.DoEvents();
        }

        private bool NodeExists(string toFind, TreeNode node)
        {
            if (toFind == "." || toFind == "..")
                return true;

            foreach (TreeNode nodeSearch in node.Nodes)
            {
                if (nodeSearch.Text == toFind)
                    return true;
            }
            return false;
        }

        private string AssembleFolderPath(TreeNode node)
        {
            string ret = "/" + node.Text;
            TreeNode parent = node.Parent;
            while (parent != null)
            {
                ret = ret.Insert(0, "/" + parent.Text);
                parent = parent.Parent;
            }
            return ret;
        }

		public void UpdateCurAppSettings()
        {
            if (this.initializing)
                return;

            Ftp ftpSettings = App.Uploaders.GetUpload(App.CurAppSettings.UploadDetails.Name) as Ftp;
            if (ftpSettings == null)
                ftpSettings = new Ftp();

            ftpSettings.Username = this.txtUsername.Text;
			ftpSettings.RememberPass = this.chkRememberPassword.Checked;
			if(ftpSettings.RememberPass)
				ftpSettings.Password = ftpSettings.Tell(this.txtPassword.Text);
            ftpSettings.ServerAddress = this.txtAddress.Text;
            ftpSettings.ServerPort = (int)this.numPort.Value;
            App.CurAppSettings.UploadDetails.UploadPath = RemoveAppendedAlbum();
			App.CurAppSettings.UploadDetails.UseDefaultAlbum = this.chkUseDefaultAlbum.Checked;
			App.CurAppSettings.UploadDetails.AppendAlbum = this.chkAppend.Checked;
			App.CurAppSettings.UploadDetails.Album = this.customAlbum;
            App.Uploaders.UpdateUpload(ftpSettings);
        }

        private void txtAddress_TextChanged(object sender, EventArgs e)
        {
			ManageTestConnectionEnabled();
			UpdateCurAppSettings();
        }

        private void txtUsername_TextChanged(object sender, EventArgs e)
        {
            ManageTestConnectionEnabled();
			UpdateCurAppSettings();
        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {
			ManageTestConnectionEnabled();
			UpdateCurAppSettings();
        }

        private void ManageTestConnectionEnabled()
        {
            if (String.IsNullOrEmpty(this.txtAddress.Text) || String.IsNullOrEmpty(this.txtUsername.Text) || String.IsNullOrEmpty(this.txtPassword.Text))
                this.btnConnect.Enabled = false;
            else
                this.btnConnect.Enabled = true;
        }

        private void treeFolders_AfterExpand(object sender, TreeViewEventArgs e)
        {
            Application.Idle += new EventHandler(Application_Idle);
            this.expandedNode = e.Node;
        }

        void Application_Idle(object sender, EventArgs e)
        {
            Application.Idle -= new EventHandler(Application_Idle);
            RefreshTreeNode(this.expandedNode);
        }

        private void UpdateLocationText()
        {
			if (this.chkAppend.Checked)
			{
				if (String.IsNullOrEmpty(this.txtAlbum.Text))
					this.txtLocation.Text = RemoveAppendedAlbum();
				else if (IsAlbumAppended())
					this.txtLocation.Text = RemoveAppendedAlbum() + "/" + this.txtAlbum.Text;
				else
					this.txtLocation.Text = this.txtLocation.Text + "/" + this.txtAlbum.Text;
			}
			else if (IsAlbumAppended())
			{
				this.txtLocation.Text = RemoveAppendedAlbum();
			}
        }

        private string RemoveAppendedAlbum()
        {
			if (String.IsNullOrEmpty(this.oldAlbum))
				return this.txtLocation.Text;

            string locationText = this.txtLocation.Text;
            string albumText = String.Empty;
			if(!String.IsNullOrEmpty(this.oldAlbum))
				albumText = "/" + this.oldAlbum;

			if (albumText.Length > locationText.Length)
				return "";
            string ret = locationText.Remove(locationText.Length - albumText.Length, albumText.Length);
            return ret;
        }

        private bool IsAlbumAppended()
        {
            string albumText = "/" + this.oldAlbum;
            albumText = albumText.ToLower();
            string location = this.txtLocation.Text;
            location = location.ToLower();
            int count = albumText.Length-1;
            int search = location.Length-1;
            bool ret = false;


            for (int x = count; x >= 0; x--)
            {
				if (search < 0)
					return false;

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

        private void treeFolders_AfterSelect(object sender, TreeViewEventArgs e)
        {
            RefreshTreeNode(e.Node);
            this.txtLocation.Text = AssembleFolderPath(e.Node);
			UpdateLocationText();
        }

        private void chkUseDefaultAlbum_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkUseDefaultAlbum.Checked)
            {
				this.txtAlbum.Enabled = false;
				this.oldAlbum = this.txtAlbum.Text;
				this.txtAlbum.Text = App.CurAppSettings.FolderSelectSettings.Album;
            }
            else
			{
				this.txtAlbum.Enabled = true;
				this.txtAlbum.Text = this.customAlbum;
            }
			UpdateCurAppSettings();
        }

        private void chkAppend_CheckedChanged(object sender, EventArgs e)
        {
			UpdateLocationText();
            UpdateCurAppSettings();
        }

        private void txtAlbum_TextChanged(object sender, EventArgs e)
        {
			this.updating = true;
			UpdateLocationText();
            this.oldAlbum = this.txtAlbum.Text;

			if (!this.chkUseDefaultAlbum.Checked)
				this.customAlbum = this.txtAlbum.Text;
			this.updating = false;
			UpdateCurAppSettings();
        }

		private void txtLocation_TextChanged(object sender, EventArgs e)
		{
			if (!this.updating && this.chkAppend.Checked && !IsAlbumAppended())
			{
				this.updating = true;
				UpdateLocationText();
				UpdateCurAppSettings();
				this.updating = false;
			}
		}

		private void txtLocation_MouseHover(object sender, EventArgs e)
		{
			if (this.locationTip == null)
				this.locationTip = new ToolTip();
			this.locationTip.SetToolTip(this.txtLocation, this.txtLocation.Text);
		}

		private void chkRememberPassword_CheckedChanged(object sender, EventArgs e)
		{
			UpdateCurAppSettings();
		}
    }
}
