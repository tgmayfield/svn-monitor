using SVNMonitor.Entities;

namespace SVNMonitor.View.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;

    internal interface IUserEntityView<T> : ISelectableView<T> where T: UserEntity
    {
        event EventHandler SelectionChanged;

        void Delete();
        void EnableCommands();
        void Refetch();
        DialogResult UserEdit(T entity);
        DialogResult UserNew(T entity);

        bool CanDelete { get; set; }

        bool CanEdit { get; set; }

        bool CanMoveDown { get; set; }

        bool CanMoveUp { get; set; }

        bool CanNew { get; set; }

        int Count { get; }

        List<T> Entities { get; set; }

        int SelectedIndex { get; set; }

        bool ShowingAllItems { get; }
    }
}

