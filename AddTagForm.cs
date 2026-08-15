using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.Drawing;


namespace TelegramTags
{
    public partial class AddTagForm : Form
    {
        public AddTagForm()
        {
            InitializeComponent();
            SetColors();
            cmbType.Items.Clear();

            cmbType.Items.Add("Game");
            cmbType.Items.Add("Anime");
            cmbType.Items.Add("Other");
            cmbType.Items.Add("Character");

            cmbType.SelectedIndexChanged += cmbType_SelectedIndexChanged;

            cmbType.SelectedIndex = 0;
        }


        private void btnSave_Click(object sender, EventArgs e)
        {
            string tag = txtTag.Text.Trim();
            string type = cmbType.Text;

            if (string.IsNullOrWhiteSpace(tag))
            {
                MessageBox.Show("هشتگ را وارد کنید.");
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

            // جلوگیری از تکراری بودن تگ
            bool duplicate = false;

            // تگ ثابت
            if (type == "Fixed")
            {
                TagItem general = allTags.FirstOrDefault(x => x.group == "General");

                if (general != null && general.Fixedtags != null)
                {
                    duplicate = general.Fixedtags.Any(x =>
                        string.Equals(x.tag, tag, StringComparison.OrdinalIgnoreCase));
                }
            }

            // عنوان Game / Anime / Other
            else if (type == "Game" ||
                     type == "Anime" ||
                     type == "Other")
            {
                duplicate = allTags.Any(x =>
                    (x.group == "Game" ||
                     x.group == "Anime" ||
                     x.group == "Other") &&
                    string.Equals(x.tag, tag, StringComparison.OrdinalIgnoreCase));
            }

            // شخصیت
            else if (type == "Character")
            {
                if (cmbCategory.SelectedItem == null)
                {
                    MessageBox.Show("یک عنوان انتخاب کنید.");
                    return;
                }

                string category = cmbCategory.SelectedItem.ToString();

                TagItem parent = allTags.FirstOrDefault(x =>
                    (x.group == "Game" ||
                     x.group == "Anime" ||
                     x.group == "Other") &&
                    x.tag == category);

                if (parent == null)
                {
                    MessageBox.Show("عنوان پیدا نشد.");
                    return;
                }

                if (parent.characters != null)
                {
                    duplicate = parent.characters.Any(x =>
                        string.Equals(x.tag, tag, StringComparison.OrdinalIgnoreCase));
                }
            }

            if (duplicate)
            {
                MessageBox.Show(
                    "این هشتگ از قبل وجود دارد.",
                    "تگ تکراری",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            // Fixed
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


            // Game / Anime / Other
            else if (type == "Game" ||
                     type == "Anime" ||
                     type == "Other")
            {
                allTags.Add(new TagItem
                {
                    group = type,
                    tag = tag,
                    characters = new List<CharacterItem>()
                });
            }


            // Character
            else if (type == "Character")
            {
                if (cmbCategory.SelectedItem == null)
                {
                    MessageBox.Show("یک عنوان انتخاب کنید.");
                    return;
                }

                string category =
                    cmbCategory.SelectedItem.ToString();


                TagItem parent = allTags.FirstOrDefault(x =>
                    (x.group == "Game" ||
                     x.group == "Anime" ||
                     x.group == "Other") &&
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


            

            Close();
        }


        private void cmbType_SelectedIndexChanged(
            object sender,
            EventArgs e)
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


            // همه Game + Anime + Other
            var categories = allTags
    .Where(x =>
        x.group == "Game" ||
        x.group == "Anime" ||
        x.group == "Other")
    .OrderBy(x => x.tag, StringComparer.OrdinalIgnoreCase)
    .ToList();

            foreach (var item in categories)
            {
                cmbCategory.Items.Add(item.tag);
            }


            if (cmbCategory.Items.Count > 0)
                cmbCategory.SelectedIndex = 0;
        }
        void SetColors()
        {
            this.BackColor = Color.FromArgb(30, 31, 34);

            lblCategory.ForeColor = Color.White;
            lblTag.ForeColor = Color.White;
            lblType.ForeColor = Color.White;
            cmbType.BackColor = Color.FromArgb(43, 45, 49);
            cmbType.ForeColor = Color.White;

            cmbCategory.BackColor = Color.FromArgb(43, 45, 49);
            cmbCategory.ForeColor = Color.White;

            txtTag.BackColor = Color.FromArgb(43, 45, 49);
            txtTag.ForeColor = Color.White;

            btnSave.BackColor = Color.FromArgb(87, 242, 135);
            btnSave.ForeColor = Color.Black;
            btnSave.FlatStyle = FlatStyle.Flat;
            btnSave.FlatAppearance.BorderSize = 0;
        }
    }

}