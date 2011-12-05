using System.Collections.Generic;

namespace SVNMonitor.View.Interfaces
{
public interface ISearchable
{
	IEnumerable<string> GetSearchKeywords();
}
}