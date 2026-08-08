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
            this.flowFixedTags = new System.Windows.Forms.FlowLayoutPanel();
            this.btnInsert = new System.Windows.Forms.Button();
            this.flowGroups = new System.Windows.Forms.FlowLayoutPanel();
            this.flowCategories = new System.Windows.Forms.FlowLayoutPanel();
            this.flowCharacters = new System.Windows.Forms.FlowLayoutPanel();
            this.SuspendLayout();
            // 
            // flowFixedTags
            // 
            this.flowFixedTags.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.flowFixedTags.Location = new System.Drawing.Point(0, 290);
            this.flowFixedTags.Name = "flowFixedTags";
            this.flowFixedTags.Size = new System.Drawing.Size(483, 37);
            this.flowFixedTags.TabIndex = 0;
            // 
            // btnInsert
            // 
            this.btnInsert.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnInsert.Font = new System.Drawing.Font("Microsoft YaHei", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnInsert.Location = new System.Drawing.Point(0, 327);
            this.btnInsert.Name = "btnInsert";
            this.btnInsert.Size = new System.Drawing.Size(483, 37);
            this.btnInsert.TabIndex = 0;
            this.btnInsert.Text = "Insert";
            this.btnInsert.UseVisualStyleBackColor = true;
            this.btnInsert.Click += new System.EventHandler(this.button1_Click);
            // 
            // flowGroups
            // 
            this.flowGroups.Dock = System.Windows.Forms.DockStyle.Top;
            this.flowGroups.Location = new System.Drawing.Point(0, 0);
            this.flowGroups.Name = "flowGroups";
            this.flowGroups.Size = new System.Drawing.Size(483, 39);
            this.flowGroups.TabIndex = 1;
            // 
            // flowCategories
            // 
            this.flowCategories.AutoScroll = true;
            this.flowCategories.Dock = System.Windows.Forms.DockStyle.Top;
            this.flowCategories.Location = new System.Drawing.Point(0, 39);
            this.flowCategories.Name = "flowCategories";
            this.flowCategories.Size = new System.Drawing.Size(483, 151);
            this.flowCategories.TabIndex = 2;
            // 
            // flowCharacters
            // 
            this.flowCharacters.AutoScroll = true;
            this.flowCharacters.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowCharacters.Location = new System.Drawing.Point(0, 190);
            this.flowCharacters.Name = "flowCharacters";
            this.flowCharacters.Size = new System.Drawing.Size(483, 100);
            this.flowCharacters.TabIndex = 3;
            // 
            // TagPickerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(483, 364);
            this.Controls.Add(this.flowCharacters);
            this.Controls.Add(this.flowCategories);
            this.Controls.Add(this.flowGroups);
            this.Controls.Add(this.flowFixedTags);
            this.Controls.Add(this.btnInsert);
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
    }
}