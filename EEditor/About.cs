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
            this.ForeColor = MainForm.themecolors.foreground;
            this.BackColor = MainForm.themecolors.background;
            for (int i = 0;i < this.Controls.Count;i++)
            {
                if (this.Controls[i].Name .Contains("groupBox"))
                {
                    for (int a = 0;a < this.Controls[i].Controls.Count;a++)
                    {
                        Console.WriteLine(this.Controls[i].Controls[a].Name);
                        if (this.Controls[i].Controls[a].Name.Contains("Label") || this.Controls[i].Controls[a].Name.Contains("label") && !this.Controls[i].Controls[a].Name.Contains("LinkLabel"))
                        {
                            this.Controls[i].Controls[a].ForeColor = MainForm.themecolors.foreground;
                            this.Controls[i].Controls[a].BackColor = MainForm.themecolors.background;
                        }
                        if (this.Controls[i].Controls[a].Name.Contains("LinkLabel"))
                        {
                            ((LinkLabel)this.Controls[i].Controls[a]).ForeColor = MainForm.themecolors.foreground;
                            ((LinkLabel)this.Controls[i].Controls[a]).BackColor = MainForm.themecolors.background;
                            ((LinkLabel)this.Controls[i].Controls[a]).LinkColor = MainForm.themecolors.link;
                            ((LinkLabel)this.Controls[i].Controls[a]).VisitedLinkColor = MainForm.themecolors.visitedlink;
                            ((LinkLabel)this.Controls[i].Controls[a]).ActiveLinkColor = MainForm.themecolors.activelink;


                        }
                        if (this.Controls[i].Controls[a].Name.Contains("Button"))
                        {
                            this.Controls[i].Controls[a].ForeColor = Color.Black;
                        }
                    }
                }
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
