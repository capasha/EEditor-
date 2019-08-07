namespace EEditor
{
    partial class SettingsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsForm));
            this.usePenToolCheckBox = new System.Windows.Forms.CheckBox();
            this.selectAllBorderCheckBox = new System.Windows.Forms.CheckBox();
            this.clearComboBox = new System.Windows.Forms.ComboBox();
            this.clearButton = new System.Windows.Forms.Button();
            this.confirmCloseCheckBox = new System.Windows.Forms.CheckBox();
            this.FasterShapeStyleCheckBox = new System.Windows.Forms.CheckBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.StatusTextToolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.StatusToolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.StatusColorToolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // usePenToolCheckBox
            // 
            this.usePenToolCheckBox.AutoSize = true;
            this.usePenToolCheckBox.Location = new System.Drawing.Point(11, 15);
            this.usePenToolCheckBox.Margin = new System.Windows.Forms.Padding(2);
            this.usePenToolCheckBox.Name = "usePenToolCheckBox";
            this.usePenToolCheckBox.Size = new System.Drawing.Size(157, 17);
            this.usePenToolCheckBox.TabIndex = 8;
            this.usePenToolCheckBox.Text = "Draw tool after block switch";
            this.usePenToolCheckBox.UseVisualStyleBackColor = true;
            this.usePenToolCheckBox.CheckedChanged += new System.EventHandler(this.usePenToolCheckBox_CheckedChanged);
            // 
            // selectAllBorderCheckBox
            // 
            this.selectAllBorderCheckBox.AutoSize = true;
            this.selectAllBorderCheckBox.Location = new System.Drawing.Point(11, 36);
            this.selectAllBorderCheckBox.Margin = new System.Windows.Forms.Padding(2);
            this.selectAllBorderCheckBox.Name = "selectAllBorderCheckBox";
            this.selectAllBorderCheckBox.Size = new System.Drawing.Size(165, 17);
            this.selectAllBorderCheckBox.TabIndex = 18;
            this.selectAllBorderCheckBox.Text = "Include borders with select all";
            this.selectAllBorderCheckBox.UseVisualStyleBackColor = true;
            this.selectAllBorderCheckBox.CheckedChanged += new System.EventHandler(this.selectAllBorderCheckBox_CheckedChanged);
            // 
            // clearComboBox
            // 
            this.clearComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.clearComboBox.Items.AddRange(new object[] {
            "Clear settings...",
            "",
            "Block hotkeys (0-9)",
            "Blocks in unknown tab",
            "",
            "Old EEditor settings & logins",
            "Current EEditor settings"});
            this.clearComboBox.Location = new System.Drawing.Point(11, 117);
            this.clearComboBox.Name = "clearComboBox";
            this.clearComboBox.Size = new System.Drawing.Size(139, 21);
            this.clearComboBox.TabIndex = 27;
            this.clearComboBox.SelectedIndexChanged += new System.EventHandler(this.clearComboBox_SelectedIndexChanged);
            // 
            // clearButton
            // 
            this.clearButton.ForeColor = System.Drawing.SystemColors.ControlText;
            this.clearButton.Location = new System.Drawing.Point(156, 117);
            this.clearButton.Name = "clearButton";
            this.clearButton.Size = new System.Drawing.Size(49, 23);
            this.clearButton.TabIndex = 28;
            this.clearButton.Text = "Clear";
            this.clearButton.UseVisualStyleBackColor = true;
            this.clearButton.Click += new System.EventHandler(this.clearButton_Click);
            // 
            // confirmCloseCheckBox
            // 
            this.confirmCloseCheckBox.AutoSize = true;
            this.confirmCloseCheckBox.Location = new System.Drawing.Point(11, 57);
            this.confirmCloseCheckBox.Margin = new System.Windows.Forms.Padding(2);
            this.confirmCloseCheckBox.Name = "confirmCloseCheckBox";
            this.confirmCloseCheckBox.Size = new System.Drawing.Size(117, 17);
            this.confirmCloseCheckBox.TabIndex = 29;
            this.confirmCloseCheckBox.Text = "Confirm EEditor exit";
            this.confirmCloseCheckBox.UseVisualStyleBackColor = true;
            this.confirmCloseCheckBox.CheckedChanged += new System.EventHandler(this.confirmCloseCheckBox_CheckedChanged);
            // 
            // FasterShapeStyleCheckBox
            // 
            this.FasterShapeStyleCheckBox.AutoSize = true;
            this.FasterShapeStyleCheckBox.Enabled = false;
            this.FasterShapeStyleCheckBox.Location = new System.Drawing.Point(11, 79);
            this.FasterShapeStyleCheckBox.Name = "FasterShapeStyleCheckBox";
            this.FasterShapeStyleCheckBox.Size = new System.Drawing.Size(111, 17);
            this.FasterShapeStyleCheckBox.TabIndex = 30;
            this.FasterShapeStyleCheckBox.Text = "Faster shape style";
            this.FasterShapeStyleCheckBox.UseVisualStyleBackColor = true;
            this.FasterShapeStyleCheckBox.Visible = false;
            this.FasterShapeStyleCheckBox.CheckedChanged += new System.EventHandler(this.FasterShapeStyleCheckBox_CheckedChanged);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StatusTextToolStripStatusLabel,
            this.StatusToolStripStatusLabel,
            this.StatusColorToolStripStatusLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 145);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(228, 22);
            this.statusStrip1.TabIndex = 31;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // StatusTextToolStripStatusLabel
            // 
            this.StatusTextToolStripStatusLabel.Name = "StatusTextToolStripStatusLabel";
            this.StatusTextToolStripStatusLabel.Size = new System.Drawing.Size(42, 17);
            this.StatusTextToolStripStatusLabel.Text = "Status:";
            this.StatusTextToolStripStatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // StatusToolStripStatusLabel
            // 
            this.StatusToolStripStatusLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.StatusToolStripStatusLabel.Name = "StatusToolStripStatusLabel";
            this.StatusToolStripStatusLabel.Size = new System.Drawing.Size(0, 17);
            // 
            // StatusColorToolStripStatusLabel
            // 
            this.StatusColorToolStripStatusLabel.BackColor = System.Drawing.SystemColors.Control;
            this.StatusColorToolStripStatusLabel.ForeColor = System.Drawing.SystemColors.Control;
            this.StatusColorToolStripStatusLabel.Name = "StatusColorToolStripStatusLabel";
            this.StatusColorToolStripStatusLabel.Size = new System.Drawing.Size(0, 17);
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(228, 167);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.FasterShapeStyleCheckBox);
            this.Controls.Add(this.usePenToolCheckBox);
            this.Controls.Add(this.selectAllBorderCheckBox);
            this.Controls.Add(this.confirmCloseCheckBox);
            this.Controls.Add(this.clearComboBox);
            this.Controls.Add(this.clearButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SettingsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "EEditor Settings";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SettingsForm_FormClosing);
            this.Load += new System.EventHandler(this.SettingsForm_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.CheckBox usePenToolCheckBox;
        private System.Windows.Forms.CheckBox selectAllBorderCheckBox;
        private System.Windows.Forms.ComboBox clearComboBox;
        private System.Windows.Forms.Button clearButton;
        private System.Windows.Forms.CheckBox confirmCloseCheckBox;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel StatusTextToolStripStatusLabel;
        private System.Windows.Forms.ToolStripStatusLabel StatusToolStripStatusLabel;
        private System.Windows.Forms.ToolStripStatusLabel StatusColorToolStripStatusLabel;
        private System.Windows.Forms.CheckBox FasterShapeStyleCheckBox;
    }
}