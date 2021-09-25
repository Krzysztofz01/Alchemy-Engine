using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace AlchemyEngine.Processing
{
    public static class ColorBlender
    {
        public static Color Blend(Color a, Color b, BlendingMode blendingMode)
        {
            switch(blendingMode)
            {
                case BlendingMode.Normal: return BlendNormal(a, b);
                case BlendingMode.Darken: return BlendDarken(a, b);
                default: throw new ArgumentOutOfRangeException(nameof(blendingMode));
            }
        }

        private static Color BlendNormal(Color a, Color b)
        {
            return a;
        }

        private static Color BlendDarken(Color a, Color b)
        {
            return Color.FromArgb(
                Math.Min(a.R, b.R),
                Math.Min(a.G, b.G),
                Math.Min(a.B, b.B));
        }


    }

    public enum BlendingMode
    {
        Normal,
        Darken,
        Multiply,
        ColorBurn,
        LinearBurn,
        Lighten,
        Screen,
        ColorDodge,
        LinearDodge,
        Overlay,
        SoftLight,
        HardLight,
        VividLight,
        LinearLight,
        PinLight,
        Difference,
        Exclusion
    }
}
