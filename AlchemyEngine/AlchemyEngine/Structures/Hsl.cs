using AlchemyEngine.Abstraction;
using AlchemyEngine.Extensions;
using System;
using System.Drawing;

namespace AlchemyEngine.Structures
{
    public class Hsl : IConvertableColor
    {
        protected int _precision = 2;

        protected int _hue;
        protected double _saturation;
        protected double _lightness;

        public Hsl()
        {
        }

        public Hsl(int hue, double saturation, double lightness)
        {
            SetHue(hue).SetSaturation(saturation).SetLightness(lightness);
        }

        public static Hsl White => new Hsl(0, 0d, 1d);
        public static Hsl Black => new Hsl(0, 0d, 0d);

        public int Hue => _hue;
        public double Saturation => _saturation;
        public double Lightness => _lightness;

        public Hsl SetHue(Func<int, int> expression) => SetHue(expression(_hue));
        public Hsl SetHue(int value)
        {
            ValidateHueValue(value);
            _hue = value;

            return this;
        }

        public Hsl SetSaturation(Func<double, double> expression) => SetSaturation(expression(_saturation));
        public Hsl SetSaturation(double value)
        {
            value = ApplyPrecision(value);

            ValidateDoubleValue(value);
            _saturation = value;

            return this;
        }

        public Hsl SetLightness(Func<double, double> expression) => SetLightness(expression(_lightness));
        public Hsl SetLightness(double value)
        {
            value = ApplyPrecision(value);

            ValidateDoubleValue(value);
            _lightness = value;

            return this;
        }

        protected void ValidateDoubleValue(double value)
        {
            if (value > 1d || value < 0d)
                throw new ArgumentException("The component value must be between 0 and 1.", nameof(value));
        }

        protected void ValidateHueValue(int value)
        {
            if (value > 360 || value < 0)
                throw new ArgumentException("The hue value must be between 0 and 360.", nameof(value));
        }

        protected double ApplyPrecision(double value)
        {
            return Math.Round(value, _precision);
        }

        protected double HueToRgbValues(double vA, double vB, double vCH)
        {
            double value = vA;

            if (vCH < 0) vCH += 1.0d;
            if (vCH > 1) vCH -= 1.0d;

            if (vCH < 1.0 / 6) value = (vA + (vB - vA) * 6.0d * vCH);
            else if (vCH < 1.0 / 2) value = vB;
            else if (vCH < 2.0 / 3) value = (vA + (vB - vA) * ((2.0d / 3.0d) - vCH) * 6.0d);

            return value;
        }

        public Color ToColor()
        {
            double dr = 0.0d;
            double dg = 0.0d;
            double db = 0.0d;

            if (_saturation == 0)
            {
                dr = dg = db = _lightness;
            }
            else
            {
                double q = (_lightness < 0.5d)
                ? _lightness * (1.0 + _saturation)
                : _lightness + _saturation - _lightness * _saturation;

                double p = 2 * _lightness - q;

                double dHue = _hue / 360.0d;

                dr = (double)HueToRgbValues(p, q, dHue + 1.0d / 3.0d);
                dg = (double)HueToRgbValues(p, q, dHue);
                db = (double)HueToRgbValues(p, q, dHue - 1.0d / 3.0d);
            }

            int r = (int)Math.Round(dr * 255);
            int g = (int)Math.Round(dg * 255);
            int b = (int)Math.Round(db * 255);

            return Color.FromArgb(r, g, b);
        }

        public Cmyk ToCmyk()
        {
            return ToColor().ToCmyk();
        }

        public Hsl ToHsl()
        {
            return this;
        }

        public YCbCr ToYCbCr()
        {
            return ToColor().ToYCbCr();
        }

        public override string ToString()
        {
            return $"H: {_hue} S: {_saturation} L:{_lightness}";
        }

        public override bool Equals(object obj)
        {
            return obj != null &&
                _hue == ((Hsl)obj)._hue &&
                _saturation == ((Hsl)obj)._saturation &&
                _lightness == ((Hsl)obj)._lightness;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
