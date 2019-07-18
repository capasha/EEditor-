using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EEditor
{
    public partial class Statistics : Form
    {
        private Dictionary<int, int> bdata = new Dictionary<int, int>();
        public Statistics()
        {
            InitializeComponent();
        }

        private void Statistics_Load(object sender, EventArgs e)
        {
            panel1.AutoScroll = true;
            panel1.BackColor = Color.Gray;
            
            for (int x = 0; x < MainForm.editArea.CurFrame.Width; x++)
            {
                for (int y = 0; y < MainForm.editArea.CurFrame.Height; y++)
                {
                    if (bdata.ContainsKey(MainForm.editArea.CurFrame.Foreground[y, x]))
                    {


                        bdata[MainForm.editArea.CurFrame.Foreground[y, x]] += 1;
                    }
                    else
                    {

                        bdata.Add(MainForm.editArea.CurFrame.Foreground[y, x], 1);
                    }
                    if (bdata.ContainsKey(MainForm.editArea.CurFrame.Background[y, x]))
                    {


                        bdata[MainForm.editArea.CurFrame.Background[y, x]] += 1;
                    }
                    else
                    {

                        bdata.Add(MainForm.editArea.CurFrame.Background[y, x], 1);
                    }
                }
            }
            sortby(0);
        }

        private void Button1_Click(object sender, EventArgs e)
        {


        }

        private void Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void FgradioButton_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void sortby(int id)
        {
            int position = 0, total = 0, wposition = 4;
            int incrw = 0;
            foreach (var val in bdata)
            {
                PictureBox table = new PictureBox();
                ToolTip tp = new ToolTip();
                tp.SetToolTip(table, val.Key.ToString());
                table.Location = new Point(wposition, position + 4);
                table.Name = $"Table_ID{val.Key}";
                table.Size = new Size(60, 30);
                Bitmap bmp = new Bitmap(table.Width, table.Height);
                Bitmap block = new Bitmap(16, 16);
                if (MainForm.miscBMI[val.Key] != 0)
                {
                    block = MainForm.miscBMD.Clone(new Rectangle(MainForm.miscBMI[val.Key] * 16, 0, 16, 16), MainForm.miscBMD.PixelFormat);
                }
                else if (MainForm.decosBMI[val.Key] != 0)
                {
                    block = MainForm.decosBMD.Clone(new Rectangle(MainForm.decosBMI[val.Key] * 16, 0, 16, 16), MainForm.decosBMD.PixelFormat);
                }
                else if (MainForm.foregroundBMI[val.Key] != 0 || val.Key == 0)
                {
                    block = MainForm.foregroundBMD.Clone(new Rectangle(MainForm.foregroundBMI[val.Key] * 16, 0, 16, 16), MainForm.foregroundBMD.PixelFormat);
                }
                else if (MainForm.backgroundBMI[val.Key] != 0)
                {
                    block = MainForm.backgroundBMD.Clone(new Rectangle(MainForm.backgroundBMI[val.Key] * 16, 0, 16, 16), MainForm.backgroundBMD.PixelFormat);
                }
                using (Graphics gr = Graphics.FromImage(bmp))
                {
                    gr.FillRectangle(new SolidBrush(Color.Gray), new Rectangle(5, 5, 100, 50));
                    gr.DrawRectangle(new Pen(Color.White), new Rectangle(5, 5, 54, 24));
                    gr.DrawImage(block, new Point(8, 8));
                    //gr.DrawString($"{val.Value}", new Font("Arial", 8, FontStyle.Regular), new SolidBrush(Color.Black), new Point(25, 9));
                    gr.DrawString($"{val.Value}", new Font("Arial", 8, FontStyle.Regular), new SolidBrush(Color.White), new Point(24, 8));
                }
                table.Image = bmp;
                wposition += 60;
                if (wposition == 244) //244
                {
                    wposition = 4;
                    position += 30;
                }
                panel1.Controls.Add(table);

            }
        }
    }
}
