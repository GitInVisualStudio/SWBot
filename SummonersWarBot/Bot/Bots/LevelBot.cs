using SummonersWarBot.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SummonersWarBot.Bot.Bots
{
    public class LevelBot : Bot
    {
        private int mode;

        public LevelBot(EnergyUsage usage, int mode, int runs = -1) : base(usage, runs)
        {
            this.mode = mode;
        }

        public override void Init()
        {
            base.Init();
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

            AddListener((Bitmap screen) =>
            {
                if (Click(screen, Resources.victory, 1129, 526, 2))
                {
                    Thread.Sleep(1500);
                    if (mode == 0)
                    {
                        TelegramBot.SendMessage("Runs left: " + Runs);
                        Runs--;
                    }
                    if (Runs <= 0)
                        TelegramBot.SendMessage("Bot finished!");
                    Click(929, 826);
                    return true;
                }
                return false;
            });

            AddListener((Bitmap screen) =>
            {
                if (Click(screen, Resources.replay, 1015, 646, 5))
                {
                    return true;
                }
                return false;
            });

        }
    }
}
