using AlchemyEngine.Abstraction;
using AlchemyEngine.Extensions;
using System;
using System.Drawing;

namespace AlchemyEngine.Structures
{
    public class Yuv : IConvertableColor
    {
        protected readonly int _precision = 4;
        protected readonly double _chrominanceLimitU = 0.436d;
        protected readonly double _chrominanceLimitV = 0.615d;

        protected double _luma;
        protected double _chrominanceU;
        protected double _chrominanceV;

        public Yuv()
        {
        }

        public Yuv(double luma, double uChrominance, double vChrominance)
        {
            SetLuma(luma).SetChrominanceU(uChrominance).SetChrominanceV(vChrominance);
        }

        public static Yuv White => new Yuv(1.0d, 0.0d, 0.0d);
        public static Yuv Black => new Yuv(0.0d, 0.0d, 0.0d);

        public double Luma => _luma;
        public double ChrominanceU => _chrominanceU;
        public double ChrominanceV => _chrominanceV;

        public Yuv SetLuma(Func<double, double> expression) => SetLuma(expression(_luma));
        public Yuv SetLuma(double value)
        {
            value = ApplyPrecision(value);

            ValidateLumaValue(value);
            _luma = value;

            return this;
        }

        public Yuv SetChrominanceU(Func<double, double> expression) => SetChrominanceU(expression(_chrominanceU));
        public Yuv SetChrominanceU(double value)
        {
            value = ApplyPrecision(value);

            ValidateChrominanceUValue(value);
            _chrominanceU = value;

            return this;
        }

        public Yuv SetChrominanceV(Func<double, double> expression) => SetChrominanceV(expression(_chrominanceV));
        public Yuv SetChrominanceV(double value)
        {
            value = ApplyPrecision(value);

            ValidateChrominanceVValue(value);
            _chrominanceV = value;

            return this;
        }

        protected void ValidateLumaValue(double value)
        {
            if (value > 1.0d || value < 0.0d)
                throw new ArgumentException("The component value must be between 0 and 1.", nameof(value));
        }

        protected void ValidateChrominanceUValue(double value)
        {
            if (value > _chrominanceLimitU || value < -_chrominanceLimitU)
                throw new ArgumentException($"The component value must be between -{_chrominanceLimitU} and {_chrominanceLimitU}.", nameof(value));
        }

        protected void ValidateChrominanceVValue(double value)
        {
            if (value > _chrominanceLimitV || value < -_chrominanceLimitV)
                throw new ArgumentException($"The component value must be between -{_chrominanceLimitV} and {_chrominanceLimitV}.", nameof(value));
        }

        protected double ApplyPrecision(double value)
        {
            return Math.Round(value, _precision);
        }

        public Cmyk ToCmyk()
        {
            return ToColor().ToCmyk();
        }

        public Color ToColor()
        {
            double red = 1.0d * _luma + 0.0d * _chrominanceU + 1.140d * _chrominanceV;
            double green = 1.0d * _luma + -0.39465d * _chrominanceU + -0.581d * _chrominanceV;
            double blue = 1.0d * _luma + 2.03211d * _chrominanceU + 0.0d * _chrominanceV;

            return Color.FromArgb(
                (int)(red * 255.0d),
                (int)(green * 255.0d),
                (int)(blue * 255.0d));
        }

        public Hsl ToHsl()
        {
            return ToColor().ToHsl();
        }

        public YCbCr ToYCbCr()
        {
            throw new NotImplementedException();
        }

        public Yuv ToYuv()
        {
            return this;
        }

        public override string ToString()
        {
            return $"Y: {_luma} U: {_chrominanceU} V:{_chrominanceV}";
        }

        public override bool Equals(object obj)
        {
            return obj != null &&
                _luma == ((Yuv)obj)._luma &&
                _chrominanceU == ((Yuv)obj)._chrominanceU &&
                _chrominanceV == ((Yuv)obj)._chrominanceV;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
