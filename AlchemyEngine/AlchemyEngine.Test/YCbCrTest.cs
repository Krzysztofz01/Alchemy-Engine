using AlchemyEngine.Structures;
using System.Drawing;
using Xunit;

namespace AlchemyEngine.Test
{
    public class YCbCrTest
    {
        [Fact]
        public void StaticWhiteYCbCrShouldByWhiteRgb()
        {
            var whiteYCbCr = YCbCr.White;

            var whiteColor = Color.FromArgb(255, 255, 255);

            Assert.Equal(whiteColor, whiteYCbCr.ToColor());
        }

        [Fact]
        public void StaticBlackYCbCrShouldBeBlackRgb()
        {
            var blackYCbCr = YCbCr.Black;

            var blackColor = Color.FromArgb(0, 0, 0);

            Assert.Equal(blackColor, blackYCbCr.ToColor());
        }

        [Fact]
        public void CustomRedYCbCrShouldBeRedRgb()
        {
            var redYCbCr = new YCbCr(76, 84, 255);

            var redColor = Color.FromArgb(255, 0, 0);

            Assert.Equal(redColor, redYCbCr.ToColor());
        }
    }
}
