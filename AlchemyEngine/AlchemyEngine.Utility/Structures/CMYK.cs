﻿using AlchemyEngine.Utility.Processing;
using System.Drawing;

namespace AlchemyEngine.Utility.Structures
{
    public class CMYK
    {
        public double C { get; set; }
        public double M { get; set; }
        public double Y { get; set; }
        public double K { get; set; }

        public CMYK()
        {
        }

        public CMYK(double C, double M, double Y, double K)
        {
            this.C = C;
            this.M = M;
            this.Y = Y;
            this.K = K;
        }

        public RGB ToRGB()
        {
            return Convert.CmykToRgb(this);
        }

        public HEX ToHEX()
        {
            return Convert.CmykToHex(this);
        }

        public HSL ToHSL()
        {
            return Convert.CmykToHsl(this);
        }

        public Color ToColor()
        {
            return Convert.CmykToColor(this);
        }

        public static CMYK GetRandom()
        {
            var rnd = new System.Random();
            return new CMYK(rnd.Next(0, 100), rnd.Next(0, 100), rnd.Next(0, 100), rnd.Next(0, 100));
        }

        public override string ToString()
        {
            return $"C: {C}%, M: {M}%, Y: {Y}%, K: {K}%";
        }
    }
}
