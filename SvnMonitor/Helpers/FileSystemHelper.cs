using System.Collections.Generic;
using System.Diagnostics;
using System;
using SVNMonitor;
using System.IO;
using SVNMonitor.Resources.Text;
using System.Reflection;
using SVNMonitor.Logging;
using SVNMonitor.View;
using System.Security.Cryptography;
using System.Drawing;
using System.Runtime.InteropServices;
using System.ComponentModel;
using ICSharpCode.SharpZipLib.Zip;
using System.IO.Compression;

namespace SVNMonitor.Helpers
{
internal static class FileSystemHelper
{
	private static Dictionary<string, Image> fileIcons;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public static string AppData
	{
		get
		{
			object[] objArray;
			string path = SessionInfo.UserAppData;
			if (string.IsNullOrEmpty(path))
			{
				path = Path.Combine(Environment.GetFolderPath(SpecialFolder.ApplicationData), "SVNMonitor");
			}
			FileSystemHelper.CreateDirectory(path);
			if (!FileSystemHelper.DirectoryExists(path))
			{
				EventLog.LogInfo(Strings.AppDataFolderCreated_FORMAT.FormatWith(new object[] { path }), path);
			}
			return path;
		}
	}

	public static Version CurrentVersion
	{
		get
		{
			Version version = Assembly.GetExecutingAssembly().GetName().Version;
			return version;
		}
	}

	public static string ProgramFiles
	{
		get
		{
			return Environment.GetEnvironmentVariable("ProgramFiles");
		}
	}

	public static string ProgramFilesx86
	{
		get
		{
			if (8 == IntPtr.Size || !string.IsNullOrEmpty(Environment.GetEnvironmentVariable("PROCESSOR_ARCHITEW6432")))
			{
				return Environment.GetEnvironmentVariable("ProgramFiles(x86)");
			}
			return FileSystemHelper.ProgramFiles;
		}
	}

	static FileSystemHelper()
	{
		FileSystemHelper.fileIcons = new Dictionary<string, Image>();
	}

	public static void Browse(string path)
	{
		object[] objArray;
		object[] objArray2;
		path = EnvironmentHelper.ExpandEnvironmentVariables(path);
		Logger.Log.Info(path);
		if (!FileSystemHelper.IsUrl(path) && !FileSystemHelper.Exists(path))
		{
			MainForm.FormInstance.ShowErrorMessage(Strings.ErrorPathDoesNotExists_FORMAT.FormatWith(new object[] { path }), Strings.SVNMonitorCaption);
			EventLog.LogError(Strings.ErrorPathDoesNotExists_FORMAT.FormatWith(new object[] { path }), path, null);
			return;
		}
		ProcessHelper.StartProcess(path);
	}

	public static byte[] CalculateMD5Hash(string location)
	{
		MD5 md5 = MD5.Create();
		int len = 0;
		List<byte[]> list = new List<byte[]>();
		string[] files = Directory.GetFiles(location);
		Sort<string>(files);
		string[] strArrays = files;
		foreach (string file in strArrays)
		{
			byte[] bytes = File.ReadAllBytes(file);
			byte[] hash = md5.ComputeHash(bytes);
			list.Add(hash);
			len = len + (int)hash.Length;
		}
		byte[] allHashes = new byte[len];
		len = 0;
		foreach (byte[] hashedBytes in list)
		{
			hashedBytes.CopyTo(allHashes, len);
			len = len + (int)hashedBytes.Length;
		}
		byte[] result = md5.ComputeHash(allHashes);
		return result;
	}

	public static void CopyFile(string sourceFileName, string destinationFileName)
	{
		File.Copy(sourceFileName, destinationFileName, true);
	}

	public static void CopyFiles(string sourceDir, string pattern, string targetDir)
	{
		string[] files = FileSystemHelper.GetFiles(sourceDir, pattern);
		string[] strArrays = files;
		foreach (string file in strArrays)
		{
			string destinationFileName = Path.Combine(targetDir, Path.GetFileName(file));
			FileSystemHelper.CopyFile(file, destinationFileName);
		}
	}

	public static void CreateDirectory(string path)
	{
		Directory.CreateDirectory(path);
	}

	public static void DeleteDirectory(string path)
	{
		FileSystemHelper.DeleteDirectory(path, true);
	}

	public static void DeleteDirectory(string path, bool recursive)
	{
		object[] objArray;
		if (!FileSystemHelper.DirectoryExists(path))
		{
			return;
		}
		try
		{
			Directory.Delete(path, recursive);
		}
		catch (Exception ex)
		{
			ErrorHandler.Append(Strings.ErrorDeletingDirectory_FORMAT.FormatWith(new object[] { path }), path, ex);
		}
	}

	public static bool DeleteFile(string fileName)
	{
		object[] objArray;
		if (!FileSystemHelper.FileExists(fileName))
		{
			return false;
		}
		FileAttributes attributes = File.GetAttributes(fileName);
		File.SetAttributes(fileName, FileAttributes.Normal);
		try
		{
			File.Delete(fileName);
			return true;
		}
		catch (Exception ex)
		{
			ErrorHandler.Append(Strings.ErrorDeletingFile_FORMAT.FormatWith(new object[] { fileName }), fileName, ex);
			File.SetAttributes(fileName, attributes);
			return false;
		}
	}

	public static void DeleteFiles(string directory, string pattern, SearchOption option)
	{
		if (!FileSystemHelper.DirectoryExists(directory))
		{
			return;
		}
		string[] files = FileSystemHelper.GetFiles(directory, pattern, option);
		string[] strArrays = files;
		foreach (string file in strArrays)
		{
			FileSystemHelper.DeleteFile(file);
		}
	}

	public static bool DirectoryExists(string name)
	{
		bool exists = FileSystemHelper.FileOrDirectoryExists(name, false);
		return exists;
	}

	public static bool Exists(string path)
	{
		if (path == null)
		{
			return false;
		}
		if (FileSystemHelper.IsUrl(path))
		{
			return false;
		}
		if (!FileSystemHelper.FileExists(path))
		{
			return FileSystemHelper.DirectoryExists(path);
		}
		return true;
	}

	public static bool ExistsNoResolve(string path)
	{
		bool exists = FileSystemHelper.FileOrDirectoryExistsNoResolve(path, true) || FileSystemHelper.FileOrDirectoryExistsNoResolve(path, false);
		return exists;
	}

	public static void Explore(string path)
	{
		object[] objArray;
		path = EnvironmentHelper.ExpandEnvironmentVariables(path);
		if (FileSystemHelper.Exists(path))
		{
			string arg = string.Format("/e,/select,\"{0}\"", path);
			Logger.Log.Info(arg);
			ProcessHelper.StartProcess("explorer.exe", arg);
			return;
		}
		string error = Strings.ErrorPathDoesNotExists_FORMAT.FormatWith(new object[] { path });
		Logger.Log.Info(error);
		EventLog.LogWarning(error, path);
		MainForm.FormInstance.ShowErrorMessage(error, Strings.SVNMonitorCaption);
	}

	private static Image ExtractAssociatedIcon(string fileName)
	{
		Image image = null;
		try
		{
			SHFILEINFO shinfo = new SHFILEINFO();
			Win32.SHGetFileInfo(fileName, 0, ref shinfo, Marshal.SizeOf(shinfo), 257);
			Icon icon = Icon.FromHandle(shinfo.hIcon);
			return icon.ToBitmap();
		}
		catch (Exception ex)
		{
			Logger.Log.Error(string.Format("Error trying to extract the associated icon of {0}", fileName), ex);
		}
		return image;
	}

	public static bool FileExists(string name)
	{
		bool exists = FileSystemHelper.FileOrDirectoryExists(name, true);
		return exists;
	}

	private static bool FileOrDirectoryExists(string path, bool file)
	{
		if (path == null)
		{
			return false;
		}
		if (FileSystemHelper.IsUrl(path))
		{
			return false;
		}
		bool exists = false;
		path = EnvironmentHelper.ExpandEnvironmentVariables(path);
		try
		{
			path = FileSystemHelper.GetFullPath(path);
		}
		catch (PathTooLongException ex)
		{
			Logger.Log.Debug(string.Format("{0} is too long.", path), ex);
		}
		try
		{
			if (file)
			{
				exists = File.Exists(path);
			}
			else
			{
			}
		}
		catch (Exception ex)
		{
			Logger.Log.Error(string.Format("Error checking FileOrDirectoryExists.Exists({0})", path), ex);
		}
		return exists;
	}

	private static bool FileOrDirectoryExistsNoResolve(string path, bool file)
	{
		bool exists = false;
		if (file)
		{
			return File.Exists(path);
		}
		exists = Directory.Exists(path);
		return exists;
	}

	public static string FixInvalidFileNameChars(string fileName, char replacement)
	{
		char[] invalidFileNameChars = Path.GetInvalidFileNameChars();
		foreach (char invalidChar in invalidFileNameChars)
		{
			fileName = fileName.Replace(invalidChar, replacement);
		}
		return fileName;
	}

	public static string FixUrl(string url)
	{
		string lower = url.ToLower();
		if (lower.StartsWith("svn:\\") || lower.StartsWith("http:\\") || lower.StartsWith("https:\\") || lower.StartsWith("file:\\"))
		{
			return url.Replace("\\", "/");
		}
		return url;
	}

	public static Image GetAssociatedIcon(string fileName)
	{
		Image image = null;
		try
		{
			string extension = FileSystemHelper.GetExtension(fileName);
			if (!FileSystemHelper.fileIcons.ContainsKey(extension))
			{
				image = FileSystemHelper.ExtractAssociatedIcon(fileName);
				FileSystemHelper.fileIcons.Add(extension, image);
			}
			else
			{
			}
		}
		catch (Exception ex)
		{
			Logger.Log.Error(string.Format("Error trying to get the associated icon of {0}", fileName), ex);
		}
		return image;
	}

	public static string[] GetDirectories(string path)
	{
		string[] dirs = FileSystemHelper.GetDirectories(path, "*");
		return dirs;
	}

	public static string[] GetDirectories(string path, string searchPattern)
	{
		string[] dirs = FileSystemHelper.GetDirectories(path, searchPattern, SearchOption.TopDirectoryOnly);
		return dirs;
	}

	public static string[] GetDirectories(string path, string searchPattern, SearchOption searchOption)
	{
		string[] dirs = Directory.GetDirectories(path, searchPattern, searchOption);
		return dirs;
	}

	public static string GetDirectoryName(string path)
	{
		string name = Path.GetDirectoryName(path);
		return name;
	}

	private static string GetExtension(string fileName)
	{
		if (FileSystemHelper.DirectoryExists(fileName))
		{
			return ":folder";
		}
		FileInfo fileInfo = new FileInfo(fileName);
		string extension = fileInfo.Extension;
		return extension;
	}

	public static string GetFileName(string path)
	{
		string name = Path.GetFileName(path);
		return name;
	}

	public static string[] GetFiles(string path)
	{
		return FileSystemHelper.GetFiles(path, "*");
	}

	public static string[] GetFiles(string path, string searchPattern)
	{
		return FileSystemHelper.GetFiles(path, searchPattern, SearchOption.TopDirectoryOnly);
	}

	public static string[] GetFiles(string path, string searchPattern, SearchOption searchOption)
	{
		return Directory.GetFiles(path, searchPattern, searchOption);
	}

	public static string GetFullPath(string path)
	{
		try
		{
			string fullPath = Path.GetFullPath(path);
			return fullPath;
		}
		catch (Exception)
		{
			return path;
		}
	}

	public static string GetSafeFileName(string fileName)
	{
		string newName = fileName;
		int index = 0;
		while (FileSystemHelper.FileExists(newName))
		{
			index++;
			newName = string.Format("{0} ({1})", fileName, index);
		}
		return newName;
	}

	public static string GetTempFileName()
	{
		string name = Path.GetTempFileName();
		return name;
	}

	public static bool IsFileUrl(string path)
	{
		if (path == null)
		{
			return false;
		}
		path = FileSystemHelper.FixUrl(path);
		bool isFileUrl = path.ToLower().Contains("file://");
		return isFileUrl;
	}

	public static bool IsUrl(string path)
	{
		if (path == null)
		{
			return false;
		}
		path = FileSystemHelper.FixUrl(path);
		return path.Contains("://");
	}

	public static void MoveFile(string sourceFileName, string destinationFileName)
	{
		File.Move(sourceFileName, destinationFileName);
	}

	public static void MoveFileToDir(string sourceFileName, string destinationDir)
	{
		string fileName = Path.GetFileName(sourceFileName);
		string destinationFileName = Path.Combine(destinationDir, fileName);
		FileSystemHelper.MoveFile(sourceFileName, destinationFileName);
	}

	public static void Open(string path)
	{
		object[] objArray;
		object[] objArray2;
		path = EnvironmentHelper.ExpandEnvironmentVariables(path);
		if (FileSystemHelper.Exists(path))
		{
			Logger.Log.Info(path);
			try
			{
				ProcessHelper.StartProcess(path);
			}
			catch (Win32Exception ex)
			{
				Logger.Log.InfoFormat("Tried opening '{0}' but got this error: {1}. Trying 'OpenWith' instead.", path, ex.Message);
				EventLog.LogError(Strings.ErrorOpeningPath_FORMAT.FormatWith(new object[] { path }), path, ex);
				EventLog.LogWarning(Strings.TryingOpenWithInstead, path);
				FileSystemHelper.OpenWith(path);
			}
		}
		else
		{
			string error = Strings.ErrorPathDoesNotExists_FORMAT.FormatWith(new object[] { path });
			EventLog.LogWarning(error, path);
			MainForm.FormInstance.ShowErrorMessage(error, Strings.SVNMonitorCaption);
		}
	}

	public static void OpenWith(string path)
	{
		object[] objArray;
		path = EnvironmentHelper.ExpandEnvironmentVariables(path);
		if (FileSystemHelper.Exists(path))
		{
			Logger.Log.Info(path);
			string runDllPath = Path.Combine(Environment.GetFolderPath(SpecialFolder.System), "RUNDLL32.EXE");
			Logger.Log.InfoFormat("RUNDLL32.EXE is '{0}'", runDllPath);
			string arg = string.Format("Shell32,OpenAs_RunDLL {0}", path);
			ProcessHelper.StartProcess(runDllPath, arg);
			return;
		}
		string error = Strings.ErrorPathDoesNotExists_FORMAT.FormatWith(new object[] { path });
		EventLog.LogWarning(error, path);
		MainForm.FormInstance.ShowErrorMessage(error, Strings.SVNMonitorCaption);
	}

	public static string ReadAllText(string path)
	{
		string text = File.ReadAllText(path);
		return text;
	}

	public static bool UnZip(string fileName, out string targetFolder)
	{
		FileInfo fileInfo = new FileInfo(fileName);
		FileSystemHelper.CreateDirectory(ref targetFolder);
		bool extracted = FileSystemHelper.UnZip(fileName, ref targetFolder);
		return extracted;
	}

	public static bool UnZip(string fileName, string target)
	{
		try
		{
			FastZip zip = new FastZip();
			zip.ExtractZip(fileName, target, string.Empty);
			return true;
		}
		catch (Exception ex)
		{
			Logger.Log.Error(string.Format("Error trying to unzip file '{0}'", fileName), ex);
		}
		return false;
	}

	public static void WriteAllText(string fileName, string contents)
	{
		File.WriteAllText(fileName, contents);
	}

	public static bool Zip(string fileName)
	{
		try
		{
			FileStream inFile = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read);
			byte[] buffer = new byte[(IntPtr)inFile.Length];
			int count = inFile.Read(buffer, 0, (int)buffer.Length);
			inFile.Close();
			if (count != (int)buffer.Length)
			{
				return false;
			}
			FileStream outFile = new FileStream(string.Concat(fileName, ".zip"), FileMode.Create);
			GZipStream compressedzipStream = new GZipStream(outFile, CompressionMode.Compress, true);
			compressedzipStream.Write(buffer, 0, (int)buffer.Length);
			compressedzipStream.Close();
			return true;
		}
		catch (Exception)
		{
			return false;
		}
	}

	public struct SHFILEINFO
	{
		public uint dwAttributes;

		public IntPtr hIcon;

		public IntPtr iIcon;

		public string szDisplayName;

		public string szTypeName;
	}

	private class Win32
	{
		public const uint SHGFI_ICON = 256;

		public const uint SHGFI_LARGEICON = 0;

		public const uint SHGFI_SMALLICON = 1;

		public Win32();

		[DllImport("shell32.dll", CharSet=CharSet.None)]
		public static extern IntPtr SHGetFileInfo(string pszPath, uint dwFileAttributes, ref SHFILEINFO psfi, uint cbSizeFileInfo, uint uFlags);
	}
}
}