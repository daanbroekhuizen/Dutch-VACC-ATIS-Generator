using System;
using System.Runtime.InteropServices;

public static class FlashingWindow
{
    public const UInt32 FLASHW_ALL = 3;
    public const UInt32 FLASHW_TIMERNOFG = 12;

    [StructLayout(LayoutKind.Sequential)]
    public struct FLASHWINFO
    {
        public FLASHWINFO(IntPtr hwnd, UInt32 dwFlags, UInt32 uCount, UInt32 dwTimeout)
        {
            this.cbSize = 0;
            this.hwnd = hwnd;
            this.dwFlags = dwFlags;
            this.uCount = uCount;
            this.dwTimeout = dwTimeout;
        }

        public UInt32 cbSize;
        public readonly IntPtr hwnd;
        public UInt32 dwFlags;
        public UInt32 uCount;
        public UInt32 dwTimeout;
    }
}
