using SVNMonitor.View.Controls;
using System;
using System.Collections.Generic;

namespace SVNMonitor.View.Interfaces
{
public interface ISearchablePanel<T>
{
	SearchTextBox<T> SearchTextBox { get; set; }

	void ClearSearch();

	IEnumerable<T> GetAllItems();

	void SetSearchResults(IEnumerable<T> results);
}
}