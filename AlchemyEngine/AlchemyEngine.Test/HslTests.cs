using AlchemyEngine.Structures;
using System.Drawing;
using Xunit;

namespace AlchemyEngine.Test
{
    public class HslTests
    {
        [Fact]
        public void StaticWhiteHslShouldBeWhiteRgb()
        {
            var whiteHsl = Hsl.White;

            var whiteColor = Color.FromArgb(255, 255, 255);

            Assert.Equal(whiteColor, whiteHsl.ToColor());
        }

        [Fact]
        public void StaticWhiteHslShuldBeWhiteCmyk()
        {
            var whiteHsl = Hsl.White;

            var whiteCmyk = Cmyk.White;

            Assert.Equal(whiteCmyk, whiteHsl.ToCmyk());
        }

        [Fact]
        public void StaticBlackHslhouldBeBlackRgb()
        {
            var blackHsl = Hsl.Black;

            var blackColor = Color.FromArgb(0, 0, 0);

            Assert.Equal(blackColor, blackHsl.ToColor());
        }

        [Fact]
        public void StaticBlackHslShouldBeBlackCmyk()
        {
            var blackHsl = Hsl.Black;

            var blackCmyk = Cmyk.Black;

            Assert.Equal(blackCmyk, blackHsl.ToCmyk());
        }

        [Fact]
        public void CustomRedHslShouldBeRedRgb()
        {
            var redHsl = new Hsl(0, 1.0f, 0.5f);

            var redColor = Color.FromArgb(255, 0, 0);

            Assert.Equal(redColor, redHsl.ToColor());
        }

        [Fact]
        public void CustomRedHslShouldBeRedCmyk()
        {
            var redHsl = new Hsl(0, 1.0f, 0.5f);

            var redCmyk = new Cmyk(0.0f, 1.0f, 1.0f, 0.0f);

            Assert.Equal(redCmyk, redHsl.ToCmyk());
        }
    }
}
