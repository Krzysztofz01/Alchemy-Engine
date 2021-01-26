using AlchemyEngine.Utility.Structures;
using System;

namespace AlchemyEngine.Utility.Processing
{
    public static class Filter
    {
        public static int GetDifference(RGB a, RGB b)
        {
            int rDist = Math.Abs(a.R - b.R);
            int gDist = Math.Abs(a.G - b.G);
            int bDist = Math.Abs(b.B - b.B);

            return rDist + gDist + bDist;
        }

        public static bool IsDifferent(RGB a, RGB b, int threshold)
        {
            if (GetDifference(a, b) < threshold) return false;
            return true;
        }
    }
}
