namespace TelegramTags
{
    partial class TagPickerForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TagPickerForm));
            this.flowFixedTags = new System.Windows.Forms.FlowLayoutPanel();
            this.btnInsert = new System.Windows.Forms.Button();
            this.flowCategories = new System.Windows.Forms.FlowLayoutPanel();
            this.flowCharacters = new System.Windows.Forms.FlowLayoutPanel();
            this.btnAddTag = new System.Windows.Forms.Button();
            this.btnManageTags = new System.Windows.Forms.Button();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.trayContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.flowAlphabet = new System.Windows.Forms.FlowLayoutPanel();
            this.SuspendLayout();
            // 
            // flowFixedTags
            // 
            this.flowFixedTags.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.flowFixedTags.Location = new System.Drawing.Point(0, 621);
            this.flowFixedTags.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.flowFixedTags.Name = "flowFixedTags";
            this.flowFixedTags.Size = new System.Drawing.Size(734, 55);
            this.flowFixedTags.TabIndex = 0;
            // 
            // btnInsert
            // 
            this.btnInsert.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnInsert.Font = new System.Drawing.Font("Microsoft YaHei", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnInsert.Location = new System.Drawing.Point(0, 766);
            this.btnInsert.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnInsert.Name = "btnInsert";
            this.btnInsert.Size = new System.Drawing.Size(734, 45);
            this.btnInsert.TabIndex = 0;
            this.btnInsert.Text = "Insert";
            this.btnInsert.UseVisualStyleBackColor = true;
            this.btnInsert.Click += new System.EventHandler(this.button1_Click);
            // 
            // flowCategories
            // 
            this.flowCategories.AutoScroll = true;
            this.flowCategories.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowCategories.Location = new System.Drawing.Point(0, 35);
            this.flowCategories.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.flowCategories.Name = "flowCategories";
            this.flowCategories.Size = new System.Drawing.Size(734, 423);
            this.flowCategories.TabIndex = 2;
            // 
            // flowCharacters
            // 
            this.flowCharacters.AutoScroll = true;
            this.flowCharacters.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.flowCharacters.Location = new System.Drawing.Point(0, 458);
            this.flowCharacters.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.flowCharacters.Name = "flowCharacters";
            this.flowCharacters.Size = new System.Drawing.Size(734, 163);
            this.flowCharacters.TabIndex = 3;
            // 
            // btnAddTag
            // 
            this.btnAddTag.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnAddTag.Font = new System.Drawing.Font("Microsoft YaHei", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddTag.Location = new System.Drawing.Point(0, 721);
            this.btnAddTag.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnAddTag.Name = "btnAddTag";
            this.btnAddTag.Size = new System.Drawing.Size(734, 45);
            this.btnAddTag.TabIndex = 1;
            this.btnAddTag.Text = "NewTag";
            this.btnAddTag.UseVisualStyleBackColor = true;
            this.btnAddTag.Click += new System.EventHandler(this.btnAddTag_Click);
            // 
            // btnManageTags
            // 
            this.btnManageTags.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnManageTags.Font = new System.Drawing.Font("Microsoft YaHei", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnManageTags.Location = new System.Drawing.Point(0, 676);
            this.btnManageTags.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnManageTags.Name = "btnManageTags";
            this.btnManageTags.Size = new System.Drawing.Size(734, 45);
            this.btnManageTags.TabIndex = 4;
            this.btnManageTags.Text = "ManageTags";
            this.btnManageTags.UseVisualStyleBackColor = true;
            this.btnManageTags.Click += new System.EventHandler(this.btnManageTags_Click);
            // 
            // notifyIcon
            // 
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "notifyIcon1";
            this.notifyIcon.Visible = true;
            // 
            // trayContextMenu
            // 
            this.trayContextMenu.Name = "trayContextMenu";
            this.trayContextMenu.Size = new System.Drawing.Size(61, 4);
            // 
            // flowAlphabet
            // 
            this.flowAlphabet.Dock = System.Windows.Forms.DockStyle.Top;
            this.flowAlphabet.Location = new System.Drawing.Point(0, 0);
            this.flowAlphabet.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.flowAlphabet.Name = "flowAlphabet";
            this.flowAlphabet.Size = new System.Drawing.Size(734, 35);
            this.flowAlphabet.TabIndex = 3;
            // 
            // TagPickerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(734, 811);
            this.Controls.Add(this.flowCategories);
            this.Controls.Add(this.flowCharacters);
            this.Controls.Add(this.flowFixedTags);
            this.Controls.Add(this.btnManageTags);
            this.Controls.Add(this.btnAddTag);
            this.Controls.Add(this.btnInsert);
            this.Controls.Add(this.flowAlphabet);
            this.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "TagPickerForm";
            this.Text = "TagPickerForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowFixedTags;
        private System.Windows.Forms.Button btnInsert;
        private System.Windows.Forms.FlowLayoutPanel flowCategories;
        private System.Windows.Forms.FlowLayoutPanel flowCharacters;
        private System.Windows.Forms.Button btnAddTag;
        private System.Windows.Forms.Button btnManageTags;
        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.ContextMenuStrip trayContextMenu;
        private System.Windows.Forms.FlowLayoutPanel flowAlphabet;
    }
}