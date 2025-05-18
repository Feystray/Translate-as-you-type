using System;
using System.Diagnostics;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Text;


namespace AutoTrans
{
    class InterceptKeys
    {
        private const int WH_KEYBOARD_LL = 13;
        private const int WM_KEYDOWN = 0x0100;
        private LowLevelKeyboardProc _proc;
        private static IntPtr _hookID = IntPtr.Zero;
        public delegate void keypressedhandler(String key);

        public event keypressedhandler keypressed;

        public InterceptKeys()
        {
            _proc = HookCallback;
        }
        public void Start()
        {
            _hookID = SetHook(_proc);
            //UnhookWindowsHookEx(_hookID);
        }

        public void Stop()
        {
            //_hookID = SetHook(_proc);
            UnhookWindowsHookEx(_hookID);
        }

        static IntPtr SetHook(LowLevelKeyboardProc proc)
        {
            using (Process curProcess = Process.GetCurrentProcess())
            using (ProcessModule curModule = curProcess.MainModule)
            {
                return SetWindowsHookEx(WH_KEYBOARD_LL, proc,
                    GetModuleHandle(curModule.ModuleName), 0);
            }
        }

        private delegate IntPtr LowLevelKeyboardProc(
            int nCode, IntPtr wParam, IntPtr lParam);


        [StructLayout(LayoutKind.Sequential)]
        private struct KBDLLHOOKSTRUCT
        {
            public uint vkCode;
            public uint scanCode;
            public uint flags;
            public uint time;
            public IntPtr dwExtraInfo;
        }
        public enum VirtualKey : ushort
        {
            SHIFT = 0x10,
            CAPITAL = 0x14,
            // ... other virtual keys
        }

        public IntPtr HookCallback(
            int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0 && wParam == (IntPtr)WM_KEYDOWN)
            {
                KBDLLHOOKSTRUCT kbInfo = (KBDLLHOOKSTRUCT)Marshal.PtrToStructure(lParam, typeof(KBDLLHOOKSTRUCT));
                Keys key = (Keys)kbInfo.vkCode;

                if (key.ToString() == "Back")
                {
                    keypressed?.Invoke("Back");
                }
                else if (key.ToString() == "Enter")
                {
                    keypressed?.Invoke("Enter");
                }
                else if (key.ToString() == "Escape")
                {
                    keypressed?.Invoke("Escape");
                }
                else if (key.ToString() == "Home")
                {
                    keypressed?.Invoke("Home");
                }
                else if (key.ToString().Equals("Right")) {
                    keypressed?.Invoke("Right");
                }
                else if (key.ToString().Equals("Left"))
                {
                    keypressed?.Invoke("Left");
                }
                else if (key.ToString().Equals("PageUp"))
                {
                    keypressed?.Invoke("PageUp");
                }
                else if (key.ToString().Equals("PageDown"))
                {
                    keypressed?.Invoke("PageDown");
                }
                else if (key.ToString().Equals("End"))
                {
                    keypressed?.Invoke("End");
                }
                else if (key.ToString().Equals("Delete"))
                {
                    keypressed?.Invoke("Delete");
                }
                else
                {
                    byte[] keyboardState = new byte[256];
                    short shiftState = GetKeyState(VirtualKey.SHIFT);
                    short capslockState = GetKeyState(VirtualKey.CAPITAL);

                    // Apply Shift and Caps Lock manually if needed
                    byte[] modifiedKeyboardState = keyboardState.ToArray(); // Create a copy

                    // Check if Shift is pressed (high-order bit is set)
                    bool isShiftPressed = (shiftState & 0x8000) != 0;
                    // Check if Caps Lock is active (low-order bit is 1)
                    bool isCapsLockActive = (capslockState & 0x0001) != 0;

                    // Modify the keyboard state to reflect Shift being pressed if needed
                    if (isShiftPressed)
                    {
                        modifiedKeyboardState[(int)VirtualKey.SHIFT] |= 0x80;
                    }
                    else
                    {
                        modifiedKeyboardState[(int)VirtualKey.SHIFT] &= 0x7F;
                    }

                    // ToUnicodeEx should handle Caps Lock based on the layout and vkCode,
                    // but sometimes explicitly setting the Caps Lock state in keyboardState
                    // can help in background processing.
                    modifiedKeyboardState[(int)VirtualKey.CAPITAL] = (byte)(isCapsLockActive ? 0x01 : 0x00);
                    if (GetKeyboardState(keyboardState))
                    {
                        StringBuilder sb = new StringBuilder(5);
                        int result = ToUnicode(kbInfo.vkCode, kbInfo.scanCode, keyboardState, sb, sb.Capacity, 0);

                        if (result > 0)
                        {
                            keypressed?.Invoke(sb.ToString());
                        }
                    }


                }

                //int vkCode = Marshal.ReadInt32(lParam);
                //keypressed?.Invoke(((Keys)vkCode).ToString());
                //Debug.WriteLine((Keys)vkCode);
            }
            return CallNextHookEx(_hookID, nCode, wParam, lParam);
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook,
            LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode,
            IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);

        [DllImport("user32.dll")]
        static extern IntPtr GetKeyboardLayout(uint idThread);
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool GetKeyboardState(byte[] lpKeyState);
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        private static extern short GetKeyState(VirtualKey nVirtKey);

        //[DllImport("user32.dll")]
        //static extern int ToUnicodeEx(uint wVirtKey, uint wScanCode, byte[] lpKeyState, [Out, MarshalAs(UnmanagedType.LPWStr, SizeParamIndex = 4)] StringBuilder pwszBuff, int cchBuff, uint wFlags, IntPtr dwhkl);

        [DllImport("user32.dll")]
        private static extern int ToUnicode(
        uint wVirtKey,
        uint wScanCode,
        byte[] lpKeyState,
        [Out, MarshalAs(UnmanagedType.LPWStr, SizeConst = 64)] StringBuilder pwszBuff,
        int cchBuff,
        uint wFlags);
    }
}