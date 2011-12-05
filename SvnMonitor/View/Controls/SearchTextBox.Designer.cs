namespace SVNMonitor.View.Controls
{
	public partial class SearchTextBox<T>
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
			editBox1 = new Janus.Windows.GridEX.EditControls.EditBox();
			panel1 = new System.Windows.Forms.Panel();
			panel1.SuspendLayout();
			base.SuspendLayout();
			editBox1.BackColor = System.Drawing.Color.White;
			editBox1.BorderStyle = Janus.Windows.GridEX.BorderStyle.Flat;
			editBox1.ButtonImageSize = new System.Drawing.Size(12, 12);
			editBox1.Dock = System.Windows.Forms.DockStyle.Fill;
			editBox1.Location = new System.Drawing.Point(0, 0);
			editBox1.Name = "editBox1";
			editBox1.Size = new System.Drawing.Size(0x114, 20);
			editBox1.TabIndex = 1;
			editBox1.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005;
			editBox1.TextChanged += editBox1_TextChanged;
			editBox1.ButtonClick += btnClear_Click;
			editBox1.Leave += editBox1_Leave;
			editBox1.Enter += editBox1_Enter;
			editBox1.KeyDown += editBox1_KeyDown;
			panel1.Controls.Add(editBox1);
			panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			panel1.Location = new System.Drawing.Point(0, 0);
			panel1.Name = "panel1";
			panel1.Size = new System.Drawing.Size(0x114, 20);
			panel1.TabIndex = 2;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.Transparent;
			base.Controls.Add(panel1);
			base.Name = "SearchTextBox";
			base.Size = new System.Drawing.Size(0x114, 20);
			panel1.ResumeLayout(false);
			panel1.PerformLayout();
			base.ResumeLayout(false);
		}

		#endregion
	
		private static bool dpiErrorLogged;
		private Janus.Windows.GridEX.EditControls.EditBox editBox1;
		private System.Windows.Forms.Panel panel1;
		private int resultsCount;
		private SVNMonitor.View.Interfaces.ISearchablePanel<T> searchablePanel;
		private System.Timers.Timer searchTimer;

	}
}
