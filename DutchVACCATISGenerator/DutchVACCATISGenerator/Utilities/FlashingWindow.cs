using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

public static class FlashingWindow
{
    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    static extern bool FlashWindowEx(ref FLASHWINFO pwfi);

    public const UInt32 FLASHW_ALL = 3;
    public const UInt32 FLASHW_TIMERNOFG = 12;

    [StructLayout(LayoutKind.Sequential)]
    public struct FLASHWINFO
    {
        public UInt32 cbSize;
        public IntPtr hwnd;
        public UInt32 dwFlags;
        public UInt32 uCount;
        public UInt32 dwTimeout;
    }

    /// <summary>
    /// Flashes a form on the task bar.
    /// </summary>
    /// <param name="form">Form - form to start flashing</param>
    /// <returns>Boolean - indicates if the window can be flashed</returns>
    public static bool FlashWindowEx(Form form)
    {
        IntPtr hWnd = form.Handle;
        FLASHWINFO fInfo = new FLASHWINFO();

        fInfo.cbSize = Convert.ToUInt32(Marshal.SizeOf(fInfo));
        fInfo.hwnd = hWnd;
        fInfo.dwFlags = FLASHW_ALL | FLASHW_TIMERNOFG;
        fInfo.uCount = UInt32.MaxValue;
        fInfo.dwTimeout = 0;

        return FlashWindowEx(ref fInfo);
    }
}
