using System;
using System.Windows.Forms;

namespace SVNMonitor.View.Controls
{
	internal partial class AnimationProgressBar : UserControl
	{
		public AnimationProgressBar()
		{
			InitializeComponent();
		}

		public override string Text
		{
			get { return label1.Text; }
			set { label1.Text = value; }
		}
	}
}