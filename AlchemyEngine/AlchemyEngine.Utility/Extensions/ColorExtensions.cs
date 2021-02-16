using AlchemyEngine.Utility.Structures;
using AlchemyEngine.Utility.Processing;
using System.Drawing;

namespace AlchemyEngine.Utility.Extensions
{
    public static class ColorExtensions
    {
        /// <summary>
        /// Get a RGB object based on given Color object value
        /// </summary>
        /// <param name="color">Target</param>
        /// <returns></returns>
        public static RGB ToRGB(this Color color)
        {
            return Convert.ColorToRgb(color);
        }

        /// <summary>
        /// Get a HEX object based on given Color object value
        /// </summary>
        /// <param name="color">Target</param>
        /// <returns></returns>
        public static HEX ToHEX(this Color color)
        {
            return Convert.ColorToHex(color);
        }

        /// <summary>
        /// Get a HSL object based on given Color object value
        /// </summary>
        /// <param name="color">Target</param>
        /// <returns></returns>
        public static HSL ToHSL(this Color color)
        {
            return Convert.ColorToHsl(color);
        }

        /// <summary>
        /// Get a CMYK object based on given Color object value
        /// </summary>
        /// <param name="color">Target</param>
        /// <returns></returns>
        public static CMYK ToCMYK(this Color color)
        {
            return Convert.ColorToCmyk(color);
        }
    }
}
