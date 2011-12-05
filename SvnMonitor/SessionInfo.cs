﻿namespace SVNMonitor
{
    using SVNMonitor.Extensions;
    using SVNMonitor.Support;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Text;

    public class SessionInfo
    {
        private static string[] commandLineArguments;
        private static Dictionary<string, PropertyInfo> sessionProperties;

        private static Dictionary<string, string> ExtractArguments(string[] args)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            if (args != null)
            {
                foreach (string arg in args)
                {
                    if ((arg.StartsWith("/") || arg.StartsWith("-")) && arg.Contains("="))
                    {
                        int eqIndex = arg.IndexOf("=");
                        string key = arg.Substring(1, eqIndex - 1);
                        string value = arg.Substring(eqIndex + 1);
                        dict.Add(key.ToLower(), value);
                    }
                }
            }
            return dict;
        }

        private static Dictionary<string, PropertyInfo> GetSessionProperties()
        {
            Dictionary<string, PropertyInfo> dict = new Dictionary<string, PropertyInfo>();
            foreach (PropertyInfo prop in typeof(SessionInfo).GetProperties(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static).Where<PropertyInfo>(p => Attribute.IsDefined(p, typeof(ParameterAttribute))))
            {
                ParameterAttribute[] atts = (ParameterAttribute[]) Attribute.GetCustomAttributes(prop, typeof(ParameterAttribute));
                foreach (ParameterAttribute att in atts)
                {
                    dict.Add(att.ArgName.ToLower(), prop);
                }
            }
            return dict;
        }

        internal static string GetUsageInformation()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("SessionInfo_UserAppData={0}{1}", UserAppData, UsageInformationSender.Separator);
            sb.AppendFormat("SessionInfo_CommandLineArguments={0}{1}", CommandLineArguments, UsageInformationSender.Separator);
            return sb.ToString();
        }

        public static void SetSessionInfo(string[] args)
        {
            commandLineArguments = args;
            if ((args != null) && (args.Length != 0))
            {
                Dictionary<string, string> values = ExtractArguments(args);
                foreach (string key in values.Keys)
                {
                    if (SessionProperties.ContainsKey(key))
                    {
                        PropertyInfo prop = SessionProperties[key];
                        string stringValue = values[key];
                        object value = Convert.ChangeType(stringValue, prop.PropertyType);
                        prop.SetValue(null, value, null);
                    }
                }
            }
        }

        [Conditional("DEBUG")]
        internal static void SetUpgradeTest(string version, string upgradeFile)
        {
            TestNewVersion = version;
            TestNewVersionFile = upgradeFile;
        }

        public static string CommandLineArguments
        {
            get
            {
                return string.Join(" ", commandLineArguments);
            }
        }

        internal static IEnumerable<PropertyInfo> Properties
        {
            get
            {
                return SessionProperties.Values.Distinct<PropertyInfo>();
            }
        }

        private static Dictionary<string, PropertyInfo> SessionProperties
        {
            get
            {
                if (sessionProperties == null)
                {
                    sessionProperties = GetSessionProperties();
                }
                return sessionProperties;
            }
        }

        [Parameter("test")]
        public static bool Test
        {
            [CompilerGenerated]
            get
            {
                return <Test>k__BackingField;
            }
            [CompilerGenerated]
            private set
            {
                <Test>k__BackingField = value;
            }
        }

        [Parameter("testnewversion")]
        public static string TestNewVersion
        {
            [CompilerGenerated]
            get
            {
                return <TestNewVersion>k__BackingField;
            }
            [CompilerGenerated]
            private set
            {
                <TestNewVersion>k__BackingField = value;
            }
        }

        [Parameter("testnewversionfile")]
        public static string TestNewVersionFile
        {
            [CompilerGenerated]
            get
            {
                return <TestNewVersionFile>k__BackingField;
            }
            [CompilerGenerated]
            private set
            {
                <TestNewVersionFile>k__BackingField = value;
            }
        }

        [Parameter("testupgrader")]
        public static bool TestUpgrader
        {
            [CompilerGenerated]
            get
            {
                return <TestUpgrader>k__BackingField;
            }
            [CompilerGenerated]
            private set
            {
                <TestUpgrader>k__BackingField = value;
            }
        }

        [Parameter("upgradedfrom"), Parameter("uf")]
        public static string UpgradedFrom
        {
            [CompilerGenerated]
            get
            {
                return <UpgradedFrom>k__BackingField;
            }
            [CompilerGenerated]
            private set
            {
                <UpgradedFrom>k__BackingField = value;
            }
        }

        [Parameter("data"), Parameter("d"), Parameter("userdata")]
        public static string UserAppData
        {
            [CompilerGenerated]
            get
            {
                return <UserAppData>k__BackingField;
            }
            [CompilerGenerated]
            private set
            {
                <UserAppData>k__BackingField = value;
            }
        }
    }
}

