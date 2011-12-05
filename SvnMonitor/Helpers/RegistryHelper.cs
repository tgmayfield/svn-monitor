﻿namespace SVNMonitor.Helpers
{
    using Microsoft.Win32;
    using System;

    internal class RegistryHelper
    {
        public static bool Exists(RegistryKey parent, string keyName)
        {
            using (RegistryKey key = parent.OpenSubKey(keyName, false))
            {
                return (key != null);
            }
        }

        public static bool Exists(RegistryKey parent, string keyName, string valueName)
        {
            using (RegistryKey key = parent.OpenSubKey(keyName, false))
            {
                if (key == null)
                {
                    return false;
                }
                return (key.GetValue(valueName) != null);
            }
        }

        public static string GetStringValue(string keyName)
        {
            return (string) GetValue(keyName);
        }

        public static string GetStringValue(RegistryKey key, string valueName)
        {
            return (string) GetValue(key, valueName);
        }

        public static string GetStringValue(string keyName, string valueName)
        {
            return (string) GetValue(keyName, valueName);
        }

        public static object GetValue(string keyName)
        {
            return Registry.GetValue(keyName, string.Empty, string.Empty);
        }

        public static object GetValue(RegistryKey key, string valueName)
        {
            return key.GetValue(valueName);
        }

        public static object GetValue(string keyName, string valueName)
        {
            return Registry.GetValue(keyName, valueName, null);
        }

        public static IDisposable Isolate(string keyName, string valueName)
        {
            return Isolate(keyName, valueName, string.Empty);
        }

        public static IDisposable Isolate(string keyName, string valueName, object tempValue)
        {
            return new RegistryIsolator(keyName, valueName, tempValue);
        }

        public static RegistryKey OpenSubKey(RegistryKey parent, string name, bool writable)
        {
            return parent.OpenSubKey(name, writable);
        }

        public static void SetValue(string keyName, string valueName, object value)
        {
            Registry.SetValue(keyName, valueName, value);
        }

        private class RegistryIsolator : IDisposable
        {
            private string keyName;
            private bool needDispose;
            private object originalValue;
            private string valueName;

            internal RegistryIsolator(string keyName, string valueName, object tempValue)
            {
                this.keyName = keyName;
                this.valueName = valueName;
                this.originalValue = RegistryHelper.GetValue(keyName, valueName);
                RegistryHelper.SetValue(keyName, valueName, tempValue);
                this.needDispose = true;
            }

            public void Dispose()
            {
                if (this.needDispose)
                {
                    RegistryHelper.SetValue(this.keyName, this.valueName, this.originalValue);
                }
            }
        }
    }
}

