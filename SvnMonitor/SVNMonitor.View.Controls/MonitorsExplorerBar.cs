using System;
using SVNMonitor.Resources.Text;
using SVNMonitor.Entities;

namespace SVNMonitor.View.Controls
{
public class MonitorsExplorerBar : UserEntitiesExplorerBar<Monitor>
{
	private EventHandler ActionClick;

	private EventHandler ConditionClick;

	protected string NoEntitiesString
	{
		get
		{
			return Strings.NoMonitorsAreDefined;
		}
	}

	public MonitorsExplorerBar()
	{
	}

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

	public event EventHandler ActionClick;
	public event EventHandler ConditionClick;
}
}