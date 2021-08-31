using AlchemyEngine.Abstraction;
using AlchemyEngine.Extensions;
using System;
using System.Drawing;

namespace AlchemyEngine.Structures
{
    public class Cmyk : IConvertableColor, IRandomColor<Cmyk>
    {
        protected float _cyan;
        protected float _magenta;
        protected float _yellow;
        protected float _key;

        public Cmyk()
        {
        }

        public Cmyk(float cyan, float magenta, float yellow, float keyColor)
        {
            SetCyan(cyan).SetMagenta(magenta).SetYellow(yellow).SetKey(keyColor);
        }

        public static Cmyk White => new Cmyk(0, 0, 0, 0);
        public static Cmyk Black => new Cmyk(0, 0, 0, 1);

        public float Cyan => _cyan;
        public float Magenta => _magenta;
        public float Yellow => _yellow;
        public float Key => _key;

        public Cmyk SetCyan(float value)
        {
            ValidateValue(value);
            _cyan = value;

            return this;
        }

        public Cmyk SetMagenta(float value)
        {
            ValidateValue(value);
            _magenta = value;

            return this;
        }

        public Cmyk SetYellow(float value)
        {
            ValidateValue(value);
            _yellow = value;

            return this;
        }

        public Cmyk SetKey(float value)
        {
            ValidateValue(value);
            _key = value;

            return this;
        }

        protected void ValidateValue(float value)
        {
            if (value > 1f || value < 0f)
                throw new ArgumentException("The component value must be between 0 and 1.", nameof(value));
        }

        public Color ToColor()
        {
            int red = Convert.ToInt32(255f * (1f - _cyan) * (1f - _key));
            int green = Convert.ToInt32(255f * (1f - _magenta) * (1f - _key));
            int blue = Convert.ToInt32(255f * (1f - _yellow) * (1f - _key));

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

        public Cmyk GetRandom()
        {
            var rnd = new Random();

            return new Cmyk()
                .SetCyan((float)rnd.NextDouble())
                .SetMagenta((float)rnd.NextDouble())
                .SetYellow((float)rnd.NextDouble())
                .SetKey((float)rnd.NextDouble());
        }
    }
}
