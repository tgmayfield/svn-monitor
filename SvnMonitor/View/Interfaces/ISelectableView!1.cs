namespace SVNMonitor.View.Interfaces
{
    using System;

    public interface ISelectableView<T>
    {
        object Invoke(Delegate method);
        object Invoke(Delegate method, params object[] args);

        bool InvokeRequired { get; }

        T SelectedItem { get; }
    }
}

