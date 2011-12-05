namespace SVNMonitor.Wizards
{
    using SVNMonitor.Actions;
    using SVNMonitor.Extensions;
    using SVNMonitor.Resources;
    using SVNMonitor.Resources.Text;
    using SVNMonitor.View.Dialogs;
    using System;
    using System.Collections.Generic;
    using System.Windows.Forms;

    [ResourceProvider("WizardCodeReviewByMail")]
    internal class CodeReviewMailerWizard : Wizard
    {
        protected override IEnumerable<Action> CreateActions(string baseName)
        {
            MailSenderAction action = new MailSenderAction {
                Subject = Strings.WizardCodeReviewByMailActionSubject_FORMAT.FormatWith(new object[] { baseName })
            };
            ActionPropertiesDialog <>g__initLocal2 = new ActionPropertiesDialog {
                Action = action
            };
            ActionPropertiesDialog dialog = <>g__initLocal2;
            if (dialog.ShowDialog() == DialogResult.Cancel)
            {
                throw new WizardCancelledException();
            }
            return new Action[] { action };
        }

        protected override string GetWizardName(string baseName)
        {
            return Strings.WizardCodeReviewByMailName_FORMAT.FormatWith(new object[] { baseName });
        }
    }
}

