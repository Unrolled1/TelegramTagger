
using System;
using System.Threading;
using System.Windows.Forms;

namespace TelegramTags
{
    internal static class Program
    {
        private static Mutex mutex = null;

        [STAThread]
        static void Main()
        {
            const string appMutex = "TelegramTags_SingleInstance";

            bool createdNew;

            mutex = new Mutex(
                true,
                appMutex,
                out createdNew);

            if (!createdNew)
            {
               

                return;
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Paths.EnsureDataFolder();
            Application.Run(new TagPickerForm());

            mutex.ReleaseMutex();
            mutex.Dispose();
        }
    }
}
