using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using SVNMonitor.Logging;

namespace SVNMonitor.Helpers
{
	internal class EnvironmentHelper
	{
		internal static string ConvertToUTF8(string text, Encoding encoding)
		{
			byte[] bytes = encoding.GetBytes(text);
			return Encoding.UTF8.GetString(bytes);
		}

		public static string ExpandEnvironmentVariables(string name)
		{
			if (string.IsNullOrEmpty(name))
			{
				return string.Empty;
			}
			return Environment.ExpandEnvironmentVariables(name);
		}

		internal static string GetEnvironmentVariables()
		{
			return GetEnvironmentVariables("Environment_Var_{0}={1}{2}", Environment.NewLine);
		}

		internal static string GetEnvironmentVariables(string formatter, string delimiter)
		{
			StringBuilder sb = new StringBuilder();
			Hashtable table = (Hashtable)Environment.GetEnvironmentVariables();
			foreach (object key in table.Keys)
			{
				string value = table[key].ToString();
				sb.Append(string.Format(formatter, key, value, delimiter));
			}
			return sb.ToString();
		}

		private static string GetFrameworkVersions()
		{
			try
			{
				IEnumerable<string> folderNames = FileSystemHelper.GetDirectories(Path.Combine(Environment.GetEnvironmentVariable("SystemRoot"), @"Microsoft.NET\Framework")).Select(f => Path.GetFileName(f));
				return string.Join(";", folderNames.ToArray());
			}
			catch (Exception ex)
			{
				Logger.Log.Error("Error trying to get the .NET Framework version: ", ex);
				return "ERROR";
			}
		}

		internal static string GetUsageInformation()
		{
			StringBuilder sb = new StringBuilder();
			sb.Append(GetEnvironmentVariables());
			sb.AppendFormat("Environment_CommandLine={0}{1}", Environment.CommandLine, Environment.NewLine);
			sb.AppendFormat("Environment_CurrentDirectory={0}{1}", Environment.CurrentDirectory, Environment.NewLine);
			sb.AppendFormat("Environment_OSVersion={0}{1}", Environment.OSVersion, Environment.NewLine);
			sb.AppendFormat("Environment_ProcessorCount={0}{1}", Environment.ProcessorCount, Environment.NewLine);
			sb.AppendFormat("Environment_SystemDirectory={0}{1}", Environment.SystemDirectory, Environment.NewLine);
			sb.AppendFormat("Environment_Version={0}{1}", Environment.Version, Environment.NewLine);
			sb.AppendFormat("Environment_UserInteractive={0}{1}", Environment.UserInteractive, Environment.NewLine);
			sb.AppendFormat("Environment_WorkingSet={0}{1}", Environment.WorkingSet, Environment.NewLine);
			sb.AppendFormat("System_DotNetFramework={0}{1}", GetFrameworkVersions(), Environment.NewLine);
			return sb.ToString();
		}
	}
}