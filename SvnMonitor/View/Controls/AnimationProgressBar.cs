using System.Windows.Forms;
using System.ComponentModel;
using System;
using System.Drawing;
using SVNMonitor.Resources;

namespace SVNMonitor.View.Controls
{
internal class AnimationProgressBar : UserControl
{
	private IContainer components;

	private Label label1;

	private PictureBox pictureBox1;

	private TableLayoutPanel tableLayoutPanel1;

	public string Text
	{
		get
		{
			return this.label1.Text;
		}
		set
		{
			this.label1.Text = value;
		}
	}

	public AnimationProgressBar()
	{
		this.InitializeComponent();
	}

	protected override void Dispose(bool disposing)
	{
		if (disposing && this.components != null)
		{
			this.components.Dispose();
		}
		base.Dispose(disposing);
	}

	private void InitializeComponent()
	{
		this.tableLayoutPanel1 = new TableLayoutPanel();
		this.label1 = new Label();
		this.pictureBox1 = new PictureBox();
		this.tableLayoutPanel1.SuspendLayout();
		this.pictureBox1.BeginInit();
		base.SuspendLayout();
		this.tableLayoutPanel1.ColumnCount = 2;
		this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
		this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
		this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
		this.tableLayoutPanel1.Controls.Add(this.pictureBox1, 0, 0);
		this.tableLayoutPanel1.Dock = DockStyle.Fill;
		this.tableLayoutPanel1.Location = new Point(0, 0);
		this.tableLayoutPanel1.Name = "tableLayoutPanel1";
		this.tableLayoutPanel1.RowCount = 1;
		this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
		this.tableLayoutPanel1.Size = new Size(401, 26);
		this.tableLayoutPanel1.TabIndex = 0;
		this.label1.AutoSize = true;
		this.label1.Dock = DockStyle.Fill;
		this.label1.Location = new Point(231, 2);
		this.label1.Margin = new Padding(3, 2, 3, 0);
		this.label1.Name = "label1";
		this.label1.Size = new Size(167, 24);
		this.label1.TabIndex = 3;
		this.label1.TextAlign = ContentAlignment.MiddleLeft;
		this.pictureBox1.Dock = DockStyle.Fill;
		this.pictureBox1.Image = Images.animation_8_1;
		this.pictureBox1.Location = new Point(3, 3);
		this.pictureBox1.Name = "pictureBox1";
		this.pictureBox1.Size = new Size(222, 20);
		this.pictureBox1.TabIndex = 2;
		this.pictureBox1.TabStop = false;
		base.AutoScaleDimensions = new SizeF(6, 13);
		base.AutoScaleMode = AutoScaleMode.Font;
		base.Controls.Add(this.tableLayoutPanel1);
		base.Name = "AnimationProgressBar";
		base.Size = new Size(401, 26);
		this.tableLayoutPanel1.ResumeLayout(false);
		this.tableLayoutPanel1.PerformLayout();
		this.pictureBox1.EndInit();
		base.ResumeLayout(false);
	}
}
}