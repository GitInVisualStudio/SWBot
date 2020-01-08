using GameOverlay.Drawing;
using GameOverlay.Windows;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SummonersWarBot
{
    public class Overlay
    {
        private readonly GraphicsWindow _window;
        private Process sw;

        private System.Drawing.Size size;
        private System.Drawing.Point locationOffset;
        private System.Drawing.Size sizeOffset;

        public System.Drawing.Size Size { get => size; set => size = value; }
        public System.Drawing.Point LocationOffset { get => locationOffset; set => locationOffset = value; }
        public System.Drawing.Size SizeOffset { get => sizeOffset; set => sizeOffset = value; }

        public Overlay(Process sw)
        {
            this.sw = sw;

            var graphics = new Graphics
            {
                MeasureFPS = false,
                PerPrimitiveAntiAliasing = true,
                TextAntiAliasing = true,
                UseMultiThreadedFactories = true,
                VSync = false,
                WindowHandle = IntPtr.Zero
            };

            _window = new StickyWindow(sw.MainWindowHandle, graphics)
            {
                IsTopmost = true,
                IsVisible = true,
                FPS = 5,
                X = 0,
                Y = 0,
                Width = 1,
                Height = 1
            };

            _window.SetupGraphics += _window_SetupGraphics;
            _window.DestroyGraphics += _window_DestroyGraphics;
            _window.DrawGraphics += _window_DrawGraphics;
        }

        public void Show()
        {
            _window.StartThread();
            if (_window.IsInitialized)
                _window.Show();
        }

        public void Close()
        {
            _window.StopThread();
            _window.Hide();
        }

        private void _window_SetupGraphics(object sender, SetupGraphicsEventArgs e)
        {
            var g = e.Graphics;
        }

        private void _window_DrawGraphics(object sender, DrawGraphicsEventArgs e)
        {
            var g = e.Graphics;
            g.ClearScene();
            g.DrawRectangle(g.CreateSolidBrush(255, 0, 0), locationOffset.X, locationOffset.Y, _window.Width - sizeOffset.Width, _window.Height - sizeOffset.Height, 1);
        }

        private void _window_DestroyGraphics(object sender, DestroyGraphicsEventArgs e)
        {

        }
    }
}
