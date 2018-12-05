using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Runtime.InteropServices;
using System.IO;
using System.Diagnostics;
using Microsoft.Win32;
using System.Windows.Forms;
using System.Collections;
using Trebuchet;

namespace Trebuchet
{
	public class DropTargetInstaller
	{
		public bool Install(Assembly exe, string entryPointTypeName, string desiredHandlerName, string iconPath, string actionText, string usingText)
		{

			Type t = exe.GetType(entryPointTypeName);

			object[] attributes = t.GetCustomAttributes(false);

			GuidAttribute guid = GetGUID(attributes);



			//Throws ArgumentExceptions to caller if the Assembly/class does not meet requirements

			VerifySuitability(entryPointTypeName, t, attributes, guid);

			string guidVal = guid.Value;



			Regasm(exe);

			WriteRegistryValues(desiredHandlerName, actionText, iconPath, entryPointTypeName, usingText, guidVal);



			return true;

		}



		/// <summary>

		/// Suitable classes must Implement the COM IDropTarget interface, be marked COMVisible, and contain a valid GUID attribute

		/// </summary>

		/// <param name="entryPointTypeName"></param>

		/// <param name="t"></param>

		/// <param name="attributes"></param>

		/// <param name="guid"></param>

		private void VerifySuitability(string entryPointTypeName, Type t, object[] attributes, GuidAttribute guid)
		{

			if (!VerifyDropTarget(t))
			{

				string msg = string.Format("Type {0} does not implement IDropTarget", entryPointTypeName);

				throw new ArgumentException(msg);

			}

			//

			if (!VerifyCOMVisible(attributes))
			{

				string msg = string.Format("Type {0} is missing COMVisble attribute", entryPointTypeName);

				throw new ArgumentException(msg);

			}

			//                        

			if (null == guid)
			{

				string msg = string.Format("Type {0} is missing Guid attribute", entryPointTypeName);

				throw new ArgumentException(msg);

			}

		}



		/// <summary>

		/// 

		/// </summary>

		/// <param name="exe"></param>

		protected void Regasm(Assembly exe)
		{

			string regasmExe = @"Microsoft.NET\Framework\v2.0.50727\RegAsm.exe";

			string regPath = Path.Combine(@"c:\windows\", regasmExe);



			//RegAsm does not support URI format (file:////c:blahblah

			ProcessStartInfo info = new ProcessStartInfo(regPath, "/codebase " + exe.CodeBase.Replace("file:///", string.Empty));

			info.RedirectStandardOutput = true;

			info.RedirectStandardError = true;

			info.UseShellExecute = false;



			Process p = Process.Start(info);

			using (StreamReader stdOut = p.StandardOutput)
			{

				using (StreamReader stdErr = p.StandardError)
				{

					p.WaitForExit();

					string output = stdOut.ReadToEnd();

					string errput = stdErr.ReadToEnd();

					//Do error logging here if stdErr is not blank, etc.

					int code = p.ExitCode;

				}

			}

		}



		/// <summary>

		/// 

		/// </summary>

		/// <param name="desiredHandlerName"></param>

		/// <param name="actionName"></param>

		/// <param name="iconPath"></param>

		/// <param name="entryPointName"></param>

		/// <param name="usingString"></param>

		/// <param name="guid"></param>

		protected void WriteRegistryValues(string desiredHandlerName, string actionName, string iconPath,

			string entryPointName, string usingString, string guid)
		{

			WriteHandlerRoot(desiredHandlerName, actionName, iconPath, entryPointName, usingString);



			WritePhotosOnArrival(desiredHandlerName);



			WriteDropTarget(entryPointName, guid);

		}



		private static void WriteDropTarget(string entryPointName, string guid)
		{

			RegistryKey classesRoot = Registry.ClassesRoot;

			if (CheckForExistance(classesRoot.GetSubKeyNames(), entryPointName))
			{
				RegistryKey invokeProgId = classesRoot.OpenSubKey(entryPointName, true);

				if (CheckForExistance(invokeProgId.GetSubKeyNames(), "shell"))//Start over
				{

					invokeProgId.DeleteSubKeyTree("shell");

				}

				RegistryKey shell = invokeProgId.CreateSubKey("shell");

				RegistryKey open = shell.CreateSubKey("open");

				RegistryKey dropTarget = open.CreateSubKey("DropTarget");

				dropTarget.SetValue("clsid", "{" + guid + "}");

				dropTarget.Close();

				open.Close();

				shell.Close();

				invokeProgId.Close();

			}

			else
			{

				//Regasm did not run

			}

			//

			classesRoot.Close();

		}

		private static bool CheckForExistance(string[] array, string search)
		{
			foreach (string text in array)
			{
				if (text == search)
					return true;
			}
			return false;
		}



		private static void WritePhotosOnArrival(string desiredHandlerName)
		{

			RegistryKey photosOnArrival =

				Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\explorer\AutoPlayHandlers\EventHandlers\ShowPicturesOnArrival", true);

			if (!CheckForExistance(photosOnArrival.GetValueNames(), desiredHandlerName))
			{

				photosOnArrival.SetValue(desiredHandlerName, string.Empty);

			}

			photosOnArrival.Close();

		}



		private static void WriteHandlerRoot(string desiredHandlerName, string actionName, string iconPath, string entryPointName, string usingString)
		{

			RegistryKey handlerRoot = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\explorer\AutoPlayHandlers\Handlers", true);

			if (CheckForExistance(handlerRoot.GetSubKeyNames(), desiredHandlerName))
			{

				handlerRoot.DeleteSubKey(desiredHandlerName);

			}

			RegistryKey handler = handlerRoot.CreateSubKey(desiredHandlerName);

			handler.SetValue("Action", actionName);

			handler.SetValue("DefaultIcon", iconPath);

			handler.SetValue("InvokeProgID", entryPointName);

			handler.SetValue("InvokeVerb", "open");

			handler.SetValue("Provider", usingString);

			handler.Close();

			handlerRoot.Close();

		}





		protected bool VerifyDropTarget(Type t)
		{

			Type[] interfaceTypes = t.GetInterfaces();

			bool dropTarg = CheckForTypeExistance(interfaceTypes, typeof(IDropTarget));

			return dropTarg;

		}

		private bool CheckForTypeExistance(Type[] interfaceTypes, Type type)
		{
			foreach (Type check in interfaceTypes)
			{
				if (check == type)
					return true;
			}
			return false;
		}



		protected bool VerifyCOMVisible(object[] attributes)
		{

			for (int i = 0; i < attributes.Length; ++i)
			{

				if (attributes[i] is ComVisibleAttribute)
				{

					return true;

				}

			}

			return false;

		}



		protected GuidAttribute GetGUID(object[] attributes)
		{

			GuidAttribute g = null;



			for (int i = 0; i < attributes.Length; ++i)
			{

				if (attributes[i] is GuidAttribute)
				{

					return (GuidAttribute)attributes[i];

				}

			}



			return g;

		}

	}

}
