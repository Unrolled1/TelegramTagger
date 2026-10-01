using System;
using System.Collections.Generic;
using System.Drawing;
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

            SetColors();
            SetupSaveButton();

            cmbType.Items.Clear();

            cmbType.Items.Add("Fixed");
            cmbType.Items.Add("Tag");
            cmbType.Items.Add("Character");

            cmbType.SelectedIndexChanged +=
                cmbType_SelectedIndexChanged;

            cmbType.SelectedIndex = 0;
        }


        // =========================
        // Save
        // =========================

        private void btnSave_Click(object sender, EventArgs e)
        {
            string tag = txtTag.Text.Trim();
            string type = cmbType.Text;

            if (string.IsNullOrWhiteSpace(tag))
            {
                MessageBox.Show("هشتگ را وارد کنید.");
                return;
            }


            // =========================
            // افزودن # برای Tag و Character
            // =========================

            if (!tag.StartsWith("#") &&
                type != "Fixed")
            {
                tag = "#" + tag;
            }


            // Fixed می‌تواند @ داشته باشد
            // مثل @EveArt2


            string file = Paths.HashtagsFile;

            if (!File.Exists(file))
            {
                MessageBox.Show(
                    "hashtags.json پیدا نشد.");

                return;
            }


            string json =
                File.ReadAllText(file);


            List<TagItem> allTags =
                JsonConvert.DeserializeObject<List<TagItem>>(json)
                ?? new List<TagItem>();


            // =========================
            // Fixed
            // =========================

            if (type == "Fixed")
            {
                TagItem general =
                    allTags.FirstOrDefault(
                        x => x.group == "Fixed");


                if (general == null)
                {
                    general = new TagItem
                    {
                        group = "Fixed",
                        tags = new List<TagItem>()
                    };

                    allTags.Add(general);
                }


                if (general.tags == null)
                {
                    general.tags =
                        new List<TagItem>();
                }


                bool duplicate =
                    general.tags.Any(x =>
                        string.Equals(
                            x.tag,
                            tag,
                            StringComparison.OrdinalIgnoreCase));


                if (duplicate)
                {
                    ShowDuplicate();
                    return;
                }


                general.tags.Add(
                    new TagItem
                    {
                        tag = tag
                    });
            }


            // =========================
            // Tag
            // =========================

            else if (type == "Tag")
            {
                TagItem tagGroup =
                    allTags.FirstOrDefault(
                        x => x.group == "Tag");


                if (tagGroup == null)
                {
                    tagGroup = new TagItem
                    {
                        group = "Tag",
                        tags = new List<TagItem>()
                    };

                    allTags.Add(tagGroup);
                }


                if (tagGroup.tags == null)
                {
                    tagGroup.tags =
                        new List<TagItem>();
                }


                bool duplicate =
                    tagGroup.tags.Any(x =>
                        string.Equals(
                            x.tag,
                            tag,
                            StringComparison.OrdinalIgnoreCase));


                if (duplicate)
                {
                    ShowDuplicate();
                    return;
                }


                tagGroup.tags.Add(
                    new TagItem
                    {
                        tag = tag,
                        characters =
                            new List<CharacterItem>()
                    });
            }


            // =========================
            // Character
            // =========================

            else if (type == "Character")
            {
                if (cmbCategory.SelectedItem == null)
                {
                    MessageBox.Show(
                        "یک Tag انتخاب کنید.");

                    return;
                }


                string parentTag =
                    cmbCategory.SelectedItem.ToString();


                TagItem tagGroup =
                    allTags.FirstOrDefault(
                        x => x.group == "Tag");


                if (tagGroup == null ||
                    tagGroup.tags == null)
                {
                    MessageBox.Show(
                        "Tag انتخاب‌شده پیدا نشد.");

                    return;
                }


                TagItem parent =
                    tagGroup.tags.FirstOrDefault(x =>
                        string.Equals(
                            x.tag,
                            parentTag,
                            StringComparison.OrdinalIgnoreCase));


                if (parent == null)
                {
                    MessageBox.Show(
                        "Tag انتخاب‌شده پیدا نشد.");

                    return;
                }


                if (parent.characters == null)
                {
                    parent.characters =
                        new List<CharacterItem>();
                }


                bool duplicate =
                    parent.characters.Any(x =>
                        string.Equals(
                            x.tag,
                            tag,
                            StringComparison.OrdinalIgnoreCase));


                if (duplicate)
                {
                    ShowDuplicate();
                    return;
                }


                parent.characters.Add(
                    new CharacterItem
                    {
                        tag = tag
                    });
            }


            // =========================
            // Save JSON
            // =========================

            string newJson =
                JsonConvert.SerializeObject(
                    allTags,
                    Formatting.Indented);


            File.WriteAllText(
                file,
                newJson);


            Close();
        }


        // =========================
        // Type Changed
        // =========================

        private void cmbType_SelectedIndexChanged(
            object sender,
            EventArgs e)
        {
            cmbCategory.Items.Clear();


            string type =
                cmbType.Text;


            // فقط Character نیاز به Parent Tag دارد
            if (type != "Character")
            {
                lblCategory.Visible = false;
                cmbCategory.Visible = false;

                return;
            }


            lblCategory.Visible = true;
            cmbCategory.Visible = true;


            string file =Paths.HashtagsFile;

            if (!File.Exists(file))
                return;


            string json =
                File.ReadAllText(file);


            List<TagItem> allTags =
                JsonConvert.DeserializeObject<List<TagItem>>(json)
                ?? new List<TagItem>();


            // =========================
            // فقط Tag ها
            // =========================

            var tagGroup =
                allTags.FirstOrDefault(
                    x => x.group == "Tag");


            if (tagGroup == null ||
                tagGroup.tags == null)
                return;


            var tags = tagGroup.tags
                .OrderBy(
                    x => x.tag,
                    StringComparer.OrdinalIgnoreCase)
                .ToList();


            foreach (var item in tags)
            {
                cmbCategory.Items.Add(
                    item.tag);
            }


            if (cmbCategory.Items.Count > 0)
            {
                cmbCategory.SelectedIndex = 0;
            }
        }


        // =========================
        // Duplicate
        // =========================

        private void ShowDuplicate()
        {
            MessageBox.Show(
                "این تگ از قبل وجود دارد.",
                "تگ تکراری",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
        }


        // =========================
        // Colors
        // =========================

        void SetColors()
        {
            this.BackColor =
                Color.FromArgb(30, 31, 34);


            lblCategory.ForeColor =
                Color.White;

            lblTag.ForeColor =
                Color.White;

            lblType.ForeColor =
                Color.White;


            cmbType.BackColor =
                Color.FromArgb(43, 45, 49);

            cmbType.ForeColor =
                Color.White;


            cmbCategory.BackColor =
                Color.FromArgb(43, 45, 49);

            cmbCategory.ForeColor =
                Color.White;


            txtTag.BackColor =
                Color.FromArgb(43, 45, 49);

            txtTag.ForeColor =
                Color.White;
        }


        // =========================
        // Save Button
        // =========================

        private void SetupSaveButton()
        {
            btnSave.UseVisualStyleBackColor =
                false;

            btnSave.FlatStyle =
                FlatStyle.Flat;

            btnSave.FlatAppearance.BorderSize =
                0;

            btnSave.BackColor =
                Color.FromArgb(87, 242, 135);

            btnSave.ForeColor =
                Color.Black;


            btnSave.MouseEnter +=
                BtnSave_MouseEnter;

            btnSave.MouseLeave +=
                BtnSave_MouseLeave;
        }


        private void BtnSave_MouseEnter(
            object sender,
            EventArgs e)
        {
            btnSave.BackColor =
                Color.FromArgb(120, 255, 160);

            btnSave.ForeColor =
                Color.Black;
        }


        private void BtnSave_MouseLeave(
            object sender,
            EventArgs e)
        {
            btnSave.BackColor =
                Color.FromArgb(87, 242, 135);

            btnSave.ForeColor =
                Color.Black;
        }
    }
}