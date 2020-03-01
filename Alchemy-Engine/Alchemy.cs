using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Alchemy_Engine
{
    class Alchemy
    {
        /* GLOBAL SETTINGS



        /* CONVERTES */

        

        /* COMPRESSORS */

        /* FILTERS */

        //colorDifferenceFilterCheck method returns true if the first color is to similar to the second color bassed on the threshold value
        private bool colorDifferenceFilterCheck(Color firstColor, Color secondColor, int threshold)
        {
            if(colorDifferenceFilterValue(firstColor, secondColor) > threshold)
            {
                return false;
            }
            return true;
        }

        //colorDifferenceFilterValue method returns a int value representing the difference between two colors
        private int colorDifferenceFilterValue(Color firstColor, Color secondColor)
        {
            int distanceRed = Math.Abs(firstColor.R - secondColor.R);
            int distanceGreen = Math.Abs(firstColor.G - secondColor.G);
            int distanceBlue = Math.Abs(firstColor.B - secondColor.B);

            return distanceRed + distanceGreen + distanceBlue;
        }

        /* ANALYZER */

        
    }
}
