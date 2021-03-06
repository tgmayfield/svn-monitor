﻿using System;
using System.Collections.Generic;
using System.Windows.Forms;

using SVNMonitor.Actions;
using SVNMonitor.Extensions;
using SVNMonitor.Resources;
using SVNMonitor.Resources.Text;
using SVNMonitor.View.Dialogs;

namespace SVNMonitor.Wizards
{
	[ResourceProvider("WizardCodeReviewByMail")]
	internal class CodeReviewMailerWizard : Wizard
	{
		protected override IEnumerable<Actions.Action> CreateActions(string baseName)
		{
			MailSenderAction action = new MailSenderAction
			{
				Subject = Strings.WizardCodeReviewByMailActionSubject_FORMAT.FormatWith(new object[]
				{
					baseName
				})
			};
			ActionPropertiesDialog tempLocal2 = new ActionPropertiesDialog
			{
				Action = action
			};
			ActionPropertiesDialog dialog = tempLocal2;
			if (dialog.ShowDialog() == DialogResult.Cancel)
			{
				throw new WizardCancelledException();
			}
			return new Actions.Action[]
			{
				action
			};
		}

		protected override string GetWizardName(string baseName)
		{
			return Strings.WizardCodeReviewByMailName_FORMAT.FormatWith(new object[]
			{
				baseName
			});
		}
	}
}