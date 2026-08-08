using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

namespace TelegramTags
{
    public static class WindowHelper
    {
        [DllImport("user32.dll")]
        static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        static extern int GetWindowText(
            IntPtr hWnd,
            StringBuilder text,
            int count);

        [DllImport("user32.dll")]
        static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        static extern bool ShowWindow(
            IntPtr hWnd,
            int nCmdShow);

        [DllImport("user32.dll")]
        static extern bool IsIconic(IntPtr hWnd);

        const int SW_RESTORE = 9;


        public static string GetActiveWindowTitle()
        {
            IntPtr handle = GetForegroundWindow();

            StringBuilder title = new StringBuilder(256);

            GetWindowText(
                handle,
                title,
                title.Capacity
            );

            return title.ToString();
        }


        public static bool FocusTelegram()
        {
            foreach (Process p in Process.GetProcesses())
            {
                if (!p.ProcessName.ToLower().Contains("telegram"))
                    continue;

                IntPtr handle = p.MainWindowHandle;

                if (handle == IntPtr.Zero)
                    continue;


                // اگر تلگرام Minimize شده
                if (IsIconic(handle))
                {
                    ShowWindow(handle, SW_RESTORE);
                }


                // آوردن تلگرام جلو
                SetForegroundWindow(handle);

                return true;
            }

            return false;
        }
    }
}