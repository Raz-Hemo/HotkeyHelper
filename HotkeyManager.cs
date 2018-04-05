using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Threading;
using System.Collections.Generic;
using System.Linq;

namespace hotkeyhelper
{
    class HotkeyManager
    {
        // Happens when a hotkey is pressed
        public static event EventHandler<HotkeyEventArgs> HotkeyPressed;
        private static void OnHotKeyPressed(HotkeyEventArgs e)
        {
            HotkeyPressed?.Invoke(null, e);
        }

        // Allows (Un)registerHotKey to be used as a delegate
        delegate void RegisterHotKeyDelegate(IntPtr hwnd, int id, uint modifiers, uint key);
        delegate void UnRegisterHotKeyDelegate(IntPtr hwnd, int id);
        private static void RegisterHotKeyInternal(IntPtr hwnd, int id, uint modifiers, uint key)
        {
            RegisterHotKey(hwnd, id, modifiers, key);
        }
        private static void UnRegisterHotKeyInternal(IntPtr hwnd, int id)
        {
            UnregisterHotKey(_hwnd, id);
        }

        // Run the invisible form on startup
        static HotkeyManager()
        {
            Thread messageLoop = new Thread(delegate ()
            {
                Application.Run(new MessageWindow());
            });
            messageLoop.Name = "MessageLoopThread";
            messageLoop.IsBackground = true;
            messageLoop.Start();
        }

        // The invisible form that gets all the hotkey events
        private static volatile MessageWindow _wnd;
        private static volatile IntPtr _hwnd;
        private static ManualResetEvent _windowReadyEvent = new ManualResetEvent(false);

        // Handle setting new hotkey arrays
        private static List<HotkeyDesc> hotkeys = new List<HotkeyDesc>();
        public static List<HotkeyDesc> Hotkeys
        {
            get => hotkeys;
            set
            {
                // Make sure window is ready
                _windowReadyEvent.WaitOne();

                // Unbind previous
                for (int i = 0; i < hotkeys.Count; i++)
                    _wnd.Invoke(new UnRegisterHotKeyDelegate(UnRegisterHotKeyInternal), _hwnd, i);

                // Bind new
                hotkeys = value;
                for (int i = 0; i < hotkeys.Count; i++)
                    _wnd.Invoke(new RegisterHotKeyDelegate(RegisterHotKeyInternal), _hwnd, i, (uint)hotkeys[i].mods, (uint)hotkeys[i].key);
            }
        }

        private class MessageWindow : Form
        {
            public MessageWindow()
            {
                _wnd = this;
                _hwnd = this.Handle;
                _windowReadyEvent.Set();
            }

            protected override void WndProc(ref Message m)
            {
                if (m.Msg == WM_HOTKEY)
                {
                    HotkeyEventArgs e = new HotkeyEventArgs(m.LParam);
                    OnHotKeyPressed(e);
                }

                base.WndProc(ref m);
            }

            protected override void SetVisibleCore(bool value)
            {
                // Ensure the window never becomes visible
                base.SetVisibleCore(false);
            }

            private const int WM_HOTKEY = 0x312;
        }


        // Import hotkey functions from user32.dll
        [DllImport("user32", SetLastError = true)]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vk);
        [DllImport("user32", SetLastError = true)]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);
    }

    public class HotkeyEventArgs : EventArgs
    {
        public readonly Keys Key;
        public readonly KeyModifiers Modifiers;

        public HotkeyEventArgs(Keys key, KeyModifiers modifiers)
        {
            Key = key;
            Modifiers = modifiers;
        }

        public HotkeyEventArgs(IntPtr hotKeyParam)
        {
            uint param = (uint)hotKeyParam.ToInt64();
            Key = (Keys)((param & 0xffff0000) >> 16);
            Modifiers = (KeyModifiers)(param & 0x0000ffff);
        }
    }

    [Flags]
    public enum KeyModifiers
    {
        Alt = 1,
        Control = 2,
        Shift = 4,
        NoRepeat = 0x4000
    }

    public class HotkeyDesc
    {
        public Keys key;
        public KeyModifiers mods;

        public HotkeyDesc(Keys key, KeyModifiers mods)
        {
            this.key = key;
            this.mods = mods;
        }

        // Create from a string representation such as "Ctrl+Alt+G"
        public HotkeyDesc(string string_key)
        {
            mods = 0;
            key = 0;

            string[] components = string_key.Split('+');
            string key_str = components.Last();

            if (key_str.Length == 1 && key_str[0] >= 'A' && key_str[0] <= 'Z')
                key = (System.Windows.Forms.Keys)(0x41 + (key_str[0] - 'A'));
            else if (key_str.Length == 1 && key_str[0] >= 'a' && key_str[0] <= 'z')
                key = (System.Windows.Forms.Keys)(0x41 + (key_str[0] - 'a'));
            else
                throw new ArgumentException("Invalid key!");

            // Extract mods
            mods |= KeyModifiers.NoRepeat;
            if (Array.IndexOf(components, "Alt") > -1) mods |= KeyModifiers.Alt;
            if (Array.IndexOf(components, "Ctrl") > -1) mods |= KeyModifiers.Control;
            if (Array.IndexOf(components, "Shift") > -1) mods |= KeyModifiers.Shift;
        }
    }
}
