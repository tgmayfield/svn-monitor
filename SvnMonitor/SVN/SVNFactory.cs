namespace SVNMonitor.SVN
{
    using SharpSvn;
    using SharpSvn.Implementation;
    using SVNMonitor.Entities;
    using SVNMonitor.Helpers;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    internal class SVNFactory
    {
        internal static SVNInfo GetInfo(Source source)
        {
            Collection<SvnInfoEventArgs> collection;
            SharpSVNClient.GetInfo(source, out collection);
            SVNInfo info = SharpSVNToSVNInfo(source, collection);
            source.SetInfo(info);
            return info;
        }

        internal static SVNLog GetLog(Source source)
        {
            return GetLog(source, -1L);
        }

        internal static SVNLog GetLog(Source source, long startRevision)
        {
            Collection<SvnLogEventArgs> collection;
            SharpSVNClient.GetLog(source, startRevision, out collection);
            SVNLog log = new SVNLog();
            if ((collection.Count != 1) || (collection[0].Revision != source.Revision))
            {
                SVNInfo info = GetInfo(source);
                Collection<SvnStatusEventArgs> statusCollection = null;
                if (!source.IsURL)
                {
                    statusCollection = GetStatusCollection(source, false, true);
                }
                log = SharpSVNToSVNLog(source, collection, info, statusCollection);
            }
            log.Source = source;
            return log;
        }

        internal static SVNStatus GetStatus(Source source, bool getRemote)
        {
            if (source.IsURL)
            {
                return null;
            }
            Collection<SvnStatusEventArgs> collection = GetStatusCollection(source, getRemote, false);
            return SharpSVNToSVNStatus(source, collection);
        }

        private static Collection<SvnStatusEventArgs> GetStatusCollection(Source source, bool getRemote, bool getAll)
        {
            Collection<SvnStatusEventArgs> collection;
            SharpSVNClient.GetStatus(source, getRemote, getAll, out collection);
            return collection;
        }

        private static Dictionary<string, SvnStatusEventArgs> GetStatusMap(Collection<SvnStatusEventArgs> collection)
        {
            Dictionary<string, SvnStatusEventArgs> map = new Dictionary<string, SvnStatusEventArgs>();
            if (collection != null)
            {
                foreach (SvnStatusEventArgs item in collection)
                {
                    Uri uri = item.Uri;
                    if (uri != null)
                    {
                        map[uri.ToString()] = item;
                    }
                }
            }
            return map;
        }

        internal static SVNLog GetUpdates(Source source)
        {
            return GetLog(source, source.Revision);
        }

        public static bool IsWorkingCopy(string path)
        {
            return SharpSVNClient.IsWorkingCopy(path);
        }

        private static SVNInfo SharpSVNToSVNInfo(Source source, Collection<SvnInfoEventArgs> collection)
        {
            SVNInfo info = new SVNInfo {
                Source = source
            };
            if ((collection != null) || (collection.Count > 0))
            {
                SvnInfoEventArgs arg = collection[0];
                info.LastChangedAuthor = arg.LastChangeAuthor;
                info.LastChangedDate = arg.LastChangeTime;
                info.Path = arg.Path;
                info.RepositoryRoot = arg.RepositoryRoot.ToString();
                info.Revision = arg.Revision;
                info.URL = arg.Uri.ToString();
            }
            return info;
        }

        private static SVNLog SharpSVNToSVNLog(Source source, Collection<SvnLogEventArgs> collection, SVNInfo info, Collection<SvnStatusEventArgs> statusCollection)
        {
            SVNLog log = new SVNLog();
            Dictionary<string, SvnStatusEventArgs> statusMap = GetStatusMap(statusCollection);
            foreach (SvnLogEventArgs logItem in collection)
            {
                DateTime localTime = logItem.Time.ToLocalTime();
                SVNLogEntry entry = new SVNLogEntry(source, logItem.Revision, logItem.Author, localTime, logItem.LogMessage);
                SvnChangeItemCollection items = logItem.ChangedPaths;
                if (items != null)
                {
                    List<SVNPath> paths = new List<SVNPath>();
                    foreach (SvnChangeItem item in items)
                    {
                        SVNPath path = new SVNPath(entry, SVNActionConverter.ToSVNAction(item.Action), item.Path);
                        string pathName = item.Path;
                        if (pathName.StartsWith("/"))
                        {
                            pathName = pathName.Substring(1);
                        }
                        string pathUri = info.RepositoryRoot + pathName;
                        path.Uri = pathUri;
                        if (statusMap.ContainsKey(pathUri))
                        {
                            SvnStatusEventArgs statusItem = statusMap[pathUri];
                            path.FilePath = statusItem.FullPath;
                        }
                        else
                        {
                            string filePath = SVNPath.GuessFilePath(entry, path.Name, info);
                            if (FileSystemHelper.Exists(filePath))
                            {
                                path.FilePath = filePath;
                            }
                            else
                            {
                                path.FilePath = pathUri;
                            }
                        }
                        paths.Add(path);
                    }
                    entry.Paths = paths;
                    log.LogEntries.Add(entry);
                }
            }
            return log;
        }

        private static SVNStatus SharpSVNToSVNStatus(Source source, Collection<SvnStatusEventArgs> collection)
        {
            SVNStatus status = new SVNStatus {
                Source = source
            };
            List<SVNStatusEntry> list = new List<SVNStatusEntry>();
            foreach (SvnStatusEventArgs statusItem in collection)
            {
                SVNStatusEntry entry = new SVNStatusEntry(status);
                if (statusItem.WorkingCopyInfo != null)
                {
                    entry.ChangeList = statusItem.WorkingCopyInfo.ChangeList;
                    entry.WorkingCopyRevision = statusItem.WorkingCopyInfo.Revision;
                }
                entry.Path = statusItem.Path;
                if (statusItem.Uri != null)
                {
                    entry.Uri = statusItem.Uri.ToString();
                }
                else
                {
                    entry.Uri = statusItem.Path;
                }
                entry.RepositoryStatus = statusItem.RemotePropertyStatus;
                entry.WorkingCopyStatus = statusItem.LocalContentStatus;
                list.Add(entry);
            }
            status.Entries = list;
            return status;
        }

        public static void Update(string path, string userName, string password)
        {
            SharpSVNClient.Update(path, userName, password);
        }
    }
}

