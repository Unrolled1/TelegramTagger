using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Newtonsoft.Json;

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
                TreeNode gameItem = new TreeNode(game.name);
                gameItem.Tag = game;

                if (game.characters != null)
                {
                    foreach (var character in game.characters)
                    {
                        TreeNode charNode =
                            new TreeNode(character.name);

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
                    new TreeNode(anime.name);

                animeItem.Tag = anime;

                if (anime.characters != null)
                {
                    foreach (var character in anime.characters)
                    {
                        TreeNode charNode =
                            new TreeNode(character.name);

                        charNode.Tag = character;

                        animeItem.Nodes.Add(charNode);
                    }
                }

                animeNode.Nodes.Add(animeItem);
            }

            treeTags.Nodes.Add(animeNode);


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
                        new TreeNode(fixedTag.name);

                    fixedItem.Tag = fixedTag;

                    fixedNode.Nodes.Add(fixedItem);
                }
            }

            treeTags.Nodes.Add(fixedNode);

            treeTags.ExpandAll();
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
    }
}