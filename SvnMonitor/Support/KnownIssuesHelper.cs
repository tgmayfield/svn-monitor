﻿namespace SVNMonitor.Support
{
    using SVNMonitor.Helpers;
    using SVNMonitor.Logging;
    using SVNMonitor.Settings;
    using SVNMonitor.Web;
    using System;
    using System.IO;
    using System.Reflection;

    internal class KnownIssuesHelper
    {
        private const int NotKnownIssue = -1;

        private static int GetEmbeddedKnownIssue(Exception ex)
        {
            int CS$1$0000;
            using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("SVNMonitor.Support.KnownIssues.xml"))
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    string xml = reader.ReadToEnd();
                    CS$1$0000 = GetKnownIssueFromXml(ex, xml);
                }
            }
            return CS$1$0000;
        }

        private static int GetKnownIssueFromXml(Exception exception, string xml)
        {
            if (!string.IsNullOrEmpty(xml))
            {
                try
                {
                    string exceptionFullName = exception.GetType().FullName;
                    string exceptionStackTrace = exception.StackTrace;
                    foreach (Issue issue in SerializationHelper.XmlDeserialize<IssuesCollection>(xml).List)
                    {
                        if (exceptionStackTrace.Contains(issue.StackFrame) && (string.IsNullOrEmpty(issue.ExceptionName) || exceptionFullName.Equals(issue.ExceptionName, StringComparison.CurrentCultureIgnoreCase)))
                        {
                            Logger.Log.DebugFormat("Known issue found: " + issue.ID, new object[0]);
                            return issue.ID;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Logger.Log.DebugFormat("Error checking for known issue: {0}", ex.ToString());
                }
            }
            return -1;
        }

        private static int GetWebKnownIssue(Exception ex)
        {
            string xml = SharpRegion.TryGetKnownIssues(ApplicationSettingsManager.Settings.InstanceID, ex);
            return GetKnownIssueFromXml(ex, xml);
        }

        public static bool IsKnownIssue(Exception ex)
        {
            return IsKnownIssue(ex, false);
        }

        public static bool IsKnownIssue(Exception ex, bool checkOnLine)
        {
            if (GetEmbeddedKnownIssue(ex) != -1)
            {
                return true;
            }
            if (!checkOnLine)
            {
                return false;
            }
            return (GetWebKnownIssue(ex) != -1);
        }
    }
}

