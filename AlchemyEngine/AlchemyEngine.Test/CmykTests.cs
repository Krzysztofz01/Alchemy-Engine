using AlchemyEngine.Structures;
using System.Drawing;
using Xunit;

namespace AlchemyEngine.Test
{
    public class CmykTests
    {
        [Fact]
        public void StaticWhiteCmykShouldBeWhiteRgb()
        {
            var whiteColor = Color.FromArgb(255, 255, 255);

            var whiteCmyk = Cmyk.White;

            Assert.Equal(whiteColor, whiteCmyk.ToColor());
        }

        [Fact]
        public void StaticBlackCmykShouldBeWhiteRgb()
        {
            var blackColor = Color.FromArgb(0, 0, 0);

            var blackCmyk = Cmyk.Black;

            Assert.Equal(blackColor, blackCmyk.ToColor());
        }

        [Fact]
        public void CustomRedCmykShouldBeRedRgb()
        {
            var redColor = Color.FromArgb(255, 0, 0);

            var redCmyk = new Cmyk(0, 1, 1, 0);

            Assert.Equal(redColor, redCmyk.ToColor());
        }
    }
}
