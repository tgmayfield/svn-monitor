using System;

namespace SVNMonitor.Helpers
{
	[Serializable, AttributeUsage(AttributeTargets.Class)]
	public class CustomAttribute : Attribute
	{
		public static bool IsCustom(Type type)
		{
			return Attribute.IsDefined(type, typeof(CustomAttribute));
		}
	}
}