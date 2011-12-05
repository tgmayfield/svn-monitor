using System;
using System.Collections.Generic;
using SVNMonitor.Resources.Text;
using SVNMonitor.Logging;
using System.Windows.Forms;

namespace SVNMonitor.Helpers
{
public class KeyInfo
{
	private static KeyInfo none;

	public bool Alt
	{
		get
		{
			return this.IsModifier(ModifierKey.Alt);
		}
	}

	public bool Control
	{
		get
		{
			return this.IsModifier(ModifierKey.Control);
		}
	}

	public Key Key
	{
		get;
		set;
	}

	public string KeyString
	{
		get
		{
			return this.ToString();
		}
	}

	public ModifierKey Modifier
	{
		get;
		set;
	}

	public static KeyInfo None
	{
		get
		{
			return KeyInfo.none;
		}
	}

	public bool Shift
	{
		get
		{
			return this.IsModifier(ModifierKey.Shift);
		}
	}

	public bool Win
	{
		get
		{
			return this.IsModifier(ModifierKey.Win);
		}
	}

	static KeyInfo()
	{
		KeyInfo keyInfo = new KeyInfo();
		keyInfo.Modifier = ModifierKey.None;
		keyInfo.Key = Key.None;
		KeyInfo.none = keyInfo;
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
		if (!obj as KeyInfo)
		{
			return false;
		}
		KeyInfo that = (KeyInfo)obj;
		if (this.Key == that.Key)
		{
			return this.Modifier == that.Modifier;
		}
		return false;
	}

	internal static IEnumerable<string> GetAvailableKeys()
	{
		List<string> list = new List<string>();
		foreach (Key key in Enum.GetValues(typeof(Key)))
		{
			if (key == Key.None)
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
		return this.Key.GetHashCode() * this.Modifier.GetHashCode();
	}

	public static KeyInfo GetKeyInfo(string keyString)
	{
		char[] chrArray;
		if (string.IsNullOrEmpty(keyString))
		{
			return KeyInfo.None;
		}
		if (KeyInfo.IsNone(keyString))
		{
			return KeyInfo.None;
		}
		string[] keys = keyString.Split(new char[] { 43 }, StringSplitOptions.RemoveEmptyEntries);
		int len = (int)keys.Length;
		if (len < 2 || len > 5)
		{
			Logger.Log.ErrorFormat("Bad key description: {0}", keyString);
			return KeyInfo.None;
		}
		Key key = Key.None;
		ModifierKey modifier = ModifierKey.None;
		for (int i = 0; i < len; i++)
		{
			string current = keys[i];
			try
			{
				if (i == len - 1)
				{
					key = (Key)Enum.Parse(typeof(Key), current, true);
					if (key == Key.None)
					{
						return KeyInfo.None;
						ModifierKey modKey = (ModifierKey)Enum.Parse(typeof(ModifierKey), current, true);
						modifier = modifier | modKey;
					}
				}
			}
			catch (Exception ex)
			{
				Logger.Log.Error(string.Format("Error parsing key: {0}", current), ex);
				return KeyInfo.None;
			}
		}
		KeyInfo keyInfo = KeyInfo.GetKeyInfo(modifier, key);
		return keyInfo;
	}

	public static KeyInfo GetKeyInfo(ModifierKey modifier, Keys realKey)
	{
		Key key = EnumHelper.ParseEnum<Key>(realKey.ToString());
		KeyInfo keyInfo = KeyInfo.GetKeyInfo(modifier, key);
		return keyInfo;
	}

	public static KeyInfo GetKeyInfo(ModifierKey modifier, Key key)
	{
		if (modifier == ModifierKey.None || key == Key.None)
		{
			return KeyInfo.None;
		}
		KeyInfo keyInfo = new KeyInfo();
		keyInfo.Modifier = modifier;
		keyInfo.Key = key;
		KeyInfo keyInfo = keyInfo;
		return keyInfo;
	}

	private bool IsModifier(ModifierKey key)
	{
		bool isModifier = (this.Modifier & key) != ModifierKey.None;
		return isModifier;
	}

	public static bool IsNone(string keyString)
	{
		if (string.IsNullOrEmpty(keyString))
		{
			return false;
		}
		if (keyString.Trim().ToLower() == KeyInfo.None.ToString().ToLower())
		{
			return true;
		}
		return false;
	}

	public static bool IsValid(string keyString)
	{
		if (string.IsNullOrEmpty(keyString))
		{
			return false;
		}
		KeyInfo keyInfo = KeyInfo.GetKeyInfo(keyString);
		if (keyInfo == null)
		{
			return false;
		}
		if (keyInfo.Equals(KeyInfo.None))
		{
			bool isNone = KeyInfo.IsNone(keyString);
			return isNone;
		}
		return true;
	}

	public override string ToString()
	{
		if (this == KeyInfo.None)
		{
			return Strings.KeyNone;
		}
		string modifier = this.Modifier.ToString().Replace(", ", "+");
		return string.Format("{0}+{1}", modifier, this.Key);
	}
}
}