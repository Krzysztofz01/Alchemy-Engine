using System.Drawing;

namespace AlchemyEngine.Utility.Extensions
{
    public static class BitmapExtensions
    {
        public static Bitmap Scale(this Bitmap bmp, int percent)
        {
            int width = (bmp.Width * percent) / 100;
            int height = (bmp.Height * percent) / 100;
            return new Bitmap(bmp, new Size(width, height));
        }
    }
}
