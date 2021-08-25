using System;
using System.Drawing;

namespace AlchemyEngine.Extensions
{
    public static class BitmapExtension
    {
        public static Bitmap Scale(this Bitmap bitmap, int percent)
        {
            if (percent > 100 || percent < 0)
                throw new ArgumentException("The percent value must be between 0 and 100.", nameof(percent));

            int width = (bitmap.Width * percent) / 100;
            int height = (bitmap.Height * percent) / 100;
            return new Bitmap(bitmap, new Size(width, height));
        }
    }
}
