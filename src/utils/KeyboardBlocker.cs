using System;
using System.Runtime.InteropServices;

namespace LoginSystem
{
    public static class KeyboardBlocker
    {
        private static IntPtr hookId = IntPtr.Zero;

        private delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);

        private static LowLevelKeyboardProc proc = HookCallback;

        public static void BlockKeys()
        {
            hookId = SetHook(proc);
        }

        public static void UnblockKeys()
        {
            if (hookId != IntPtr.Zero)
                UnhookWindowsHookEx(hookId);
        }

        private static IntPtr SetHook(LowLevelKeyboardProc proc)
        {
            using var curProc = System.Diagnostics.Process.GetCurrentProcess();
            using var curModule = curProc.MainModule;

            return SetWindowsHookEx(
                13,                      // WH_KEYBOARD_LL
                proc,
                GetModuleHandle(curModule.ModuleName),
                0
            );
        }

        private static IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0)
            {
                int vkCode = Marshal.ReadInt32(lParam);

                // ❌ BLOKIR tombol berbahaya
                if (vkCode == 0x09 && IsAltPressed()) return (IntPtr)1; // ALT+TAB
                if (vkCode == 0x1B && IsAltPressed()) return (IntPtr)1; // ALT+ESC
                if (vkCode == 0x73 && IsAltPressed()) return (IntPtr)1; // ALT+F4
                if (vkCode == 0x5B || vkCode == 0x5C) return (IntPtr)1; // Left/Right Windows Key
                if (vkCode == 0x2E && IsCtrlAltPressed()) return (IntPtr)1; // CTRL+ALT+DEL (tidak 100% bisa diblok)

                // ⭕ TOMBOL LAIN DIIZINKAN → TextBox bisa dipakai normal
            }

            return CallNextHookEx(hookId, nCode, wParam, lParam);
        }

        private static bool IsAltPressed() => (GetKeyState(0x12) & 0x8000) != 0;
        private static bool IsCtrlAltPressed() =>
            (GetKeyState(0x12) & 0x8000) != 0 && (GetKeyState(0x11) & 0x8000) != 0;

        [DllImport("user32.dll")]
        private static extern IntPtr SetWindowsHookEx(int idHook,
            LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll")]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll")]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll")]
        private static extern short GetKeyState(int nVirtKey);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);
    }
}
