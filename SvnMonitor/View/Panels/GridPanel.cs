namespace SVNMonitor.View.Panels
{
    using Janus.Windows.GridEX;
    using System;
    using System.Windows.Forms;

    internal class GridPanel : UserControl
    {
        public virtual string GetGridLayout()
        {
            return this.Grid.GetLayout().GetXmlString();
        }

        public void SetGridLayout()
        {
            this.SetGridLayout(this.LayoutSettings);
        }

        public virtual void SetGridLayout(string layoutString)
        {
            if (!string.IsNullOrEmpty(layoutString))
            {
                GridEXLayout layout = GridEXLayout.FromXMLString(layoutString);
                this.Grid.LoadLayout(layout);
            }
        }

        protected virtual Janus.Windows.GridEX.GridEX Grid
        {
            get
            {
                return null;
            }
        }

        protected virtual string LayoutSettings
        {
            get
            {
                return string.Empty;
            }
        }
    }
}

