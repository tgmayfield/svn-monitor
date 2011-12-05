namespace SVNMonitor.Helpers
{
    using System;
    using System.Runtime.CompilerServices;

    internal class UserTypeInfo : IComparable
    {
        public int CompareTo(object obj)
        {
            UserTypeInfo that = (UserTypeInfo) obj;
            return this.DisplayName.CompareTo(that.DisplayName);
        }

        public override string ToString()
        {
            return this.DisplayName;
        }

        public string DisplayName { get; set; }

        public System.Type Type { get; set; }
    }
}

