using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace TelegramTags
{
    public partial class MainForm : Form
    {
        HotKeyManager hotKey;
        TagPickerForm tagPicker;

        [DllImport("user32.dll")]
        static extern IntPtr GetForegroundWindow();

        public MainForm()
        {
            InitializeComponent();

            hotKey = new HotKeyManager(this);

            // TagPicker فعلاً ساخته نمی‌شود
            tagPicker = null;
        }

        private bool IsTelegramActive()
        {
            IntPtr hwnd = GetForegroundWindow();

            foreach (Process p in Process.GetProcesses())
            {
                if (p.MainWindowHandle == hwnd &&
                    p.ProcessName.ToLower().Contains("telegram"))
                {
                    return true;
                }
            }

            return false;
        }

        protected override void WndProc(ref Message m)
        {
            const int WM_HOTKEY = 0x0312;

            if (m.Msg == WM_HOTKEY)
            {
                if (IsTelegramActive())
                {
                    if (tagPicker == null || tagPicker.IsDisposed)
                    {
                        tagPicker = new TagPickerForm();
                        tagPicker.FormClosed += TagPicker_FormClosed;
                        tagPicker.Show();
                    }
                    else
                    {
                        tagPicker.BringToFront();
                        tagPicker.Activate();
                    }
                }
            }

            base.WndProc(ref m);
        }

        private void TagPicker_FormClosed(object sender, FormClosedEventArgs e)
        {
            tagPicker = null;

            // بستن TagPicker = پایان کامل برنامه
            Application.Exit();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (hotKey != null)
            {
                hotKey.Dispose();
                hotKey = null;
            }

            if (AutoTagger != null)
            {
                AutoTagger.Visible = false;
                AutoTagger.Dispose();
            }

            base.OnFormClosing(e);
        }
    }
}