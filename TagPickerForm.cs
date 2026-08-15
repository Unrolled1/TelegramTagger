using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace TelegramTags
{
    public partial class TagPickerForm : Form
    {
        List<TagItem> allTags = new List<TagItem>();

        List<Button> groupButtons = new List<Button>();

        List<CheckBox> selectedCharacterTags = new List<CheckBox>();

        List<CheckBox> selectedFixedTags = new List<CheckBox>();

        string selectedGroup = "Anime";

        string selectedCategory = "";


        public TagPickerForm()
        {
            InitializeComponent();


            SetColors();
            LoadGroups();
            LoadTags();
        }


        void LoadGroups()
        {
            string[] groups =
            {
                "Anime",
                "Game",
                "Other"
            };

            foreach (string group in groups)
            {
                Button btn = new Button();

                btn.Text = group;
                btn.AutoSize = true;
                btn.Padding = new Padding(10, 5, 10, 5);

                btn.Tag = group;

                btn.Click += Group_Click;
                btn.BackColor = Color.FromArgb(49, 51, 56);
                btn.ForeColor = Color.White;
                btn.FlatStyle = FlatStyle.Flat;
                btn.FlatAppearance.BorderSize = 0;

                flowGroups.Controls.Add(btn);

                groupButtons.Add(btn);
            }
        }


        void LoadTags()
        {
            string file = "hashtags.json";

            if (!File.Exists(file))
            {
                MessageBox.Show("hashtags.json پیدا نشد.");
                return;
            }

            string json = File.ReadAllText(file);

            allTags = JsonConvert.DeserializeObject<List<TagItem>>(json)
                      ?? new List<TagItem>();

            LoadFixedTags();
        }


        void LoadFixedTags()
        {
            flowFixedTags.Controls.Clear();

            selectedFixedTags.Clear();

            var general = allTags.FirstOrDefault(x => x.group == "General");

            if (general == null || general.Fixedtags == null)
                return;

            foreach (var item in general.Fixedtags)
            {
                CheckBox cb = new CheckBox();

                cb.Text = item.tag;
                cb.Tag = item.tag;

                cb.AutoSize = true;
                cb.ForeColor = Color.White;
                cb.BackColor = Color.Transparent;

                flowFixedTags.Controls.Add(cb);

                selectedFixedTags.Add(cb);
            }
        }


        void LoadCategories()
        {
            flowCategories.Controls.Clear();
            flowCharacters.Controls.Clear();

            selectedCharacterTags.Clear();
            selectedCategory = "";

            if (selectedGroup != "Game" &&
                selectedGroup != "Anime" &&
                selectedGroup != "Other")
                return;

            var categories = allTags
    .Where(x => x.group == selectedGroup)
    .OrderBy(x => x.tag, StringComparer.OrdinalIgnoreCase)
    .ToList();

            foreach (var item in categories)
            {
                Button btn = new Button();

                btn.Text = item.tag;
                btn.AutoSize = true;
                btn.Padding = new Padding(10, 5, 10, 5);

                btn.Tag = item;

                btn.Click += Category_Click;

                btn.BackColor = Color.FromArgb(49, 51, 56);
                btn.ForeColor = Color.White;
                btn.FlatStyle = FlatStyle.Flat;
                btn.FlatAppearance.BorderSize = 0;

                flowCategories.Controls.Add(btn);
            }
        }

        void LoadCharacters(TagItem game)
        {
            flowCharacters.Controls.Clear();
            selectedCharacterTags.Clear();

            if (game.characters == null)
                return;

            foreach (var item in game.characters)
            {
                CheckBox cb = new CheckBox();

                cb.Text = item.tag;
                cb.Tag = item.tag;

                cb.AutoSize = true;
                cb.ForeColor = Color.White;
                cb.BackColor = Color.Transparent;

                flowCharacters.Controls.Add(cb);

                selectedCharacterTags.Add(cb);
            }

            flowCharacters.PerformLayout();
            flowCharacters.Refresh();
        }


        private void Group_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;

            selectedGroup = btn.Tag.ToString();

            LoadCategories();
        }


        private void Category_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;

            TagItem game = (TagItem)btn.Tag;

            selectedCategory = game.tag;

            LoadCharacters(game);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string result = "";

            // تگ اصلی Game / Anime / Other
            if ((selectedGroup == "Game" ||
                 selectedGroup == "Anime" ||
                 selectedGroup == "Other") &&
                !string.IsNullOrWhiteSpace(selectedCategory))
            {
                TagItem item = allTags.FirstOrDefault(x =>
                    (x.group == "Game" ||
                     x.group == "Anime" ||
                     x.group == "Other") &&
                    x.tag == selectedCategory);

                if (item != null)
                {
                    result += item.tag + " ";
                }
            }

            // تگ شخصیت‌ها
            foreach (CheckBox cb in selectedCharacterTags)
            {
                if (cb.Checked)
                {
                    result += cb.Tag.ToString() + " ";
                }
            }

            // تگ‌های ثابت
            foreach (CheckBox cb in selectedFixedTags)
            {
                if (cb.Checked)
                {
                    result += cb.Tag.ToString() + " ";
                }
            }

            result = result.Trim();

            if (string.IsNullOrWhiteSpace(result))
                return;

            Clipboard.SetText(result);

            System.Threading.Thread.Sleep(300);

            if (WindowHelper.FocusTelegram())
            {
                System.Threading.Thread.Sleep(500);
                SendKeys.SendWait("^v");
            }

            ResetForm();
        }

        private void ResetForm()
        {
            // برداشتن تیک شخصیت‌ها
            foreach (CheckBox cb in selectedCharacterTags)
            {
                cb.Checked = false;
            }


            // برداشتن تیک هشتگ‌های ثابت
            foreach (CheckBox cb in selectedFixedTags)
            {
                cb.Checked = false;
            }


            // حذف لیست شخصیت‌ها
            selectedCharacterTags.Clear();

            flowCharacters.Controls.Clear();


            // بازی‌ها دوباره نمایش داده شوند
            // ولی تب Game حفظ شود
            if (selectedGroup == "Game")
            {
                LoadCategories();
            }
        }

        private void btnAddTag_Click(object sender, EventArgs e)
        {
            AddTagForm form = new AddTagForm();

            form.ShowDialog();

            LoadTags();

            LoadCategories();
        }

        private void btnManageTags_Click(object sender, EventArgs e)
        {
            ManageTagsForm form = new ManageTagsForm();

            form.ShowDialog();

            LoadTags();
            LoadCategories();
        }
        void SetColors()
        {
            this.BackColor = Color.FromArgb(30, 31, 34);

            flowGroups.BackColor = Color.FromArgb(43, 45, 49);
            flowCategories.BackColor = Color.FromArgb(43, 45, 49);
            flowCharacters.BackColor = Color.FromArgb(43, 45, 49);
            flowFixedTags.BackColor = Color.FromArgb(43, 45, 49);

            SetButtonColors(flowGroups);
            SetButtonColors(flowCategories);

            btnInsert.BackColor = Color.FromArgb(88, 101, 242);
            btnInsert.ForeColor = Color.White;

            btnAddTag.BackColor = Color.FromArgb(87, 242, 135);
            btnAddTag.ForeColor = Color.Black;

            btnManageTags.BackColor = Color.Red;
            btnManageTags.ForeColor = Color.White;
        }

        void SetButtonColors(Control parent)
        {
            foreach (Control control in parent.Controls)
            {
                if (control is Button btn)
                {
                    btn.BackColor = Color.FromArgb(49, 51, 56);
                    btn.ForeColor = Color.White;
                    btn.FlatStyle = FlatStyle.Flat;
                    btn.FlatAppearance.BorderSize = 0;
                }
            }
        }
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;

                cp.ExStyle &= ~0x00000080; // WS_EX_TOOLWINDOW
                cp.ExStyle |= 0x00040000;  // WS_EX_APPWINDOW

                return cp;
            }
        }
    }
}