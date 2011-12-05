namespace SVNMonitor.View.Controls
{
    using SVNMonitor.Entities;
    using SVNMonitor.Resources.Text;
    using System;
    using System.Runtime.CompilerServices;

    public class MonitorsExplorerBar : UserEntitiesExplorerBar<Monitor>
    {
        public event EventHandler ActionClick;

        public event EventHandler ConditionClick;

        protected override UserEntityExplorerBarGroup<Monitor> GetEntityGroup(Monitor entity)
        {
            return new MonitorExplorerBarGroup(entity);
        }

        protected virtual void OnActionClick()
        {
            if (this.ActionClick != null)
            {
                this.ActionClick(this, EventArgs.Empty);
            }
        }

        protected virtual void OnConditionClick()
        {
            if (this.ConditionClick != null)
            {
                this.ConditionClick(this, EventArgs.Empty);
            }
        }

        protected override string NoEntitiesString
        {
            get
            {
                return Strings.NoMonitorsAreDefined;
            }
        }
    }
}

