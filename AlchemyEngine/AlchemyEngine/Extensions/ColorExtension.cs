using AlchemyEngine.Structures;
using System;
using System.Drawing;

namespace AlchemyEngine.Extensions
{
    public static class ColorExtension
    {
        public static string ToHex(this Color color)
        {
            return $"#{color.R:X2}{color.G:X2}{color.B:X2}";
        }

        public static Cmyk ToCmyk(this Color color)
        {
            double red = color.RedInterval();
            double green = color.GreenInterval();
            double blue = color.BlueInterval();

            double k = Math.Min(1.0d - red, Math.Min(1.0d - green, 1.0d - blue));
            double kDiff = 1.0d - k;

            double c = (kDiff > 0) ? (1.0d - red - k) / kDiff : 0;
            double m = (kDiff > 0) ? (1.0d - green - k) / kDiff : 0;
            double y = (kDiff > 0) ? (1.0d - blue - k) / kDiff : 0;

            return new Cmyk(c, m, y, k);
        }

        public static Hsl ToHsl(this Color color)
        {
            double red = color.RedInterval();
            double green = color.GreenInterval();
            double blue = color.BlueInterval();

            double min = Math.Min(red, Math.Min(green, blue));
            double max = Math.Max(red, Math.Max(green, blue));
            double delta = max - min;

            double lightness = (max + min) / 2.0d;
            double saturation = 0.0d;
            int hue = 0;

            if (delta != 0)
            {
                saturation = (lightness <= 0.5d) ? (delta / (max + min)) : (delta / (2.0d - max - min));

                double fHue;

                if (red == max)
                {
                    fHue = ((green - blue) / 6.0d) / delta;
                }
                else if (green == max)
                {
                    fHue = (1.0d / 3.0d) + ((blue - red) / 6.0d) / delta;
                }
                else
                {
                    fHue = (2.0d / 3.0d) + ((red - green) / 6.0d) / delta;
                }

                if (fHue < 0) fHue += 1;
                if (fHue > 1) fHue -= 1;

                hue = (int)(fHue * 360);
            }

            return new Hsl(hue, saturation, lightness);
        }

        public static YCbCr ToYCbCr(this Color color)
        {
            int y = (int)(0 + (0.299f * color.R) + (0.587f * color.G) + (0.114f * color.B));
            int cb = (int)(128 - (0.168736f * color.R) - (0.331264f * color.G) + (0.5f * color.B));
            int cr = (int)(128 + (0.5f * color.R) - (0.418688f * color.G) - (0.081312f * color.B));

            return new YCbCr(y, cb, cr);
        }

        public static Yuv ToYuv(this Color color)
        {
            double red = color.RedInterval();
            double green = color.GreenInterval();
            double blue = color.BlueInterval();

            double luma = 0.299d * red + 0.587d * green + 0.114d * blue;
            double chU = -0.14713d * red + -0.28886d * green + 0.436d * blue;
            double chV = 0.615d * red + -0.51499 * green + -0.10001d * blue;

            return new Yuv(luma, chU, chV);
        }

        public static Color SetRed(this Color color, Func<int, int> expression)
        {
            return Color.FromArgb(color.A, expression(color.R), color.G, color.B);
        }

        public static Color SetGreen(this Color color, Func<int, int> expression)
        {
            return Color.FromArgb(color.A, color.R, expression(color.G), color.B);
        }

        public static Color SetBlue(this Color color, Func<int, int> expression)
        {
            return Color.FromArgb(color.A, color.R, color.G, expression(color.B));
        }

        public static Color SetAlpha(this Color color, Func<int, int> expression)
        {
            return Color.FromArgb(expression(color.A), color.R, color.G, color.B);
        }

        public static double RedInterval(this Color color)
        {
            return color.R / 255.0d;
        }

        public static double GreenInterval(this Color color)
        {
            return color.G / 255.0d;
        }

        public static double BlueInterval(this Color color)
        {
            return color.B / 255.0d;
        }
    }
}
