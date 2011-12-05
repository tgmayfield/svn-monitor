using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using Janus.Windows.ExplorerBar;

using SVNMonitor.Entities;
using SVNMonitor.Extensions;
using SVNMonitor.Resources;
using SVNMonitor.Resources.Text;
using SVNMonitor.Settings;

namespace SVNMonitor.View.Controls
{
	public class SourceExplorerBarGroup : UserEntityExplorerBarGroup<SVNMonitor.Entities.Source>
	{
		private ExplorerBarItem changesItem;
		private ExplorerBarItem conflictsItem;
		private ExplorerBarItem lastCheckItem;
		private ExplorerBarItem pathItem;
		private ExplorerBarItem syncdItem;
		private ExplorerBarItem unversionedItem;
		private ExplorerBarItem updatesItem;
		private ExplorerBarItem urlItem;

		public SourceExplorerBarGroup(SVNMonitor.Entities.Source source)
			: base(source)
		{
		}

		protected override void CreateSubItems()
		{
			base.CreateSubItems();
			pathItem = new ExplorerBarItem();
			pathItem.Key = "Path";
			pathItem.ItemType = ItemType.Description;
			urlItem = new ExplorerBarItem();
			urlItem.Key = "URL";
			urlItem.ItemType = ItemType.Description;
			urlItem.StateStyles.FormatStyle.ForeColor = Color.SteelBlue;
			urlItem.Visible = false;
			lastCheckItem = new ExplorerBarItem();
			lastCheckItem.Key = "LastCheck";
			lastCheckItem.Visible = ApplicationSettingsManager.Settings.SourcesPanelShowLastCheck;
			lastCheckItem.ItemType = ItemType.Description;
			lastCheckItem.StateStyles.FormatStyle.ForeColor = Color.DarkGray;
			syncdItem = new ExplorerBarItem();
			syncdItem.Key = "Syncd";
			syncdItem.Visible = false;
			syncdItem.ItemType = ItemType.LinkButton;
			syncdItem.Cursor = Cursors.Hand;
			syncdItem.StateStyles.FormatStyle.ForeColor = Color.DarkGray;
			syncdItem.StateStyles.FormatStyle.FontItalic = TriState.True;
			updatesItem = new ExplorerBarItem();
			updatesItem.Key = "Updates";
			updatesItem.ItemType = ItemType.LinkButton;
			updatesItem.Cursor = Cursors.Hand;
			updatesItem.Image = Images.arrow_down_green;
			updatesItem.Visible = false;
			changesItem = new ExplorerBarItem();
			changesItem.Key = "Changes";
			changesItem.ItemType = ItemType.LinkButton;
			changesItem.Cursor = Cursors.Hand;
			changesItem.Image = Images.arrow_up_blue;
			changesItem.Visible = false;
			unversionedItem = new ExplorerBarItem();
			unversionedItem.Key = "Unversioned";
			unversionedItem.ItemType = ItemType.LinkButton;
			unversionedItem.Cursor = Cursors.Hand;
			unversionedItem.Image = Images.unversioned;
			unversionedItem.Visible = false;
			conflictsItem = new ExplorerBarItem();
			conflictsItem.Key = "Conflicts";
			conflictsItem.ItemType = ItemType.LinkButton;
			conflictsItem.Cursor = Cursors.Hand;
			conflictsItem.Image = Images.warning;
			conflictsItem.Visible = false;
			base.Items.AddRange(new[]
			{
				pathItem, urlItem, lastCheckItem, syncdItem, base.ErrorItem, updatesItem, changesItem, unversionedItem, conflictsItem
			});
		}

		public override void RefreshEntity()
		{
			if (MainForm.FormInstance.InvokeRequired)
			{
				MainForm.FormInstance.BeginInvoke(new MethodInvoker(RefreshEntity));
			}
			else
			{
				base.RefreshEntity();
				SetPath();
				SetLastCheck();
				bool hasStatus = false;
				hasStatus |= base.SetError();
				hasStatus |= SetUpdates();
				hasStatus |= SetModified();
				hasStatus |= SetUnversioned();
				hasStatus |= SetConflicts();
				SetSyncd(!hasStatus);
			}
		}

		private bool SetConflicts()
		{
			int possibleConflictedCount = Source.PossibleConflictedFilePathsCount;
			if (!Source.Enabled || (possibleConflictedCount <= 0))
			{
				conflictsItem.Text = string.Empty;
				conflictsItem.ToolTipText = string.Empty;
				conflictsItem.Visible = false;
			}
			else
			{
				string conflicted = Strings.SourcesExplorerBar_PossibleConflicted_FORMAT.FormatWith(new object[]
				{
					possibleConflictedCount, (possibleConflictedCount == 1) ? Strings.SourcesExplorerBar_PossibleConflictedItem : Strings.SourcesExplorerBar_PossibleConflictedItems
				});
				StringBuilder sb = new StringBuilder();
				sb.AppendLine(conflicted + ":");
				sb.AppendLine();
				int count = 0;
				foreach (string filePath in Source.GetLog(false).PossibleConflictedFilePaths)
				{
					if (count > 10)
					{
						sb.AppendLine(Strings.SourcesExplorerBar_PossibleConflictedAndMore);
						break;
					}
					sb.AppendLine(filePath);
					count++;
				}
				conflictsItem.Text = conflicted;
				conflictsItem.ToolTipText = string.Format("{0}{1}({2})", sb, Environment.NewLine, Strings.SourcesExplorerBar_PossibleConflictedClickToViewDiff);
				conflictsItem.Visible = true;
			}
			return conflictsItem.Visible;
		}

		private void SetLastCheck()
		{
			if (!Source.Enabled)
			{
				lastCheckItem.Text = string.Empty;
				lastCheckItem.ToolTipText = string.Empty;
				lastCheckItem.Visible = false;
			}
			else if (Source.LastCheck.HasValue)
			{
				lastCheckItem.Text = string.Format("{0} {1}", Strings.SourcesExplorerBar_LastCheckedText, Source.LastCheck);
				lastCheckItem.ToolTipText = string.Format("{0} {1}", Strings.SourcesExplorerBar_LastCheckedTipText, Source.LastCheck);
				string revisionText = string.Empty;
				string revisionTipText = string.Empty;
				if (Source.IsURL)
				{
					revisionText = string.Format(", {0} {1}", Strings.SourcesExplorerBar_Revision, Source.Revision);
					revisionTipText = string.Format("{0}{1} {2}", Environment.NewLine, Strings.SourcesExplorerBar_Revision, Source.Revision);
				}
				else
				{
					SVNInfo info = Source.GetInfo(false);
					if (info != null)
					{
						revisionText = string.Format(", {0} {1}/{2}", Strings.SourcesExplorerBar_Revision, info.Revision, Source.Revision);
						revisionTipText = string.Format("{0}{1} {2}{0}{3} {4}", new object[]
						{
							Environment.NewLine, Strings.SourcesExplorerBar_RevisionLocal, info.Revision, Strings.SourcesExplorerBar_RevisionRepository, Source.Revision
						});
					}
				}
				lastCheckItem.Text = lastCheckItem.Text + revisionText;
				lastCheckItem.ToolTipText = lastCheckItem.ToolTipText + revisionTipText;
			}
			else
			{
				lastCheckItem.Text = Strings.SourcesExplorerBar_NotYetCheckedText;
				lastCheckItem.ToolTipText = Strings.SourcesExplorerBar_NotYetCheckedTipText;
			}
			lastCheckItem.Visible = ApplicationSettingsManager.Settings.SourcesPanelShowLastCheck;
		}

		private bool SetModified()
		{
			int modifiedFilesCount = Source.ModifiedCount;
			if (!Source.Enabled || (modifiedFilesCount <= 0))
			{
				changesItem.Text = string.Empty;
				changesItem.ToolTipText = string.Empty;
				changesItem.Visible = false;
			}
			else
			{
				string modified = Strings.ModifiedItems_FORMAT.FormatWith(new object[]
				{
					modifiedFilesCount, (modifiedFilesCount == 1) ? Strings.ModifiedItems_Item : Strings.ModifiedItems_Items
				});
				StringBuilder sb = new StringBuilder();
				sb.AppendLine(modified + ":");
				sb.AppendLine();
				int count = 0;
				foreach (SVNStatusEntry entry in Source.LocalStatus.ModifiedEntries)
				{
					if (count > 10)
					{
						sb.AppendLine(Strings.AndMore);
						break;
					}
					sb.Append(entry.Path);
					sb.AppendLine();
					count++;
				}
				changesItem.Text = modified;
				changesItem.ToolTipText = string.Format("{0}{1}({2})", sb, Environment.NewLine, Strings.ClickToOpenCommitDialog);
				changesItem.Visible = true;
			}
			return changesItem.Visible;
		}

		protected override void SetNameAndImage()
		{
			base.Text = Source.Name;
			if (Source.Enabled)
			{
				if (Source.IsURL)
				{
					if (Source.Updating)
					{
						base.Image = Images.repo_updating;
					}
					else
					{
						base.Image = Images.repo;
					}
				}
				else if (Source.Updating)
				{
					base.Image = Images.wc_updating;
				}
				else
				{
					base.Image = Images.wc;
				}
			}
			else
			{
				base.Text = base.Text + string.Format(" ({0})", Strings.DisabledEntity);
				if (Source.IsURL)
				{
					base.Image = Images.repo_disabled;
				}
				else
				{
					base.Image = Images.wc_disabled;
				}
			}
		}

		private void SetPath()
		{
			pathItem.Text = Source.Path;
			pathItem.ToolTipText = pathItem.Text;
			pathItem.Visible = ApplicationSettingsManager.Settings.SourcesPanelShowPath;
			if (Source.IsURL)
			{
				urlItem.Visible = false;
			}
			else
			{
				SVNInfo info = Source.GetInfo(false);
				if (info != null)
				{
					urlItem.Text = info.URL;
					urlItem.ToolTipText = urlItem.Text;
					urlItem.Visible = ApplicationSettingsManager.Settings.SourcesPanelShowUrl;
				}
				else
				{
					urlItem.Visible = false;
				}
			}
		}

		protected override void SetProperties()
		{
			base.Expandable = false;
		}

		private void SetSyncd(bool syncd)
		{
			if (Source.Enabled && syncd)
			{
				if (!Source.IsURL)
				{
					syncdItem.Text = Strings.SourcesExplorerBar_NoUpdatesNoChanges;
					syncdItem.ItemType = ItemType.LinkButton;
					syncdItem.ToolTipText = string.Format("{0}{1}({2})", syncdItem.Text, Environment.NewLine, Strings.SourcesExplorerBar_NoUpdatesNoChangesClickToCheckModifications);
					syncdItem.Visible = ApplicationSettingsManager.Settings.SourcesPanelShowNoUpdates;
				}
				else
				{
					syncdItem.Visible = false;
				}
			}
			else
			{
				syncdItem.Text = string.Empty;
				syncdItem.ToolTipText = string.Empty;
				syncdItem.Visible = false;
			}
		}

		private bool SetUnversioned()
		{
			int unversionedCount = Source.UnversionedCount;
			if (!Source.Enabled || (unversionedCount <= 0))
			{
				unversionedItem.Text = string.Empty;
				unversionedItem.ToolTipText = string.Empty;
				unversionedItem.Visible = false;
			}
			else
			{
				string unversioned = Strings.UnversionedItems_FORMAT.FormatWith(new object[]
				{
					unversionedCount, (unversionedCount == 1) ? Strings.UnversionedItems_Item : Strings.UnversionedItems_Items
				});
				StringBuilder sb = new StringBuilder();
				sb.AppendLine(unversioned + ":");
				sb.AppendLine();
				int count = 0;
				foreach (SVNStatusEntry entry in Source.LocalStatus.UnversionedEntries)
				{
					if (count > 10)
					{
						sb.AppendLine(Strings.AndMore);
						break;
					}
					sb.Append(entry.Path);
					sb.AppendLine();
					count++;
				}
				unversionedItem.Text = unversioned;
				unversionedItem.ToolTipText = string.Format("{0}{1}({2})", sb, Environment.NewLine, Strings.ClickToAdd);
				unversionedItem.Visible = true;
			}
			return unversionedItem.Visible;
		}

		private bool SetUpdates()
		{
			int updatesCount = Source.UnreadLogEntriesCount;
			int updatesFileCount = Source.UnreadPathsCount;
			int recommendedCount = Source.UnreadRecommendedCount;
			string updates = string.Empty;
			if (Source.Enabled && (updatesCount > 0))
			{
				updates = Strings.AvailableUpdates_FORMAT.FormatWith(new object[]
				{
					updatesCount.ToString() + ((updatesCount >= ApplicationSettingsManager.Settings.LogEntriesPageSize) ? "+" : string.Empty), (updatesCount == 1) ? Strings.AvailableUpdates_Update : Strings.AvailableUpdates_Updates, updatesFileCount, (updatesFileCount == 1) ? Strings.AvailableUpdates_Item : Strings.AvailableUpdates_Items
				});
				updatesItem.Text = updates;
				updatesItem.ToolTipText = string.Format("{0}{1}({2})", updatesItem.Text, Environment.NewLine, Strings.ClickToUpdateHEAD);
				updatesItem.Visible = true;
				if (recommendedCount > 0)
				{
					updatesItem.Image = Images.recommend_down;
				}
				else
				{
					updatesItem.Image = Images.arrow_down_green;
				}
			}
			else
			{
				updatesItem.Text = string.Empty;
				updatesItem.ToolTipText = string.Empty;
				updatesItem.Visible = false;
			}
			return updatesItem.Visible;
		}

		public SVNMonitor.Entities.Source Source
		{
			get { return base.Entity; }
		}
	}
}