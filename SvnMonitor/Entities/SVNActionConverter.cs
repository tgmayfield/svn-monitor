namespace SVNMonitor.Entities
{
    using SharpSvn;
    using System;

    internal static class SVNActionConverter
    {
        public static SVNAction ToSVNAction(SvnChangeAction action)
        {
            SvnChangeAction CS$0$0000 = action;
            if (CS$0$0000 <= SvnChangeAction.Add)
            {
                if ((CS$0$0000 != SvnChangeAction.None) && (CS$0$0000 == SvnChangeAction.Add))
                {
                    return SVNAction.Added;
                }
            }
            else
            {
                switch (CS$0$0000)
                {
                    case SvnChangeAction.Delete:
                        return SVNAction.Deleted;

                    case SvnChangeAction.Modify:
                        return SVNAction.Modified;

                    case SvnChangeAction.Replace:
                        return SVNAction.Replaced;
                }
            }
            return SVNAction.Added;
        }
    }
}

