using System;
using System.Drawing;

namespace AlchemyEngine.Utility.Processing
{
    public class Blending
    {
        public static Bitmap BlendFixed(Bitmap a, Bitmap b, BlendingMode mode)
        {
            throw new NotImplementedException();
        }

        public static Bitmap Blend(Bitmap a, Bitmap b, int topOffset, int leftOffset, BlendingMode mode)
        {
            throw new NotImplementedException();
        }

        private static byte ChannelBlendNormal(byte a, byte b)
        {
            return b;
        }

        private static byte ChannelBlendMultiply(byte a, byte b)
        {
            double aDbl = a / 255.0;
            double bDbl = b / 255.0;

            return VerifyRange(aDbl * bDbl);
        }

        private static byte ChannelBlendScreen(byte a, byte b)
        {
            double aDbl = a / 255.0;
            double bDbl = b / 255.0;
            
            return VerifyRange(1.0 - (1.0 - aDbl) * (1.0 - bDbl));
        }

        private static byte ChannelBlendOverlay(byte a, byte b)
        {
            double aDbl = a / 255.0;
            double bDbl = b / 255.0;

            return VerifyRange((aDbl < 0.5) ? 2.0 * aDbl * bDbl : 1.0 - 2.0 * (1.0 - aDbl) * (1.0 - bDbl));
        }

        private static byte ChannelBlendHardLight(byte a, byte b)
        {
            double aDbl = a / 255.0;
            double bDbl = b / 255.0;

            return VerifyRange((aDbl > 0.5) ? 2.0 * aDbl * bDbl : 1.0 - 2.0 * (1.0 - aDbl) * (1.0 - bDbl));
        }

        private static byte ChannelBlendSoftLight(byte a, byte b)
        {
            double aDbl = a / 255.0;
            double bDbl = b / 255.0;

            return VerifyRange(
                (bDbl < 0.5) ? 2.0 * aDbl * bDbl + aDbl * aDbl * (1.0 - 2.0 * bDbl) : 2.0 * aDbl * (1.0 - bDbl) + Math.Sqrt(aDbl) * (2.0 * bDbl - 1.0));
        }

        private static byte VerifyRange(double value)
        {
            double dblRange = Math.Max(Math.Min(value, 1.0), 0.0);
            return (byte)(Math.Max(Math.Min(value * 255.0, 255.0), 0.0));
        }
    }

    public enum BlendingMode
    {
        Normal,
        Multiply,
        Screen,
        Overlay,
        HardLight,
        SoftLight
    }
}