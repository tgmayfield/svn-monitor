using System;

namespace SVNMonitor.Helpers
{
[AttributeUsage(AttributeTargets.Method | AttributeTargets.Property, AllowMultiple=false)]
public class AssociatedUserActionAttribute : Attribute
{
	public UserAction UserAction
	{
		get;
		private set;
	}

	public AssociatedUserActionAttribute(UserAction userAction)
	{
		this.UserAction = userAction;
	}
}
}