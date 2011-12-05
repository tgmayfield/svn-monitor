using System;

namespace SVNMonitor.Helpers
{
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Method, AllowMultiple = false)]
	public class AssociatedUserActionAttribute : Attribute
	{
		public AssociatedUserActionAttribute(SVNMonitor.Helpers.UserAction userAction)
		{
			UserAction = userAction;
		}

		public SVNMonitor.Helpers.UserAction UserAction { get; private set; }
	}
}