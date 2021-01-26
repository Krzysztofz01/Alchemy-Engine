using AlchemyEngine.Utility.Structures;
using System;
using System.Drawing;
using System.Globalization;

namespace AlchemyEngine.Utility.Processing
{
    static class Convert
    {
        public static HEX RgbToHex(RGB rgb)
        {
            return new HEX($"#{rgb.R:X2}{rgb.G:X2}{rgb.B:X2}");
        }

        public static HEX CmykToHex(CMYK cmyk)
        {
            var rgb = CmykToRgb(cmyk);
            return RgbToHex(rgb);
        }

        public static HEX HslToHex(HSL hsl)
        {
            var rgb = HslToRgb(hsl);
            return RgbToHex(rgb);
        }

        public static HEX ColorToHex(Color color)
        {
            return new HEX($"#{color.R:X2}{color.G:X2}{color.B:X2}");
        }

        public static CMYK RgbToCmyk(RGB rgb)
        {
            double dblR = rgb.R / 255.0;
            double dblG = rgb.G / 255.0;
            double dblB = rgb.B / 255.0;

            double k = 1 - Math.Max(Math.Max(dblR, dblG), dblB);
            double c = (1 - dblR - k) / (1 - k);
            double m = (1 - dblG - k) / (1 - k);
            double y = (1 - dblB - k) / (1 - k);

            return new CMYK(c, m, y, k);
        }

        public static CMYK HslToCmyk(HSL hsl)
        {
            var rgb = HslToRgb(hsl);
            return RgbToCmyk(rgb);
        }

        public static CMYK HexToCmyk(HEX hex)
        {
            var rgb = HexToRgb(hex);
            return RgbToCmyk(rgb);
        }

        public static CMYK ColorToCmyk(Color color)
        {
            var rgb = ColorToRgb(color);
            return RgbToCmyk(rgb);
        }

        public static HSL CmykToHsl(CMYK cmyk)
        {
            var rgb = CmykToRgb(cmyk);
            return RgbToHsl(rgb);
        }

        public static HSL HexToHsl(HEX hex)
        {
            var rgb = HexToRgb(hex);
            return RgbToHsl(rgb);
        }

        public static HSL RgbToHsl(RGB rgb)
        {
            double h = 0.0;
            double s = 0.0;
            double l = 0.0;
            double r = rgb.R / 255.0;
            double g = rgb.G / 255.0;
            double b = rgb.B / 255.0;

            double min = Math.Min(r, Math.Min(g, b));
            double max = Math.Max(r, Math.Max(g, b));

            l = (min + max) / 2.0;

            if(min != max)
            {
                if(l <= 0.5)
                {
                    s = (max - min) / (max + min);
                }
                else
                {
                    s = (max - min) / (2.0 - max - min);
                }

                if(max == r)
                {
                    h = (g - b) / (max - min);
                }
                else if(max == g)
                {
                    h = 2.0 + (b - r) / (max - min);
                }
                else if(max == b)
                {
                    h = 4.0 + (r - g) / (max - min);
                }

                h *= 60.0;
                if(h < 0)
                {
                    h += 360.0;
                }
            }
            return new HSL(h, s, l);
        }

        public static HSL ColorToHsl(Color color)
        {
            var rgb = ColorToRgb(color);
            return RgbToHsl(rgb);
        }

        public static RGB CmykToRgb(CMYK cmyk)
        {
            byte r = (byte)(255 * (1 - cmyk.C) * (1 - cmyk.K));
            byte g = (byte)(255 * (1 - cmyk.M) * (1 - cmyk.K));
            byte b = (byte)(255 * (1 - cmyk.Y) * (1 - cmyk.K));

            return new RGB(r, g, b);
        }

        public static RGB HexToRgb(HEX hex)
        {
            byte rHexVal = byte.Parse(hex.Value.Substring(1, 2), NumberStyles.HexNumber);
            byte gHexVal = byte.Parse(hex.Value.Substring(3, 2), NumberStyles.HexNumber);
            byte bHexVal = byte.Parse(hex.Value.Substring(5, 2), NumberStyles.HexNumber);

            return new RGB(rHexVal, gHexVal, bHexVal);
        }

        public static RGB HslToRgb(HSL hsl)
        {
            double p2 = 0.0;
            double dblR = 0.0;
            double dblG = 0.0;
            double dblB = 0.0;

            if(hsl.L <= 0.5)
            {
                p2 = hsl.L * (1 + hsl.S);
            }
            else
            {
                p2 = hsl.L + hsl.S - (hsl.L * hsl.S);
            }

            double p1 = 2 * hsl.L - p2;

            if(hsl.S == 0)
            {
                dblR = hsl.L;
                dblG = hsl.L;
                dblB = hsl.L;
            }
            else
            {
                dblR = qqhToRgb(p1, p2, hsl.H + 120);
                dblG = qqhToRgb(p1, p2, hsl.H);
                dblB = qqhToRgb(p1, p2, hsl.H - 120);
            }

            return new RGB((byte)(dblR * 255.0), (byte)(dblG * 255.0), (byte)(dblB * 255.0));
        }

        public static RGB ColorToRgb(Color color)
        {
            return new RGB(color.R, color.G, color.B);
        }

        private static double qqhToRgb(double q1, double q2, double hue)
        {
            if (hue > 360)
            {
                hue -= 360;
            }
            else if (hue < 0)
            {
                hue += 360;
            }

            if (hue < 60)
            {
                return q1 + (q2 - q1) * hue / 60;
            }

            if (hue < 180)
            {
                return q2;
            }

            if (hue < 240)
            {
                return q1 + (q2 - q1) * (240 - hue) / 60;
            }
            return q1;
        }

        public static Color RgbToColor(RGB rgb)
        {
            return Color.FromArgb(rgb.R, rgb.G, rgb.B);
        }

        public static Color HexToColor(HEX hex)
        {
            byte rHexVal = byte.Parse(hex.Value.Substring(1, 2), NumberStyles.HexNumber);
            byte gHexVal = byte.Parse(hex.Value.Substring(3, 2), NumberStyles.HexNumber);
            byte bHexVal = byte.Parse(hex.Value.Substring(5, 2), NumberStyles.HexNumber);

            return Color.FromArgb(rHexVal, gHexVal, bHexVal);
        }

        public static Color HslToColor(HSL hsl)
        {
            var rgb = HslToRgb(hsl);
            return Color.FromArgb(rgb.R, rgb.G, rgb.B);
        }

        public static Color CmykToColor(CMYK cmyk)
        {
            var rgb = CmykToRgb(cmyk);
            return Color.FromArgb(rgb.R, rgb.G, rgb.B);
        }
    }
}
