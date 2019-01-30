using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Net;
using System.Threading;
using SharpCompress.Archives;
using SharpCompress.Readers;
using System.Diagnostics;
namespace EEditor
{
    public partial class About : Form
    {
        MainForm Frm1;
        public About(MainForm F)
        {
            InitializeComponent();
            this.Text = "About EEditor " + this.ProductVersion;
            Frm1 = F;
        }

        private void About_Load(object sender, EventArgs e)
        {
        }

        #region Main links
        private void Button_Click(object sender, EventArgs e)
        {
            string link = null;
            switch (((Button)sender).Name.ToString())
            {
                case "ForumButton":
                        link = "http://forums.everybodyedits.com/viewtopic.php?id=32502";
                    break;
                case "BugsOrFeatureButton":
                        link = "https://github.com/capasha/eeditor/issues";
                    break;
                case "CreditButton":
                        link = "https://github.com/capasha/eeditor/wiki/Credits";
                    break;
                case "WikiButton":
                        link = "https://github.com/capasha/eeditor/wiki";
                    break;
                case "HomepageButton":
                        link = "https://github.com/capasha/eeditor/";
                    break;
            }
            DialogResult dgresult = MessageBox.Show("Do you want to open this link in your webbrowser?", "Information", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (dgresult == DialogResult.OK)
            {
                Process.Start(link);
            }
        }
        #endregion

        #region Check version
        //Check for newer version
        private void Updater_Click(object sender, EventArgs e)
        {

        }
        public void checkVersion(bool button)
        {
            if (button)
            {
                if (File.Exists(Directory.GetCurrentDirectory() + @"\EEditorDownloader.exe"))
                {
                    ProcessStartInfo prc = new ProcessStartInfo();
                    prc.FileName = Directory.GetCurrentDirectory() + @"\EEditorDownloader.exe";
                    prc.Arguments = @"/silent";
                    Process.Start(prc);
                }
            }
        }


        #endregion
        private void linkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string link = null;
            switch (((LinkLabel)sender).Text)
            {
                case "font and text insertion":
                    link = "https://forums.everybodyedits.com/viewtopic.php?pid=515488#p515488";
                    break;
                case "Google":
                    link = "https://github.com/google/material-design-icons";
                    break;
                case "Material Design Icons contributors":
                    link = "https://materialdesignicons.com/";
                    break;
                case "Fatcow Icons":
                    link = "https://www.fatcow.com/free-icons";
                    break;
                case "Bresenham's Line Algorithm":
                    link = "http://www.roguebasin.com/index.php?title=Bresenham%27s_Line_Algorithm";
                    break;
                case "Json.NET Newtonsoft":
                    link = "https://www.newtonsoft.com/json";
                    break;
                case "PlayerIO SDK":
                    link = "https://playerio.com/download/";
                    break;
                case "Costura Fody - DLL Packer":
                    link = "https://github.com/Fody/Costura";
                    break;
                case "SharpCompress - ZIP tool":
                    link = "https://github.com/adamhathcock/sharpcompress";
                    break;
                case "Yonom/Processor's InitParser":
                    link = "https://gist.github.com/Yonom/3c9ebfe69b1432452f9b";
                    break;
            }
            DialogResult dr = MessageBox.Show("Do you want to open this link in your webbrowser?", "Information", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (dr == DialogResult.Yes)
            {
                Process.Start(link);
            }
        }

    }
}
