using System;
using Janus.Windows.UI;

namespace SVNMonitor.Helpers
{
public static class InheritableBooleanHelper
{
	public static bool ToBool(this InheritableBoolean value)
	{
		return value == InheritableBoolean.True;
	}

	public static InheritableBoolean ToInheritableBoolean(this bool value)
	{
		InheritableBoolean result = value || InheritableBoolean.False;
		return result;
	}
}
}