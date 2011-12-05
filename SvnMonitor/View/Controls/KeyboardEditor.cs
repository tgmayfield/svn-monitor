using System.Linq;

namespace SVNMonitor.View.Controls
{
    using Janus.Windows.GridEX;
    using SVNMonitor.Extensions;
    using SVNMonitor.Helpers;
    using SVNMonitor.Logging;
    using SVNMonitor.Resources.Text;
    using SVNMonitor.Settings;
    using SVNMonitor.View;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Drawing;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;

    public class KeyboardEditor : UserControl
    {
        private Button btnApply;
        private Button btnDefault;
        private Button btnReset;
        private CheckBox checkAlt;
        private CheckBox checkControl;
        private CheckBox checkShift;
        private CheckBox checkWin;
        private ComboBox comboKey;
        private IContainer components;
        private Janus.Windows.GridEX.GridEX gridEX1;
        private Label lblDefaultKeyString;
        private Label lblDescription;
        private IEnumerable<KeyboardEditorRow> list;
        private Panel panel1;
        private Panel panel2;
        private bool? refreshButtons;
        private GroupBox uiGroupBox1;

        public KeyboardEditor()
        {
            this.InitializeComponent();
        }

        private void ApplyKeyInfo()
        {
            KeyboardEditorRow row = this.SelectedRow;
            if (row != null)
            {
                KeyInfo keyInfo = this.GetKeyInfo();
                KeyboardEditorRow assignedRow = null;
                if (!this.IsValidKeyInfo(keyInfo, out assignedRow))
                {
                    if (assignedRow != null)
                    {
                        MainForm.FormInstance.ShowErrorMessage(Strings.ErrorKeyAlreadyAssigned_FORMAT.FormatWith(new object[] { assignedRow.Text }), Strings.SVNMonitorCaption);
                    }
                }
                else
                {
                    row.KeyInfo = this.GetKeyInfo();
                    this.Refetch();
                }
            }
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            Logger.LogUserAction();
            this.ApplyKeyInfo();
        }

        private void btnDefault_Click(object sender, EventArgs e)
        {
            Logger.LogUserAction();
            this.refreshButtons = true;
            this.SetDefaultKeyInfo();
            this.ApplyKeyInfo();
            this.refreshButtons = null;
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            Logger.LogUserAction();
            this.refreshButtons = true;
            this.ResetKeyInfo();
            this.refreshButtons = null;
        }

        private void Check_Changed(object sender, EventArgs e)
        {
            Logger.LogUserAction();
            this.RefreshButtons();
        }

        private void comboKey_SelectedIndexChanged(object sender, EventArgs e)
        {
            Logger.LogUserAction();
            this.RefreshButtons();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private KeyInfo GetKeyInfo()
        {
            Key key;
            ModifierKey modifier = ModifierKey.None;
            if (this.checkAlt.Checked)
            {
                modifier |= ModifierKey.None | ModifierKey.Alt;
            }
            if (this.checkControl.Checked)
            {
                modifier |= ModifierKey.Control;
            }
            if (this.checkShift.Checked)
            {
                modifier |= ModifierKey.Shift;
            }
            if (this.checkWin.Checked)
            {
                modifier |= ModifierKey.Win;
            }
            string selectedKey = this.comboKey.SelectedItem == null ? Strings.KeyNone : comboKey.SelectedItem.ToString();
            if ((selectedKey == Strings.KeyNone) || string.IsNullOrEmpty(selectedKey))
            {
                key = Key.None;
            }
            else
            {
                key = EnumHelper.ParseEnum<Key>(selectedKey);
            }
            return KeyInfo.GetKeyInfo(modifier, key);
        }

        private KeyInfo GetSelectedDefaultKeyInfo()
        {
            KeyboardEditorRow row = this.SelectedRow;
            if (row == null)
            {
                return KeyInfo.None;
            }
            return KeyInfo.GetKeyInfo(ApplicationSettingsManager.GetDefault<string>(row.AssociatedSetting));
        }

        private void gridEX1_FormattingRow(object sender, RowLoadEventArgs e)
        {
            GridEXCell imageCell = e.Row.Cells["Image"];
            KeyboardEditorRow row = (KeyboardEditorRow) e.Row.DataRow;
            imageCell.Image = row.Image;
        }

        private void gridEX1_SelectionChanged(object sender, EventArgs e)
        {
            this.SetSelectedKeyInfo();
            this.RefreshButtons();
        }

        private void InitializeComponent()
        {
            GridEXLayout gridEX1_DesignTimeLayout = new GridEXLayout();
            ComponentResourceManager resources = new ComponentResourceManager(typeof(KeyboardEditor));
            this.gridEX1 = new Janus.Windows.GridEX.GridEX();
            this.checkWin = new CheckBox();
            this.comboKey = new ComboBox();
            this.checkControl = new CheckBox();
            this.checkShift = new CheckBox();
            this.checkAlt = new CheckBox();
            this.btnApply = new Button();
            this.btnReset = new Button();
            this.btnDefault = new Button();
            this.uiGroupBox1 = new GroupBox();
            this.lblDescription = new Label();
            this.lblDefaultKeyString = new Label();
            this.panel1 = new Panel();
            this.panel2 = new Panel();
            ((ISupportInitialize) this.gridEX1).BeginInit();
            this.uiGroupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            base.SuspendLayout();
            this.gridEX1.AllowColumnDrag = false;
            this.gridEX1.AllowEdit = InheritableBoolean.False;
            this.gridEX1.AutomaticSort = false;
            this.gridEX1.ColumnAutoResize = true;
            this.gridEX1.ColumnHeaders = InheritableBoolean.False;
            resources.ApplyResources(gridEX1_DesignTimeLayout, "gridEX1_DesignTimeLayout");
            this.gridEX1.DesignTimeLayout = gridEX1_DesignTimeLayout;
            resources.ApplyResources(this.gridEX1, "gridEX1");
            this.gridEX1.EnterKeyBehavior = EnterKeyBehavior.None;
            this.gridEX1.FocusStyle = FocusStyle.Solid;
            this.gridEX1.GridLineColor = Color.Silver;
            this.gridEX1.GridLines = GridLines.Horizontal;
            this.gridEX1.GroupByBoxVisible = false;
            this.gridEX1.HideSelection = HideSelection.HighlightInactive;
            this.gridEX1.Name = "gridEX1";
            this.gridEX1.TabKeyBehavior = TabKeyBehavior.ControlNavigation;
            this.gridEX1.FormattingRow += new RowLoadEventHandler(this.gridEX1_FormattingRow);
            this.gridEX1.SelectionChanged += new EventHandler(this.gridEX1_SelectionChanged);
            resources.ApplyResources(this.checkWin, "checkWin");
            this.checkWin.Name = "checkWin";
            this.checkWin.CheckedChanged += new EventHandler(this.Check_Changed);
            resources.ApplyResources(this.comboKey, "comboKey");
            this.comboKey.DropDownStyle = ComboBoxStyle.DropDownList;
            this.comboKey.Name = "comboKey";
            this.comboKey.SelectedIndexChanged += new EventHandler(this.comboKey_SelectedIndexChanged);
            resources.ApplyResources(this.checkControl, "checkControl");
            this.checkControl.Name = "checkControl";
            this.checkControl.CheckedChanged += new EventHandler(this.Check_Changed);
            resources.ApplyResources(this.checkShift, "checkShift");
            this.checkShift.Name = "checkShift";
            this.checkShift.CheckedChanged += new EventHandler(this.Check_Changed);
            resources.ApplyResources(this.checkAlt, "checkAlt");
            this.checkAlt.Name = "checkAlt";
            this.checkAlt.CheckedChanged += new EventHandler(this.Check_Changed);
            resources.ApplyResources(this.btnApply, "btnApply");
            this.btnApply.Name = "btnApply";
            this.btnApply.Click += new EventHandler(this.btnApply_Click);
            resources.ApplyResources(this.btnReset, "btnReset");
            this.btnReset.Name = "btnReset";
            this.btnReset.Click += new EventHandler(this.btnReset_Click);
            resources.ApplyResources(this.btnDefault, "btnDefault");
            this.btnDefault.Name = "btnDefault";
            this.btnDefault.Click += new EventHandler(this.btnDefault_Click);
            this.uiGroupBox1.Controls.Add(this.lblDescription);
            this.uiGroupBox1.Controls.Add(this.comboKey);
            this.uiGroupBox1.Controls.Add(this.checkAlt);
            this.uiGroupBox1.Controls.Add(this.checkShift);
            this.uiGroupBox1.Controls.Add(this.checkControl);
            this.uiGroupBox1.Controls.Add(this.checkWin);
            this.uiGroupBox1.Controls.Add(this.lblDefaultKeyString);
            resources.ApplyResources(this.uiGroupBox1, "uiGroupBox1");
            this.uiGroupBox1.Name = "uiGroupBox1";
            this.uiGroupBox1.TabStop = false;
            resources.ApplyResources(this.lblDescription, "lblDescription");
            this.lblDescription.Name = "lblDescription";
            resources.ApplyResources(this.lblDefaultKeyString, "lblDefaultKeyString");
            this.lblDefaultKeyString.BackColor = Color.Transparent;
            this.lblDefaultKeyString.ForeColor = Color.MidnightBlue;
            this.lblDefaultKeyString.Name = "lblDefaultKeyString";
            this.panel1.Controls.Add(this.btnDefault);
            this.panel1.Controls.Add(this.btnApply);
            this.panel1.Controls.Add(this.btnReset);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            this.panel2.Controls.Add(this.uiGroupBox1);
            resources.ApplyResources(this.panel2, "panel2");
            this.panel2.Name = "panel2";
            resources.ApplyResources(this, "$this");
            base.AutoScaleMode = AutoScaleMode.Font;
            base.Controls.Add(this.gridEX1);
            base.Controls.Add(this.panel2);
            base.Controls.Add(this.panel1);
            base.Name = "KeyboardEditor";
            ((ISupportInitialize) this.gridEX1).EndInit();
            this.uiGroupBox1.ResumeLayout(false);
            this.uiGroupBox1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            base.ResumeLayout(false);
        }

        private bool IsValidKeyInfo(KeyInfo keyInfo, out KeyboardEditorRow assignedRow)
        {
            if (keyInfo.Equals(KeyInfo.None))
            {
                assignedRow = null;
                return true;
            }
            KeyboardEditorRow currentRow = this.SelectedRow;
            assignedRow = null;
            if (currentRow == null)
            {
                return false;
            }
            foreach (KeyboardEditorRow row in this.List.Where<KeyboardEditorRow>(r => r != currentRow))
            {
                if (row.KeyInfo.Equals(keyInfo))
                {
                    assignedRow = row;
                    return false;
                }
            }
            return true;
        }

        private void Refetch()
        {
            this.Grid.Refetch();
            this.RefreshButtons();
        }

        private void RefreshButtons()
        {
            if (this.refreshButtons.HasValue)
            {
                if (!this.refreshButtons.Value)
                {
                    return;
                }
                this.refreshButtons = false;
            }
            KeyboardEditorRow row = this.SelectedRow;
            if (row == null)
            {
                this.btnApply.Enabled = this.btnDefault.Enabled = this.btnReset.Enabled = false;
            }
            else
            {
                KeyInfo currentKeyInfo = this.GetKeyInfo();
                KeyInfo defaultKeyInfo = this.GetSelectedDefaultKeyInfo();
                this.btnDefault.Enabled = !currentKeyInfo.Equals(defaultKeyInfo);
                this.btnReset.Enabled = this.btnApply.Enabled = !currentKeyInfo.Equals(row.KeyInfo);
            }
        }

        private void RefreshDataSource()
        {
            this.Grid.DataSource = this.List;
        }

        private void RefreshDefaultKeyString()
        {
            KeyInfo defaultKeyInfo = this.GetSelectedDefaultKeyInfo();
            this.lblDefaultKeyString.Text = defaultKeyInfo.KeyString;
        }

        private void ResetKeyInfo()
        {
            KeyboardEditorRow row = this.SelectedRow;
            if (row != null)
            {
                this.SetKeyInfo(row.KeyInfo);
                this.ApplyKeyInfo();
                this.Refetch();
            }
        }

        private void SetDefaultKeyInfo()
        {
            KeyInfo defaultKeyInfo = this.GetSelectedDefaultKeyInfo();
            this.SetKeyInfo(defaultKeyInfo);
            this.ApplyKeyInfo();
            this.Refetch();
        }

        private void SetKeyDataSource()
        {
            this.comboKey.DataSource = KeyInfo.GetAvailableKeys();
        }

        private void SetKeyInfo(KeyInfo keyInfo)
        {
            this.checkAlt.Checked = keyInfo.Alt;
            this.checkControl.Checked = keyInfo.Control;
            this.checkShift.Checked = keyInfo.Shift;
            this.checkWin.Checked = keyInfo.Win;
            this.SetKeyDataSource();
            if (keyInfo.Key == Key.None)
            {
                this.comboKey.SelectedItem = Strings.KeyNone;
            }
            else
            {
                this.comboKey.SelectedItem = keyInfo.Key.ToString();
            }
        }

        private void SetSelectedKeyInfo()
        {
            KeyboardEditorRow row = this.SelectedRow;
            if (row == null)
            {
                this.SetKeyInfo(KeyInfo.None);
            }
            else
            {
                this.uiGroupBox1.Text = row.Text;
                this.lblDescription.Text = row.Description;
                KeyInfo selectedDefaultKeyInfo = this.GetSelectedDefaultKeyInfo();
                this.lblDefaultKeyString.Text = string.Format("{0}: {1}", Strings.KeyBoardEditorDefaultLabel, selectedDefaultKeyInfo.KeyString);
                this.SetKeyInfo(row.KeyInfo);
            }
        }

        [Browsable(false)]
        private Janus.Windows.GridEX.GridEX Grid
        {
            [DebuggerNonUserCode]
            get
            {
                return this.gridEX1;
            }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IEnumerable<KeyboardEditorRow> List
        {
            [DebuggerNonUserCode]
            get
            {
                return this.list;
            }
            set
            {
                this.list = value;
                this.RefreshDataSource();
            }
        }

        [Browsable(false)]
        private KeyboardEditorRow SelectedRow
        {
            get
            {
                if (this.Grid.SelectedItems.Count == 0)
                {
                    return null;
                }
                return (KeyboardEditorRow) this.Grid.SelectedItems[0].GetRow().DataRow;
            }
        }
    }
}

