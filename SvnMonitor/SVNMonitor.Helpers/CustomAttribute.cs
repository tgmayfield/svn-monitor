using System;

namespace SVNMonitor.Helpers
{
[AttributeUsage(AttributeTargets.Class)]
[Serializable]
public class CustomAttribute : Attribute
{
	public CustomAttribute()
	{
	}

	public static bool IsCustom(Type type)
	{
		bool isCustom = Attribute.IsDefined(type, typeof(CustomAttribute));
		return isCustom;
	}
}
}