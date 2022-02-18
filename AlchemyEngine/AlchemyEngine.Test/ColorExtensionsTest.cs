using AlchemyEngine.Structures;
using AlchemyEngine.Extensions;
using System.Drawing;
using Xunit;

namespace AlchemyEngine.Test
{
    public class ColorExtensionsTest
    {
        [Fact]
        public void ShouldReturnUpdatedRedUsingExpression()
        {
            var color = Color.FromArgb(128, 128, 128, 128);

            var updatedColor = color.SetRed(v => v + 12);

            var expected = 140;

            Assert.Equal(expected, updatedColor.R);
        }

        [Fact]
        public void ShouldReturnUpdatedGreenUsingExpression()
        {
            var color = Color.FromArgb(128, 128, 128, 128);

            var updatedColor = color.SetGreen(v => v + 12);

            var expected = 140;

            Assert.Equal(expected, updatedColor.G);
        }

        [Fact]
        public void ShouldReturnUpdatedBlueUsingExpression()
        {
            var color = Color.FromArgb(128, 128, 128, 128);

            var updatedColor = color.SetBlue(v => v + 12);

            var expected = 140;

            Assert.Equal(expected, updatedColor.B);
        }

        [Fact]
        public void ShouldReturnUpdatedAlphaUsingExpression()
        {
            var color = Color.FromArgb(128, 128, 128, 128);

            var updatedColor = color.SetAlpha(v => v + 12);

            var expected = 140;

            Assert.Equal(expected, updatedColor.A);
        }

        [Fact]
        public void ShouldConvertToHexPresentation()
        {
            var color = Color.FromArgb(128, 128, 128);

            var hex = color.ToHex();

            var expected = "#808080";

            Assert.Equal(expected, hex);
        }

        [Fact]
        public void ShouldConvertToHslBlack()
        {
            var color = Color.FromArgb(0, 0, 0);

            var hsl = color.ToHsl();

            Assert.Equal(color, hsl.ToColor());
        }

        [Fact]
        public void ShouldConvertToHslWhite()
        {
            var color = Color.FromArgb(255, 255, 255);

            var hsl = color.ToHsl();

            Assert.Equal(color, hsl.ToColor());
        }

        [Fact]
        public void ShouldConvertToCmykBlack()
        {
            var color = Color.FromArgb(0, 0, 0);

            var cmyk = color.ToCmyk();

            Assert.Equal(color, cmyk.ToColor());
        }

        [Fact]
        public void ShouldConvertToCmykWhite()
        {
            var color = Color.FromArgb(255, 255, 255);

            var cmyk = color.ToCmyk();

            Assert.Equal(color, cmyk.ToColor());
        }
    }
}
