using System;
using System.Text;
using SVNMonitor.Support;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using SVNMonitor.Extensions;
using SVNMonitor.Logging;

namespace SVNMonitor.Helpers
{
internal class EnvironmentHelper
{
	public EnvironmentHelper()
	{
	}

	internal static string ConvertToUTF8(string text, Encoding encoding)
	{
		byte[] bytes = encoding.GetBytes(text);
		string result = Encoding.UTF8.GetString(bytes);
		return result;
	}

	public static string ExpandEnvironmentVariables(string name)
	{
		if (string.IsNullOrEmpty(name))
		{
			return string.Empty;
		}
		string expanded = Environment.ExpandEnvironmentVariables(name);
		return expanded;
	}

	internal static string GetEnvironmentVariables()
	{
		string vars = EnvironmentHelper.GetEnvironmentVariables("Environment_Var_{0}={1}{2}", UsageInformationSender.Separator);
		return vars;
	}

	internal static string GetEnvironmentVariables(string formatter, string delimiter)
	{
		StringBuilder sb = new StringBuilder();
		Hashtable table = (Hashtable)Environment.GetEnvironmentVariables();
		foreach (object key in table.Keys)
		{
			string @value = table[key].ToString();
			sb.Append(string.Format(formatter, key, @value, delimiter));
		}
		return sb.ToString();
	}

	private static string GetFrameworkVersions()
	{
		try
		{
			string[] frameworkFolders = FileSystemHelper.GetDirectories(Path.Combine(Environment.GetEnvironmentVariable("SystemRoot"), "Microsoft.NET\\Framework"));
			IEnumerable<string> folderNames = frameworkFolders.Select<string,string>(new Func<string, string>((f) => Path.GetFileName(f)));
			return string.Join(";", folderNames.ToArray<string>());
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
		sb.Append(EnvironmentHelper.GetEnvironmentVariables());
		sb.AppendFormat("Environment_CommandLine={0}{1}", Environment.CommandLine, UsageInformationSender.Separator);
		sb.AppendFormat("Environment_CurrentDirectory={0}{1}", Environment.CurrentDirectory, UsageInformationSender.Separator);
		sb.AppendFormat("Environment_OSVersion={0}{1}", Environment.OSVersion, UsageInformationSender.Separator);
		sb.AppendFormat("Environment_ProcessorCount={0}{1}", Environment.ProcessorCount, UsageInformationSender.Separator);
		sb.AppendFormat("Environment_SystemDirectory={0}{1}", Environment.SystemDirectory, UsageInformationSender.Separator);
		sb.AppendFormat("Environment_Version={0}{1}", Environment.Version, UsageInformationSender.Separator);
		sb.AppendFormat("Environment_UserInteractive={0}{1}", Environment.UserInteractive, UsageInformationSender.Separator);
		sb.AppendFormat("Environment_WorkingSet={0}{1}", Environment.WorkingSet, UsageInformationSender.Separator);
		sb.AppendFormat("System_DotNetFramework={0}{1}", EnvironmentHelper.GetFrameworkVersions(), UsageInformationSender.Separator);
		return sb.ToString();
	}
}
}