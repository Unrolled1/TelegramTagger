using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Runtime.InteropServices;
using System.Linq;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TelegramTags
{
    
    public partial class MainForm : Form
    {
        HotKeyManager hotKey;
        [DllImport("user32.dll")]
        static extern IntPtr GetForegroundWindow();
        public MainForm()
        {
            InitializeComponent();
            hotKey = new HotKeyManager(this);
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
    private void MainForm_Load(object sender, EventArgs e)
        {

        }
        
        protected override void WndProc(ref Message m)
        {
            const int WM_HOTKEY = 0x0312;

            if (m.Msg == WM_HOTKEY)
            {
                if (IsTelegramActive())
                {
                    TagPickerForm f = new TagPickerForm();

                    Point pos = Cursor.Position;

                    f.StartPosition = FormStartPosition.Manual;
                    f.Location = pos;

                    f.Show();
                }
            }

            base.WndProc(ref m);
        }
    }
}
