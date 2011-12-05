using Janus.Windows.ExplorerBar;
using SVNMonitor.Entities;
using System;
using System.Drawing;
using SVNMonitor.Settings;
using System.Windows.Forms;
using SVNMonitor.Resources;
using SVNMonitor.View;
using SVNMonitor.Resources.Text;
using System.Text;

namespace SVNMonitor.View.Controls
{
public class SourceExplorerBarGroup : UserEntityExplorerBarGroup<Source>
{
	private ExplorerBarItem changesItem;

	private ExplorerBarItem conflictsItem;

	private ExplorerBarItem lastCheckItem;

	private ExplorerBarItem pathItem;

	private ExplorerBarItem syncdItem;

	private ExplorerBarItem unversionedItem;

	private ExplorerBarItem updatesItem;

	private ExplorerBarItem urlItem;

	public Source Source
	{
		get
		{
			return base.Entity;
		}
	}

	public SourceExplorerBarGroup(Source source)
	{
	}

	protected override void CreateSubItems()
	{
		ExplorerBarItem[] explorerBarItemArray;
		base.CreateSubItems();
		this.pathItem = new ExplorerBarItem();
		this.pathItem.Key = "Path";
		this.pathItem.ItemType = ItemType.Description;
		this.urlItem = new ExplorerBarItem();
		this.urlItem.Key = "URL";
		this.urlItem.ItemType = ItemType.Description;
		this.urlItem.StateStyles.FormatStyle.ForeColor = Color.SteelBlue;
		this.urlItem.Visible = false;
		this.lastCheckItem = new ExplorerBarItem();
		this.lastCheckItem.Key = "LastCheck";
		this.lastCheckItem.Visible = ApplicationSettingsManager.Settings.SourcesPanelShowLastCheck;
		this.lastCheckItem.ItemType = ItemType.Description;
		this.lastCheckItem.StateStyles.FormatStyle.ForeColor = Color.DarkGray;
		this.syncdItem = new ExplorerBarItem();
		this.syncdItem.Key = "Syncd";
		this.syncdItem.Visible = false;
		this.syncdItem.ItemType = ItemType.LinkButton;
		this.syncdItem.Cursor = Cursors.Hand;
		this.syncdItem.StateStyles.FormatStyle.ForeColor = Color.DarkGray;
		this.syncdItem.StateStyles.FormatStyle.FontItalic = TriState.True;
		this.updatesItem = new ExplorerBarItem();
		this.updatesItem.Key = "Updates";
		this.updatesItem.ItemType = ItemType.LinkButton;
		this.updatesItem.Cursor = Cursors.Hand;
		this.updatesItem.Image = Images.arrow_down_green;
		this.updatesItem.Visible = false;
		this.changesItem = new ExplorerBarItem();
		this.changesItem.Key = "Changes";
		this.changesItem.ItemType = ItemType.LinkButton;
		this.changesItem.Cursor = Cursors.Hand;
		this.changesItem.Image = Images.arrow_up_blue;
		this.changesItem.Visible = false;
		this.unversionedItem = new ExplorerBarItem();
		this.unversionedItem.Key = "Unversioned";
		this.unversionedItem.ItemType = ItemType.LinkButton;
		this.unversionedItem.Cursor = Cursors.Hand;
		this.unversionedItem.Image = Images.unversioned;
		this.unversionedItem.Visible = false;
		this.conflictsItem = new ExplorerBarItem();
		this.conflictsItem.Key = "Conflicts";
		this.conflictsItem.ItemType = ItemType.LinkButton;
		this.conflictsItem.Cursor = Cursors.Hand;
		this.conflictsItem.Image = Images.warning;
		this.conflictsItem.Visible = false;
		base.Items.AddRange(new ExplorerBarItem[] { this.pathItem, this.urlItem, this.lastCheckItem, this.syncdItem, base.ErrorItem, this.updatesItem, this.changesItem, this.unversionedItem, this.conflictsItem });
	}

	public override void RefreshEntity()
	{
		if (MainForm.FormInstance.InvokeRequired)
		{
			base.BeginInvoke(new MethodInvoker(this.RefreshEntity));
			return;
		}
		base.RefreshEntity();
		this.SetPath();
		this.SetLastCheck();
		bool hasStatus = false;
		hasStatus = hasStatus | base.SetError();
		hasStatus = hasStatus | this.SetUpdates();
		hasStatus = hasStatus | this.SetModified();
		hasStatus = hasStatus | this.SetUnversioned();
		hasStatus = hasStatus | this.SetConflicts();
		this.SetSyncd(hasStatus == 0);
	}

	private bool SetConflicts()
	{
		object[] objArray;
		int possibleConflictedCount = this.Source.PossibleConflictedFilePathsCount;
		if (this.Source.Enabled && possibleConflictedCount > 0)
		{
			string conflicted = Strings.SourcesExplorerBar_PossibleConflicted_FORMAT.FormatWith(new object[] { possibleConflictedCount, (possibleConflictedCount == 1 ? Strings.SourcesExplorerBar_PossibleConflictedItem : Strings.SourcesExplorerBar_PossibleConflictedItems) });
			StringBuilder sb = new StringBuilder();
			sb.AppendLine(string.Concat(conflicted, ":"));
			sb.AppendLine();
			int count = 0;
			SVNLog log = this.Source.GetLog(false);
			foreach (string filePath in log.PossibleConflictedFilePaths)
			{
				if (count > 10)
				{
					sb.AppendLine(Strings.SourcesExplorerBar_PossibleConflictedAndMore);
					break;
				}
				sb.AppendLine(filePath);
				count++;
			}
			this.conflictsItem.Text = conflicted;
			this.conflictsItem.ToolTipText = string.Format("{0}{1}({2})", sb, Environment.NewLine, Strings.SourcesExplorerBar_PossibleConflictedClickToViewDiff);
			this.conflictsItem.Visible = true;
		}
		return this.conflictsItem.Visible;
	}

	private void SetLastCheck()
	{
		object[] objArray;
		if (!this.Source.Enabled)
		{
			this.lastCheckItem.Text = string.Empty;
			this.lastCheckItem.ToolTipText = string.Empty;
			this.lastCheckItem.Visible = false;
		}
		else
		{
			DateTime? lastCheck = this.Source.LastCheck;
			if (lastCheck.HasValue)
			{
				this.lastCheckItem.Text = string.Format("{0} {1}", Strings.SourcesExplorerBar_LastCheckedText, this.Source.LastCheck);
				this.lastCheckItem.ToolTipText = string.Format("{0} {1}", Strings.SourcesExplorerBar_LastCheckedTipText, this.Source.LastCheck);
				string revisionText = string.Empty;
				string revisionTipText = string.Empty;
				if (this.Source.IsURL)
				{
					revisionText = string.Format(", {0} {1}", Strings.SourcesExplorerBar_Revision, this.Source.Revision);
					revisionTipText = string.Format("{0}{1} {2}", Environment.NewLine, Strings.SourcesExplorerBar_Revision, this.Source.Revision);
				}
				else
				{
					SVNInfo info = this.Source.GetInfo(false);
					if (info != null)
					{
						revisionText = string.Format(", {0} {1}/{2}", Strings.SourcesExplorerBar_Revision, info.Revision, this.Source.Revision);
						revisionTipText = string.Format("{0}{1} {2}{0}{3} {4}", new object[] { Environment.NewLine, Strings.SourcesExplorerBar_RevisionLocal, info.Revision, Strings.SourcesExplorerBar_RevisionRepository, this.Source.Revision });
					}
				}
				this.lastCheckItem.Text = string.Concat(this.lastCheckItem.Text, revisionText);
				this.lastCheckItem.ToolTipText = string.Concat(this.lastCheckItem.ToolTipText, revisionTipText);
			}
			else
			{
				this.lastCheckItem.Text = Strings.SourcesExplorerBar_NotYetCheckedText;
				this.lastCheckItem.ToolTipText = Strings.SourcesExplorerBar_NotYetCheckedTipText;
			}
		}
		this.lastCheckItem.Visible = ApplicationSettingsManager.Settings.SourcesPanelShowLastCheck;
	}

	private bool SetModified()
	{
		object[] objArray;
		int modifiedFilesCount = this.Source.ModifiedCount;
		if (this.Source.Enabled && modifiedFilesCount > 0)
		{
			string modified = Strings.ModifiedItems_FORMAT.FormatWith(new object[] { modifiedFilesCount, (modifiedFilesCount == 1 ? Strings.ModifiedItems_Item : Strings.ModifiedItems_Items) });
			StringBuilder sb = new StringBuilder();
			sb.AppendLine(string.Concat(modified, ":"));
			sb.AppendLine();
			int count = 0;
			foreach (SVNStatusEntry entry in this.Source.LocalStatus.ModifiedEntries)
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
			this.changesItem.Text = modified;
			this.changesItem.ToolTipText = string.Format("{0}{1}({2})", sb, Environment.NewLine, Strings.ClickToOpenCommitDialog);
			this.changesItem.Visible = true;
		}
		return this.changesItem.Visible;
	}

	protected override void SetNameAndImage()
	{
		base.Text = this.Source.Name;
		if (this.Source.Enabled)
		{
			if (this.Source.IsURL)
			{
				if (this.Source.Updating)
				{
					base.Image = Images.repo_updating;
					return;
				}
			}
		}
		base.Image = Images.repo;
		return;
		if (this.Source.Updating)
		{
			base.Image = Images.wc_updating;
			return;
		}
		base.Image = Images.wc;
		return;
		base.Text = string.Concat(this.Text, string.Format(" ({0})", Strings.DisabledEntity));
		if (this.Source.IsURL)
		{
			base.Image = Images.repo_disabled;
			return;
		}
		base.Image = Images.wc_disabled;
	}

	private void SetPath()
	{
		this.pathItem.Text = this.Source.Path;
		this.pathItem.ToolTipText = this.pathItem.Text;
		this.pathItem.Visible = ApplicationSettingsManager.Settings.SourcesPanelShowPath;
		if (this.Source.IsURL)
		{
			this.urlItem.Visible = false;
			return;
		}
		SVNInfo info = this.Source.GetInfo(false);
		if (info != null)
		{
			this.urlItem.Text = info.URL;
			this.urlItem.ToolTipText = this.urlItem.Text;
			this.urlItem.Visible = ApplicationSettingsManager.Settings.SourcesPanelShowUrl;
			return;
		}
		this.urlItem.Visible = false;
	}

	protected override void SetProperties()
	{
		base.Expandable = false;
	}

	private void SetSyncd(bool syncd)
	{
		if (this.Source.Enabled || syncd || !this.Source.IsURL)
		{
			this.syncdItem.Text = Strings.SourcesExplorerBar_NoUpdatesNoChanges;
			this.syncdItem.ItemType = ItemType.LinkButton;
			this.syncdItem.ToolTipText = string.Format("{0}{1}({2})", this.syncdItem.Text, Environment.NewLine, Strings.SourcesExplorerBar_NoUpdatesNoChangesClickToCheckModifications);
			this.syncdItem.Visible = ApplicationSettingsManager.Settings.SourcesPanelShowNoUpdates;
			return;
		}
		this.syncdItem.Visible = false;
		return;
		this.syncdItem.Text = string.Empty;
		this.syncdItem.ToolTipText = string.Empty;
		this.syncdItem.Visible = false;
	}

	private bool SetUnversioned()
	{
		object[] objArray;
		int unversionedCount = this.Source.UnversionedCount;
		if (this.Source.Enabled && unversionedCount > 0)
		{
			string unversioned = Strings.UnversionedItems_FORMAT.FormatWith(new object[] { unversionedCount, (unversionedCount == 1 ? Strings.UnversionedItems_Item : Strings.UnversionedItems_Items) });
			StringBuilder sb = new StringBuilder();
			sb.AppendLine(string.Concat(unversioned, ":"));
			sb.AppendLine();
			int count = 0;
			foreach (SVNStatusEntry entry in this.Source.LocalStatus.UnversionedEntries)
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
			this.unversionedItem.Text = unversioned;
			this.unversionedItem.ToolTipText = string.Format("{0}{1}({2})", sb, Environment.NewLine, Strings.ClickToAdd);
			this.unversionedItem.Visible = true;
		}
		return this.unversionedItem.Visible;
	}

	private bool SetUpdates()
	{
		object[] objArray;
		int updatesCount = this.Source.UnreadLogEntriesCount;
		int updatesFileCount = this.Source.UnreadPathsCount;
		int recommendedCount = this.Source.UnreadRecommendedCount;
		string updates = string.Empty;
		if (this.Source.Enabled && updatesCount > 0)
		{
			updates = Strings.AvailableUpdates_FORMAT.FormatWith(new object[] { string.Concat(updatesCount.ToString(), (updatesCount >= ApplicationSettingsManager.Settings.LogEntriesPageSize ? "+" : string.Empty)), (updatesCount == 1 ? Strings.AvailableUpdates_Update : Strings.AvailableUpdates_Updates), updatesFileCount, (updatesFileCount == 1 ? Strings.AvailableUpdates_Item : Strings.AvailableUpdates_Items) });
			this.updatesItem.Text = updates;
			this.updatesItem.ToolTipText = string.Format("{0}{1}({2})", this.updatesItem.Text, Environment.NewLine, Strings.ClickToUpdateHEAD);
			this.updatesItem.Visible = true;
			if (recommendedCount > 0)
			{
				this.updatesItem.Image = Images.recommend_down;
			}
			else
			{
				this.updatesItem.Image = Images.arrow_down_green;
				this.updatesItem.Text = string.Empty;
				this.updatesItem.ToolTipText = string.Empty;
				this.updatesItem.Visible = false;
			}
		}
		return this.updatesItem.Visible;
	}
}
}