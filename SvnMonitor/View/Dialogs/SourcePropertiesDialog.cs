using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

using SVNMonitor.Extensions;
using SVNMonitor.Logging;
using SVNMonitor.Resources.Text;

namespace SVNMonitor.View.Dialogs
{
	internal partial class SourcePropertiesDialog : BaseDialog
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
			InitializeComponent();
		}

		public SourcePropertiesDialog(SVNMonitor.Entities.Source source)
			: this()
		{
			Source = source;
		}

		private void BindData()
		{
			txtName.Text = Source.Name;
			checkEnabled.Checked = Source.Enabled;
			checkEnableRecommendations.Checked = Source.EnableRecommendations;
			if (Source.IsURL)
			{
				txtUrl.Text = Source.Path;
				radioUrl.Checked = true;
			}
			else
			{
				txtPath.Text = Source.Path;
				radioPath.Checked = true;
			}
			checkAuthenticate.Checked = Source.Authenticate;
			txtUsername.Text = Source.UserName;
			txtPassword.Text = Source.Password;
		}

		private void BrowsePath()
		{
			FolderBrowserDialog tempLocal0 = new FolderBrowserDialog
			{
				SelectedPath = txtPath.Text
			};
			FolderBrowserDialog dialog = tempLocal0;
			if (dialog.ShowDialog() == DialogResult.OK)
			{
				txtPath.Text = dialog.SelectedPath;
			}
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			Logger.LogUserAction();
			Save();
		}

		private void checkAuthenticate_CheckedChanged(object sender, EventArgs e)
		{
			Logger.LogUserAction();
			groupAuthenticate.Enabled = checkAuthenticate.Checked;
		}

		private void CheckChanges()
		{
			btnOK.Enabled = IsValid;
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void Field_Changed(object sender, EventArgs e)
		{
			Logger.LogUserAction();
			CheckChanges();
		}

		private void InitializeComponent()
		{
			ComponentResourceManager resources = new ComponentResourceManager(typeof(SourcePropertiesDialog));
			groupSource = new GroupBox();
			groupPath = new GroupBox();
			linkPath = new LinkLabel();
			lblUrlInfo = new Label();
			radioUrl = new RadioButton();
			txtPath = new TextBox();
			radioPath = new RadioButton();
			txtUrl = new TextBox();
			lblAuthenticationNote = new Label();
			checkEnableRecommendations = new CheckBox();
			checkEnabled = new CheckBox();
			groupAuthenticate = new GroupBox();
			txtPassword = new TextBox();
			lblPassword = new Label();
			txtUsername = new TextBox();
			lblUserName = new Label();
			txtName = new TextBox();
			checkAuthenticate = new CheckBox();
			lblName = new Label();
			btnCancel = new Button();
			btnOK = new Button();
			groupSource.SuspendLayout();
			groupPath.SuspendLayout();
			groupAuthenticate.SuspendLayout();
			base.SuspendLayout();
			resources.ApplyResources(groupSource, "groupSource");
			groupSource.Controls.Add(groupPath);
			groupSource.Controls.Add(lblAuthenticationNote);
			groupSource.Controls.Add(checkEnableRecommendations);
			groupSource.Controls.Add(checkEnabled);
			groupSource.Controls.Add(groupAuthenticate);
			groupSource.Controls.Add(txtName);
			groupSource.Controls.Add(checkAuthenticate);
			groupSource.Controls.Add(lblName);
			groupSource.Name = "groupSource";
			groupSource.TabStop = false;
			resources.ApplyResources(groupPath, "groupPath");
			groupPath.Controls.Add(linkPath);
			groupPath.Controls.Add(lblUrlInfo);
			groupPath.Controls.Add(radioUrl);
			groupPath.Controls.Add(txtPath);
			groupPath.Controls.Add(radioPath);
			groupPath.Controls.Add(txtUrl);
			groupPath.Name = "groupPath";
			groupPath.TabStop = false;
			resources.ApplyResources(linkPath, "linkPath");
			linkPath.Name = "linkPath";
			linkPath.TabStop = true;
			linkPath.LinkClicked += linkPath_LinkClicked;
			resources.ApplyResources(lblUrlInfo, "lblUrlInfo");
			lblUrlInfo.ForeColor = Color.DarkRed;
			lblUrlInfo.Name = "lblUrlInfo";
			resources.ApplyResources(radioUrl, "radioUrl");
			radioUrl.Name = "radioUrl";
			radioUrl.TabStop = true;
			radioUrl.UseVisualStyleBackColor = true;
			radioUrl.CheckedChanged += radioPathUrl_CheckedChanged;
			resources.ApplyResources(txtPath, "txtPath");
			txtPath.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
			txtPath.AutoCompleteSource = AutoCompleteSource.FileSystem;
			txtPath.Name = "txtPath";
			resources.ApplyResources(radioPath, "radioPath");
			radioPath.Name = "radioPath";
			radioPath.TabStop = true;
			radioPath.UseVisualStyleBackColor = true;
			radioPath.CheckedChanged += radioPathUrl_CheckedChanged;
			resources.ApplyResources(txtUrl, "txtUrl");
			txtUrl.Name = "txtUrl";
			resources.ApplyResources(lblAuthenticationNote, "lblAuthenticationNote");
			lblAuthenticationNote.ForeColor = Color.DarkGreen;
			lblAuthenticationNote.Name = "lblAuthenticationNote";
			resources.ApplyResources(checkEnableRecommendations, "checkEnableRecommendations");
			checkEnableRecommendations.Name = "checkEnableRecommendations";
			resources.ApplyResources(checkEnabled, "checkEnabled");
			checkEnabled.Name = "checkEnabled";
			resources.ApplyResources(groupAuthenticate, "groupAuthenticate");
			groupAuthenticate.Controls.Add(txtPassword);
			groupAuthenticate.Controls.Add(lblPassword);
			groupAuthenticate.Controls.Add(txtUsername);
			groupAuthenticate.Controls.Add(lblUserName);
			groupAuthenticate.Name = "groupAuthenticate";
			groupAuthenticate.TabStop = false;
			resources.ApplyResources(txtPassword, "txtPassword");
			txtPassword.Name = "txtPassword";
			resources.ApplyResources(lblPassword, "lblPassword");
			lblPassword.Name = "lblPassword";
			resources.ApplyResources(txtUsername, "txtUsername");
			txtUsername.Name = "txtUsername";
			resources.ApplyResources(lblUserName, "lblUserName");
			lblUserName.Name = "lblUserName";
			resources.ApplyResources(txtName, "txtName");
			txtName.Name = "txtName";
			resources.ApplyResources(checkAuthenticate, "checkAuthenticate");
			checkAuthenticate.Name = "checkAuthenticate";
			checkAuthenticate.CheckedChanged += checkAuthenticate_CheckedChanged;
			resources.ApplyResources(lblName, "lblName");
			lblName.Name = "lblName";
			resources.ApplyResources(btnCancel, "btnCancel");
			btnCancel.DialogResult = DialogResult.Cancel;
			btnCancel.Name = "btnCancel";
			resources.ApplyResources(btnOK, "btnOK");
			btnOK.DialogResult = DialogResult.OK;
			btnOK.Name = "btnOK";
			btnOK.Click += btnOK_Click;
			base.AcceptButton = btnOK;
			resources.ApplyResources(this, "$this");
			base.AutoScaleMode = AutoScaleMode.Font;
			base.CancelButton = btnCancel;
			base.Controls.Add(btnOK);
			base.Controls.Add(btnCancel);
			base.Controls.Add(groupSource);
			base.FormBorderStyle = FormBorderStyle.FixedDialog;
			base.KeyPreview = true;
			base.Name = "SourcePropertiesDialog";
			base.ShowInTaskbar = false;
			base.Load += SourcePropertiesDialog_Load;
			groupSource.ResumeLayout(false);
			groupSource.PerformLayout();
			groupPath.ResumeLayout(false);
			groupPath.PerformLayout();
			groupAuthenticate.ResumeLayout(false);
			groupAuthenticate.PerformLayout();
			base.ResumeLayout(false);
		}

		private void linkPath_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			Logger.LogUserAction();
			BrowsePath();
		}

		protected override void OnFormClosing(FormClosingEventArgs e)
		{
			base.OnFormClosing(e);
			if (!Source.Saved)
			{
				SVNMonitor.Entities.Source existing = MonitorSettings.Instance.GetEnumerableSources().FirstOrDefault(s => s.Path.Equals(Source.Path, StringComparison.InvariantCultureIgnoreCase));
				if ((existing != null) && (MessageBox.Show(Strings.WarningExistingSourceWithSamePath_FORMAT.FormatWith(new object[]
				{
					existing.Name
				}), Strings.SVNMonitorCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) == DialogResult.No))
				{
					e.Cancel = true;
				}
			}
		}

		private void radioPathUrl_CheckedChanged(object sender, EventArgs e)
		{
			Logger.LogUserAction();
			SetPathOrUrlEnabled();
		}

		private void RegisterChangedEvents()
		{
			txtName.TextChanged += Field_Changed;
			txtPassword.TextChanged += Field_Changed;
			txtPath.TextChanged += Field_Changed;
			txtUsername.TextChanged += Field_Changed;
			checkAuthenticate.CheckedChanged += Field_Changed;
			checkEnabled.CheckedChanged += Field_Changed;
			radioUrl.CheckedChanged += Field_Changed;
			radioPath.CheckedChanged += Field_Changed;
			txtUrl.TextChanged += Field_Changed;
		}

		private void Save()
		{
			Source.Name = txtName.Text;
			Source.Enabled = checkEnabled.Checked;
			Source.EnableRecommendations = checkEnableRecommendations.Checked;
			Source.Path = radioPath.Checked ? txtPath.Text : txtUrl.Text;
			Source.Authenticate = checkAuthenticate.Checked;
			Source.UserName = txtUsername.Text;
			Source.Password = txtPassword.Text;
		}

		private void SetPathOrUrlEnabled()
		{
			if (radioPath.Checked)
			{
				txtPath.Enabled = true;
				txtUrl.Enabled = false;
				linkPath.Visible = true;
				txtPath.Focus();
			}
			else
			{
				txtPath.Enabled = false;
				txtUrl.Enabled = true;
				linkPath.Visible = false;
				txtUrl.Focus();
			}
		}

		public static DialogResult ShowDialog(SVNMonitor.Entities.Source source)
		{
			SourcePropertiesDialog dialog = new SourcePropertiesDialog(source);
			return dialog.ShowDialog();
		}

		private void SourcePropertiesDialog_Load(object sender, EventArgs e)
		{
			RegisterChangedEvents();
			CheckChanges();
		}

		[Browsable(false)]
		private bool IsValid
		{
			get
			{
				if (!string.IsNullOrEmpty(txtName.Text))
				{
					if (radioPath.Checked)
					{
						if (string.IsNullOrEmpty(txtPath.Text))
						{
							return false;
						}
						goto Label_0058;
					}
					if (radioUrl.Checked)
					{
						if (string.IsNullOrEmpty(txtUrl.Text))
						{
							return false;
						}
						goto Label_0058;
					}
				}
				return false;
				Label_0058:
				if (checkAuthenticate.Checked)
				{
					if (string.IsNullOrEmpty(txtUsername.Text))
					{
						return false;
					}
					if (string.IsNullOrEmpty(txtPassword.Text))
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
			get { return source; }
			private set
			{
				source = value;
				if (Source != null)
				{
					Source.SetRejectionPoint();
					BindData();
				}
			}
		}
	}
}