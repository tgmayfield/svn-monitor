using System.Windows.Forms;
using Janus.Windows.GridEX;
using System;

namespace SVNMonitor.View.Panels
{
internal class GridPanel : UserControl
{
	protected GridEX Grid
	{
		get
		{
			return null;
		}
	}

	protected string LayoutSettings
	{
		get
		{
			return string.Empty;
		}
	}

	public GridPanel()
	{
	}

	public virtual string GetGridLayout()
	{
		GridEXLayout layout = this.Grid.GetLayout();
		return layout.GetXmlString();
	}

	public void SetGridLayout()
	{
		this.SetGridLayout(this.LayoutSettings);
	}

	public virtual void SetGridLayout(string layoutString)
	{
		if (string.IsNullOrEmpty(layoutString))
		{
			return;
		}
		GridEXLayout layout = GridEXLayout.FromXMLString(layoutString);
		this.Grid.LoadLayout(layout);
	}
}
}