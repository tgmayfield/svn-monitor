using System;
using System.Resources;

namespace SVNMonitor.Helpers
{
	internal class EnumHelper
	{
		private static ResourceManager enumsResourceManager;

		public static bool IsFlagged(Enum value, Enum flag)
		{
			int intValue = value.GetHashCode();
			int intFlag = flag.GetHashCode();
			return ((intValue & intFlag) == intFlag);
		}

		public static T ParseEnum<T>(string valueName)
		{
			return (T)Enum.Parse(typeof(T), valueName);
		}

		public static string TranslateEnumValue<T>(T value)
		{
			string name = string.Format("{0}.{1}", typeof(T).Name, value);
			string translation = UIHelper.GetString(EnumsResourceManager, name, value.ToString());
			if (string.IsNullOrEmpty(translation))
			{
				translation = value.ToString();
			}
			return translation;
		}

		private static ResourceManager EnumsResourceManager
		{
			get
			{
				if (enumsResourceManager == null)
				{
					string resourceName = "SVNMonitor.Resources.Text.Enums";
					enumsResourceManager = new ResourceManager(resourceName, typeof(EnumHelper).Assembly);
				}
				return enumsResourceManager;
			}
		}
	}
}