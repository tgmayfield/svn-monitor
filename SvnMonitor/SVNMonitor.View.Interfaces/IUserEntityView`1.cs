using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SVNMonitor.View.Interfaces
{
internal interface IUserEntityView<T> : ISelectableView<T>
{
	bool CanDelete { get; set; }

	bool CanEdit { get; set; }

	bool CanMoveDown { get; set; }

	bool CanMoveUp { get; set; }

	bool CanNew { get; set; }

	int Count { get; }

	List<T> Entities { get; set; }

	int SelectedIndex { get; set; }

	bool ShowingAllItems { get; }

	void Delete();

	void EnableCommands();

	void Refetch();

	DialogResult UserEdit(T entity);

	DialogResult UserNew(T entity);

	event EventHandler SelectionChanged;
}
}