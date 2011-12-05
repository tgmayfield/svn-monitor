using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace SVNMonitor.Helpers
{
	public sealed class KeyboardHook : IDisposable
	{
		private int _currentId;
		private readonly Window _window = new Window();

		public event EventHandler<KeyPressedEventArgs> KeyPressed;

		public KeyboardHook()
		{
			_window.KeyPressed += delegate(object sender, KeyPressedEventArgs args)
			{
				if (KeyPressed != null)
				{
					KeyPressed(this, args);
				}
			};
		}

		public void Dispose()
		{
			for (int i = _currentId; i > 0; i--)
			{
				UnregisterHotKey(_window.Handle, i);
			}
			_window.Dispose();
		}

		public int RegisterHotKey(ModifierKey modifier, Keys realKey)
		{
			_currentId++;
			if (!RegisterHotKey(_window.Handle, _currentId, (uint)modifier, (uint)realKey))
			{
				throw new InvalidOperationException("Could not register the hot key.");
			}
			return _currentId;
		}

		[DllImport("user32.dll")]
		private static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vk);

		public void UnregisterHotKey(int id)
		{
			UnregisterHotKey(_window.Handle, id);
		}

		[DllImport("user32.dll")]
		private static extern bool UnregisterHotKey(IntPtr hWnd, int id);

		private class Window : NativeWindow, IDisposable
		{
			private static int WM_HOTKEY = 0x312;

			public event EventHandler<KeyPressedEventArgs> KeyPressed;

			public Window()
			{
				CreateHandle(new CreateParams());
			}

			public void Dispose()
			{
				DestroyHandle();
			}

			[DebuggerNonUserCode]
			protected override void WndProc(ref Message m)
			{
				base.WndProc(ref m);
				if (m.Msg == WM_HOTKEY)
				{
					Keys key = ((Keys)(((int)m.LParam) >> 0x10)) & Keys.KeyCode;
					ModifierKey modifier = ((ModifierKey)((int)m.LParam)) & ((ModifierKey)0xffff);
					if (KeyPressed != null)
					{
						KeyPressed(this, new KeyPressedEventArgs(modifier, key));
					}
				}
			}
		}
	}
}