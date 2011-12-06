using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

using SVNMonitor.Entities;
using SVNMonitor.Logging;
using SVNMonitor.Settings;
using SVNMonitor.View.Interfaces;
using SVNMonitor.View.Panels;

namespace SVNMonitor.View.Dialogs
{
	public partial class LogEntryDetailsDialog : BaseDialog
	{
		private static readonly List<LogEntryDetailsDialog> openDialogs = new List<LogEntryDetailsDialog>();
		private SVNLogEntry logEntry;
		
		public LogEntryDetailsDialog()
		{
			InitializeComponent();
		}

		public LogEntryDetailsDialog(ILogEntriesView view, SVNLogEntry entry)
			: this()
		{
			ParentView = view;
			LogEntry = entry;
		}

		private void btnDiff_Click(object sender, EventArgs e)
		{
			Logger.LogUserAction();
			LogEntry.Diff();
		}

		private void btnLog_Click(object sender, EventArgs e)
		{
			Logger.LogUserAction();
			LogEntry.OpenSVNLog();
		}

		private void btnNext_Click(object sender, EventArgs e)
		{
			Logger.LogUserAction();
			GetNextLogEntry();
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			Logger.LogUserAction();
			base.Close();
		}

		private void btnPrevious_Click(object sender, EventArgs e)
		{
			Logger.LogUserAction();
			GetPreviousLogEntry();
		}

		private void btnRollback_Click(object sender, EventArgs e)
		{
			Logger.LogUserAction();
			LogEntry.Rollback();
			base.Close();
		}

		private void btnUpdate_Click(object sender, EventArgs e)
		{
			Logger.LogUserAction();
			LogEntry.SVNUpdate();
			base.Close();
		}

		private void EnableCommands()
		{
			btnUpdate.Enabled = btnRollback.Enabled = btnLog.Enabled = btnDiff.Enabled = false;
			if (LogEntry != null)
			{
				if (!LogEntry.Source.IsURL)
				{
					btnUpdate.Enabled = LogEntry.Unread;
					btnRollback.Enabled = !LogEntry.Unread;
				}
				btnLog.Enabled = true;
				btnDiff.Enabled = true;
				btnPrevious.Enabled = ParentView.CanGetPreviousLogEntry(LogEntry);
				btnNext.Enabled = ParentView.CanGetNextLogEntry(LogEntry);
			}
		}

		private void GetNextLogEntry()
		{
			if (ParentView.CanGetNextLogEntry(LogEntry))
			{
				SVNLogEntry next = ParentView.GetNextLogEntry(LogEntry);
				LogEntry = next;
			}
		}

		private static Func<LogEntryDetailsDialog, bool> GetPredicate(SVNLogEntry entry)
		{
			return d => (d.LogEntry == entry);
		}

		private void GetPreviousLogEntry()
		{
			if (ParentView.CanGetPreviousLogEntry(LogEntry))
			{
				SVNLogEntry next = ParentView.GetPreviousLogEntry(LogEntry);
				LogEntry = next;
			}
		}

		[DllImport("user32")]
		private static extern bool HideCaret(IntPtr hWnd);

		private void HideTextBoxCaret(TextBox textBox)
		{
			try
			{
				HideCaret(textBox.Handle);
			}
			catch
			{
			}
		}

		private void LoadSettings()
		{
			base.Left = ApplicationSettingsManager.Settings.LogEntryDetailsDialogLocationX;
			base.Top = ApplicationSettingsManager.Settings.LogEntryDetailsDialogLocationY;
			base.Height = ApplicationSettingsManager.Settings.LogEntryDetailsDialogSizeHeight;
			base.Width = ApplicationSettingsManager.Settings.LogEntryDetailsDialogSizeWidth;
			splitContainer1.SplitterDistance = ApplicationSettingsManager.Settings.LogEntryDetailsDialogSplitterDistance;
		}

		private void LogEntryDetailsDialog_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Control || e.Alt)
			{
				if (e.KeyCode == Keys.Right)
				{
					GetNextLogEntry();
				}
				else if (e.KeyCode == Keys.Left)
				{
					GetPreviousLogEntry();
				}
			}
		}

		protected override void OnFormClosed(FormClosedEventArgs e)
		{
			base.OnFormClosed(e);
			SaveSettings();
			if (LogEntry != null)
			{
				LogEntry.Source.StatusChanged -= Source_StatusChanged;
				if (OpenDialogs.Contains(this))
				{
					OpenDialogs.Remove(this);
				}
			}
		}

		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			txtLogMessage.GotFocus += (s, ea) => HideTextBoxCaret(txtLogMessage);
			txtAuthor.GotFocus += (s, ea) => HideTextBoxCaret(txtAuthor);
			txtDateTime.GotFocus += (s, ea) => HideTextBoxCaret(txtDateTime);
			LoadSettings();
		}

		private void RefreshPaths()
		{
			if (base.InvokeRequired)
			{
				base.BeginInvoke(new MethodInvoker(RefreshPaths));
			}
			else
			{
				pathsPanel1.SetPaths();
			}
		}

		private void SaveSettings()
		{
			ApplicationSettingsManager.Settings.LogEntryDetailsDialogLocationX = base.Left;
			ApplicationSettingsManager.Settings.LogEntryDetailsDialogLocationY = base.Top;
			ApplicationSettingsManager.Settings.LogEntryDetailsDialogSizeHeight = base.Height;
			ApplicationSettingsManager.Settings.LogEntryDetailsDialogSizeWidth = base.Width;
			ApplicationSettingsManager.Settings.LogEntryDetailsDialogSplitterDistance = splitContainer1.SplitterDistance;
		}

		private void SetLogEntryDetails()
		{
			if (base.InvokeRequired)
			{
				base.BeginInvoke(new MethodInvoker(SetLogEntryDetails));
			}
			else if (LogEntry != null)
			{
				LogEntry.Source.StatusChanged -= Source_StatusChanged;
				LogEntry.Source.StatusChanged += Source_StatusChanged;
				Text = string.Format("{0} [{1}]", LogEntry.Source.Name, LogEntry.Revision);
				txtAuthor.Text = LogEntry.Author;
				txtDateTime.Text = LogEntry.CommitedOn.ToString();
				txtLogMessage.Text = LogEntry.Message;
				pathsPanel1.SetPaths(LogEntry.Paths);
			}
		}

		public static void ShowLogEntryDetails(ILogEntriesView view, SVNLogEntry entry)
		{
			LogEntryDetailsDialog dialog;
			var predicate = GetPredicate(entry);
			if (OpenDialogs.Any(predicate))
			{
				dialog = OpenDialogs.Where(predicate).First();
			}
			else
			{
				dialog = new LogEntryDetailsDialog(view, entry);
				OpenDialogs.Add(dialog);
			}
			if (dialog.WindowState == FormWindowState.Minimized)
			{
				dialog.WindowState = FormWindowState.Normal;
			}
			dialog.Show();
			dialog.Activate();
		}

		private void Source_StatusChanged(object sender, StatusChangedEventArgs e)
		{
			RefreshPaths();
		}

		public SVNLogEntry LogEntry
		{
			[DebuggerNonUserCode]
			get { return logEntry; }
			private set
			{
				logEntry = value;
				SetLogEntryDetails();
				EnableCommands();
			}
		}

		private static List<LogEntryDetailsDialog> OpenDialogs
		{
			[DebuggerNonUserCode]
			get { return openDialogs; }
		}

		public ILogEntriesView ParentView { get; private set; }
	}
}