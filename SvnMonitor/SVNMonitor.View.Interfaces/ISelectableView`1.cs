using System;

namespace SVNMonitor.View.Interfaces
{
public interface ISelectableView<T>
{
	bool InvokeRequired { get; }

	T SelectedItem { get; }

	object Invoke(Delegate method);

	object Invoke(Delegate method, object[] args);
}
}