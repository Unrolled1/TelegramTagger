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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TagPickerForm));
            this.flowFixedTags = new System.Windows.Forms.FlowLayoutPanel();
            this.btnInsert = new System.Windows.Forms.Button();
            this.flowGroups = new System.Windows.Forms.FlowLayoutPanel();
            this.flowCategories = new System.Windows.Forms.FlowLayoutPanel();
            this.flowCharacters = new System.Windows.Forms.FlowLayoutPanel();
            this.btnAddTag = new System.Windows.Forms.Button();
            this.btnManageTags = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // flowFixedTags
            // 
            this.flowFixedTags.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.flowFixedTags.Location = new System.Drawing.Point(0, 530);
            this.flowFixedTags.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.flowFixedTags.Name = "flowFixedTags";
            this.flowFixedTags.Size = new System.Drawing.Size(709, 55);
            this.flowFixedTags.TabIndex = 0;
            // 
            // btnInsert
            // 
            this.btnInsert.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnInsert.Font = new System.Drawing.Font("Microsoft YaHei", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnInsert.Location = new System.Drawing.Point(0, 695);
            this.btnInsert.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnInsert.Name = "btnInsert";
            this.btnInsert.Size = new System.Drawing.Size(709, 55);
            this.btnInsert.TabIndex = 0;
            this.btnInsert.Text = "Insert";
            this.btnInsert.UseVisualStyleBackColor = true;
            this.btnInsert.Click += new System.EventHandler(this.button1_Click);
            // 
            // flowGroups
            // 
            this.flowGroups.Dock = System.Windows.Forms.DockStyle.Top;
            this.flowGroups.Location = new System.Drawing.Point(0, 0);
            this.flowGroups.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.flowGroups.Name = "flowGroups";
            this.flowGroups.Size = new System.Drawing.Size(709, 57);
            this.flowGroups.TabIndex = 1;
            // 
            // flowCategories
            // 
            this.flowCategories.AutoScroll = true;
            this.flowCategories.Dock = System.Windows.Forms.DockStyle.Top;
            this.flowCategories.Location = new System.Drawing.Point(0, 57);
            this.flowCategories.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.flowCategories.Name = "flowCategories";
            this.flowCategories.Size = new System.Drawing.Size(709, 324);
            this.flowCategories.TabIndex = 2;
            // 
            // flowCharacters
            // 
            this.flowCharacters.AutoScroll = true;
            this.flowCharacters.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowCharacters.Location = new System.Drawing.Point(0, 381);
            this.flowCharacters.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.flowCharacters.Name = "flowCharacters";
            this.flowCharacters.Size = new System.Drawing.Size(709, 149);
            this.flowCharacters.TabIndex = 3;
            // 
            // btnAddTag
            // 
            this.btnAddTag.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnAddTag.Font = new System.Drawing.Font("Microsoft YaHei", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddTag.Location = new System.Drawing.Point(0, 640);
            this.btnAddTag.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnAddTag.Name = "btnAddTag";
            this.btnAddTag.Size = new System.Drawing.Size(709, 55);
            this.btnAddTag.TabIndex = 1;
            this.btnAddTag.Text = "NewTag";
            this.btnAddTag.UseVisualStyleBackColor = true;
            this.btnAddTag.Click += new System.EventHandler(this.btnAddTag_Click);
            // 
            // btnManageTags
            // 
            this.btnManageTags.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnManageTags.Font = new System.Drawing.Font("Microsoft YaHei", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnManageTags.Location = new System.Drawing.Point(0, 585);
            this.btnManageTags.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnManageTags.Name = "btnManageTags";
            this.btnManageTags.Size = new System.Drawing.Size(709, 55);
            this.btnManageTags.TabIndex = 4;
            this.btnManageTags.Text = "ManageTags";
            this.btnManageTags.UseVisualStyleBackColor = true;
            this.btnManageTags.Click += new System.EventHandler(this.btnManageTags_Click);
            // 
            // TagPickerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(709, 750);
            this.Controls.Add(this.flowCharacters);
            this.Controls.Add(this.flowCategories);
            this.Controls.Add(this.flowGroups);
            this.Controls.Add(this.flowFixedTags);
            this.Controls.Add(this.btnManageTags);
            this.Controls.Add(this.btnAddTag);
            this.Controls.Add(this.btnInsert);
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
        private System.Windows.Forms.FlowLayoutPanel flowGroups;
        private System.Windows.Forms.FlowLayoutPanel flowCategories;
        private System.Windows.Forms.FlowLayoutPanel flowCharacters;
        private System.Windows.Forms.Button btnAddTag;
        private System.Windows.Forms.Button btnManageTags;
    }
}