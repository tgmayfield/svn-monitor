namespace SVNMonitor.View.Interfaces
{
    using SVNMonitor.View.Controls;
    using System;
    using System.Collections.Generic;

    public interface ISearchablePanel<T> where T: ISearchable
    {
        void ClearSearch();
        IEnumerable<T> GetAllItems();
        void SetSearchResults(IEnumerable<T> results);

        SearchTextBox<T> SearchTextBox { get; set; }
    }
}

