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
        public LevelBot(EnergyUsage usage, int runs = -1) : base(usage, runs)
        {
        }

        public override void Init()
        {
            base.Init();
            AddListener((Bitmap screen) =>
            {
                return Click(screen, Resources.button, 662, 815, 10)/*Sell rune*/
                | Click(screen, Resources.button, 808, 776, 10) /*accept scrolls*/
                | Click(screen, Resources.button, 808, 836, 10) /*accept dropped monster*/
                | Click(screen, Resources.button, 650, 677, 10) /*resend battle info*/
                ;
            });
            AddListener((Bitmap screen) =>
            {
                if (Click(screen, Resources.victory, 1129, 526, 2))
                {
                    Thread.Sleep(1500);
                    //Runs--;
                    Click(929, 526);
                    return true;
                }
                return false;
            });

            AddListener((Bitmap screen) =>
            {
                if(Click(screen, Resources.replay, 1015, 646, 5))
                {
                    Runs++;
                    return true;
                }
                return false;
            });

        }
    }
}
