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
        public override void Init()
        {
            base.Init();
            AddListener((Bitmap screen) =>
            {
                return Click(screen, Resources.button, 1376, 688, 5); //TODO: Create relative locations
            });
            AddListener((Bitmap screen) =>
            {
                if(BitmapUtils.BitmapEquals2(screen, Resources.button, 955, 622, 10))
                {
                    Click(1356, 318);
                    return true;
                }
                return false;
            });
            AddListener((Bitmap screen) =>
            {
                return Click(screen, Resources.button, 662, 815, 10)/*Sell rune*/ | Click(screen, Resources.replay, 336, 550, 5) /*replay*/ | Click(screen, Resources.button, 808, 776, 10) /*accept scrolls*/ | Click(screen, Resources.button, 808, 836, 10) /*accept dropped monster*/;
            });
            AddListener((Bitmap screen) =>
            {
                if (Click(screen, Resources.victory, 1129, 526, 2))
                {
                    Thread.Sleep(1000);
                    Click(1129, 526);
                    return true;
                }
                return false;
            });
        }
    }
}
