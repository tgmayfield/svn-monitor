using System.Linq;

namespace SVNMonitor.View.Dialogs
{
    using SVNMonitor;
    using SVNMonitor.Entities;
    using SVNMonitor.Extensions;
    using SVNMonitor.Logging;
    using SVNMonitor.Resources.Text;
    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Drawing;
    using System.Windows.Forms;

    internal class SourcePropertiesDialog : BaseDialog
    {
        private Button btnCancel;
        private Button btnOK;
        private CheckBox checkAuthenticate;
        private CheckBox checkEnabled;
        private CheckBox checkEnableRecommendations;
        private IContainer components;
        private GroupBox groupAuthenticate;
        private GroupBox groupPath;
        private GroupBox groupSource;
        private Label lblAuthenticationNote;
        private Label lblName;
        private Label lblPassword;
        private Label lblUrlInfo;
        private Label lblUserName;
        private LinkLabel linkPath;
        private RadioButton radioPath;
        private RadioButton radioUrl;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private SVNMonitor.Entities.Source source;
        private TextBox txtName;
        private TextBox txtPassword;
        private TextBox txtPath;
        private TextBox txtUrl;
        private TextBox txtUsername;

        public SourcePropertiesDialog()
        {
            this.InitializeComponent();
        }

        public SourcePropertiesDialog(SVNMonitor.Entities.Source source) : this()
        {
            this.Source = source;
        }

        private void BindData()
        {
            this.txtName.Text = this.Source.Name;
            this.checkEnabled.Checked = this.Source.Enabled;
            this.checkEnableRecommendations.Checked = this.Source.EnableRecommendations;
            if (this.Source.IsURL)
            {
                this.txtUrl.Text = this.Source.Path;
                this.radioUrl.Checked = true;
            }
            else
            {
                this.txtPath.Text = this.Source.Path;
                this.radioPath.Checked = true;
            }
            this.checkAuthenticate.Checked = this.Source.Authenticate;
            this.txtUsername.Text = this.Source.UserName;
            this.txtPassword.Text = this.Source.Password;
        }

        private void BrowsePath()
        {
            FolderBrowserDialog tempLocal0 = new FolderBrowserDialog {
                SelectedPath = this.txtPath.Text
            };
            FolderBrowserDialog dialog = tempLocal0;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                this.txtPath.Text = dialog.SelectedPath;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            Logger.LogUserAction();
            this.Save();
        }

        private void checkAuthenticate_CheckedChanged(object sender, EventArgs e)
        {
            Logger.LogUserAction();
            this.groupAuthenticate.Enabled = this.checkAuthenticate.Checked;
        }

        private void CheckChanges()
        {
            this.btnOK.Enabled = this.IsValid;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void Field_Changed(object sender, EventArgs e)
        {
            Logger.LogUserAction();
            this.CheckChanges();
        }

        private void InitializeComponent()
        {
            ComponentResourceManager resources = new ComponentResourceManager(typeof(SourcePropertiesDialog));
            this.groupSource = new GroupBox();
            this.groupPath = new GroupBox();
            this.linkPath = new LinkLabel();
            this.lblUrlInfo = new Label();
            this.radioUrl = new RadioButton();
            this.txtPath = new TextBox();
            this.radioPath = new RadioButton();
            this.txtUrl = new TextBox();
            this.lblAuthenticationNote = new Label();
            this.checkEnableRecommendations = new CheckBox();
            this.checkEnabled = new CheckBox();
            this.groupAuthenticate = new GroupBox();
            this.txtPassword = new TextBox();
            this.lblPassword = new Label();
            this.txtUsername = new TextBox();
            this.lblUserName = new Label();
            this.txtName = new TextBox();
            this.checkAuthenticate = new CheckBox();
            this.lblName = new Label();
            this.btnCancel = new Button();
            this.btnOK = new Button();
            this.groupSource.SuspendLayout();
            this.groupPath.SuspendLayout();
            this.groupAuthenticate.SuspendLayout();
            base.SuspendLayout();
            resources.ApplyResources(this.groupSource, "groupSource");
            this.groupSource.Controls.Add(this.groupPath);
            this.groupSource.Controls.Add(this.lblAuthenticationNote);
            this.groupSource.Controls.Add(this.checkEnableRecommendations);
            this.groupSource.Controls.Add(this.checkEnabled);
            this.groupSource.Controls.Add(this.groupAuthenticate);
            this.groupSource.Controls.Add(this.txtName);
            this.groupSource.Controls.Add(this.checkAuthenticate);
            this.groupSource.Controls.Add(this.lblName);
            this.groupSource.Name = "groupSource";
            this.groupSource.TabStop = false;
            resources.ApplyResources(this.groupPath, "groupPath");
            this.groupPath.Controls.Add(this.linkPath);
            this.groupPath.Controls.Add(this.lblUrlInfo);
            this.groupPath.Controls.Add(this.radioUrl);
            this.groupPath.Controls.Add(this.txtPath);
            this.groupPath.Controls.Add(this.radioPath);
            this.groupPath.Controls.Add(this.txtUrl);
            this.groupPath.Name = "groupPath";
            this.groupPath.TabStop = false;
            resources.ApplyResources(this.linkPath, "linkPath");
            this.linkPath.Name = "linkPath";
            this.linkPath.TabStop = true;
            this.linkPath.LinkClicked += new LinkLabelLinkClickedEventHandler(this.linkPath_LinkClicked);
            resources.ApplyResources(this.lblUrlInfo, "lblUrlInfo");
            this.lblUrlInfo.ForeColor = Color.DarkRed;
            this.lblUrlInfo.Name = "lblUrlInfo";
            resources.ApplyResources(this.radioUrl, "radioUrl");
            this.radioUrl.Name = "radioUrl";
            this.radioUrl.TabStop = true;
            this.radioUrl.UseVisualStyleBackColor = true;
            this.radioUrl.CheckedChanged += new EventHandler(this.radioPathUrl_CheckedChanged);
            resources.ApplyResources(this.txtPath, "txtPath");
            this.txtPath.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            this.txtPath.AutoCompleteSource = AutoCompleteSource.FileSystem;
            this.txtPath.Name = "txtPath";
            resources.ApplyResources(this.radioPath, "radioPath");
            this.radioPath.Name = "radioPath";
            this.radioPath.TabStop = true;
            this.radioPath.UseVisualStyleBackColor = true;
            this.radioPath.CheckedChanged += new EventHandler(this.radioPathUrl_CheckedChanged);
            resources.ApplyResources(this.txtUrl, "txtUrl");
            this.txtUrl.Name = "txtUrl";
            resources.ApplyResources(this.lblAuthenticationNote, "lblAuthenticationNote");
            this.lblAuthenticationNote.ForeColor = Color.DarkGreen;
            this.lblAuthenticationNote.Name = "lblAuthenticationNote";
            resources.ApplyResources(this.checkEnableRecommendations, "checkEnableRecommendations");
            this.checkEnableRecommendations.Name = "checkEnableRecommendations";
            resources.ApplyResources(this.checkEnabled, "checkEnabled");
            this.checkEnabled.Name = "checkEnabled";
            resources.ApplyResources(this.groupAuthenticate, "groupAuthenticate");
            this.groupAuthenticate.Controls.Add(this.txtPassword);
            this.groupAuthenticate.Controls.Add(this.lblPassword);
            this.groupAuthenticate.Controls.Add(this.txtUsername);
            this.groupAuthenticate.Controls.Add(this.lblUserName);
            this.groupAuthenticate.Name = "groupAuthenticate";
            this.groupAuthenticate.TabStop = false;
            resources.ApplyResources(this.txtPassword, "txtPassword");
            this.txtPassword.Name = "txtPassword";
            resources.ApplyResources(this.lblPassword, "lblPassword");
            this.lblPassword.Name = "lblPassword";
            resources.ApplyResources(this.txtUsername, "txtUsername");
            this.txtUsername.Name = "txtUsername";
            resources.ApplyResources(this.lblUserName, "lblUserName");
            this.lblUserName.Name = "lblUserName";
            resources.ApplyResources(this.txtName, "txtName");
            this.txtName.Name = "txtName";
            resources.ApplyResources(this.checkAuthenticate, "checkAuthenticate");
            this.checkAuthenticate.Name = "checkAuthenticate";
            this.checkAuthenticate.CheckedChanged += new EventHandler(this.checkAuthenticate_CheckedChanged);
            resources.ApplyResources(this.lblName, "lblName");
            this.lblName.Name = "lblName";
            resources.ApplyResources(this.btnCancel, "btnCancel");
            this.btnCancel.DialogResult = DialogResult.Cancel;
            this.btnCancel.Name = "btnCancel";
            resources.ApplyResources(this.btnOK, "btnOK");
            this.btnOK.DialogResult = DialogResult.OK;
            this.btnOK.Name = "btnOK";
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            base.AcceptButton = this.btnOK;
            resources.ApplyResources(this, "$this");
            base.AutoScaleMode = AutoScaleMode.Font;
            base.CancelButton = this.btnCancel;
            base.Controls.Add(this.btnOK);
            base.Controls.Add(this.btnCancel);
            base.Controls.Add(this.groupSource);
            base.FormBorderStyle = FormBorderStyle.FixedDialog;
            base.KeyPreview = true;
            base.Name = "SourcePropertiesDialog";
            base.ShowInTaskbar = false;
            base.Load += new EventHandler(this.SourcePropertiesDialog_Load);
            this.groupSource.ResumeLayout(false);
            this.groupSource.PerformLayout();
            this.groupPath.ResumeLayout(false);
            this.groupPath.PerformLayout();
            this.groupAuthenticate.ResumeLayout(false);
            this.groupAuthenticate.PerformLayout();
            base.ResumeLayout(false);
        }

        private void linkPath_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Logger.LogUserAction();
            this.BrowsePath();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            if (!this.Source.Saved)
            {
                SVNMonitor.Entities.Source existing = MonitorSettings.Instance.GetEnumerableSources().FirstOrDefault<SVNMonitor.Entities.Source>(s => s.Path.Equals(this.Source.Path, StringComparison.InvariantCultureIgnoreCase));
                if ((existing != null) && (MessageBox.Show(Strings.WarningExistingSourceWithSamePath_FORMAT.FormatWith(new object[] { existing.Name }), Strings.SVNMonitorCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) == DialogResult.No))
                {
                    e.Cancel = true;
                }
            }
        }

        private void radioPathUrl_CheckedChanged(object sender, EventArgs e)
        {
            Logger.LogUserAction();
            this.SetPathOrUrlEnabled();
        }

        private void RegisterChangedEvents()
        {
            this.txtName.TextChanged += new EventHandler(this.Field_Changed);
            this.txtPassword.TextChanged += new EventHandler(this.Field_Changed);
            this.txtPath.TextChanged += new EventHandler(this.Field_Changed);
            this.txtUsername.TextChanged += new EventHandler(this.Field_Changed);
            this.checkAuthenticate.CheckedChanged += new EventHandler(this.Field_Changed);
            this.checkEnabled.CheckedChanged += new EventHandler(this.Field_Changed);
            this.radioUrl.CheckedChanged += new EventHandler(this.Field_Changed);
            this.radioPath.CheckedChanged += new EventHandler(this.Field_Changed);
            this.txtUrl.TextChanged += new EventHandler(this.Field_Changed);
        }

        private void Save()
        {
            this.Source.Name = this.txtName.Text;
            this.Source.Enabled = this.checkEnabled.Checked;
            this.Source.EnableRecommendations = this.checkEnableRecommendations.Checked;
            this.Source.Path = this.radioPath.Checked ? this.txtPath.Text : this.txtUrl.Text;
            this.Source.Authenticate = this.checkAuthenticate.Checked;
            this.Source.UserName = this.txtUsername.Text;
            this.Source.Password = this.txtPassword.Text;
        }

        private void SetPathOrUrlEnabled()
        {
            if (this.radioPath.Checked)
            {
                this.txtPath.Enabled = true;
                this.txtUrl.Enabled = false;
                this.linkPath.Visible = true;
                this.txtPath.Focus();
            }
            else
            {
                this.txtPath.Enabled = false;
                this.txtUrl.Enabled = true;
                this.linkPath.Visible = false;
                this.txtUrl.Focus();
            }
        }

        public static DialogResult ShowDialog(SVNMonitor.Entities.Source source)
        {
            SourcePropertiesDialog dialog = new SourcePropertiesDialog(source);
            return dialog.ShowDialog();
        }

        private void SourcePropertiesDialog_Load(object sender, EventArgs e)
        {
            this.RegisterChangedEvents();
            this.CheckChanges();
        }

        [Browsable(false)]
        private bool IsValid
        {
            get
            {
                if (!string.IsNullOrEmpty(this.txtName.Text))
                {
                    if (this.radioPath.Checked)
                    {
                        if (string.IsNullOrEmpty(this.txtPath.Text))
                        {
                            return false;
                        }
                        goto Label_0058;
                    }
                    if (this.radioUrl.Checked)
                    {
                        if (string.IsNullOrEmpty(this.txtUrl.Text))
                        {
                            return false;
                        }
                        goto Label_0058;
                    }
                }
                return false;
            Label_0058:
                if (this.checkAuthenticate.Checked)
                {
                    if (string.IsNullOrEmpty(this.txtUsername.Text))
                    {
                        return false;
                    }
                    if (string.IsNullOrEmpty(this.txtPassword.Text))
                    {
                        return false;
                    }
                }
                return true;
            }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public SVNMonitor.Entities.Source Source
        {
            [DebuggerNonUserCode]
            get
            {
                return this.source;
            }
            private set
            {
                this.source = value;
                if (this.Source != null)
                {
                    this.Source.SetRejectionPoint();
                    this.BindData();
                }
            }
        }
    }
}

