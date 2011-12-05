using System;
using SVNMonitor.Logging;
using SVNMonitor;
using SVNMonitor.Entities;
using SharpSvn;

namespace SVNMonitor.Helpers
{
internal class ErrorHandler
{
	public ErrorHandler()
	{
	}

	public static long Append(string message, object sourceObject, Exception exception)
	{
		Logger.Log.Error(message, exception);
		long id = EventLog.LogError(message, sourceObject, exception);
		return id;
	}

	public static void HandleEntityError(UserEntity entity, string errorText)
	{
		ErrorHandler.HandleEntityError(entity, errorText, null);
	}

	public static void HandleEntityError(UserEntity entity, string errorText, Exception ex)
	{
		long errorID = ErrorHandler.Append(errorText, entity, ex);
		if (entity.IsAlive)
		{
			entity.SetError(errorText, errorID);
		}
	}

	public static void HandleEntityException(UserEntity entity, Exception ex)
	{
		string message = string.Format("{0}: {1}", entity.Name, ex.Message);
		Logger.Log.Error(ex.ToString());
		ErrorHandler.HandleEntityError(entity, message, ex);
	}

	public static void HandleSourceSVNException(Source source, SvnException svnex)
	{
		if (source != null)
		{
			string message = string.Format("{0}: {1}", source.Name, svnex.Message);
			Logger.Log.Error(message);
			if (source.IsAlive)
			{
				long errorID = ErrorHandler.Append(message, source, svnex);
				source.SetError(message, errorID);
				return;
			}
		}
		else
		{
			Logger.Log.Error(svnex.ToString());
		}
	}
}
}