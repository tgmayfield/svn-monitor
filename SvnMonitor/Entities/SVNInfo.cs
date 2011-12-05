using System;
using System.Web;

using SVNMonitor.Extensions;
using SVNMonitor.Helpers;
using SVNMonitor.Logging;
using SVNMonitor.Resources.Text;
using SVNMonitor.SVN;

namespace SVNMonitor.Entities
{
	[Serializable]
	public class SVNInfo
	{
		internal static SVNInfo Create(SVNMonitor.Entities.Source source)
		{
			SVNInfo info = null;
			try
			{
				info = SVNFactory.GetInfo(source);
			}
			catch (Exception ex)
			{
				ErrorHandler.Append(Strings.ErrorGettingInfoOfSource_FORMAT.FormatWith(new object[]
				{
					source.Name
				}), source, ex);
			}
			return info;
		}

		public override string ToString()
		{
			return HttpUtility.UrlDecode(string.Format("Path: {0}{1}URL: {2}{1}Root: {3}", new object[]
			{
				Path, Environment.NewLine, URL, RepositoryRoot
			}));
		}

		internal void Update()
		{
			if (Source != null)
			{
				Logger.Log.DebugFormat("svnFactory.GetInfo(source={0})", Source);
				SVNInfo newInfo = SVNFactory.GetInfo(Source);
				if (newInfo != null)
				{
					URL = newInfo.URL;
					RepositoryRoot = newInfo.RepositoryRoot;
					Revision = newInfo.Revision;
					LastChangedAuthor = newInfo.LastChangedAuthor;
					LastChangedDate = newInfo.LastChangedDate;
				}
			}
		}

		public string LastChangedAuthor { get; set; }

		public DateTime LastChangedDate { get; set; }

		public string Path { get; set; }

		public string RepositoryRoot { get; set; }

		public long Revision { get; set; }

		public SVNMonitor.Entities.Source Source { get; set; }

		public string URL { get; set; }
	}
}