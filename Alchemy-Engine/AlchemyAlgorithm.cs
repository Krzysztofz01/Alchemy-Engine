using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Media = System.Windows.Media;
using Drawing = System.Drawing;

namespace Alchemy_Engine
{
    class AlchemyAlgorithm
    {
        private static int getRand()
        {
            return new Random(
                Convert.ToInt32(Regex.Match(Guid.NewGuid().ToString(), @"\d+").Value)).Next(0, 255);
        }

        public static Drawing.Color getRandomColor()
        {
            return Drawing.Color.FromArgb(getRand(), getRand(), getRand());
        }

        public static Media.Color getRandomBrushColor()
        {
            return Media.Color.FromRgb(
                (byte)getRand(), 
                (byte)getRand(),
                (byte)getRand());
        }

        public static Media.SolidColorBrush getRandomSolidBrush()
        {
            return new Media.SolidColorBrush(Media.Color.FromRgb(
                (byte)getRand(),
                (byte)getRand(),
                (byte)getRand()));
        }

        public static Media.LinearGradientBrush getRandomLinearGradientBrush()
        {
            return new Media.LinearGradientBrush(
                Media.Color.FromRgb(
                    (byte)getRand(),
                    (byte)getRand(),
                    (byte)getRand()),
                Media.Color.FromRgb(
                    (byte)getRand(),
                    (byte)getRand(),
                    (byte)getRand()),
                90.0);      
        }
        
    }
}
