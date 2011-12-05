using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using SVNMonitor.Support;
using System.Diagnostics;

namespace SVNMonitor
{
public class SessionInfo
{
	private static string[] commandLineArguments;

	private static Dictionary<string, PropertyInfo> sessionProperties;

	public static string CommandLineArguments
	{
		get
		{
			string line = string.Join(" ", SessionInfo.commandLineArguments);
			return line;
		}
	}

	internal static IEnumerable<PropertyInfo> Properties
	{
		get
		{
			IEnumerable<PropertyInfo> list = SessionInfo.SessionProperties.Values.Distinct<PropertyInfo>();
			return list;
		}
	}

	private static Dictionary<string, PropertyInfo> SessionProperties
	{
		get
		{
			if (SessionInfo.sessionProperties == null)
			{
				SessionInfo.sessionProperties = SessionInfo.GetSessionProperties();
			}
			return SessionInfo.sessionProperties;
		}
	}

	[Parameter("test")]
	public static bool Test
	{
		get
		{
			return SessionInfo.<Test>k__BackingField;
		}
		private set
		{
			value;
		}
	}

	[Parameter("testnewversion")]
	public static string TestNewVersion
	{
		get
		{
			return SessionInfo.<TestNewVersion>k__BackingField;
		}
		private set
		{
			value;
		}
	}

	[Parameter("testnewversionfile")]
	public static string TestNewVersionFile
	{
		get
		{
			return SessionInfo.<TestNewVersionFile>k__BackingField;
		}
		private set
		{
			value;
		}
	}

	[Parameter("testupgrader")]
	public static bool TestUpgrader
	{
		get
		{
			return SessionInfo.<TestUpgrader>k__BackingField;
		}
		private set
		{
			value;
		}
	}

	[Parameter("upgradedfrom")]
	[Parameter("uf")]
	public static string UpgradedFrom
	{
		get
		{
			return SessionInfo.<UpgradedFrom>k__BackingField;
		}
		private set
		{
			value;
		}
	}

	[Parameter("data")]
	[Parameter("d")]
	[Parameter("userdata")]
	public static string UserAppData
	{
		get
		{
			return SessionInfo.<UserAppData>k__BackingField;
		}
		private set
		{
			value;
		}
	}

	public SessionInfo()
	{
	}

	private static Dictionary<string, string> ExtractArguments(string[] args)
	{
		Dictionary<string, string> dict = new Dictionary<string, string>();
		if (args != null)
		{
			string[] strArrays = args;
			foreach (string arg in strArrays)
			{
				if ((arg.StartsWith("/") || arg.StartsWith("-")) && arg.Contains("="))
				{
					int eqIndex = arg.IndexOf("=");
					string key = arg.Substring(1, eqIndex - 1);
					string @value = arg.Substring(eqIndex + 1);
					dict.Add(key.ToLower(), @value);
				}
			}
			return dict;
		}
		return dict;
	}

	private static Dictionary<string, PropertyInfo> GetSessionProperties()
	{
		Dictionary<string, PropertyInfo> dict = new Dictionary<string, PropertyInfo>();
		PropertyInfo[] props = typeof(SessionInfo).GetProperties(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
		foreach (PropertyInfo prop in props.Where<PropertyInfo>(new Predicate<PropertyInfo>((p) => Attribute.IsDefined(p, typeof(ParameterAttribute)))))
		{
			ParameterAttribute[] atts = (ParameterAttribute[])Attribute.GetCustomAttributes(prop, typeof(ParameterAttribute));
			ParameterAttribute[] parameterAttributeArray = atts;
			foreach (ParameterAttribute att in parameterAttributeArray)
			{
				dict.Add(att.ArgName.ToLower(), prop);
			}
		}
		return dict;
	}

	internal static string GetUsageInformation()
	{
		StringBuilder sb = new StringBuilder();
		sb.AppendFormat("SessionInfo_UserAppData={0}{1}", SessionInfo.UserAppData, UsageInformationSender.Separator);
		sb.AppendFormat("SessionInfo_CommandLineArguments={0}{1}", SessionInfo.CommandLineArguments, UsageInformationSender.Separator);
		return sb.ToString();
	}

	public static void SetSessionInfo(string[] args)
	{
		SessionInfo.commandLineArguments = args;
		if (args == null)
		{
			return;
		}
		if ((int)args.Length == 0)
		{
			return;
		}
		Dictionary<string, string> values = SessionInfo.ExtractArguments(args);
		foreach (string key in values.Keys)
		{
			if (SessionInfo.SessionProperties.ContainsKey(key))
			{
				PropertyInfo prop = SessionInfo.SessionProperties[key];
				string stringValue = values[key];
				object @value = Convert.ChangeType(stringValue, prop.PropertyType);
				prop.SetValue(null, @value, null);
			}
		}
	}

	[Conditional("DEBUG")]
	internal static void SetUpgradeTest(string version, string upgradeFile)
	{
		SessionInfo.TestNewVersion = version;
		SessionInfo.TestNewVersionFile = upgradeFile;
	}
}
}