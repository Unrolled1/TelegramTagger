using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.Drawing;

namespace TelegramTags
{
    public partial class ManageTagsForm : Form
    {
        List<TagItem> allTags = new List<TagItem>();

        public ManageTagsForm()
        {
            InitializeComponent();

            LoadTags();

            btnDelete.Click += btnDelete_Click;
        }

        // =========================
        // Load Tags
        // =========================

        void LoadTags()
        {
            treeTags.Nodes.Clear();

            string file = Paths.HashtagsFile;

            if (!File.Exists(file))
                return;

            string json = File.ReadAllText(file);

            allTags =
                JsonConvert.DeserializeObject<List<TagItem>>(json)
                ?? new List<TagItem>();


            // =========================
            // همه Tag ها
            // یکجا و الفبایی
            // =========================

            TreeNode tagsNode = new TreeNode("Tags");
            tagsNode.Tag = "Group";


            TagItem tagGroup =
                allTags.FirstOrDefault(
                    x => x.group == "Tag");


            if (tagGroup != null &&
                tagGroup.tags != null)
            {
                var mainTags = tagGroup.tags
                    .OrderBy(x => x.tag,
                        StringComparer.OrdinalIgnoreCase)
                    .ToList();


                foreach (var item in mainTags)
                {
                    TreeNode tagNode =
                        new TreeNode(item.tag);

                    tagNode.Tag = item;


                    // =========================
                    // Characters
                    // =========================

                    if (item.characters != null)
                    {
                        foreach (var character in item.characters
                            .OrderBy(x => x.tag,
                                StringComparer.OrdinalIgnoreCase))
                        {
                            TreeNode charNode =
                                new TreeNode(character.tag);

                            charNode.Tag = character;

                            tagNode.Nodes.Add(charNode);
                        }
                    }


                    tagsNode.Nodes.Add(tagNode);
                }
            }


            treeTags.Nodes.Add(tagsNode);


            // =========================
            // Fixed Tags
            // =========================

            TreeNode fixedNode =
                new TreeNode("Fixed");

            fixedNode.Tag = "Group";


            TagItem general =
                allTags.FirstOrDefault(
                    x => x.group == "Fixed");


            if (general != null &&
                general.tags != null)
            {
                foreach (var fixedTag in general.tags
                    .OrderBy(x => x.tag,
                        StringComparer.OrdinalIgnoreCase))
                {
                    TreeNode fixedItem =
                        new TreeNode(fixedTag.tag);

                    fixedItem.Tag = fixedTag;

                    fixedNode.Nodes.Add(fixedItem);
                }
            }


            treeTags.Nodes.Add(fixedNode);

            treeTags.ExpandAll();
        }


        // =========================
        // Input Box
        // =========================

        private string ShowInputBox(
            string title,
            string value)
        {
            using (Form form = new Form())
            using (TextBox textBox = new TextBox())
            using (Button btnOk = new Button())
            using (Button btnCancel = new Button())
            {
                form.Text = title;
                form.StartPosition =
                    FormStartPosition.CenterParent;

                form.FormBorderStyle =
                    FormBorderStyle.FixedDialog;

                form.MinimizeBox = false;
                form.MaximizeBox = false;
                form.ShowInTaskbar = false;

                form.Font = this.Font;

                form.Size = new Size(
                    this.ClientSize.Width,
                    140);


                textBox.Font = this.Font;
                textBox.Text = value;

                textBox.Left = 15;
                textBox.Top = 15;

                textBox.Width =
                    form.ClientSize.Width - 30;


                btnOk.Text = "Edit";
                btnOk.Font = this.Font;

                btnOk.DialogResult =
                    DialogResult.OK;

                btnOk.Width = 80;
                btnOk.Height = 35;

                btnOk.Left =
                    form.ClientSize.Width - 175;

                btnOk.Top = 55;


                btnCancel.Text = "Cancel";
                btnCancel.Font = this.Font;

                btnCancel.DialogResult =
                    DialogResult.Cancel;

                btnCancel.Width = 80;
                btnCancel.Height = 35;

                btnCancel.Left =
                    form.ClientSize.Width - 85;

                btnCancel.Top = 55;


                form.Controls.Add(textBox);
                form.Controls.Add(btnOk);
                form.Controls.Add(btnCancel);


                form.AcceptButton = btnOk;
                form.CancelButton = btnCancel;

                form.ShowIcon = false;


                form.Load += (s, e) =>
                {
                    textBox.SelectAll();
                    textBox.Focus();
                };


                if (form.ShowDialog(this)
                    == DialogResult.OK)
                {
                    return textBox.Text;
                }


                return "";
            }
        }


        // =========================
        // Delete
        // =========================

        private void btnDelete_Click(
            object sender,
            EventArgs e)
        {
            if (treeTags.SelectedNode == null)
                return;


            TreeNode node =
                treeTags.SelectedNode;


            // گروه Tags یا Fixed
            // قابل حذف نیستند
            if (node.Tag is string)
            {
                MessageBox.Show(
                    "این مورد قابل حذف نیست.");

                return;
            }


            DialogResult result =
                MessageBox.Show(
                    "آیا مطمئن هستید که می‌خواهید این مورد را حذف کنید؟",
                    "حذف هشتگ",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);


            if (result != DialogResult.Yes)
                return;


            // =========================
            // Character
            // =========================

            if (node.Tag is CharacterItem character)
            {
                TagItem parent =
                    node.Parent?.Tag as TagItem;


                if (parent != null &&
                    parent.characters != null)
                {
                    parent.characters.Remove(
                        character);
                }
            }


            // =========================
            // Main Tag
            // =========================

            else if (node.Tag is TagItem item)
            {
                TagItem tagGroup =
                    allTags.FirstOrDefault(
                        x => x.group == "Tag");


                if (tagGroup != null &&
                    tagGroup.tags != null)
                {
                    tagGroup.tags.Remove(item);
                }
            }


            // =========================
            // Fixed Tag
            // =========================

            else if (node.Tag is TagItem fixedTag)
            {
                TagItem general =
                    allTags.FirstOrDefault(
                        x => x.group == "Fixed");


                if (general != null &&
                    general.tags != null)
                {
                    general.tags.Remove(
                        fixedTag);
                }
            }


            SaveTags();

            LoadTags();
        }


        // =========================
        // Save
        // =========================

        void SaveTags()
        {
            string json =
                JsonConvert.SerializeObject(
                    allTags,
                    Formatting.Indented);


            File.WriteAllText(
                "hashtags.json",
                json);
        }


        // =========================
        // Close
        // =========================

        private void btnClose_Click(
            object sender,
            EventArgs e)
        {
            Close();
        }


        // =========================
        // Edit
        // =========================

        private void btnEdit_Click(
            object sender,
            EventArgs e)
        {
            if (treeTags.SelectedNode == null)
                return;


            TreeNode node =
                treeTags.SelectedNode;


            // Tags / Fixed عنوان‌ها
            // قابل ویرایش نیستند
            if (node.Tag is string)
            {
                MessageBox.Show(
                    "این مورد قابل ویرایش نیست.");

                return;
            }


            string currentTag = "";


            if (node.Tag is TagItem item)
                currentTag = item.tag;

            else if (node.Tag is CharacterItem character)
                currentTag = character.tag;


            if (string.IsNullOrWhiteSpace(
                currentTag))
                return;


            string newTag =
                ShowInputBox(
                    "ویرایش هشتگ",
                    currentTag).Trim();


            if (string.IsNullOrWhiteSpace(
                newTag))
                return;


            if (!newTag.StartsWith("#"))
                newTag = "#" + newTag;


            if (newTag.Length <= 1)
            {
                MessageBox.Show(
                    "هشتگ معتبر نیست.");

                return;
            }


            // =========================
            // Main Tag (inside Tag group)
            // =========================

            if (node.Tag is TagItem tagItem)
            {
                TagItem tagGroup =
                    allTags.FirstOrDefault(
                        x => x.group == "Tag");


                // اگر این آیتم داخل گروه Tag است → ویرایش Tag
                if (tagGroup != null &&
                    tagGroup.tags != null &&
                    tagGroup.tags.Contains(tagItem))
                {
                    bool duplicate =
                        tagGroup.tags.Any(x =>
                            x != tagItem &&
                            string.Equals(
                                x.tag,
                                newTag,
                                StringComparison.OrdinalIgnoreCase));


                    if (duplicate)
                    {
                        MessageBox.Show(
                            "این هشتگ از قبل وجود دارد.");

                        return;
                    }


                    tagItem.tag = newTag;
                }

                // در غیر این صورت → احتمالاً Fixed Tag
                else
                {
                    TagItem general =
                        allTags.FirstOrDefault(
                            x => x.group == "Fixed");


                    if (general == null ||
                        general.tags == null)
                    {
                        MessageBox.Show(
                            "لیست تگ‌های ثابت پیدا نشد.");

                        return;
                    }


                    bool duplicate =
                        general.tags.Any(x =>
                            x != tagItem &&
                            string.Equals(
                                x.tag,
                                newTag,
                                StringComparison.OrdinalIgnoreCase));


                    if (duplicate)
                    {
                        MessageBox.Show(
                            "این هشتگ از قبل وجود دارد.");

                        return;
                    }


                    tagItem.tag = newTag;
                }
            }


            // =========================
            // Character
            // =========================

            else if (node.Tag is CharacterItem characterItem)
            {
                TagItem parent =
                    node.Parent?.Tag as TagItem;


                if (parent == null ||
                    parent.characters == null)
                {
                    MessageBox.Show(
                        "عنوان شخصیت پیدا نشد.");

                    return;
                }


                bool duplicate =
                    parent.characters.Any(x =>
                        x != characterItem &&
                        string.Equals(
                            x.tag,
                            newTag,
                            StringComparison.OrdinalIgnoreCase));


                if (duplicate)
                {
                    MessageBox.Show(
                        "این هشتگ در این عنوان از قبل وجود دارد.");

                    return;
                }


                characterItem.tag = newTag;
            }


            SaveTags();

            LoadTags();
        }
    }
}