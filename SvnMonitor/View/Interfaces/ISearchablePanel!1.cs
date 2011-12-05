using System;
using System.Collections.Generic;

using SVNMonitor.View.Controls;

namespace SVNMonitor.View.Interfaces
{
	public interface ISearchablePanel<T>
		where T : ISearchable
	{
		void ClearSearch();
		IEnumerable<T> GetAllItems();
		void SetSearchResults(IEnumerable<T> results);

		SearchTextBox<T> SearchTextBox { get; set; }
	}
}