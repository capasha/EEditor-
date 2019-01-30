using System;
using System.Windows.Forms;
using System.Diagnostics;
namespace EEditor
{
    public partial class AboutUpdate : Form
    {
        public string labelNewVer { get { return newVersionLabel.Text; } set { newVersionLabel.Text = value; } }
        public string labelOldVer { get { return currentVersionLabel.Text; } set { currentVersionLabel.Text = value; } }
        public RichTextBox richtextboxChangelog { get { return richTextBox1; } set { richTextBox1 = value; } }
        public string downloadlink { get; set; }
        public AboutUpdate()
        {
            InitializeComponent();
        }

        private void AboutUpdate_Load(object sender, EventArgs e)
        {
        }

        private void DownloadButton_Click(object sender, EventArgs e)
        {
            Process.Start(downloadlink);
        }
    }
}
