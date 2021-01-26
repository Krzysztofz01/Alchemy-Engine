using AlchemyEngine.Utility.Processing;
using System.Drawing;

namespace AlchemyEngine.Utility.Structures
{
    public class HEX
    {
        public string Value { get; set; }

        public HEX()
        {
        }

        public HEX(string hexValue)
        {
            Value = hexValue;
        }

        public HEX(int R, int G, int B)
        {
            Value = $"#{ R.ToString("X2") }{ G.ToString("X2") }{ B.ToString("X2") }";
        }

        public CMYK ToCMYK()
        {
            return Convert.HexToCmyk(this);
        }

        public HSL ToHSL()
        {
            return Convert.HexToHsl(this);
        }

        public RGB ToRGB()
        {
            return Convert.HexToRgb(this);
        }

        public Color ToColor()
        {
            return Convert.HexToColor(this);
        }
    }
}
