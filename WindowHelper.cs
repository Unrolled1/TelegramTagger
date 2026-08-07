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


        public static void FocusTelegram()
        {
            foreach (Process p in Process.GetProcesses())
            {
                if (p.ProcessName.ToLower().Contains("telegram"))
                {
                    if (p.MainWindowHandle != IntPtr.Zero)
                    {
                        SetForegroundWindow(p.MainWindowHandle);
                        return;
                    }
                }
            }
        }
    }
}