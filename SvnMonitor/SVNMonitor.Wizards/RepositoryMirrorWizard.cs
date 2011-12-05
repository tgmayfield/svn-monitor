using SVNMonitor.Resources;
using System.Collections.Generic;
using System;
using SVNMonitor.Actions;
using System.Windows.Forms;
using SVNMonitor.Resources.Text;

namespace SVNMonitor.Wizards
{
[ResourceProvider("WizardRepositoryMirror")]
internal class RepositoryMirrorWizard : Wizard
{
	public RepositoryMirrorWizard()
	{
	}

	protected override IEnumerable<Action> CreateActions(string baseName)
	{
		SVNUpdateAction action = new SVNUpdateAction();
		FolderBrowserDialog folderDialog = new FolderBrowserDialog();
		DialogResult folderDialogResult = folderDialog.ShowDialog();
		if (folderDialogResult == DialogResult.Cancel)
		{
			throw new WizardCancelledException();
		}
		action.Path = folderDialog.SelectedPath;
		Action[] actionArray = new Action[1];
		actionArray[0] = action;
		return actionArray;
	}

	protected override string GetWizardName(string baseName)
	{
		object[] objArray;
		return Strings.WizardRepositoryMirrorName_FORMAT.FormatWith(new object[] { baseName });
	}
}
}