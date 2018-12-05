using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.IO;
using Controls.ModifyRegistry;
using System.Net;
using Trebuchet.Settings;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using Trebuchet.Settings.ImageTypes;
using System.Reflection;
using Microsoft.Win32;
using System.Security.AccessControl;
using System.Security.Permissions;
using System.Media;

namespace Trebuchet
{
    public partial class App : Form
    {
		public static event EventHandler CurAppSettingChanging;
		public static event EventHandler CurAppSettingChanged;
		public static event EventHandler SettingLoaded;
        public static App TheApp;
        public static Core TheCore;
		public static List<ImageColorMatrix> RunningMatrixList = new List<ImageColorMatrix>();
		public static InterfaceSettings IntSettings = new InterfaceSettings();
        public static SettingsFile1 AppSettings = new SettingsFile1();
        public static UploadSettings Uploaders = new UploadSettings();
		public static ImageMatricies ImageMatricies = new ImageMatricies();
		public static List<SettingsFile1> SettingsList = new List<SettingsFile1>();
		private static int curAppSettingsIndex;
		public static string MemoryDirectory = "";
		public static bool CopyFromStick = true;
		public static bool UsesStick = false;
		public event EventHandler UploadersChanged;
		public event EventHandler Saving;
		public event EventHandler Loading;
        public static string DateText = "";
        private string loadedSettings = "";
        private bool updatingSettings = false;
		private bool updating = false;
        private Dialog dialog = null;
        public const string RestartMessage = "In order to change modes, photoding needs to restart.  " +
			"Any changes since your last successful organization will be lost.  Please press OK to continue.";
		public const string UploaderExt = ".dup";
		public const string CustomEffectExt = ".dfx";
		public const string StandardSettingExt = ".dd";
		public const string UserSettingExt = ".ds";
		public const string IntSettingExt = ".di";
		private ToolTip tooltip = new ToolTip();

        public App()
        {
            InitializeComponent();
			App.UsesStick = false;
        }

		public App(string drive)
		{
			InitializeComponent();

			App.UsesStick = true;
			string driveDir = PicturesExist(drive);
			string dcimDir = String.Empty;
			if(DcimExists(drive))
				dcimDir = PicturesExist(Path.Combine(drive, "DCIM"));

			if (dcimDir != String.Empty)
			{
				App.MemoryDirectory = dcimDir;
				App.CopyFromStick = true;
			}
			else if (driveDir != String.Empty)
			{
				App.MemoryDirectory = driveDir;
				App.CopyFromStick = true;
			}
			else
			{
				App.MemoryDirectory = drive;
				App.CopyFromStick = false;
			}
		}

		public static SettingsFile1 CurAppSettings
		{
			get 
			{
				if (App.SettingsList == null || App.SettingsList.Count == 0)
					return null;

				return App.SettingsList[App.CurAppSettingsIndex]; 
			}
		}

		public static int CurAppSettingsIndex
		{
			get { return App.curAppSettingsIndex; }
			set
			{
				if (App.CurAppSettingChanging != null)
					App.CurAppSettingChanging(App.CurAppSettings, new EventArgs());
				App.curAppSettingsIndex = value;
				App.AppSettings.LoadSettings(App.CurAppSettings);
				if (App.CurAppSettingChanged != null)
					App.CurAppSettingChanged(App.CurAppSettings, new EventArgs());
			}
		}

		private string PicturesExist(string path)
		{
			string[] directories = Directory.GetDirectories(path);
			foreach (string dir in directories)
			{
				if (PicturesExist(dir) != String.Empty)
					return dir;
			}

			string[] files = Directory.GetFiles(path);
			foreach (string file in files)
			{
				if(Core.IsValidExtension(Path.GetExtension(file)))
					return path;
			}
			return String.Empty;
		}

		private bool DcimExists(string drive)
		{
			string[] directories = Directory.GetDirectories(drive);
			foreach (string dir in directories)
			{
				if (dir.ToUpper().Contains("DCIM"))
					return true;
			}
			return false;
		}

		public static int SetToDefaultIndex()
		{
			int defaultIndex = 0;
			foreach (SettingsFile1 file in App.SettingsList)
			{
				if (file.Name == "settings" || file.Name == "Default")
				{
					defaultIndex = App.SettingsList.IndexOf(file);
					App.CurAppSettingsIndex = defaultIndex;
					App.AppSettings.LoadSettings(App.CurAppSettings);
					return defaultIndex;
				}
			}
			return defaultIndex;
		}

		public static ColorMatrix GetIdentityMatrix()
		{
			float[][] colorMatrixElements = { 
                   new float[] {1.0f,  0.0f,  0.0f,  0.0f, 0.0f},
                   new float[] {0.0f,  1.0f,  0.0f,  0.0f, 0.0f},
                   new float[] {0.0f,  0.0f,  1.0f,  0.0f, 0.0f},
                   new float[] {0.0f,  0.0f,  0.0f,  1.0f, 0.0f},
                   new float[] {0.0f,  0.0f,  0.0f,  0.0f, 1.0f}
                };

			ColorMatrix wmColorMatrix = new
							ColorMatrix(colorMatrixElements);
			return wmColorMatrix;
		}
		/*
		Matrix00, Matrix01, Matrix02, Matrix03, Matrix04,
		Matrix10, Matrix11, Matrix12, Matrix13, Matrix14,
		Matrix20, Matrix21, Matrix22, Matrix23, Matrix24,
		Matrix30, Matrix31, Matrix32, Matrix33, Matrix34,
		Matrix40, Matrix41, Matrix42, Matrix43, Matrix44,
		*/

		public static ColorMatrix MultiplyMatrices(ColorMatrix m1, ColorMatrix m2)
		{
			ColorMatrix final = new ColorMatrix();

			final.Matrix00 = m1.Matrix00 * m2.Matrix00 + m1.Matrix01 * m2.Matrix10 + m1.Matrix02 * m2.Matrix20 + m1.Matrix03 * m2.Matrix30 + m1.Matrix04 * m2.Matrix40;
			final.Matrix01 = m1.Matrix00 * m2.Matrix01 + m1.Matrix01 * m2.Matrix11 + m1.Matrix02 * m2.Matrix21 + m1.Matrix03 * m2.Matrix31 + m1.Matrix04 * m2.Matrix41;
			final.Matrix02 = m1.Matrix00 * m2.Matrix02 + m1.Matrix01 * m2.Matrix12 + m1.Matrix02 * m2.Matrix22 + m1.Matrix03 * m2.Matrix32 + m1.Matrix04 * m2.Matrix42;
			final.Matrix03 = m1.Matrix00 * m2.Matrix03 + m1.Matrix01 * m2.Matrix13 + m1.Matrix02 * m2.Matrix23 + m1.Matrix03 * m2.Matrix33 + m1.Matrix04 * m2.Matrix43;
			final.Matrix04 = m1.Matrix00 * m2.Matrix04 + m1.Matrix01 * m2.Matrix14 + m1.Matrix02 * m2.Matrix24 + m1.Matrix03 * m2.Matrix34 + m1.Matrix04 * m2.Matrix44;

			final.Matrix10 = m1.Matrix10 * m2.Matrix00 + m1.Matrix11 * m2.Matrix10 + m1.Matrix12 * m2.Matrix20 + m1.Matrix13 * m2.Matrix30 + m1.Matrix14 * m2.Matrix40;
			final.Matrix11 = m1.Matrix10 * m2.Matrix01 + m1.Matrix11 * m2.Matrix11 + m1.Matrix12 * m2.Matrix21 + m1.Matrix13 * m2.Matrix31 + m1.Matrix14 * m2.Matrix41;
			final.Matrix12 = m1.Matrix10 * m2.Matrix02 + m1.Matrix11 * m2.Matrix12 + m1.Matrix12 * m2.Matrix22 + m1.Matrix13 * m2.Matrix32 + m1.Matrix14 * m2.Matrix42;
			final.Matrix13 = m1.Matrix10 * m2.Matrix03 + m1.Matrix11 * m2.Matrix13 + m1.Matrix12 * m2.Matrix23 + m1.Matrix13 * m2.Matrix33 + m1.Matrix14 * m2.Matrix43;
			final.Matrix14 = m1.Matrix10 * m2.Matrix04 + m1.Matrix11 * m2.Matrix14 + m1.Matrix12 * m2.Matrix24 + m1.Matrix13 * m2.Matrix34 + m1.Matrix14 * m2.Matrix44;

			final.Matrix20 = m1.Matrix20 * m2.Matrix00 + m1.Matrix21 * m2.Matrix10 + m1.Matrix22 * m2.Matrix20 + m1.Matrix23 * m2.Matrix30 + m1.Matrix24 * m2.Matrix40;
			final.Matrix21 = m1.Matrix20 * m2.Matrix01 + m1.Matrix21 * m2.Matrix11 + m1.Matrix22 * m2.Matrix21 + m1.Matrix23 * m2.Matrix31 + m1.Matrix24 * m2.Matrix41;
			final.Matrix22 = m1.Matrix20 * m2.Matrix02 + m1.Matrix21 * m2.Matrix12 + m1.Matrix22 * m2.Matrix22 + m1.Matrix23 * m2.Matrix32 + m1.Matrix24 * m2.Matrix42;
			final.Matrix23 = m1.Matrix20 * m2.Matrix03 + m1.Matrix21 * m2.Matrix13 + m1.Matrix22 * m2.Matrix23 + m1.Matrix23 * m2.Matrix33 + m1.Matrix24 * m2.Matrix43;
			final.Matrix24 = m1.Matrix20 * m2.Matrix04 + m1.Matrix21 * m2.Matrix14 + m1.Matrix22 * m2.Matrix24 + m1.Matrix23 * m2.Matrix34 + m1.Matrix24 * m2.Matrix44;

			final.Matrix30 = m1.Matrix30 * m2.Matrix00 + m1.Matrix31 * m2.Matrix10 + m1.Matrix32 * m2.Matrix20 + m1.Matrix33 * m2.Matrix30 + m1.Matrix34 * m2.Matrix40;
			final.Matrix31 = m1.Matrix30 * m2.Matrix01 + m1.Matrix31 * m2.Matrix11 + m1.Matrix32 * m2.Matrix21 + m1.Matrix33 * m2.Matrix31 + m1.Matrix34 * m2.Matrix41;
			final.Matrix32 = m1.Matrix30 * m2.Matrix02 + m1.Matrix31 * m2.Matrix12 + m1.Matrix32 * m2.Matrix22 + m1.Matrix33 * m2.Matrix32 + m1.Matrix34 * m2.Matrix42;
			final.Matrix33 = m1.Matrix30 * m2.Matrix03 + m1.Matrix31 * m2.Matrix13 + m1.Matrix32 * m2.Matrix23 + m1.Matrix33 * m2.Matrix33 + m1.Matrix34 * m2.Matrix43;
			final.Matrix34 = m1.Matrix30 * m2.Matrix04 + m1.Matrix31 * m2.Matrix14 + m1.Matrix32 * m2.Matrix24 + m1.Matrix33 * m2.Matrix34 + m1.Matrix34 * m2.Matrix44;

			final.Matrix40 = m1.Matrix40 * m2.Matrix00 + m1.Matrix41 * m2.Matrix10 + m1.Matrix42 * m2.Matrix20 + m1.Matrix43 * m2.Matrix30 + m1.Matrix44 * m2.Matrix40;
			final.Matrix41 = m1.Matrix40 * m2.Matrix01 + m1.Matrix41 * m2.Matrix11 + m1.Matrix42 * m2.Matrix21 + m1.Matrix43 * m2.Matrix31 + m1.Matrix44 * m2.Matrix41;
			final.Matrix42 = m1.Matrix40 * m2.Matrix02 + m1.Matrix41 * m2.Matrix12 + m1.Matrix42 * m2.Matrix22 + m1.Matrix43 * m2.Matrix32 + m1.Matrix44 * m2.Matrix42;
			final.Matrix43 = m1.Matrix40 * m2.Matrix03 + m1.Matrix41 * m2.Matrix13 + m1.Matrix42 * m2.Matrix23 + m1.Matrix43 * m2.Matrix33 + m1.Matrix44 * m2.Matrix43;
			final.Matrix44 = m1.Matrix40 * m2.Matrix04 + m1.Matrix41 * m2.Matrix14 + m1.Matrix42 * m2.Matrix24 + m1.Matrix43 * m2.Matrix34 + m1.Matrix44 * m2.Matrix44;
			
			return final;
		}

        protected override void OnLoad(EventArgs e)
        {
            TheApp = this;
			TheCore = new Core();
			string path1 = Application.UserAppDataPath;
			path1 = Path.GetDirectoryName(path1);
			string path2 = Application.CommonAppDataPath;
			path2 = Path.GetDirectoryName(path2);
			string path3 = "Operations";

			LoadInterfaceSettings();

			LoadPrimarySettings();
			LoadSavedSettings();
            LoadUploaders();
			LoadCustomEffects();


			//Handle Memory Stick events
			if (App.UsesStick)
			{
				if (App.CopyFromStick)
				{
					App.AppSettings.FolderSelectSettings.UseDefaultDestination = false;
					App.AppSettings.FolderSelectSettings.SourceFolder = App.MemoryDirectory;
				}
				else
				{
					App.AppSettings.FolderSelectSettings.UseDefaultDestination = false;
					App.AppSettings.FolderSelectSettings.DestinationFolder = App.MemoryDirectory;
				}
			}

            base.OnLoad(e);
			CurAppSettingChanging(this, new EventArgs());
            CurAppSettings.LoadSettings(AppSettings);
			CurAppSettingChanged(this, new EventArgs());

            this.panelActivate.Visible = false;
			this.panelActivate.Enabled = false;

		}

		private void LoadPrimarySettings()
		{
			if (!LoadSettings())
			{
				RestoreDefaultSettings();
			}
		}

		protected override void OnClosing(CancelEventArgs e)
		{
			SaveInterfaceSettings();
			base.OnClosing(e);
		}

		#region Loading
		private bool LoadInterfaceSettings()
		{
			try
			{
				string path = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\" + Application.ProductName;

				if (!Directory.Exists(path))
					Directory.CreateDirectory(path);

				if (File.Exists(path + "\\settings" + App.IntSettingExt))
				{
					XmlSerializer serializer = new XmlSerializer(typeof(InterfaceSettings));
					FileStream fileStream = File.Open(path + "\\settings" + App.IntSettingExt, FileMode.Open);
					InterfaceSettings settings = serializer.Deserialize(fileStream) as InterfaceSettings;
					settings.LoadSettings(settings);
					fileStream.Close();
					App.IntSettings = settings;
				}
				else
				{
					App.IntSettings.LoadDefaults();
					return false;
				}
			}
			catch (Exception e)
			{
				MessageBox.Show("Interface settings could not be loaded. Error: " + e.Message);
				return false;
			}
			return true;
		}

		private bool LoadCustomEffects()
		{
			try
			{
				string path = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\" + Application.ProductName;

				if (!Directory.Exists(path))
					Directory.CreateDirectory(path);

				if (File.Exists(path + "\\customEffects" + App.CustomEffectExt))
				{
					XmlSerializer serializer = new XmlSerializer(typeof(ImageMatricies));
					FileStream fileStream = File.Open(path + "\\customEffects" + App.CustomEffectExt, FileMode.Open);
					ImageMatricies settings = serializer.Deserialize(fileStream) as ImageMatricies;
					settings.LoadSettings(settings);
					fileStream.Close();
					App.ImageMatricies = settings;
				}
				else
					return false;
			}
			catch (Exception e)
			{
				MessageBox.Show("Custom Effect settings could not be loaded. Error: " + e.Message);
				return false;
			}
			return true;
		}

        private bool LoadUploaders()
        {
            try
            {
                string path = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\" + Application.ProductName;

                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                if (File.Exists(path + "\\uploaders" + App.UploaderExt))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(UploadSettings));
                    FileStream fileStream = File.Open(path + "\\uploaders" + App.UploaderExt, FileMode.Open);
                    UploadSettings settings = serializer.Deserialize(fileStream) as UploadSettings;
                    settings.LoadSettings(settings);
                    fileStream.Close();
					App.Uploaders = settings;
                }
                else
                    return false;
            }
            catch (Exception e)
            {
                MessageBox.Show("Uploader settings could not be loaded. Error: " + e.Message);
                return false;
            }
            return true;
		}

		public bool LoadSettings()
		{
			try
			{
				string path = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\" + Application.ProductName;

				if (!Directory.Exists(path))
					Directory.CreateDirectory(path);

				if (File.Exists(path + "\\settings" + App.StandardSettingExt))
				{
					XmlSerializer serializer = new XmlSerializer(typeof(SettingsFile1));
					FileStream fileStream = File.Open(path + "\\settings" + App.StandardSettingExt, FileMode.Open);
					SettingsFile1 settings = serializer.Deserialize(fileStream) as SettingsFile1;
					int mode = settings.ApplicationMode;
					settings.Name = "Default";
					if(settings != null)
						App.SettingsList.Add(settings);
					string newSettingsFile = settings.SettingsLoaded;
					if (settings.SettingsLoaded != SettingsFile1.Default)
					{
						try
						{
							fileStream.Close();
							this.loadedSettings = settings.SettingsLoaded;
							fileStream = File.Open(path + '\\' + settings.SettingsLoaded + App.UserSettingExt, FileMode.Open);
							settings = serializer.Deserialize(fileStream) as SettingsFile1;
							if (settings != null)
							{
								settings.Name = settings.SettingsLoaded;
								App.SettingsList.Add(settings);
								App.CurAppSettingsIndex = App.SettingsList.Count - 1;
							}
							settings.ApplicationMode = (int)SettingsFile1.ModeTypes.Dialog;

						}
						catch (FileNotFoundException e)
						{
							MessageBox.Show("The last settings file used cannot be found, reverting to last known settings");
						}
						catch (Exception e)
						{
							MessageBox.Show("Error in loading settings file: " + e.Message);
						}
					}
					else
					{
						this.loadedSettings = "Default";
					}
					settings.SettingsLoaded = newSettingsFile;
					AppSettings.SettingsLoaded = settings.SettingsLoaded;
					AppSettings.LoadSettings(settings);
					if (String.IsNullOrEmpty(AppSettings.SettingsLoaded))
					{
						this.loadedSettings = "settings";
						AppSettings.SettingsLoaded = "Default";
						SaveSettings("settings");
					}
					CurAppSettings.SettingsLoaded = AppSettings.SettingsLoaded;
					CurAppSettings.LoadSettings(AppSettings);
					fileStream.Close();

					if (this.Loading != null)
						this.Loading(this, new EventArgs());
				}
				else
					return false;
			}
			catch (Exception e)
			{
				MessageBox.Show("Settings could not be loaded - defaults will be used. Error: " + e.Message);
				return false;
			}

			return true;
		}

		#endregion

		#region Saving
		public bool SaveUploaders()
        {
            try
            {
				XmlSerializer serializer = new XmlSerializer(typeof(UploadSettings));
                string path = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\" + Application.ProductName;

                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
                FileStream fileStream = File.Open(path + "\\uploaders"  + App.UploaderExt, FileMode.Create);

                serializer.Serialize(fileStream, Uploaders);
                fileStream.Close();
            }
            catch (Exception e)
            {
                return false;
            }

            return true;
		}

		public bool SaveInterfaceSettings()
		{
			try
			{
				XmlSerializer serializer = new XmlSerializer(typeof(InterfaceSettings));
				string path = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\" + Application.ProductName;

				if (!Directory.Exists(path))
					Directory.CreateDirectory(path);
				FileStream fileStream = File.Open(path + "\\settings" + App.IntSettingExt, FileMode.Create);

				serializer.Serialize(fileStream, IntSettings);
				fileStream.Close();
			}
			catch (Exception e)
			{
				return false;
			}

			return true;
		}

		public bool SaveSettings(string fileName)
		{
			try
			{
				XmlSerializer serializer = new XmlSerializer(typeof(SettingsFile1));
				string path = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\" + Application.ProductName;

				if (!Directory.Exists(path))
					Directory.CreateDirectory(path);
				FileStream fileStream = null;
				if (fileName == "settings")
					fileStream = File.Open(path + '\\' + fileName + App.StandardSettingExt, FileMode.Create);
				else
					fileStream = File.Open(path + '\\' + fileName + App.UserSettingExt, FileMode.Create);


				serializer.Serialize(fileStream, AppSettings);
				fileStream.Close();
			}
			catch (Exception e)
			{
				return false;
			}

			return true;
		}

		public bool DeleteSettings(string fileName)
		{
			if (fileName == "settings" || fileName == "Default")
				return false;

			string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), Application.ProductName);
			path = Path.Combine(path, fileName);
			path += App.UserSettingExt;

			if (File.Exists(path))
				File.Delete(path);

			return true;
		}

		public bool SaveCurrentSettings(string fileName)
		{
			if (String.IsNullOrEmpty(fileName))
				throw new InvalidOperationException();

			try
			{
				XmlSerializer serializer = new XmlSerializer(typeof(SettingsFile1));
				string path = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\" + Application.ProductName;

				if (!Directory.Exists(path))
					Directory.CreateDirectory(path);
				FileStream fileStream = null;
				if (fileName == "settings")
					fileStream = File.Open(path + '\\' + fileName + App.StandardSettingExt, FileMode.Create);
				else
					fileStream = File.Open(path + '\\' + fileName + App.UserSettingExt, FileMode.Create);
				serializer.Serialize(fileStream, CurAppSettings);
				fileStream.Close();

				if (fileName == App.AppSettings.SettingsLoaded)
				{
					fileStream = File.Open(path + '\\' + "settings" + App.StandardSettingExt, FileMode.Create);
					serializer.Serialize(fileStream, AppSettings);
					fileStream.Close();					
				}
			}
			catch (Exception ex)
			{
				System.Diagnostics.Debug.WriteLine("Save Current Exception: " + ex.Message);
				return false;
			}

			return true;
		}
		#endregion

		private void RestoreDefaultSettings()
        {
            App.AppSettings.LoadAllDefaults();
			App.AppSettings.Name = "Default";
			App.SettingsList.Add(App.AppSettings);
			App.SetToDefaultIndex();
            App.CurAppSettings.LoadSettings(App.AppSettings);
			SaveCurrentSettings("settings");
        }        

        private int FindLastDirectory(string directoryPath)
        {
            int positionExtraDir = 0;
            int positionVersion = 0;
            for (int x = 0; x < directoryPath.Length; x++)
            {
                if (directoryPath[x] == '\\')
                {
                    positionExtraDir = positionVersion;
                    positionVersion = x;
                }
            }
            return positionExtraDir;
        }


        private void btnContinue_Click(object sender, EventArgs e)
        {
            SaveSettings("settings");
            ContinueToEditor();
        }

        private void ContinueToEditor()
        {
            this.Visible = false;

			bool maximized = IntSettings.Maximized;
			Size size = IntSettings.WindowSize;
            this.dialog = new Dialog();
			this.dialog.Size = size;

			if (maximized)
				this.dialog.WindowState = FormWindowState.Maximized;
			else
				this.dialog.WindowState = FormWindowState.Normal;

            this.dialog.BigMenu = this.menuAppStrip1;
            this.dialog.Initialize();
            this.dialog.ModeClosing += new EventHandler(TrebuchetModeClosing);
			try
			{
				this.dialog.ShowDialog();
			}
			catch (Exception ex)
			{
				string path = System.Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
				path = Path.Combine(path, "Carpe Tempestas, LLC\\photoding\\log.txt");
				XmlSerializer serializer = new XmlSerializer(typeof(SettingsFile1));
				StringWriter settingsStream = new StringWriter();
				string settings = String.Empty;
				try
				{
					serializer.Serialize(settingsStream, App.CurAppSettings);		
				}
				catch (Exception ex2)
				{
					settingsStream.Dispose();
					settingsStream = null;
				}

				if (File.Exists(path))
					File.Delete(path);
				if (settingsStream != null)
				{
					File.WriteAllText(path, ex.Message + System.Environment.NewLine + ex.StackTrace
						+ System.Environment.NewLine + System.Environment.NewLine + settingsStream.ToString());
				}
				else
				{
					File.WriteAllText(path, ex.Message + System.Environment.NewLine + ex.StackTrace);
				}
				

				if (MessageBox.Show("photoding has experienced a critical error and must close.  If you would like to help improve photoding,"
					+ "please send an email to support@carpetempestas.com and attach the log found at: " + System.Environment.NewLine +
					path + System.Environment.NewLine + System.Environment.NewLine +
					"Would you like to automatically restart photoding?",
					"Critical Error", MessageBoxButtons.YesNo) == DialogResult.Yes)
				{
					Application.Restart();
				}
				Close();
			}
		}

		private void LoadSavedSettings()
		{
			BackgroundWorker bwLoader = new BackgroundWorker();
			bwLoader.DoWork += new DoWorkEventHandler(bwLoader_DoWork);
			bwLoader.RunWorkerAsync();
		}

		void bwLoader_DoWork(object sender, DoWorkEventArgs e)
		{
			string path = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\" + Application.ProductName;
			DirectoryInfo di = new DirectoryInfo(path);
			FileInfo[] rgFiles = di.GetFiles("*.*");
			XmlSerializer serializer = new XmlSerializer(typeof(SettingsFile1));
			foreach (FileInfo fi in rgFiles)
			{
				if (!SettingAlreadyLoaded(Path.GetFileNameWithoutExtension(fi.FullName)) && fi.Extension == App.UserSettingExt)
				{
					DeserializeSetting(serializer, fi);
					if (App.SettingLoaded != null)
						App.SettingLoaded(this, new EventArgs());
				}
			}
		}

		private bool SettingAlreadyLoaded(string settingName)
		{
			foreach (SettingsFile1 file in App.SettingsList)
			{
				if (file.Name == settingName)
					return true;
			}
			return false;
		}

		private void DeserializeSetting(XmlSerializer serializer, FileInfo fi)
		{
			FileStream tempStream = File.Open(fi.FullName, FileMode.Open);
			SettingsFile1 tempSetting = serializer.Deserialize(tempStream) as SettingsFile1;
			if (tempSetting != null)
			{
				tempSetting.Name = Path.GetFileNameWithoutExtension(fi.FullName);
				App.SettingsList.Add(tempSetting);
			}
			else
			{
				MessageBox.Show("Could not serialize: " + fi.FullName);
			}
			tempStream.Close();
		}

        private bool LoadParticularSettings(string path)
        {
            try
            {
                if (File.Exists(path))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(SettingsFile1));
                    FileStream fileStream = File.Open(path, FileMode.Open);
                    SettingsFile1 loadedSettings = serializer.Deserialize(fileStream) as SettingsFile1;
                    AppSettings.LoadSettings(loadedSettings);
                    CurAppSettings.LoadSettings(AppSettings);
                    fileStream.Close();
                    return true;
                }
                else
                    return false;
            }
            catch(Exception e)
            {
                MessageBox.Show("Settings could not be loaded - defaults will be used. Error: " + e.Message);
                return false;
            }

        }

        void TrebuchetModeClosing(object sender, EventArgs e)
        {
            Application.DoEvents();
            this.Close();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            About about = new About();
            about.TopLevel = true;
            about.TopMost = true;
            Rectangle rect = about.Bounds;
            rect.X = this.Bounds.Width / 2 - rect.Width / 2;
            rect.Y = this.Bounds.Height / 2 - rect.Height / 2;
            about.Bounds = rect;
            about.Show();
            about.BringToFront();
            about.Focus();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem item = sender as ToolStripMenuItem;
            if (item != null)
			{
				if (this.Saving != null)
					this.Saving(this, new EventArgs());
				AppSettings.LoadSettings(CurAppSettings);
                SaveCurrentSettings(CurAppSettings.Name);
				SaveUploaders();
				SaveCustomEffects();
            }
        }

		public bool SaveCustomEffects()
		{
			try
			{
				XmlSerializer serializer = new XmlSerializer(typeof(ImageMatricies));
				string path = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\" + Application.ProductName;

				if (!Directory.Exists(path))
					Directory.CreateDirectory(path);
				FileStream fileStream = File.Open(path + "\\customEffects" + App.CustomEffectExt, FileMode.Create);

				serializer.Serialize(fileStream, ImageMatricies);
				fileStream.Close();
			}
			catch (Exception e)
			{
				System.Diagnostics.Debug.WriteLine(e.Message);
				return false;
			}

			return true;
		}

        private void checkForUpdatesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool updateAvailable = false; //TODO: Come up with new download methodology
            if (updateAvailable)
            {                
                if (MessageBox.Show("There appears to be a new version available, would you like to go to the photoding website?",
                    "New Update Available", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    System.Diagnostics.Process.Start("IExplore", "https://github.com/Carpe-Tempestas/photoding");
                }
            }
            else
            {                
                MessageBox.Show("You have the latest version of photoding - thanks for checking and have a wonderful day!");
            }
        }

        private void txtUsername_TextChanged(object sender, EventArgs e)
        {
            ValidateActivateButton();
        }

        private void ValidateActivateButton()
        {
            if (!String.IsNullOrEmpty(this.txtUsername.Text) && !String.IsNullOrEmpty(this.mtxtPassword.Text))
            {
                this.btnActivate.Enabled = true;
            }
            else
            {
                this.btnActivate.Enabled = false;
            }
        }

        private void OnMaskTextChanged(object sender, EventArgs e)
        {
            ValidateActivateButton();
        }


        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (msg.Msg != 256)
                return base.ProcessCmdKey(ref msg, keyData);
            else
                return base.ProcessCmdKey(ref msg, keyData);
        }

		private void EnsureAutoplayUsage()
		{
			//string autoplayHandlers = @"Software\Microsoft\Windows\CurrentVersion\explorer\AutoplayHandlers";
			//string eventHandlers = @"\EventHandlers";
			//string showPictures = @"\ShowPicturesOnArrival";
			//string trebHandler = "TrebOrganizePicturesOnArrival";
			//string handlers = @"\Handlers";
			//string valueAction = @"Action";
			//string valueActionData = "Organize Pictures";
			//string valueDefaultIcon = @"DefaultIcon";
			//string valueDefaultIconData = Assembly.GetExecutingAssembly().Location;
			//string valueInvokeProgID = @"InvokeProgID";
			//string valueInvokeProgIDData = "Treb.Organizer";
			//string valueInvokeVerb = @"InvokeVerb";
			//string valueInvokeVerbData = "import";
			//string valueProvider = @"Provider";
			//string valueProviderData = "Trebuchet";
			//string classesRoot = @"\shell\import\command";

			////RegistryKey eventHandlerKey = Registry.LocalMachine.OpenSubKey(autoplayHandlers + eventHandlers + showPictures);
			////RegistryKey handlerKey = Registry.LocalMachine.OpenSubKey(autoplayHandlers + handlers);
			////RegistryKey classesKey = Registry.ClassesRoot.OpenSubKey(valueInvokeProgIDData);

			//string user = Environment.UserDomainName + "\\" + Environment.UserName;

			//RegistrySecurity rs = new RegistrySecurity();

			//// Allow the current user to read and delete the key.
			////
			//rs.AddAccessRule(new RegistryAccessRule(user,
			//    RegistryRights.SetValue,
			//    InheritanceFlags.None,
			//    PropagationFlags.None,
			//    AccessControlType.Allow));

			////eventHandlerKey.SetAccessControl(rs);
			////handlerKey.SetAccessControl(rs);
			////classesKey.SetAccessControl(rs);

			//if (this.autoplayEventsToolStripMenuItem.Checked)
			//{
				
			//    //Add event handler to ShowPicturesOnArrival
			//    //if (eventHandlerKey.GetValue(trebHandler) == null)
			//    //RegistryKey eventHandlerKey = Registry.LocalMachine.OpenSubKey(autoplayHandlers + eventHandlers + showPictures, true);
			//    RegistryKey photosOnArrival =
			//        Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\explorer\AutoPlayHandlers\EventHandlers\ShowPicturesOnArrival", true);
			//    photosOnArrival.SetValue(trebHandler, "");

			//    RegistryKey handlerKey = null;
			//    //Add handler data for new event handler
			//    if (handlerKey != null)
			//    {
			//        handlerKey = Registry.CurrentUser.CreateSubKey(autoplayHandlers + handlers + "\\" + trebHandler, RegistryKeyPermissionCheck.Default, rs);
			//        handlerKey.SetValue(valueAction, valueActionData, RegistryValueKind.String);
			//        handlerKey.SetValue(valueDefaultIcon, valueDefaultIconData, RegistryValueKind.String);
			//        handlerKey.SetValue(valueInvokeProgID, valueInvokeProgIDData, RegistryValueKind.String);
			//        handlerKey.SetValue(valueInvokeVerb, valueInvokeVerbData, RegistryValueKind.String);
			//        handlerKey.SetValue(valueProvider, valueProviderData, RegistryValueKind.String);
			//    }

			//    RegistryKey classesKey = null;
			//    if (classesKey == null)
			//    {
			//        classesKey = Registry.ClassesRoot.CreateSubKey(valueInvokeProgIDData + classesRoot, RegistryKeyPermissionCheck.Default, rs);
			//        classesKey.SetValue(null, valueDefaultIconData + " \"%1\"");
			//    }

			//    App.IntSettings.UseAutoPlay = true;
			//}
			//else
			//{
			//    Registry.CurrentUser.DeleteSubKeyTree(autoplayHandlers + eventHandlers + showPictures + "\\" + trebHandler);
			//    Registry.CurrentUser.DeleteSubKeyTree(autoplayHandlers + handlers + "\\" + trebHandler);
			//    Registry.ClassesRoot.DeleteSubKeyTree(valueInvokeProgIDData);

			//    App.IntSettings.UseAutoPlay = false;
			//}


			//RegistryKey eventHandlerKey = Registry.LocalMachine.OpenSubKey(autoplayHandlers+eventHandlers+showPictures);
			//RegistryKey handlerKey = Registry.LocalMachine.OpenSubKey(autoplayHandlers + handlers);
			//RegistryKey classesKey = Registry.ClassesRoot.OpenSubKey(valueInvokeProgIDData);

			//string user = Environment.UserDomainName + "\\" + Environment.UserName;

			//RegistrySecurity rs = new RegistrySecurity();

			//// Allow the current user to read and delete the key.
			////
			//rs.AddAccessRule(new RegistryAccessRule(user,
			//    RegistryRights.FullControl,
			//    InheritanceFlags.None,
			//    PropagationFlags.None,
			//    AccessControlType.Allow));

			////eventHandlerKey.SetAccessControl(rs);
			////handlerKey.SetAccessControl(rs);
			////classesKey.SetAccessControl(rs);

			//if (this.autoplayEventsToolStripMenuItem.Checked)
			//{
			//    //Add event handler to ShowPicturesOnArrival
			//    if (eventHandlerKey.GetValue(trebHandler) == null)
			//        eventHandlerKey.SetValue(trebHandler, "", RegistryValueKind.String);

			//    //Add handler data for new event handler
			//    if (handlerKey != null)
			//    {
			//        handlerKey.CreateSubKey(trebHandler, RegistryKeyPermissionCheck.Default, rs);
			//        handlerKey = handlerKey.OpenSubKey(trebHandler, true);
			//        handlerKey.SetValue(valueAction, valueActionData, RegistryValueKind.String);
			//        handlerKey.SetValue(valueDefaultIcon, valueDefaultIconData, RegistryValueKind.String);
			//        handlerKey.SetValue(valueInvokeProgID, valueInvokeProgIDData, RegistryValueKind.String);
			//        handlerKey.SetValue(valueInvokeVerb, valueInvokeVerbData, RegistryValueKind.String);
			//        handlerKey.SetValue(valueProvider, valueProviderData, RegistryValueKind.String);
			//    }

			//    if (classesKey == null)
			//    {
			//        classesKey = Registry.ClassesRoot.CreateSubKey(valueInvokeProgIDData + classesRoot, RegistryKeyPermissionCheck.Default, rs);
			//        classesKey.SetValue(null, valueDefaultIconData + " \"%1\"");
			//    }

			//    App.IntSettings.UseAutoPlay = true;
			//}
			//else
			//{
			//    if(eventHandlerKey != null && eventHandlerKey.GetValue(trebHandler) != null)
			//        eventHandlerKey.DeleteValue(trebHandler);

			//    if (handlerKey != null && handlerKey.OpenSubKey(trebHandler, true) != null)
			//    {
			//        handlerKey.DeleteSubKey(trebHandler);
			//    }

			//    if (classesKey != null && classesKey.OpenSubKey(valueInvokeProgIDData, true) != null)
			//    {
			//        Registry.ClassesRoot.DeleteSubKeyTree(valueInvokeProgIDData);
			//    }

			//    App.IntSettings.UseAutoPlay = false;
			//}

			SaveInterfaceSettings();
		}

		private void lblLicense_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			System.Diagnostics.Process.Start("IExplore", "http://www.carpetempestas.com/photoding/buy.php");
		}

		private void btnOrganize_Click(object sender, EventArgs e)
		{
			if (this.btnOrganize.Text == "Organize")
			{
				App.TheCore.CalculateDryRun();
				this.progressControl1.Bar.Minimum = 0;
				this.progressControl1.Bar.Maximum = App.TheCore.FileTotal;
				App.TheCore.ProgressMade += new EventHandler(TheCore_ProgressMade);
				App.TheCore.Finished += new EventHandler(TheCore_Finished);
				this.panelProgress.Visible = true;
				if(this.panelActivate.Visible)
					this.Size = new Size(this.Size.Width, this.Size.Height + this.panelProgress.Height);
				this.btnOrganize.Text = "Cancel";
				this.tooltip.SetToolTip(this.btnOrganize, "Click here to cancel organization.");
				App.TheCore.Organize();
			}
			else if (this.btnOrganize.Text == "Cancel" && this.btnOrganize.Enabled == true)
			{
				this.btnOrganize.Enabled = false;
				App.TheCore.Cancelling += new EventHandler(TheCore_Cancelling);
				App.TheCore.CancelOrganizing();
			}
		}

		void TheCore_Cancelling(object sender, EventArgs e)
		{
			this.btnOrganize.Text = "Cancelling";
			this.tooltip.SetToolTip(this.btnOrganize, "The organization is being cancelled, please wait until this is finished.");
		}

		void TheCore_Finished(object sender, EventArgs e)
		{
			this.btnOrganize.Text = "Organize";
			this.tooltip.SetToolTip(this.btnOrganize, "Click here to organize your photos.");
			this.btnOrganize.Enabled = true;
			if (this.panelActivate.Visible)
				this.Size = new Size(this.Size.Width, this.Size.Height - this.panelProgress.Height);
			this.panelProgress.Visible = false;
			string path = Application.ExecutablePath;
			path = Path.GetDirectoryName(path);
			path = Path.Combine(path, "ding.wav");
			if (File.Exists(path))
			{
				SoundPlayer player = new SoundPlayer(path);
				player.Play();
			}
		}

		public void TheCore_ProgressMade(object sender, EventArgs e)
		{
			if (this.InvokeRequired)
				this.BeginInvoke((MethodInvoker)delegate { this.TheCore_ProgressMade(this, e); });
			else
			{
				if (this.progressControl1.ProgressTitle != App.TheCore.ProgressTitle)
				{
					this.progressControl1.Bar.Value = 0;
					this.progressControl1.ProgressTitle = App.TheCore.ProgressTitle;
				}

				if (this.progressControl1.Details != App.TheCore.ProgressDetails)
				{
					this.progressControl1.Details = App.TheCore.ProgressDetails;
					try
					{
						this.progressControl1.Bar.Value = App.TheCore.CurrentFile;
					}
					catch (ArgumentOutOfRangeException ex)
					{
						this.progressControl1.Bar.Value = this.progressControl1.Bar.Maximum;
					}
				}
			}
		}

		//private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
		//{
		//    string path = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\" + Application.ProductName;
		//    SaveFileDialog dialog = new SaveFileDialog();
		//    dialog.OverwritePrompt = true;
		//    dialog.InitialDirectory = path;
		//    dialog.DefaultExt = ".ud";
		//    dialog.Filter = "photoding Settings Files (*.ud) |*.ud";
		//    dialog.Title = "Save photoding Settings As...";
		//    dialog.ValidateNames = true;
		//    dialog.ShowDialog();

		//    if (!String.IsNullOrEmpty(dialog.FileName))
		//    {
		//        string filename = dialog.FileName;
		//        int lastSlashIndex = 0;
		//        for (int x = filename.Length - 1; x > 0; x--)
		//        {
		//            if (filename[x] == '\\')
		//            {
		//                lastSlashIndex = x;
		//                break;
		//            }
		//        }
		//        filename = filename.Remove(0, lastSlashIndex + 1);
		//        filename = filename.Remove(filename.Length - 4, 4);
		//        AppSettings.SettingsLoaded = filename;
		//        SaveCurrentSettings(filename);
		//        LoadSettings();
		//        AppSettings.SettingsLoaded = filename;
		//        SaveSettings("settings");
		//        Application.Restart();
		//    }
		//}
    }
}