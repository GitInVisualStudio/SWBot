using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SummonersWarBot
{
    public class BitmapUtils
    {
        public Color GetColor(int x, int y, Bitmap bitmap)
        {
            return bitmap.GetPixel(x, y);
        }

        /// <param name="color">color to compare</param>
        /// <param name="x">location X</param>
        /// <param name="y">location Y</param>
        /// <param name="bitmap">origin Bitmap to compare</param>
        /// <param name="toleranz">0-255</param>
        /// <returns></returns>
        public bool IsColor(Color color, int x, int y, Bitmap bitmap, int toleranz = 0)
        {
            Color bitmapColor = GetColor(x, y, bitmap);
            return Math.Abs(bitmapColor.R - color.R) <= toleranz && Math.Abs(bitmapColor.G - color.G) <= toleranz && Math.Abs(bitmapColor.B - color.B) <= toleranz;
        }
    }
}
