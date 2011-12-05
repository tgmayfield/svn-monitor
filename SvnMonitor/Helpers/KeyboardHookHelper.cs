using System.Collections.Generic;
using System;
using System.Windows.Forms;
using SVNMonitor.Logging;
using SVNMonitor.Settings;
using SVNMonitor.View;
using SVNMonitor.Entities;
using SVNMonitor;
using SVNMonitor.Resources.Text;

namespace SVNMonitor.Helpers
{
internal class KeyboardHookHelper
{
	private static Dictionary<KeyInfo, MethodInvoker> keyActions;

	private static KeyboardHook keyboardHook;

	private static KeyboardHook KeyHook
	{
		get
		{
			return KeyboardHookHelper.keyboardHook;
		}
		set
		{
			KeyboardHookHelper.keyboardHook = value;
		}
	}

	public KeyboardHookHelper()
	{
	}

	private static void AddKeyAction(string key, MethodInvoker action)
	{
		KeyInfo keyInfo;
		try
		{
			keyInfo = KeyInfo.GetKeyInfo(key);
			KeyboardHookHelper.keyActions.Add(keyInfo, action);
		}
		catch (Exception ex)
		{
			Logger.Log.Error(string.Format("Error setting hotkey: {0}", key), ex);
		}
		if (keyInfo == KeyInfo.None)
		{
		}
		else
		{
		}
	}

	private static void CreateKeyActionsMap()
	{
		KeyboardHookHelper.keyActions = new Dictionary<KeyInfo, MethodInvoker>();
		KeyboardHookHelper.AddKeyAction(ApplicationSettingsManager.Settings.KeyboardShortcutShowMainWindow, new MethodInvoker(MainForm.FormInstance.ShowOrHideForm));
		KeyboardHookHelper.AddKeyAction(ApplicationSettingsManager.Settings.KeyboardShortcutUpdateAllAvailable, new MethodInvoker(null.Source.SVNUpdateAllAvailable));
		KeyboardHookHelper.AddKeyAction(ApplicationSettingsManager.Settings.KeyboardShortcutCheckModifications, new MethodInvoker(null.Source.SVNCheckAllModifications));
		KeyboardHookHelper.AddKeyAction(ApplicationSettingsManager.Settings.KeyboardShortcutCheckSources, new MethodInvoker(Updater.Instance.QueueUpdates));
	}

	private static void CreateKeyboardHook()
	{
		try
		{
			KeyboardHookHelper.Uninstall();
			KeyboardHookHelper.KeyHook = new KeyboardHook();
			KeyboardHookHelper.KeyHook.add_KeyPressed(new EventHandler<KeyPressedEventArgs>(null.KeyboardHookHelper.KeyHook_KeyPressed));
		}
		catch (Exception ex)
		{
			Logger.Log.Error("Error creating keyboard-hook.", ex);
		}
	}

	private static void HandleKey(KeyInfo keyInfo)
	{
		MethodInvoker method = KeyboardHookHelper.keyActions[keyInfo];
		method();
	}

	internal static void Install()
	{
		KeyboardHookHelper.CreateKeyActionsMap();
		KeyboardHookHelper.CreateKeyboardHook();
		KeyboardHookHelper.RegisterKeys();
	}

	internal static bool IsKeyAvailable(KeyInfo keyInfo)
	{
		bool isAvailable = false;
		try
		{
			Keys realKey = EnumHelper.ParseEnum<Keys>(keyInfo.Key.ToString());
			int id = KeyboardHookHelper.KeyHook.RegisterHotKey(keyInfo.Modifier, realKey);
			KeyboardHookHelper.KeyHook.UnregisterHotKey(id);
			return true;
		}
		catch (InvalidOperationException)
		{
		}
		catch (Exception ex)
		{
			Logger.Log.Error(string.Format("Error checking for available key: {0}", keyInfo), ex);
		}
		return isAvailable;
	}

	private static void KeyHook_KeyPressed(object sender, KeyPressedEventArgs e)
	{
		try
		{
			Logger.Log.InfoFormat("Hooked-key pressed: {0}", e.KeyInfo);
			KeyboardHookHelper.HandleKey(e.KeyInfo);
		}
		catch (Exception ex)
		{
			Logger.Log.Error("Error handling a keyboard hook", ex);
		}
	}

	private static void RegisterKeys()
	{
		object[] objArray;
		foreach (KeyInfo keyInfo in KeyboardHookHelper.keyActions.Keys)
		{
			try
			{
				Keys realKey = EnumHelper.ParseEnum<Keys>(keyInfo.Key.ToString());
				KeyboardHookHelper.KeyHook.RegisterHotKey(keyInfo.Modifier, realKey);
			}
			catch (Exception ex)
			{
				string text = Strings.ErrorAssignShortcutKey_FORMAT.FormatWith(new object[] { keyInfo });
				ErrorHandler.Append(text, keyInfo, ex);
			}
		}
	}

	internal static void Uninstall()
	{
		if (KeyboardHookHelper.KeyHook != null)
		{
			KeyboardHookHelper.KeyHook.Dispose();
		}
	}
}
}