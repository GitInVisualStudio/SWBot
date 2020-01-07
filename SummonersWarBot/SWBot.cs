using SummonersWarBot.Properties;
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
            //new Thread to handle the process finding asyn
            new System.Threading.Thread(FindProcess).Start();

            timer = new Timer()
            {
                Interval = 1000
            };
            DoubleBuffered = true;
            timer.Tick += OnRefresh;
            timer.Start();
            bot = new b::Bots.LevelBot();
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
            //if (!running)
            //    return;
            //Bitmap screen = GetScreen();
            //Bitmap runArea = BitmapUtils.GetBitmap(screen, 1016 - 100, 289, Resources.rune1.Width + 280, Resources.rune1.Height);
            //runArea = BitmapUtils.RemoveColor(runArea, Color.FromArgb(37, 24, 15), Color.Black, 130);
            //int width = 0;
            //for (int i = runArea.Width - 1; i >= 0; i--)
            //{
            //    if (BitmapUtils.IsColor(runArea.GetPixel(i, runArea.Height / 2), Color.FromArgb(255, 0, 0, 0), 20))
            //    {
            //        width = i;
            //        break;
            //    }
            //}
            //Bitmap token = BitmapUtils.GetBitmap(runArea, width - Resources.rune1.Width + 3, 0, Resources.rune1.Width, Resources.rune1.Height);
            //int index = 0;
            //float max = 0;
            //for (int i = 0; i < runes.Length; i++)
            //{
            //    float current = BitmapUtils.GetBitmapEquals(token, runes[i], 0, 0);
            //    if (current > max)
            //    {
            //        max = current;
            //        index = i;
            //    }
            //}
            //e.Graphics.DrawString("Current slot:" + (index + 1), new Font("Arial", 12), Brushes.Black, 0, 0);
            //e.Graphics.DrawString("Sure with about:" + ((int)max) + "%", new Font("Arial", 12), Brushes.Black, 0, 15);
            //e.Graphics.DrawImage(Resources.runeToken, width, 0);
            //e.Graphics.DrawImage(runArea, 0, 0);
        }

        private Bitmap[] runes = new Bitmap[] { BitmapUtils.ScaleBitmap(Resources.rune1, 0.9f),
            BitmapUtils.ScaleBitmap(Resources.rune2, 0.9f),
        BitmapUtils.ScaleBitmap(Resources.rune3, 0.9f),
        BitmapUtils.ScaleBitmap(Resources.rune4, 0.9f),
        BitmapUtils.ScaleBitmap(Resources.rune5, 0.9f),
        BitmapUtils.ScaleBitmap(Resources.rune6, 0.9f)};

        private void Tick()
        {
            Bitmap screen = GetScreen();
            bot?.OnTick(screen);
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
    }
}
