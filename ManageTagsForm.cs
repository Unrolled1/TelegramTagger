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

        void LoadTags()
        {
            treeTags.Nodes.Clear();

            string file = "hashtags.json";

            if (!File.Exists(file))
                return;

            string json = File.ReadAllText(file);

            allTags = JsonConvert.DeserializeObject<List<TagItem>>(json)
                      ?? new List<TagItem>();


            // Game
            TreeNode gameNode = new TreeNode("Game");
            gameNode.Tag = "Group";

            foreach (var game in allTags.Where(x => x.group == "Game"))
            {
                TreeNode gameItem = new TreeNode(game.tag);
                gameItem.Tag = game;

                if (game.characters != null)
                {
                    foreach (var character in game.characters)
                    {
                        TreeNode charNode =
                            new TreeNode(character.tag);

                        charNode.Tag = character;

                        gameItem.Nodes.Add(charNode);
                    }
                }

                gameNode.Nodes.Add(gameItem);
            }

            treeTags.Nodes.Add(gameNode);


            // Anime
            TreeNode animeNode = new TreeNode("Anime");
            animeNode.Tag = "Group";

            foreach (var anime in allTags.Where(x => x.group == "Anime"))
            {
                TreeNode animeItem =
                    new TreeNode(anime.tag);

                animeItem.Tag = anime;

                if (anime.characters != null)
                {
                    foreach (var character in anime.characters)
                    {
                        TreeNode charNode =
                            new TreeNode(character.tag);

                        charNode.Tag = character;

                        animeItem.Nodes.Add(charNode);
                    }
                }

                animeNode.Nodes.Add(animeItem);
            }

            treeTags.Nodes.Add(animeNode);

            // Other
            TreeNode otherNode = new TreeNode("Other");
            otherNode.Tag = "Group";

            foreach (var other in allTags.Where(x => x.group == "Other"))
            {
                TreeNode otherItem = new TreeNode(other.tag);
                otherItem.Tag = other;

                if (other.characters != null)
                {
                    foreach (var character in other.characters)
                    {
                        TreeNode charNode =
                            new TreeNode(character.tag);

                        charNode.Tag = character;

                        otherItem.Nodes.Add(charNode);
                    }
                }

                otherNode.Nodes.Add(otherItem);
            }

            treeTags.Nodes.Add(otherNode);
            // Fixed
            TreeNode fixedNode = new TreeNode("Fixed");
            fixedNode.Tag = "Group";

            TagItem general = allTags
                .FirstOrDefault(x => x.group == "General");

            if (general != null && general.Fixedtags != null)
            {
                foreach (var fixedTag in general.Fixedtags)
                {
                    TreeNode fixedItem =
                        new TreeNode(fixedTag.tag);

                    fixedItem.Tag = fixedTag;

                    fixedNode.Nodes.Add(fixedItem);
                }
            }

            treeTags.Nodes.Add(fixedNode);

        }

        private string ShowInputBox(string title, string value)
        {
            using (Form form = new Form())
            using (TextBox textBox = new TextBox())
            using (Button btnOk = new Button())
            using (Button btnCancel = new Button())
            {
                form.Text = title;
                form.StartPosition = FormStartPosition.CenterParent;
                form.FormBorderStyle = FormBorderStyle.FixedDialog;
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
                textBox.Width = form.ClientSize.Width - 30;

                btnOk.Text = "Edit";
                btnOk.Font = this.Font;
                btnOk.DialogResult = DialogResult.OK;
                btnOk.Width = 80;
                btnOk.Height = 35;
                btnOk.Left = form.ClientSize.Width - 175;
                btnOk.Top = 55;

                btnCancel.Text = "Cancel";
                btnCancel.Font = this.Font;
                btnCancel.DialogResult = DialogResult.Cancel;
                btnCancel.Width = 80;
                btnCancel.Height = 35;
                btnCancel.Left = form.ClientSize.Width - 85;
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

                if (form.ShowDialog(this) == DialogResult.OK)
                    return textBox.Text;

                return "";
            }
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (treeTags.SelectedNode == null)
                return;

            TreeNode node = treeTags.SelectedNode;

            if (node.Tag is string)
            {
                MessageBox.Show("این مورد قابل حذف نیست.");
                return;
            }


            DialogResult result = MessageBox.Show(
                "آیا مطمئن هستید که می‌خواهید این مورد را حذف کنید؟",
                "حذف هشتگ",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (result != DialogResult.Yes)
                return;


            // شخصیت
            if (node.Tag is CharacterItem)
            {
                CharacterItem character =
                    (CharacterItem)node.Tag;

                TreeNode parentNode = node.Parent;

                TagItem parent = parentNode.Tag as TagItem;

                if (parent != null && parent.characters != null)
                {
                    parent.characters.Remove(character);
                }
            }


            // بازی یا انیمه
            else if (node.Tag is TagItem)
            {
                TagItem item =
                    (TagItem)node.Tag;

                allTags.Remove(item);
            }


            // هشتگ ثابت
            else if (node.Tag is FixedTagItem)
            {
                FixedTagItem fixedTag =
                    (FixedTagItem)node.Tag;

                TagItem general = allTags
                    .FirstOrDefault(x => x.group == "General");

                if (general != null &&
                    general.Fixedtags != null)
                {
                    general.Fixedtags.Remove(fixedTag);
                }
            }


            SaveTags();

            LoadTags();
        }


        void SaveTags()
        {
            string json = JsonConvert.SerializeObject(
                allTags,
                Formatting.Indented);

            File.WriteAllText(
                "hashtags.json",
                json);
        }


        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (treeTags.SelectedNode == null)
                return;

            TreeNode node = treeTags.SelectedNode;

            if (node.Tag is string)
            {
                MessageBox.Show("این مورد قابل ویرایش نیست.");
                return;
            }

            string currentTag = "";

            if (node.Tag is TagItem)
                currentTag = ((TagItem)node.Tag).tag;
            else if (node.Tag is CharacterItem)
                currentTag = ((CharacterItem)node.Tag).tag;
            else if (node.Tag is FixedTagItem)
                currentTag = ((FixedTagItem)node.Tag).tag;

            if (string.IsNullOrWhiteSpace(currentTag))
                return;

            string newTag = ShowInputBox("ویرایش هشتگ", currentTag).Trim();

            if (string.IsNullOrWhiteSpace(newTag))
                return;

            if (!newTag.StartsWith("#"))
                newTag = "#" + newTag;

            if (node.Tag is TagItem item)
            {
                bool duplicate = allTags.Any(x =>
                    x != item &&
                    (x.group == "Game" ||
                     x.group == "Anime" ||
                     x.group == "Other") &&
                    string.Equals(
                        x.tag,
                        newTag,
                        StringComparison.OrdinalIgnoreCase));

                if (duplicate)
                {
                    MessageBox.Show("این هشتگ از قبل وجود دارد.");
                    return;
                }

                item.tag = newTag;
            }
            else if (node.Tag is CharacterItem character)
            {
                TagItem parent = node.Parent.Tag as TagItem;

                if (parent != null && parent.characters != null)
                {
                    bool duplicate = parent.characters.Any(x =>
                        x != character &&
                        string.Equals(
                            x.tag,
                            newTag,
                            StringComparison.OrdinalIgnoreCase));

                    if (duplicate)
                    {
                        MessageBox.Show("این هشتگ در این عنوان از قبل وجود دارد.");
                        return;
                    }
                }

                character.tag = newTag;
            }
            else if (node.Tag is FixedTagItem fixedTag)
            {
                TagItem general = allTags
                    .FirstOrDefault(x => x.group == "General");

                if (general != null && general.Fixedtags != null)
                {
                    bool duplicate = general.Fixedtags.Any(x =>
                        x != fixedTag &&
                        string.Equals(
                            x.tag,
                            newTag,
                            StringComparison.OrdinalIgnoreCase));

                    if (duplicate)
                    {
                        MessageBox.Show("این هشتگ از قبل وجود دارد.");
                        return;
                    }
                }

                fixedTag.tag = newTag;
            }

            SaveTags();
            LoadTags();
        }
    }
}