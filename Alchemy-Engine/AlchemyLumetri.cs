using System;
using System.Drawing;

namespace Alchemy_Engine
{
    class AlchemyLumetri
    {
        public static void greyScaleAverage(Bitmap bitmap)
        {
            for (int y = 0; y < bitmap.Height; y++)
            {
                for (int x = 0; x < bitmap.Width; x++)
                {
                    try
                    {
                        Color currentPixel = bitmap.GetPixel(x, y);
                        int greyScaledValue = (currentPixel.R + currentPixel.G + currentPixel.B) / 3;
                        bitmap.SetPixel(x, y, Color.FromArgb(greyScaledValue, greyScaledValue, greyScaledValue));
                    }
                    catch(Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
            }
        }

        public static void invertColors(Bitmap bitmap)
        {
            for (int y = 0; y < bitmap.Height; y++)
            {
                for (int x = 0; x < bitmap.Width; x++)
                {
                    try
                    {
                        Color currentPixel = bitmap.GetPixel(x, y);
                        int invR = 255 - currentPixel.R;
                        int invG = 255 - currentPixel.G;
                        int invB = 255 - currentPixel.B;
                        bitmap.SetPixel(x, y, Color.FromArgb(invR, invG, invB));
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
            }
        }
    }
}
