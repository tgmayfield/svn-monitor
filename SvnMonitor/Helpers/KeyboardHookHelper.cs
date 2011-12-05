using SVNMonitor.Entities;

namespace SVNMonitor.Helpers
{
    using SVNMonitor;
    using SVNMonitor.Extensions;
    using SVNMonitor.Logging;
    using SVNMonitor.Resources.Text;
    using SVNMonitor.Settings;
    using SVNMonitor.View;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Windows.Forms;

    internal class KeyboardHookHelper
    {
        private static Dictionary<KeyInfo, MethodInvoker> keyActions;
        private static KeyboardHook keyboardHook;

        private static void AddKeyAction(string key, MethodInvoker action)
        {
            try
            {
                KeyInfo keyInfo = KeyInfo.GetKeyInfo(key);
                if (keyInfo != KeyInfo.None)
                {
                    keyActions.Add(keyInfo, action);
                }
            }
            catch (Exception ex)
            {
                Logger.Log.Error(string.Format("Error setting hotkey: {0}", key), ex);
            }
        }

        private static void CreateKeyActionsMap()
        {
            keyActions = new Dictionary<KeyInfo, MethodInvoker>();
            AddKeyAction(ApplicationSettingsManager.Settings.KeyboardShortcutShowMainWindow, new MethodInvoker(MainForm.FormInstance.ShowOrHideForm));
            AddKeyAction(ApplicationSettingsManager.Settings.KeyboardShortcutUpdateAllAvailable, new MethodInvoker(Source.SVNUpdateAllAvailable));
            AddKeyAction(ApplicationSettingsManager.Settings.KeyboardShortcutCheckModifications, new MethodInvoker(Source.SVNCheckAllModifications));
            AddKeyAction(ApplicationSettingsManager.Settings.KeyboardShortcutCheckSources, new MethodInvoker(Updater.Instance.QueueUpdates));
        }

        private static void CreateKeyboardHook()
        {
            try
            {
                Uninstall();
                KeyHook = new KeyboardHook();
                KeyHook.KeyPressed += new EventHandler<KeyPressedEventArgs>(KeyboardHookHelper.KeyHook_KeyPressed);
            }
            catch (Exception ex)
            {
                Logger.Log.Error("Error creating keyboard-hook.", ex);
            }
        }

        private static void HandleKey(KeyInfo keyInfo)
        {
            MethodInvoker method = keyActions[keyInfo];
            method();
        }

        internal static void Install()
        {
            CreateKeyActionsMap();
            CreateKeyboardHook();
            RegisterKeys();
        }

        internal static bool IsKeyAvailable(KeyInfo keyInfo)
        {
            bool isAvailable = false;
            try
            {
                Keys realKey = EnumHelper.ParseEnum<Keys>(keyInfo.Key.ToString());
                int id = KeyHook.RegisterHotKey(keyInfo.Modifier, realKey);
                KeyHook.UnregisterHotKey(id);
                isAvailable = true;
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
                HandleKey(e.KeyInfo);
            }
            catch (Exception ex)
            {
                Logger.Log.Error("Error handling a keyboard hook", ex);
            }
        }

        private static void RegisterKeys()
        {
            foreach (KeyInfo keyInfo in keyActions.Keys)
            {
                try
                {
                    Keys realKey = EnumHelper.ParseEnum<Keys>(keyInfo.Key.ToString());
                    KeyHook.RegisterHotKey(keyInfo.Modifier, realKey);
                }
                catch (Exception ex)
                {
                    ErrorHandler.Append(Strings.ErrorAssignShortcutKey_FORMAT.FormatWith(new object[] { keyInfo }), keyInfo, ex);
                }
            }
        }

        internal static void Uninstall()
        {
            if (KeyHook != null)
            {
                KeyHook.Dispose();
            }
        }

        private static KeyboardHook KeyHook
        {
            [DebuggerNonUserCode]
            get
            {
                return keyboardHook;
            }
            [DebuggerNonUserCode]
            set
            {
                keyboardHook = value;
            }
        }
    }
}

