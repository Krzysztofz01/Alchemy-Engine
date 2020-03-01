using System.Drawing;

namespace Alchemy_Engine
{
    class AlchemyConverter
    {
        //Convert Color object to Hex string
        public static string colorToHex(Color color)
        {
            return "#" + color.R.ToString("X2") + color.G.ToString("X2") + color.B.ToString("X2");
        }

        //Convert Hex string to Color object
        public static Color hexToColor(string color)
        {
            return ColorTranslator.FromHtml(color);
        }
    }
}
