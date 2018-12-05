using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Text;
using System.Windows.Forms;
using System.IO;
using ICSharpCode.SharpZipLib.Zip;

using Trebuchet.Properties;
using Trebuchet.Controls;
using Trebuchet.Settings;
using System.Management;
using ICSharpCode.SharpZipLib.Checksums;
using Trebuchet.Settings.WatermarkTypes;
using EnterpriseDT.Net.Ftp;
using Trebuchet.Settings.UploadTypes;
using System.Security;
using System.Runtime.InteropServices;
//using Facebook.Components;
using System.Collections.ObjectModel;
//using Facebook.Entity;
using FlickrNet;
using Google.GData.Photos;
using Google.GData.Client;
using HelperClasses;
using Trebuchet.Settings.ImageTypes;
using Trebuchet.Helper_Classes;

namespace Trebuchet
{
    public class Core : IDisposable
    {
        List<string> paths = new List<string>();
		List<string> tempPaths = new List<string>();
		bool useTempPaths = false;
		bool stillResizing = false;
		bool stillAdjusting = false;
        private string source;
        private string destination;
        private ImageFormat format = ImageFormat.Jpeg;
        private SettingsFile1 settings = new SettingsFile1();
        private string selectedPhotoPath = String.Empty;
		private Image selectedImage = null;
		private string selectedWatermarkPath = String.Empty;
		private Image watermarkImage = null;
		private Image remapImage = null;
		private int remapPercent = 0;
		private FastBitmap fastRemapImage = null;
		private FileStream selectedFile = null;
        private const float SmallestWatermarkRatio = 0.04f;
        private bool disposed = false;
		private const string Organized = "\\organized\\";
		private const string Adjusted = "\\adjusted\\";
        private const string Resized = "\\resized\\";
        private const string Renamed = "\\renamed\\";
		private const string Watermarked = "\\watermarked\\";
		//private const string Temp = "\\temp\\";
		private int modeTotal = 0;
		private int fileTotal = 0;
		private int currentMode = 0;
		private int currentFile = 0;
		private BackgroundWorker organizingThread = new BackgroundWorker();
		public event EventHandler Cancelling;
		public event EventHandler Finished;
		public event EventHandler ProgressMade;
		private string error;
		private string progressTitle;
		private string progressDetails;
		private string lastTempDirectory;

        public enum Alignment
        {
            TopLeft,
            TopCenter,
            TopRight,
            CenterLeft,
            CenterCenter,
            CenterRight,
            BottomLeft,
            BottomCenter,
            BottomRight,
            Tile,
            Custom
        };

        public enum Actions
        {
            Move,
            Copy,
            DoNothing,
        };

        public Core()
        {
			this.organizingThread.WorkerSupportsCancellation = true;
			this.organizingThread.DoWork += new DoWorkEventHandler(organizingThread_DoWork);
			this.organizingThread.RunWorkerCompleted += new RunWorkerCompletedEventHandler(organizingThread_RunWorkerCompleted);
        }

        ~Core()
        {
            Dispose(false);
        }
        
        // Implement IDisposable.
        // Do not make this method virtual.
        // A derived class should not be able to override this method.
        public void Dispose()
        {
            Dispose(true);
            // This object will be cleaned up by the Dispose method.
            // Therefore, you should call GC.SupressFinalize to
            // take this object off the finalization queue
            // and prevent finalization code for this object
            // from executing a second time.
            GC.SuppressFinalize(this);
        }


        private void Dispose(bool disposing)
        {
            // Check to see if Dispose has already been called.
            if (!this.disposed)
            {
                // If disposing equals true, dispose all managed
                // and unmanaged resources.
                if (disposing)
                {
                    // Dispose managed resources.
					if (this.selectedImage != null)
						this.selectedImage.Dispose();

                }

                // Call the appropriate methods to clean up
                // unmanaged resources here.
                // If disposing is false,
                // only the following code is executed.

                // Note disposing has been done.
                this.disposed = true;

            }
        }

		public string Error
		{
			get { return this.error; }
			set { this.error = value; }
		}

		public int ModeTotal
		{
			get { return this.modeTotal; }
			set { this.modeTotal = value; }
		}

		public int FileTotal
		{
			get { return this.fileTotal; }
			set { this.fileTotal = value; }
		}

		public int CurrentFile
		{
			get { return this.currentFile; }
			set { this.currentFile = value; }
		}

		public int CurrentMode
		{
			get { return this.currentMode; }
			set 
			{ 
				this.currentMode = value;
				this.CurrentFile = 0;
			}
		}
		

		public string ProgressTitle
		{
			get { return this.progressTitle; }
			set 
			{ 
				this.progressTitle = value;
				if (this.ProgressMade != null)
					this.ProgressMade(this, new EventArgs());
			}
		}

		public string ProgressDetails
		{
			get { return this.progressDetails; }
			set
			{
				this.progressDetails = value;
				this.CurrentFile++;
				if (this.ProgressMade != null)
					this.ProgressMade(this, new EventArgs());
			}
		}

        public string SelectedPhotoPath
        {
            get { return this.selectedPhotoPath; }
            set 
            {
                if (value != this.selectedPhotoPath)
                {
                    if (this.selectedImage != null)
                    {
						CleanUpStream();
                        this.selectedImage.Dispose();
                        this.selectedImage = null;
                    }
                    this.selectedPhotoPath = value;
					if (!String.IsNullOrEmpty(this.selectedPhotoPath))
					{
						CleanUpStream();
						try
						{
							this.selectedFile = new FileStream(this.SelectedPhotoPath, FileMode.Open, FileAccess.Read);
							this.selectedImage = Image.FromStream(this.selectedFile);
						}
						catch (Exception ex)
						{
							System.Diagnostics.Debug.WriteLine("Core Error: " + ex.Message);
						}
					}
                }
            }
        }

		private void CleanUpStream()
		{
			if (this.selectedFile != null)
			{
				this.selectedFile.Close();
				this.selectedFile.Dispose();
				this.selectedFile = null;
			}
		}

		public bool CalculateDryRun()
		{
			this.ModeTotal = 0;
			this.FileTotal = 0;

			if (String.IsNullOrEmpty(App.CurAppSettings.FolderSelectSettings.SourceFolder)
				&& !Directory.Exists(App.CurAppSettings.FolderSelectSettings.SourceFolder))
				return false;
			
			this.FileTotal = Directory.GetFiles(App.CurAppSettings.FolderSelectSettings.SourceFolder).Length;
			if (!App.CurAppSettings.FolderSelectSettings.UseDefaultDestination &&
				(App.CurAppSettings.FolderSelectSettings.FolderAction == 0 || 
				App.CurAppSettings.FolderSelectSettings.FolderAction == 1))
			{
				this.ModeTotal++;
			}

			if (App.CurAppSettings.RenameMode)
			{
				this.ModeTotal++;
				if (App.CurAppSettings.RenameSettings.CreateDir)
					this.ModeTotal++;
			}

			if (App.CurAppSettings.ImageAdjSettings.CurrentEffect.InUse)
				this.ModeTotal++;

			if (App.CurAppSettings.ResizeMode)
			{
				this.ModeTotal++;
				if (App.CurAppSettings.ResizeSettings.CreateDir)
					this.ModeTotal++;
			}

			if (App.CurAppSettings.WatermarkMode)
			{
				this.ModeTotal++;
				if (App.CurAppSettings.WatermarkSettings.CreateDir)
					this.ModeTotal++;
			}

			if (App.CurAppSettings.CompressMode)
				this.ModeTotal++;

			if (App.CurAppSettings.UploadModeEnabled)
				this.ModeTotal++;

			return true;

		}

        private bool PerformFinalCheck()
        {
			if (!CalculateDryRun())
				return false;

            if (App.CurAppSettings.RenameMode ||
                App.CurAppSettings.ResizeMode ||
                App.CurAppSettings.WatermarkMode ||
                App.CurAppSettings.CompressMode ||
                App.CurAppSettings.UploadModeEnabled ||
                App.CurAppSettings.ImageAdjSettings.IsImageGettingAdjusted())
                return true;
            else
                return false;
		}

		public void CancelOrganizing()
		{
			if (this.organizingThread.IsBusy)
			{
				this.organizingThread.CancelAsync();
				if (this.Cancelling != null)
					this.Cancelling(this, new EventArgs());
			}
		}

		void organizingThread_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			if (this.Finished != null)
				this.Finished(this, new EventArgs());
		}

		void organizingThread_DoWork(object sender, DoWorkEventArgs e)
		{
			this.CurrentMode = 0;
			if (PerformFinalCheck())
			{
				this.error = String.Empty;
				this.settings.LoadSettings(App.CurAppSettings);

				if (!Directory.Exists(App.CurAppSettings.FolderSelectSettings.SourceFolder))
				{
					this.Error = "Source directory not valid.";
					MessageBox.Show(this.Error);
					return;
				}

				if (!ValidateDirectories())
				{
					return;
				}
				else
				{
					this.CurrentMode++;
				}

				this.stillResizing = false;
				this.useTempPaths = false;


				DirectoryInfo di = new DirectoryInfo(App.CurAppSettings.FolderSelectSettings.SourceFolder);
				FileInfo[] rgFiles = di.GetFiles("*.*");
				this.ProgressDetails = "";

				if (App.CurAppSettings.RenameMode && String.IsNullOrEmpty(this.Error) && !this.organizingThread.CancellationPending)
				{
					RenamePictures();
					this.CurrentMode++;
				}

				GetPictures();

				if (App.CurAppSettings.ImageAdjSettings.CurrentEffect != null && App.CurAppSettings.ImageAdjSettings.CurrentEffect.InUse
					&& String.IsNullOrEmpty(this.Error) && !this.organizingThread.CancellationPending)
				{
					AdjustPictures();
					this.CurrentMode++;
				}

				if (App.CurAppSettings.ResizeMode && String.IsNullOrEmpty(this.Error) && !this.organizingThread.CancellationPending)
				{
					ResizePictures();
					this.CurrentMode++;
				}

				if (App.CurAppSettings.WatermarkMode && String.IsNullOrEmpty(this.Error) && !this.organizingThread.CancellationPending)
				{
					WatermarkPictures();
					this.CurrentMode++;
				}

				if (App.CurAppSettings.CompressMode && String.IsNullOrEmpty(this.Error) && !this.organizingThread.CancellationPending)
				{
					ZipImages();
					this.CurrentMode++;
				}

				if (App.CurAppSettings.UploadModeEnabled && String.IsNullOrEmpty(this.Error) && !this.organizingThread.CancellationPending)
				{
					UploadPictures();
					this.CurrentMode++;
				}

				this.paths.Clear();
				this.tempPaths.Clear();
				GC.Collect();

				if (String.IsNullOrEmpty(this.Error) && !this.organizingThread.CancellationPending)
				{
					App.AppSettings.LoadSettings(App.CurAppSettings);
					if (App.AppSettings.SettingsLoaded != SettingsFile1.Default)
						App.TheApp.SaveSettings(App.AppSettings.SettingsLoaded);
					else
						App.TheApp.SaveSettings("settings");
					App.TheApp.SaveUploaders();
				}
			}
			else
			{
				this.Error = "No modes were selected so nothing could be done.";
				MessageBox.Show(this.Error);
			}
		}

        public void Organize()
        {
			this.organizingThread.RunWorkerAsync();
        }

        private bool ValidateDirectories()
        {
            if(App.CurAppSettings.FolderSelectSettings.UseDefaultDestination)
            {
                this.source = App.CurAppSettings.FolderSelectSettings.SourceFolder;
                this.destination = this.source;
                return true;
            }

            if (!Directory.Exists(App.CurAppSettings.FolderSelectSettings.DestinationFolder))
            {
                MessageBox.Show("Destination directory not found, create?", "Destination Not Found", MessageBoxButtons.YesNo);
                Directory.CreateDirectory(App.CurAppSettings.FolderSelectSettings.DestinationFolder);
            }
            this.destination = App.CurAppSettings.FolderSelectSettings.DestinationFolder;

            DirectoryInfo di = new DirectoryInfo(App.CurAppSettings.FolderSelectSettings.SourceFolder);
            FileInfo[] rgFiles = di.GetFiles("*.*");
			if (App.CurAppSettings.FolderSelectSettings.FolderAction == 0) //Move
            {
                this.ProgressTitle = "Moving Progress...";
                foreach (FileInfo fi in rgFiles)
                {
                    try
                    {
                        this.ProgressDetails = String.Format("{0}", fi.Name);
                        fi.MoveTo(this.destination + "\\" + fi.Name);
                    }
                    catch (Exception e)
                    {
                        System.Diagnostics.Debug.WriteLine("Out of Memory Exception, or file is not valid image.  Error details: " + e.Message);
                        return false;
                    }
                    this.source = this.destination;
                    this.ProgressDetails = String.Format("Finished moving files");
                }
            }
			else if (App.CurAppSettings.FolderSelectSettings.FolderAction == 1) //Copy
            {
                this.ProgressTitle = "Copying Progress...";
                foreach (FileInfo fi in rgFiles)
                {
                    try
                    {
                        this.ProgressDetails = String.Format("{0}", fi.Name);
                        fi.CopyTo(this.destination + "\\" + fi.Name);
                    }
                    catch (Exception e)
                    {
                        System.Diagnostics.Debug.WriteLine("Out of Memory Exception, or file is not valid image.  Error details: " + e.Message);
                        return false;
                    }
					if (this.organizingThread.CancellationPending)
						return false;
                }
                this.source = this.destination;
                this.ProgressDetails = String.Format("Finished copying files");
            }
			else if (App.CurAppSettings.FolderSelectSettings.FolderAction == 2) //Do Nothing
            {
                this.source = this.destination;
            }
            else
                return false;

            return true;
        }

        private Font GetFont()
        {
            TypeConverter tc = TypeDescriptor.GetConverter(typeof(Font));
			if (GetWatermarkSettings() == null)
				return new Font(FontFamily.GenericSansSerif, 12);
			else
				return (Font)tc.ConvertFromString(((WatText)GetWatermarkSettings()).WatFont);
        }

        private void GetPictures()
        {
            if (this.paths.Count > 0)
                return;

            try
            {
                DirectoryInfo di = null;
                if (!String.IsNullOrEmpty(this.source))
                    di = new DirectoryInfo(this.source);
                else
                    di = new DirectoryInfo(App.CurAppSettings.FolderSelectSettings.SourceFolder);

                FileInfo[] rgFiles = di.GetFiles("*.*");
                foreach (FileInfo fi in rgFiles)
                {
                    try
                    {
                        if(IsValidExtension(fi.Extension))
                            this.paths.Add(fi.FullName);
                        GC.Collect();
                    }
                    catch (Exception e)
                    {
                        System.Diagnostics.Debug.WriteLine("Out of Memory Exception, or file is not valid image.  Error details: " + e.Message);
                    }
                }
            }
            catch (Exception e)
            {
				this.Error = "An error occurred during initial file processing: " + e.Message;
                MessageBox.Show(this.Error);
                return;
            }
        }

        public static bool IsValidExtension(string ext)
        {
            ext = ext.ToLower();
            ext = ext.Remove(0, 1);
            if (ext == "jpeg" || ext == "jpg" || ext == "tif" || ext == "tiff" || ext == "png" || ext == "gif"
                || ext == "bmp" || ext == "exif" || ext == "wmf")
                return true;
            else
                return false;
        }

        private void RenamePictures()
		{
			this.ProgressTitle = "Renaming photos";
            RenameOriginals();
            CreateRenameDirectory();
            this.ProgressDetails = "Renaming photos finished.";
        }

        private void RenameOriginals()
        {
            int count = 0;
			if (App.CurAppSettings.RenameSettings.RenameOriginals && App.CurAppSettings.FolderSelectSettings.FolderAction != (int)Actions.Move)
            {
                try
                {
                    DirectoryInfo di = new DirectoryInfo(App.CurAppSettings.FolderSelectSettings.SourceFolder);
                    FileInfo[] rgFiles = di.GetFiles("*.*");
                    foreach (FileInfo fi in rgFiles)
                    {
                        this.ProgressDetails = String.Format("Renaming {0} to {1}", fi.Name,
                            GetFileName(count));
                        if(IsValidExtension(fi.Extension))
                            fi.MoveTo(App.CurAppSettings.FolderSelectSettings.SourceFolder + "\\" + GetFileName(count++) + fi.Extension);
					}

					//We've renamed the originals so we have to redo all the paths to go along with the new names
					this.paths.Clear();
					GetPictures();
                }
                catch (Exception e)
                {
					this.Error = "An error occurred during initial file processing: " + e.Message;
                    MessageBox.Show(this.Error);
                    return;
                }
                this.ProgressDetails = "Finished renaming originals.";
            }
            else if(App.CurAppSettings.RenameSettings.RenameOriginals)
            {
                try
                {
                    DirectoryInfo di = new DirectoryInfo(this.source);
                    FileInfo[] rgFiles = di.GetFiles("*.*");
                    foreach (FileInfo fi in rgFiles)
                    {
                        this.ProgressDetails = String.Format("Renaming {0} to {1}", fi.Name,
                            GetFileName(count));
                        fi.MoveTo(this.source + "\\" + GetFileName(count++) + fi.Extension);
					}

					//We've renamed the originals so we have to redo all the paths to go along with the new names
					this.paths.Clear();
					GetPictures();
                }
                catch (Exception e)
                {
					this.Error = "An error occurred during initial file processing: " + e.Message;
                    MessageBox.Show(this.Error);
                    return;
                }
                this.ProgressDetails = "Finished renaming originals.";
            }
        }

        private SettingsBase GetWatermarkSettings()
        {
            return App.CurAppSettings.WatermarkSettings.Setting;
        }

        private void CreateRenameDirectory()
        {
            int count = 0;
            if (App.CurAppSettings.RenameSettings.CreateDir)
            {
                try
                {
                    DirectoryInfo di = new DirectoryInfo(this.source);
                    DirectoryInfo diRenamed = new DirectoryInfo(this.destination + Core.Renamed);
                    if (!diRenamed.Exists)
                        diRenamed.Create();

                    FileInfo[] rgFiles = di.GetFiles("*.*");
                    foreach (FileInfo fi in rgFiles)
                    {
                        this.ProgressDetails = String.Format(Core.Renamed + "{0}", GetFileName(count));
                        this.paths.Add(this.source + Core.Renamed + GetFileName(count) + fi.Extension);
                        fi.CopyTo(this.paths[this.paths.Count - 1], true);
                        count++;
                    }
                }
                catch (Exception e)
                {
					this.Error = "An error occurred during renaming file processing: " + e.Message;
                    MessageBox.Show(this.Error);
                    return;
                }
                this.ProgressDetails = "Finished creating rename directory.";
            }
            else if (!App.CurAppSettings.RenameSettings.RenameOriginals && !App.CurAppSettings.ResizeMode && !App.CurAppSettings.WatermarkMode)
            {
                try
                {
                    DirectoryInfo di = new DirectoryInfo(this.source);
                    DirectoryInfo diRenamed = new DirectoryInfo(this.destination + Core.Organized);
                    if (App.CurAppSettings.FolderSelectSettings.UseDefaultDestination && !diRenamed.Exists)
                        diRenamed.Create();

                    FileInfo[] rgFiles = di.GetFiles("*.*");

                    if (!App.CurAppSettings.FolderSelectSettings.UseDefaultDestination
						&& (App.CurAppSettings.FolderSelectSettings.FolderAction == (int)Actions.DoNothing ||
						App.CurAppSettings.FolderSelectSettings.FolderAction == (int)Actions.Copy ||
						App.CurAppSettings.FolderSelectSettings.FolderAction == (int)Actions.Move))
                    {
                        foreach (FileInfo fi in rgFiles)
                        {
                            this.ProgressDetails = String.Format("{0}", GetFileName(count));
                            this.paths.Add(this.source + "\\" + GetFileName(count) + fi.Extension);
                            fi.MoveTo(this.paths[this.paths.Count - 1]);
                            count++;
                        }
                    }
                    else
                    {
                        foreach (FileInfo fi in rgFiles)
                        {
                            this.ProgressDetails = String.Format(Core.Organized + "{0}", GetFileName(count));
                            this.paths.Add(this.source + Core.Organized + GetFileName(count) + fi.Extension);
                            fi.CopyTo(this.paths[this.paths.Count - 1], true);
                            count++;
                        }
                    }
                }
                catch (Exception e)
                {
					this.Error = "An error occurred during renaming file processing: " + e.Message;
                    MessageBox.Show(this.Error);
                    return;
                }
                this.ProgressDetails = "Finished renaming photos.";
            }
        }

        public void InitializeSettings()
        {
            this.settings.LoadSettings(App.CurAppSettings);
        }

        public void RemovePictures()
        {
            this.paths.Clear();
            this.settings = null;
        }

        public string GetFileName(int count)
        {
            if (App.CurAppSettings.RenameMode)
            {
                if (this.paths.Count <= count)
                    GetPictures();

                string mask = App.CurAppSettings.RenameSettings.FileMask;
                if (mask.Contains("{date}"))
                {
                    int dateIndex = mask.IndexOf("{date}");
                    mask = mask.Remove(dateIndex, 6);
                    if (String.IsNullOrEmpty(App.DateText))
                    {
                        DateTimePicker datePicker = new DateTimePicker();
                        datePicker.Format = DateTimePickerFormat.Custom;
                        datePicker.CustomFormat = App.CurAppSettings.RenameSettings.DateFormat;
                        datePicker.CreateControl();
                        mask = mask.Insert(dateIndex, datePicker.Text);
                    }
                    else
                    {
                        mask = mask.Insert(dateIndex, App.DateText);
                    }
                }
                if (App.CurAppSettings.RenameSettings.IDBeforeMask)
                {
                    return GetNumString(count+1) + mask;
                }
                else
                {
                    return mask + GetNumString(count+1);
                }
            }
            else
            {
                int right = FindPeriod(count);
                int left = GetLastDirectoryCharacter(count, right);
                return this.paths[count].Substring(left, right - left);
            }
        }

        private int GetLastDirectoryCharacter(int count, int right)
        {
            string path = this.paths[count];
            int temp = path.Length - 1;
            while (path[temp] != '\\' && temp > 0)
                temp--;
            return temp;
        }

        private int FindPeriod(int count)
        {
            string path = this.paths[count];
            int temp = path.Length - 1;
            while (path[temp] != '.' && temp > 0)
                temp--;
            return temp;
        }

        private string GetNumString(int count)
        {
            string text = count.ToString();
            if (App.CurAppSettings.RenameSettings.NumericIDLength != 1)
            {
                while (text.Length < App.CurAppSettings.RenameSettings.NumericIDLength)
                {
                    text = text.Insert(0, "0");
                }
            }

            return text;
        }

        private bool ResizePictures()
		{
			this.ProgressTitle = "Resizing photos";

            Image image = null;
			string directory = "";
            this.stillResizing = true;
            for(int x = 0;x<this.paths.Count && !this.organizingThread.CancellationPending;x++)
            {
                if(App.CurAppSettings.ResizeSettings.CreateDir)
                    directory = Core.Resized;
                
                this.ProgressDetails = String.Format(directory + "{0}", GetFileName(x));
                image = GetImage(x);
                ResizePic(ref image, true);
				if (App.CurAppSettings.ResizeSettings.CreateDir)
					WriteFile(ref image, x, Path.Combine(this.destination, Core.Resized));
				WriteFile(ref image, x, Path.Combine(this.destination, Core.Organized));
            }
            this.ProgressDetails = "Finished resizing photos.";
            this.stillResizing = false;
			if (this.organizingThread.CancellationPending)
				return false;
			else
				return true;
        }

        public Image ResizePic(ref Image image, bool dispose)
        {
            if (App.CurAppSettings.ResizeSettings.Choice == 0)
                image = ScaleByPercent(ref image, (int)App.CurAppSettings.ResizeSettings.Percent, dispose);
            else
                image = ScaleByBound(ref image, App.CurAppSettings.ResizeSettings.BoundWidth, 
                    App.CurAppSettings.ResizeSettings.BoundHeight, dispose);
            return image;
        }

		private void WriteFile(ref Image image, int count, string dirName)
		{
			if (!String.IsNullOrEmpty(dirName))
			{
				DirectoryInfo specificDir = new DirectoryInfo(this.destination + dirName);
				if (!specificDir.Exists)
					specificDir.Create();
				try
				{
					this.paths[count] = this.destination + dirName + GetFileName(count) + GetExtension(this.paths[count]);
					SaveFile(ref image, this.paths[count]);
				}
				catch (Exception e)
				{
					MessageBox.Show("An error occurred during resized directory creation.  Most likely, you've already "
					+ "ran the watermark part of the program and it has created files there to the exact name you've specified."
					+ "  Delete the directory and run this part of the program again.");
					return;
				}
			}
			else
			{
				try
				{
					if (this.CurrentMode < this.ModeTotal)
					{
						DirectoryInfo specificDir = new DirectoryInfo(this.destination + Core.Organized);
						if (!specificDir.Exists)
							specificDir.Create();

						this.paths[count] = this.destination + Core.Organized + GetFileName(count) + GetExtension(this.paths[count]);
						SaveFile(ref image, this.paths[count]);
					}
					else
					{
						if (!App.CurAppSettings.FolderSelectSettings.UseDefaultDestination && App.CurAppSettings.FolderSelectSettings.FolderAction == (int)Actions.DoNothing)
							SaveFile(ref image, this.destination + "\\" + GetFileName(count) + GetExtension(this.paths[count]));
						else
						{
							DirectoryInfo diOrganized = new DirectoryInfo(this.destination + Core.Organized);
							if (!diOrganized.Exists)
								diOrganized.Create();
							SaveFile(ref image, this.destination + Core.Organized + GetFileName(count) + GetExtension(this.paths[count]));
						}
					}
				}
				catch (Exception e)
				{
					MessageBox.Show("An error occurred during tempResize directory creation.  Most likely, you've already "
					+ "ran the tempResize part of the program and it has created files there to the exact name you've specified."
					+ "  Delete the directory and run this part of the program again.");
					return;
				}
			}
		}

        private Image GetImage(int count)
        {
			this.SelectedPhotoPath = GetPath(count);
			Image image = (Image)this.selectedImage.Clone();
			bool fullyCloned = false;
			while (!fullyCloned)
			{
				try
				{
					int x = image.PropertyItems.Length;
					fullyCloned = true;
				}
				catch (Exception e)
				{
					System.Diagnostics.Debug.WriteLine("Not cloned yet.");
					System.Threading.Thread.Sleep(10);
				}
			}
			//this.SelectedPhotoPath = String.Empty;
            this.format = image.RawFormat;
			return image;
        }

		private string GetPath(int count)
		{
			string path = String.Empty;
			path = this.paths[count];
			return path;				
		}

        private Image ScaleByPercent(ref Image imgPhoto, int Percent, bool dispose)
        {
            float nPercent = ((float)Percent / 100);

            int sourceWidth = imgPhoto.Width;
            int sourceHeight = imgPhoto.Height;
            int sourceX = 0;
            int sourceY = 0;

            int destX = 0;
            int destY = 0;
            int destWidth = (int)(sourceWidth * nPercent);
            int destHeight = (int)(sourceHeight * nPercent);

            if (destWidth <= 0 || destHeight <= 0)
                return imgPhoto;

            Bitmap bmPhoto = new Bitmap(destWidth, destHeight, PixelFormat.Format24bppRgb);

            if (!App.CurAppSettings.ImageAdjSettings.UseDefaultResolution)
                bmPhoto.SetResolution((float)App.CurAppSettings.ImageAdjSettings.ResolutionDPI, (float)App.CurAppSettings.ImageAdjSettings.ResolutionDPI);
            else
                bmPhoto.SetResolution(imgPhoto.HorizontalResolution, imgPhoto.VerticalResolution);

            Graphics grPhoto = Graphics.FromImage(bmPhoto);
            grPhoto.InterpolationMode = InterpolationMode.HighQualityBicubic;

            grPhoto.DrawImage(imgPhoto,
                new Rectangle(destX, destY, destWidth, destHeight),
                new Rectangle(sourceX, sourceY, sourceWidth, sourceHeight),
                GraphicsUnit.Pixel);

			foreach (PropertyItem item in imgPhoto.PropertyItems)
			{
				bmPhoto.SetPropertyItem(item);
			}
            if (dispose)
            {
                imgPhoto.Dispose();
                imgPhoto = null;
            }
            grPhoto.Dispose();
            return bmPhoto;
        }

        public Image ScaleToSize(ref Image imgPhoto, Rectangle sourceBounds, Rectangle destBounds, bool dispose)
        {
            Bitmap bmPhoto = new Bitmap(destBounds.Width, destBounds.Height, PixelFormat.Format24bppRgb);
            Graphics grPhoto = Graphics.FromImage(bmPhoto);
            grPhoto.PixelOffsetMode = PixelOffsetMode.Half;
            grPhoto.InterpolationMode = InterpolationMode.NearestNeighbor;

            grPhoto.DrawImage(imgPhoto,
                destBounds,
                sourceBounds,
                GraphicsUnit.Pixel);

			foreach (PropertyItem item in imgPhoto.PropertyItems)
			{
				bmPhoto.SetPropertyItem(item);
			}
			if (dispose)
			{
				imgPhoto.Dispose();
				imgPhoto = null;
			}
            grPhoto.Dispose();
            return bmPhoto;
        }

        public Image ScaleByBound(ref Image imgPhoto, int width, int height, bool dispose)
        {
            int sourceWidth = imgPhoto.Width;
            int sourceHeight = imgPhoto.Height;
            int sourceX = 0;
            int sourceY = 0;

            int destX = 0;
            int destY = 0;
            int destWidth = width;
            int destHeight = height;

            if (App.CurAppSettings.ResizeSettings.BoundWidthEnabled && !App.CurAppSettings.ResizeSettings.BoundHeightEnabled)
            {
                float percent = (float)destWidth / (float)sourceWidth;
                destHeight = (int)(sourceHeight * percent);
            }
            else if (!App.CurAppSettings.ResizeSettings.BoundWidthEnabled && App.CurAppSettings.ResizeSettings.BoundHeightEnabled)
            {
                float percent = (float)destHeight / (float)sourceHeight;
                destWidth = (int)(sourceWidth * percent);
            }

            Bitmap bmPhoto = new Bitmap(destWidth, destHeight, PixelFormat.Format24bppRgb);

            if (!App.CurAppSettings.ImageAdjSettings.UseDefaultResolution)
                bmPhoto.SetResolution((float)App.CurAppSettings.ImageAdjSettings.ResolutionDPI, (float)App.CurAppSettings.ImageAdjSettings.ResolutionDPI);
            else
                bmPhoto.SetResolution(imgPhoto.HorizontalResolution, imgPhoto.VerticalResolution);

            Graphics grPhoto = Graphics.FromImage(bmPhoto);
            grPhoto.InterpolationMode = InterpolationMode.HighQualityBicubic;

            grPhoto.DrawImage(imgPhoto,
                new Rectangle(destX, destY, destWidth, destHeight),
                new Rectangle(sourceX, sourceY, sourceWidth, sourceHeight),
                GraphicsUnit.Pixel);

			foreach (PropertyItem item in imgPhoto.PropertyItems)
			{
				bmPhoto.SetPropertyItem(item);
			}

            if (dispose)
            {
                imgPhoto.Dispose();
                imgPhoto = null;
            }
            grPhoto.Dispose();
			return bmPhoto;
        }

        public Image AutoScaleByBound(ref Image imgPhoto, int width, int height)
        {
			if (width <= 0 || height <= 0)
				return imgPhoto;

            int sourceWidth = imgPhoto.Width;
            int sourceHeight = imgPhoto.Height;
            int sourceX = 0;
            int sourceY = 0;

            int destX = 0;
            int destY = 0;
            int destWidth = width;
            int destHeight = height;

            float imagePercent = (float)imgPhoto.Width / (float)imgPhoto.Height;
            float boundPercent = (float)width / (float)height;

            if (imagePercent > boundPercent)
            {
                destHeight = (int)((float)width / (float)imgPhoto.Width * (float)imgPhoto.Height);
            }
            else
            {
                destWidth = (int)((float)height / (float)imgPhoto.Height * (float)imgPhoto.Width);
            }

            Bitmap bmPhoto = new Bitmap(destWidth, destHeight, PixelFormat.Format24bppRgb);
            Graphics grPhoto = Graphics.FromImage(bmPhoto);
            grPhoto.InterpolationMode = InterpolationMode.HighQualityBicubic;

            grPhoto.DrawImage(imgPhoto,
                new Rectangle(destX, destY, destWidth, destHeight),
                new Rectangle(sourceX, sourceY, sourceWidth, sourceHeight),
                GraphicsUnit.Pixel);

			foreach (PropertyItem item in imgPhoto.PropertyItems)
			{
				bmPhoto.SetPropertyItem(item);
			}
            imgPhoto.Dispose();
            imgPhoto = null;
            grPhoto.Dispose();
            return bmPhoto;
		}

		private bool AdjustPictures()
		{
			this.ProgressTitle = "Adjusting photos";

			Image image = null;
			string directory = "";
			this.stillAdjusting = true;
			for(int x = 0; x < this.paths.Count && !this.organizingThread.CancellationPending; x++)
			{
				if (App.CurAppSettings.ImageAdjSettings.CreateDir)
					directory = Core.Adjusted;

				this.ProgressDetails = String.Format(directory + "{0}", GetFileName(x));
				image = GetImage(x);
				image = ApplyMatrix(image);
				WriteFile(ref image, x, directory);
			}


			this.ProgressDetails = "Finished adjusting photos.";
			this.stillAdjusting = false;

			if (this.organizingThread.CancellationPending)
				return false;
			else
				return true;
		}

		public Image ApplyMatrix(Image image)
		{
			if (App.CurAppSettings.ImageAdjSettings.CurrentEffect == null || !App.CurAppSettings.ImageAdjSettings.CurrentEffect.InUse)
				return image;

			int phWidth = image.Width;
			int phHeight = image.Height;
			Bitmap bmPhoto = new Bitmap(phWidth, phHeight,
								 image.PixelFormat);
			bmPhoto.SetResolution(image.HorizontalResolution, image.VerticalResolution);
			ImageAttributes imageAttributes = new ImageAttributes();
			imageAttributes.SetColorMatrix(FastColorMatrix.ConvertToColorMatrix(App.CurAppSettings.ImageAdjSettings.CurrentEffect.ColMatrix)); 

			Graphics grPhoto = Graphics.FromImage(bmPhoto);
			grPhoto.SmoothingMode = SmoothingMode.AntiAlias;
			grPhoto.InterpolationMode = InterpolationMode.HighQualityBicubic;
			grPhoto.DrawImage(
				image,
				new Rectangle(0, 0, phWidth, phHeight),
				0,
				0,
				phWidth,
				phHeight,
				GraphicsUnit.Pixel,
				imageAttributes);

			foreach (PropertyItem item in image.PropertyItems)
			{
				bmPhoto.SetPropertyItem(item);
			}
			image.Dispose();
			return bmPhoto;
		}

        private bool WatermarkPictures()
		{
			this.ProgressTitle = "Watermarking photos";

            Image image = null;
			for (int x = 0; x < this.paths.Count && !this.organizingThread.CancellationPending; x++)
            {
                this.ProgressDetails = String.Format("{0}", GetFileName(x));
                image = GetImage(x);
                WriteWatermarkedImage(WatermarkPic(image), x);
                image.Dispose();
                image = null;
            }
			this.ProgressDetails = "Finished watermarking photos.";

			if (this.organizingThread.CancellationPending)
				return false;
			else
				return true;
        }

        private void WriteWatermarkedImage(Image image, int count)
        {
            if (App.CurAppSettings.WatermarkSettings.CreateDir)
            {
                DirectoryInfo diWater = new DirectoryInfo(this.destination + Core.Watermarked);
                if (!diWater.Exists)
                    diWater.Create();
                try
                {
                    SaveFile(ref image, this.destination + Core.Watermarked + GetFileName(count) + GetExtension(this.paths[count]));
                }
                catch (Exception e)
                {
                    MessageBox.Show("An error occurred during watermark directory creation.  Most likely, you've already "
                    + "ran the watermark part of the program and it has created files there to the exact name you've specified."
                    + "  Delete the directory and run this part of the program again.");
                    return;
                }
            }
            else
            {
                DirectoryInfo diWater = new DirectoryInfo(this.destination + Core.Organized);
                if (App.CurAppSettings.FolderSelectSettings.UseDefaultDestination && !diWater.Exists)
                    diWater.Create();
                try
                {
					if (!App.CurAppSettings.FolderSelectSettings.UseDefaultDestination && App.CurAppSettings.FolderSelectSettings.FolderAction == (int)Actions.DoNothing)
                        SaveFile(ref image, this.destination + "\\" + GetFileName(count) + GetExtension(this.paths[count]));
                    else
                        SaveFile(ref image, this.destination + Core.Organized + GetFileName(count) + GetExtension(this.paths[count]));
                }
                catch (Exception e)
                {
                    MessageBox.Show("An error occurred during organized directory creation.  Most likely, you've already "
                    + "ran the organized part of the program and it has created files there to the exact name you've specified."
                    + "  Delete the directory and run this part of the program again.");
                    return;
                }
            }
        }

        public Image WatermarkPic(Image image)
        {
            int phWidth = image.Width;
            int phHeight = image.Height;

            Bitmap bmPhoto = new Bitmap(phWidth, phHeight,
                                 image.PixelFormat);

            bmPhoto.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            Graphics grPhoto = Graphics.FromImage(bmPhoto);
            grPhoto.SmoothingMode = SmoothingMode.AntiAlias;
            grPhoto.DrawImage(
                image,
                new Rectangle(0, 0, phWidth, phHeight),
                0,
                0,
                phWidth,
                phHeight,
                GraphicsUnit.Pixel);

            if (App.CurAppSettings.WatermarkSettings.WatType == Watermark.WatermarkType.TextWatermark) //Text
            {
                StringFormat stringFormat = GetAlignmentFormat();
                if (GetWatermarkSettings() == null || String.IsNullOrEmpty(((WatText)GetWatermarkSettings()).WatermarkText))
                    return image;

                SizeF textSize = grPhoto.MeasureString(((WatText)GetWatermarkSettings()).WatermarkText, GetFont());
                textSize = grPhoto.MeasureString(((WatText)GetWatermarkSettings()).WatermarkText, GetFont(), phWidth, stringFormat);

                if ((float)textSize.Height / (float)phHeight < Core.SmallestWatermarkRatio)
                    return image;

                Color backColor = App.CurAppSettings.ConvertFromColorString(((WatText)GetWatermarkSettings()).TextBackground);
                SolidBrush backColorBrush = new SolidBrush(
                    Color.FromArgb((int)App.CurAppSettings.WatermarkSettings.Transparency,
                    backColor.R,
                    backColor.G,
                    backColor.B));

                Color foreColor = App.CurAppSettings.ConvertFromColorString(((WatText)GetWatermarkSettings()).TextForeground);
                SolidBrush foreColorBrush = new SolidBrush(
                             Color.FromArgb(App.CurAppSettings.WatermarkSettings.Transparency,
                             foreColor.R,
                             foreColor.G,
                             foreColor.B));

                if (App.CurAppSettings.WatermarkSettings.Location == Watermark.WatermarkLocation.Tile)
                {
                    PointF topLeft = Point.Empty;
                    Rectangle spacing = GetSpacing();
                    int xCount = (int)((float)(phWidth - spacing.Left) / (float)(textSize.Width + spacing.Width));
                    int yCount = (int)((float)(phHeight - spacing.Top) / (float)(textSize.Height + spacing.Height));

                    for (int x = 0; x < xCount; x++)
                    {
                        for (int y = 0; y < yCount; y++)
                        {
                            topLeft.X = x * (textSize.Width + spacing.Width) + spacing.Left;
                            topLeft.Y = y * (textSize.Height + spacing.Height) + spacing.Top;
                            DrawWatermarkText(grPhoto, backColorBrush, foreColorBrush, topLeft, stringFormat);                           
                        }
                    }
                }
                else if (App.CurAppSettings.WatermarkSettings.Location == Watermark.WatermarkLocation.Custom)
                {
                    DrawWatermarkText(grPhoto, backColorBrush, foreColorBrush, 
                        App.CurAppSettings.ConvertFromPointString(App.CurAppSettings.WatermarkSettings.CustomLocation), stringFormat);
                }
                else
                {
                    PointF topLeft = GetTextCoordinates(textSize, new System.Drawing.Size(phWidth, phHeight));
                    topLeft.X += 1;
                    topLeft.Y += 1;
                    DrawWatermarkText(grPhoto, backColorBrush, foreColorBrush, topLeft, stringFormat);
                }
            }
            else if (App.CurAppSettings.WatermarkSettings.WatType == Watermark.WatermarkType.GraphicWatermark)  //Graphic
            {
                ImageAttributes imageAttributes = new ImageAttributes();
                ColorMap colorMap = new ColorMap();

                Color keyColor = App.CurAppSettings.ConvertFromColorString(((Graphic)GetWatermarkSettings()).Key);
                colorMap.OldColor = Color.FromArgb(255,
                    keyColor.R,
                    keyColor.G,
                    keyColor.B);
                colorMap.NewColor=Color.FromArgb(0, 0, 0, 0);
                ColorMap[] remapTable = {colorMap};

                imageAttributes.SetRemapTable(remapTable, ColorAdjustType.Bitmap);

                float percent = (float)(App.CurAppSettings.WatermarkSettings.Transparency) / 254.0f; 
                float percentRemap = 1.0f;
                if (((Graphic)GetWatermarkSettings()).RemapPercent != 0)
                    percentRemap = 1.0f + 100.0f / (float)((Graphic)GetWatermarkSettings()).RemapPercent;
                float[][] colorMatrixElements = { 
                   new float[] {1.0f,  0.0f,  0.0f,  0.0f, 0.0f},
                   new float[] {0.0f,  1.0f,  0.0f,  0.0f, 0.0f},
                   new float[] {0.0f,  0.0f,  1.0f,  0.0f, 0.0f},
                   new float[] {0.0f,  0.0f,  0.0f,  percent, 0.0f},
                   new float[] {0.0f,  0.0f,  0.0f,  0.0f, 1.0f}
                };

                ColorMatrix wmColorMatrix = new
                                ColorMatrix(colorMatrixElements);

                imageAttributes.SetColorMatrix(wmColorMatrix,
                                       ColorMatrixFlag.Default,
                                         ColorAdjustType.Bitmap);
                imageAttributes.SetWrapMode(WrapMode.TileFlipXY);

				if (!File.Exists(((Graphic)GetWatermarkSettings()).Location))
					return bmPhoto;

				Image imgWatermark = null;
				string watermarkLocation = ((Graphic)GetWatermarkSettings()).Location;

				if (((Graphic)GetWatermarkSettings()).RemapPercent != 0)
					imgWatermark = RemapColors(((Graphic)GetWatermarkSettings()).RemapPercent, watermarkLocation, keyColor);
				else
				{
					imgWatermark = Image.FromFile(watermarkLocation, true);
				}

                Rectangle croppedBounds = GetCroppedGraphicBounds(imgWatermark);
                Rectangle graphicBounds = GetGraphicBounds();
                Rectangle spacing = GetSpacing();
                imgWatermark = ScaleToSize(ref imgWatermark, croppedBounds, graphicBounds, false);

                if (App.CurAppSettings.WatermarkSettings.Location == Watermark.WatermarkLocation.Tile)
                {
                    int xCount = (int)((float)(phWidth - spacing.Left) / (float)(graphicBounds.Width + spacing.Width));
                    int yCount = (int)((float)(phHeight - spacing.Top) / (float)(graphicBounds.Height + spacing.Height));

                    Point position = Point.Empty;
                    for (int x = 0; x < xCount; x++)
                    {
                        for (int y = 0; y < yCount; y++)
                        {
                            position.X = x * (graphicBounds.Width + spacing.Width) + spacing.Left;
                            position.Y = y * (graphicBounds.Height + spacing.Height) + spacing.Top;
                            DrawWatermarkImage(grPhoto, imgWatermark, position, imageAttributes);
                        }
                    }

                }
                else if (App.CurAppSettings.WatermarkSettings.Location == Watermark.WatermarkLocation.Custom)
                {
                    DrawWatermarkImage(grPhoto, imgWatermark, 
                        App.CurAppSettings.ConvertFromPointString(App.CurAppSettings.WatermarkSettings.CustomLocation), 
                        imageAttributes);
                }
                else
                {
                    Point position = GetImagePosition(image, imgWatermark);
                    DrawWatermarkImage(grPhoto, imgWatermark, position, imageAttributes);
                }
            }

			foreach (PropertyItem item in image.PropertyItems)
			{
				bmPhoto.SetPropertyItem(item);
			}
			image.Dispose();
			return bmPhoto;
        }

		private Image RemapColors(int remapPercent, string imageWatermark, Color remapColor)
		{
			if (this.watermarkImage == null || (this.watermarkImage != null && imageWatermark != this.selectedWatermarkPath))
			{
				if (this.watermarkImage != null)
				{
					this.watermarkImage.Dispose();
					this.watermarkImage = null;
				}
				if (this.remapImage != null)
				{
					this.remapImage.Dispose();
					this.remapImage = null;
				}
				this.watermarkImage = new Bitmap(imageWatermark);
				this.selectedWatermarkPath = imageWatermark;
				this.remapImage = this.watermarkImage.Clone() as Image;
				this.fastRemapImage = new FastBitmap(this.remapImage as Bitmap);
			}
			
			if (this.remapPercent != remapPercent)
			{
				this.remapImage = this.watermarkImage.Clone() as Image;
				this.fastRemapImage = new FastBitmap(this.remapImage as Bitmap);
				this.remapPercent = remapPercent;
				int bound = (int)(255.0f * (float)remapPercent / 100.0f);
				int width = this.watermarkImage.Width;
				int height = this.watermarkImage.Height;
				Color upperBound = Color.FromArgb(
					GetUpperColorBound(remapColor.R, bound),
					GetUpperColorBound(remapColor.G, bound),
					GetUpperColorBound(remapColor.B, bound));
				Color lowerBound = Color.FromArgb(
					GetLowerColorBound(remapColor.R, bound),
					GetLowerColorBound(remapColor.G, bound),
					GetLowerColorBound(remapColor.B, bound));
				Color temp = remapColor;
					

				for (int x = 0; x < width; x++)
				{
					for (int y = 0; y < height; y++)
					{
						temp = this.fastRemapImage.GetPixel(x, y);
						if (temp.R >= lowerBound.R && temp.R <= upperBound.R //Red
							&& temp.G >= lowerBound.G && temp.G <= upperBound.G //Green
							&& temp.B >= lowerBound.B && temp.B <= upperBound.B) //Blue
							this.fastRemapImage.SetPixel(x, y, remapColor);
					}
				}
				this.fastRemapImage.Release();
			}
			return this.remapImage;
		}

		private int GetLowerColorBound(int colorValue, int bound)
		{
			int temp = colorValue - bound;
			if (temp < 0)
				return 0;
			else
				return temp;
		}

		private int GetUpperColorBound(int colorValue, int bound)
		{
			int temp = colorValue + bound;
			if (temp > 255)
				return 255;
			else
				return temp;
		}

		public Rectangle GetWatermarkBounds()
        {
			if (this.selectedPhotoPath == String.Empty)
				return Rectangle.Empty;

			Image imgSelected = Image.FromFile(this.selectedPhotoPath, true);
            Point customPosition = App.CurAppSettings.ConvertFromPointString(App.CurAppSettings.WatermarkSettings.CustomLocation);

            if (App.CurAppSettings.WatermarkSettings.WatType == Watermark.WatermarkType.TextWatermark)
            {
                //For text watermarks, we need to know what is the smallest and largest possible fonts
                //that we could use to legibly put the watermark on the picture
                Font baseFont = GetFont();
                SizeF textDimensions = SizeF.Empty;
                StringFormat stringFormat = GetAlignmentFormat();

                Bitmap bmPhoto = new Bitmap(imgSelected.Width, imgSelected.Height,
                                     imgSelected.PixelFormat);

                bmPhoto.SetResolution(imgSelected.HorizontalResolution, imgSelected.VerticalResolution);

                Graphics grPhoto = Graphics.FromImage(bmPhoto);
                grPhoto.SmoothingMode = SmoothingMode.AntiAlias;
                grPhoto.DrawImage(
                    imgSelected,
                    new Rectangle(0, 0, imgSelected.Width, imgSelected.Height),
                    0,
                    0,
                    imgSelected.Width,
                    imgSelected.Height,
                    GraphicsUnit.Pixel);

                textDimensions = grPhoto.MeasureString(((WatText)GetWatermarkSettings()).WatermarkText, baseFont);

                return new Rectangle(customPosition.X, customPosition.Y, (int)textDimensions.Width, (int)textDimensions.Height);
            }
            else if (App.CurAppSettings.WatermarkSettings.WatType == Watermark.WatermarkType.GraphicWatermark)
            {
                //For picture watermarks, we need to know the smallest and largest percentages to allow
                //when resizing the graphic according to the aspect ratio of the watermark graphic
				if (!File.Exists(((Graphic)GetWatermarkSettings()).Location))
					return Rectangle.Empty;

				Image imgWatermark = Image.FromFile(((Graphic)GetWatermarkSettings()).Location, true);
                Rectangle croppedBounds = GetCroppedGraphicBounds(imgWatermark);
                
                return new Rectangle(customPosition, new System.Drawing.Size(croppedBounds.Width, croppedBounds.Height));
            }
            return Rectangle.Empty;
        }

        public void GetFrequencyBounds(out int minimum, out int maximum)
        {
            minimum = 1;
            maximum = 150;
            
            if (String.IsNullOrEmpty(this.selectedPhotoPath))
                return;

            //Get the selected photo and resize it if need be
			Image imgSelected = Image.FromFile(this.selectedPhotoPath);

            if (App.CurAppSettings.ResizeMode)
                imgSelected = ResizePic(ref imgSelected, false);

			System.Drawing.Size selectedSize = new System.Drawing.Size(imgSelected.Width, imgSelected.Height);
            Rectangle spacing = GetSpacing();

			System.Drawing.Size minimumPixelSizes =
				new System.Drawing.Size((int)((float)selectedSize.Width * Core.SmallestWatermarkRatio),
                (int)((float)selectedSize.Height * Core.SmallestWatermarkRatio));

            int minimumPixelSize = -1;
            if (minimumPixelSizes.Height < minimumPixelSizes.Width)
                minimumPixelSize = minimumPixelSizes.Height;
            else
                minimumPixelSize = minimumPixelSizes.Width;

            //Get our minimums and maximums
            if (App.CurAppSettings.WatermarkSettings.WatType == Watermark.WatermarkType.TextWatermark
				&& GetWatermarkSettings() != null
                && ((WatText)GetWatermarkSettings()).WatermarkText.Length > 0)
            {
                //For text watermarks, we need to know what is the smallest and largest possible fonts
                //that we could use to legibly put the watermark on the picture
                int textSize = 1;
                Font baseFont = GetFont();
                SizeF textDimensions = SizeF.Empty;
                StringFormat stringFormat = GetAlignmentFormat();

                Bitmap bmPhoto = new Bitmap(selectedSize.Width, selectedSize.Height,
                                     imgSelected.PixelFormat);

                bmPhoto.SetResolution(imgSelected.HorizontalResolution, imgSelected.VerticalResolution);

                Graphics grPhoto = Graphics.FromImage(bmPhoto);
                grPhoto.SmoothingMode = SmoothingMode.AntiAlias;
                grPhoto.DrawImage(
                    imgSelected,
                    new Rectangle(0, 0, selectedSize.Width, selectedSize.Height),
                    0,
                    0,
                    selectedSize.Width,
                    selectedSize.Height,
                    GraphicsUnit.Pixel);

                //Get minimum
				Font tempFont = null;
                while (textDimensions.Width < minimumPixelSizes.Width || textDimensions.Height < minimumPixelSizes.Height)
                {
                    tempFont = new Font(baseFont.FontFamily, textSize++, baseFont.Style);
					textDimensions = grPhoto.MeasureString(((WatText)GetWatermarkSettings()).WatermarkText, tempFont);
                    textDimensions.Width = textDimensions.Width + spacing.Left + spacing.Width;
                    textDimensions.Height = textDimensions.Height + spacing.Top + spacing.Height;
					tempFont.Dispose();
                }
                minimum = textSize;

                //Get maximum
                while (textDimensions.Width < selectedSize.Width && textDimensions.Height < selectedSize.Height)
				{
					tempFont = new Font(baseFont.FontFamily, textSize++, baseFont.Style);
					textDimensions = grPhoto.MeasureString(((WatText)GetWatermarkSettings()).WatermarkText, tempFont);
                    textDimensions.Width = textDimensions.Width + spacing.Left + spacing.Width;
                    textDimensions.Height = textDimensions.Height + spacing.Top + spacing.Height;
					tempFont.Dispose();
                }
                maximum = textSize - 1;
            }
            else if (App.CurAppSettings.WatermarkSettings.WatType == Watermark.WatermarkType.GraphicWatermark
				&& GetWatermarkSettings() != null
                && ((Graphic)GetWatermarkSettings()).Height > 0 && ((Graphic)GetWatermarkSettings()).Width > 0)
            {
                //For picture watermarks, we need to know the smallest and largest percentages to allow
                //when resizing the graphic according to the aspect ratio of the watermark graphic
                if (!File.Exists(((Graphic)GetWatermarkSettings()).Location))
                {
                    minimum = 0;
                    maximum = 0;
                    return;
                }

				Image imgWatermark = Image.FromFile(((Graphic)GetWatermarkSettings()).Location, true);
                Rectangle croppedBounds = GetCroppedGraphicBounds(imgWatermark);
				System.Drawing.Size testBounds = System.Drawing.Size.Empty;
                int percent = 1;

                //Start at current and go small
                while (testBounds.Width < minimumPixelSizes.Width && testBounds.Height < minimumPixelSizes.Height)
                {
                    testBounds.Width = (int)((float)croppedBounds.Width * (float)percent / 100.0f);
                    testBounds.Width = testBounds.Width + spacing.Left + spacing.Width;
                    testBounds.Height = (int)((float)croppedBounds.Height * (float)percent / 100.0f);
                    testBounds.Height = testBounds.Height + spacing.Top + spacing.Height;

                    percent++;
                }

                minimum = percent;

                //Start at current and go large
                while (testBounds.Width < selectedSize.Width && testBounds.Height < selectedSize.Height)
                {
                    testBounds.Width = (int)((float)croppedBounds.Width * (float)percent / 100.0f);
                    testBounds.Width = testBounds.Width + spacing.Left + spacing.Width;
                    testBounds.Height = (int)((float)croppedBounds.Height * (float)percent / 100.0f);
                    testBounds.Height = testBounds.Height + spacing.Top + spacing.Height;

                    percent++;
                }
                maximum = percent - 1;
            }
        }

        private Rectangle GetGraphicBounds()
        {
			if (App.CurAppSettings.WatermarkSettings.WatType == Watermark.WatermarkType.GraphicWatermark)
				return new Rectangle(0, 0, ((Graphic)GetWatermarkSettings()).Width, ((Graphic)GetWatermarkSettings()).Height);
			else
				return Rectangle.Empty;
        }

        private Rectangle GetSpacing()
        {
            return new Rectangle(App.CurAppSettings.WatermarkSettings.SpacingLeft, App.CurAppSettings.WatermarkSettings.SpacingTop, 
                App.CurAppSettings.WatermarkSettings.SpacingWidth, App.CurAppSettings.WatermarkSettings.SpacingHeight);
        }

        private Rectangle GetCroppedGraphicBounds(Image image)
        {
            Rectangle bounds = Rectangle.Empty;
            if (App.CurAppSettings.WatermarkSettings.WatType == Watermark.WatermarkType.GraphicWatermark)
            {
                bounds.X = ((Graphic)GetWatermarkSettings()).CropLeft;
                bounds.Width = image.Width - ((Graphic)GetWatermarkSettings()).CropLeft - ((Graphic)GetWatermarkSettings()).CropRight;
                bounds.Y = ((Graphic)GetWatermarkSettings()).CropTop;
                bounds.Height = image.Height - ((Graphic)GetWatermarkSettings()).CropTop - ((Graphic)GetWatermarkSettings()).CropBottom;
            }
            return bounds;
        }

        private void DrawWatermarkImage(Graphics grPhoto, Image imgWatermark, Point topLeft, ImageAttributes imageAttributes)
        {
            grPhoto.DrawImage(imgWatermark,
                new Rectangle(topLeft.X, topLeft.Y, imgWatermark.Width, imgWatermark.Height),
                0,
                0,
                imgWatermark.Width,
                imgWatermark.Height,
                GraphicsUnit.Pixel,
                imageAttributes);
        }

        private void DrawWatermarkText(Graphics grPhoto, SolidBrush backColor, SolidBrush foreColor, PointF topLeft, StringFormat stringFormat)
        {
            grPhoto.DrawString(((WatText)GetWatermarkSettings()).WatermarkText,
                GetFont(),
                backColor,
                new PointF(topLeft.X + 1, topLeft.Y + 1), stringFormat);

            grPhoto.DrawString(((WatText)GetWatermarkSettings()).WatermarkText,
                GetFont(),
                foreColor,
                topLeft, stringFormat);
        }

        private Point GetImagePosition(Image image, Image imgWatermark)
        {
            const int spacing = 10;

            Point location = new Point(-1, -1);

            //Set the easy left and right X coordinates
            if (App.CurAppSettings.WatermarkSettings.Location == Watermark.WatermarkLocation.TopLeft ||
                App.CurAppSettings.WatermarkSettings.Location == Watermark.WatermarkLocation.CenterLeft ||
                App.CurAppSettings.WatermarkSettings.Location == Watermark.WatermarkLocation.BottomLeft)
            {
                location.X = spacing;
            }
            else if (App.CurAppSettings.WatermarkSettings.Location == Watermark.WatermarkLocation.TopRight ||
                App.CurAppSettings.WatermarkSettings.Location == Watermark.WatermarkLocation.CenterRight ||
                App.CurAppSettings.WatermarkSettings.Location == Watermark.WatermarkLocation.BottomRight)
            {
                location.X = image.Width - imgWatermark.Width - spacing;
            }

            //Set the easy top and bottom Y coordinates
            if (App.CurAppSettings.WatermarkSettings.Location == Watermark.WatermarkLocation.TopLeft ||
                App.CurAppSettings.WatermarkSettings.Location == Watermark.WatermarkLocation.TopCenter ||
                App.CurAppSettings.WatermarkSettings.Location == Watermark.WatermarkLocation.TopRight)
            {
                location.Y = spacing;
            }
            else if (App.CurAppSettings.WatermarkSettings.Location == Watermark.WatermarkLocation.BottomLeft ||
                App.CurAppSettings.WatermarkSettings.Location == Watermark.WatermarkLocation.BottomCenter ||
                App.CurAppSettings.WatermarkSettings.Location == Watermark.WatermarkLocation.BottomRight)
            {
                location.Y = image.Height - imgWatermark.Height - spacing;
            }

            //Take the easy way if we can get out now
            if(location.X > -1 && location.Y > -1)
                return location;

            //If we've gotten this far, then we'll need these calculations
            Point waterCenter = new Point(imgWatermark.Width / 2, imgWatermark.Height / 2);
            Point imageCenter = new Point(image.Width / 2, image.Height / 2);
            
            //Set the X location if we're centering on the X coordinate
            if (App.CurAppSettings.WatermarkSettings.Location == Watermark.WatermarkLocation.TopCenter ||
                App.CurAppSettings.WatermarkSettings.Location == Watermark.WatermarkLocation.CenterCenter ||
                App.CurAppSettings.WatermarkSettings.Location == Watermark.WatermarkLocation.BottomCenter)
            {
                location.X = imageCenter.X - waterCenter.X;
            }

            //Set the Y location if we're centering on the Y coordinate
            if (App.CurAppSettings.WatermarkSettings.Location == Watermark.WatermarkLocation.CenterLeft ||
                App.CurAppSettings.WatermarkSettings.Location == Watermark.WatermarkLocation.CenterCenter ||
                App.CurAppSettings.WatermarkSettings.Location == Watermark.WatermarkLocation.CenterRight)
            {
                location.Y = imageCenter.Y - waterCenter.Y;
            }

            //Add in last case scenario where the image to use as a watermark is bigger than the size of the photo being watermark
            if (location.X < 0)
                location.X = 0;

            if (location.Y < 0)
                location.Y = 0;

            return location;
        }

        private StringFormat GetAlignmentFormat()
        {
            StringFormat stringFormat = new StringFormat();
            if (App.CurAppSettings.WatermarkSettings.Location == Watermark.WatermarkLocation.TopLeft ||
                App.CurAppSettings.WatermarkSettings.Location == Watermark.WatermarkLocation.CenterLeft ||
                App.CurAppSettings.WatermarkSettings.Location == Watermark.WatermarkLocation.BottomLeft)
            {
                stringFormat.Alignment = StringAlignment.Near;
            }
            else if (App.CurAppSettings.WatermarkSettings.Location == Watermark.WatermarkLocation.TopCenter ||
                App.CurAppSettings.WatermarkSettings.Location == Watermark.WatermarkLocation.CenterCenter ||
                App.CurAppSettings.WatermarkSettings.Location == Watermark.WatermarkLocation.BottomCenter)
            {
                stringFormat.Alignment = StringAlignment.Center;
            }
            else if (App.CurAppSettings.WatermarkSettings.Location == Watermark.WatermarkLocation.TopRight ||
                 App.CurAppSettings.WatermarkSettings.Location == Watermark.WatermarkLocation.CenterRight ||
                 App.CurAppSettings.WatermarkSettings.Location == Watermark.WatermarkLocation.BottomRight)
            {
                stringFormat.Alignment = StringAlignment.Far;
            }

            if (App.CurAppSettings.WatermarkSettings.Location == Watermark.WatermarkLocation.TopLeft ||
                App.CurAppSettings.WatermarkSettings.Location == Watermark.WatermarkLocation.TopCenter ||
                App.CurAppSettings.WatermarkSettings.Location == Watermark.WatermarkLocation.TopRight)
                stringFormat.LineAlignment = StringAlignment.Near;
            else if (App.CurAppSettings.WatermarkSettings.Location == Watermark.WatermarkLocation.CenterLeft ||
                App.CurAppSettings.WatermarkSettings.Location == Watermark.WatermarkLocation.CenterCenter ||
                App.CurAppSettings.WatermarkSettings.Location == Watermark.WatermarkLocation.CenterRight)
                stringFormat.LineAlignment = StringAlignment.Center;
            else if (App.CurAppSettings.WatermarkSettings.Location == Watermark.WatermarkLocation.BottomLeft ||
                 App.CurAppSettings.WatermarkSettings.Location == Watermark.WatermarkLocation.BottomCenter ||
                 App.CurAppSettings.WatermarkSettings.Location == Watermark.WatermarkLocation.BottomRight)
                stringFormat.LineAlignment = StringAlignment.Far;
            return stringFormat;
            
        }

        private PointF GetTextCoordinates(SizeF textSize, SizeF imageSize)
        {
            PointF coordinate = new PointF();
            coordinate.Y = ApplyVerticalAlignment((int)textSize.Height, (int)imageSize.Height);
            coordinate.X = ApplyHorizontalAlignment((int)textSize.Width, (int)imageSize.Width);
            return coordinate;
        }

        private float ApplyHorizontalAlignment(int textWidth, int imageWidth)
        {
            if (App.CurAppSettings.WatermarkSettings.Location == Watermark.WatermarkLocation.TopLeft ||
                App.CurAppSettings.WatermarkSettings.Location == Watermark.WatermarkLocation.CenterLeft ||
                App.CurAppSettings.WatermarkSettings.Location == Watermark.WatermarkLocation.BottomLeft)
                return 5.0f;
            else if (App.CurAppSettings.WatermarkSettings.Location == Watermark.WatermarkLocation.TopCenter ||
                App.CurAppSettings.WatermarkSettings.Location == Watermark.WatermarkLocation.CenterCenter ||
                App.CurAppSettings.WatermarkSettings.Location == Watermark.WatermarkLocation.BottomCenter)
                return (float)((float)imageWidth / 2.0);
            else if (App.CurAppSettings.WatermarkSettings.Location == Watermark.WatermarkLocation.TopRight ||
                 App.CurAppSettings.WatermarkSettings.Location == Watermark.WatermarkLocation.CenterRight ||
                 App.CurAppSettings.WatermarkSettings.Location == Watermark.WatermarkLocation.BottomRight)
                return (float)((float)imageWidth - 5.0);
            return 5.0f;
        }

        private float ApplyVerticalAlignment(int textHeight, int imageHeight)
        {
            if (App.CurAppSettings.WatermarkSettings.Location == Watermark.WatermarkLocation.TopLeft ||
                 App.CurAppSettings.WatermarkSettings.Location == Watermark.WatermarkLocation.TopCenter ||
                 App.CurAppSettings.WatermarkSettings.Location == Watermark.WatermarkLocation.TopRight)
                return 5.0f;
            else if (App.CurAppSettings.WatermarkSettings.Location == Watermark.WatermarkLocation.CenterLeft ||
                 App.CurAppSettings.WatermarkSettings.Location == Watermark.WatermarkLocation.CenterCenter ||
                 App.CurAppSettings.WatermarkSettings.Location == Watermark.WatermarkLocation.CenterRight)
                return (float)((float)imageHeight / 2.0);
            else if (App.CurAppSettings.WatermarkSettings.Location == Watermark.WatermarkLocation.BottomLeft ||
                 App.CurAppSettings.WatermarkSettings.Location == Watermark.WatermarkLocation.BottomCenter ||
                 App.CurAppSettings.WatermarkSettings.Location == Watermark.WatermarkLocation.BottomRight)
                return (float)((float)imageHeight - 5.0);
            return 5.0f;
        }

        private string GetExtension(string path)
        {
            int period = path.IndexOf('.');
            path = path.Substring(period, path.Length - period);
			if (App.CurAppSettings.ImageAdjSettings.UseLowerCaseExtensions)
				path = path.ToLower();
			else
				path = path.ToUpper();
            return path;
        }

        public void SaveFile(ref Image image, string path)
        {
            ImageCodecInfo myImageCodecInfo;
            System.Drawing.Imaging.Encoder myEncoder;
            EncoderParameter myEncoderParameter;
            EncoderParameters myEncoderParameters;

            try
            {
				if (App.CurAppSettings.ImageAdjSettings.Format == "Default")
					myImageCodecInfo = GetEncoderInfo(GetImageFormatString(this.format));
				else					
					myImageCodecInfo = GetEncoderInfo(GetMediaString());
            }
            catch
            {
                myImageCodecInfo = GetEncoderInfo("image/jpeg");
            }

            // Create an Encoder object based on the GUID

            // for the Quality parameter category.
            myEncoder = System.Drawing.Imaging.Encoder.Quality;
            myEncoderParameters = new EncoderParameters(1);

            myEncoderParameter = new EncoderParameter(myEncoder, (long)App.CurAppSettings.ImageAdjSettings.ImageQuality);
            myEncoderParameters.Param[0] = myEncoderParameter;
            //save new image to file system.
			if (File.Exists(path))
			{
				string temp = Path.GetFileName(path);
				temp = temp.Insert(0, "_");
				image.Save(Path.Combine(Path.GetDirectoryName(path), temp), myImageCodecInfo, myEncoderParameters);
				image.Dispose();
				image = null;
				this.SelectedPhotoPath = String.Empty;
				File.Delete(path);
				File.Move(Path.Combine(Path.GetDirectoryName(path), temp), path);
				System.Diagnostics.Debug.WriteLine("File exists, but now moved: " + path);
			}
			else
			{
				System.Diagnostics.Debug.WriteLine("Attempting to save regularly: " + path);
				image.Save(path, myImageCodecInfo, myEncoderParameters);
				System.Diagnostics.Debug.WriteLine("Save successful: " + path);
				image.Dispose();
				image = null;
			}
            GC.Collect();
        }

		private ImageFormat GetImageFormat(string format)
		{
			if (format == "image/jpeg")
				return ImageFormat.Jpeg;
			else if (format == "image/png")
				return ImageFormat.Png;
			else if (format == "image/gif")
				return ImageFormat.Gif;
			else if (format == "image/bmp")
				return ImageFormat.Bmp;
			else if (format == "image/tiff")
				return ImageFormat.Tiff;
			else if (format == "image/exif")
				return ImageFormat.Exif;
			else if (format == "image/wmf")
				return ImageFormat.Wmf;
			else if (format == "image/memorybmp")
				return ImageFormat.MemoryBmp;
			else
				return ImageFormat.Jpeg;
		}

		private string GetImageFormatString(ImageFormat format)
		{
			if (format == ImageFormat.Jpeg)
				return "image/jpeg";
			else if (format == ImageFormat.Png)
				return "image/png";
			else if (format == ImageFormat.Gif)
				return "image/gif";
			else if (format == ImageFormat.Bmp)
				return "image/bmp";
			else if (format == ImageFormat.Tiff)
				return "image/tiff";
			else if (format == ImageFormat.Exif)
				return "image/exif";
			else if (format == ImageFormat.Wmf)
				return "image/wmf";
			else if (format == ImageFormat.MemoryBmp)
				return "image/memorybmp";
			else
				return "image/jpeg";
		}

		private string GetMediaString()
		{
			return App.CurAppSettings.ImageAdjSettings.Format;
		}

        private static ImageCodecInfo GetEncoderInfo(String mimeType)
        {
            int j;
            ImageCodecInfo[] encoders;
            encoders = ImageCodecInfo.GetImageEncoders();
            for (j = 0; j < encoders.Length; ++j)
            {
                if (encoders[j].MimeType == mimeType)
                    return encoders[j];
            }
            return null;
        }

        public Image GetOrganizedImage(int imageIndex)
        {
            DirectoryInfo di = new DirectoryInfo(App.CurAppSettings.FolderSelectSettings.SourceFolder);
            FileInfo[] rgFiles = di.GetFiles("*.*");
            Image image = null;
            if (imageIndex <= rgFiles.Length)
            {
				image = Image.FromFile(rgFiles[imageIndex].FullName);
                if (App.CurAppSettings.ResizeMode)
                {
                    if (App.CurAppSettings.ResizeSettings.Choice == 0)
                        image = ScaleByPercent(ref image, (int)App.CurAppSettings.ResizeSettings.Percent, true);
                    else
                        image = ScaleByBound(ref image, App.CurAppSettings.ResizeSettings.BoundWidth, App.CurAppSettings.ResizeSettings.BoundHeight, true);
                }

                if (App.CurAppSettings.WatermarkMode)
                {
                    image = WatermarkPic(image);
                }
            }
            return image;
        }

        private bool ZipImages()
        {
            this.ProgressTitle = "Compressing photos";
            string zipPath;
            if (App.CurAppSettings.CompressSettings.UseDefault)
                zipPath = CompressControl.GetDefaultPath();
            else
                zipPath = App.CurAppSettings.CompressSettings.ZipFile;
            if(File.Exists(zipPath))
                File.Delete(zipPath);

			if (zipPath == String.Empty)
			{
				zipPath = this.destination + Core.Organized + "compressedPhotos.zip";
			}

            DirectoryInfo info = new DirectoryInfo(this.destination + Core.Organized);
            ZipOutputStream zip = new ZipOutputStream(File.Create(zipPath));
            zip.SetLevel(App.CurAppSettings.CompressSettings.Level);
            zip.Password = App.CurAppSettings.CompressSettings.Password;

            FileInfo[] files = info.GetFiles();
            ZipEntry entry = null;
            byte[] buffer = new byte[2048];
            int index = 1;
            foreach (FileInfo fileInfo in files)
            {
                try
                {
                    entry = new ZipEntry(fileInfo.Name);
                    entry.DateTime = DateTime.Now;
                    entry.Comment = fileInfo.Name;
                    entry.ZipFileIndex = index;
                    using (FileStream fs = File.OpenRead(fileInfo.FullName))
                    {
                        this.ProgressDetails = fileInfo.Name;
                        entry.Size = fs.Length;
                        zip.PutNextEntry(entry);
                        int sourceBytes;
                        do
                        {
                            sourceBytes = fs.Read(buffer, 0, buffer.Length);
                            zip.Write(buffer, 0, sourceBytes);
                        }
                        while (sourceBytes > 0);

                    }
                }
                catch (IOException ex)
                {
                    continue;
                }

				if (this.organizingThread.CancellationPending)
					return false;
            }
            zip.Finish();
            zip.Close();
            this.ProgressDetails = "Finished compressing photos.";
			return true;
        }

        private string GetDestinationName()
        {
            int index = this.destination.Length - 1;
            char slash = this.destination[index];
            if (slash == '\\')
            {
                slash = this.destination[--index];
            }
            int count = 0;
            while (slash != '\\')
            {
                slash = this.destination[index--];
                count++;
            }
            return this.destination.Substring(index + 1, count);
        }

        private bool UploadPictures()
        {
            bool ret = false;
			string title = "Uploading images";
            switch(App.Uploaders.GetUpload(App.CurAppSettings.UploadDetails.Name).GetMethod())
            {
				case Trebuchet.Settings.UploadSettings.UploadType.Ftp:
					this.ProgressTitle = title + " via FTP";
                    ret =  UploadUsingFtp();
                    break;
				case Trebuchet.Settings.UploadSettings.UploadType.Facebook:
					this.ProgressTitle = title + " via FaceBook";
					ret = UploadUsingFaceBook();
                    break;
				case Trebuchet.Settings.UploadSettings.UploadType.Flickr:
					this.ProgressTitle = title + " via Flickr";
					ret = UploadUsingFlickr();
                    break;
				case Trebuchet.Settings.UploadSettings.UploadType.Picasa:
					this.ProgressTitle = title + " via Picasa";
					ret = UploadUsingPicasa();
                    break;
				case Trebuchet.Settings.UploadSettings.UploadType.Email:
					this.ProgressTitle = title + " via Email";
					ret = UploadUsingEmail();
					break;
                default:
                    break;
            }
            return ret;
        }

		private bool UploadUsingEmail()
		{
			try
			{
				UploadBase uploadSettings = App.Uploaders.GetUpload(App.CurAppSettings.UploadDetails.Name);
				Email emailSettings = uploadSettings as Email;
				if (emailSettings == null)
					return false;

				List<string> files = new List<string>();
				if (emailSettings.AttachMode == Email.AttachModes.Compressed)
				{
					if (!App.CurAppSettings.CompressMode)
					{
						string temp = this.ProgressTitle;
						ZipImages();
						this.ProgressTitle = temp;
					}
					if (App.CurAppSettings.CompressSettings.ZipFile == String.Empty)
						files.Add(this.destination + Core.Organized + "compressedPhotos.zip");
					else
						files.Add(App.CurAppSettings.CompressSettings.ZipFile);
				}
				else
				{
					if (Directory.Exists(this.destination + Core.Organized))
					{
						string temp = "";
						for (int x = 0; x < this.paths.Count && !this.organizingThread.CancellationPending; x++)
						{
							temp = GetFileName(x) + GetExtension(this.paths[x]);
							this.ProgressDetails = "Attaching: " + temp;
							files.Add(this.destination + Core.Organized + temp);
						}

						this.ProgressDetails = "Finished attaching photos...";
						if (this.organizingThread.CancellationPending)
							return false;
					}
				}
				this.progressDetails = "Creating email, this may take a few minutes.";

				string album = String.Empty;
				if (App.CurAppSettings.UploadDetails.UseDefaultAlbum)
					album = App.CurAppSettings.FolderSelectSettings.Album;
				else
					album = App.CurAppSettings.UploadDetails.Album;

				MAPI.SendMail(files.ToArray(), album, App.CurAppSettings.UploadDetails.Description);
				return true;
			}
			catch (Exception e)
			{
				return false;
			}
		}

		private bool UploadUsingFaceBook()
		{
            //Facebook API changed - removed functionality

			//if (!App.TheCord.CheckUploader(2))
			//{
			//    MessageBox.Show("Upload failed.  Please visit https://github.com/Carpe-Tempestas/photoding for details.");
			//    return false;
			//}

			//FacebookService facebook = new Facebook.Components.FacebookService();
			//facebook.ApplicationKey = "9af06a1d94bb82cc78b3a3a5ffb56a46";
			//facebook.Secret = "0bb7fa5cf80eca9bd1eadae64f10e217";
			//UploadBase uploadSettings = App.Uploaders.GetUpload(App.CurAppSettings.UploadDetails.Name);
			//FaceBook facebookSettings = uploadSettings as FaceBook;
			//if (facebookSettings == null)
			//    return false;
			//if(!facebookSettings.SessionExpires && !String.IsNullOrEmpty(facebookSettings.SessionKey))
			//    facebook.SessionKey = facebookSettings.Request(facebookSettings.SessionKey);

			//try
			//{
			//    facebook.GetLoggedInUser();
			//}
			//catch(Exception e)
			//{
			//    facebook.SessionKey = "";
			//    facebookSettings.SessionKey = "";
			//    facebookSettings.SessionExpires = true;
			//}

			//if (Directory.Exists(this.destination + Core.Organized))
			//{
			//    string temp = "";
			//    FileInfo info = null;
			//    string album = String.Empty;
			//    Album facebookAlbum = null;
				
			//    if (App.CurAppSettings.UploadDetails.UseDefaultAlbum)
			//        album = App.CurAppSettings.FolderSelectSettings.Album;
			//    else
			//        album = App.CurAppSettings.UploadDetails.Album;

			//    facebookAlbum = FaceBookAlbumExists(facebook, album);
			//    if (facebookAlbum == null)
			//        facebookAlbum = facebook.CreateAlbum(album, App.CurAppSettings.UploadDetails.Location, App.CurAppSettings.UploadDetails.Description);
				
			//    for(int x = 0; x < this.paths.Count && !this.organizingThread.CancellationPending; x++)
			//    {
			//        temp = GetFileName(x) + GetExtension(this.paths[x]);
			//        info = new FileInfo(this.destination+Core.Organized+temp.Remove(0,1));
			//        this.ProgressDetails = "Uploading: " + temp;
			//        facebook.UploadPhoto(facebookAlbum.AlbumId, info);
			//    }

			//    facebookSettings.SessionExpires = facebook.SessionExpires;
			//    facebookSettings.SessionKey = facebookSettings.Tell(facebook.SessionKey);
			//    App.Uploaders.UpdateUpload(facebookSettings);

			//    this.ProgressDetails = "Finished uploading photos...";
			//    if (this.organizingThread.CancellationPending)
			//        return false;
			//    else
			//        return true;
			//}
			return false;
		}

		//private Album FaceBookAlbumExists(FacebookService facebook, string albumName)
		//{
		//    Collection<Album> albums = facebook.GetPhotoAlbums();
		//    foreach (Album album in albums)
		//    {
		//        if (album.Name == albumName)
		//            return album;
		//    }
		//    return null;
		//}

        private bool UploadUsingFtp()
        {
            UploadBase uploadSettings = App.Uploaders.GetUpload(App.CurAppSettings.UploadDetails.Name);
            Ftp ftpSettings = uploadSettings as Ftp;
            if (ftpSettings == null)
                return false;

            FTPConnection ftpConn = new FTPConnection();
            ftpConn.ServerAddress = ftpSettings.ServerAddress;
            ftpConn.ServerPort = ftpSettings.ServerPort;
            ftpConn.ConnectMode = FTPConnectMode.PASV;
            ftpConn.UserName = ftpSettings.Username;

			if (ftpSettings.Password == String.Empty)
			{
				PassDialog pass = new PassDialog("FTP");
				if (pass.ShowDialog() == DialogResult.OK)
				{
					ftpConn.Password = pass.GetPass();
				}
				else
				{
					MessageBox.Show("Uploading cannot continue without a valid password.");
					return false;
				}
			}
			else
			{
				ftpConn.Password = ftpSettings.Request(ftpSettings.Password);
			}

			string uploadPath = App.CurAppSettings.UploadDetails.UploadPath;
			if (App.CurAppSettings.UploadDetails.AppendAlbum)
			{
				if (App.CurAppSettings.UploadDetails.UseDefaultAlbum)
					uploadPath = uploadPath + "/" + App.CurAppSettings.FolderSelectSettings.Album;
				else
					uploadPath = uploadPath + "/" + App.CurAppSettings.UploadDetails.Album;
			}

            try
            {
                ftpConn.Connect();
				try
				{
					ftpConn.ChangeWorkingDirectory(uploadPath);
				}
				catch(Exception e)
				{
					ftpConn.CreateDirectory(uploadPath);
					ftpConn.ChangeWorkingDirectory(uploadPath);
				}

                if (Directory.Exists(this.destination + Core.Organized))
                {
                    string name = "";
					string temp = "";
                    for (int x = 0; x < this.paths.Count && !this.organizingThread.CancellationPending; x++)
                    {
                        name = GetFileName(x) + GetExtension(this.paths[x]);
						if (name.StartsWith("\\"))
							temp = name.Remove(0, 1);
						else
							temp = name;
                        this.ProgressDetails = "Uploading: " + temp;
                        ftpConn.UploadFile(this.destination + Core.Organized + name,
                            uploadPath + "/" + temp);
                    }
					
					this.ProgressDetails = "Finished uploading photos...";
					if (this.organizingThread.CancellationPending)
						return false;
					else
						return true;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            return false;
        }

		private bool UploadUsingFlickr()
		{
			UploadBase uploadSettings = App.Uploaders.GetUpload(App.CurAppSettings.UploadDetails.Name);
			Trebuchet.Settings.UploadTypes.Flickr flickrSettings = uploadSettings as Trebuchet.Settings.UploadTypes.Flickr;
			if (flickrSettings == null)
				return false;

			FlickrNet.Flickr flickr = new FlickrNet.Flickr();			
			flickr.ApiKey = "Needs new keys and secrets";
            flickr.ApiKey = "Needs new keys and secrets";
            flickr.ApiSecret = "Needs new keys and secrets";
            flickr.ApiSecret = "Needs new keys and secrets";

			string frob = "";
			Auth auth = null;

			if (String.IsNullOrEmpty(flickrSettings.Token))
			{
				CreateNewFlickrSettings(flickr, flickrSettings, ref frob, ref auth);
			}
			else
			{
				try
				{
					auth = flickr.AuthCheckToken(flickrSettings.Request(flickrSettings.Token));
					if (auth == null || auth.Token != flickrSettings.Request(flickrSettings.Token))
					{
						CreateNewFlickrSettings(flickr, flickrSettings, ref frob, ref auth);
					}
				}
				catch (Exception e)
				{
					CreateNewFlickrSettings(flickr, flickrSettings, ref frob, ref auth);
				}
			}
			flickr.AuthToken = auth.Token;

			if (Directory.Exists(this.destination + Core.Organized))
			{
				string temp = "";
				FileInfo info = null;
				string album = String.Empty;
				Photoset flickrAlbum = null;

				if (App.CurAppSettings.UploadDetails.UseDefaultAlbum)
					album = App.CurAppSettings.FolderSelectSettings.Album;
				else
					album = App.CurAppSettings.UploadDetails.Album;

				try
				{
					flickrAlbum = FlickrAlbumExists(flickr, album);
				}
				catch (Exception e)
				{
					System.Diagnostics.Debug.WriteLine("Exception: " + e.Message);
				}
				try
				{
					if (flickrAlbum == null)
						flickrAlbum = flickr.PhotosetsCreate(album, App.CurAppSettings.UploadDetails.Description, null);
				}
				catch (Exception ex)
				{
					System.Diagnostics.Debug.WriteLine("Exception: " + ex.Message);
				}

				List<string> photoIDs = new List<string>();

				try
				{
					for (int x = 0; x < this.paths.Count && !this.organizingThread.CancellationPending; x++)
					{
						temp = GetFileName(x) + GetExtension(this.paths[x]);
						if (temp.StartsWith("\\"))
							temp = temp.Remove(0, 1);
						info = new FileInfo(this.destination + Core.Organized + temp);
						this.ProgressDetails = "Uploading: " + temp;
						photoIDs.Add(flickr.UploadPicture(info.FullName, temp, "", "", GetFlickrPrivacy(flickrSettings), flickrSettings.AllowFamily, flickrSettings.AllowFriends));
					}

					for (int x = 0; x < this.paths.Count && !this.organizingThread.CancellationPending; x++)
					{
						this.ProgressDetails = "Adding to album: " + temp;
						if (flickrAlbum == null)
						{
							flickrAlbum = flickr.PhotosetsCreate(album, App.CurAppSettings.UploadDetails.Description, photoIDs[0]);
						}
						else
						{
							flickr.PhotosetsAddPhoto(flickrAlbum.PhotosetId, photoIDs[x]);
						}
					}
					if (this.organizingThread.CancellationPending)
						return false;
				}
				catch (Exception e)
				{
					System.Diagnostics.Debug.WriteLine("Exception: " + e.Message);
				}
				this.ProgressDetails = "Finished uploading photos...";
				return true;
			}
			return false;
		}

		private bool GetFlickrPrivacy(Trebuchet.Settings.UploadTypes.Flickr flickrSettings)
		{
			if (flickrSettings.Privacy == Trebuchet.Settings.UploadTypes.Flickr.FlickrPrivacyType.Public)
				return true;
			else
				return false;
		}

		private Photoset FlickrAlbumExists(FlickrNet.Flickr flickr, string album)
		{
			Photosets photosets = flickr.PhotosetsGetList();
			foreach (Photoset photoset in photosets.PhotosetCollection)
			{
				if (photoset.Title == album)
					return photoset;
			}
			return null;
		}

		private void CreateNewFlickrSettings(FlickrNet.Flickr flickr, Trebuchet.Settings.UploadTypes.Flickr flickrSettings, ref string frob, ref Auth auth)
		{
			frob = flickr.AuthGetFrob();
			string url = flickr.AuthCalcUrl(frob, AuthLevel.Write);
			System.Diagnostics.Process.Start(url);
			System.Threading.Thread.Sleep(5000);
			auth = flickr.AuthGetToken(frob);
			flickrSettings.Token = flickrSettings.Tell(auth.Token);
			flickrSettings.UserID = auth.User.UserId;
			if (auth.Permissions == AuthLevel.Write)
				flickrSettings.WriteAccess = true;
			else
				flickrSettings.WriteAccess = false;
			App.Uploaders.UpdateUpload(flickrSettings);
		}

		private bool UploadUsingPicasa()
		{
			if (Directory.Exists(this.destination + Core.Organized))
			{
				Picasa picasaSettings = App.Uploaders.GetUpload(App.CurAppSettings.UploadDetails.Name) as Picasa;

				try
				{
					string album = String.Empty;

					if (App.CurAppSettings.UploadDetails.UseDefaultAlbum)
						album = App.CurAppSettings.FolderSelectSettings.Album;
					else
						album = App.CurAppSettings.UploadDetails.Album;

					string name = String.Empty;
					string temp = String.Empty;
					PicasaService service = new PicasaService("photoding");
					((GDataRequestFactory)service.RequestFactory).KeepAlive = false;

					if (picasaSettings.Password == String.Empty)
					{
						PassDialog pass = new PassDialog("Picasa");
						if (pass.ShowDialog() == DialogResult.OK)
						{
							service.setUserCredentials(picasaSettings.Username, pass.GetPass());
						}
						else
						{
							MessageBox.Show("Uploading cannot continue without a valid password.");
							return false;
						}
					}
					else
					{
						service.setUserCredentials(picasaSettings.Username, picasaSettings.Request(picasaSettings.Password));
					}

					string uriText = "http://picasaweb.google.com/data/feed/api/user/default/";
					Uri uri = new Uri(uriText); 

					PicasaEntry returnedPhoto = null;
					FileStream fs = null;
					AlbumEntry albumEntry = new AlbumEntry();
					int retries = 0;
					album = album.Replace(" ", "");
					albumEntry.Title = new AtomTextConstruct(AtomTextConstructElementType.Title, album);
					albumEntry.Summary = new AtomTextConstruct(AtomTextConstructElementType.Summary, App.CurAppSettings.UploadDetails.Description);
					
					AlbumAccessor accessor = new AlbumAccessor(albumEntry);
					if (picasaSettings.Privacy == Picasa.PicasaPrivacyType.Private)
						accessor.Access = "private";
					else
						accessor.Access =  "public";
					AtomEntry atomEntry = service.Insert(uri, albumEntry);	
					uri = new Uri(uriText + "album/" + album);				
					
					for (int x = 0; x < this.paths.Count; x++)
					{
						name = GetFileName(x) + GetExtension(this.paths[x]);
						if (name.StartsWith("\\"))
							temp = name.Remove(0, 1);
						else
							temp = name;
						fs = File.OpenRead(this.destination + Core.Organized + name);
						this.ProgressDetails = "Uploading: " + temp;
						returnedPhoto = null;
						while (returnedPhoto == null && retries < 10 && !this.organizingThread.CancellationPending)
						{
							try
							{
								returnedPhoto = service.Insert(uri, fs, GetImageFormatString(this.format), this.paths[x]) as PicasaEntry;
								returnedPhoto.Title = new AtomTextConstruct(AtomTextConstructElementType.Title, temp);
								returnedPhoto.Update();
								retries = 0;
								System.Diagnostics.Debug.WriteLine("Picasa: Uploading success!");
							}
							catch (Exception ex)
							{
								retries++;
								System.Diagnostics.Debug.WriteLine(ex.Message);
							}
						}
						if (fs != null)
							fs.Close();

						if (retries == 10)
							throw new Exception("Can't upload");
					}
				}
				catch (Exception e)
				{
					System.Diagnostics.Debug.WriteLine("Exception: " + e.Message);
				}
				this.ProgressDetails = "Finished uploading photos...";
				if (this.organizingThread.CancellationPending)
					return false;
				else
					return true;
			}
			return false;
		}
    }
}
