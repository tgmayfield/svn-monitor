using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Diagnostics;

namespace SVNMonitor.Helpers
{
public sealed class KeyboardHook : IDisposable
{
	private int _currentId;

	private Window _window;

	private EventHandler<KeyPressedEventArgs> KeyPressed;

	public KeyboardHook()
	{
		EventHandler<KeyPressedEventArgs> eventHandler = null;
		this._window = new Window();
	}

	public void Dispose()
	{
		for (int i = this._currentId; i > 0; i--)
		{
			KeyboardHook.UnregisterHotKey(this._window.Handle, i);
		}
		this._window.Dispose();
	}

	[DllImport("user32.dll", CharSet=CharSet.None)]
	private static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vk);

	public int RegisterHotKey(ModifierKey modifier, Keys realKey)
	{
		this._currentId = this._currentId + 1;
		if (!KeyboardHook.RegisterHotKey(this._window.Handle, this._currentId, modifier, realKey))
		{
			throw new InvalidOperationException("Could not register the hot key.");
		}
		return this._currentId;
	}

	[DllImport("user32.dll", CharSet=CharSet.None)]
	private static extern bool UnregisterHotKey(IntPtr hWnd, int id);

	public void UnregisterHotKey(int id)
	{
		KeyboardHook.UnregisterHotKey(this._window.Handle, id);
	}

	public event EventHandler<KeyPressedEventArgs> KeyPressed;
	private class Window : NativeWindow, IDisposable
	{
		private EventHandler<KeyPressedEventArgs> KeyPressed;

		private static int WM_HOTKEY;

		static Window();

		public Window();

		public void Dispose();

		[DebuggerNonUserCode]
		protected override void WndProc(ref Message m);

		public event EventHandler<KeyPressedEventArgs> KeyPressed;;
	}
}
}