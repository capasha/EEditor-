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
            for (int i = 0;i < this.Controls.Count;i++)
            {
                Console.WriteLine(this.Controls[i].Name);
                //if (this.Controls[i].name)
            }
        }

        #region Main links
        private void Button_Click(object sender, EventArgs e)
        {
            string link1 = null;
            switch (((Button)sender).Name.ToString())
            {
                case "ForumButton":
                        link1 = "http://forums.everybodyedits.com/viewtopic.php?id=32502";
                    break;
                case "BugsOrFeatureButton":
                        link1 = "https://github.com/capasha/EEditor-/issues";
                    break;
                case "CreditButton":
                        link1 = "https://github.com/capasha/EEditor-/wiki/Credits";
                    break;
                case "WikiButton":
                        link1 = "https://github.com/capasha/EEditor-/wiki";
                    break;
                case "HomepageButton":
                        link1 = "https://github.com/capasha/EEditor-";
                    break;
            }
            DialogResult dgresult = MessageBox.Show("Do you want to open this link in your webbrowser?", "Information", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (dgresult == DialogResult.Yes)
            {
                Process.Start(link1);
            }
        }
        #endregion

        #region Check version


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
                case "Lukem's .eelvl parser":
                    link = "https://gitlab.com/LukeM212/EELVL/tree/legacy";
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
