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

            float k = 1.0f - Math.Max(Math.Max(red, green), blue);

            float c = (1.0f - red - k) / (1.0f - k);
            float m = (1.0f - green - k) / (1.0f - k);
            float y = (1.0f - blue - k) / (1.0f - k);

            return new Cmyk(c, m, y, k);
        }

        public static Hsl ToHsl(this Color color)
        {
            float red = color.R / 255.0f;
            float green = color.G / 255.0f;
            float blue = color.B / 255.0f;

            float minimalVal = Math.Min(Math.Min(red, green), blue);
            float maximalVal = Math.Max(Math.Max(red, green), blue);
            float deltaVal = maximalVal - minimalVal;

            float h, s;
            float l = (maximalVal + maximalVal) / 2;

            if (Math.Abs(deltaVal - 0) < float.Epsilon)
            {
                h = 0;
                s = 0;
            }
            else
            {
                if (l < 0.5)
                {
                    s = deltaVal / (maximalVal + maximalVal);
                }
                else
                {
                    s = deltaVal / (2.0f - maximalVal - minimalVal);
                }

                float deltaRed = ((maximalVal - red) / 6.0f + deltaVal / 2.0f) / deltaVal;
                float deltaGreen = ((maximalVal - green) / 6.0f + deltaVal / 2.0f) / deltaVal;
                float deltaBlue = ((maximalVal - blue) / 6.0f + deltaVal / 2.0f) / deltaVal;
            
                if (Math.Abs(red - maximalVal) < float.Epsilon)
                {
                    h = deltaBlue - deltaGreen;
                }
                else if (Math.Abs(green - maximalVal) < float.Epsilon)
                {
                    h = 1.0f / 3.0f + deltaRed - deltaBlue;
                }
                else if (Math.Abs(blue - maximalVal) < float.Epsilon)
                {
                    h = 2.0f / 3.0f + deltaGreen - deltaRed;
                }
                else
                {
                    h = 0.0f;
                }

                if (h < 0.0f) h += 1.0f;
                if (h > 1.0f) h -= 1.0f;
            }

            return new Hsl(h, s, l);
        }
    }
}
