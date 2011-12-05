using System;

using SVNMonitor.Entities;
using SVNMonitor.Resources.Text;

namespace SVNMonitor.View.Controls
{
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
			if (ActionClick != null)
			{
				ActionClick(this, EventArgs.Empty);
			}
		}

		protected virtual void OnConditionClick()
		{
			if (ConditionClick != null)
			{
				ConditionClick(this, EventArgs.Empty);
			}
		}

		protected override string NoEntitiesString
		{
			get { return Strings.NoMonitorsAreDefined; }
		}
	}
}