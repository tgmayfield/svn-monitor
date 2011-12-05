using System;

namespace SVNMonitor.Settings.Validation
{
	internal class KeyboardShortcutConfigValidatorAttribute : ConfigValidatorAttribute
	{
		public KeyboardShortcutConfigValidatorAttribute()
			: base(typeof(KeyboardShortcutConfigValidator), new object[0])
		{
		}
	}
}