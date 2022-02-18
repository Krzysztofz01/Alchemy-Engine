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
            float red = color.R / 255.0f;
            float green = color.G / 255.0f;
            float blue = color.B / 255.0f;

            float k = Math.Min(1.0f - red, Math.Min(1.0f - green, 1.0f - blue));
            float kDiff = 1.0f - k;

            float c = (kDiff > 0) ? (1.0f - red - k) / kDiff : 0;
            float m = (kDiff > 0) ? (1.0f - green - k) / kDiff : 0;
            float y = (kDiff > 0) ? (1.0f - blue - k) / kDiff : 0;

            return new Cmyk(c, m, y, k);
        }

        public static Hsl ToHsl(this Color color)
        {
            float red = color.R / 255.0f;
            float green = color.G / 255.0f;
            float blue = color.B / 255.0f;

            float min = Math.Min(red, Math.Min(green, blue));
            float max = Math.Max(red, Math.Max(green, blue));
            float delta = max - min;

            float lightness = (max + min) / 2.0f;
            float saturation = 0.0f;
            int hue = 0;

            if (delta != 0)
            {
                saturation = (lightness <= 0.5) ? (delta / (max + min)) : (delta / (2 - max - min));

                float fHue;

                if (red == max)
                {
                    fHue = ((green - blue) / 6) / delta;
                }
                else if (green == max)
                {
                    fHue = (1.0f / 3) + ((blue - red) / 6) / delta;
                }
                else
                {
                    fHue = (2.0f / 3) + ((red - green) / 6) / delta;
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
