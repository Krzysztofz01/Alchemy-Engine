using AlchemyEngine.Structures;
using AlchemyEngine.Extensions;
using System.Drawing;
using Xunit;

namespace AlchemyEngine.Test
{
    public class ColorExtensionsTest
    {
        [Fact]
        public void CustomWhiteRgbShouldBeWhiteCmyk()
        {
            var whiteCmyk = Cmyk.White;

            var whiteColor = Color.FromArgb(255, 255, 255);

            Assert.Equal(whiteCmyk, whiteColor.ToCmyk());
        }

        [Fact]
        public void CustomWhiteRgbShouldBeWhiteHsl()
        {
            var whiteHsl = Hsl.White;

            var whiteColor = Color.FromArgb(255, 255, 255);

            Assert.Equal(whiteHsl, whiteColor.ToHsl());
        }

        [Fact]
        public void CustomWhiteRgbShouldBeWhiteYCbCr()
        {
            var whiteYCbCr = YCbCr.White;

            var whiteColor = Color.FromArgb(255, 255, 255);

            Assert.Equal(whiteYCbCr, whiteColor.ToYCbCr());
        }

        [Fact]
        public void CustomBlackRgbShouldBeBlackCmyk()
        {
            var blackCmyk = Cmyk.Black;

            var blackColor = Color.FromArgb(0, 0, 0);

            Assert.Equal(blackCmyk, blackColor.ToCmyk());
        }

        [Fact]
        public void CustomBlackRgbShouldBeBlackHsl()
        {
            var blackHsl = Hsl.Black;

            var blackColor = Color.FromArgb(0, 0, 0);

            Assert.Equal(blackHsl, blackColor.ToHsl());
        }

        [Fact]
        public void CustomBlackRgbShouldBeBlackYCbCr()
        {
            var blackYCbCr = YCbCr.Black;

            var blackColor = Color.FromArgb(0, 0, 0);

            Assert.Equal(blackYCbCr, blackColor.ToYCbCr());
        }

        [Fact]
        public void CustomRedRgbShouldBeRedCmyk()
        {
            var redCmyk = new Cmyk(0, 1, 1, 0);

            var redColor = Color.FromArgb(255, 0, 0);

            Assert.Equal(redCmyk, redColor.ToCmyk());
        }

        [Fact]
        public void CustomRedRgbShouldBeRedHsl()
        {
            var redHsl = new Hsl(0, 1.0f, 0.5f);

            var redColor = Color.FromArgb(255, 0, 0);

            Assert.Equal(redHsl, redColor.ToHsl());
        }

        [Fact]
        public void CustomRedRgbShouldBeRedYCbCr()
        {
            var redYCbCr = new YCbCr(76, 84, 255);

            var redColor = Color.FromArgb(255, 0, 0);

            Assert.Equal(redYCbCr, redColor.ToYCbCr());
        }
    }
}
