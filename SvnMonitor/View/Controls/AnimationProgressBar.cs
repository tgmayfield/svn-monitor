namespace SVNMonitor.View.Controls
{
    using SVNMonitor.Resources;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    internal class AnimationProgressBar : UserControl
    {
        private IContainer components;
        private Label label1;
        private PictureBox pictureBox1;
        private TableLayoutPanel tableLayoutPanel1;

        public AnimationProgressBar()
        {
            this.InitializeComponent();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
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
            ((ISupportInitialize) this.pictureBox1).BeginInit();
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
            this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
            this.tableLayoutPanel1.Size = new Size(0x191, 0x1a);
            this.tableLayoutPanel1.TabIndex = 0;
            this.label1.AutoSize = true;
            this.label1.Dock = DockStyle.Fill;
            this.label1.Location = new Point(0xe7, 2);
            this.label1.Margin = new Padding(3, 2, 3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0xa7, 0x18);
            this.label1.TabIndex = 3;
            this.label1.TextAlign = ContentAlignment.MiddleLeft;
            this.pictureBox1.Dock = DockStyle.Fill;
            this.pictureBox1.Image = Images.animation_8_1;
            this.pictureBox1.Location = new Point(3, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new Size(0xde, 20);
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.Controls.Add(this.tableLayoutPanel1);
            base.Name = "AnimationProgressBar";
            base.Size = new Size(0x191, 0x1a);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((ISupportInitialize) this.pictureBox1).EndInit();
            base.ResumeLayout(false);
        }

        public override string Text
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
    }
}

