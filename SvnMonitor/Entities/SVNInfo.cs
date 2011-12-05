using System;
using SVNMonitor.SVN;
using SVNMonitor.Helpers;
using SVNMonitor.Resources.Text;
using System.Web;
using SVNMonitor.Logging;

namespace SVNMonitor.Entities
{
[Serializable]
public class SVNInfo
{
	public string LastChangedAuthor
	{
		get;
		set;
	}

	public DateTime LastChangedDate
	{
		get;
		set;
	}

	public string Path
	{
		get;
		set;
	}

	public string RepositoryRoot
	{
		get;
		set;
	}

	public long Revision
	{
		get;
		set;
	}

	public Source Source
	{
		get;
		set;
	}

	public string URL
	{
		get;
		set;
	}

	public SVNInfo()
	{
	}

	internal static SVNInfo Create(Source source)
	{
		object[] objArray;
		SVNInfo info = null;
		try
		{
			return SVNFactory.GetInfo(source);
		}
		catch (Exception ex)
		{
			ErrorHandler.Append(Strings.ErrorGettingInfoOfSource_FORMAT.FormatWith(new object[] { source.Name }), source, ex);
		}
		return info;
	}

	public override string ToString()
	{
		object[] objArray;
		string str = string.Format("Path: {0}{1}URL: {2}{1}Root: {3}", new object[] { this.Path, Environment.NewLine, this.URL, this.RepositoryRoot });
		string decoded = HttpUtility.UrlDecode(str);
		return decoded;
	}

	internal void Update()
	{
		if (this.Source == null)
		{
			return;
		}
		Logger.Log.DebugFormat("svnFactory.GetInfo(source={0})", this.Source);
		SVNInfo newInfo = SVNFactory.GetInfo(this.Source);
		if (newInfo == null)
		{
			return;
		}
		this.URL = newInfo.URL;
		this.RepositoryRoot = newInfo.RepositoryRoot;
		this.Revision = newInfo.Revision;
		this.LastChangedAuthor = newInfo.LastChangedAuthor;
		this.LastChangedDate = newInfo.LastChangedDate;
	}
}
}