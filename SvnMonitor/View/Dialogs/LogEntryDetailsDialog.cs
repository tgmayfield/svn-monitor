using System.Linq;

namespace SVNMonitor.View.Dialogs
{
    using SVNMonitor.Entities;
    using SVNMonitor.Extensions;
    using SVNMonitor.Logging;
    using SVNMonitor.Settings;
    using SVNMonitor.View.Interfaces;
    using SVNMonitor.View.Panels;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;

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
        private static List<LogEntryDetailsDialog> openDialogs = new List<LogEntryDetailsDialog>();
        private PathsPanel pathsPanel1;
        private SplitContainer splitContainer1;
        private TextBox txtAuthor;
        private TextBox txtDateTime;
        private TextBox txtLogMessage;

        public LogEntryDetailsDialog()
        {
            this.InitializeComponent();
        }

        public LogEntryDetailsDialog(ILogEntriesView view, SVNLogEntry entry) : this()
        {
            this.ParentView = view;
            this.LogEntry = entry;
        }

        private void btnDiff_Click(object sender, EventArgs e)
        {
            Logger.LogUserAction();
            this.LogEntry.Diff();
        }

        private void btnLog_Click(object sender, EventArgs e)
        {
            Logger.LogUserAction();
            this.LogEntry.OpenSVNLog();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            Logger.LogUserAction();
            this.GetNextLogEntry();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            Logger.LogUserAction();
            base.Close();
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            Logger.LogUserAction();
            this.GetPreviousLogEntry();
        }

        private void btnRollback_Click(object sender, EventArgs e)
        {
            Logger.LogUserAction();
            this.LogEntry.Rollback();
            base.Close();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            Logger.LogUserAction();
            this.LogEntry.SVNUpdate();
            base.Close();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void EnableCommands()
        {
            this.btnUpdate.Enabled = this.btnRollback.Enabled = this.btnLog.Enabled = this.btnDiff.Enabled = false;
            if (this.LogEntry != null)
            {
                if (!this.LogEntry.Source.IsURL)
                {
                    this.btnUpdate.Enabled = this.LogEntry.Unread;
                    this.btnRollback.Enabled = !this.LogEntry.Unread;
                }
                this.btnLog.Enabled = true;
                this.btnDiff.Enabled = true;
                this.btnPrevious.Enabled = this.ParentView.CanGetPreviousLogEntry(this.LogEntry);
                this.btnNext.Enabled = this.ParentView.CanGetNextLogEntry(this.LogEntry);
            }
        }

        private void GetNextLogEntry()
        {
            if (this.ParentView.CanGetNextLogEntry(this.LogEntry))
            {
                SVNLogEntry next = this.ParentView.GetNextLogEntry(this.LogEntry);
                this.LogEntry = next;
            }
        }

        private static Func<LogEntryDetailsDialog, bool> GetPredicate(SVNLogEntry entry)
        {
            return d => (d.LogEntry == entry);
        }

        private void GetPreviousLogEntry()
        {
            if (this.ParentView.CanGetPreviousLogEntry(this.LogEntry))
            {
                SVNLogEntry next = this.ParentView.GetPreviousLogEntry(this.LogEntry);
                this.LogEntry = next;
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
            this.txtLogMessage = new TextBox();
            this.btnOK = new Button();
            this.label1 = new Label();
            this.txtAuthor = new TextBox();
            this.label2 = new Label();
            this.txtDateTime = new TextBox();
            this.label3 = new Label();
            this.splitContainer1 = new SplitContainer();
            this.pathsPanel1 = new PathsPanel();
            this.btnUpdate = new Button();
            this.btnRollback = new Button();
            this.btnLog = new Button();
            this.btnDiff = new Button();
            this.btnPrevious = new Button();
            this.btnNext = new Button();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.pathsPanel1.BeginInit();
            base.SuspendLayout();
            this.txtLogMessage.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            this.txtLogMessage.BackColor = Color.White;
            this.txtLogMessage.Location = new Point(0, 20);
            this.txtLogMessage.Margin = new Padding(4, 4, 4, 4);
            this.txtLogMessage.Multiline = true;
            this.txtLogMessage.Name = "txtLogMessage";
            this.txtLogMessage.ReadOnly = true;
            this.txtLogMessage.ScrollBars = ScrollBars.Both;
            this.txtLogMessage.Size = new Size(0x289, 0x9f);
            this.txtLogMessage.TabIndex = 1;
            this.btnOK.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
            this.btnOK.DialogResult = DialogResult.OK;
            this.btnOK.Location = new Point(0x237, 610);
            this.btnOK.Margin = new Padding(4, 4, 4, 4);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(100, 0x1c);
            this.btnOK.TabIndex = 11;
            this.btnOK.Text = "&Close";
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            this.label1.AutoSize = true;
            this.label1.Location = new Point(0x10, 0x12);
            this.label1.Margin = new Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x36, 0x11);
            this.label1.TabIndex = 0;
            this.label1.Text = "Author:";
            this.txtAuthor.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            this.txtAuthor.BackColor = Color.White;
            this.txtAuthor.Location = new Point(0x10, 0x26);
            this.txtAuthor.Margin = new Padding(4, 4, 4, 4);
            this.txtAuthor.Name = "txtAuthor";
            this.txtAuthor.ReadOnly = true;
            this.txtAuthor.Size = new Size(0x289, 0x16);
            this.txtAuthor.TabIndex = 1;
            this.label2.AutoSize = true;
            this.label2.Location = new Point(0x10, 0x42);
            this.label2.Margin = new Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new Size(100, 0x11);
            this.label2.TabIndex = 2;
            this.label2.Text = "Date and time:";
            this.txtDateTime.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            this.txtDateTime.BackColor = Color.White;
            this.txtDateTime.Location = new Point(0x10, 0x56);
            this.txtDateTime.Margin = new Padding(4, 4, 4, 4);
            this.txtDateTime.Name = "txtDateTime";
            this.txtDateTime.ReadOnly = true;
            this.txtDateTime.Size = new Size(0x289, 0x16);
            this.txtDateTime.TabIndex = 3;
            this.label3.AutoSize = true;
            this.label3.Location = new Point(0, 0);
            this.label3.Margin = new Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x45, 0x11);
            this.label3.TabIndex = 0;
            this.label3.Text = "Message:";
            this.splitContainer1.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            this.splitContainer1.Location = new Point(0x10, 0x76);
            this.splitContainer1.Margin = new Padding(4, 4, 4, 4);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = Orientation.Horizontal;
            this.splitContainer1.Panel1.Controls.Add(this.label3);
            this.splitContainer1.Panel1.Controls.Add(this.txtLogMessage);
            this.splitContainer1.Panel2.Controls.Add(this.pathsPanel1);
            this.splitContainer1.Size = new Size(0x28b, 0x1e5);
            this.splitContainer1.SplitterDistance = 0xb8;
            this.splitContainer1.SplitterWidth = 7;
            this.splitContainer1.TabIndex = 4;
            this.pathsPanel1.BorderStyle = BorderStyle.FixedSingle;
            this.pathsPanel1.Dock = DockStyle.Fill;
            this.pathsPanel1.Location = new Point(0, 0);
            this.pathsPanel1.LogEntriesView = null;
            this.pathsPanel1.Margin = new Padding(5, 5, 5, 5);
            this.pathsPanel1.Name = "pathsPanel1";
            this.pathsPanel1.SearchTextBox = null;
            this.pathsPanel1.Size = new Size(0x28b, 0x126);
            this.pathsPanel1.TabIndex = 0;
            this.btnUpdate.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
            this.btnUpdate.DialogResult = DialogResult.OK;
            this.btnUpdate.Image = (Image) resources.GetObject("btnUpdate.Image");
            this.btnUpdate.Location = new Point(0x10, 610);
            this.btnUpdate.Margin = new Padding(4, 4, 4, 4);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new Size(100, 0x1c);
            this.btnUpdate.TabIndex = 5;
            this.btnUpdate.Text = "&Update";
            this.btnUpdate.TextImageRelation = TextImageRelation.ImageBeforeText;
            this.btnUpdate.Click += new EventHandler(this.btnUpdate_Click);
            this.btnRollback.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
            this.btnRollback.DialogResult = DialogResult.OK;
            this.btnRollback.Image = (Image) resources.GetObject("btnRollback.Image");
            this.btnRollback.Location = new Point(0x7c, 610);
            this.btnRollback.Margin = new Padding(4, 4, 4, 4);
            this.btnRollback.Name = "btnRollback";
            this.btnRollback.Size = new Size(100, 0x1c);
            this.btnRollback.TabIndex = 6;
            this.btnRollback.Text = "&Rollback";
            this.btnRollback.TextImageRelation = TextImageRelation.ImageBeforeText;
            this.btnRollback.Click += new EventHandler(this.btnRollback_Click);
            this.btnLog.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
            this.btnLog.DialogResult = DialogResult.OK;
            this.btnLog.Image = (Image) resources.GetObject("btnLog.Image");
            this.btnLog.Location = new Point(0xe8, 610);
            this.btnLog.Margin = new Padding(4, 4, 4, 4);
            this.btnLog.Name = "btnLog";
            this.btnLog.Size = new Size(100, 0x1c);
            this.btnLog.TabIndex = 7;
            this.btnLog.Text = "&Log";
            this.btnLog.TextImageRelation = TextImageRelation.ImageBeforeText;
            this.btnLog.Click += new EventHandler(this.btnLog_Click);
            this.btnDiff.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
            this.btnDiff.DialogResult = DialogResult.OK;
            this.btnDiff.Image = (Image) resources.GetObject("btnDiff.Image");
            this.btnDiff.Location = new Point(340, 610);
            this.btnDiff.Margin = new Padding(4, 4, 4, 4);
            this.btnDiff.Name = "btnDiff";
            this.btnDiff.Size = new Size(100, 0x1c);
            this.btnDiff.TabIndex = 8;
            this.btnDiff.Text = "&Diff";
            this.btnDiff.TextImageRelation = TextImageRelation.ImageBeforeText;
            this.btnDiff.Click += new EventHandler(this.btnDiff_Click);
            this.btnPrevious.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
            this.btnPrevious.DialogResult = DialogResult.OK;
            this.btnPrevious.Image = (Image) resources.GetObject("btnPrevious.Image");
            this.btnPrevious.Location = new Point(0x1e9, 610);
            this.btnPrevious.Margin = new Padding(4, 4, 4, 4);
            this.btnPrevious.Name = "btnPrevious";
            this.btnPrevious.Size = new Size(0x1f, 0x1c);
            this.btnPrevious.TabIndex = 9;
            this.btnPrevious.Click += new EventHandler(this.btnPrevious_Click);
            this.btnNext.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
            this.btnNext.DialogResult = DialogResult.OK;
            this.btnNext.Image = (Image) resources.GetObject("btnNext.Image");
            this.btnNext.Location = new Point(0x210, 610);
            this.btnNext.Margin = new Padding(4, 4, 4, 4);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new Size(0x1f, 0x1c);
            this.btnNext.TabIndex = 10;
            this.btnNext.Click += new EventHandler(this.btnNext_Click);
            base.AcceptButton = this.btnOK;
            base.AutoScaleDimensions = new SizeF(8f, 16f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.CancelButton = this.btnOK;
            base.ClientSize = new Size(0x2ab, 0x28e);
            base.Controls.Add(this.btnNext);
            base.Controls.Add(this.btnPrevious);
            base.Controls.Add(this.splitContainer1);
            base.Controls.Add(this.txtDateTime);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.txtAuthor);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.btnDiff);
            base.Controls.Add(this.btnLog);
            base.Controls.Add(this.btnRollback);
            base.Controls.Add(this.btnUpdate);
            base.Controls.Add(this.btnOK);
            base.Icon = (Icon) resources.GetObject("$this.Icon");
            base.KeyPreview = true;
            base.Margin = new Padding(4, 4, 4, 4);
            base.MaximizeBox = true;
            base.MinimizeBox = true;
            this.MinimumSize = new Size(0x26d, 0x1fb);
            base.Name = "LogEntryDetailsDialog";
            base.SizeGripStyle = SizeGripStyle.Show;
            base.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "[Name [Revision]]";
            base.KeyDown += new KeyEventHandler(this.LogEntryDetailsDialog_KeyDown);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.pathsPanel1.EndInit();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void LoadSettings()
        {
            base.Left = ApplicationSettingsManager.Settings.LogEntryDetailsDialogLocationX;
            base.Top = ApplicationSettingsManager.Settings.LogEntryDetailsDialogLocationY;
            base.Height = ApplicationSettingsManager.Settings.LogEntryDetailsDialogSizeHeight;
            base.Width = ApplicationSettingsManager.Settings.LogEntryDetailsDialogSizeWidth;
            this.splitContainer1.SplitterDistance = ApplicationSettingsManager.Settings.LogEntryDetailsDialogSplitterDistance;
        }

        private void LogEntryDetailsDialog_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control || e.Alt)
            {
                if (e.KeyCode == Keys.Right)
                {
                    this.GetNextLogEntry();
                }
                else if (e.KeyCode == Keys.Left)
                {
                    this.GetPreviousLogEntry();
                }
            }
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            base.OnFormClosed(e);
            this.SaveSettings();
            if (this.LogEntry != null)
            {
                this.LogEntry.Source.StatusChanged -= new EventHandler<StatusChangedEventArgs>(this.Source_StatusChanged);
                if (OpenDialogs.Contains(this))
                {
                    OpenDialogs.Remove(this);
                }
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.txtLogMessage.GotFocus += (s, ea) => this.HideTextBoxCaret(this.txtLogMessage);
            this.txtAuthor.GotFocus += (s, ea) => this.HideTextBoxCaret(this.txtAuthor);
            this.txtDateTime.GotFocus += (s, ea) => this.HideTextBoxCaret(this.txtDateTime);
            this.LoadSettings();
        }

        private void RefreshPaths()
        {
            if (base.InvokeRequired)
            {
                base.BeginInvoke(new MethodInvoker(this.RefreshPaths));
            }
            else
            {
                this.pathsPanel1.SetPaths();
            }
        }

        private void SaveSettings()
        {
            ApplicationSettingsManager.Settings.LogEntryDetailsDialogLocationX = base.Left;
            ApplicationSettingsManager.Settings.LogEntryDetailsDialogLocationY = base.Top;
            ApplicationSettingsManager.Settings.LogEntryDetailsDialogSizeHeight = base.Height;
            ApplicationSettingsManager.Settings.LogEntryDetailsDialogSizeWidth = base.Width;
            ApplicationSettingsManager.Settings.LogEntryDetailsDialogSplitterDistance = this.splitContainer1.SplitterDistance;
        }

        private void SetLogEntryDetails()
        {
            if (base.InvokeRequired)
            {
                base.BeginInvoke(new MethodInvoker(this.SetLogEntryDetails));
            }
            else if (this.LogEntry != null)
            {
                this.LogEntry.Source.StatusChanged -= new EventHandler<StatusChangedEventArgs>(this.Source_StatusChanged);
                this.LogEntry.Source.StatusChanged += new EventHandler<StatusChangedEventArgs>(this.Source_StatusChanged);
                this.Text = string.Format("{0} [{1}]", this.LogEntry.Source.Name, this.LogEntry.Revision);
                this.txtAuthor.Text = this.LogEntry.Author;
                this.txtDateTime.Text = this.LogEntry.CommitedOn.ToString();
                this.txtLogMessage.Text = this.LogEntry.Message;
                this.pathsPanel1.SetPaths(this.LogEntry.Paths);
            }
        }

        public static void ShowLogEntryDetails(ILogEntriesView view, SVNLogEntry entry)
        {
            LogEntryDetailsDialog dialog;
            var predicate = GetPredicate(entry);
            if (OpenDialogs.Any<LogEntryDetailsDialog>(predicate))
            {
                dialog = OpenDialogs.Where<LogEntryDetailsDialog>(predicate).First<LogEntryDetailsDialog>();
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
            this.RefreshPaths();
        }

        public SVNLogEntry LogEntry
        {
            [DebuggerNonUserCode]
            get
            {
                return this.logEntry;
            }
            private set
            {
                this.logEntry = value;
                this.SetLogEntryDetails();
                this.EnableCommands();
            }
        }

        private static List<LogEntryDetailsDialog> OpenDialogs
        {
            [DebuggerNonUserCode]
            get
            {
                return openDialogs;
            }
        }

        public ILogEntriesView ParentView { get; private set; }
    }
}

