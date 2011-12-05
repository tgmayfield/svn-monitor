using System;

namespace SVNMonitor.View.Interfaces
{
	public interface ISelectableView<T>
	{
		object Invoke(Delegate method);
		object Invoke(Delegate method, params object[] args);

		bool InvokeRequired { get; }

		T SelectedItem { get; }
	}
}