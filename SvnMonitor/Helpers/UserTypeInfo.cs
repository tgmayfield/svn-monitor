using System;

namespace SVNMonitor.Helpers
{
	internal class UserTypeInfo : IComparable
	{
		public int CompareTo(object obj)
		{
			UserTypeInfo that = (UserTypeInfo)obj;
			return DisplayName.CompareTo(that.DisplayName);
		}

		public override string ToString()
		{
			return DisplayName;
		}

		public string DisplayName { get; set; }

		public System.Type Type { get; set; }
	}
}