using AlchemyEngine.Abstraction;
using AlchemyEngine.Extensions;
using System;
using System.Drawing;

namespace AlchemyEngine.Structures
{
    public class Cmyk : IConvertableColor
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
            _cyan = cyan;
            _magenta = magenta;
            _yellow = yellow;
            _key = keyColor;
        }

        public static Cmyk White => new Cmyk(0, 0, 0, 0);
        public static Cmyk Black => new Cmyk(0, 0, 0, 100);

        public float GetCyan() => _cyan;
        public float GetMagenta() => _magenta;
        public float GetYellow() => _yellow;
        public float GetKey() => _key;

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
    }
}
