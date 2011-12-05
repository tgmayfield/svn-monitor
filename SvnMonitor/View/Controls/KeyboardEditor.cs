using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

using Janus.Windows.GridEX;

using SVNMonitor.Extensions;
using SVNMonitor.Helpers;
using SVNMonitor.Logging;
using SVNMonitor.Resources.Text;
using SVNMonitor.Settings;

namespace SVNMonitor.View.Controls
{
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
			InitializeComponent();
		}

		private void ApplyKeyInfo()
		{
			KeyboardEditorRow row = SelectedRow;
			if (row != null)
			{
				KeyInfo keyInfo = GetKeyInfo();
				KeyboardEditorRow assignedRow = null;
				if (!IsValidKeyInfo(keyInfo, out assignedRow))
				{
					if (assignedRow != null)
					{
						MainForm.FormInstance.ShowErrorMessage(Strings.ErrorKeyAlreadyAssigned_FORMAT.FormatWith(new object[]
						{
							assignedRow.Text
						}), Strings.SVNMonitorCaption);
					}
				}
				else
				{
					row.KeyInfo = GetKeyInfo();
					Refetch();
				}
			}
		}

		private void btnApply_Click(object sender, EventArgs e)
		{
			Logger.LogUserAction();
			ApplyKeyInfo();
		}

		private void btnDefault_Click(object sender, EventArgs e)
		{
			Logger.LogUserAction();
			refreshButtons = true;
			SetDefaultKeyInfo();
			ApplyKeyInfo();
			refreshButtons = null;
		}

		private void btnReset_Click(object sender, EventArgs e)
		{
			Logger.LogUserAction();
			refreshButtons = true;
			ResetKeyInfo();
			refreshButtons = null;
		}

		private void Check_Changed(object sender, EventArgs e)
		{
			Logger.LogUserAction();
			RefreshButtons();
		}

		private void comboKey_SelectedIndexChanged(object sender, EventArgs e)
		{
			Logger.LogUserAction();
			RefreshButtons();
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		private KeyInfo GetKeyInfo()
		{
			Key key;
			ModifierKey modifier = ModifierKey.None;
			if (checkAlt.Checked)
			{
				modifier |= ModifierKey.None | ModifierKey.Alt;
			}
			if (checkControl.Checked)
			{
				modifier |= ModifierKey.Control;
			}
			if (checkShift.Checked)
			{
				modifier |= ModifierKey.Shift;
			}
			if (checkWin.Checked)
			{
				modifier |= ModifierKey.Win;
			}
			string selectedKey = comboKey.SelectedItem == null ? Strings.KeyNone : comboKey.SelectedItem.ToString();
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
			KeyboardEditorRow row = SelectedRow;
			if (row == null)
			{
				return KeyInfo.None;
			}
			return KeyInfo.GetKeyInfo(ApplicationSettingsManager.GetDefault<string>(row.AssociatedSetting));
		}

		private void gridEX1_FormattingRow(object sender, RowLoadEventArgs e)
		{
			GridEXCell imageCell = e.Row.Cells["Image"];
			KeyboardEditorRow row = (KeyboardEditorRow)e.Row.DataRow;
			imageCell.Image = row.Image;
		}

		private void gridEX1_SelectionChanged(object sender, EventArgs e)
		{
			SetSelectedKeyInfo();
			RefreshButtons();
		}

		private void InitializeComponent()
		{
			GridEXLayout gridEX1_DesignTimeLayout = new GridEXLayout();
			ComponentResourceManager resources = new ComponentResourceManager(typeof(KeyboardEditor));
			gridEX1 = new Janus.Windows.GridEX.GridEX();
			checkWin = new CheckBox();
			comboKey = new ComboBox();
			checkControl = new CheckBox();
			checkShift = new CheckBox();
			checkAlt = new CheckBox();
			btnApply = new Button();
			btnReset = new Button();
			btnDefault = new Button();
			uiGroupBox1 = new GroupBox();
			lblDescription = new Label();
			lblDefaultKeyString = new Label();
			panel1 = new Panel();
			panel2 = new Panel();
			((ISupportInitialize)gridEX1).BeginInit();
			uiGroupBox1.SuspendLayout();
			panel1.SuspendLayout();
			panel2.SuspendLayout();
			base.SuspendLayout();
			gridEX1.AllowColumnDrag = false;
			gridEX1.AllowEdit = InheritableBoolean.False;
			gridEX1.AutomaticSort = false;
			gridEX1.ColumnAutoResize = true;
			gridEX1.ColumnHeaders = InheritableBoolean.False;
			resources.ApplyResources(gridEX1_DesignTimeLayout, "gridEX1_DesignTimeLayout");
			gridEX1.DesignTimeLayout = gridEX1_DesignTimeLayout;
			resources.ApplyResources(gridEX1, "gridEX1");
			gridEX1.EnterKeyBehavior = EnterKeyBehavior.None;
			gridEX1.FocusStyle = FocusStyle.Solid;
			gridEX1.GridLineColor = Color.Silver;
			gridEX1.GridLines = GridLines.Horizontal;
			gridEX1.GroupByBoxVisible = false;
			gridEX1.HideSelection = HideSelection.HighlightInactive;
			gridEX1.Name = "gridEX1";
			gridEX1.TabKeyBehavior = TabKeyBehavior.ControlNavigation;
			gridEX1.FormattingRow += gridEX1_FormattingRow;
			gridEX1.SelectionChanged += gridEX1_SelectionChanged;
			resources.ApplyResources(checkWin, "checkWin");
			checkWin.Name = "checkWin";
			checkWin.CheckedChanged += Check_Changed;
			resources.ApplyResources(comboKey, "comboKey");
			comboKey.DropDownStyle = ComboBoxStyle.DropDownList;
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
			lblDefaultKeyString.BackColor = Color.Transparent;
			lblDefaultKeyString.ForeColor = Color.MidnightBlue;
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
			base.AutoScaleMode = AutoScaleMode.Font;
			base.Controls.Add(gridEX1);
			base.Controls.Add(panel2);
			base.Controls.Add(panel1);
			base.Name = "KeyboardEditor";
			((ISupportInitialize)gridEX1).EndInit();
			uiGroupBox1.ResumeLayout(false);
			uiGroupBox1.PerformLayout();
			panel1.ResumeLayout(false);
			panel2.ResumeLayout(false);
			base.ResumeLayout(false);
		}

		private bool IsValidKeyInfo(KeyInfo keyInfo, out KeyboardEditorRow assignedRow)
		{
			if (keyInfo.Equals(KeyInfo.None))
			{
				assignedRow = null;
				return true;
			}
			KeyboardEditorRow currentRow = SelectedRow;
			assignedRow = null;
			if (currentRow == null)
			{
				return false;
			}
			foreach (KeyboardEditorRow row in List.Where(r => r != currentRow))
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
			Grid.Refetch();
			RefreshButtons();
		}

		private void RefreshButtons()
		{
			if (refreshButtons.HasValue)
			{
				if (!refreshButtons.Value)
				{
					return;
				}
				refreshButtons = false;
			}
			KeyboardEditorRow row = SelectedRow;
			if (row == null)
			{
				btnApply.Enabled = btnDefault.Enabled = btnReset.Enabled = false;
			}
			else
			{
				KeyInfo currentKeyInfo = GetKeyInfo();
				KeyInfo defaultKeyInfo = GetSelectedDefaultKeyInfo();
				btnDefault.Enabled = !currentKeyInfo.Equals(defaultKeyInfo);
				btnReset.Enabled = btnApply.Enabled = !currentKeyInfo.Equals(row.KeyInfo);
			}
		}

		private void RefreshDataSource()
		{
			Grid.DataSource = List;
		}

		private void RefreshDefaultKeyString()
		{
			KeyInfo defaultKeyInfo = GetSelectedDefaultKeyInfo();
			lblDefaultKeyString.Text = defaultKeyInfo.KeyString;
		}

		private void ResetKeyInfo()
		{
			KeyboardEditorRow row = SelectedRow;
			if (row != null)
			{
				SetKeyInfo(row.KeyInfo);
				ApplyKeyInfo();
				Refetch();
			}
		}

		private void SetDefaultKeyInfo()
		{
			KeyInfo defaultKeyInfo = GetSelectedDefaultKeyInfo();
			SetKeyInfo(defaultKeyInfo);
			ApplyKeyInfo();
			Refetch();
		}

		private void SetKeyDataSource()
		{
			comboKey.DataSource = KeyInfo.GetAvailableKeys();
		}

		private void SetKeyInfo(KeyInfo keyInfo)
		{
			checkAlt.Checked = keyInfo.Alt;
			checkControl.Checked = keyInfo.Control;
			checkShift.Checked = keyInfo.Shift;
			checkWin.Checked = keyInfo.Win;
			SetKeyDataSource();
			if (keyInfo.Key == Key.None)
			{
				comboKey.SelectedItem = Strings.KeyNone;
			}
			else
			{
				comboKey.SelectedItem = keyInfo.Key.ToString();
			}
		}

		private void SetSelectedKeyInfo()
		{
			KeyboardEditorRow row = SelectedRow;
			if (row == null)
			{
				SetKeyInfo(KeyInfo.None);
			}
			else
			{
				uiGroupBox1.Text = row.Text;
				lblDescription.Text = row.Description;
				KeyInfo selectedDefaultKeyInfo = GetSelectedDefaultKeyInfo();
				lblDefaultKeyString.Text = string.Format("{0}: {1}", Strings.KeyBoardEditorDefaultLabel, selectedDefaultKeyInfo.KeyString);
				SetKeyInfo(row.KeyInfo);
			}
		}

		[Browsable(false)]
		private Janus.Windows.GridEX.GridEX Grid
		{
			[DebuggerNonUserCode]
			get { return gridEX1; }
		}

		[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public IEnumerable<KeyboardEditorRow> List
		{
			[DebuggerNonUserCode]
			get { return list; }
			set
			{
				list = value;
				RefreshDataSource();
			}
		}

		[Browsable(false)]
		private KeyboardEditorRow SelectedRow
		{
			get
			{
				if (Grid.SelectedItems.Count == 0)
				{
					return null;
				}
				return (KeyboardEditorRow)Grid.SelectedItems[0].GetRow().DataRow;
			}
		}
	}
}