using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace ms_alchemy_api.Alchemy
{
    public class AlchemyConverter
    {
        /// <summary>
        /// Convert Drawing Color object into hex string
        /// </summary>
        /// <param name="color">Color object</param>
        /// <returns>Hex string</returns>
        public static string colorToHex(Color color)
        {
            return "#" + color.R.ToString("X2") + color.G.ToString("X2") + color.B.ToString("X2");
        }

        /// <summary>
        /// Convert RGB int values into hex string
        /// </summary>
        /// <param name="R">Red int</param>
        /// <param name="G">Green int</param>
        /// <param name="B">Blue int</param>
        /// <returns>Hex string</returns>
        public static string colorToHex(int R, int G, int B)
        {
            return "#" + R.ToString("X2") + G.ToString("X2") + B.ToString("X2");
        }

        /// <summary>
        /// Convert Drawing.Color object into HSL struct
        /// </summary>
        /// <param name="color">Color object</param>
        /// <returns>HSL struct</returns>
        public static HSL colorToHsl(Color color)
        {
            double h = 0.0;
            double s = 0.0;
            double l = 0.0;

            double r = color.R / 255.0;
            double g = color.G / 255.0;
            double b = color.B / 255.0;

            double min = Math.Min(r, Math.Min(g, b));
            double max = Math.Max(r, Math.Max(g, b));

            l = (min + max) / 2.0;

            if (min == max)
            {
                h = 0.0;
                s = 0.0;
            }
            else
            {
                if (l <= 0.5)
                {
                    s = (max - min) / (max + min);
                }
                else
                {
                    s = ((max - min) / (2.0 - max - min));
                }

                if (max == r)
                {
                    h = (g - b) / (max - min);
                }
                else if (max == g)
                {
                    h = 2.0 + (b - r) / (max - min);
                }
                else if (max == b)
                {
                    h = 4.0 + (r - g) / (max - min);
                }

                h *= 60.0;
                if (h < 0)
                {
                    h += 360.0;
                }
            }

            return new HSL(h, s, l);
        }

        /// <summary>
        /// Convert HSL struct into Drawing.Color object
        /// </summary>
        /// <param name="hsl">HSL struct</param>
        /// <returns>Color object</returns>
        public static Color hslToColor(HSL hsl)
        {
            double p2 = 0.0;
            double doubleR = 0.0;
            double doubleG = 0.0;
            double doubleB = 0.0;

            if (hsl.L <= 0.5)
            {
                p2 = hsl.L * (1 + hsl.S);
            }
            else
            {
                p2 = hsl.L + hsl.S - (hsl.L * hsl.S);
            }

            double p1 = 2 * hsl.L - p2;

            if (hsl.S == 0)
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
    }

    public struct HSL
    {
        public double H { get; set; }
        public double S { get; set; }
        public double L { get; set; }
        public HSL(double hue, double saturation, double luminance)
        {
            H = hue;
            S = saturation;
            L = luminance;
        }
    }
}