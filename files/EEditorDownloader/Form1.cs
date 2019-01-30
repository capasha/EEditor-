using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using SharpCompress;
using SharpCompress.Readers;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SharpCompress.Archives;
using System.Diagnostics;
using System.Threading;
namespace EEditorDownloader
{
    public partial class Form1 : Form
    {
        public static Semaphore s1 = new Semaphore(0, 1);
        public static bool silent = false;
        public Form1(string[] args)
        {
            InitializeComponent();
            if (args.Count() == 1) if (args[0] == "/silent") silent = true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            TextStatuslabel.ForeColor = Color.Blue;
            string EEditorCurrentVersion = null;
            string text = null;
            string newversion = null;
            if (File.Exists(Directory.GetCurrentDirectory() + "\\EEditor.exe"))
            {
                FileVersionInfo.GetVersionInfo(Path.Combine(Directory.GetCurrentDirectory(), "EEditor.exe"));
                FileVersionInfo myFileVersionInfo = FileVersionInfo.GetVersionInfo(Directory.GetCurrentDirectory() + "\\EEditor.exe");
                EEditorCurrentVersion = myFileVersionInfo.FileVersion;



                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://api.github.com/repos/capasha/eeditor/releases/latest");
                request.Method = "GET";
                request.Accept = "application/vnd.github.v3+json";
                request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; rv:48.0) Gecko/20100101 Firefox/48.0";

                using (var response = request.GetResponse() as HttpWebResponse)
                {
                    if (request.HaveResponse && response != null)
                    {
                        using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                        {
                            text = reader.ReadToEnd();
                        }
                        //Console.WriteLine(text);
                        dynamic stuff1 = Newtonsoft.Json.JsonConvert.DeserializeObject(text);
                        //if (stuff1["browser_download_url"] != null) Console.WriteLine(stuff1["browser_download_url"]);
                        if (stuff1["tag_name"] != null) newversion = stuff1["tag_name"].ToString();
                        //Console.WriteLine("Version: " + stuff1["tag_name"] + "\nDownload Link: " + stuff1["html_url"] + "\n\nChangelog: \n" + stuff1["body"]);
                        if (Convert.ToInt32(EEditorCurrentVersion.Replace(".", "")) < Convert.ToInt32(newversion.Replace(".", "")))
                        {

                            if (stuff1["body"] != null)
                            {
                                string[] split = stuff1["body"].ToString().Split('*');

                                for (int i = 0; i < split.Length; i++)
                                {
                                    if (split[i].Length > 1)
                                    {
                                        var value = split[i].Substring(1, split[i].Length - 1);
                                        if (value.StartsWith("Added"))
                                        {
                                            ChangelogRichtextbox.SelectionColor = Color.Green;
                                            ChangelogRichtextbox.AppendText("Added: ");
                                            ChangelogRichtextbox.SelectionColor = Color.Black;
                                            ChangelogRichtextbox.AppendText(value.Replace("Added", "").Substring(1, 1).ToUpper() + value.Replace("Added", "").Substring(2, value.Replace("Added", "").Length - 2));
                                        }
                                        else if (value.StartsWith("Removed"))
                                        {
                                            ChangelogRichtextbox.SelectionColor = Color.Red;
                                            ChangelogRichtextbox.AppendText("Removed: ");
                                            ChangelogRichtextbox.SelectionColor = Color.Black;
                                            ChangelogRichtextbox.AppendText(value.Replace("Removed", "").Substring(1, 1).ToUpper() + value.Replace("Removed", "").Substring(2, value.Replace("Removed", "").Length - 2));
                                        }
                                        else if (value.StartsWith("Fixed"))
                                        {
                                            ChangelogRichtextbox.SelectionColor = Color.Blue;
                                            ChangelogRichtextbox.AppendText("Fixed: ");
                                            ChangelogRichtextbox.SelectionColor = Color.Black;
                                            ChangelogRichtextbox.AppendText(value.Replace("Fixed", "").Substring(1, 1).ToUpper() + value.Replace("Fixed", "").Substring(2, value.Replace("Fixed", "").Length - 2));
                                        }
                                        else
                                        {
                                            ChangelogRichtextbox.SelectionColor = Color.Orange;
                                            ChangelogRichtextbox.AppendText(value);
                                        }
                                    }
                                }
                            }
                            button1.Enabled = true;
                            TextStatuslabel.ForeColor = Color.Green;
                            TextStatuslabel.Text = "Found a new update. Click on Install.";
                        }
                        else
                        {
                            if (silent) this.Close();
                            else
                            {
                                TextStatuslabel.ForeColor = Color.Green;
                                TextStatuslabel.Text = "You have the newest version.";
                            }
                        }


                    }
                    else
                    {
                        TextStatuslabel.Text = "Where are I? I can't find EEditor!";
                        TextStatuslabel.ForeColor = Color.Red;
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string EEditorCurrentVersion = null;
            string newversion = null;
            string zipper = null;
            string text = null;
            bool processExist = false;
            Process[] processlist = Process.GetProcesses();
            foreach (Process theprocess in processlist)
            {
                if (theprocess.ProcessName == "EEditor")
                {

                    processExist = true;
                    break;
                }

            }
            if (processExist)
            {
                TextStatuslabel.Text = "Close EEditor to continue.";
                TextStatuslabel.ForeColor = Color.Red;

            }
            else {
            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://api.github.com/repos/capasha/eeditor/releases/latest");
                request.Method = "GET";
                request.Accept = "application/vnd.github.v3+json";
                request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; rv:48.0) Gecko/20100101 Firefox/48.0";

                using (var response = request.GetResponse() as HttpWebResponse)
                {
                    if (request.HaveResponse && response != null)
                    {
                        using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                        {
                            text = reader.ReadToEnd();
                        }
                        dynamic stuff1 = Newtonsoft.Json.JsonConvert.DeserializeObject(text);
                        if (stuff1["tag_name"] != null) newversion = stuff1["tag_name"].ToString();
                        if (stuff1["assets"] != null)
                        {
                            foreach (var val in stuff1["assets"])
                            {
                                if (val["browser_download_url"] != null)
                                {
                                    zipper = val["browser_download_url"];
                                    break;

                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
                if (File.Exists(Directory.GetCurrentDirectory() + "\\EEditor.exe"))
                {

                    FileVersionInfo.GetVersionInfo(Path.Combine(Directory.GetCurrentDirectory(), "\\EEditor.exe"));
                    FileVersionInfo myFileVersionInfo = FileVersionInfo.GetVersionInfo(Directory.GetCurrentDirectory() + "\\EEditor.exe");
                    EEditorCurrentVersion = myFileVersionInfo.FileVersion;
                    if (Convert.ToInt32(EEditorCurrentVersion.Replace(".", "")) < Convert.ToInt32(newversion.Replace(".", "")))
                    {
                        if (zipper != null)
                        {
                            var file = Path.GetTempPath();
                            WebClient wc = new WebClient();
                            wc.DownloadFileAsync(new Uri(zipper), file + "EEditor_downloaded.zip");
                            wc.DownloadProgressChanged += delegate (object sender2, DownloadProgressChangedEventArgs m)
                            {
                                progressBar1.Value = m.ProgressPercentage;
                            };
                            wc.DownloadFileCompleted += delegate (object sender1, System.ComponentModel.AsyncCompletedEventArgs ee)
                            {
                                if (File.Exists(file + "EEditor_downloaded.zip"))
                                {
                                    var archive = ArchiveFactory.Open(file + "EEditor_downloaded.zip");
                                    foreach (var entry in archive.Entries)
                                    {
                                        if (!entry.IsDirectory)
                                        {
                                            if (entry.Key != "EEditorDownloader.exe")
                                                entry.WriteToDirectory(Directory.GetCurrentDirectory(), new ExtractionOptions() { ExtractFullPath = true, Overwrite = true });
                                        }
                                    }
                                    TextStatuslabel.Text = "Finished.";
                                    TextStatuslabel.ForeColor = Color.Green;
                                }
                            };
                        }
                    }
                }
            }
        }
    }
}
