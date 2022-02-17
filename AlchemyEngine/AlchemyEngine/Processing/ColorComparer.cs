using AlchemyEngine.Abstraction;
using System;
using System.Drawing;

namespace AlchemyEngine.Processing
{
    public static class ColorComparer
    {
        public static double Distance(IConvertableColor a, IConvertableColor b)
        {
            return Distance(a.ToColor(), b.ToColor());
        }

        public static double Distance(Color a, IConvertableColor b)
        {
            return Distance(a, b.ToColor());
        }

        public static double Distance(Color a, Color b)
        {
            int redDistance = Math.Abs(a.R - b.R);
            int greenDistance = Math.Abs(a.G - b.G);
            int blueDistance = Math.Abs(a.B - b.B);

            return redDistance + greenDistance + blueDistance;
        }
    }
}
