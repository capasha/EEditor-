using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Threading;
using System.IO;
namespace EEditor
{
    public partial class InsertImageForm : Form
    {
        private string[,] Area;
        private string[,] Back;
        private string[,] Coins;
        private string[,] Id;
        private string[,] Target;
        private string[,] Text1;
        private string[,] Text2;
        private string[,] Text3;
        private string[,] Text4;
        private bool exit = false;
        int n;
        float[] H;
        float[] S;
        float[] B;

        private Thread thread;
        public static List<int> Blocks = new List<int>();
        public static List<int> Background = new List<int>();
        public static List<int> SpecialMorph = new List<int>();
        public static List<int> SpecialAction = new List<int>();
        public static string path2file = null;

        public InsertImageForm()
        {
            InitializeComponent();
            checkBoxBackground.Checked = MainForm.userdata.imageBackgrounds;
            checkBoxBlocks.Checked = MainForm.userdata.imageBlocks;
            MorphablecheckBox.Checked = MainForm.userdata.imageSpecialblocksMorph;
            ActionBlockscheckBox.Checked = MainForm.userdata.imageSpecialblocksAction;
            n = Minimap.ImageColor.Count();
            H = new float[n];
            S = new float[n];
            B = new float[n];
            for (int i = 0; i < n; ++i)
                if (Minimap.ImageColor[i])
                {
                    Color c = Color.FromArgb((int)Minimap.Colors[i]);
                    H[i] = c.GetHue();
                    S[i] = c.GetSaturation();
                    B[i] = c.GetBrightness();
                }
        }

        public void loadImageButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog imageFileDialog = new OpenFileDialog()
            {
                Filter = "Images |*.jpg;*.jpeg;*.png;*.gif;*.bmp;*.ico",
                Title = "Choose an Image"
            };
            if (imageFileDialog.ShowDialog() == DialogResult.OK)
            {
                path2file = imageFileDialog.FileName;
                //pictureBox1.Image = image;
                //this.Invalidate();
            }
        }

        public void loadDroppedImage(string filename)
        {
            Bitmap originalImage = new Bitmap(Bitmap.FromFile(filename));
            thread = new Thread(() => Transform(originalImage));
            thread.Start();

            //pictureBox1.Image = image;
            //this.Invalidate();
        }
        #region EEditor from image to ee
        private double DistanceHSB(float H0, float S0, float B0, int k)
        {
            return Math.Pow(H0 - H[k], 2) + Math.Pow(S0 - S[k], 2) + Math.Pow(B0 - B[k], 2);
        }

        private int BestMatchHSB(Color c)
        {
            int j = 0;
            float H = c.GetHue();
            float S = c.GetSaturation();
            float B = c.GetBrightness();
            double d = DistanceHSB(H, S, B, 0);
            for (int i = 1; i < n; i++)
            {
                if (Minimap.ImageColor[i])
                {
                    double dist = DistanceHSB(H, S, B, i);
                    if (dist < d)
                    {
                        d = dist;
                        j = i;
                    }
                }
                if (exit) break;
            }
            return j;
        }

        private double Distance(Color a, Color b)
        {
            return Math.Pow(a.R - b.R, 2) + Math.Pow(a.G - b.G, 2) + Math.Pow(a.B - b.B, 2);
        }

        private int BestMatchRGB(Color c)
        {
            int j = 0;
            int a = 0;
            double d = Distance(c, Color.FromArgb((int)Minimap.Colors[0]));
            if (checkBoxBackground.Checked)
            {
                foreach (int i in Background)
                {
                    if (Minimap.ImageColor[i])
                    {
                        double dist = Distance(c, Color.FromArgb((int)Minimap.Colors[i]));
                        if (dist < d)
                        {
                            d = dist;
                            j = i;
                        }
                    }
                    if (exit) break;
                }
            }
            if (checkBoxBlocks.Checked)
            {
                foreach (int i in Blocks)
                {
                    if (Minimap.ImageColor[i])
                    {
                        double dist = Distance(c, Color.FromArgb((int)Minimap.Colors[i]));
                        if (dist < d)
                        {
                            d = dist;
                            j = i;
                        }
                    }
                    if (exit) break;
                }
            }
            if (MorphablecheckBox.Checked)
            {
                foreach (int i in SpecialMorph)
                {
                    if (Minimap.ImageColor[i])
                    {
                        double dist = Distance(c, Color.FromArgb((int)Minimap.Colors[i]));
                        if (dist < d)
                        {
                            d = dist;
                            j = i;
                        }
                    }
                    if (exit) break;
                }
            }
            if (ActionBlockscheckBox.Checked)
            {
                foreach (int i in SpecialAction)
                {
                    if (Minimap.ImageColor[i])
                    {
                        double dist = Distance(c, Color.FromArgb((int)Minimap.Colors[i]));
                        if (dist < d)
                        {
                            d = dist;
                            j = i;
                        }
                    }
                    if (exit) break;
                }
            }

            return j;
        }
        #endregion

        private void Transform(Bitmap image)
        {
            int width = image.Width;
            int height = image.Height;
            int incr = 0;
            Area = new string[height, width];
            Back = new string[height, width];
            Coins = new string[height, width];
            Id = new string[height, width];
            Target = new string[height, width];
            Text1 = new string[height, width];
            Text2 = new string[height, width];
            Text3 = new string[height, width];
            Text4 = new string[height, width];
            if (width > MainForm.editArea.BlockWidth || height > MainForm.editArea.BlockHeight)
            {
                DialogResult rs = MessageBox.Show("The image is bigger than the world you have. Do you want to continue?", "Image bigger than world", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (rs == DialogResult.Yes)
                {
                    for (int x = 0; x < width; x++)
                    {
                        for (int y = 0; y < height; y++)
                        {
                            int c;
                            if (image.GetPixel(x, y).A == 255 && image.GetPixel(x, y).R == 0 && image.GetPixel(x, y).G == 0 && image.GetPixel(x, y).G == 0)
                            {
                                c = 0;
                            }
                            else if (image.GetPixel(x, y).A == 0 && image.GetPixel(x, y).R == 0 && image.GetPixel(x, y).G == 0 && image.GetPixel(x, y).G == 0)
                            {
                                c = 0;
                            }
                            else
                            {
                                c = BestMatchRGB(image.GetPixel(x, y));
                            }
                            if (c < 500 || c >= 1001 || c == -1)
                                Area[y, x] = Convert.ToString(c);
                            else
                                Back[y, x] = Convert.ToString(c);
                            if (exit) break;
                        }
                        if (exit) break;
                        incr += 1;
                        if (progressBar1.InvokeRequired)
                        {
                            progressBar1.Invoke((MethodInvoker)delegate
                            {
                                double progress = ((double)incr / width) * 100;
                                progressBar1.Value = (int)progress;
                            });
                        }
                    }
                }
                else
                {
                    return;
                }
            }
            else
            {
                for (int x = 0; x < width; x++)
                {
                    for (int y = 0; y < height; y++)
                    {
                        int c;
                        if (image.GetPixel(x, y).A == 255 && image.GetPixel(x, y).R == 0 && image.GetPixel(x, y).G == 0 && image.GetPixel(x, y).G == 0)
                        {
                            c = 0;
                        }
                        else if (image.GetPixel(x, y).A == 0 && image.GetPixel(x, y).R == 0 && image.GetPixel(x, y).G == 0 && image.GetPixel(x, y).G == 0)
                        {
                            c = 0;
                        }
                        else
                        {
                            c = BestMatchRGB(image.GetPixel(x, y));
                        }
                        if (c < 500 || c >= 1001 || c == -1)
                            Area[y, x] = Convert.ToString(c);
                        else
                            Back[y, x] = Convert.ToString(c);
                        if (exit) break;
                    }
                    if (exit) break;
                    incr += 1;
                    if (progressBar1.InvokeRequired)
                    {
                        progressBar1.Invoke((MethodInvoker)delegate
                        {
                            double progress = ((double)incr / width) * 100;
                            progressBar1.Value = (int)progress;
                        });
                    }
                }
            }
            DialogResult imagedone = MessageBox.Show("The image has been loaded! Would you like to insert it now?\n\nYes - inserts loaded image at current world position\nNo - adds loaded image to clipboard, so you can paste it with Ctrl + V\nCancel - lets you pick another image", "Image loaded", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            if (imagedone == DialogResult.Yes)
            {
                this.Invoke((MethodInvoker)delegate
                {
                    Clipboard.SetData("EEData", new string[][,] { Area, Back, Coins, Id, Target, Text1,Text2,Text3,Text4 });
                    MainForm.editArea.MainForm.SetMarkTool();
                    MainForm.editArea.Focus();
                    SendKeys.Send("^{v}");
                    Close();
                });
            }
            try
            {
                thread.Abort();
            }
            catch
            {
            }
        }


        private void checkBoxBackground_CheckedChanged(object sender, EventArgs e)
        {
            MainForm.userdata.imageBackgrounds = checkBoxBackground.Checked;
        }

        private void checkBoxBlocks_CheckedChanged(object sender, EventArgs e)
        {
            MainForm.userdata.imageBlocks = checkBoxBlocks.Checked;
        }

        private void MorphablecheckBox_CheckedChanged(object sender, EventArgs e)
        {
            MainForm.userdata.imageSpecialblocksMorph = MorphablecheckBox.Checked;
        }

        private void ActionBlockscheckBox_CheckedChanged(object sender, EventArgs e)
        {
            MainForm.userdata.imageSpecialblocksAction = ActionBlockscheckBox.Checked;
        }

        private void InsertImageForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            exit = true;
        }

        private void GeneratorButton_Click(object sender, EventArgs e)
        {
            if (path2file != null && File.Exists(path2file))
            {
                Bitmap originalImage = new Bitmap(Bitmap.FromFile(path2file));
                thread = new Thread(() => Transform(originalImage));
                thread.Start();
                path2file = null;
            }
            else
            {
                FlexibleMessageBox.Show("The picture doesn't exist or isn't loaded.");
            }
        }
    }
}
