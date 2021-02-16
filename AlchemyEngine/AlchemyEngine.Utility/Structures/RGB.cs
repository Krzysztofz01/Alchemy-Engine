using AlchemyEngine.Utility.Processing;
using System.Drawing;

namespace AlchemyEngine.Utility.Structures
{
    public class RGB
    {
        public byte R { get; set; }
        public byte G { get; set; }
        public byte B { get; set; }

        public RGB()
        {
        }

        public RGB(byte R, byte G, byte B)
        {
            this.R = R;
            this.G = G;
            this.B = B;
        }

        public CMYK ToCMYK()
        {
            return Convert.RgbToCmyk(this);
        }

        public HEX ToHEX()
        {
            return Convert.RgbToHex(this);
        }

        public HSL ToHSL()
        {
            return Convert.RgbToHsl(this);
        }

        public Color ToColor()
        {
            return Convert.RgbToColor(this);
        }

        public static RGB GetRandom()
        {
            var rnd = new System.Random();
            return new RGB((byte)rnd.Next(0, 255), (byte)rnd.Next(0, 255), (byte)rnd.Next(0, 255));
        }

        public override string ToString()
        {
            return $"R: {R}, G: {G}, B: {B}";
        }
    }
}
