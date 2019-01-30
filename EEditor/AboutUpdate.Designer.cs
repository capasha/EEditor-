namespace EEditor
{
    partial class AboutUpdate
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
            this.currentVersionLabel = new System.Windows.Forms.Label();
            this.newVersionLabel = new System.Windows.Forms.Label();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.versionGroupBox = new System.Windows.Forms.GroupBox();
            this.changelogGroupBox = new System.Windows.Forms.GroupBox();
            this.DownloadButton = new System.Windows.Forms.Button();
            this.versionGroupBox.SuspendLayout();
            this.changelogGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // currentVersionLabel
            // 
            this.currentVersionLabel.AutoSize = true;
            this.currentVersionLabel.Location = new System.Drawing.Point(6, 24);
            this.currentVersionLabel.Name = "currentVersionLabel";
            this.currentVersionLabel.Size = new System.Drawing.Size(80, 13);
            this.currentVersionLabel.TabIndex = 0;
            this.currentVersionLabel.Text = "Current: 1.3.2.0";
            // 
            // newVersionLabel
            // 
            this.newVersionLabel.AutoSize = true;
            this.newVersionLabel.Location = new System.Drawing.Point(129, 24);
            this.newVersionLabel.Name = "newVersionLabel";
            this.newVersionLabel.Size = new System.Drawing.Size(82, 13);
            this.newVersionLabel.TabIndex = 1;
            this.newVersionLabel.Text = "Newest: 1.3.3.0";
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(12, 19);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.Size = new System.Drawing.Size(310, 182);
            this.richTextBox1.TabIndex = 2;
            this.richTextBox1.Text = "";
            // 
            // versionGroupBox
            // 
            this.versionGroupBox.Controls.Add(this.currentVersionLabel);
            this.versionGroupBox.Controls.Add(this.newVersionLabel);
            this.versionGroupBox.Location = new System.Drawing.Point(12, 12);
            this.versionGroupBox.Name = "versionGroupBox";
            this.versionGroupBox.Size = new System.Drawing.Size(334, 49);
            this.versionGroupBox.TabIndex = 3;
            this.versionGroupBox.TabStop = false;
            this.versionGroupBox.Text = "Version";
            // 
            // changelogGroupBox
            // 
            this.changelogGroupBox.Controls.Add(this.richTextBox1);
            this.changelogGroupBox.Location = new System.Drawing.Point(12, 67);
            this.changelogGroupBox.Name = "changelogGroupBox";
            this.changelogGroupBox.Size = new System.Drawing.Size(334, 215);
            this.changelogGroupBox.TabIndex = 4;
            this.changelogGroupBox.TabStop = false;
            this.changelogGroupBox.Text = "Changelog";
            // 
            // DownloadButton
            // 
            this.DownloadButton.Location = new System.Drawing.Point(12, 288);
            this.DownloadButton.Name = "DownloadButton";
            this.DownloadButton.Size = new System.Drawing.Size(75, 23);
            this.DownloadButton.TabIndex = 5;
            this.DownloadButton.Text = "Download";
            this.DownloadButton.UseVisualStyleBackColor = true;
            this.DownloadButton.Click += new System.EventHandler(this.DownloadButton_Click);
            // 
            // AboutUpdate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(361, 319);
            this.Controls.Add(this.DownloadButton);
            this.Controls.Add(this.changelogGroupBox);
            this.Controls.Add(this.versionGroupBox);
            this.Name = "AboutUpdate";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Updater";
            this.Load += new System.EventHandler(this.AboutUpdate_Load);
            this.versionGroupBox.ResumeLayout(false);
            this.versionGroupBox.PerformLayout();
            this.changelogGroupBox.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label currentVersionLabel;
        private System.Windows.Forms.Label newVersionLabel;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.GroupBox versionGroupBox;
        private System.Windows.Forms.GroupBox changelogGroupBox;
        private System.Windows.Forms.Button DownloadButton;
    }
}