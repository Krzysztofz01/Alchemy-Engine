using AlchemyEngine.Abstraction;
using AlchemyEngine.Extensions;
using System;
using System.Drawing;

namespace AlchemyEngine.Structures
{
    public class Hsl : IConvertableColor
    {
        //Values 0-100 0-1 0-360
        protected float _hue;
        protected float _saturation;
        protected float _lightness;

        public Hsl()
        {
        }

        public Hsl(float hue, float saturation, float lightness)
        {
            _hue = hue;
            _saturation = saturation;
            _lightness = lightness;
        }

        public static Hsl White => new Hsl(0, 0, 100);
        public static Hsl Black => new Hsl(0, 0, 0);

        public float GetHue() => _hue;
        public float GetSaturation() => _saturation;
        public float GetLightness() => _lightness;

        public Hsl SetHue(float value)
        {
            ValidateValue(value);
            _hue = value;

            return this;
        }

        public Hsl SetSaturation(float value)
        {
            ValidateValue(value);
            _saturation = value;

            return this;
        }

        public Hsl SetLightness(float value)
        {
            ValidateValue(value);
            _lightness = value;

            return this;
        }

        protected void ValidateValue(float value)
        {
            if (value > 1f || value < 0f)
                throw new ArgumentException("The component value must be between 0 and 1.", nameof(value));
        }

        public Color ToColor()
        {
            throw new NotImplementedException();
        }

        public Cmyk ToCmyk()
        {
            return ToColor().ToCmyk();
        }

        public Hsl ToHsl()
        {
            return this;
        }
    }
}
