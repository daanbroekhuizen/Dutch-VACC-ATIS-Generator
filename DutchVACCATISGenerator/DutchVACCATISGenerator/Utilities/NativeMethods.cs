using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using static FlashingWindow;

namespace DutchVACCATISGenerator.Utilities
{
    public static class NativeMethods
    {
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool FlashWindowEx(ref FLASHWINFO pwfi);

        /// <summary>
        /// Flashes a form on the task bar.
        /// </summary>
        /// <param name="form">Form - form to start flashing</param>
        /// <returns>Boolean - indicates if the window can be flashed</returns>
        public static bool FlashWindowEx(Form form)
        {
            FLASHWINFO fInfo = new FLASHWINFO(
                hwnd: form.Handle,
                dwFlags: FLASHW_ALL | FLASHW_TIMERNOFG,
                uCount: UInt32.MaxValue,
                dwTimeout: 0);

            fInfo.cbSize = Convert.ToUInt32(Marshal.SizeOf(fInfo));

            return FlashWindowEx(ref fInfo);
        }

        /// <summary>
        /// Retrieves a handle to the foreground window (the window with which the user is currently working).
        /// </summary>
        /// <returns>IntPtr - Handle of foreground window</returns>
        [DllImport("user32.dll")]
        internal static extern IntPtr GetForegroundWindow();
    }
}
