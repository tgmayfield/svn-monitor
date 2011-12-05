using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Timers;
using System.Windows.Forms;

using Janus.Windows.GridEX;
using Janus.Windows.GridEX.EditControls;

using SVNMonitor.Logging;
using SVNMonitor.Resources;
using SVNMonitor.View.Interfaces;

namespace SVNMonitor.View.Controls
{
	public class SearchTextBox<T> : UserControl
		where T : ISearchable
	{
		private IContainer components;
		private static bool dpiErrorLogged;
		private EditBox editBox1;
		private Panel panel1;
		private int resultsCount;
		private ISearchablePanel<T> searchablePanel;
		private System.Timers.Timer searchTimer;

		public SearchTextBox()
		{
			InitializeComponent();
			InitSearchTimer();
		}

		private void AdjustLocation(Graphics g)
		{
			if (base.Parent != null)
			{
				int defaultDpi = 0x60;
				int dpi = defaultDpi;
				try
				{
					dpi = (int)g.DpiX;
					float factor = (dpi) / ((float)defaultDpi);
					int rightMargin = (int)(factor * RightMargin);
					base.Left = (base.Parent.Width - base.Width) - rightMargin;
					float topPadding = (base.Parent.Height - editBox1.Height) / 2;
					topPadding -= base.Top;
					panel1.Padding = new Padding(0, (int)topPadding, 0, 0);
				}
				catch (Exception ex)
				{
					if (!SearchTextBox<T>.dpiErrorLogged)
					{
						Logger.Log.Error(string.Format("Error adjusting dpi: {0}", dpi), ex);
						SearchTextBox<T>.dpiErrorLogged = true;
					}
				}
			}
		}

		private void btnClear_Click(object sender, EventArgs e)
		{
			Logger.LogUserAction();
			Clear();
		}

		public void Clear()
		{
			editBox1.Clear();
			SearchElapsed();
		}

		public void ClearNoFocus()
		{
			Clear();
			SetNoSearchBackColor();
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void editBox1_Enter(object sender, EventArgs e)
		{
			SetSearchBackColor();
		}

		private void editBox1_KeyDown(object sender, KeyEventArgs e)
		{
			Logger.LogUserAction();
			if (e.KeyCode == Keys.Escape)
			{
				Clear();
			}
			else if (e.KeyCode == Keys.Return)
			{
				searchTimer.Stop();
				SearchElapsed();
			}
		}

		private void editBox1_Leave(object sender, EventArgs e)
		{
			if (string.IsNullOrEmpty(editBox1.Text))
			{
				SetNoSearchBackColor();
			}
		}

		private void editBox1_TextChanged(object sender, EventArgs e)
		{
			Logger.LogUserAction();
			StartSearchTimer();
		}

		private void InitializeComponent()
		{
			editBox1 = new EditBox();
			panel1 = new Panel();
			panel1.SuspendLayout();
			base.SuspendLayout();
			editBox1.BackColor = Color.White;
			editBox1.BorderStyle = Janus.Windows.GridEX.BorderStyle.Flat;
			editBox1.ButtonImageSize = new Size(12, 12);
			editBox1.Dock = DockStyle.Fill;
			editBox1.Location = new Point(0, 0);
			editBox1.Name = "editBox1";
			editBox1.Size = new Size(0x114, 20);
			editBox1.TabIndex = 1;
			editBox1.VisualStyle = VisualStyle.VS2005;
			editBox1.TextChanged += editBox1_TextChanged;
			editBox1.ButtonClick += btnClear_Click;
			editBox1.Leave += editBox1_Leave;
			editBox1.Enter += editBox1_Enter;
			editBox1.KeyDown += editBox1_KeyDown;
			panel1.Controls.Add(editBox1);
			panel1.Dock = DockStyle.Fill;
			panel1.Location = new Point(0, 0);
			panel1.Name = "panel1";
			panel1.Size = new Size(0x114, 20);
			panel1.TabIndex = 2;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			BackColor = Color.Transparent;
			base.Controls.Add(panel1);
			base.Name = "SearchTextBox";
			base.Size = new Size(0x114, 20);
			panel1.ResumeLayout(false);
			panel1.PerformLayout();
			base.ResumeLayout(false);
		}

		private void InitSearchTimer()
		{
			searchTimer = new System.Timers.Timer();
			searchTimer.Interval = 800.0;
			searchTimer.AutoReset = false;
			searchTimer.Elapsed += searchTimer_Elapsed;
		}

		private void InternalClear()
		{
			SearchablePanel.ClearSearch();
			editBox1.ButtonStyle = EditButtonStyle.NoButton;
		}

		private int InternalSearch(string text, Predicate<T> filter)
		{
			List<T> results = new List<T>();
			IEnumerable<T> items = SearchablePanel.GetAllItems();
			if (items == null)
			{
				return 0;
			}
			foreach (T item in items)
			{
				IEnumerable<string> keywords = item.GetSearchKeywords();
				if (keywords == null)
				{
					return 0;
				}
				foreach (string str in keywords)
				{
					if ((str != null) && (str.IndexOf(text, StringComparison.InvariantCultureIgnoreCase) != -1))
					{
						if (filter == null)
						{
							results.Add(item);
						}
						else if (filter(item))
						{
							results.Add(item);
						}
						break;
					}
				}
			}
			SearchablePanel.SetSearchResults(results);
			return results.Count;
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			AdjustLocation(e.Graphics);
		}

		public int Search()
		{
			return Search(null);
		}

		public int Search(Predicate<T> filter)
		{
			if (SearchablePanel == null)
			{
				return 0;
			}
			string text = editBox1.Text;
			int resultsCount = 0;
			if (string.IsNullOrEmpty(text) && (filter == null))
			{
				InternalClear();
				return resultsCount;
			}
			resultsCount = InternalSearch(text, filter);
			if (!string.IsNullOrEmpty(text))
			{
				editBox1.ButtonStyle = EditButtonStyle.Image;
				editBox1.ButtonImage = Images.selection_delete;
			}
			return resultsCount;
		}

		private void SearchElapsed()
		{
			if (base.InvokeRequired)
			{
				base.BeginInvoke(new MethodInvoker(SearchElapsed));
			}
			else
			{
				searchTimer.Stop();
				resultsCount = Search();
				SetSearchBackColor();
			}
		}

		private void searchTimer_Elapsed(object sender, ElapsedEventArgs e)
		{
			SearchElapsed();
		}

		private void SetNoSearchBackColor()
		{
			editBox1.BackColor = Color.White;
		}

		private void SetSearchBackColor()
		{
			if (string.IsNullOrEmpty(editBox1.Text))
			{
				editBox1.BackColor = Color.FromArgb(0xff, 0xff, 200);
			}
			else if (resultsCount > 0)
			{
				editBox1.BackColor = Color.Yellow;
			}
			else
			{
				editBox1.BackColor = Color.Salmon;
			}
		}

		private void StartSearchTimer()
		{
			searchTimer.Stop();
			searchTimer.Start();
		}

		public int RightMargin { get; set; }

		public ISearchablePanel<T> SearchablePanel
		{
			[DebuggerNonUserCode]
			get { return searchablePanel; }
			set
			{
				searchablePanel = value;
				searchablePanel.SearchTextBox = this;
			}
		}
	}
}