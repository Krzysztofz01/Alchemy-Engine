using AlchemyEngine.Structures;
using System;
using System.Drawing;

namespace AlchemyEngine.Processing
{
    public static class ColorComparer
    {
        public static float Distance(Color a, Color b)
        {
            int redDistance = Math.Abs(a.R - b.R);
            int greenDistance = Math.Abs(a.G - b.G);
            int blueDistance = Math.Abs(a.B - b.B);

            return redDistance + greenDistance + blueDistance;
        }

        public static float Distance(Cmyk a, Cmyk b)
        {
            float cyanDistance = Math.Abs(a.Cyan - b.Cyan);
            float magentaDistance = Math.Abs(a.Magenta - b.Magenta);
            float yelloDistance = Math.Abs(a.Yellow - b.Yellow);
            float keyDistance = Math.Abs(a.Key - b.Key);

            return cyanDistance + magentaDistance + yelloDistance + keyDistance;
        }
    }
}
