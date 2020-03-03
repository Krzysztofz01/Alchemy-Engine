using System;
using System.Drawing;

namespace Alchemy_Engine
{
    class AlchemyConverter
    {
        //Convert Color struct to Hex string
        public static string colorToHex(Color color)
        {
            return "#" + color.R.ToString("X2") + color.G.ToString("X2") + color.B.ToString("X2");
        }

        //Convert Hex string to Color object
        public static Color hexToColor(string color)
        {
            return ColorTranslator.FromHtml(color);
        }

        //Convert Color struct to HSL struct (RGB to HSL)
        public static HSL colorToHsl(Color color)
        {
            HSL hsl = new HSL(0, 0, 0);

            double doubleR = color.R / 255.0;
            double doubleG = color.G / 255.0;
            double doubleB = color.B / 255.0;

            int maxValue = Math.Max(color.R, Math.Max(color.G, color.B));
            int minValue = Math.Min(color.R, Math.Min(color.G, color.B));

            double diff = maxValue - minValue;
            hsl.L = (maxValue + minValue) / 2;
            if(Math.Abs(diff) < 0.00001)
            {
                hsl.S = 0;
                hsl.H = 0;
            }
            else
            {
                if(hsl.L <= 0.5)
                {
                    hsl.S = (int) Math.Round(diff / (maxValue + minValue));
                }
                else
                {
                    hsl.S = (int) Math.Round(diff / (2 - maxValue - minValue));
                }

                double distanceR = (maxValue - doubleR) / diff;
                double distanceG = (maxValue - doubleG) / diff;
                double distanceB = (maxValue - doubleB) / diff;

                if(doubleR == maxValue)
                {
                    hsl.H = (int) Math.Round(distanceB - distanceG);
                }
                else if(doubleG == maxValue)
                {
                    hsl.H = (int) Math.Round(2 + distanceR - distanceB);
                }
                else
                {
                    hsl.H = (int) Math.Round(4 + distanceG - distanceR);
                }

                hsl.H *= 60;
                if(hsl.H < 0)
                {
                    hsl.H += 360;
                }
            }

            return hsl;
        }

        //Convert HSL struct to Color struct (HSL to RGB)
        public static Color hslToColor(HSL hsl)
        {
            double p2 = 0.0;
            double doubleR = 0.0;
            double doubleG = 0.0;
            double doubleB = 0.0;

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
                doubleR = hsl.L;
                doubleG = hsl.L;
                doubleB = hsl.L;
            }
            else
            {
                doubleR = qqhToRgb(p1, p2, hsl.H + 120);
                doubleG = qqhToRgb(p1, p2, hsl.H);
                doubleB = qqhToRgb(p1, p2, hsl.H - 120);
            }

            return Color.FromArgb(
                (int)(doubleR * 255.0),
                (int)(doubleG * 255.0),
                (int)(doubleB * 255.0));
        }

        private static double qqhToRgb(double q1, double q2, double hue)
        {
            if(hue > 360)
            {
                hue -= 360;
            }
            else if(hue < 0)
            {
                hue += 360;
            }

            if(hue < 60)
            {
                return q1 + (q2 - q1) * hue / 60;
            }

            if(hue < 180)
            {
                return q2;
            }

            if(hue < 240)
            {
                return q1 + (q2 - q1) * (240 - hue) / 60;
            }
            return q1;
        }

        
    }

    struct HSL
    {
        public int H { get; set; }
        public int S { get; set; }
        public int L { get; set; }
        public HSL(int hue, int saturation, int luminance)
        {
            H = hue;
            S = saturation;
            L = luminance;
        }
    }
}
