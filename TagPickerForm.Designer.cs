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
            this.flowTags = new System.Windows.Forms.FlowLayoutPanel();
            this.btnInsert = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // flowTags
            // 
            this.flowTags.AutoScroll = true;
            this.flowTags.Dock = System.Windows.Forms.DockStyle.Top;
            this.flowTags.Location = new System.Drawing.Point(0, 0);
            this.flowTags.Name = "flowTags";
            this.flowTags.Size = new System.Drawing.Size(270, 100);
            this.flowTags.TabIndex = 0;
            // 
            // btnInsert
            // 
            this.btnInsert.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnInsert.Location = new System.Drawing.Point(0, 200);
            this.btnInsert.Name = "btnInsert";
            this.btnInsert.Size = new System.Drawing.Size(270, 23);
            this.btnInsert.TabIndex = 0;
            this.btnInsert.Text = "Insert";
            this.btnInsert.UseVisualStyleBackColor = true;
            this.btnInsert.Click += new System.EventHandler(this.button1_Click);
            // 
            // TagPickerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(270, 223);
            this.Controls.Add(this.btnInsert);
            this.Controls.Add(this.flowTags);
            this.Name = "TagPickerForm";
            this.Text = "TagPickerForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowTags;
        private System.Windows.Forms.Button btnInsert;
    }
}