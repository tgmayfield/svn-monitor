namespace SVNMonitor.Helpers
{
    using System;
    using System.Runtime.CompilerServices;

    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Method, AllowMultiple=false)]
    public class AssociatedUserActionAttribute : Attribute
    {
        public AssociatedUserActionAttribute(SVNMonitor.Helpers.UserAction userAction)
        {
            this.UserAction = userAction;
        }

        public SVNMonitor.Helpers.UserAction UserAction { get; private set; }
    }
}

