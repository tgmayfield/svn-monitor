using System.Resources;
using System;

namespace SVNMonitor.Helpers
{
internal class EnumHelper
{
	private static ResourceManager enumsResourceManager;

	private static ResourceManager EnumsResourceManager
	{
		get
		{
			if (EnumHelper.enumsResourceManager == null)
			{
				string resourceName = "SVNMonitor.Resources.Text.Enums";
				EnumHelper.enumsResourceManager = new ResourceManager(resourceName, typeof(EnumHelper).Assembly);
			}
			return EnumHelper.enumsResourceManager;
		}
	}

	public EnumHelper()
	{
	}

	public static bool IsFlagged(Enum value, Enum flag)
	{
		int intValue = value.GetHashCode();
		int intFlag = flag.GetHashCode();
		bool isFlagged = (intValue & intFlag) == intFlag;
		return isFlagged;
	}

	public static T ParseEnum<T>(string valueName)
	{
		T enumValue = (T)Enum.Parse(typeof(T), valueName);
		return enumValue;
	}

	public static string TranslateEnumValue<T>(T value)
	{
		string name = string.Format("{0}.{1}", typeof(T).Name, value);
		string translation = UIHelper.GetString(EnumHelper.EnumsResourceManager, name, value.ToString());
		if (string.IsNullOrEmpty(translation))
		{
			translation = &value.ToString();
		}
		return translation;
	}
}
}