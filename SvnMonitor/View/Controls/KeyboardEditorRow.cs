using System;
using System.Drawing;
using SVNMonitor.Helpers;

namespace SVNMonitor.View.Controls
{
public class KeyboardEditorRow
{
	public string AssociatedSetting
	{
		get;
		set;
	}

	public string Description
	{
		get;
		set;
	}

	public Image Image
	{
		get;
		set;
	}

	public KeyInfo KeyInfo
	{
		get;
		set;
	}

	public string KeyString
	{
		get
		{
			return this.KeyInfo.KeyString;
		}
	}

	public string Text
	{
		get;
		set;
	}

	public KeyboardEditorRow()
	{
	}
}
}