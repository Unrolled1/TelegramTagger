using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace TelegramTags
{
    public class HotKeyManager
    {
        [DllImport("user32.dll")]
        static extern bool RegisterHotKey(
            IntPtr hWnd,
            int id,
            uint fsModifiers,
            uint vk);

        [DllImport("user32.dll")]
        static extern bool UnregisterHotKey(
            IntPtr hWnd,
            int id);


        public const int ID = 9000;

        Form form;


        public HotKeyManager(Form form)
        {
            this.form = form;

            RegisterHotKey(
                form.Handle,
                ID,
                0,
                (uint)Keys.F8
            );
        }


        public void Dispose()
        {
            UnregisterHotKey(
                form.Handle,
                ID
            );
        }
    }
}