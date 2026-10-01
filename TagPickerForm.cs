
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

        List<CheckBox> selectedCharacterTags = new List<CheckBox>();

        List<CheckBox> selectedFixedTags = new List<CheckBox>();

        Dictionary<char, Control> alphabetTargets =
    new Dictionary<char, Control>();

        // آیتم انتخاب‌شده از لیست بازی / انیمه
        TagItem selectedTagItem = null;


        public TagPickerForm()
        {
            InitializeComponent();

            // تنظیمات NotifyIcon
            var exitMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            exitMenuItem.Text = "Exit";
            exitMenuItem.Click += new System.EventHandler(this.ExitApp);

            this.trayContextMenu.Items.Clear();
            this.trayContextMenu.Items.Add(exitMenuItem);

            this.notifyIcon.ContextMenuStrip = this.trayContextMenu;
            this.notifyIcon.Icon =
                System.Drawing.Icon.ExtractAssociatedIcon(Application.ExecutablePath);

            this.notifyIcon.Text = "Tag Picker";
            this.notifyIcon.Visible = true;
            this.notifyIcon.DoubleClick +=
                new System.EventHandler(this.ShowForm);

            SetColors();
            LoadTags();
        }


        // نمایش فرم
        private void ShowForm(object sender, EventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
            this.BringToFront();
        }


        // خروج کامل
        private void ExitApp(object sender, EventArgs e)
        {
            notifyIcon.Visible = false;
            Application.Exit();
        }


        // جلوگیری از بسته شدن فرم با X
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;

                this.Hide();

                notifyIcon.ShowBalloonTip(
                    1000,
                    "Tag Picker",
                    "برنامه در سینی سیستم ادامه داد...",
                    ToolTipIcon.Info
                );
            }
            else
            {
                base.OnFormClosing(e);
            }
        }


        // =========================
        // Load Tags
        // =========================

        void LoadTags()
        {
            string file = Paths.HashtagsFile;

            if (!File.Exists(file))
            {
                MessageBox.Show("hashtags.json پیدا نشد.");
                return;
            }

            string json = File.ReadAllText(file);

            allTags =
                JsonConvert.DeserializeObject<List<TagItem>>(json)
                ?? new List<TagItem>();

            LoadMainTags();
            LoadFixedTags();

            // لیست شخصیت‌ها در شروع خالی باشد
            flowCharacters.Controls.Clear();
            selectedCharacterTags.Clear();

            selectedTagItem = null;
        }


        // =========================
        // Main Tags
        // =========================
        // همه Game + Anime + Other
        // در یک لیست
        // =========================

        void LoadMainTags()
        {
            flowCategories.Controls.Clear();
            flowAlphabet.Controls.Clear();
            alphabetTargets.Clear();

            var tagGroup = allTags.FirstOrDefault(x => x.group == "Tag");
            if (tagGroup == null || tagGroup.tags == null) return;

            var mainTags = tagGroup.tags
                .OrderBy(x => x.tag, StringComparer.OrdinalIgnoreCase)
                .ToList();

            char? lastLetter = null;
            bool firstGroup = true;

            foreach (var item in mainTags)
            {
                string cleanTag = item.tag?.TrimStart('#');
                if (string.IsNullOrWhiteSpace(cleanTag)) continue;

                char firstLetter = char.ToUpperInvariant(cleanTag[0]);

                // =========================
                // شروع گروه جدید حرف
                // =========================
                if (lastLetter != firstLetter)
                {
                    // اگه گروه قبلی وجود داشت، اول یه خط جداکننده بذار
                    if (!firstGroup)
                    {
                        Panel separator = new Panel();
                        separator.Height = 1;
                        separator.Width = flowCategories.ClientSize.Width - 20;
                        separator.BackColor = Color.FromArgb(70, 72, 78);
                        separator.Margin = new Padding(5, 5, 5, 5);

                        flowCategories.Controls.Add(separator);
                    }

                    firstGroup = false;

                    // =========================
                    // Label حرف
                    // =========================
                    Label letterLabel = new Label();
                    letterLabel.Text = firstLetter.ToString();
                    letterLabel.AutoSize = true;
                    letterLabel.Font = new Font(
                        this.Font.FontFamily, 16, FontStyle.Bold);
                    letterLabel.ForeColor = Color.FromArgb(180, 200, 255);
                    letterLabel.BackColor = Color.Transparent;
                    letterLabel.Margin = new Padding(5, 10, 5, 5);

                    alphabetTargets[firstLetter] = letterLabel;
                    flowCategories.Controls.Add(letterLabel);

                    // =========================
                    // دکمه حروف الفبا
                    // =========================
                    Button btnLetter = new Button();
                    btnLetter.Text = firstLetter.ToString();
                    btnLetter.Width = 30;
                    btnLetter.Height = 28;
                    btnLetter.Margin = new Padding(2);
                    btnLetter.BackColor = Color.FromArgb(49, 51, 56);
                    btnLetter.ForeColor = Color.White;
                    btnLetter.FlatStyle = FlatStyle.Flat;
                    btnLetter.FlatAppearance.BorderSize = 0;
                    btnLetter.Cursor = Cursors.Hand;
                    btnLetter.Tag = letterLabel;
                    btnLetter.Click += AlphabetButton_Click;
                    flowAlphabet.Controls.Add(btnLetter);

                    lastLetter = firstLetter;
                }

                // =========================
                // دکمه تگ
                // =========================
                Button btn = new Button();
                btn.Text = item.tag;
                btn.AutoSize = true;
                btn.Padding = new Padding(10, 5, 10, 5);
                btn.Tag = item;
                btn.Click += MainTag_Click;
                btn.BackColor = Color.FromArgb(49, 51, 56);
                btn.ForeColor = Color.White;
                btn.FlatStyle = FlatStyle.Flat;
                btn.FlatAppearance.BorderSize = 0;
                btn.Margin = new Padding(3);

                flowCategories.Controls.Add(btn);
            }

            flowAlphabet.PerformLayout();
            flowCategories.PerformLayout();
            flowAlphabet.Refresh();
            flowCategories.Refresh();
        }

        private void AlphabetButton_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;

            Control target =
                btn.Tag as Control;

            if (target == null)
                return;

            flowCategories.ScrollControlIntoView(target);
        }

        // =========================
        // Main Tag Click
        // =========================

        private void MainTag_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;

            TagItem item = (TagItem)btn.Tag;

            selectedTagItem = item;

            LoadCharacters(item);
        }


        // =========================
        // Characters
        // =========================

void LoadCharacters(TagItem item)
        {
            flowCharacters.Controls.Clear();

            selectedCharacterTags.Clear();

            if (item == null ||
                item.characters == null)
                return;


            var characters = item.characters
                .OrderBy(x => x.tag, StringComparer.OrdinalIgnoreCase)
                .ToList();

            char? lastLetter = null;


            foreach (var character in characters)
            {
                string cleanTag =
                    character.tag?.TrimStart('#');

                if (string.IsNullOrWhiteSpace(cleanTag))
                    continue;


                char firstLetter =
                    char.ToUpperInvariant(cleanTag[0]);


                // =========================
                // Alphabet Letter
                // =========================

                if (lastLetter != firstLetter)
                {
                    Label letterLabel =
                        new Label();

                    letterLabel.Text =
                        firstLetter.ToString();

                    letterLabel.AutoSize = true;

                    letterLabel.Font =
                        new Font(
                            this.Font.FontFamily,
                            14,
                            FontStyle.Bold);

                    letterLabel.ForeColor =
                        Color.White;

                    letterLabel.BackColor =
                        Color.Transparent;

                    letterLabel.Margin =
                        new Padding(5, 12, 5, 3);

                    flowCharacters.Controls.Add(letterLabel);


                    lastLetter = firstLetter;
                }


                // =========================
                // Character CheckBox
                // =========================

                CheckBox cb =
                    new CheckBox();

                cb.Text =
                    character.tag;

                cb.Tag =
                    character.tag;

                cb.AutoSize = true;

                cb.ForeColor =
                    Color.White;

                cb.BackColor =
                    Color.Transparent;


                flowCharacters.Controls.Add(cb);

                selectedCharacterTags.Add(cb);
            }


            flowCharacters.PerformLayout();
            flowCharacters.Refresh();
        }



        // =========================
        // Fixed Tags
        // =========================

        void LoadFixedTags()
        {
            flowFixedTags.Controls.Clear();
            selectedFixedTags.Clear();

            var general = allTags
                .FirstOrDefault(x => x.group == "Fixed");

            // ✅ Correct property: 'tags', not 'Fixedtags'
            if (general == null || general.tags == null)
                return;

            foreach (var item in general.tags
                .OrderBy(x => x.tag, StringComparer.OrdinalIgnoreCase))
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

            flowFixedTags.PerformLayout();
            flowFixedTags.Refresh();
        }


        // =========================
        // Insert
        // =========================

        private void button1_Click(object sender, EventArgs e)
        {
            string result = "";


            // =========================
            // Main Tag
            // =========================

            if (selectedTagItem != null)
            {
                result += selectedTagItem.tag + " ";
            }


            // =========================
            // Characters
            // =========================

            foreach (CheckBox cb in selectedCharacterTags)
            {
                if (cb.Checked)
                {
                    result += cb.Tag.ToString() + " ";
                }
            }


            // =========================
            // Fixed Tags
            // =========================

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


        // =========================
        // Reset
        // =========================

        private void ResetForm()
        {
            // برداشتن تیک شخصیت‌ها
            foreach (CheckBox cb in selectedCharacterTags)
            {
                cb.Checked = false;
            }


            // برداشتن تیک Fixed Tags
            foreach (CheckBox cb in selectedFixedTags)
            {
                cb.Checked = false;
            }


            // حذف شخصیت‌ها
            selectedCharacterTags.Clear();

            flowCharacters.Controls.Clear();


            // انتخاب اصلی هم پاک شود
            selectedTagItem = null;
        }


        // =========================
        // Add Tag
        // =========================

        private void btnAddTag_Click(object sender, EventArgs e)
        {
            AddTagForm form = new AddTagForm();

            form.ShowDialog();

            LoadTags();
        }


        // =========================
        // Manage Tags
        // =========================

        private void btnManageTags_Click(object sender, EventArgs e)
        {
            ManageTagsForm form = new ManageTagsForm();

            form.ShowDialog();

            LoadTags();
        }


        // =========================
        // Colors
        // =========================

        void SetColors()
        {
            this.BackColor = Color.FromArgb(30, 31, 34);
            flowAlphabet.BackColor =
    Color.FromArgb(43, 45, 49);
            flowCategories.BackColor =
                Color.FromArgb(43, 45, 49);

            flowCharacters.BackColor =
                Color.FromArgb(43, 45, 49);

            flowFixedTags.BackColor =
                Color.FromArgb(43, 45, 49);


            SetButtonColors(flowCategories);


            btnInsert.BackColor =
                Color.FromArgb(88, 101, 242);

            btnInsert.ForeColor = Color.White;


            btnAddTag.BackColor =
                Color.FromArgb(87, 242, 135);

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
                    btn.BackColor =
                        Color.FromArgb(49, 51, 56);

                    btn.ForeColor = Color.White;

                    btn.FlatStyle =
                        FlatStyle.Flat;

                    btn.FlatAppearance.BorderSize = 0;
                }
            }
        }


        // =========================
        // Window Style
        // =========================

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;

                cp.ExStyle &= ~0x00000080;
                cp.ExStyle |= 0x00040000;

                return cp;
            }
        }
    }
}
