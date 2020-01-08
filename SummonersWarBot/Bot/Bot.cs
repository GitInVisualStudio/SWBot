using SummonersWarBot.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SummonersWarBot.Bot
{
    public class Bot
    {
        private List<Func<Bitmap, bool>> listener;
        private bool finished;
        private int runs;
        private EnergyUsage usage;
        private bool started;
        private event EventHandler<Bitmap> OnPrepare;
        private event EventHandler<Bitmap> OnEnergyEmpty;

        public int Runs
        {
            get
            {
                return runs;
            }
            set
            {
                if (value <= 0)
                    Finished = true;
                runs = value;
            }
        }

        public EnergyUsage Usage { get => usage; set => usage = value; }
        public bool Finished { get => finished; set => finished = value; }

        public Bot(EnergyUsage usage, int runs = -1)
        {
            Usage = usage;
            Runs = runs;
            started = true;
            Init();
        }

        public virtual void Init()
        {
            listener = new List<Func<Bitmap, bool>>();
            Finished = false;

            AddListener((Bitmap screen) =>
            {
                if (BitmapUtils.BitmapEquals2(screen, Resources.button, 1376, 688, 5))//TODO: Create relative locations
                {
                    if (!started)
                        OnPrepare?.Invoke(this, screen);
                    started = false;
                    Runs--;
                    if (Runs > 0)
                        Click(1376, 688);
                    else
                    {
                        TelegramBot.SendMessage("Bot finished!");
                    }
                    return true;
                }
                return false;
            });

            AddListener((Bitmap screen) =>
            {
                if (BitmapUtils.BitmapEquals2(screen, Resources.button, 955, 622, 10))
                {
                    switch (Usage)
                    {
                        case EnergyUsage.WAIT:
                            Click(1356, 318);
                            break;
                        case EnergyUsage.GIFTBOX:
                            Click(1056, 647);//Slect Giftbox
                            Thread.Sleep(500);
                            Click(1148, 391);//Use first
                            Thread.Sleep(500);
                            Click(1322, 226);//Close
                            break;
                    }
                    OnEnergyEmpty?.Invoke(this, screen);
                }
                return false;
            });

            AddListener((Bitmap screen) =>
            {
                //return Click(screen, Resources.replay, 354, 536, 5); //TODO: Create relative locations
                if (BitmapUtils.BitmapEquals2(screen, Resources.replay, 354, 536, 3) && BitmapUtils.BitmapEquals2(screen, Resources.replay, 994, 536, 3))
                {
                    started = false;
                    Click(354, 536);
                    return true;
                }
                return false;
            });
        }

        public void AddListener(Func<Bitmap, bool> action)
        {
            listener.Add(action);
        }

        public virtual void OnTick(Bitmap screen)
        {
            if (Finished)
                return;
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

        private const int MOUSEEVENT_LEFTDOWN = 0x02;
        private const int MOUSEEVENT_LEFTUP = 0x04;

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

        public enum EnergyUsage
        {
            WAIT = 0,
            BUY = 1,
            GIFTBOX = 2,
            BOTH = 3
        }
    }
}
