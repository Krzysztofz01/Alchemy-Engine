using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alchemy_Engine
{
    class AlchemyFilter
    {
        public AlchemyFilter()
        {

        }

        //INPUT: Two Color objects and filter threshold
        //OUTPUT: True if these colors are to similar according to the threshold
        public bool filterColorDifference(Color firstColor, Color secondColor, int threshold)
        {
            if (colorDifference(firstColor, secondColor) > threshold)
            {
                return false;
            }
            return true;
        }

        //INPUT: Two Color objects
        //OUTPUT: Integer value representing the difference between these two colors
        public int colorDifference(Color firstColor, Color secondColor)
        {
            int distanceRed = Math.Abs(firstColor.R - secondColor.R);
            int distanceGreen = Math.Abs(firstColor.G - secondColor.G);
            int distanceBlue = Math.Abs(firstColor.B - secondColor.B);

            return distanceRed + distanceGreen + distanceBlue;
        }

    }
}
