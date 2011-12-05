using System;
using System.Collections.Generic;
using System.Windows.Forms;

using SVNMonitor.Actions;
using SVNMonitor.Extensions;
using SVNMonitor.Resources;
using SVNMonitor.Resources.Text;

namespace SVNMonitor.Wizards
{
	[ResourceProvider("WizardRepositoryMirror")]
	internal class RepositoryMirrorWizard : Wizard
	{
		protected override IEnumerable<Actions.Action> CreateActions(string baseName)
		{
			SVNUpdateAction action = new SVNUpdateAction();
			FolderBrowserDialog folderDialog = new FolderBrowserDialog();
			if (folderDialog.ShowDialog() == DialogResult.Cancel)
			{
				throw new WizardCancelledException();
			}
			action.Path = folderDialog.SelectedPath;
			return new Actions.Action[]
			{
				action
			};
		}

		protected override string GetWizardName(string baseName)
		{
			return Strings.WizardRepositoryMirrorName_FORMAT.FormatWith(new object[]
			{
				baseName
			});
		}
	}
}