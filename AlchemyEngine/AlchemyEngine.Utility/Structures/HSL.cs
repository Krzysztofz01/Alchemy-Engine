using AlchemyEngine.Utility.Processing;
using System.Drawing;

namespace AlchemyEngine.Utility.Structures
{
    public class HSL
    {
        public double H { get; set; }
        public double S { get; set; }
        public double L { get; set; }

        public HSL()
        {
        }

        public HSL(double H, double S, double L)
        {
            this.H = H;
            this.S = S;
            this.L = L;
        }

        public CMYK ToCMYK()
        {
            return Convert.HslToCmyk(this);
        }

        public HEX ToHEX()
        {
            return Convert.HslToHex(this);
        }

        public RGB ToRGB()
        {
            return Convert.HslToRgb(this);
        }

        public Color ToColor()
        {
            return Convert.HslToColor(this);
        }

        public static HSL GetRandom()
        {
            var rnd = new System.Random();
            return new HSL(rnd.Next(1, 365), rnd.Next(40, 55), rnd.Next(40, 55));
        }
    }
}
