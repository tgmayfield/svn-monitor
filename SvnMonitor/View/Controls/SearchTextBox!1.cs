namespace SVNMonitor.View.Controls
{
    using Janus.Windows.GridEX;
    using Janus.Windows.GridEX.EditControls;
    using SVNMonitor.Logging;
    using SVNMonitor.Resources;
    using SVNMonitor.View.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Timers;
    using System.Windows.Forms;

    public class SearchTextBox<T> : UserControl where T: ISearchable
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
            this.InitializeComponent();
            this.InitSearchTimer();
        }

        private void AdjustLocation(Graphics g)
        {
            if (base.Parent != null)
            {
                int defaultDpi = 0x60;
                int dpi = defaultDpi;
                try
                {
                    dpi = (int) g.DpiX;
                    float factor = ((float) dpi) / ((float) defaultDpi);
                    int rightMargin = (int) (factor * this.RightMargin);
                    base.Left = (base.Parent.Width - base.Width) - rightMargin;
                    float topPadding = (base.Parent.Height - this.editBox1.Height) / 2;
                    topPadding -= base.Top;
                    this.panel1.Padding = new Padding(0, (int) topPadding, 0, 0);
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
            this.Clear();
        }

        public void Clear()
        {
            this.editBox1.Clear();
            this.SearchElapsed();
        }

        public void ClearNoFocus()
        {
            this.Clear();
            this.SetNoSearchBackColor();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void editBox1_Enter(object sender, EventArgs e)
        {
            this.SetSearchBackColor();
        }

        private void editBox1_KeyDown(object sender, KeyEventArgs e)
        {
            Logger.LogUserAction();
            if (e.KeyCode == Keys.Escape)
            {
                this.Clear();
            }
            else if (e.KeyCode == Keys.Return)
            {
                this.searchTimer.Stop();
                this.SearchElapsed();
            }
        }

        private void editBox1_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.editBox1.Text))
            {
                this.SetNoSearchBackColor();
            }
        }

        private void editBox1_TextChanged(object sender, EventArgs e)
        {
            Logger.LogUserAction();
            this.StartSearchTimer();
        }

        private void InitializeComponent()
        {
            this.editBox1 = new EditBox();
            this.panel1 = new Panel();
            this.panel1.SuspendLayout();
            base.SuspendLayout();
            this.editBox1.BackColor = Color.White;
            this.editBox1.BorderStyle = Janus.Windows.GridEX.BorderStyle.Flat;
            this.editBox1.ButtonImageSize = new Size(12, 12);
            this.editBox1.Dock = DockStyle.Fill;
            this.editBox1.Location = new Point(0, 0);
            this.editBox1.Name = "editBox1";
            this.editBox1.Size = new Size(0x114, 20);
            this.editBox1.TabIndex = 1;
            this.editBox1.VisualStyle = VisualStyle.VS2005;
            this.editBox1.TextChanged += new EventHandler(this.editBox1_TextChanged);
            this.editBox1.ButtonClick += new EventHandler(this.btnClear_Click);
            this.editBox1.Leave += new EventHandler(this.editBox1_Leave);
            this.editBox1.Enter += new EventHandler(this.editBox1_Enter);
            this.editBox1.KeyDown += new KeyEventHandler(this.editBox1_KeyDown);
            this.panel1.Controls.Add(this.editBox1);
            this.panel1.Dock = DockStyle.Fill;
            this.panel1.Location = new Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(0x114, 20);
            this.panel1.TabIndex = 2;
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = Color.Transparent;
            base.Controls.Add(this.panel1);
            base.Name = "SearchTextBox";
            base.Size = new Size(0x114, 20);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            base.ResumeLayout(false);
        }

        private void InitSearchTimer()
        {
            this.searchTimer = new System.Timers.Timer();
            this.searchTimer.Interval = 800.0;
            this.searchTimer.AutoReset = false;
            this.searchTimer.Elapsed += new ElapsedEventHandler(this.searchTimer_Elapsed);
        }

        private void InternalClear()
        {
            this.SearchablePanel.ClearSearch();
            this.editBox1.ButtonStyle = EditButtonStyle.NoButton;
        }

        private int InternalSearch(string text, Predicate<T> filter)
        {
            List<T> results = new List<T>();
            IEnumerable<T> items = this.SearchablePanel.GetAllItems();
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
            this.SearchablePanel.SetSearchResults(results);
            return results.Count;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            this.AdjustLocation(e.Graphics);
        }

        public int Search()
        {
            return this.Search(null);
        }

        public int Search(Predicate<T> filter)
        {
            if (this.SearchablePanel == null)
            {
                return 0;
            }
            string text = this.editBox1.Text;
            int resultsCount = 0;
            if (string.IsNullOrEmpty(text) && (filter == null))
            {
                this.InternalClear();
                return resultsCount;
            }
            resultsCount = this.InternalSearch(text, filter);
            if (!string.IsNullOrEmpty(text))
            {
                this.editBox1.ButtonStyle = EditButtonStyle.Image;
                this.editBox1.ButtonImage = Images.selection_delete;
            }
            return resultsCount;
        }

        private void SearchElapsed()
        {
            if (base.InvokeRequired)
            {
                base.BeginInvoke(new MethodInvoker(this.SearchElapsed));
            }
            else
            {
                this.searchTimer.Stop();
                this.resultsCount = this.Search();
                this.SetSearchBackColor();
            }
        }

        private void searchTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            this.SearchElapsed();
        }

        private void SetNoSearchBackColor()
        {
            this.editBox1.BackColor = Color.White;
        }

        private void SetSearchBackColor()
        {
            if (string.IsNullOrEmpty(this.editBox1.Text))
            {
                this.editBox1.BackColor = Color.FromArgb(0xff, 0xff, 200);
            }
            else if (this.resultsCount > 0)
            {
                this.editBox1.BackColor = Color.Yellow;
            }
            else
            {
                this.editBox1.BackColor = Color.Salmon;
            }
        }

        private void StartSearchTimer()
        {
            this.searchTimer.Stop();
            this.searchTimer.Start();
        }

		public int RightMargin { get; set; }

        public ISearchablePanel<T> SearchablePanel
        {
            [DebuggerNonUserCode]
            get
            {
                return this.searchablePanel;
            }
            set
            {
                this.searchablePanel = value;
                this.searchablePanel.SearchTextBox = (SearchTextBox<T>) this;
            }
        }
    }
}

