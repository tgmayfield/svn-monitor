namespace SVNMonitor.Helpers
{
    using SharpSvn;
    using SVNMonitor;
    using SVNMonitor.Entities;
    using SVNMonitor.Logging;
    using System;

    internal class ErrorHandler
    {
        public static long Append(string message, object sourceObject, Exception exception)
        {
            Logger.Log.Error(message, exception);
            return EventLog.LogError(message, sourceObject, exception);
        }

        public static void HandleEntityError(UserEntity entity, string errorText)
        {
            HandleEntityError(entity, errorText, null);
        }

        public static void HandleEntityError(UserEntity entity, string errorText, Exception ex)
        {
            long errorID = Append(errorText, entity, ex);
            if (entity.IsAlive)
            {
                entity.SetError(errorText, errorID);
            }
        }

        public static void HandleEntityException(UserEntity entity, Exception ex)
        {
            string message = string.Format("{0}: {1}", entity.Name, ex.Message);
            Logger.Log.Error(ex.ToString());
            HandleEntityError(entity, message, ex);
        }

        public static void HandleSourceSVNException(Source source, SvnException svnex)
        {
            if (source != null)
            {
                string message = string.Format("{0}: {1}", source.Name, svnex.Message);
                Logger.Log.Error(message);
                if (source.IsAlive)
                {
                    long errorID = Append(message, source, svnex);
                    source.SetError(message, errorID);
                }
            }
            else
            {
                Logger.Log.Error(svnex.ToString());
            }
        }
    }
}

