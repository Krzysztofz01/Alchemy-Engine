using AlchemyEngine.Abstraction;
using AlchemyEngine.Extensions;
using System;
using System.Drawing;

namespace AlchemyEngine.Processing
{
    public static class ColorBlender
    {
        public static Color Blend(IConvertableColor a, IConvertableColor b, BlendingMode blendingMode)
        {
            return Blend(a.ToColor(), b.ToColor(), blendingMode);
        }

        public static Color Blend(Color a, Color b, BlendingMode blendingMode)
        {
            return blendingMode switch
            {
                BlendingMode.Normal => BlendNormal(a, b),
                BlendingMode.Darken => BlendDarken(a, b),
                BlendingMode.Multiply => BlendMultiply(a, b),
                BlendingMode.Screen => BlendScreen(a, b),
                BlendingMode.Overlay => BlendOverlay(a, b),
                BlendingMode.SoftLight => BlendSoftLight(a, b),
                BlendingMode.HardLight => BlendHardLight(a, b),
                BlendingMode.Divide => BlendDivide(a, b),
                BlendingMode.Addition => BlendAddition(a, b),
                BlendingMode.Subtract => BlendSubtract(a, b),
                BlendingMode.Difference => BlendDifference(a, b),
                _ => throw new ArgumentOutOfRangeException(nameof(blendingMode)),
            };
        }

        private static Color BlendNormal(Color _, Color b)
        {
            return b;
        }

        private static Color BlendDarken(Color a, Color b)
        {
            return Color.FromArgb(
                Math.Min(a.R, b.R),
                Math.Min(a.G, b.G),
                Math.Min(a.B, b.B));
        }

        private static Color BlendMultiply(Color a, Color b)
        {
            double red = a.RedInterval() * b.RedInterval();
            double green = a.GreenInterval() * b.GreenInterval();
            double blue = a.BlueInterval() * b.BlueInterval();

            return Color.FromArgb((int)(red * 255), (int)(green * 255), (int)(blue * 255));
        }

        private static Color BlendScreen(Color a, Color b)
        {
            double red = 1.0d - (1.0d - a.RedInterval()) * (1.0d - b.RedInterval());
            double green = 1.0d - (1.0d - a.GreenInterval()) * (1.0d - b.GreenInterval());
            double blue = 1.0d - (1.0d - a.BlueInterval()) * (1.0d - b.BlueInterval());

            return Color.FromArgb((int)(red * 255), (int)(green * 255), (int)(blue *255));
        }

        private static Color BlendOverlay(Color a, Color b)
        {
            double red = (a.RedInterval() < 0.5d)
                ? 2.0d * a.RedInterval() * b.RedInterval()
                : 1.0d - 2.0d * (1.0d - a.RedInterval()) * (1.0d - b.RedInterval());

            double green = (a.GreenInterval() < 0.5d)
                ? 2.0d * a.GreenInterval() * b.GreenInterval()
                : 1.0d - 2.0d * (1.0d - a.GreenInterval()) * (1.0d - b.GreenInterval());

            double blue = (a.BlueInterval() < 0.5d)
                ? 2.0d * a.BlueInterval() * b.BlueInterval()
                : 1.0d - 2.0d * (1.0d - a.BlueInterval()) * (1.0d - b.BlueInterval());

            return Color.FromArgb((int)(red * 255), (int)(green * 255), (int)(blue * 255));
        }

        private static Color BlendHardLight(Color a, Color b)
        {
            double red = (a.RedInterval() > 0.5d)
                ? 2.0d * a.RedInterval() * b.RedInterval()
                : 1.0d - 2.0d * (1.0d - a.RedInterval()) * (1.0d - b.RedInterval());

            double green = (a.GreenInterval() > 0.5d)
                ? 2.0d * a.GreenInterval() * b.GreenInterval()
                : 1.0d - 2.0d * (1.0d - a.GreenInterval()) * (1.0d - b.GreenInterval());

            double blue = (a.BlueInterval() > 0.5d)
                ? 2.0d * a.BlueInterval() * b.BlueInterval()
                : 1.0d - 2.0d * (1.0d - a.BlueInterval()) * (1.0d - b.BlueInterval());

            return Color.FromArgb((int)(red * 255), (int)(green * 255), (int)(blue * 255));
        }

        private static Color BlendSoftLight(Color a, Color b)
        {
            double red = (1.0d - 2.0d * b.RedInterval()) * Math.Pow(a.RedInterval(), 2) + 2.0d * a.RedInterval() * b.RedInterval();
            double green = (1.0d - 2.0d * b.GreenInterval()) * Math.Pow(a.GreenInterval(), 2) + 2.0d * a.GreenInterval() * b.GreenInterval();
            double blue = (1.0d - 2.0d * b.BlueInterval()) * Math.Pow(a.BlueInterval(), 2) + 2.0d * a.BlueInterval() * b.BlueInterval();

            return Color.FromArgb((int)(red * 255), (int)(green * 255), (int)(blue * 255));
        }

        private static Color BlendDivide(Color a, Color b)
        {
            double red = (b.RedInterval() == 0)
                ? a.RedInterval()
                : a.RedInterval() / b.RedInterval();

            double green = (b.GreenInterval() == 0)
                ? a.GreenInterval()
                : a.GreenInterval() / b.GreenInterval();

            double blue = (b.BlueInterval() == 0)
                ? a.BlueInterval()
                : a.BlueInterval() / b.BlueInterval();

            return Color.FromArgb(
                CorrectComponentRange((int)(red * 255)),
                CorrectComponentRange((int)(green * 255)),
                CorrectComponentRange((int)(blue * 255)));
        }

        private static Color BlendAddition(Color a, Color b)
        {
            double red = a.RedInterval() + b.RedInterval();
            double green = a.GreenInterval() + b.GreenInterval();
            double blue = a.BlueInterval() + b.BlueInterval();

            return Color.FromArgb(
                CorrectComponentRange((int)(red * 255)),
                CorrectComponentRange((int)(green * 255)),
                CorrectComponentRange((int)(blue * 255)));
        }

        private static Color BlendSubtract(Color a, Color b)
        {
            double red = a.RedInterval() - b.RedInterval();
            double green = a.GreenInterval() - b.GreenInterval();
            double blue = a.BlueInterval() - b.BlueInterval();

            return Color.FromArgb(
                CorrectComponentRange((int)(red * 255)),
                CorrectComponentRange((int)(green * 255)),
                CorrectComponentRange((int)(blue * 255)));
        }

        private static Color BlendDifference(Color a, Color b)
        {
            double red = Math.Abs(a.RedInterval() - b.RedInterval());
            double green = Math.Abs(a.GreenInterval() - b.GreenInterval());
            double blue = Math.Abs(a.BlueInterval() - b.BlueInterval());

            return Color.FromArgb(
                CorrectComponentRange((int)(red * 255)),
                CorrectComponentRange((int)(green * 255)),
                CorrectComponentRange((int)(blue * 255)));
        }

        private static int CorrectComponentRange(int a)
        {
            return Math.Min(255, Math.Max(0, a));
        }
    }

    public enum BlendingMode
    {
        Normal,
        Darken,
        Multiply,
        Screen,
        Overlay,
        SoftLight,
        HardLight,
        Difference,
        Divide,
        Addition,
        Subtract
    }
}
