﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;

using SVNMonitor.Logging;
using SVNMonitor.Resources.Text;

namespace SVNMonitor.Helpers
{
	public class KeyInfo
	{
		private static readonly KeyInfo none;

		static KeyInfo()
		{
			KeyInfo tempLocal1 = new KeyInfo
			{
				Modifier = ModifierKey.None,
				Key = SVNMonitor.Helpers.Key.None
			};
			none = tempLocal1;
		}

		private KeyInfo()
		{
		}

		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			if (!(obj is KeyInfo))
			{
				return false;
			}
			KeyInfo that = (KeyInfo)obj;
			return ((Key == that.Key) && (Modifier == that.Modifier));
		}

		internal static IEnumerable<string> GetAvailableKeys()
		{
			List<string> list = new List<string>();
			foreach (SVNMonitor.Helpers.Key key in Enum.GetValues(typeof(SVNMonitor.Helpers.Key)))
			{
				if (key == SVNMonitor.Helpers.Key.None)
				{
					list.Add(Strings.KeyNone);
				}
				else
				{
					list.Add(key.ToString());
				}
			}
			return list;
		}

		public override int GetHashCode()
		{
			return (Key.GetHashCode() * Modifier.GetHashCode());
		}

		public static KeyInfo GetKeyInfo(string keyString)
		{
			if (string.IsNullOrEmpty(keyString))
			{
				return None;
			}
			if (IsNone(keyString))
			{
				return None;
			}
			string[] keys = keyString.Split(new[]
			{
				'+'
			}, StringSplitOptions.RemoveEmptyEntries);
			int len = keys.Length;
			if ((len < 2) || (len > 5))
			{
				Logger.Log.ErrorFormat("Bad key description: {0}", keyString);
				return None;
			}
			SVNMonitor.Helpers.Key key = SVNMonitor.Helpers.Key.None;
			ModifierKey modifier = ModifierKey.None;
			for (int i = 0; i < len; i++)
			{
				string current = keys[i];
				try
				{
					if (i == (len - 1))
					{
						key = (SVNMonitor.Helpers.Key)Enum.Parse(typeof(SVNMonitor.Helpers.Key), current, true);
						if (key == SVNMonitor.Helpers.Key.None)
						{
							return None;
						}
					}
					else
					{
						ModifierKey modKey = (ModifierKey)Enum.Parse(typeof(ModifierKey), current, true);
						modifier |= modKey;
					}
				}
				catch (Exception ex)
				{
					Logger.Log.Error(string.Format("Error parsing key: {0}", current), ex);
					return None;
				}
			}
			return GetKeyInfo(modifier, key);
		}

		public static KeyInfo GetKeyInfo(ModifierKey modifier, SVNMonitor.Helpers.Key key)
		{
			if ((modifier == ModifierKey.None) || (key == SVNMonitor.Helpers.Key.None))
			{
				return None;
			}
			return new KeyInfo
			{
				Modifier = modifier,
				Key = key
			};
		}

		public static KeyInfo GetKeyInfo(ModifierKey modifier, Keys realKey)
		{
			SVNMonitor.Helpers.Key key = EnumHelper.ParseEnum<SVNMonitor.Helpers.Key>(realKey.ToString());
			return GetKeyInfo(modifier, key);
		}

		private bool IsModifier(ModifierKey key)
		{
			return ((Modifier & key) != ModifierKey.None);
		}

		public static bool IsNone(string keyString)
		{
			if (string.IsNullOrEmpty(keyString))
			{
				return false;
			}
			return (keyString.Trim().ToLower() == None.ToString().ToLower());
		}

		public static bool IsValid(string keyString)
		{
			if (string.IsNullOrEmpty(keyString))
			{
				return false;
			}
			KeyInfo keyInfo = GetKeyInfo(keyString);
			if (keyInfo == null)
			{
				return false;
			}
			if (keyInfo.Equals(None))
			{
				return IsNone(keyString);
			}
			return true;
		}

		public override string ToString()
		{
			if (this == None)
			{
				return Strings.KeyNone;
			}
			string modifier = Modifier.ToString().Replace(", ", "+");
			return string.Format("{0}+{1}", modifier, Key);
		}

		public bool Alt
		{
			[DebuggerNonUserCode]
			get { return IsModifier(ModifierKey.None | ModifierKey.Alt); }
		}

		public bool Control
		{
			[DebuggerNonUserCode]
			get { return IsModifier(ModifierKey.Control); }
		}

		public SVNMonitor.Helpers.Key Key { get; set; }

		public string KeyString
		{
			get { return ToString(); }
		}

		public ModifierKey Modifier { get; set; }

		public static KeyInfo None
		{
			[DebuggerNonUserCode]
			get { return none; }
		}

		public bool Shift
		{
			[DebuggerNonUserCode]
			get { return IsModifier(ModifierKey.Shift); }
		}

		public bool Win
		{
			[DebuggerNonUserCode]
			get { return IsModifier(ModifierKey.Win); }
		}
	}
}