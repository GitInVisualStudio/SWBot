using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SummonersWarBot.Bot
{
    public class Bot
    {
        private List<Func<Bitmap, bool>> listener;
        private bool finished;

        public Bot()
        {
            Init();
        }

        public virtual void Init()
        {
            listener = new List<Func<Bitmap, bool>>();
            finished = false;
        }

        public void AddListener(Func<Bitmap, bool> action)
        {
            listener.Add(action);
        }

        public virtual void OnTick(Bitmap screen)
        {
            if (listener.Count <= 0)
            {
                finished = true;
                return;
            }
            foreach (Func<Bitmap, bool> func in listener)
                if(func(screen))
                    return;
        }

        public bool Click(Bitmap screen, Bitmap bitmap, int x, int y, float toleranz = 0)
        {
            if (BitmapUtils.BitmapEquals2(screen, bitmap, x, y, toleranz))
            {
                Click(x, y);
                return true;
            }
            return false;
        }

        public void Click(int x, int y)
        {
            Point prevCursorPosition = default(Point);
            while(!GetCursorPos(ref prevCursorPosition));
            SetCursorPos(x, y);
            mouse_event(MOUSEEVENT_LEFTDOWN, x, y, 1, 0);
            mouse_event(MOUSEEVENT_LEFTUP, x, y, 1, 0);
            SetCursorPos(prevCursorPosition.X, prevCursorPosition.Y);
        }

        [DllImport("User32.Dll")]
        public static extern long SetCursorPos(int x, int y);

        [DllImport("user32.dll")]
        private static extern bool GetCursorPos(ref Point lpPoint);

        [DllImport("user32.dll")]
        public static extern void mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);

        private const int MOUSEEVENT_LEFTDOWN = 0x02;
        private const int MOUSEEVENT_LEFTUP = 0x04;
        private const int MOUSEEVENT_MIDDLEDOWN = 0x20;
        private const int MOUSEEVENT_MIDDLEUP = 0x40;
        private const int MOUSEEVENT_RIGHTDOWN = 0x08;
        private const int MOUSEEVENT_RIGHTUP = 0x10;
    }
}
