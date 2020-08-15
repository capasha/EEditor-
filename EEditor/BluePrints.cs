using EELVL;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EEditor
{
    public partial class BluePrints : Form
    {
        private Bitmap img4 = new Bitmap(25 * 16, 25 * 16);
        private Bitmap img1 = new Bitmap(25 * 16, 25 * 16);
        private string test = $"{Directory.GetCurrentDirectory()}\\test.json";
        private BlockContainer container = new BlockContainer();

        public BluePrints()
        {
            InitializeComponent();
            var container = new BlockContainer() { Blocks = new List<Block>() };
        }

        private void BluePrints_Load(object sender, EventArgs e)
        {
            if (Clipboard.ContainsData("EEBlueprints"))
            {

                string[][,] data = (string[][,])Clipboard.GetData("EEBlueprints");
                if (data?.Length == 9)
                {
                    using (Graphics gr = Graphics.FromImage(img4))
                    {
                        gr.Clear(Color.Gray);
                    }
                    for (int y = 0; y < data[1].GetLength(0); y++)
                    {
                        for (int x = 0; x < data[1].GetLength(1); x++)
                        {
                            int bid1 = Convert.ToInt32(data[1][y, x]);


                            if (bid1 >= 500 && bid1 <= 999)
                            {
                                if (MainForm.backgroundBMI[bid1] != 0 || bid1 == 500)
                                {
                                    container.Blocks.Add(new Block() { BlockID = bid1, Layer = 1, X = x, Y = y });
                                    using (Graphics gr1 = Graphics.FromImage(img4))
                                    {
                                        gr1.DrawImage(MainForm.backgroundBMD.Clone(new Rectangle(MainForm.backgroundBMI[bid1] * 16, 0, 16, 16), MainForm.backgroundBMD.PixelFormat), x * 16, y * 16);
                                    }
                                }
                            }
                        }

                    }
                    for (int y = 0; y < data[0].GetLength(0); y++)
                    {
                        for (int x = 0; x < data[0].GetLength(1); x++)
                        {
                            int bid = Convert.ToInt32(data[0][y, x]);

                            using (Graphics gr = Graphics.FromImage(img4))
                            {
                                if (bid < 500 || bid >= 1001)
                                {
                                    if (MainForm.decosBMI[bid] != 0)
                                    {
                                        img1 = bdata.getRotation(bid, Convert.ToInt32(data[2][y, x]));
                                        if (img1 != null)
                                        {
                                            gr.DrawImage(img1, new Rectangle(x * 16, y * 16, 16, 16));
                                            container.Blocks.Add(new Block() { BlockID = bid, Layer = 0, X = x, Y = y, Param = Convert.ToInt32(data[2][y, x]) });
                                        }
                                        else
                                        {
                                            container.Blocks.Add(new Block() { BlockID = bid, Layer = 0, X = x, Y = y });
                                            gr.DrawImage(MainForm.decosBMD.Clone(new Rectangle(MainForm.decosBMI[Convert.ToInt32(data[0][y, x])] * 16, 0, 16, 16), MainForm.decosBMD.PixelFormat), x * 16, y * 16);
                                        }
                                    }
                                    else if (MainForm.miscBMI[bid] != 0 || bid == 119)
                                    {
                                        img1 = bdata.getRotation(bid, Convert.ToInt32(data[2][y, x]));
                                        if (img1 != null)
                                        {
                                            gr.DrawImage(img1, new Rectangle(x * 16, y * 16, 16, 16));
                                            container.Blocks.Add(new Block() { BlockID = bid, Layer = 0, X = x, Y = y, Param = Convert.ToInt32(data[2][y, x]) });
                                        }
                                        else
                                        {
                                            container.Blocks.Add(new Block() { BlockID = bid, Layer = 0, X = x, Y = y });
                                            gr.DrawImage(MainForm.miscBMD.Clone(new Rectangle(MainForm.miscBMI[Convert.ToInt32(data[0][y, x])] * 16, 0, 16, 16), MainForm.miscBMD.PixelFormat), x * 16, y * 16);
                                        }
                                    }
                                    else if (bid != 0)
                                    {
                                        container.Blocks.Add(new Block() { BlockID = bid, Layer = 0, X = x, Y = y });
                                        gr.DrawImage(MainForm.foregroundBMD.Clone(new Rectangle(MainForm.foregroundBMI[Convert.ToInt32(data[0][y, x])] * 16, 0, 16, 16), MainForm.foregroundBMD.PixelFormat), x * 16, y * 16);
                                    }
                                }
                            }
                        }
                    }
                    using (Graphics gr = Graphics.FromImage(img4))
                    {
                        gr.DrawRectangle(new Pen(Color.Black), new Rectangle(0, 0, data[0].GetLength(1) * 16 - 1, data[0].GetLength(0) * 16 - 1));
                    }
                    //Clipboard.Clear();
                    //Console.WriteLine(data[0]);
                    pictureBox1.Image = img4;
                    pictureBox1.Width = data[0].GetLength(1) * 16;
                    pictureBox1.Height = data[0].GetLength(0) * 16;

                }
                //;
                //Console.WriteLine(data[0]);
                Clipboard.Clear();

            }
            // Clipboard.Clear();
        }

        private void BluePrints_FormClosing(object sender, FormClosingEventArgs e)
        {
        }

        private void SaveBPButton_Click(object sender, EventArgs e)
        {
            if (File.Exists(test)) File.Delete(test);
            File.WriteAllText(test, JsonConvert.SerializeObject(container, Newtonsoft.Json.Formatting.Indented));
        }

        private void LoadButton_Click(object sender, EventArgs e)
        {
            string json = null;

            if (File.Exists(test))
            {
                var serializer = new JsonSerializer();

                using (var sw = new StreamReader(test))
                using (var reader = new JsonTextReader(sw))
                {
                    json = serializer.Deserialize(reader).ToString();
                }
            }
            if (json != null)
            {
                container = JsonConvert.DeserializeObject<BlockContainer>(json);
                foreach (var data in container.Blocks)
                {
                    if (data.Layer == 1)
                    {
                        if (data.BlockID >= 500 && data.BlockID <= 999)
                        {
                            if (MainForm.backgroundBMI[data.BlockID] != 0 || data.BlockID == 500)
                            {
                                using (Graphics gr1 = Graphics.FromImage(img4))
                                {
                                    gr1.DrawImage(MainForm.backgroundBMD.Clone(new Rectangle(MainForm.backgroundBMI[data.BlockID] * 16, 0, 16, 16), MainForm.backgroundBMD.PixelFormat), data.X * 16, data.Y * 16);
                                }
                            }
                        }
                    }
                    if (data.Layer == 0)
                    {
                        using (Graphics gr = Graphics.FromImage(img4))
                        {
                            if (data.BlockID < 500 || data.BlockID >= 1001)
                            {
                                if (MainForm.decosBMI[data.BlockID] != 0)
                                {
                                    if (data.Param == null) img1 = null;
                                    else img1 = bdata.getRotation(data.BlockID, Convert.ToInt32(data.Param));
                                    if (img1 != null)
                                    {
                                        gr.DrawImage(img1, new Rectangle(data.X * 16, data.Y * 16, 16, 16));

                                    }
                                    else
                                    {
                                        gr.DrawImage(MainForm.decosBMD.Clone(new Rectangle(MainForm.decosBMI[data.BlockID] * 16, 0, 16, 16), MainForm.decosBMD.PixelFormat), data.X * 16, data.Y * 16);
                                    }
                                }
                                else if (MainForm.miscBMI[data.BlockID] != 0 || data.BlockID == 119)
                                {
                                    if (data.Param == null) img1 = null;
                                    else img1 = bdata.getRotation(data.BlockID, Convert.ToInt32(data.Param));
                                    if (img1 != null)
                                    {
                                        gr.DrawImage(img1, new Rectangle(data.X * 16, data.Y * 16, 16, 16));

                                    }
                                    else
                                    {

                                        gr.DrawImage(MainForm.miscBMD.Clone(new Rectangle(MainForm.miscBMI[Convert.ToInt32(data.BlockID)] * 16, 0, 16, 16), MainForm.miscBMD.PixelFormat), data.X * 16, data.Y * 16);
                                    }
                                }
                                else if (data.BlockID != 0)
                                {

                                    gr.DrawImage(MainForm.foregroundBMD.Clone(new Rectangle(MainForm.foregroundBMI[Convert.ToInt32(data.BlockID)] * 16, 0, 16, 16), MainForm.foregroundBMD.PixelFormat), data.X * 16, data.Y * 16);
                                }
                            }
                        };
                    }
                }
                pictureBox1.Image = img4;
            }
        }

        private void ClipBoardButton_Click(object sender, EventArgs e)
        {
        }
    }
    public class Block
    {
        public int BlockID { get; set; }
        public int Layer { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Param { get; set; }
    }

    public class BlockContainer
    {
        public List<Block> Blocks { get; set; } = new List<Block>();
    }
}
