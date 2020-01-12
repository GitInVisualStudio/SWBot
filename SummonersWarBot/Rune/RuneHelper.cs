using SummonersWarBot.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SummonersWarBot.Rune
{
    public class RuneHelper
    {

        private static Bitmap[] runes = new Bitmap[] { BitmapUtils.ScaleBitmap(Resources.rune1, 1f),
            BitmapUtils.ScaleBitmap(Resources.rune2, 1f),
        BitmapUtils.ScaleBitmap(Resources.rune3, 1f),
        BitmapUtils.ScaleBitmap(Resources.rune4, 1f),
        BitmapUtils.ScaleBitmap(Resources.rune5, 1f),
        BitmapUtils.ScaleBitmap(Resources.rune6, 1f)};

        public static int GetSlot(Bitmap screen, int x, int y)
        {
            Bitmap runArea = BitmapUtils.GetBitmap(screen, x - 100, y, Resources.rune1.Width + 350, Resources.rune1.Height);
            runArea = BitmapUtils.RemoveColor(runArea, Color.FromArgb(37, 24, 15), Color.Black, 130);
            int width = 0;
            for (int i = runArea.Width - 1; i >= 0; i--)
            {
                if (BitmapUtils.IsColor(runArea.GetPixel(i, runArea.Height / 2), Color.FromArgb(255, 0, 0, 0), 20))
                {
                    width = i;
                    break;
                }
            }

            Bitmap token = BitmapUtils.GetBitmap(runArea, width - Resources.rune1.Width + 10, 0, Resources.rune1.Width, Resources.rune1.Height);
            int index = 0;
            float max = 0;
            for (int i = 0; i < runes.Length; i++)
            {
                float current = BitmapUtils.GetBitmapEquals(token, runes[i], 0, 0);
                if (current > max)
                {
                    max = current;
                    index = i;
                }
            }
            return index + 1;
        }

        public static List<EnumSub> GetSubs(Bitmap screen, int x, int y)
        {
            List<EnumSub> subs = new List<EnumSub>();
            Bitmap tokenSubs = BitmapUtils.GetBitmap(screen, x, y, 100, 200);
                //BitmapUtils.RemoveColor(runArea, Color.FromArgb(37, 24, 15), Color.Black, 130);
            return subs;
        }
    }
}
