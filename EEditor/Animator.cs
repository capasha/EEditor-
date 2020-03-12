using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using PlayerIOClient;
using System.Windows.Forms;
using System.Drawing;
using System.Threading.Tasks;

namespace EEditor
{
    internal class Animator
    {
        Connection conn;
        private List<blocks> placedBlocks = new List<blocks>();
        private List<blocks> placeBlocks = new List<blocks>();
        List<Frame> frames;
        int uploaded = 0;
        System.Timers.Timer timer = new System.Timers.Timer();
        List<string[]> firstFrame = new List<string[]>();
        bool b = false;
        private static Semaphore locker;
        Frame remoteFrame;
        List<Frame> remoteFrame_;
        string levelPassword;
        bool shuffle;
        bool reversed;
        bool vertical;
        static int Gtotal;
        static int Gcurrent;
        static int Gcurrent1;
        static int Max1000 = 0;
        static int maxBlocks = 0;
        int botid = 0;
        DateTime epochStartTime;
        System.Threading.Timer passTimer;
        DateTime start;
        object[] param;
        bool restart = false;
        public static System.Windows.Forms.ProgressBar pb; //Make AnimateForm.cs' progressbar work with this upload status
        public static IntPtr afHandle; //Make TaskbarProgress.cs' progressbar work with this upload status
        string[] ignoreMessages = new string[] { "m", "updatemeta", "show", "hide", "k", "init2", "add", "left", "b" };
        public Animator(List<Frame> frames, Connection conn, string levelPassword, bool shuffle, bool reversed, bool vertical)
        {
            locker = new Semaphore(0, 1);
            this.frames = frames;
            this.remoteFrame_ = frames;
            this.conn = conn;
            this.levelPassword = levelPassword;
            this.shuffle = shuffle;
            this.reversed = reversed;
            this.vertical = vertical;
            conn.OnMessage += OnMessage;
            Control.CheckForIllegalCrossThreadCalls = false;
        }

        public event EventHandler<StatusChangedArgs> StatusChanged;

        protected void OnStatusChanged(string msg, DateTime epoch, bool done = false, int totallines = 0, int countedlines = 0)
        {
            StatusChanged?.Invoke(this, new StatusChangedArgs(msg, epoch, done, totallines, countedlines));
        }

        public void Shuffle(List<string[]> l)
        {
            Random r = new Random();
            for (int i = 0; i < l.Count; ++i)
            {
                int j = r.Next(l.Count);
                string[] t = l[i];
                l[i] = l[j];
                l[j] = t;
            }
        }

        public void reverse(List<string[]> l)
        {
            l.Reverse();
        }

        public void Run()
        {
            conn.Send("init");
            locker.WaitOne();
            Gcurrent1 = 0;
            bool drawblocks = false;
            //var inca = 0;
            //if (frames.Count == 1) inca = 0;
            //else inca = 1;
            List<string[]> diff = null;
            //restart:
            //for (int i = inca; i < frames.Count; i++)
            //{
            start = DateTime.Now;

            firstFrame = frames[0].Diff(remoteFrame);
            ModifyProgressBarColor.SetState(pb, 1);
            TaskbarProgress.SetState(afHandle, TaskbarProgress.TaskbarStates.Normal);
            var dt = System.DateTime.UtcNow;
            if (pb.InvokeRequired) pb.Invoke((MethodInvoker)delegate { pb.Maximum = firstFrame.Count; });
            else pb.Maximum = firstFrame.Count;
            epochStartTime = dt;

            //stopit:
            if (frames.Count == 1)
            {
                diff = frames[0].Diff(remoteFrame);
            }
            else
            {
                //diff = frames[i].Diff(remoteFrame);
            }
            if (MainForm.userdata.drawMixed) Shuffle(diff);
            if (!MainForm.userdata.drawMixed && MainForm.userdata.reverse) diff.Reverse();
            if (!MainForm.userdata.drawMixed && MainForm.userdata.random)
            {
                diff.Sort((a, b) => (a.ToString()[0].CompareTo(b.ToString()[0])));
            }
            Gtotal = diff.Count;
            Gcurrent = 0;
            maxBlocks = 0;
            int total = diff.Count;
            //OnStatusChanged("Uploading blocks to level. (Total: " + Gcurrent1 + "/" + Gtotal + ")", dt, false, Gtotal, Gcurrent);
            TaskbarProgress.SetValue(afHandle, Gcurrent, Gtotal); //Set TaskbarProgress.cs' values
            //Queue<string[]> queue = new Queue<string[]>(diff);
            List<string[]> blocks1 = new List<string[]>(diff);

            foreach (var value in blocks1)
            {
                if (value.Length == 4)
                {
                    placeBlocks.Add(new blocks() { layer = Convert.ToInt32(value[3]), x = Convert.ToInt32(value[0]), y = Convert.ToInt32(value[1]), bid = Convert.ToInt32(value[2]), data = new object[] { null } });
                }
                if (value.Length == 5)
                {
                    placeBlocks.Add(new blocks() { layer = Convert.ToInt32(value[3]), x = Convert.ToInt32(value[0]), y = Convert.ToInt32(value[1]), bid = Convert.ToInt32(value[2]), data = new object[] { value[4] } });
                }
                if (value.Length == 8)
                {
                    placeBlocks.Add(new blocks() { layer = Convert.ToInt32(value[3]), x = Convert.ToInt32(value[0]), y = Convert.ToInt32(value[1]), bid = Convert.ToInt32(value[2]), data = new object[] { value[4], value[5], value[6], value[7] } });
                }
                if (value.Length == 7)
                {
                    placeBlocks.Add(new blocks() { layer = Convert.ToInt32(value[3]), x = Convert.ToInt32(value[0]), y = Convert.ToInt32(value[1]), bid = Convert.ToInt32(value[2]), data = new object[] { value[4], value[5], value[6] } });
                }
                if (value.Length == 6)
                {
                    placeBlocks.Add(new blocks() { layer = Convert.ToInt32(value[3]), x = Convert.ToInt32(value[0]), y = Convert.ToInt32(value[1]), bid = Convert.ToInt32(value[2]), data = new object[] { value[4], value[5] } });
                }
            }
            if (placeBlocks.Count != 0)
            {
                int totalblocks = placeBlocks.Count();
                int count = 0;
                foreach (blocks block in placeBlocks)
                {
                    if (block != null)
                    {
                        if (block.data.Length == 1)
                        {


                            sendParam(conn, new object[] { block.layer, block.x, block.y, block.bid, Convert.ToInt32(block.data[0]) });

                        }
                        else if (block.data.Length == 4)
                        {
                            sendParam(conn, new object[] { block.layer, block.x, block.y, block.bid, block.data[0], block.data[1], block.data[2], block.data[3] });

                        }
                        else if (block.data.Length == 3)
                        {
                            sendParam(conn, new object[] { block.layer, block.x, block.y, block.bid, Convert.ToInt32(block.data[0]), Convert.ToInt32(block.data[1]), Convert.ToInt32(block.data[2]) });

                        }
                        else if (block.data.Length == 2)
                        {
                            if (block.bid == 385)
                            {
                                sendParam(conn, new object[] { block.layer, block.x, block.y, block.bid, block.data[1], Convert.ToInt32(block.data[0]) });

                            }
                            else
                            {
                                sendParam(conn, new object[] { block.layer, block.x, block.y, block.bid, block.data[0], Convert.ToInt32(block.data[1]) });
                            }
                        }

                        OnStatusChanged("Uploading blocks to level. (Total: " + placedBlocks.Count + "/" + totalblocks + ")", DateTime.MinValue, false, totalblocks, placedBlocks.Count);
                        OnStatusChanged("", epochStartTime, false, totalblocks, placedBlocks.Count);
                        if (Convert.ToDouble(placedBlocks.Count) <= pb.Maximum && Convert.ToDouble(placedBlocks.Count) >= pb.Minimum) { if (pb.InvokeRequired) pb.Invoke((MethodInvoker)delegate { pb.Value = placedBlocks.Count; }); TaskbarProgress.SetValue(afHandle, placedBlocks.Count, firstFrame.Count); }
                        Thread.Sleep(MainForm.userdata.uploadDelay);
                    }
                }
                OnStatusChanged("Level upload complete!", DateTime.MinValue, true, placeBlocks.Count, 0);
                TaskbarProgress.SetValue(afHandle, 0, firstFrame.Count);
                if (MainForm.userdata.saveWorldCrew)
                {
                    if (AnimateForm.saveRights)
                    {
                        conn.Send("save");
                    }
                }
            }
            else
            {

                OnStatusChanged("Nothing to upload!", DateTime.MinValue, true, 0, 0);
            }
        }

        static async void sendParam(Connection con, object[] param)
        {
            await Task.Run(() => con.Send("b", param));
        }

        void OnMessage(object sender, PlayerIOClient.Message e)
        {
            if (e.Type == "b")
            {
                if (botid == (int)e.GetInt(4))
                {
                    placedBlocks.Add(new blocks() { layer = e.GetInt(0), x = e.GetInt(1), y = e.GetInt(2), bid = e.GetInt(3), data = new object[] { null } });
                }
            }
            else if (e.Type == "bc")
            {
                if (botid == (int)e.GetInt(4))
                {
                    placedBlocks.Add(new blocks() { layer = 0, x = e.GetInt(0), y = e.GetInt(1), bid = e.GetInt(2), data = new object[] { e.GetInt(3) } });
                }
            }
            else if (e.Type == "wp")
            {
                if (botid == (int)e.GetInt(5))
                {
                    placedBlocks.Add(new blocks() { layer = 0, x = e.GetInt(0), y = e.GetInt(1), bid = e.GetInt(2), data = new object[] { e.GetString(3), e.GetInt(4) } });
                }

            }
            else if (e.Type == "ts")
            {
                if (botid == (int)e.GetInt(5))
                {
                    placedBlocks.Add(new blocks() { layer = 0, x = e.GetInt(0), y = e.GetInt(1), bid = e.GetInt(2), data = new object[] { e.GetString(3), e.GetInt(4), } });
                }
            }
            else if (e.Type == "bn")
            {
                if (botid == (int)e.GetInt(7))
                {
                    placedBlocks.Add(new blocks() { layer = 0, x = e.GetInt(0), y = e.GetInt(1), bid = e.GetInt(2), data = new object[] { e.GetString(3), e.GetString(4), e.GetString(5), e.GetString(6) } });
                }
            }
            else if (e.Type == "pt")
            {
                if (botid == (int)e.GetInt(6))
                {
                    placedBlocks.Add(new blocks() { layer = 0, x = e.GetInt(0), y = e.GetInt(1), bid = e.GetInt(2), data = new object[] { e.GetInt(3), e.GetInt(4), e.GetInt(5) } });
                }
            }
            else if (e.Type == "bs")
            {
                if (botid == (int)e.GetInt(4))
                {
                    placedBlocks.Add(new blocks() { layer = 0, x = e.GetInt(0), y = e.GetInt(1), bid = e.GetInt(2), data = new object[] { e.GetInt(3) } });
                }
            }
            else if (e.Type == "access")
            {
                passTimer.Dispose();
                locker.Release();
            }
            else if (e.Type == "init")
            {
                botid = e.GetInt(5);
                AnimateForm.editRights = false;
                AnimateForm.saveRights = false;
                AnimateForm.crewEdit = false;
                AnimateForm.crewWorld = false;
                if (e[25].GetType() == typeof(String) && e[15].GetType() == typeof(Boolean)) { if (e.GetString(25).Contains(bdata.tostring("bm9ib3Q=", false)) && !e.GetBoolean(15)) { OnStatusChanged(string.Empty, DateTime.MinValue, true, 0, 0); return; } }
                remoteFrame = Frame.FromMessage(e);
                conn.Send("init2");
                OnStatusChanged("Connected to the world.", DateTime.MinValue, false, 0, 0);
                if (frames[0].Width <= remoteFrame.Width && frames[0].Height <= remoteFrame.Height)
                {
                }
                else
                {
                    Gtotal = Gcurrent = pb.Maximum = pb.Value = 1;
                    ModifyProgressBarColor.SetState(pb, 2);
                    TaskbarProgress.SetValue(afHandle, Gcurrent, Gtotal);
                    TaskbarProgress.SetState(afHandle, TaskbarProgress.TaskbarStates.Error);
                    OnStatusChanged("Wrong level size. Please create a level with the size of " + remoteFrame.Width + "x" + remoteFrame.Height + ".", DateTime.MinValue, true, 0, 0);
                    return;
                }
                if (e.GetBoolean(34))
                {
                    AnimateForm.crewWorld = true;
                    if (e.GetBoolean(14))
                    {
                        AnimateForm.crewEdit = true;
                        if (e.GetBoolean(31))
                        {
                            AnimateForm.saveRights = true;
                            if (MainForm.userdata.useColor)
                            {
                                if (MainForm.userdata.thisColor != Color.Transparent)
                                {
                                    var hex = ColorTranslator.ToHtml(MainForm.userdata.thisColor);
                                    conn.Send("say", "/bgcolor " + hex);
                                }
                                else
                                {
                                    conn.Send("say", "/bgcolor none");
                                }
                            }
                            else
                            {
                                conn.Send("say", "/bgcolor none");
                            }
                        }
                        locker.Release();
                    }
                    else
                    {
                        OnStatusChanged("Crew: You doesn't have edit rights in this world", DateTime.MinValue, true, 0, 0);
                        return;
                    }
                }
                else if (e.GetString(0) != e.GetString(13))
                {
                    if (MainForm.userdata.level.StartsWith("OW") && levelPassword.Length == 0)
                    {
                        if (e.GetBoolean(14))
                        {
                            MainForm.OpenWorld = true;
                            MainForm.OpenWorldCode = false;
                            locker.Release();
                        }
                        else
                        {
                            MainForm.OpenWorld = false;
                            MainForm.OpenWorldCode = false;
                            OnStatusChanged("You need a password for this world", DateTime.MinValue, true, 0, 0);
                            return;
                        }
                    }
                    else if (MainForm.userdata.level.StartsWith("OW") && levelPassword.Length > 0)
                    {
                        if (!e.GetBoolean(14))
                        {
                            MainForm.OpenWorld = true;
                            MainForm.OpenWorldCode = true;
                            conn.Send("access", levelPassword);
                            passTimer = new System.Threading.Timer(x => OnStatusChanged("Wrong level code. Please enter the right one and retry.", DateTime.MinValue, true, 0, 0), null, 5000, Timeout.Infinite);
                        }
                        else
                        {
                            MainForm.OpenWorld = true;
                            MainForm.OpenWorldCode = false;
                            OnStatusChanged("This world isn't password protected", DateTime.MinValue, true, 0, 0);
                            return;
                        }
                    }
                    else
                    {
                        if (MainForm.userdata.saveWorldCrew)
                        {
                            OnStatusChanged("You are not the owner of this world. You can't save.", DateTime.MinValue, true, 0, 0);
                            return;
                        }
                        else
                        {
                            if (levelPassword.Length > 0)
                            {
                                Gtotal = Gcurrent = pb.Maximum = pb.Value = 1;
                                ModifyProgressBarColor.SetState(pb, 3);
                                TaskbarProgress.SetValue(afHandle, Gcurrent, Gtotal);
                                TaskbarProgress.SetState(afHandle, TaskbarProgress.TaskbarStates.Paused);
                                conn.Send("access", levelPassword);
                                passTimer = new System.Threading.Timer(x => OnStatusChanged("Wrong level code. Please enter the right one and retry.", DateTime.MinValue, true, 0, 0), null, 5000, Timeout.Infinite);
                            }
                            else if (e.GetBoolean(14))
                            {
                                AnimateForm.editRights = true;
                                locker.Release();
                            }
                            else
                            {
                                passTimer = new System.Threading.Timer(x => OnStatusChanged("Didn't get edit. Timer stopped.", DateTime.MinValue, true, 0, 0), null, 20000, Timeout.Infinite);
                            }
                        }
                    }
                }
                else if (e.GetString(0) == e.GetString(13))
                {
                    if (MainForm.userdata.useColor)
                    {
                        if (MainForm.userdata.thisColor != Color.Transparent)
                        {
                            var hex = ColorTranslator.ToHtml(MainForm.userdata.thisColor);
                            conn.Send("say", "/bgcolor " + hex);
                        }
                        else
                        {
                            conn.Send("say", "/bgcolor none");
                        }
                    }
                    else
                    {
                        conn.Send("say", "/bgcolor none");
                    }
                    AnimateForm.editRights = true;
                    AnimateForm.saveRights = true;
                    locker.Release();
                }
            }
            else
            {
                switch (e.Type)
                {
                    case "info":
                        switch (e[0].ToString())
                        {
                            case "Limit reached":
                                OnStatusChanged("Limit Reached.", DateTime.MinValue, true, 0, 0);
                                break;
                            case "World not available":
                                OnStatusChanged("World is not availabe.", DateTime.MinValue, true, 0, 0);
                                break;
                            case "You are banned":
                                OnStatusChanged("You have been kicked.", DateTime.MinValue, true, 0, 0);
                                break;
                            default:
                                Console.WriteLine(e.ToString());
                                break;
                        }
                        break;
                }
            }
        }
    }

    public static class upl
    {
        public static object[] blockdata { get; set; }
    }
    public class blocks : IEquatable<blocks>
    {
        public int layer { get; set; }
        public int x { get; set; }
        public int y { get; set; }
        public int bid { get; set; }
        public object[] data { get; set; }

        public bool Equals(blocks other)
        {
            // Would still want to check for null etc. first.
            return this.layer == other.layer &&
                   this.x == other.x &&
                   this.y == other.y &&
                   this.bid == other.bid &&
                   this.data == other.data;
        }
    }
}
