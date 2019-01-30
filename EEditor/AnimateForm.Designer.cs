namespace EEditor
{
    partial class AnimateForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.TimeRunningTextBox = new System.Windows.Forms.TextBox();
            this.TimeRunningLabel = new System.Windows.Forms.Label();
            this.uploadProgressBar = new System.Windows.Forms.ProgressBar();
            this.button1 = new System.Windows.Forms.Button();
            this.levelTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.levelPassTextBox = new System.Windows.Forms.TextBox();
            this.shuffleCheckBox = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.numericUpDown2 = new System.Windows.Forms.NumericUpDown();
            this.autoSaveCheckBox = new System.Windows.Forms.CheckBox();
            this.uploadOptionButton1 = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.BPSNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.BPSLabel = new System.Windows.Forms.Label();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.BPSCheckBox = new System.Windows.Forms.CheckBox();
            this.checkBoxReverse = new System.Windows.Forms.CheckBox();
            this.checkBoxRandom = new System.Windows.Forms.CheckBox();
            this.IgnoreDrawingCheckBox = new System.Windows.Forms.CheckBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BPSNumericUpDown)).BeginInit();
            this.groupBox5.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(171, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Idle. Click Start to upload the level.";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.TimeRunningTextBox);
            this.groupBox1.Controls.Add(this.TimeRunningLabel);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.uploadProgressBar);
            this.groupBox1.Location = new System.Drawing.Point(8, 351);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(318, 93);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Status";
            // 
            // TimeRunningTextBox
            // 
            this.TimeRunningTextBox.BackColor = System.Drawing.SystemColors.Control;
            this.TimeRunningTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TimeRunningTextBox.Location = new System.Drawing.Point(89, 63);
            this.TimeRunningTextBox.Name = "TimeRunningTextBox";
            this.TimeRunningTextBox.Size = new System.Drawing.Size(107, 20);
            this.TimeRunningTextBox.TabIndex = 26;
            // 
            // TimeRunningLabel
            // 
            this.TimeRunningLabel.AutoSize = true;
            this.TimeRunningLabel.Location = new System.Drawing.Point(6, 65);
            this.TimeRunningLabel.Name = "TimeRunningLabel";
            this.TimeRunningLabel.Size = new System.Drawing.Size(79, 13);
            this.TimeRunningLabel.TabIndex = 25;
            this.TimeRunningLabel.Text = "Time Running: ";
            // 
            // uploadProgressBar
            // 
            this.uploadProgressBar.Location = new System.Drawing.Point(6, 32);
            this.uploadProgressBar.Name = "uploadProgressBar";
            this.uploadProgressBar.Size = new System.Drawing.Size(300, 21);
            this.uploadProgressBar.TabIndex = 24;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(222, 5);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(110, 49);
            this.button1.TabIndex = 2;
            this.button1.Text = "Start";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // levelTextBox
            // 
            this.levelTextBox.Location = new System.Drawing.Point(78, 6);
            this.levelTextBox.Name = "levelTextBox";
            this.levelTextBox.Size = new System.Drawing.Size(127, 20);
            this.levelTextBox.TabIndex = 5;
            this.levelTextBox.TextChanged += new System.EventHandler(this.levelTextBox_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(5, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Level ID";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(5, 35);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(60, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Level code";
            // 
            // levelPassTextBox
            // 
            this.levelPassTextBox.Location = new System.Drawing.Point(78, 35);
            this.levelPassTextBox.Name = "levelPassTextBox";
            this.levelPassTextBox.Size = new System.Drawing.Size(127, 20);
            this.levelPassTextBox.TabIndex = 10;
            this.levelPassTextBox.TextChanged += new System.EventHandler(this.levelPassTextBox_TextChanged);
            // 
            // shuffleCheckBox
            // 
            this.shuffleCheckBox.AutoSize = true;
            this.shuffleCheckBox.Location = new System.Drawing.Point(14, 51);
            this.shuffleCheckBox.Name = "shuffleCheckBox";
            this.shuffleCheckBox.Size = new System.Drawing.Size(59, 17);
            this.shuffleCheckBox.TabIndex = 11;
            this.shuffleCheckBox.Text = "Shuffle";
            this.shuffleCheckBox.UseVisualStyleBackColor = true;
            this.shuffleCheckBox.CheckedChanged += new System.EventHandler(this.shuffleCheckBox_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(128, 36);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 13);
            this.label2.TabIndex = 21;
            this.label2.Text = "Delay:";
            // 
            // numericUpDown2
            // 
            this.numericUpDown2.Location = new System.Drawing.Point(169, 34);
            this.numericUpDown2.Margin = new System.Windows.Forms.Padding(2);
            this.numericUpDown2.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this.numericUpDown2.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown2.Name = "numericUpDown2";
            this.numericUpDown2.Size = new System.Drawing.Size(78, 20);
            this.numericUpDown2.TabIndex = 22;
            this.numericUpDown2.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDown2.ValueChanged += new System.EventHandler(this.numericUpDown2_ValueChanged);
            // 
            // autoSaveCheckBox
            // 
            this.autoSaveCheckBox.AutoSize = true;
            this.autoSaveCheckBox.Location = new System.Drawing.Point(78, 61);
            this.autoSaveCheckBox.Name = "autoSaveCheckBox";
            this.autoSaveCheckBox.Size = new System.Drawing.Size(119, 17);
            this.autoSaveCheckBox.TabIndex = 26;
            this.autoSaveCheckBox.Text = "Auto-save EE world";
            this.autoSaveCheckBox.UseVisualStyleBackColor = true;
            this.autoSaveCheckBox.CheckedChanged += new System.EventHandler(this.autoSaveCheckBox_CheckedChanged);
            // 
            // uploadOptionButton1
            // 
            this.uploadOptionButton1.AutoSize = true;
            this.uploadOptionButton1.Checked = true;
            this.uploadOptionButton1.Location = new System.Drawing.Point(14, 28);
            this.uploadOptionButton1.Name = "uploadOptionButton1";
            this.uploadOptionButton1.Size = new System.Drawing.Size(61, 17);
            this.uploadOptionButton1.TabIndex = 27;
            this.uploadOptionButton1.TabStop = true;
            this.uploadOptionButton1.Text = " Normal";
            this.uploadOptionButton1.UseVisualStyleBackColor = true;
            this.uploadOptionButton1.CheckedChanged += new System.EventHandler(this.uploadOptionButton1_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.groupBox4);
            this.groupBox2.Controls.Add(this.groupBox5);
            this.groupBox2.Location = new System.Drawing.Point(8, 84);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(318, 182);
            this.groupBox2.TabIndex = 30;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Uploading";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.BPSNumericUpDown);
            this.groupBox4.Controls.Add(this.BPSLabel);
            this.groupBox4.Controls.Add(this.numericUpDown2);
            this.groupBox4.Controls.Add(this.label2);
            this.groupBox4.Location = new System.Drawing.Point(12, 19);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(294, 69);
            this.groupBox4.TabIndex = 34;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Uploading Delay";
            // 
            // BPSNumericUpDown
            // 
            this.BPSNumericUpDown.Location = new System.Drawing.Point(46, 34);
            this.BPSNumericUpDown.Margin = new System.Windows.Forms.Padding(2);
            this.BPSNumericUpDown.Maximum = new decimal(new int[] {
            999999,
            0,
            0,
            0});
            this.BPSNumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.BPSNumericUpDown.Name = "BPSNumericUpDown";
            this.BPSNumericUpDown.Size = new System.Drawing.Size(78, 20);
            this.BPSNumericUpDown.TabIndex = 24;
            this.BPSNumericUpDown.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.BPSNumericUpDown.ValueChanged += new System.EventHandler(this.BPSNumericUpDown_ValueChanged);
            this.BPSNumericUpDown.KeyUp += new System.Windows.Forms.KeyEventHandler(this.BPSNumericUpDown_KeyUp);
            // 
            // BPSLabel
            // 
            this.BPSLabel.AutoSize = true;
            this.BPSLabel.Location = new System.Drawing.Point(6, 36);
            this.BPSLabel.Name = "BPSLabel";
            this.BPSLabel.Size = new System.Drawing.Size(31, 13);
            this.BPSLabel.TabIndex = 23;
            this.BPSLabel.Text = "BPS:";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.BPSCheckBox);
            this.groupBox5.Controls.Add(this.shuffleCheckBox);
            this.groupBox5.Controls.Add(this.uploadOptionButton1);
            this.groupBox5.Controls.Add(this.checkBoxReverse);
            this.groupBox5.Controls.Add(this.checkBoxRandom);
            this.groupBox5.Location = new System.Drawing.Point(12, 94);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(294, 75);
            this.groupBox5.TabIndex = 32;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Uploading Way";
            // 
            // BPSCheckBox
            // 
            this.BPSCheckBox.AutoSize = true;
            this.BPSCheckBox.Location = new System.Drawing.Point(77, 28);
            this.BPSCheckBox.Name = "BPSCheckBox";
            this.BPSCheckBox.Size = new System.Drawing.Size(47, 17);
            this.BPSCheckBox.TabIndex = 32;
            this.BPSCheckBox.Text = "BPS";
            this.BPSCheckBox.UseVisualStyleBackColor = true;
            this.BPSCheckBox.CheckedChanged += new System.EventHandler(this.BPSCheckBox_CheckedChanged);
            // 
            // checkBoxReverse
            // 
            this.checkBoxReverse.AutoSize = true;
            this.checkBoxReverse.Location = new System.Drawing.Point(79, 51);
            this.checkBoxReverse.Name = "checkBoxReverse";
            this.checkBoxReverse.Size = new System.Drawing.Size(66, 17);
            this.checkBoxReverse.TabIndex = 30;
            this.checkBoxReverse.Text = "Reverse";
            this.checkBoxReverse.UseVisualStyleBackColor = true;
            this.checkBoxReverse.CheckedChanged += new System.EventHandler(this.checkBoxReverse_CheckedChanged);
            // 
            // checkBoxRandom
            // 
            this.checkBoxRandom.AutoSize = true;
            this.checkBoxRandom.Location = new System.Drawing.Point(151, 51);
            this.checkBoxRandom.Name = "checkBoxRandom";
            this.checkBoxRandom.Size = new System.Drawing.Size(66, 17);
            this.checkBoxRandom.TabIndex = 31;
            this.checkBoxRandom.Text = "Random";
            this.checkBoxRandom.UseVisualStyleBackColor = true;
            this.checkBoxRandom.CheckedChanged += new System.EventHandler(this.checkBoxRandom_CheckedChanged);
            // 
            // IgnoreDrawingCheckBox
            // 
            this.IgnoreDrawingCheckBox.AutoSize = true;
            this.IgnoreDrawingCheckBox.Location = new System.Drawing.Point(9, 19);
            this.IgnoreDrawingCheckBox.Name = "IgnoreDrawingCheckBox";
            this.IgnoreDrawingCheckBox.Size = new System.Drawing.Size(98, 17);
            this.IgnoreDrawingCheckBox.TabIndex = 32;
            this.IgnoreDrawingCheckBox.Text = "Ignore Drawing";
            this.IgnoreDrawingCheckBox.UseVisualStyleBackColor = true;
            this.IgnoreDrawingCheckBox.CheckedChanged += new System.EventHandler(this.IgnoreDrawingCheckBox_CheckedChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.IgnoreDrawingCheckBox);
            this.groupBox3.Location = new System.Drawing.Point(8, 294);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(318, 51);
            this.groupBox3.TabIndex = 33;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Misc";
            // 
            // AnimateForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(339, 494);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.autoSaveCheckBox);
            this.Controls.Add(this.levelPassTextBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.levelTextBox);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.MaximizeBox = false;
            this.Name = "AnimateForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Upload";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AnimateForm_FormClosing);
            this.Load += new System.EventHandler(this.AnimateForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BPSNumericUpDown)).EndInit();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox levelTextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox levelPassTextBox;
        private System.Windows.Forms.CheckBox shuffleCheckBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numericUpDown2;
        public System.Windows.Forms.ProgressBar uploadProgressBar;
        private System.Windows.Forms.CheckBox autoSaveCheckBox;
        private System.Windows.Forms.RadioButton uploadOptionButton1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox checkBoxReverse;
        private System.Windows.Forms.CheckBox checkBoxRandom;
        private System.Windows.Forms.TextBox TimeRunningTextBox;
        private System.Windows.Forms.Label TimeRunningLabel;
        private System.Windows.Forms.CheckBox IgnoreDrawingCheckBox;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.NumericUpDown BPSNumericUpDown;
        private System.Windows.Forms.Label BPSLabel;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.CheckBox BPSCheckBox;
    }
}