namespace SVNMonitor.View.Dialogs
{
	internal partial class SourcePropertiesDialog
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SourcePropertiesDialog));
			groupSource = new System.Windows.Forms.GroupBox();
			groupPath = new System.Windows.Forms.GroupBox();
			linkPath = new System.Windows.Forms.LinkLabel();
			lblUrlInfo = new System.Windows.Forms.Label();
			radioUrl = new System.Windows.Forms.RadioButton();
			txtPath = new System.Windows.Forms.TextBox();
			radioPath = new System.Windows.Forms.RadioButton();
			txtUrl = new System.Windows.Forms.TextBox();
			lblAuthenticationNote = new System.Windows.Forms.Label();
			checkEnableRecommendations = new System.Windows.Forms.CheckBox();
			checkEnabled = new System.Windows.Forms.CheckBox();
			groupAuthenticate = new System.Windows.Forms.GroupBox();
			txtPassword = new System.Windows.Forms.TextBox();
			lblPassword = new System.Windows.Forms.Label();
			txtUsername = new System.Windows.Forms.TextBox();
			lblUserName = new System.Windows.Forms.Label();
			txtName = new System.Windows.Forms.TextBox();
			checkAuthenticate = new System.Windows.Forms.CheckBox();
			lblName = new System.Windows.Forms.Label();
			btnCancel = new System.Windows.Forms.Button();
			btnOK = new System.Windows.Forms.Button();
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
			lblUrlInfo.ForeColor = System.Drawing.Color.DarkRed;
			lblUrlInfo.Name = "lblUrlInfo";
			resources.ApplyResources(radioUrl, "radioUrl");
			radioUrl.Name = "radioUrl";
			radioUrl.TabStop = true;
			radioUrl.UseVisualStyleBackColor = true;
			radioUrl.CheckedChanged += radioPathUrl_CheckedChanged;
			resources.ApplyResources(txtPath, "txtPath");
			txtPath.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			txtPath.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystem;
			txtPath.Name = "txtPath";
			resources.ApplyResources(radioPath, "radioPath");
			radioPath.Name = "radioPath";
			radioPath.TabStop = true;
			radioPath.UseVisualStyleBackColor = true;
			radioPath.CheckedChanged += radioPathUrl_CheckedChanged;
			resources.ApplyResources(txtUrl, "txtUrl");
			txtUrl.Name = "txtUrl";
			resources.ApplyResources(lblAuthenticationNote, "lblAuthenticationNote");
			lblAuthenticationNote.ForeColor = System.Drawing.Color.DarkGreen;
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
			btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			btnCancel.Name = "btnCancel";
			resources.ApplyResources(btnOK, "btnOK");
			btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			btnOK.Name = "btnOK";
			btnOK.Click += btnOK_Click;
			base.AcceptButton = btnOK;
			resources.ApplyResources(this, "$this");
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.CancelButton = btnCancel;
			base.Controls.Add(btnOK);
			base.Controls.Add(btnCancel);
			base.Controls.Add(groupSource);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
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

		#endregion
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.CheckBox checkAuthenticate;
		private System.Windows.Forms.CheckBox checkEnabled;
		private System.Windows.Forms.CheckBox checkEnableRecommendations;
		private System.Windows.Forms.GroupBox groupAuthenticate;
		private System.Windows.Forms.GroupBox groupPath;
		private System.Windows.Forms.GroupBox groupSource;
		private System.Windows.Forms.Label lblAuthenticationNote;
		private System.Windows.Forms.Label lblName;
		private System.Windows.Forms.Label lblPassword;
		private System.Windows.Forms.Label lblUrlInfo;
		private System.Windows.Forms.Label lblUserName;
		private System.Windows.Forms.LinkLabel linkPath;
		private System.Windows.Forms.RadioButton radioPath;
		private System.Windows.Forms.RadioButton radioUrl;
		private System.Windows.Forms.TextBox txtName;
		private System.Windows.Forms.TextBox txtPassword;
		private System.Windows.Forms.TextBox txtPath;
		private System.Windows.Forms.TextBox txtUrl;
		private System.Windows.Forms.TextBox txtUsername;
	}
}