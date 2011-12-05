using System;
using Microsoft.Win32;

namespace SVNMonitor.Helpers
{
internal class RegistryHelper
{
	public RegistryHelper()
	{
	}

	public static bool Exists(RegistryKey parent, string keyName, string valueName)
	{
		RegistryKey key = parent.OpenSubKey(keyName, false);
		using (key)
		{
			if (key == null)
			{
				return false;
			}
			object @value = key.GetValue(valueName);
			return @value != null;
		}
	}

	public static bool Exists(RegistryKey parent, string keyName)
	{
		RegistryKey key = parent.OpenSubKey(keyName, false);
		using (key)
		{
			return key != null;
		}
	}

	public static string GetStringValue(string keyName, string valueName)
	{
		object @value = RegistryHelper.GetValue(keyName, valueName);
		return (string)@value;
	}

	public static string GetStringValue(string keyName)
	{
		object @value = RegistryHelper.GetValue(keyName);
		return (string)@value;
	}

	public static string GetStringValue(RegistryKey key, string valueName)
	{
		object @value = RegistryHelper.GetValue(key, valueName);
		return (string)@value;
	}

	public static object GetValue(string keyName, string valueName)
	{
		object @value = Registry.GetValue(keyName, valueName, null);
		return @value;
	}

	public static object GetValue(string keyName)
	{
		object @value = Registry.GetValue(keyName, string.Empty, string.Empty);
		return @value;
	}

	public static object GetValue(RegistryKey key, string valueName)
	{
		object @value = key.GetValue(valueName);
		return @value;
	}

	public static IDisposable Isolate(string keyName, string valueName)
	{
		return RegistryHelper.Isolate(keyName, valueName, string.Empty);
	}

	public static IDisposable Isolate(string keyName, string valueName, object tempValue)
	{
		return new RegistryIsolator(keyName, valueName, tempValue);
	}

	public static RegistryKey OpenSubKey(RegistryKey parent, string name, bool writable)
	{
		RegistryKey key = parent.OpenSubKey(name, writable);
		return key;
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

		internal RegistryIsolator(string keyName, string valueName, object tempValue);

		public void Dispose();
	}
}
}