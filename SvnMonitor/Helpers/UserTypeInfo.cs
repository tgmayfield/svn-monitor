using System;

namespace SVNMonitor.Helpers
{
internal class UserTypeInfo : IComparable
{
	public string DisplayName
	{
		get;
		set;
	}

	public Type Type
	{
		get;
		set;
	}

	public UserTypeInfo()
	{
	}

	public int CompareTo(object obj)
	{
		UserTypeInfo that = (UserTypeInfo)obj;
		return this.DisplayName.CompareTo(that.DisplayName);
	}

	public override string ToString()
	{
		return this.DisplayName;
	}
}
}