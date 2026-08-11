using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace TelegramTags
{
    public partial class AddTagForm : Form
    {
        public AddTagForm()
        {
            InitializeComponent();
            cmbType.SelectedIndexChanged += cmbType_SelectedIndexChanged;
            cmbType.Items.Clear();

            cmbType.Items.Add("Game");
            cmbType.Items.Add("Anime");
            cmbType.Items.Add("Fixed");
            cmbType.Items.Add("Character");
            cmbType.SelectedIndex = 0;
        }


        private void btnSave_Click(object sender, EventArgs e)
        {
            string tag = txtTag.Text.Trim();
            string type = cmbType.Text;

            if ( string.IsNullOrWhiteSpace(tag))
            {
                MessageBox.Show("نام و هشتگ را وارد کنید.");
                return;
            }

            if (!tag.StartsWith("#"))
                tag = "#" + tag;

            string file = "hashtags.json";

            if (!File.Exists(file))
            {
                MessageBox.Show("hashtags.json پیدا نشد.");
                return;
            }

            string json = File.ReadAllText(file);

            List<TagItem> allTags =
                JsonConvert.DeserializeObject<List<TagItem>>(json)
                ?? new List<TagItem>();


            // =========================
            // هشتگ ثابت
            // =========================

            if (type == "Fixed")
            {
                TagItem general = allTags
                    .FirstOrDefault(x => x.group == "General");

                if (general == null)
                {
                    general = new TagItem
                    {
                        group = "General",
                        Fixedtags = new List<FixedTagItem>()
                    };

                    allTags.Add(general);
                }

                if (general.Fixedtags == null)
                    general.Fixedtags = new List<FixedTagItem>();

                general.Fixedtags.Add(new FixedTagItem
                {
                    tag = tag
                });
            }


            // =========================
            // Game یا Anime جدید
            // =========================

            else if (type == "Game" || type == "Anime")
            {
                allTags.Add(new TagItem
                {
                    group = type,
                    tag = tag,
                    characters = new List<CharacterItem>()
                });
            }


            // =========================
            // شخصیت
            // =========================

            else if (type == "Character")
            {
                if (cmbCategory.SelectedItem == null)
                {
                    MessageBox.Show("یک عنوان انتخاب کنید.");
                    return;
                }

                string category = cmbCategory.SelectedItem.ToString();

                TagItem parent = allTags.FirstOrDefault(x =>
                    (x.group == "Game" || x.group == "Anime") &&
                    x.tag == category);

                if (parent == null)
                {
                    MessageBox.Show("عنوان پیدا نشد.");
                    return;
                }

                if (parent.characters == null)
                    parent.characters = new List<CharacterItem>();

                parent.characters.Add(new CharacterItem
                {
                    tag = tag
                });
            }


            string newJson = JsonConvert.SerializeObject(
                allTags,
                Formatting.Indented
            );

            File.WriteAllText(file, newJson);

            MessageBox.Show("با موفقیت اضافه شد.");

            Close();
        }

        private void cmbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbCategory.Items.Clear();

            string type = cmbType.Text;

            if (type != "Character")
            {
                lblCategory.Visible = false;
                cmbCategory.Visible = false;
                return;
            }

            lblCategory.Visible = true;
            cmbCategory.Visible = true;

            string file = "hashtags.json";

            if (!File.Exists(file))
                return;

            string json = File.ReadAllText(file);

            List<TagItem> allTags =
                JsonConvert.DeserializeObject<List<TagItem>>(json)
                ?? new List<TagItem>();

            foreach (var item in allTags.Where(x =>
                x.group == "Game" || x.group == "Anime"))
            {
                cmbCategory.Items.Add(item.tag);
            }

            if (cmbCategory.Items.Count > 0)
                cmbCategory.SelectedIndex = 0;
        }

    }
}