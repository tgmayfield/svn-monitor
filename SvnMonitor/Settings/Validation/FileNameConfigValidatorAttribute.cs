namespace SVNMonitor.Settings.Validation
{
    using System;

    internal class FileNameConfigValidatorAttribute : ConfigValidatorAttribute
    {
        public FileNameConfigValidatorAttribute() : base(typeof(FileNameConfigValidator), new object[0])
        {
        }
    }
}

