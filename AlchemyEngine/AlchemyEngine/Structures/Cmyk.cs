using AlchemyEngine.Abstraction;
using AlchemyEngine.Extensions;
using System;
using System.Drawing;

namespace AlchemyEngine.Structures
{
    public class Cmyk : IConvertableColor
    {
        protected int _precision = 2;

        protected double _cyan;
        protected double _magenta;
        protected double _yellow;
        protected double _key;

        public Cmyk()
        {
        }

        public Cmyk(double cyan, double magenta, double yellow, double keyColor)
        {
            SetCyan(cyan).SetMagenta(magenta).SetYellow(yellow).SetKey(keyColor);
        }

        public static Cmyk White => new Cmyk(0, 0, 0, 0);
        public static Cmyk Black => new Cmyk(0, 0, 0, 1);

        public double Cyan => _cyan;
        public double Magenta => _magenta;
        public double Yellow => _yellow;
        public double Key => _key;

        public Cmyk SetCyan(double value)
        {
            value = ApplyPrecision(value);

            ValidateValue(value);
            _cyan = value;

            return this;
        }

        public Cmyk SetMagenta(double value)
        {
            value = ApplyPrecision(value);

            ValidateValue(value);
            _magenta = value;

            return this;
        }

        public Cmyk SetYellow(double value)
        {
            value = ApplyPrecision(value);

            ValidateValue(value);
            _yellow = value;

            return this;
        }

        public Cmyk SetKey(double value)
        {
            value = ApplyPrecision(value);

            ValidateValue(value);
            _key = value;

            return this;
        }

        protected void ValidateValue(double value)
        {
            if (value > 1d || value < 0d)
                throw new ArgumentException("The component value must be between 0 and 1.", nameof(value));
        }

        protected double ApplyPrecision(double value)
        {
            return Math.Round(value, _precision);
        }

        public Color ToColor()
        {
            int red = Convert.ToInt32(255d * (1d - _cyan) * (1d - _key));
            int green = Convert.ToInt32(255d * (1d - _magenta) * (1d - _key));
            int blue = Convert.ToInt32(255d * (1d - _yellow) * (1d - _key));

            return Color.FromArgb(red, green, blue);
        }

        public Cmyk ToCmyk()
        {
            return this;
        }

        public Hsl ToHsl()
        {
            return ToColor().ToHsl();
        }

        public YCbCr ToYCbCr()
        {
            return ToColor().ToYCbCr();
        }

        public override string ToString()
        {
            return $"C: {_cyan} M: {_magenta} Y: {_yellow} K: {_key}";
        }

        public override bool Equals(object obj)
        {
            return obj != null &&
                _cyan == ((Cmyk)obj)._cyan &&
                _yellow == ((Cmyk)obj)._yellow &&
                _magenta == ((Cmyk)obj)._magenta &&
                _key == ((Cmyk)obj)._key;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
