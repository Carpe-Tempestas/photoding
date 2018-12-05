using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;
using Trebuchet.Settings;
using System.Xml.Serialization;

namespace Trebuchet
{
	class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main(string[] args)
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			try
			{
				if (args.Length > 0)
				{
					if (args[0].Length > 0 && args[0][1] == ':')
					{
						string drive = args[0].Replace('\"', '\\');
						if (Directory.Exists(drive))
							Application.Run(new App(drive));
						else
							Application.Run(new App());
					}
					else
					{
						Application.Run(new App());
					}
				}
				else
					Application.Run(new App());
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
			}
		}
	}
}