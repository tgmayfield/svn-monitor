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
	public partial class KeyboardEditor : UserControl
	{
		private IEnumerable<KeyboardEditorRow> list;
		private bool? refreshButtons;
		
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
