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

            LoadGroups();
            LoadTags();
        }


        void LoadGroups()
        {
            string[] groups =
            {
                "Anime",
                "Game",
                "Stream"
            };

            foreach (string group in groups)
            {
                Button btn = new Button();

                btn.Text = group;
                btn.Width = 80;
                btn.Height = 35;

                btn.Tag = group;

                btn.Click += Group_Click;

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

                cb.Text = item.name;
                cb.Tag = item.tag;

                cb.AutoSize = true;
                cb.Font = new Font("Tahoma", 10);

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

            if (selectedGroup != "Game" && selectedGroup != "Anime")
                return;

            var categories = allTags
                .Where(x => x.group == selectedGroup)
                .ToList();

            foreach (var game in categories)
            {
                Button btn = new Button();

                btn.Text = game.name;
                btn.Width = 130;
                btn.Height = 35;

                // خود TagItem را داخل Tag نگه می‌داریم
                btn.Tag = game;

                btn.Click += Category_Click;

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

                cb.Text = item.name;
                cb.Tag = item.tag;

                cb.AutoSize = true;
                cb.Font = new Font("Tahoma", 10);

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

            selectedCategory = game.name;

            LoadCharacters(game);
        }


        private void button1_Click(object sender, EventArgs e)
        {
            string result = "";


            // تگ بازی فقط یک بار
            if (selectedGroup == "Game" &&
                !string.IsNullOrWhiteSpace(selectedCategory))
            {
                TagItem game = allTags.FirstOrDefault(x =>
                    x.group == "Game" &&
                    x.name == selectedCategory);

                if (game != null)
                {
                    result += game.tag + " ";
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
    }
}