using System;

namespace SVNMonitor.Helpers
{
	[Flags]
	internal enum TypeRequirements
	{
		None,
		Serializable,
		NonCustom
	}
}