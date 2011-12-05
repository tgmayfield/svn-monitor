using System;
using System.Windows.Forms;

using Janus.Windows.GridEX;

namespace SVNMonitor.View.Panels
{
	internal partial class GridPanel : UserControl
	{
		public virtual string GetGridLayout()
		{
			return Grid.GetLayout().GetXmlString();
		}

		public void SetGridLayout()
		{
			SetGridLayout(LayoutSettings);
		}

		public virtual void SetGridLayout(string layoutString)
		{
			if (!string.IsNullOrEmpty(layoutString))
			{
				GridEXLayout layout = GridEXLayout.FromXMLString(layoutString);
				Grid.LoadLayout(layout);
			}
		}

		protected virtual Janus.Windows.GridEX.GridEX Grid
		{
			get { return null; }
		}

		protected virtual string LayoutSettings
		{
			get { return string.Empty; }
		}
	}
}