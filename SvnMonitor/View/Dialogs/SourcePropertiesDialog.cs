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
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private SVNMonitor.Entities.Source source;
		
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

		private void Field_Changed(object sender, EventArgs e)
		{
			Logger.LogUserAction();
			CheckChanges();
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