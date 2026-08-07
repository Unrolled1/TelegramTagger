using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.IO;

namespace TelegramTags
{
    public partial class TagPickerForm : Form
    {
        public TagPickerForm()
        {
            InitializeComponent();
            LoadTags();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string result = "";

            foreach (var cb in selectedTags)
            {
                if (cb.Checked)
                {
                    result += cb.Tag.ToString() + " ";
                }
            }


            Clipboard.SetText(result.Trim());


            this.Close();


            System.Threading.Thread.Sleep(300);


            WindowHelper.FocusTelegram();


            System.Threading.Thread.Sleep(300);


            SendKeys.SendWait("^v");
        }
        List<CheckBox> selectedTags = new List<CheckBox>();

       

        void LoadTags()
        {
            string file = "hashtags.json";

            if (!File.Exists(file))
                return;

            var json = File.ReadAllText(file);

            var tags = JsonConvert.DeserializeObject<List<TagItem>>(json);


            foreach (var item in tags)
            {
                CheckBox cb = new CheckBox();

                cb.Text = item.name;
                cb.Tag = item.tags;

                cb.AutoSize = true;
                cb.Font = new Font("vazir", 10);

                flowTags.Controls.Add(cb);

                selectedTags.Add(cb);
            }
        }
    }
}
