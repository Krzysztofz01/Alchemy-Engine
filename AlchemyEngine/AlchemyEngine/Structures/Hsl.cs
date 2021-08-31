using AlchemyEngine.Abstraction;
using AlchemyEngine.Extensions;
using System;
using System.Drawing;

namespace AlchemyEngine.Structures
{
    public class Hsl : IConvertableColor, IRandomColor<Hsl>
    {
        protected int _hue;
        protected float _saturation;
        protected float _lightness;

        public Hsl()
        {
        }

        public Hsl(int hue, float saturation, float lightness)
        {
            SetHue(hue).SetSaturation(saturation).SetLightness(lightness);
        }

        public static Hsl White => new Hsl(0, 0f, 1f);
        public static Hsl Black => new Hsl(0, 0f, 0f);

        public int Hue => _hue;
        public float Saturation => _saturation;
        public float Lightness => _lightness;

        public Hsl SetHue(int value)
        {
            ValidateHueValue(value);
            _hue = value;

            return this;
        }

        public Hsl SetSaturation(float value)
        {
            ValidateFloatValue(value);
            _saturation = value;

            return this;
        }

        public Hsl SetLightness(float value)
        {
            ValidateFloatValue(value);
            _lightness = value;

            return this;
        }

        protected void ValidateFloatValue(float value)
        {
            if (value > 1f || value < 0f)
                throw new ArgumentException("The component value must be between 0 and 1.", nameof(value));
        }

        protected void ValidateHueValue(int value)
        {
            if (value > 360 || value < 0)
                throw new ArgumentException("The hue value must be between 0 and 360.", nameof(value));
        }

        public Color ToColor()
        {
            int red, green, blue;

            if (_saturation == 0)
            {
                red = green = blue = (int)(_lightness * 255);
            }
            else
            {
                float fHue = _hue * 360.0f;

                float vB = (_lightness < 0.5f) ?
                    (_lightness * (1 + _saturation)) : ((_lightness + _saturation) - (_lightness * _saturation));

                float vA = 2 * _lightness - vB;

                red = (int)(255 * HueToRgbValues(vA, vB, fHue + (1.0f / 3)));
                green = (int)(255 * HueToRgbValues(vA, vB, fHue));
                blue = (int)(255 * HueToRgbValues(vA, vB, fHue - (1.0f / 3)));
            }

            return Color.FromArgb(red, green, blue);
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

        protected float HueToRgbValues(float vA, float vB, float vCH)
        {
            if (vCH < 0) vCH += 1;
            if (vCH > 1) vCH -= 1;

            if ((6 * vCH) < 1) return (vA + (vB - vA) * 6 * vCH);
            if ((2 * vCH) < 1) return vB;
            if ((3 * vCH) < 2) return (vA + (vB - vA) * ((2.0f / 3) - vCH) * 6);

            return vA;
        }

        public Hsl GetRandom()
        {
            var rnd = new Random();

            return new Hsl()
                .SetHue(rnd.Next(0, 365))
                .SetSaturation((float)rnd.NextDouble())
                .SetLightness((float)rnd.NextDouble());
        }
    }
}
