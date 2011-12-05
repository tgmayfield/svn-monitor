using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using SVNMonitor.Resources;

namespace SVNMonitor.View.Controls
{
	internal class AnimationProgressBar : UserControl
	{
		private IContainer components;
		private Label label1;
		private PictureBox pictureBox1;
		private TableLayoutPanel tableLayoutPanel1;

		public AnimationProgressBar()
		{
			InitializeComponent();
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void InitializeComponent()
		{
			tableLayoutPanel1 = new TableLayoutPanel();
			label1 = new Label();
			pictureBox1 = new PictureBox();
			tableLayoutPanel1.SuspendLayout();
			((ISupportInitialize)pictureBox1).BeginInit();
			base.SuspendLayout();
			tableLayoutPanel1.ColumnCount = 2;
			tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
			tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
			tableLayoutPanel1.Controls.Add(label1, 0, 0);
			tableLayoutPanel1.Controls.Add(pictureBox1, 0, 0);
			tableLayoutPanel1.Dock = DockStyle.Fill;
			tableLayoutPanel1.Location = new Point(0, 0);
			tableLayoutPanel1.Name = "tableLayoutPanel1";
			tableLayoutPanel1.RowCount = 1;
			tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
			tableLayoutPanel1.Size = new Size(0x191, 0x1a);
			tableLayoutPanel1.TabIndex = 0;
			label1.AutoSize = true;
			label1.Dock = DockStyle.Fill;
			label1.Location = new Point(0xe7, 2);
			label1.Margin = new Padding(3, 2, 3, 0);
			label1.Name = "label1";
			label1.Size = new Size(0xa7, 0x18);
			label1.TabIndex = 3;
			label1.TextAlign = ContentAlignment.MiddleLeft;
			pictureBox1.Dock = DockStyle.Fill;
			pictureBox1.Image = Images.animation_8_1;
			pictureBox1.Location = new Point(3, 3);
			pictureBox1.Name = "pictureBox1";
			pictureBox1.Size = new Size(0xde, 20);
			pictureBox1.TabIndex = 2;
			pictureBox1.TabStop = false;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.Controls.Add(tableLayoutPanel1);
			base.Name = "AnimationProgressBar";
			base.Size = new Size(0x191, 0x1a);
			tableLayoutPanel1.ResumeLayout(false);
			tableLayoutPanel1.PerformLayout();
			((ISupportInitialize)pictureBox1).EndInit();
			base.ResumeLayout(false);
		}

		public override string Text
		{
			get { return label1.Text; }
			set { label1.Text = value; }
		}
	}
}