namespace SVNMonitor.View.Interfaces
{
    using System.Collections.Generic;

    public interface ISearchable
    {
        IEnumerable<string> GetSearchKeywords();
    }
}

