using AlchemyEngine.Abstraction;
using AlchemyEngine.Extensions;
using System;
using System.Drawing;

namespace AlchemyEngine.Structures
{
    public class YCbCr : IConvertableColor, IRandomColor<YCbCr>
    {
        protected int _y;
        protected int _cb;
        protected int _cr;

        public YCbCr()
        {
        }

        public YCbCr(int y, int cb, int cr)
        {
            SetY(y).SetCb(cb).SetCr(cr);
        }

        public static YCbCr White => new YCbCr(255, 128, 128);
        public static YCbCr Black => new YCbCr(0, 128, 128);

        public int Y => _y;
        public int Cb => _cb;
        public int Cr => _cr;

        public YCbCr SetY(int value)
        {
            ValidateValue(value);
            _y = value;

            return this;
        }

        public YCbCr SetCb(int value)
        {
            ValidateValue(value);
            _cb = value;

            return this;
        }

        public YCbCr SetCr(int value)
        {
            ValidateValue(value);
            _cr = value;

            return this;
        }

        protected void ValidateValue(int value)
        {
            if (value > 255 || value < 0)
                throw new ArgumentException("The component value must be between 0 and 255.", nameof(value));
        }

        public Color ToColor()
        {
            int red = (int)(_y + 1.402f * (_cr - 128));
            int green = (int)((_y - 0.344136f) * (_cb - 128) - 0.714136 * (_cr - 128));
            int blue = (int)(_y + 1.772 * (_cb - 128));

            return Color.FromArgb(red, green, blue);
        }

        public Cmyk ToCmyk()
        {
            return ToColor().ToCmyk();
        }

        public Hsl ToHsl()
        {
            return ToColor().ToHsl();
        }

        public YCbCr ToYCbCr()
        {
            return this;
        }

        public override string ToString()
        {
            return $"Y: {_y} Cb: {_cb} Cr: {_cr}";
        }

        public override bool Equals(object obj)
        {
            return obj != null &&
                _y == ((YCbCr)obj)._y &&
                _cb == ((YCbCr)obj)._cb &&
                _cr == ((YCbCr)obj)._cr;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public YCbCr GetRandom()
        {
            var rnd = new Random();

            return new YCbCr()
                .SetY(rnd.Next(0, 255))
                .SetCb(rnd.Next(0, 255))
                .SetCr(rnd.Next(0, 255));
        }
    }
}
