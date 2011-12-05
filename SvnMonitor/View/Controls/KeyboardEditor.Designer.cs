namespace SVNMonitor.View.Controls
{
	public partial class KeyboardEditor
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
			Janus.Windows.GridEX.GridEXLayout gridEX1_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(KeyboardEditor));
			gridEX1 = new Janus.Windows.GridEX.GridEX();
			checkWin = new System.Windows.Forms.CheckBox();
			comboKey = new System.Windows.Forms.ComboBox();
			checkControl = new System.Windows.Forms.CheckBox();
			checkShift = new System.Windows.Forms.CheckBox();
			checkAlt = new System.Windows.Forms.CheckBox();
			btnApply = new System.Windows.Forms.Button();
			btnReset = new System.Windows.Forms.Button();
			btnDefault = new System.Windows.Forms.Button();
			uiGroupBox1 = new System.Windows.Forms.GroupBox();
			lblDescription = new System.Windows.Forms.Label();
			lblDefaultKeyString = new System.Windows.Forms.Label();
			panel1 = new System.Windows.Forms.Panel();
			panel2 = new System.Windows.Forms.Panel();
			((System.ComponentModel.ISupportInitialize)gridEX1).BeginInit();
			uiGroupBox1.SuspendLayout();
			panel1.SuspendLayout();
			panel2.SuspendLayout();
			base.SuspendLayout();
			gridEX1.AllowColumnDrag = false;
			gridEX1.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False;
			gridEX1.AutomaticSort = false;
			gridEX1.ColumnAutoResize = true;
			gridEX1.ColumnHeaders = Janus.Windows.GridEX.InheritableBoolean.False;
			resources.ApplyResources(gridEX1_DesignTimeLayout, "gridEX1_DesignTimeLayout");
			gridEX1.DesignTimeLayout = gridEX1_DesignTimeLayout;
			resources.ApplyResources(gridEX1, "gridEX1");
			gridEX1.EnterKeyBehavior = Janus.Windows.GridEX.EnterKeyBehavior.None;
			gridEX1.FocusStyle = Janus.Windows.GridEX.FocusStyle.Solid;
			gridEX1.GridLineColor = System.Drawing.Color.Silver;
			gridEX1.GridLines = Janus.Windows.GridEX.GridLines.Horizontal;
			gridEX1.GroupByBoxVisible = false;
			gridEX1.HideSelection = Janus.Windows.GridEX.HideSelection.HighlightInactive;
			gridEX1.Name = "gridEX1";
			gridEX1.TabKeyBehavior = Janus.Windows.GridEX.TabKeyBehavior.ControlNavigation;
			gridEX1.FormattingRow += gridEX1_FormattingRow;
			gridEX1.SelectionChanged += gridEX1_SelectionChanged;
			resources.ApplyResources(checkWin, "checkWin");
			checkWin.Name = "checkWin";
			checkWin.CheckedChanged += Check_Changed;
			resources.ApplyResources(comboKey, "comboKey");
			comboKey.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			comboKey.Name = "comboKey";
			comboKey.SelectedIndexChanged += comboKey_SelectedIndexChanged;
			resources.ApplyResources(checkControl, "checkControl");
			checkControl.Name = "checkControl";
			checkControl.CheckedChanged += Check_Changed;
			resources.ApplyResources(checkShift, "checkShift");
			checkShift.Name = "checkShift";
			checkShift.CheckedChanged += Check_Changed;
			resources.ApplyResources(checkAlt, "checkAlt");
			checkAlt.Name = "checkAlt";
			checkAlt.CheckedChanged += Check_Changed;
			resources.ApplyResources(btnApply, "btnApply");
			btnApply.Name = "btnApply";
			btnApply.Click += btnApply_Click;
			resources.ApplyResources(btnReset, "btnReset");
			btnReset.Name = "btnReset";
			btnReset.Click += btnReset_Click;
			resources.ApplyResources(btnDefault, "btnDefault");
			btnDefault.Name = "btnDefault";
			btnDefault.Click += btnDefault_Click;
			uiGroupBox1.Controls.Add(lblDescription);
			uiGroupBox1.Controls.Add(comboKey);
			uiGroupBox1.Controls.Add(checkAlt);
			uiGroupBox1.Controls.Add(checkShift);
			uiGroupBox1.Controls.Add(checkControl);
			uiGroupBox1.Controls.Add(checkWin);
			uiGroupBox1.Controls.Add(lblDefaultKeyString);
			resources.ApplyResources(uiGroupBox1, "uiGroupBox1");
			uiGroupBox1.Name = "uiGroupBox1";
			uiGroupBox1.TabStop = false;
			resources.ApplyResources(lblDescription, "lblDescription");
			lblDescription.Name = "lblDescription";
			resources.ApplyResources(lblDefaultKeyString, "lblDefaultKeyString");
			lblDefaultKeyString.BackColor = System.Drawing.Color.Transparent;
			lblDefaultKeyString.ForeColor = System.Drawing.Color.MidnightBlue;
			lblDefaultKeyString.Name = "lblDefaultKeyString";
			panel1.Controls.Add(btnDefault);
			panel1.Controls.Add(btnApply);
			panel1.Controls.Add(btnReset);
			resources.ApplyResources(panel1, "panel1");
			panel1.Name = "panel1";
			panel2.Controls.Add(uiGroupBox1);
			resources.ApplyResources(panel2, "panel2");
			panel2.Name = "panel2";
			resources.ApplyResources(this, "$this");
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.Controls.Add(gridEX1);
			base.Controls.Add(panel2);
			base.Controls.Add(panel1);
			base.Name = "KeyboardEditor";
			((System.ComponentModel.ISupportInitialize)gridEX1).EndInit();
			uiGroupBox1.ResumeLayout(false);
			uiGroupBox1.PerformLayout();
			panel1.ResumeLayout(false);
			panel2.ResumeLayout(false);
			base.ResumeLayout(false);
		}
		#endregion

		private System.Windows.Forms.Button btnApply;
		private System.Windows.Forms.Button btnDefault;
		private System.Windows.Forms.Button btnReset;
		private System.Windows.Forms.CheckBox checkAlt;
		private System.Windows.Forms.CheckBox checkControl;
		private System.Windows.Forms.CheckBox checkShift;
		private System.Windows.Forms.CheckBox checkWin;
		private System.Windows.Forms.ComboBox comboKey;
		private Janus.Windows.GridEX.GridEX gridEX1;
		private System.Windows.Forms.Label lblDefaultKeyString;
		private System.Windows.Forms.Label lblDescription;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.GroupBox uiGroupBox1;
	}
}
