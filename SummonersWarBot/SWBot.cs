using Microsoft.Win32;
using SummonersWarBot.Properties;
using SummonersWarBot.Rune;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using b = SummonersWarBot.Bot;

namespace SummonersWarBot
{
    public partial class SWBot : Form
    {
        private Process sw;
        private Rect prevRect;
        private Size size, offsetSize;
        private Point location, offsetLocation;
        private Timer timer;
        private bool running;
        private Overlay overlay;
        private b::Bot bot;

        public SWBot()
        {
            InitializeComponent();

            boxEnergy.Items.Add(b::Bot.EnergyUsage.WAIT);
            boxEnergy.Items.Add(b::Bot.EnergyUsage.BUY);
            boxEnergy.Items.Add(b::Bot.EnergyUsage.GIFTBOX);
            boxEnergy.Items.Add(b::Bot.EnergyUsage.BOTH);
            boxEnergy.SelectedItem = b::Bot.EnergyUsage.WAIT;

            boxMode.Items.Add("Farm");
            boxMode.Items.Add("Lvln");
            boxMode.SelectedIndex = 0;

            //new Thread to handle the process finding asyn
            new System.Threading.Thread(FindProcess).Start();
            new System.Threading.Thread(HandleTelegramBot).Start();

            timer = new Timer()
            {
                Interval = 1000
            };

            DoubleBuffered = true;
            timer.Tick += OnRefresh;
            timer.Start();
            lblLinkId.Text = "LinkId: " + GetHwid();
            b::TelegramBot.ReadUser();
            FormClosed += FornClosed;
        }

        private void FornClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                Environment.Exit(-1);
            }
            catch (Exception)
            {
            }
        }

        private void HandleTelegramBot()
        {
            while (true)
            {
                b::TelegramBot.Update().Wait();
                System.Threading.Thread.Sleep(500);
            }
        }

        private void FindProcess()
        {
            while (sw == null)
            {
                Process[] processes = Process.GetProcessesByName("Bluestacks");
                foreach (Process p in processes)
                    if ((int)p.MainWindowHandle != 0)
                        sw = p;
                System.Threading.Thread.Sleep(100);
            }
            if (sw == null)
                Environment.Exit(0);
            RefreshWindow();
            overlay = new Overlay(sw);
            overlay.Show();
        }

        private void OnRefresh(object sender, EventArgs e)
        {
            Rect rect = default(Rect);
            if (!GetWindowRect(sw.MainWindowHandle, ref rect) || rect < 0)
            {
                status.Text = "closed";
                return;
            }
            else
                status.Text = "opend";
            if (rect != prevRect)
                RefreshWindow();
            if (running)
                Tick();
            //this.Refresh();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (!running)
                return;
            //Bitmap screen = GetScreen();
            ////e.Graphics.DrawString("Current slot:" + RuneHelper.GetSlot(screen, 381, 71), new Font("Arial", 12), Brushes.Black, 0, 0);
            ////RuneHelper.GetSubs(screen, 70, 260);
            //int x = 70;
            //int y = 260;
            //List<EnumSub> subs = new List<EnumSub>();
            //Bitmap tokenSubs = BitmapUtils.GetBitmap(screen, x, y, 170, 200);
            //tokenSubs = BitmapUtils.RemoveColor(tokenSubs, Color.FromArgb(37, 24, 15), Color.Black, 80);
            //e.Graphics.DrawImage(tokenSubs, 0, 0);
        }

        private void Tick()
        {
            Bitmap screen = GetScreen();
            bot?.OnTick(screen);
            if (bot.Finished)
                statusBot.Text = "Finished";
            screen.Dispose();
        }

        private Bitmap GetScreen()
        {
            Bitmap bitmap = new Bitmap(size.Width - offsetSize.Width - offsetLocation.X, size.Height - offsetSize.Height - offsetLocation.Y);
            Graphics g = Graphics.FromImage(bitmap);
            g.CopyFromScreen(location.X + offsetLocation.X, location.Y + offsetLocation.Y, 0, 0, size);
            g.Dispose();
            return bitmap;
        }

        private void RefreshWindow()
        {
            Rect rect = default(Rect);
            if (!GetWindowRect(sw.MainWindowHandle, ref rect))
                return;
            location.X = rect.Left;
            location.Y = rect.Top;
            size.Width = rect.Right - rect.Left;
            size.Height = rect.Bottom - rect.Top;
            prevRect = rect;
        }

        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetWindowRect(IntPtr hWnd, ref Rect lpRect);

        [StructLayout(LayoutKind.Sequential)]
        public struct Rect
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;

            public override string ToString()
            {
                return $"Top: {Top} Bottom: {Bottom} Right: {Right} Left: {Left}";
            }

            public static bool operator==(Rect r1, Rect r2)
            {
                return r1.Left == r2.Left && r1.Top == r2.Top && r1.Right == r2.Right && r1.Bottom == r2.Bottom;
            }

            public static bool operator !=(Rect r1, Rect r2) => !(r1 == r2);
            

            public static bool operator <(Rect r, int value) => r.Left + r.Right + r.Top + r.Bottom < value;
            

            public static bool operator >(Rect r, int value) => r.Left + r.Right + r.Top + r.Bottom > value;
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            btnStart.Text = running ? "Start" : "Stop";
            running = !running;
            if (running)
            {
                bot = new b::Bots.LevelBot((b::Bot.EnergyUsage)boxEnergy.SelectedItem, boxMode.SelectedIndex, (int)numRuns.Value);
            }
            statusBot.Text = running ? "running" : "idle";
        }

        private void rpsChanged(object sender, EventArgs e)
        {
            timer.Interval = (int)(1000 / (float)rps.Value);
        }

        private void checkBoxShow_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxShow.Checked)
                overlay.Show();
            else
                new System.Threading.Thread(overlay.Close).Start();
            
        }

        private void offsetChanged(object sender, EventArgs e)
        {
            offsetLocation = new Point((int)offsetLeft.Value, (int)offsetTop.Value);
            offsetSize = new Size((int)offsetRight.Value, (int)offsetBottom.Value);
            overlay.SizeOffset = offsetSize;
            overlay.LocationOffset = offsetLocation;
        }

        public static string GetHwid()
        {
            string location = @"SOFTWARE\Microsoft\Cryptography";
            string name = "MachineGuid";

            using (RegistryKey localMachineX64View = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64))
            {
                using (RegistryKey rk = localMachineX64View.OpenSubKey(location))
                {
                    if (rk == null)
                        throw new KeyNotFoundException(
                            string.Format("Key Not Found: {0}", location));

                    object machineGuid = rk.GetValue(name);
                    if (machineGuid == null)
                        throw new IndexOutOfRangeException(
                            string.Format("Index Not Found: {0}", name));

                    return machineGuid.ToString().ToUpper().Substring(0, 4);
                }
            }
        }
    }
}
