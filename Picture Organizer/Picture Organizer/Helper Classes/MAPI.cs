using System;
using System.Runtime.InteropServices;
using System.IO;

namespace Trebuchet.Helper_Classes
{
	class MAPI
	{
		private const int MAPI_LOGON_UI = 0x00000001;
		private const int MAPI_DIALOG = 0x00000008;

		public static int SendMail(string[] aAttachments, string sSubject, string sBody)
		{
			IntPtr ptrSession = new IntPtr(0);
			IntPtr ptrWinHandle = new IntPtr(0);
			int iFiles = 0;
			int iFilePtr = 0;

			// Create a message
			MapiMessage msg = new MapiMessage();
			msg.subject = sSubject;
			msg.noteText = sBody;

			// Get the number of files
			iFiles = aAttachments.GetLength(0);
			MapiFileDesc[] fileDesc = new MapiFileDesc[iFiles];

			int iSizeofMapiDesc = Marshal.SizeOf(typeof(MapiFileDesc)) * iFiles;
			IntPtr ptrMapiDesc = Marshal.AllocHGlobal(iSizeofMapiDesc);

			foreach (string sAttachment in aAttachments)
			{
				fileDesc[iFilePtr] = new MapiFileDesc();
				fileDesc[iFilePtr].position = -1;
				int ptr = (int)ptrMapiDesc + (iFilePtr * Marshal.SizeOf(typeof(MapiFileDesc)));
				string sPath = sAttachment;
				fileDesc[iFilePtr].name = Path.GetFileName(sPath);
				fileDesc[iFilePtr].path = sPath;
				Marshal.StructureToPtr(fileDesc[iFilePtr], (IntPtr)ptr, false);

				iFilePtr++;
			}

			msg.files = ptrMapiDesc;
			msg.fileCount = iFiles;

			return (MAPISendMail(ptrSession, ptrWinHandle, msg, MAPI_LOGON_UI | MAPI_DIALOG, 0));
		}
		[DllImport("MAPI32.DLL")]
		private static extern int MAPISendMail(IntPtr sess, IntPtr hwnd, MapiMessage message, int flg, int rsv);
	}

	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
	public class MapiMessage
	{
		public int reserved;
		public string subject;
		public string noteText;
		public string messageType;
		public string dateReceived;
		public string conversationID;
		public int flags;
		public IntPtr originator;
		public int recipCount;
		public IntPtr recips;
		public int fileCount;
		public IntPtr files;
	}

	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
	public class MapiFileDesc
	{
		public int reserved;
		public int flags;
		public int position;
		public string path;
		public string name;
		public IntPtr type;
	}

	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
	public class MapiRecipDesc
	{
		public int reserved;
		public int recipClass;
		public string name;
		public string adress;
		public int eidSize;
		public IntPtr entryID;
	}
}