using AlchemyEngine.Structures;
using System.Drawing;
using Xunit;

namespace AlchemyEngine.Test
{
    public class HslTests
    {
        [Fact]
        public void ShouldUpdateHueUsingDefault()
        {
            var hsl = new Hsl(50, .5d, .5d);

            hsl.SetHue(100);

            var expected = 100;

            Assert.Equal(expected, hsl.Hue);
        }

        [Fact]
        public void ShouldUpdateHueUsingExpression()
        {
            var hsl = new Hsl(50, .5d, .5d);

            hsl.SetHue(v => v + 50);

            var expected = 100;

            Assert.Equal(expected, hsl.Hue);
        }

        [Fact]
        public void ShouldUpdateSaturationUsingDefault()
        {
            var hsl = new Hsl(50, .5d, .5d);

            hsl.SetSaturation(1d);

            var expected = 1d;

            Assert.Equal(expected, hsl.Saturation);
        }

        [Fact]
        public void ShouldUpdateSaturationUsingExpression()
        {
            var hsl = new Hsl(50, .5d, .5d);

            hsl.SetSaturation(v => v + .5d);

            var expected = 1d;

            Assert.Equal(expected, hsl.Saturation);
        }

        [Fact]
        public void ShouldUpdateLightnessUsingDefault()
        {
            var hsl = new Hsl(50, .5d, .5d);

            hsl.SetLightness(1d);

            var expected = 1d;

            Assert.Equal(expected, hsl.Lightness);
        }

        [Fact]
        public void ShouldUpdateLightnessUsingExpression()
        {
            var hsl = new Hsl(50, .5d, .5d);

            hsl.SetLightness(v => v + .5d);

            var expected = 1d;

            Assert.Equal(expected, hsl.Lightness);
        }

        [Fact]
        public void ConstWhiteHslShouldBeWhite()
        {
            var expected = Color.FromArgb(255, 255, 255);

            var actual = Hsl.White.ToColor();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ConstBlackHslShouldBeBlack()
        {
            var expected = Color.FromArgb(0, 0, 0);

            var actual = Hsl.Black.ToColor();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void FlatRedHslShouldBeFlatRed()
        {
            var expected = Color.FromArgb(255, 0, 0);

            var actual = new Hsl(0, 1d, .5d).ToColor();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void FlatGreenHslShouldBeFlatGreen()
        {
            var expected = Color.FromArgb(0, 255, 0);

            var actual = new Hsl(120, 1d, .5d).ToColor();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void FlatBlueHslShouldBeFlatBlue()
        {
            var expected = Color.FromArgb(0, 0, 255);

            var actual = new Hsl(240, 1d, .5d).ToColor();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void HslToHslShouldApplyNoChanges()
        {
            var expected = new Hsl(40, .1d, .4d);

            var actual = expected.ToHsl();

            Assert.Equal(expected, actual);
        }
    }
}
