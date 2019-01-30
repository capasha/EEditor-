using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using Microsoft.VisualBasic.FileIO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace EEditor
{
    public partial class SettingsForm : Form
    {
        public static bool reset { get; set; }
        private bool formload = false;
        private int[] lastColors = new int[7];
        public SettingsForm()
        {
            InitializeComponent();
        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            formload = true;

            #region Setting checkboxes
            ToolTip tp = new ToolTip();
            tp.SetToolTip(usePenToolCheckBox, "Enables draw tool after switching blocks.");
            tp.SetToolTip(selectAllBorderCheckBox, "Includes bordering blocks when selecting the whole world by hotkey Ctrl+A.");
            tp.SetToolTip(confirmCloseCheckBox, "Prompts when you attempt to close EEditor.");
            tp.SetToolTip(updateCheckCheckBox, "Checks for updates on every EEditor start.");

            usePenToolCheckBox.Checked = MainForm.userdata.usePenTool;
            selectAllBorderCheckBox.Checked = MainForm.userdata.selectAllBorder;
            confirmCloseCheckBox.Checked = MainForm.userdata.confirmClose;
            updateCheckCheckBox.Checked = MainForm.userdata.updateChecker;
            #endregion

            clearComboBox.SelectedIndex = 0; //Show "Clear settings..." by default
            picbox();
            checkBox1.Checked = MainForm.userdata.themeBorder;
            formload = false;
        }

        #region Setting checkboxes
        private void usePenToolCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            MainForm.userdata.usePenTool = usePenToolCheckBox.Checked;
        }

        private void selectAllBorderCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            MainForm.userdata.selectAllBorder = selectAllBorderCheckBox.Checked;
        }

        private void confirmCloseCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            MainForm.userdata.confirmClose = confirmCloseCheckBox.Checked;
        }

        private void updateCheckCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            MainForm.userdata.updateChecker = updateCheckCheckBox.Checked;
        }
        #endregion

        #region Clear settings
        //Combobox - enable or disable clear button according to value
        private void clearComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (clearComboBox.SelectedIndex == 0 || clearComboBox.SelectedIndex == 1 || clearComboBox.SelectedIndex == 4)
            {
                clearButton.Enabled = false;
                clearComboBox.SelectedIndex = 0;
            }
            else
            {
                clearButton.Enabled = true;
            }
        }
        //Button - remove selected settings, prompt on important ones
        private void clearButton_Click(object sender, EventArgs e)
        {
            switch (clearComboBox.SelectedIndex)
            {
                case 0:
                case 1:
                case 4:
                    break;
                case 2: //Block hotkeys
                    MainForm.resethotkeys = true;
                    MessageBox.Show("Block hotkeys have been cleared.", "Hotkeys cleared", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                    break;
                case 3: //Blocks in unknown tab
                    MainForm.userdata.newestBlocks.Clear();
                    MessageBox.Show("Unknown blocks have been cleared.", "Unknown blocks cleared", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
                case 5: //Old settings
                    var path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData).Replace("Roaming", "Local");
                    if (Directory.Exists(path + "\\EEditor"))
                    {
                        string[] dir = Directory.GetDirectories(path + "\\EEditor");
                        if (dir.Length > 0)
                        {
                            DialogResult result = MessageBox.Show("Found " + dir.Length + " folders that contain old settings and login information.\nWould you like to remove them?", "Clear old settings", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            if (result == DialogResult.Yes)
                            {
                                for (int i = 0; i < dir.Length; i++)
                                {
                                    FileSystem.DeleteDirectory(dir[i], UIOption.OnlyErrorDialogs, RecycleOption.SendToRecycleBin);
                                }
                                MessageBox.Show("Old settings have been removed.", "Old settings removed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                        else { MessageBox.Show("Couldn't find any EEditor logs.", "No logs found", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                    }
                    else
                    {
                        MessageBox.Show("Couldn't find any EEditor logs", "No logs found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    break;
                case 6: //current settings
                    MainForm.userdata = new userData()
                    {
                        username = "guest",
                        newestBlocks = new List<JToken>(),
                        uploadDelay = 5,
                        brickHotkeys = "",
                        sprayr = 5,
                        sprayp = 10,
                        confirmClose = true,
                        updateChecker = true,
                        uploadOption = 0,
                        themeBlock = SystemColors.Window,
                        themeBlocks = SystemColors.Control,
                        themeBlockBG = Color.Transparent,
                        themeIcons = Color.Black,
                        themeIconsBg = Color.Transparent,
                        themeToolbarBg = SystemColors.Control,
                        themeToolbarText = SystemColors.ControlText,
                        themeBorder = false,
                        themeClean = false,
                        imageBackgrounds = true,
                        imageBlocks = true,
                        imageSpecialblocksMorph = false,
                        imageSpecialblocksAction = false,
                        random = false,
                        reverse = false,
                        ColorFG = true,
                        ColorBG = true,
                        ignoreplacing = false,
                        randomLines = false,
                        BPSblocks = 100,
                        BPSplacing = false,
                        IgnoreBlocks = new List<JToken>()
                    };
                    MainForm.OpenWorld = false;
                    MainForm.userdata.useColor = false;
                    MainForm.userdata.thisColor = Color.Transparent;
                    MainForm.editArea.MainForm.updateImageColor();
                    Clipboard.Clear();
                    ToolPen.rotation.Clear();
                    ToolPen.id.Clear();
                    ToolPen.redolist.Clear();
                    ToolPen.undolist.Clear();
                    ToolPen.text.Clear();
                    ToolPen.target.Clear();
                    File.WriteAllText(Directory.GetCurrentDirectory() + "\\settings.json", JsonConvert.SerializeObject(MainForm.userdata, Newtonsoft.Json.Formatting.Indented));
                    reset = true;
                    this.Close();
                    break;
            }
            clearComboBox.SelectedIndex = 0;
        }
        #endregion
        private void picbox()
        {
            for (int a = 0; a < 7; a++)
            {
                Bitmap bmp = new Bitmap(16, 16);
                Graphics gr = Graphics.FromImage(bmp);
                switch (a)
                {
                    case 0:
                        gr.Clear(MainForm.userdata.themeBlocks);
                        lastColors[0] = ColorTranslator.ToOle(MainForm.userdata.themeBlocks);
                        break;
                    case 1:
                        gr.Clear(MainForm.userdata.themeBlock);
                        lastColors[1] = ColorTranslator.ToOle(MainForm.userdata.themeBlock);
                        break;
                    case 2:
                        gr.Clear(MainForm.userdata.themeIcons);
                        lastColors[2] = ColorTranslator.ToOle(MainForm.userdata.themeIcons);
                        break;
                    case 3:
                        if (MainForm.userdata.themeIconsBg == Color.Transparent)
                        {
                            gr.DrawString("T", new Font("system", 8, FontStyle.Regular), Brushes.Black, new Point(2, 2));
                        }
                        else
                        {
                            gr.Clear(MainForm.userdata.themeIconsBg);
                        }
                        lastColors[3] = ColorTranslator.ToOle(MainForm.userdata.themeIconsBg);
                        break;
                    case 4:
                        gr.Clear(MainForm.userdata.themeToolbarBg);
                        lastColors[4] = ColorTranslator.ToOle(MainForm.userdata.themeToolbarBg);
                        break;
                    case 5:
                        gr.Clear(MainForm.userdata.themeToolbarText);
                        lastColors[5] = ColorTranslator.ToOle(MainForm.userdata.themeToolbarText);
                        break;
                    case 6:
                        gr.Clear(MainForm.userdata.themeBlockBG);
                        lastColors[6] = ColorTranslator.ToOle(MainForm.userdata.themeBlockBG);
                        break;
                }
                gr.DrawRectangle(new Pen(Color.Black), new Rectangle(0, 0, 15, 15));
                switch (a)
                {
                    case 0:
                        pictureBox2.Image = bmp;
                        break;
                    case 1:
                        pictureBox1.Image = bmp;
                        break;
                    case 2:
                        pictureBox3.Image = bmp;
                        break;
                    case 3:
                        pictureBox4.Image = bmp;
                        break;
                    case 4:
                        pictureBox5.Image = bmp;
                        break;
                    case 5:
                        pictureBox6.Image = bmp;
                        break;
                    case 6:
                        pictureBox7.Image = bmp;
                        break;
                }
            }
        }
        /*for (int i = 0; i < 4; i++)
        {
            Bitmap bmp = new Bitmap(16, 16);
            Graphics gr = Graphics.FromImage(bmp);
            gr.Clear(MainForm.userdata.themeBlock);
            gr.DrawRectangle(new Pen(Color.Black), new Rectangle(0, 0, 15, 15));
        }*/

        private void SettingsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (!formload)
            {
                MainForm.userdata.themeBorder = checkBox1.Checked;
                MainForm.editArea.MainForm.rebuildGUI(false);
            }
        }

        private void pictureClicker(object sender,EventArgs e)
        {
            ColorDialog dg = new ColorDialog() {
                CustomColors = lastColors
            };

            if (dg.ShowDialog() == DialogResult.OK)
            {
                Bitmap bmp = new Bitmap(16, 16);
                using (Graphics gr = Graphics.FromImage(bmp))
                {
                    gr.Clear(dg.Color);
                    gr.DrawRectangle(new Pen(Color.Black), new Rectangle(0, 0, 15, 15));
                }
                switch (((PictureBox)sender).Name)
                {
                    case "pictureBox1":
                        pictureBox1.Image = bmp;
                        MainForm.userdata.themeBlock = dg.Color;
                        MainForm.editArea.MainForm.rebuildGUI(false);
                        lastColors[0] = ColorTranslator.ToOle(MainForm.userdata.themeBlock);
                        break;
                    case "pictureBox2":
                        MainForm.userdata.themeBlocks = dg.Color;
                        pictureBox2.Image = bmp;
                        MainForm.editArea.MainForm.rebuildGUI(false);
                        lastColors[1] = ColorTranslator.ToOle(MainForm.userdata.themeBlocks);
                        break;
                    case "pictureBox3":
                        pictureBox3.Image = bmp;
                        MainForm.userdata.themeIcons = dg.Color;
                        MainForm.editArea.MainForm.updateImageColor();
                        lastColors[2] = ColorTranslator.ToOle(MainForm.userdata.themeIcons);
                        break;
                    case "pictureBox4":
                        pictureBox4.Image = bmp;
                        MainForm.userdata.themeIconsBg = dg.Color;
                        MainForm.editArea.MainForm.updateImageColor();
                        lastColors[3] = ColorTranslator.ToOle(MainForm.userdata.themeIconsBg);
                        break;
                    case "pictureBox5":
                        pictureBox5.Image = bmp;
                        MainForm.userdata.themeToolbarBg = dg.Color;
                        MainForm.editArea.MainForm.updateImageColor();
                        lastColors[4] = ColorTranslator.ToOle(MainForm.userdata.themeToolbarBg);
                        break;
                    case "pictureBox6":
                        pictureBox6.Image = bmp;
                        MainForm.userdata.themeToolbarText = dg.Color;
                        MainForm.editArea.MainForm.updateImageColor();
                        lastColors[5] = ColorTranslator.ToOle(MainForm.userdata.themeToolbarText);
                        break;
                    case "pictureBox7":
                        pictureBox7.Image = bmp;
                        MainForm.userdata.themeBlockBG = dg.Color;
                        MainForm.editArea.MainForm.rebuildGUI(false);
                        lastColors[6] = ColorTranslator.ToOle(MainForm.userdata.themeBlockBG);
                        break;
                }
                /* 
                 * ## Picture Box 1
                 pictureBox1.Image = bmp;
                 MainForm.userdata.themeBlock = dg.Color;
                 MainForm.editArea.MainForm.rebuildGUI(false);
                 * ## Picture Box 2
                 MainForm.userdata.themeBlocks = dg.Color;
                 pictureBox2.Image = bmp;
                 MainForm.editArea.MainForm.rebuildGUI(false);
                 * ## Picture Box 3
                 pictureBox3.Image = bmp;
                 MainForm.userdata.themeIcons = dg.Color;
                 MainForm.editArea.MainForm.updateImageColor();
                 * ## Picture Box 4
                 pictureBox4.Image = bmp;
                 MainForm.userdata.themeIconsBg = dg.Color;
                 MainForm.editArea.MainForm.updateImageColor();
                 * ## Picture Box 5
                 pictureBox5.Image = bmp;
                 MainForm.userdata.themeToolbarBg = dg.Color;
                 MainForm.editArea.MainForm.updateImageColor();
                 * ## Picture Box 6
                 pictureBox6.Image = bmp;
                 MainForm.userdata.themeToolbarText = dg.Color;
                 MainForm.editArea.MainForm.updateImageColor();
                 * ## Picture Box 7
                 pictureBox7.Image = bmp;
                 MainForm.userdata.themeBlockBG = dg.Color;
                 MainForm.editArea.MainForm.rebuildGUI(false);
                 */
            }
        }
    }
}
