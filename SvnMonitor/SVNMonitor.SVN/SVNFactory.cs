using SVNMonitor.Entities;
using System.Collections.ObjectModel;
using System;
using System.Collections.Generic;
using SharpSvn;
using SharpSvn.Implementation;
using SVNMonitor.Helpers;

namespace SVNMonitor.SVN
{
internal class SVNFactory
{
	public SVNFactory()
	{
	}

	internal static SVNInfo GetInfo(Source source)
	{
		Collection<SvnInfoEventArgs> collection = null;
		SharpSVNClient.GetInfo(source, out collection);
		SVNInfo info = SVNFactory.SharpSVNToSVNInfo(source, collection);
		source.SetInfo(info);
		return info;
	}

	internal static SVNLog GetLog(Source source)
	{
		SVNLog log = SVNFactory.GetLog(source, (long)-1);
		return log;
	}

	internal static SVNLog GetLog(Source source, long startRevision)
	{
		Collection<SvnLogEventArgs> collection = null;
		SharpSVNClient.GetLog(source, startRevision, out collection);
		SVNLog log = new SVNLog();
		if (collection.Count != 1 || collection[0].Revision != source.Revision)
		{
			SVNInfo info = SVNFactory.GetInfo(source);
			Collection<SvnStatusEventArgs> statusCollection = null;
			if (!source.IsURL)
			{
				statusCollection = SVNFactory.GetStatusCollection(source, false, true);
			}
			log = SVNFactory.SharpSVNToSVNLog(source, collection, info, statusCollection);
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
		Collection<SvnStatusEventArgs> collection = SVNFactory.GetStatusCollection(source, getRemote, false);
		SVNStatus status = SVNFactory.SharpSVNToSVNStatus(source, collection);
		return status;
	}

	private static Collection<SvnStatusEventArgs> GetStatusCollection(Source source, bool getRemote, bool getAll)
	{
		SharpSVNClient.GetStatus(source, getRemote, getAll, out collection);
	}

	private static Dictionary<string, SvnStatusEventArgs> GetStatusMap(Collection<SvnStatusEventArgs> collection)
	{
		Dictionary<string, SvnStatusEventArgs> map = new Dictionary<string, SvnStatusEventArgs>();
		if (collection == null)
		{
			break;
		}
		foreach (SvnStatusEventArgs item in collection)
		{
			Uri uri = item.Uri;
			if (uri != null)
			{
				map.Item = uri.ToString();
			}
		}
		return map;
	}

	internal static SVNLog GetUpdates(Source source)
	{
		SVNLog log = SVNFactory.GetLog(source, source.Revision);
		return log;
	}

	public static bool IsWorkingCopy(string path)
	{
		bool isWorkingCopy = SharpSVNClient.IsWorkingCopy(path);
		return isWorkingCopy;
	}

	private static SVNInfo SharpSVNToSVNInfo(Source source, Collection<SvnInfoEventArgs> collection)
	{
		SVNInfo info = new SVNInfo();
		info.Source = source;
		if (collection != null || collection.Count > 0)
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
		Dictionary<string, SvnStatusEventArgs> statusMap = SVNFactory.GetStatusMap(statusCollection);
		foreach (SvnLogEventArgs logItem in collection)
		{
			DateTime time = logItem.Time;
			DateTime localTime = time.ToLocalTime();
			SVNLogEntry entry = new SVNLogEntry(source, logItem.Revision, logItem.Author, localTime, logItem.LogMessage);
			SvnChangeItemCollection items = logItem.ChangedPaths;
			if (items == null)
			{
				break;
			}
			List<SVNPath> paths = new List<SVNPath>();
			foreach (SvnChangeItem item in items)
			{
				SVNPath path = new SVNPath(entry, SVNActionConverter.ToSVNAction(item.Action), item.Path);
				string pathName = item.Path;
				if (pathName.StartsWith("/"))
				{
					pathName = pathName.Substring(1);
				}
				string pathUri = string.Concat(info.RepositoryRoot, pathName);
				path.Uri = pathUri;
				if (statusMap.ContainsKey(pathUri))
				{
					SvnStatusEventArgs statusItem = statusMap[pathUri];
					path.FilePath = statusItem.FullPath;
					continue;
				}
				string filePath = SVNPath.GuessFilePath(entry, path.Name, info);
				if (FileSystemHelper.Exists(filePath))
				{
					path.FilePath = filePath;
					continue;
				}
				path.FilePath = pathUri;
				paths.Add(path);
			}
			entry.Paths = paths;
			log.LogEntries.Add(entry);
		}
		return log;
	}

	private static SVNStatus SharpSVNToSVNStatus(Source source, Collection<SvnStatusEventArgs> collection)
	{
		SVNStatus status = new SVNStatus();
		status.Source = source;
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
				continue;
			}
			entry.Uri = statusItem.Path;
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