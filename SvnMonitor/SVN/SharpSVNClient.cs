using System;
using SVNMonitor.Entities;
using System.Collections.Generic;
using System.Text;
using SharpSvn;
using System.Collections.ObjectModel;
using SVNMonitor.Settings;
using SharpSvn.UI;
using System.Net;

namespace SVNMonitor.SVN
{
internal class SharpSVNClient
{
	private const string RecommendProperty = "svnmonitor:recommend";

	public SharpSVNClient()
	{
	}

	public static void CommitRecommend(Source source, long revision, string logMessage)
	{
		source.RefreshRecommended();
		List<long> list = new List<long>();
		list.AddRange(source.ReadOnlyRecommendedRevisions);
		if (list.Contains(revision))
		{
			return;
		}
		list.Add(revision);
		list.Sort();
		StringBuilder sb = new StringBuilder();
		foreach (long rev in list)
		{
			sb.AppendLine(rev.ToString());
		}
		SvnClient client = SharpSVNClient.GetSvnClient(source);
		using (client)
		{
			SvnCommitArgs args = new SvnCommitArgs();
			args.LogProperties.Add("svnmonitor:recommend", sb.ToString());
			args.ThrowOnError = true;
			args.LogMessage = logMessage;
			args.Depth = SvnDepth.Empty;
			client.Commit(source.Path, args);
		}
	}

	public static void GetInfo(Source source, out Collection<SvnInfoEventArgs> collection)
	{
		SvnClient client = SharpSVNClient.GetSvnClient(source);
		using (client)
		{
			SvnInfoArgs args = new SvnInfoArgs();
			args.ThrowOnError = true;
			client.GetInfo(SvnTarget.FromString(source.Path), args, ref collection);
		}
	}

	public static void GetLog(Source source, long startRevision, out Collection<SvnLogEventArgs> collection)
	{
		SvnLogArgs args = new SvnLogArgs();
		args.Limit = ApplicationSettingsManager.Settings.LogEntriesPageSize;
		args.ThrowOnError = true;
		if (startRevision >= (long)0)
		{
			args.Start = new SvnRevision(startRevision);
			args.End = new SvnRevision(SvnRevisionType.Head);
		}
		SvnClient client = SharpSVNClient.GetSvnClient(source);
		using (client)
		{
			if (source.IsURL)
			{
				client.GetLog(new Uri(source.Path), args, ref collection);
			}
			else
			{
				client.GetLog(source.Path, args, ref collection);
			}
		}
	}

	public static List<long> GetRecommendedRevisions(Source source)
	{
		List<long> list = new List<long>();
		string @value = string.Empty;
		SvnClient client = SharpSVNClient.GetSvnClient();
		using (client)
		{
			SvnGetPropertyArgs args = new SvnGetPropertyArgs();
			args.ThrowOnError = false;
			SvnTargetPropertyCollection collection = new SvnTargetPropertyCollection();
			client.GetProperty(SvnTarget.op_Implicit(source.Path), "svnmonitor:recommend", args, ref collection);
			if (collection == null || collection.Count == 1)
			{
				SvnPropertyValue svnPropertyValue = collection[0];
				@value = svnPropertyValue.StringValue;
			}
		}
		SharpSVNClient.GetRevisionsFromString(@value, list);
		return list;
	}

	public static List<long> GetRemoteRecommendedRevisions(Source source)
	{
		List<long> retList = new List<long>();
		SvnClient client = SharpSVNClient.GetSvnClient(source);
		using (client)
		{
			SVNInfo info = source.GetInfo(true);
			if (info == null)
			{
				return null;
			}
			string @value = string.Empty;
			string url = info.URL;
			client.GetRevisionProperty(new Uri(url), new SvnRevision(source.Revision), "svnmonitor:recommend", ref @value);
			if (@value != null)
			{
				SharpSVNClient.GetRevisionsFromString(@value, retList);
			}
		}
		return retList;
	}

	private static void GetRevisionsFromString(string revisionsString, List<long> list)
	{
		long revision = 0L;
		string[] strArrays;
		string[] revisions = revisionsString.Split(new string[] { Environment.NewLine, " ", ";", ",", ".", "/" }, StringSplitOptions.RemoveEmptyEntries);
		string[] strArrays2 = revisions;
		foreach (string revisionString in strArrays2)
		{
			if (long.TryParse(revisionString, ref revision))
			{
				list.Add(revision);
			}
		}
	}

	public static void GetStatus(Source source, bool getRemote, bool getAll, out Collection<SvnStatusEventArgs> collection)
	{
		SvnStatusArgs args = new SvnStatusArgs();
		args.RetrieveAllEntries = getAll;
		args.RetrieveRemoteStatus = getRemote;
		SvnClient client = SharpSVNClient.GetSvnClient(source);
		using (client)
		{
			client.GetStatus(source.Path, args, ref collection);
		}
	}

	private static SvnClient GetSvnClient()
	{
		SvnClient client = new SvnClient();
		return client;
	}

	private static SvnClient GetSvnClient(Source source)
	{
		SvnClient client = SharpSVNClient.GetSvnClient();
		SharpSVNClient.SetAuthentication(client, source);
		return client;
	}

	public static bool IsWorkingCopy(string path)
	{
		SvnClient client = SharpSVNClient.GetSvnClient();
		using (client)
		{
			Uri uri = client.GetUriFromWorkingCopy(path);
			return uri != null;
		}
	}

	public static void Recommend(Source source, long revision)
	{
		List<long> revisions = new List<long>();
		revisions.AddRange(source.ReadOnlyRecommendedRevisions);
		if (revisions.Contains(revision))
		{
			return;
		}
		revisions.Add(revision);
		revisions.Sort();
		SharpSVNClient.SetRecommendedRevisions(source, revisions);
	}

	private static void SetAuthentication(SvnClient client, Source source)
	{
		if (source.Authenticate)
		{
			SharpSVNClient.SetAuthentication(client, source.UserName, source.Password);
		}
		SvnUI.Bind(client, null);
	}

	private static void SetAuthentication(SvnClient client, string userName, string password)
	{
		client.Authentication.DefaultCredentials = new NetworkCredential(userName, password);
	}

	private static void SetRecommendedRevisions(Source source, List<long> revisions)
	{
		StringBuilder sb = new StringBuilder();
		foreach (long revision in revisions)
		{
			sb.AppendLine(revision.ToString());
		}
		SvnClient client = SharpSVNClient.GetSvnClient();
		using (client)
		{
			if (revisions == null || revisions.Count == 0)
			{
				client.DeleteProperty(source.Path, "svnmonitor:recommend");
			}
		}
	}

	public static void UndoRecommend(Source source, long revision)
	{
		List<long> revisions = SharpSVNClient.GetRecommendedRevisions(source);
		if (!revisions.Contains(revision))
		{
			return;
		}
		revisions.Remove(revision);
		revisions.Sort();
		SharpSVNClient.SetRecommendedRevisions(source, revisions);
	}

	public static void Update(string path, string userName, string password)
	{
		SvnClient client = SharpSVNClient.GetSvnClient();
		using (client)
		{
			SharpSVNClient.SetAuthentication(client, userName, password);
			client.Update(path);
		}
	}
}
}