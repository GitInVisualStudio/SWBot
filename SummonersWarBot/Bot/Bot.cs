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
        protected const int MOUSEEVENT_LEFTDOWN = 0x02;
        protected const int MOUSEEVENT_LEFTUP = 0x04;
        private List<Func<Bitmap, bool>> listener;
        private bool finished;
        private int runs;
        private EnergyUsage usage;
        private bool started;
        private event EventHandler<Bitmap> OnPrepare;
        private event EventHandler<Bitmap> OnEnergyEmpty;
        private int mode;
        private bool shouldPrepare, shouldWait;
        private List<int> maxLvl;
        private int soltsToFill;

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
        public int Mode { get => mode; set => mode = value; }
        public int Toleranz { get => toleranz; set => toleranz = value; }

        private int toleranz;

        public Bot(EnergyUsage usage, int mode, int runs = -1)
        {
            Usage = usage;
            Runs = runs;
            this.Mode = mode;
            Init();
        }

        public virtual void Init()
        {
            started = true;
            listener = new List<Func<Bitmap, bool>>();
            Finished = false;
            maxLvl = new List<int>();
            shouldPrepare = false;
            shouldWait = false;
            Toleranz = 7;
            if (mode != 0)
                runs = 1;

            AddListener(Start);

            AddListener((Bitmap screen) =>
            {
                if (BitmapUtils.BitmapEquals(screen, Resources.button, 621, 622, 10))
                {
                    switch (Usage)
                    {
                        case EnergyUsage.WAIT:
                            Click(1356, 318);
                            break;
                        case EnergyUsage.BUY:
                            Click(710, 660);//Slect Shop
                            Thread.Sleep(500);
                            Click(710, 660);//Slect energy
                            Thread.Sleep(500);
                            Click(710, 660);//accept
                            Thread.Sleep(2000);
                            Click(920, 630);//ok
                            Thread.Sleep(500);
                            Click(1700, 144);//ok
                            Thread.Sleep(500);
                            break;
                        case EnergyUsage.GIFTBOX:
                            if (!BitmapUtils.BitmapEquals(screen, Resources.button, 958, 620, 5))
                            {
                                Click(1356, 318);
                                break;
                            }
                            Click(1056, 647);//Slect Giftbox
                            Thread.Sleep(500);
                            Click(1148, 391);//Use first
                            Thread.Sleep(500);
                            Click(1322, 226);//Close
                            break;
                    }
                    OnEnergyEmpty?.Invoke(this, screen);
                    return true;
                }
                return false;
            });

            AddListener((Bitmap screen) =>
            {
                //return Click(screen, Resources.replay, 354, 536, 5); //TODO: Create relative locations
                if (BitmapUtils.BitmapEquals(screen, Resources.replay, 354, 536, 3) && BitmapUtils.BitmapEquals(screen, Resources.replay, 994, 536, 3))
                {
                    started = false;
                    shouldWait = false;
                    if (shouldPrepare)
                    {
                        Click(1083, 852);
                        return true;
                    }
                    Click(354, 536);
                    return true;
                }
                return false;
            });

            AddListener((Bitmap screen) => //Level Up
            {
                return Click(screen, Resources.levelUp, 802, 461, 5);
            });

            AddListener((Bitmap screen) =>
            {
                return Click(screen, Resources.button, 662, 800, 10)/*Sell rune*/
                | Click(screen, Resources.button, 808, 776, 10) /*accept scrolls*/
                | Click(screen, Resources.button, 650, 605, 10) /*accept selling the run*/
                | Click(screen, Resources.button, 808, 836, 10) /*accept dropped monster*/
                | Click(screen, Resources.button, 808, 876, 10) /*essenz*/
                | Click(screen, Resources.button, 650, 677, 10) /*resend battle info*/
                ;
            });

            AddListener(Victory);

            AddListener((Bitmap screen) =>
            {
                if (BitmapUtils.BitmapEquals(screen, Resources.replay, 1015, 646, 3))
                {
                    Click(1015, 646);
                    Thread.Sleep(1000);
                    Click(800, 200);
                    return true;
                }
                return false;
            });
        }

        private int wait;

        protected virtual bool Victory(Bitmap screen)
        {
            if (BitmapUtils.BitmapEquals(screen, Resources.victory, 650, 160, 5))
            {
                if (!shouldWait)
                {
                    shouldWait = true;
                    wait = 2;
                    return true;
                }
                shouldWait = false;
                if (!shouldPrepare)
                    SetMaxLvl(screen);
                shouldPrepare = maxLvl.Count != 0;
                Click(650, 160);
                Thread.Sleep(1000);
                if (Mode == 0)
                {
                    TelegramBot.SendMessage("Runs left: " + Runs);
                    Runs--;
                }
                if (Finished)
                    TelegramBot.SendMessage("Bot finished!");
                Click(929, 826);
                wait = 2;
                return true;
            }
            return false;
        }

        protected virtual bool Start(Bitmap screen)
        {
            if (BitmapUtils.BitmapEquals(screen, Resources.button, 1376, 688, 5))//TODO: Create relative locations
            {
                if (mode == 0)
                {
                    if (started)
                    {
                        OnPrepare?.Invoke(this, screen);
                        started = false;
                    }
                    Runs--;
                    Click(1380, 690);
                }
                else
                    HandleMons(screen);
                return true;
            }
            return false;
        }

        private void HandleMons(Bitmap screen)
        {
            if (started)
            {
                started = false;
                OnPrepare?.Invoke(this, screen);
                return;
            }

            if (shouldPrepare)
            {
                foreach (int slot in maxLvl)
                {
                    switch (slot)
                    {
                        case 0:
                            Click(276, 419);
                            break;
                        case 1:
                            Click(652, 411);
                            break;
                        case 2:
                            Click(490, 504);
                            break;
                    }
                    Thread.Sleep(200);
                }
                soltsToFill = maxLvl.Count;
                maxLvl.Clear();
                shouldPrepare = false;
                return;
            }
            
            if (soltsToFill > 0 || (soltsToFill == 0 && SlotLeft(screen)))
                AddMons(screen);
            else
                Click(1376, 688);
        }

        private void AddMons(Bitmap screen)
        {
            Bitmap lvl = BitmapUtils.GetBitmap(screen, 224, 755, 1000, 35);
            Bitmap tokenLvl = BitmapUtils.KeepColor(lvl, Color.FromArgb(244, 236, 220), Color.Black, 70);
            for (int i = 33; i < lvl.Width; i++)
            {
                if (BitmapUtils.IsColor(lvl.GetPixel(i, lvl.Height / 2), Color.FromArgb(41, 27, 14), 20))
                {
                    if (BitmapUtils.BitmapEquals(tokenLvl, Resources.lvl1, i - 27, 8, 4) && !BitmapUtils.BitmapEquals(lvl, Resources.lvl0, i - 35, 5, 18.5f))
                    {
                        Click(224 + i - 20, 755);
                        soltsToFill--;
                        Thread.Sleep(1000);
                        return;
                    }
                }
            }
            Scroll(1000, 720, 600, 720);
            Thread.Sleep(1000);
        }

        private bool SlotLeft(Bitmap screen)
        {
            return BitmapUtils.BitmapEquals(screen, Resources.slot, 424, 429, 10) || BitmapUtils.BitmapEquals(screen, Resources.slot, 241, 333, 10) || BitmapUtils.BitmapEquals(screen, Resources.slot, 604, 336, 10);
        }

        private void SetMaxLvl(Bitmap screen)
        {
            maxLvl.Clear();
            for (int i = 1; i < 4; i++)
            {
                if (IsMaxLvl(screen, i))
                {
                    maxLvl.Add(i - 1);
                }
            }
        }

        private bool IsMaxLvl(Bitmap screen, int solt)
        {
            int x = 515, y = 640;
            if (solt == 3)
                y += 100;
            else
                x += solt * 361;
            Bitmap maxLvl = BitmapUtils.GetBitmap(screen, x, y, 200, 60);
            maxLvl = BitmapUtils.KeepColor(maxLvl, Color.FromArgb(240, 194, 12), Color.Black, 80);
            int offsetY = 0;
            for (int i = 0; i < maxLvl.Height; i++)
                if (maxLvl.GetPixel(6, i).A != 0)
                {
                    offsetY = i;
                    break;
                }
            maxLvl = BitmapUtils.GetBitmap(maxLvl, 0, offsetY, maxLvl.Width, Resources.maxLvl.Height);
            return BitmapUtils.BitmapEquals(maxLvl, Resources.maxLvl, 0, 0, 6);
        }

        public void AddListener(Func<Bitmap, bool> action)
        {
            listener.Add(action);
        }

        public virtual void OnTick(Bitmap screen)
        {
            if (Finished)
                return;
            if (wait > 0)
            {
                wait--;
                return;
            }
            foreach (Func<Bitmap, bool> func in listener)
                if(func(screen))
                    return;
        }

        public bool Click(Bitmap screen, Bitmap bitmap, int x, int y, float toleranz = 0)
        {
            if (BitmapUtils.BitmapEquals(screen, bitmap, x, y, toleranz))
            {
                Click(x, y);
                return true;
            }
            return false;
        }

        public void Scroll(int x, int y, int x2, int y2)
        {
            Point prevCursorPosition = default(Point);
            while (!GetCursorPos(ref prevCursorPosition));
            SetCursorPos(x, y);
            mouse_event(MOUSEEVENT_LEFTDOWN, x, y, 0, 0);
            SetCursorPos(x2, y2);
            Thread.Sleep(20);
            mouse_event(MOUSEEVENT_LEFTUP, x2, y2, 1, 0);
            SetCursorPos(prevCursorPosition.X, prevCursorPosition.Y);
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

        [DllImport("user32.dll")]
        public static extern long SetCursorPos(int x, int y);

        [DllImport("user32.dll")]
        public static extern bool GetCursorPos(ref Point lpPoint);

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
