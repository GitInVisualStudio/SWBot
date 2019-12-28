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

        public SWBot()
        {
            InitializeComponent();
            //new Thread to handle the process finding asyn
            new System.Threading.Thread(FindProcess).Start();

            timer = new Timer()
            {
                Interval = 1000
            };

            timer.Tick += OnRefresh;
            timer.Start();
        }

        private void FindProcess()
        {
            while((sw = Process.GetProcessesByName("Bluestacks").FirstOrDefault()) == null)
                System.Threading.Thread.Sleep(100);
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
        }

        private void Tick()
        {
            Bitmap screen = GetScreen();
        }

        private Bitmap GetScreen()
        {
            Bitmap bitmap = new Bitmap(size.Width, size.Height);
            Graphics g = Graphics.FromImage(bitmap);
            g.CopyFromScreen(location.X, location.Y, 0, 0, size);
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
