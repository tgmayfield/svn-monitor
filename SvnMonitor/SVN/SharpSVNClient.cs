using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net;
using System.Text;
using System.Windows.Forms;

using SVNMonitor.Entities;
using SVNMonitor.Settings;

using SharpSvn;
using SharpSvn.UI;

namespace SVNMonitor.SVN
{
	internal class SharpSVNClient
	{
		private const string RecommendProperty = "svnmonitor:recommend";

		public static void CommitRecommend(Source source, long revision, string logMessage)
		{
			source.RefreshRecommended();
			List<long> list = new List<long>();
			list.AddRange(source.ReadOnlyRecommendedRevisions);
			if (!list.Contains(revision))
			{
				list.Add(revision);
				list.Sort();
				StringBuilder sb = new StringBuilder();
				foreach (long rev in list)
				{
					sb.AppendLine(rev.ToString());
				}
				using (SvnClient client = GetSvnClient(source))
				{
					SvnCommitArgs args = new SvnCommitArgs();
					args.LogProperties.Add("svnmonitor:recommend", sb.ToString());
					args.ThrowOnError = true;
					args.LogMessage = logMessage;
					args.Depth = SvnDepth.Empty;
					client.Commit(source.Path, args);
				}
			}
		}

		public static void GetInfo(Source source, out Collection<SvnInfoEventArgs> collection)
		{
			using (SvnClient client = GetSvnClient(source))
			{
				SvnInfoArgs args = new SvnInfoArgs
				{
					ThrowOnError = true
				};
				client.GetInfo(SvnTarget.FromString(source.Path), args, out collection);
			}
		}

		public static void GetLog(Source source, long startRevision, out Collection<SvnLogEventArgs> collection)
		{
			SvnLogArgs args = new SvnLogArgs
			{
				Limit = ApplicationSettingsManager.Settings.LogEntriesPageSize,
				ThrowOnError = true
			};
			if (startRevision >= 0L)
			{
				args.Start = new SvnRevision(startRevision);
				args.End = new SvnRevision(SvnRevisionType.Head);
			}
			using (SvnClient client = GetSvnClient(source))
			{
				if (source.IsURL)
				{
					client.GetLog(new Uri(source.Path), args, out collection);
				}
				else
				{
					client.GetLog(source.Path, args, out collection);
				}
			}
		}

		public static List<long> GetRecommendedRevisions(Source source)
		{
			List<long> list = new List<long>();
			string value = string.Empty;
			using (SvnClient client = GetSvnClient())
			{
				SvnGetPropertyArgs args = new SvnGetPropertyArgs
				{
					ThrowOnError = false
				};
				SvnTargetPropertyCollection collection = new SvnTargetPropertyCollection();
				client.GetProperty(source.Path, "svnmonitor:recommend", args, out collection);
				if ((collection != null) && (collection.Count == 1))
				{
					SvnPropertyValue svnPropertyValue = collection[0];
					value = svnPropertyValue.StringValue;
				}
			}
			GetRevisionsFromString(value, list);
			return list;
		}

		public static List<long> GetRemoteRecommendedRevisions(Source source)
		{
			List<long> retList = new List<long>();
			using (SvnClient client = GetSvnClient(source))
			{
				SVNInfo info = source.GetInfo(true);
				if (info == null)
				{
					return null;
				}
				string value = string.Empty;
				string url = info.URL;
				client.GetRevisionProperty(new Uri(url), new SvnRevision(source.Revision), "svnmonitor:recommend", out value);
				if (value != null)
				{
					GetRevisionsFromString(value, retList);
				}
			}
			return retList;
		}

		private static void GetRevisionsFromString(string revisionsString, List<long> list)
		{
			foreach (string revisionString in revisionsString.Split(new[]
			{
				Environment.NewLine, " ", ";", ",", ".", "/"
			}, StringSplitOptions.RemoveEmptyEntries))
			{
				long revision;
				if (long.TryParse(revisionString, out revision))
				{
					list.Add(revision);
				}
			}
		}

		public static void GetStatus(Source source, bool getRemote, bool getAll, out Collection<SvnStatusEventArgs> collection)
		{
			SvnStatusArgs args = new SvnStatusArgs
			{
				RetrieveAllEntries = getAll,
				RetrieveRemoteStatus = getRemote
			};
			using (SvnClient client = GetSvnClient(source))
			{
				client.GetStatus(source.Path, args, out collection);
			}
		}

		private static SvnClient GetSvnClient()
		{
			return new SvnClient();
		}

		private static SvnClient GetSvnClient(Source source)
		{
			SvnClient client = GetSvnClient();
			SetAuthentication(client, source);
			return client;
		}

		public static bool IsWorkingCopy(string path)
		{
			using (SvnClient client = GetSvnClient())
			{
				return (client.GetUriFromWorkingCopy(path) != null);
			}
		}

		public static void Recommend(Source source, long revision)
		{
			List<long> revisions = new List<long>();
			revisions.AddRange(source.ReadOnlyRecommendedRevisions);
			if (!revisions.Contains(revision))
			{
				revisions.Add(revision);
				revisions.Sort();
				SetRecommendedRevisions(source, revisions);
			}
		}

		private static void SetAuthentication(SvnClient client, Source source)
		{
			if (source.Authenticate)
			{
				SetAuthentication(client, source.UserName, source.Password);
			}
			SvnUI.Bind(client, (IWin32Window)null);
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
			using (SvnClient client = GetSvnClient())
			{
				if ((revisions == null) || (revisions.Count == 0))
				{
					client.DeleteProperty(source.Path, "svnmonitor:recommend");
				}
				else
				{
					client.SetProperty(source.Path, "svnmonitor:recommend", sb.ToString());
				}
			}
		}

		public static void UndoRecommend(Source source, long revision)
		{
			List<long> revisions = GetRecommendedRevisions(source);
			if (revisions.Contains(revision))
			{
				revisions.Remove(revision);
				revisions.Sort();
				SetRecommendedRevisions(source, revisions);
			}
		}

		public static void Update(string path, string userName, string password)
		{
			using (SvnClient client = GetSvnClient())
			{
				SetAuthentication(client, userName, password);
				client.Update(path);
			}
		}
	}
}