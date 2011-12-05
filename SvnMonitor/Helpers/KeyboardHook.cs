namespace SVNMonitor.Helpers
{
    using System;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;

    public sealed class KeyboardHook : IDisposable
    {
        private int _currentId;
        private Window _window = new Window();

        public event EventHandler<KeyPressedEventArgs> KeyPressed;

        public KeyboardHook()
        {
            this._window.KeyPressed += delegate (object sender, KeyPressedEventArgs args) {
                if (this.KeyPressed != null)
                {
                    this.KeyPressed(this, args);
                }
            };
        }

        public void Dispose()
        {
            for (int i = this._currentId; i > 0; i--)
            {
                UnregisterHotKey(this._window.Handle, i);
            }
            this._window.Dispose();
        }

        public int RegisterHotKey(ModifierKey modifier, Keys realKey)
        {
            this._currentId++;
            if (!RegisterHotKey(this._window.Handle, this._currentId, (uint) modifier, (uint) realKey))
            {
                throw new InvalidOperationException("Could not register the hot key.");
            }
            return this._currentId;
        }

        [DllImport("user32.dll")]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vk);
        public void UnregisterHotKey(int id)
        {
            UnregisterHotKey(this._window.Handle, id);
        }

        [DllImport("user32.dll")]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        private class Window : NativeWindow, IDisposable
        {
            private static int WM_HOTKEY = 0x312;

            public event EventHandler<KeyPressedEventArgs> KeyPressed;

            public Window()
            {
                this.CreateHandle(new CreateParams());
            }

            public void Dispose()
            {
                this.DestroyHandle();
            }

            [DebuggerNonUserCode]
            protected override void WndProc(ref Message m)
            {
                base.WndProc(ref m);
                if (m.Msg == WM_HOTKEY)
                {
                    Keys key = ((Keys) (((int) m.LParam) >> 0x10)) & Keys.KeyCode;
                    ModifierKey modifier = ((ModifierKey) ((int) m.LParam)) & ((ModifierKey) 0xffff);
                    if (this.KeyPressed != null)
                    {
                        this.KeyPressed(this, new KeyPressedEventArgs(modifier, key));
                    }
                }
            }
        }
    }
}

