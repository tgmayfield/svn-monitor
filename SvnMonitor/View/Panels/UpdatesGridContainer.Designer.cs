namespace SVNMonitor.View.Panels
{
	internal partial class UpdatesGridContainer
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
			Janus.Windows.GridEX.GridEXLayout logEntriesGrid1_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UpdatesGridContainer));
			logEntriesGrid1 = new SVNMonitor.View.Controls.LogEntriesGrid();
			((System.ComponentModel.ISupportInitialize)logEntriesGrid1).BeginInit();
			base.SuspendLayout();
			logEntriesGrid1.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False;
			logEntriesGrid1.BorderStyle = Janus.Windows.GridEX.BorderStyle.None;
			logEntriesGrid1.BuiltInTextsData = "<LocalizableData ID=\"LocalizableStrings\" Collection=\"true\"><FilterRowConditionEqual>xxxxx</FilterRowConditionEqual></LocalizableData>";
			logEntriesGrid1.ColumnAutoResize = true;
			logEntriesGrid1_DesignTimeLayout.LayoutString = resources.GetString("logEntriesGrid1_DesignTimeLayout.LayoutString");
			logEntriesGrid1.DesignTimeLayout = logEntriesGrid1_DesignTimeLayout;
			logEntriesGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
			logEntriesGrid1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			logEntriesGrid1.GridLineColor = System.Drawing.Color.WhiteSmoke;
			logEntriesGrid1.GridLines = Janus.Windows.GridEX.GridLines.Horizontal;
			logEntriesGrid1.GroupByBoxVisible = false;
			logEntriesGrid1.HideSelection = Janus.Windows.GridEX.HideSelection.HighlightInactive;
			logEntriesGrid1.Hierarchical = true;
			logEntriesGrid1.Location = new System.Drawing.Point(0, 0);
			logEntriesGrid1.Name = "logEntriesGrid1";
			logEntriesGrid1.RepeatHeaders = Janus.Windows.GridEX.InheritableBoolean.False;
			logEntriesGrid1.SelectedInactiveFormatStyle.BackColor = System.Drawing.Color.WhiteSmoke;
			logEntriesGrid1.SelectionMode = Janus.Windows.GridEX.SelectionMode.MultipleSelection;
			logEntriesGrid1.Size = new System.Drawing.Size(0x238, 0x17e);
			logEntriesGrid1.TabIndex = 4;
			logEntriesGrid1.TreeLineColor = System.Drawing.SystemColors.ControlLight;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.Controls.Add(logEntriesGrid1);
			base.Name = "UpdatesGridContainer";
			base.Size = new System.Drawing.Size(0x238, 0x17e);
			((System.ComponentModel.ISupportInitialize)logEntriesGrid1).EndInit();
			base.ResumeLayout(false);
		}

		#endregion

		private SVNMonitor.View.Controls.LogEntriesGrid logEntriesGrid1;
	}
}