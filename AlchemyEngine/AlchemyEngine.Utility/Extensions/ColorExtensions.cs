using AlchemyEngine.Utility.Structures;
using AlchemyEngine.Utility.Processing;
using System.Drawing;

namespace AlchemyEngine.Utility.Extensions
{
    public static class ColorExtensions
    {
        public static RGB ToRGB(this Color color)
        {
            return Convert.ColorToRgb(color);
        }

        public static HEX ToHEX(this Color color)
        {
            return Convert.ColorToHex(color);
        }

        public static HSL ToHSL(this Color color)
        {
            return Convert.ColorToHsl(color);
        }

        public static CMYK ToCMYK(this Color color)
        {
            return Convert.ColorToCmyk(color);
        }
    }
}
