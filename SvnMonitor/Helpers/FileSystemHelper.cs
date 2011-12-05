namespace SVNMonitor.Helpers
{
    using ICSharpCode.SharpZipLib.Zip;
    using SVNMonitor;
    using SVNMonitor.Extensions;
    using SVNMonitor.Logging;
    using SVNMonitor.Resources.Text;
    using SVNMonitor.View;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Drawing;
    using System.IO;
    using System.IO.Compression;
    using System.Reflection;
    using System.Runtime.InteropServices;
    using System.Security.Cryptography;

    internal static class FileSystemHelper
    {
        private static Dictionary<string, Image> fileIcons = new Dictionary<string, Image>();

        public static void Browse(string path)
        {
            path = EnvironmentHelper.ExpandEnvironmentVariables(path);
            Logger.Log.Info(path);
            if (!IsUrl(path) && !Exists(path))
            {
                MainForm.FormInstance.ShowErrorMessage(Strings.ErrorPathDoesNotExists_FORMAT.FormatWith(new object[] { path }), Strings.SVNMonitorCaption);
                SVNMonitor.EventLog.LogError(Strings.ErrorPathDoesNotExists_FORMAT.FormatWith(new object[] { path }), path, null);
            }
            else
            {
                ProcessHelper.StartProcess(path);
            }
        }

        public static byte[] CalculateMD5Hash(string location)
        {
            MD5 md5 = MD5.Create();
            int len = 0;
            List<byte[]> list = new List<byte[]>();
            string[] files = Directory.GetFiles(location);
            Array.Sort<string>(files);
            foreach (string file in files)
            {
                byte[] bytes = File.ReadAllBytes(file);
                byte[] hash = md5.ComputeHash(bytes);
                list.Add(hash);
                len += hash.Length;
            }
            byte[] allHashes = new byte[len];
            len = 0;
            foreach (byte[] hashedBytes in list)
            {
                hashedBytes.CopyTo(allHashes, len);
                len += hashedBytes.Length;
            }
            return md5.ComputeHash(allHashes);
        }

        public static void CopyFile(string sourceFileName, string destinationFileName)
        {
            File.Copy(sourceFileName, destinationFileName, true);
        }

        public static void CopyFiles(string sourceDir, string pattern, string targetDir)
        {
            foreach (string file in GetFiles(sourceDir, pattern))
            {
                string destinationFileName = Path.Combine(targetDir, Path.GetFileName(file));
                CopyFile(file, destinationFileName);
            }
        }

        public static void CreateDirectory(string path)
        {
            Directory.CreateDirectory(path);
        }

        public static void DeleteDirectory(string path)
        {
            DeleteDirectory(path, true);
        }

        public static void DeleteDirectory(string path, bool recursive)
        {
            if (DirectoryExists(path))
            {
                try
                {
                    Directory.Delete(path, recursive);
                }
                catch (Exception ex)
                {
                    ErrorHandler.Append(Strings.ErrorDeletingDirectory_FORMAT.FormatWith(new object[] { path }), path, ex);
                }
            }
        }

        public static bool DeleteFile(string fileName)
        {
            if (!FileExists(fileName))
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
            if (DirectoryExists(directory))
            {
                foreach (string file in GetFiles(directory, pattern, option))
                {
                    DeleteFile(file);
                }
            }
        }

        public static bool DirectoryExists(string name)
        {
            return FileOrDirectoryExists(name, false);
        }

        public static bool Exists(string path)
        {
            if (path == null)
            {
                return false;
            }
            if (IsUrl(path))
            {
                return false;
            }
            if (!FileExists(path))
            {
                return DirectoryExists(path);
            }
            return true;
        }

        public static bool ExistsNoResolve(string path)
        {
            return (FileOrDirectoryExistsNoResolve(path, true) || FileOrDirectoryExistsNoResolve(path, false));
        }

        public static void Explore(string path)
        {
            path = EnvironmentHelper.ExpandEnvironmentVariables(path);
            if (Exists(path))
            {
                string arg = string.Format("/e,/select,\"{0}\"", path);
                Logger.Log.Info(arg);
                ProcessHelper.StartProcess("explorer.exe", arg);
            }
            else
            {
                string error = Strings.ErrorPathDoesNotExists_FORMAT.FormatWith(new object[] { path });
                Logger.Log.Info(error);
                SVNMonitor.EventLog.LogWarning(error, path);
                MainForm.FormInstance.ShowErrorMessage(error, Strings.SVNMonitorCaption);
            }
        }

        private static Image ExtractAssociatedIcon(string fileName)
        {
            Image image = null;
            try
            {
                SHFILEINFO shinfo = new SHFILEINFO();
                Win32.SHGetFileInfo(fileName, 0, ref shinfo, (uint) Marshal.SizeOf(shinfo), 0x101);
                image = Icon.FromHandle(shinfo.hIcon).ToBitmap();
            }
            catch (Exception ex)
            {
                Logger.Log.Error(string.Format("Error trying to extract the associated icon of {0}", fileName), ex);
            }
            return image;
        }

        public static bool FileExists(string name)
        {
            return FileOrDirectoryExists(name, true);
        }

        private static bool FileOrDirectoryExists(string path, bool file)
        {
            if (path == null)
            {
                return false;
            }
            if (IsUrl(path))
            {
                return false;
            }
            bool exists = false;
            path = EnvironmentHelper.ExpandEnvironmentVariables(path);
            try
            {
                path = GetFullPath(path);
            }
            catch (PathTooLongException ex)
            {
                Logger.Log.Debug(string.Format("{0} is too long.", path), ex);
            }
            try
            {
                if (file)
                {
                    return File.Exists(path);
                }
                exists = Directory.Exists(path);
            }
            catch (Exception ex)
            {
                Logger.Log.Error(string.Format("Error checking FileOrDirectoryExists.Exists({0})", path), ex);
            }
            return exists;
        }

        private static bool FileOrDirectoryExistsNoResolve(string path, bool file)
        {
            if (file)
            {
                return File.Exists(path);
            }
            return Directory.Exists(path);
        }

        public static string FixInvalidFileNameChars(string fileName, char replacement)
        {
            foreach (char invalidChar in Path.GetInvalidFileNameChars())
            {
                fileName = fileName.Replace(invalidChar, replacement);
            }
            return fileName;
        }

        public static string FixUrl(string url)
        {
            string lower = url.ToLower();
            if ((!lower.StartsWith(@"svn:\") && !lower.StartsWith(@"http:\")) && (!lower.StartsWith(@"https:\") && !lower.StartsWith(@"file:\")))
            {
                return url;
            }
            return url.Replace(@"\", "/");
        }

        public static Image GetAssociatedIcon(string fileName)
        {
            Image image = null;
            try
            {
                string extension = GetExtension(fileName);
                if (!fileIcons.ContainsKey(extension))
                {
                    image = ExtractAssociatedIcon(fileName);
                    fileIcons.Add(extension, image);
                    return image;
                }
                image = fileIcons[extension];
            }
            catch (Exception ex)
            {
                Logger.Log.Error(string.Format("Error trying to get the associated icon of {0}", fileName), ex);
            }
            return image;
        }

        public static string[] GetDirectories(string path)
        {
            return GetDirectories(path, "*");
        }

        public static string[] GetDirectories(string path, string searchPattern)
        {
            return GetDirectories(path, searchPattern, SearchOption.TopDirectoryOnly);
        }

        public static string[] GetDirectories(string path, string searchPattern, SearchOption searchOption)
        {
            return Directory.GetDirectories(path, searchPattern, searchOption);
        }

        public static string GetDirectoryName(string path)
        {
            return Path.GetDirectoryName(path);
        }

        private static string GetExtension(string fileName)
        {
            if (DirectoryExists(fileName))
            {
                return ":folder";
            }
            FileInfo fileInfo = new FileInfo(fileName);
            return fileInfo.Extension;
        }

        public static string GetFileName(string path)
        {
            return Path.GetFileName(path);
        }

        public static string[] GetFiles(string path)
        {
            return GetFiles(path, "*");
        }

        public static string[] GetFiles(string path, string searchPattern)
        {
            return GetFiles(path, searchPattern, SearchOption.TopDirectoryOnly);
        }

        public static string[] GetFiles(string path, string searchPattern, SearchOption searchOption)
        {
            return Directory.GetFiles(path, searchPattern, searchOption);
        }

        public static string GetFullPath(string path)
        {
            try
            {
                return Path.GetFullPath(path);
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
            while (FileExists(newName))
            {
                index++;
                newName = string.Format("{0} ({1})", fileName, index);
            }
            return newName;
        }

        public static string GetTempFileName()
        {
            return Path.GetTempFileName();
        }

        public static bool IsFileUrl(string path)
        {
            if (path == null)
            {
                return false;
            }
            path = FixUrl(path);
            return path.ToLower().Contains("file://");
        }

        public static bool IsUrl(string path)
        {
            if (path == null)
            {
                return false;
            }
            path = FixUrl(path);
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
            MoveFile(sourceFileName, destinationFileName);
        }

        public static void Open(string path)
        {
            path = EnvironmentHelper.ExpandEnvironmentVariables(path);
            if (Exists(path))
            {
                Logger.Log.Info(path);
                try
                {
                    ProcessHelper.StartProcess(path);
                }
                catch (Win32Exception ex)
                {
                    Logger.Log.InfoFormat("Tried opening '{0}' but got this error: {1}. Trying 'OpenWith' instead.", path, ex.Message);
                    SVNMonitor.EventLog.LogError(Strings.ErrorOpeningPath_FORMAT.FormatWith(new object[] { path }), path, ex);
                    SVNMonitor.EventLog.LogWarning(Strings.TryingOpenWithInstead, path);
                    OpenWith(path);
                }
            }
            else
            {
                string error = Strings.ErrorPathDoesNotExists_FORMAT.FormatWith(new object[] { path });
                SVNMonitor.EventLog.LogWarning(error, path);
                MainForm.FormInstance.ShowErrorMessage(error, Strings.SVNMonitorCaption);
            }
        }

        public static void OpenWith(string path)
        {
            path = EnvironmentHelper.ExpandEnvironmentVariables(path);
            if (Exists(path))
            {
                Logger.Log.Info(path);
                string runDllPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.System), "RUNDLL32.EXE");
                Logger.Log.InfoFormat("RUNDLL32.EXE is '{0}'", runDllPath);
                string arg = string.Format("Shell32,OpenAs_RunDLL {0}", path);
                ProcessHelper.StartProcess(runDllPath, arg);
            }
            else
            {
                string error = Strings.ErrorPathDoesNotExists_FORMAT.FormatWith(new object[] { path });
                SVNMonitor.EventLog.LogWarning(error, path);
                MainForm.FormInstance.ShowErrorMessage(error, Strings.SVNMonitorCaption);
            }
        }

        public static string ReadAllText(string path)
        {
            return File.ReadAllText(path);
        }

        public static bool UnZip(string fileName, string target)
        {
            try
            {
                new FastZip().ExtractZip(fileName, target, string.Empty);
                return true;
            }
            catch (Exception ex)
            {
                Logger.Log.Error(string.Format("Error trying to unzip file '{0}'", fileName), ex);
            }
            return false;
        }

        public static bool UnZip(string fileName, out string targetFolder)
        {
            FileInfo fileInfo = new FileInfo(fileName);
            targetFolder = Path.Combine(fileInfo.DirectoryName, fileInfo.Name + @"_\");
            CreateDirectory(targetFolder);
            return UnZip(fileName, (string) targetFolder);
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
                byte[] buffer = new byte[inFile.Length];
                int count = inFile.Read(buffer, 0, buffer.Length);
                inFile.Close();
                if (count != buffer.Length)
                {
                    return false;
                }
                FileStream outFile = new FileStream(fileName + ".zip", FileMode.Create);
                GZipStream compressedzipStream = new GZipStream(outFile, CompressionMode.Compress, true);
                compressedzipStream.Write(buffer, 0, buffer.Length);
                compressedzipStream.Close();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public static string AppData
        {
            get
            {
                string path = SessionInfo.UserAppData;
                if (string.IsNullOrEmpty(path))
                {
                    path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "SVNMonitor");
                }
                bool created = !DirectoryExists(path);
                CreateDirectory(path);
                if (created)
                {
                    SVNMonitor.EventLog.LogInfo(Strings.AppDataFolderCreated_FORMAT.FormatWith(new object[] { path }), path);
                }
                return path;
            }
        }

        public static Version CurrentVersion
        {
            get
            {
                return Assembly.GetExecutingAssembly().GetName().Version;
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
                if ((8 != IntPtr.Size) && string.IsNullOrEmpty(Environment.GetEnvironmentVariable("PROCESSOR_ARCHITEW6432")))
                {
                    return ProgramFiles;
                }
                return Environment.GetEnvironmentVariable("ProgramFiles(x86)");
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct SHFILEINFO
        {
            public IntPtr hIcon;
            public IntPtr iIcon;
            public uint dwAttributes;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst=260)]
            public string szDisplayName;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst=80)]
            public string szTypeName;
        }

        private class Win32
        {
            public const uint SHGFI_ICON = 0x100;
            public const uint SHGFI_LARGEICON = 0;
            public const uint SHGFI_SMALLICON = 1;

            [DllImport("shell32.dll")]
            public static extern IntPtr SHGetFileInfo(string pszPath, uint dwFileAttributes, ref FileSystemHelper.SHFILEINFO psfi, uint cbSizeFileInfo, uint uFlags);
        }
    }
}

