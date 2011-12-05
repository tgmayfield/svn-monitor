using System.Windows.Forms;
using System.ComponentModel;
using System;
using Janus.Windows.GridEX.EditControls;
using SVNMonitor.View.Interfaces;
using System.Timers;
using System.Drawing;
using SVNMonitor.Logging;
using Janus.Windows.GridEX;
using System.Collections.Generic;

namespace SVNMonitor.View.Controls
{
public class SearchTextBox<T> : UserControl
{
	private IContainer components;

	private static bool dpiErrorLogged;

	private EditBox editBox1;

	private Panel panel1;

	private int resultsCount;

	private ISearchablePanel<T> searchablePanel;

	private Timer searchTimer;

	public int RightMargin
	{
		get
		{
			return this.<RightMargin>k__BackingField;
		}
		set
		{
			this.<RightMargin>k__BackingField = value;
		}
	}

	public ISearchablePanel<T> SearchablePanel
	{
		get
		{
			return this.searchablePanel;
		}
		set
		{
			this.searchablePanel = value;
			this.searchablePanel.SearchTextBox = this;
		}
	}

	public SearchTextBox()
	{
		base.InitializeComponent();
		base.InitSearchTimer();
	}

	private void AdjustLocation(Graphics g)
	{
		if (base.Parent == null)
		{
			return;
		}
		int defaultDpi = 96;
		int dpi = defaultDpi;
		try
		{
			dpi = (int)g.DpiX;
			float factor = (float)dpi / (float)defaultDpi;
			int rightMargin = (int)factor * (float)base.RightMargin;
			base.Left = this.Parent.Width - this.Width - rightMargin;
			float topPadding = (float)(base.Parent.Height - this.editBox1.Height) / 2;
			topPadding = topPadding - (float)base.Top;
			this.panel1.Padding = new Padding(0, (int)topPadding, 0, 0);
		}
		catch (Exception ex)
		{
			if (!SearchTextBox<T>.dpiErrorLogged)
			{
				Logger.Log.Error(string.Format("Error adjusting dpi: {0}", dpi), ex);
				SearchTextBox<T>.dpiErrorLogged = 1;
			}
		}
	}

	private void btnClear_Click(object sender, EventArgs e)
	{
		Logger.LogUserAction();
		base.Clear();
	}

	public void Clear()
	{
		this.editBox1.Clear();
		base.SearchElapsed();
	}

	public void ClearNoFocus()
	{
		base.Clear();
		base.SetNoSearchBackColor();
	}

	protected override void Dispose(bool disposing)
	{
		if (disposing && this.components != null)
		{
			this.components.Dispose();
		}
		base.Dispose(disposing);
	}

	private void editBox1_Enter(object sender, EventArgs e)
	{
		base.SetSearchBackColor();
	}

	private void editBox1_KeyDown(object sender, KeyEventArgs e)
	{
		Logger.LogUserAction();
		if (e.KeyCode == 27)
		{
			base.Clear();
			return;
		}
		if (e.KeyCode == 13)
		{
			this.searchTimer.Stop();
			base.SearchElapsed();
		}
	}

	private void editBox1_Leave(object sender, EventArgs e)
	{
		if (string.IsNullOrEmpty(this.editBox1.Text))
		{
			base.SetNoSearchBackColor();
		}
	}

	private void editBox1_TextChanged(object sender, EventArgs e)
	{
		Logger.LogUserAction();
		base.StartSearchTimer();
	}

	private void InitializeComponent()
	{
		this.editBox1 = new EditBox();
		this.panel1 = new Panel();
		this.panel1.SuspendLayout();
		base.SuspendLayout();
		this.editBox1.BackColor = Color.White;
		this.editBox1.BorderStyle = BorderStyle.Flat;
		this.editBox1.ButtonImageSize = new Size(12, 12);
		this.editBox1.Dock = DockStyle.Fill;
		this.editBox1.Location = new Point(0, 0);
		this.editBox1.Name = "editBox1";
		this.editBox1.Size = new Size(276, 20);
		this.editBox1.TabIndex = 1;
		this.editBox1.VisualStyle = VisualStyle.VS2005;
		this.editBox1.add_TextChanged(new EventHandler(this.editBox1_TextChanged));
		this.editBox1.add_ButtonClick(new EventHandler(this.btnClear_Click));
		this.editBox1.add_Leave(new EventHandler(this.editBox1_Leave));
		this.editBox1.add_Enter(new EventHandler(this.editBox1_Enter));
		this.editBox1.add_KeyDown(new KeyEventHandler(this.editBox1_KeyDown));
		this.panel1.Controls.Add(this.editBox1);
		this.panel1.Dock = DockStyle.Fill;
		this.panel1.Location = new Point(0, 0);
		this.panel1.Name = "panel1";
		this.panel1.Size = new Size(276, 20);
		this.panel1.TabIndex = 2;
		base.AutoScaleDimensions = new SizeF(6, 13);
		base.AutoScaleMode = AutoScaleMode.Font;
		base.BackColor = Color.Transparent;
		base.Controls.Add(this.panel1);
		base.Name = "SearchTextBox";
		base.Size = new Size(276, 20);
		this.panel1.ResumeLayout(false);
		this.panel1.PerformLayout();
		base.ResumeLayout(false);
	}

	private void InitSearchTimer()
	{
		this.searchTimer = new Timer();
		this.searchTimer.Interval = 800;
		this.searchTimer.AutoReset = false;
		this.searchTimer.Elapsed += new ElapsedEventHandler(this.searchTimer_Elapsed);
	}

	private void InternalClear()
	{
		base.SearchablePanel.ClearSearch();
		this.editBox1.ButtonStyle = EditButtonStyle.NoButton;
	}

	private int InternalSearch(string text, Predicate<T> filter)
	{
		List<T> results = new List<T>();
		IEnumerable<T> items = base.SearchablePanel.GetAllItems();
		if (items == null)
		{
			return 0;
		}
		foreach (T item in items)
		{
			IEnumerable<string> keywords = &item.GetSearchKeywords();
			if (keywords == null)
			{
				return 0;
			}
			foreach (string str in keywords)
			{
				if (str != null && str.IndexOf(text, StringComparison.InvariantCultureIgnoreCase) != -1)
				{
					if (filter == null)
					{
						results.Add(item);
						break;
					}
					if (filter(item))
					{
						results.Add(item);
						break;
					}
				}
			}
		}
		base.SearchablePanel.SetSearchResults(results);
		return results.Count;
	}

	protected override void OnPaint(PaintEventArgs e)
	{
		base.OnPaint(e);
		base.AdjustLocation(e.Graphics);
	}

	public int Search()
	{
		return base.Search(null);
	}

	public int Search(Predicate<T> filter)
	{
		if (base.SearchablePanel == null)
		{
			return 0;
		}
		string text = this.editBox1.Text;
		int resultsCount = 0;
		if (string.IsNullOrEmpty(text) && filter == null)
		{
			base.InternalClear();
		}
		return resultsCount;
	}

	private void SearchElapsed()
	{
		if (base.InvokeRequired)
		{
			base.BeginInvoke(new MethodInvoker(this.SearchElapsed));
			return;
		}
		this.searchTimer.Stop();
		this.resultsCount = base.Search();
		base.SetSearchBackColor();
	}

	private void searchTimer_Elapsed(object sender, ElapsedEventArgs e)
	{
		base.SearchElapsed();
	}

	private void SetNoSearchBackColor()
	{
		this.editBox1.BackColor = Color.White;
	}

	private void SetSearchBackColor()
	{
		if (string.IsNullOrEmpty(this.editBox1.Text))
		{
			this.editBox1.BackColor = Color.FromArgb(255, 255, 200);
			return;
		}
		if (this.resultsCount > 0)
		{
			this.editBox1.BackColor = Color.Yellow;
			return;
		}
		this.editBox1.BackColor = Color.Salmon;
	}

	private void StartSearchTimer()
	{
		this.searchTimer.Stop();
		this.searchTimer.Start();
	}
}
}