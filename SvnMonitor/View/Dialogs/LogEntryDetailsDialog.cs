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
	public class LogEntryDetailsDialog : BaseDialog
	{
		private Button btnDiff;
		private Button btnLog;
		private Button btnNext;
		private Button btnOK;
		private Button btnPrevious;
		private Button btnRollback;
		private Button btnUpdate;
		private IContainer components;
		private Label label1;
		private Label label2;
		private Label label3;
		private SVNLogEntry logEntry;
		private static readonly List<LogEntryDetailsDialog> openDialogs = new List<LogEntryDetailsDialog>();
		private PathsPanel pathsPanel1;
		private SplitContainer splitContainer1;
		private TextBox txtAuthor;
		private TextBox txtDateTime;
		private TextBox txtLogMessage;

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

		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
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

		private void InitializeComponent()
		{
			ComponentResourceManager resources = new ComponentResourceManager(typeof(LogEntryDetailsDialog));
			txtLogMessage = new TextBox();
			btnOK = new Button();
			label1 = new Label();
			txtAuthor = new TextBox();
			label2 = new Label();
			txtDateTime = new TextBox();
			label3 = new Label();
			splitContainer1 = new SplitContainer();
			pathsPanel1 = new PathsPanel();
			btnUpdate = new Button();
			btnRollback = new Button();
			btnLog = new Button();
			btnDiff = new Button();
			btnPrevious = new Button();
			btnNext = new Button();
			splitContainer1.Panel1.SuspendLayout();
			splitContainer1.Panel2.SuspendLayout();
			splitContainer1.SuspendLayout();
			pathsPanel1.BeginInit();
			base.SuspendLayout();
			txtLogMessage.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
			txtLogMessage.BackColor = Color.White;
			txtLogMessage.Location = new Point(0, 20);
			txtLogMessage.Margin = new Padding(4, 4, 4, 4);
			txtLogMessage.Multiline = true;
			txtLogMessage.Name = "txtLogMessage";
			txtLogMessage.ReadOnly = true;
			txtLogMessage.ScrollBars = ScrollBars.Both;
			txtLogMessage.Size = new Size(0x289, 0x9f);
			txtLogMessage.TabIndex = 1;
			btnOK.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
			btnOK.DialogResult = DialogResult.OK;
			btnOK.Location = new Point(0x237, 610);
			btnOK.Margin = new Padding(4, 4, 4, 4);
			btnOK.Name = "btnOK";
			btnOK.Size = new Size(100, 0x1c);
			btnOK.TabIndex = 11;
			btnOK.Text = "&Close";
			btnOK.Click += btnOK_Click;
			label1.AutoSize = true;
			label1.Location = new Point(0x10, 0x12);
			label1.Margin = new Padding(4, 0, 4, 0);
			label1.Name = "label1";
			label1.Size = new Size(0x36, 0x11);
			label1.TabIndex = 0;
			label1.Text = "Author:";
			txtAuthor.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
			txtAuthor.BackColor = Color.White;
			txtAuthor.Location = new Point(0x10, 0x26);
			txtAuthor.Margin = new Padding(4, 4, 4, 4);
			txtAuthor.Name = "txtAuthor";
			txtAuthor.ReadOnly = true;
			txtAuthor.Size = new Size(0x289, 0x16);
			txtAuthor.TabIndex = 1;
			label2.AutoSize = true;
			label2.Location = new Point(0x10, 0x42);
			label2.Margin = new Padding(4, 0, 4, 0);
			label2.Name = "label2";
			label2.Size = new Size(100, 0x11);
			label2.TabIndex = 2;
			label2.Text = "Date and time:";
			txtDateTime.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
			txtDateTime.BackColor = Color.White;
			txtDateTime.Location = new Point(0x10, 0x56);
			txtDateTime.Margin = new Padding(4, 4, 4, 4);
			txtDateTime.Name = "txtDateTime";
			txtDateTime.ReadOnly = true;
			txtDateTime.Size = new Size(0x289, 0x16);
			txtDateTime.TabIndex = 3;
			label3.AutoSize = true;
			label3.Location = new Point(0, 0);
			label3.Margin = new Padding(4, 0, 4, 0);
			label3.Name = "label3";
			label3.Size = new Size(0x45, 0x11);
			label3.TabIndex = 0;
			label3.Text = "Message:";
			splitContainer1.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
			splitContainer1.Location = new Point(0x10, 0x76);
			splitContainer1.Margin = new Padding(4, 4, 4, 4);
			splitContainer1.Name = "splitContainer1";
			splitContainer1.Orientation = Orientation.Horizontal;
			splitContainer1.Panel1.Controls.Add(label3);
			splitContainer1.Panel1.Controls.Add(txtLogMessage);
			splitContainer1.Panel2.Controls.Add(pathsPanel1);
			splitContainer1.Size = new Size(0x28b, 0x1e5);
			splitContainer1.SplitterDistance = 0xb8;
			splitContainer1.SplitterWidth = 7;
			splitContainer1.TabIndex = 4;
			pathsPanel1.BorderStyle = BorderStyle.FixedSingle;
			pathsPanel1.Dock = DockStyle.Fill;
			pathsPanel1.Location = new Point(0, 0);
			pathsPanel1.LogEntriesView = null;
			pathsPanel1.Margin = new Padding(5, 5, 5, 5);
			pathsPanel1.Name = "pathsPanel1";
			pathsPanel1.SearchTextBox = null;
			pathsPanel1.Size = new Size(0x28b, 0x126);
			pathsPanel1.TabIndex = 0;
			btnUpdate.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
			btnUpdate.DialogResult = DialogResult.OK;
			btnUpdate.Image = (Image)resources.GetObject("btnUpdate.Image");
			btnUpdate.Location = new Point(0x10, 610);
			btnUpdate.Margin = new Padding(4, 4, 4, 4);
			btnUpdate.Name = "btnUpdate";
			btnUpdate.Size = new Size(100, 0x1c);
			btnUpdate.TabIndex = 5;
			btnUpdate.Text = "&Update";
			btnUpdate.TextImageRelation = TextImageRelation.ImageBeforeText;
			btnUpdate.Click += btnUpdate_Click;
			btnRollback.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
			btnRollback.DialogResult = DialogResult.OK;
			btnRollback.Image = (Image)resources.GetObject("btnRollback.Image");
			btnRollback.Location = new Point(0x7c, 610);
			btnRollback.Margin = new Padding(4, 4, 4, 4);
			btnRollback.Name = "btnRollback";
			btnRollback.Size = new Size(100, 0x1c);
			btnRollback.TabIndex = 6;
			btnRollback.Text = "&Rollback";
			btnRollback.TextImageRelation = TextImageRelation.ImageBeforeText;
			btnRollback.Click += btnRollback_Click;
			btnLog.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
			btnLog.DialogResult = DialogResult.OK;
			btnLog.Image = (Image)resources.GetObject("btnLog.Image");
			btnLog.Location = new Point(0xe8, 610);
			btnLog.Margin = new Padding(4, 4, 4, 4);
			btnLog.Name = "btnLog";
			btnLog.Size = new Size(100, 0x1c);
			btnLog.TabIndex = 7;
			btnLog.Text = "&Log";
			btnLog.TextImageRelation = TextImageRelation.ImageBeforeText;
			btnLog.Click += btnLog_Click;
			btnDiff.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
			btnDiff.DialogResult = DialogResult.OK;
			btnDiff.Image = (Image)resources.GetObject("btnDiff.Image");
			btnDiff.Location = new Point(340, 610);
			btnDiff.Margin = new Padding(4, 4, 4, 4);
			btnDiff.Name = "btnDiff";
			btnDiff.Size = new Size(100, 0x1c);
			btnDiff.TabIndex = 8;
			btnDiff.Text = "&Diff";
			btnDiff.TextImageRelation = TextImageRelation.ImageBeforeText;
			btnDiff.Click += btnDiff_Click;
			btnPrevious.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
			btnPrevious.DialogResult = DialogResult.OK;
			btnPrevious.Image = (Image)resources.GetObject("btnPrevious.Image");
			btnPrevious.Location = new Point(0x1e9, 610);
			btnPrevious.Margin = new Padding(4, 4, 4, 4);
			btnPrevious.Name = "btnPrevious";
			btnPrevious.Size = new Size(0x1f, 0x1c);
			btnPrevious.TabIndex = 9;
			btnPrevious.Click += btnPrevious_Click;
			btnNext.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
			btnNext.DialogResult = DialogResult.OK;
			btnNext.Image = (Image)resources.GetObject("btnNext.Image");
			btnNext.Location = new Point(0x210, 610);
			btnNext.Margin = new Padding(4, 4, 4, 4);
			btnNext.Name = "btnNext";
			btnNext.Size = new Size(0x1f, 0x1c);
			btnNext.TabIndex = 10;
			btnNext.Click += btnNext_Click;
			base.AcceptButton = btnOK;
			base.AutoScaleDimensions = new SizeF(8f, 16f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.CancelButton = btnOK;
			base.ClientSize = new Size(0x2ab, 0x28e);
			base.Controls.Add(btnNext);
			base.Controls.Add(btnPrevious);
			base.Controls.Add(splitContainer1);
			base.Controls.Add(txtDateTime);
			base.Controls.Add(label2);
			base.Controls.Add(txtAuthor);
			base.Controls.Add(label1);
			base.Controls.Add(btnDiff);
			base.Controls.Add(btnLog);
			base.Controls.Add(btnRollback);
			base.Controls.Add(btnUpdate);
			base.Controls.Add(btnOK);
			base.Icon = (Icon)resources.GetObject("$this.Icon");
			base.KeyPreview = true;
			base.Margin = new Padding(4, 4, 4, 4);
			base.MaximizeBox = true;
			base.MinimizeBox = true;
			MinimumSize = new Size(0x26d, 0x1fb);
			base.Name = "LogEntryDetailsDialog";
			base.SizeGripStyle = SizeGripStyle.Show;
			base.StartPosition = FormStartPosition.CenterScreen;
			Text = "[Name [Revision]]";
			base.KeyDown += LogEntryDetailsDialog_KeyDown;
			splitContainer1.Panel1.ResumeLayout(false);
			splitContainer1.Panel1.PerformLayout();
			splitContainer1.Panel2.ResumeLayout(false);
			splitContainer1.ResumeLayout(false);
			pathsPanel1.EndInit();
			base.ResumeLayout(false);
			base.PerformLayout();
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