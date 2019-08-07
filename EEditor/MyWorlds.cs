using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PlayerIOClient;
using System.Text.RegularExpressions;
using System.Threading;
using Newtonsoft.Json;
using System.IO;
using Newtonsoft.Json.Linq;
namespace EEditor
{
    public partial class MyWorlds : Form
    {
        private Semaphore s1 = new Semaphore(0, 1);
        private Semaphore s2 = new Semaphore(0, 1);
        private List<string> rooms = new List<string>();
        public static myWorlds myworlds = new myWorlds();
        public string selectedworld = null;
        public Dictionary<string, myWorlds> worlds = new Dictionary<string, myWorlds>();
        private ListViewColumnSorter listviewsorter;
        public MyWorlds()
        {
            InitializeComponent();
            listviewsorter = new ListViewColumnSorter();
            listView1.ListViewItemSorter = listviewsorter;
            listView1.Sort();
        }

        private void MyWorlds_Load(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            loadWorlds(false);
        }
        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (((ListView)sender).SelectedIndices.Count == 1)
            {
                selectedworld = ((ListView)sender).SelectedItems[0].SubItems[2].Text;
               /* switch (MainForm.accs[MainForm.userdata.username].loginMethod)
                {
                    default:
                    case 0:
                        PlayerIO.QuickConnect.SimpleConnect(bdata.gameID, MainForm.accs[MainForm.userdata.username].login, MainForm.accs[MainForm.userdata.username].password, null, (Client client) => { GetMinimap(client, selectedworld); }, (PlayerIOError error) => { Errorhandler1(error); });
                        break;
                    case 2:
                        PlayerIO.QuickConnect.KongregateConnect(bdata.gameID, MainForm.accs[MainForm.userdata.username].login, MainForm.accs[MainForm.userdata.username].password, null, (Client client) => { GetMinimap(client, selectedworld); }, (PlayerIOError error) => { Errorhandler1(error); });
                        break;
                    case 4:
                        PlayerIO.QuickConnect.SimpleConnect(bdata.gameID, MainForm.accs[MainForm.userdata.username].login, MainForm.accs[MainForm.userdata.username].password, null, (Client cli) =>
                        {
                            cli.Multiplayer.CreateJoinRoom("$service-room", "AuthRoom", true, null, new Dictionary<string, string>() { { "type", "Link" } }, (Connection con) =>
                            {
                                con.OnMessage += (object sender1, PlayerIOClient.Message m) =>
                                {
                                    if (m.Type == "auth") PlayerIO.Authenticate(bdata.gameID, "connected", new Dictionary<string, string>() { { "userId", m.GetString(0) }, { "auth", m.GetString(1) } }, null, (Client client) => { GetMinimap(client, selectedworld); }, (PlayerIOError error) => { Errorhandler1(error); });
                                };
                            },
                            (PlayerIOError error) => { Errorhandler1(error); });
                        }, (PlayerIOError error) => { Errorhandler1(error); });
                        break;
                }*/
                //if (worlds.Count() > 0) File.WriteAllText(Directory.GetCurrentDirectory() + "\\" + MainForm.userdata.username + ".myworlds.json", JsonConvert.SerializeObject(worlds, Newtonsoft.Json.Formatting.Indented));
                //this.Close();
            }
        }
        private void Errorhandler1(PlayerIOError error)
        {

        }
        private void GetMinimap(Client client, string worldid) {
            /*client.BigDB.Load("worlds",worldid,(DatabaseObject dbo) =>
            {

            }*/
            }
        private void MyWorlds_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (worlds.Count() > 0) File.WriteAllText(Directory.GetCurrentDirectory() + "\\" + MainForm.userdata.username + ".myworlds.json", JsonConvert.SerializeObject(worlds, Newtonsoft.Json.Formatting.Indented));
        }

        private void ResetButton_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            loadWorlds(true);
        }
        private void loadWorlds(bool reset)
        {
            int incr = 0, total = 0;
            listView1.Enabled = false;
            listView1.BeginUpdate();
            if (MainForm.userdata.username != "guest")
            {
                retry:
                if (File.Exists(Directory.GetCurrentDirectory() + "\\" + MainForm.userdata.username + ".myworlds.json"))
                {
                    if (reset)
                    {
                        File.Delete(Directory.GetCurrentDirectory() + "\\" + MainForm.userdata.username + ".myworlds.json");
                        goto retry;
                    }
                    var output = JObject.Parse(File.ReadAllText(Directory.GetCurrentDirectory() + "\\" + MainForm.userdata.username + ".myworlds.json"));
                    total = output.Count;
                    foreach (var property in output)
                    {
                        ListViewItem lvi = new ListViewItem();
                        lvi.Text = property.Value["name"].ToString();
                        lvi.SubItems.Add(property.Value["size"].ToString());
                        lvi.SubItems.Add(property.Key);
                        listView1.Items.Add(lvi);
                        progressBar1.Value = (incr * 100) / total;
                        incr++;
                    }
                    progressBar1.Value = 100;
                    listView1.Enabled = true;
                    listView1.EndUpdate();
                }
                else
                {
                    switch (MainForm.accs[MainForm.userdata.username].loginMethod)
                    {
                        default:
                        case 0:
                            PlayerIO.QuickConnect.SimpleConnect(bdata.gameID, MainForm.accs[MainForm.userdata.username].login, MainForm.accs[MainForm.userdata.username].password, null, (Client client) => { loginToWorlds(client); }, (PlayerIOError error) => { Errorhandler(error); });
                            break;
                        case 2:
                            PlayerIO.QuickConnect.KongregateConnect(bdata.gameID, MainForm.accs[MainForm.userdata.username].login, MainForm.accs[MainForm.userdata.username].password, null, (Client client) => { loginToWorlds(client); }, (PlayerIOError error) => { Errorhandler(error); });
                            break;
                        case 4:
                            PlayerIO.QuickConnect.SimpleConnect(bdata.gameID, MainForm.accs[MainForm.userdata.username].login, MainForm.accs[MainForm.userdata.username].password, null, (Client cli) =>
                            {
                                cli.Multiplayer.CreateJoinRoom("$service-room", "AuthRoom", true, null, new Dictionary<string, string>() { { "type", "Link" } }, (Connection con) =>
                                {
                                    con.OnMessage += (object sender1, PlayerIOClient.Message m) =>
                                    {
                                        if (m.Type == "auth") PlayerIO.Authenticate(bdata.gameID, "connected", new Dictionary<string, string>() { { "userId", m.GetString(0) }, { "auth", m.GetString(1) } }, null, (Client client) => { loginToWorlds(client); }, (PlayerIOError error) => { Errorhandler(error); });
                                    };
                                },
                                (PlayerIOError error) => { Errorhandler(error); });
                            }, (PlayerIOError error) => { Errorhandler(error); });
                            break;
                    }

                }
            }
        }
        private void Errorhandler(PlayerIOError error)
        {
            MessageBox.Show($"Error: {error.Message}");
        }
        private void loginToWorlds(Client client)
        {
            int incr = 0, incr1 = 0, total1 = 0, total = 0;
            listView1.Enabled = false;
            client.BigDB.LoadMyPlayerObject((DatabaseObject dbo) =>
            {
                total = dbo.Count;
                if (dbo != null)
                {
                    foreach (var value in dbo)
                    {
                        if (Regex.IsMatch(value.Key, "world[0-9]{0,5}x[0-9]{0,5}"))
                        {
                            if (!rooms.Contains(value.Value)) rooms.Add((string)value.Value);

                        }
                        switch (value.Key)
                        {
                            case "room0":
                            case "worldhome":
                            case "betaonlyroom":
                                if (!rooms.Contains(value.Value)) rooms.Add((string)value.Value);
                                break;
                        }
                        incr++;
                        if (total == incr)
                        {
                            s1.Release();
                        }

                    }
                }
            });

            s1.WaitOne();
            total1 = rooms.Count;
            foreach (var value1 in rooms)
            {
                DatabaseObject[] dba = client.BigDB.LoadKeys("worlds", value1);
                if (dba != null)
                {
                    this.Invoke((MethodInvoker)delegate
                    {
                        ListViewItem lvi = new ListViewItem();
                        string names = null;
                        int h = 0;
                        int w = 0;
                        string wh = null;
                        if (!dba[0].Contains("width") && !dba[0].Contains("height"))
                        {
                            if (dba[0].Contains("type"))
                            {
                                switch (dba[0]["type"])
                                {
                                    case 1:
                                        w = 50;
                                        h = 50;
                                        break;
                                    case 2:
                                        w = 100;
                                        h = 100;
                                        break;
                                    default:
                                    case 3:
                                        w = 200;
                                        h = 200;
                                        break;
                                    case 4:
                                        w = 400;
                                        h = 50;
                                        break;
                                    case 5:
                                        w = 400;
                                        h = 200;
                                        break;
                                    case 6:
                                        w = 100;
                                        h = 400;
                                        break;
                                    case 7:
                                        w = 636;
                                        h = 50;
                                        break;
                                    case 8:
                                        w = 110;
                                        h = 110;
                                        break;
                                    case 11:
                                        w = 300;
                                        h = 300;
                                        break;
                                    case 12:
                                        w = 250;
                                        h = 150;
                                        break;
                                }
                                wh = w + "x" + h;
                            }
                            else
                            {
                                wh = "?x?";
                            }
                        }
                        else
                        {
                            wh = dba[0]["width"].ToString() + "x" + dba[0]["height"].ToString();
                        }
                        names = !dba[0].Contains("name") || dba[0]["name"].ToString() == null ? "Untitled World" : dba[0]["name"].ToString();
                        lvi.Text = names;
                        lvi.SubItems.Add(wh);
                        lvi.SubItems.Add(value1);
                        listView1.Items.Add(lvi);
                        progressBar1.Value = (incr1 * 100) / total1;
                        if (!worlds.ContainsKey(value1)) worlds.Add(value1, new myWorlds() { name = names, size = wh });
                    });
                    incr1++;
                    if (incr1 >= total1)
                    {
                        s2.Release();
                    }
                }
                else
                {
                    s2.Release();
                }
            }
            
            s2.WaitOne();
            if (listView1.InvokeRequired)
            {
                this.Invoke((MethodInvoker)delegate
                {
                    listView1.Enabled = true;
                    listView1.EndUpdate();
                });
            }
            if (!listView1.InvokeRequired)
            {
                listView1.Enabled = true;
                listView1.EndUpdate();
            }
            if (progressBar1.InvokeRequired)
            {
                this.Invoke((MethodInvoker)delegate
                {
                    progressBar1.Value = 100;
                });
            }
            if (!progressBar1.InvokeRequired)
            {
                progressBar1.Value = 100;
            }
        }
        private void listView1_ColumnClick(object sender, ColumnClickEventArgs e)
        {

            if (e.Column == listviewsorter.SortColumn)
            {
                // Reverse the current sort direction for this column.
                if (listviewsorter.Order == SortOrder.Ascending)
                {
                    listviewsorter.Order = SortOrder.Descending;
                }
                else
                {
                    listviewsorter.Order = SortOrder.Ascending;
                }
            }
            else
            {
                // Set the column number that is to be sorted; default to ascending.
                listviewsorter.SortColumn = e.Column;
                listviewsorter.Order = SortOrder.Ascending;
            }
            listView1.Sort();
        }

        private void LoadWorldButton_Click(object sender, EventArgs e)
        {
            if (selectedworld != null)
            {
                MainForm.userdata.level = selectedworld;
                this.Close();
            }
        }
    }
    public class myWorlds
    {
        public string name { get; set; }
        public string size { get; set; }
    }

}
