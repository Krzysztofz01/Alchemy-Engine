using System.Drawing;
using AlchemyEngine.Extensions;
using Xunit;

namespace AlchemyEngine.Test
{
    public class BitmapExtensionsTest
    {
        [Fact]
        public void BitampShouldScaleByGivenPercent()
        {
            var bitmapWithExpectedSize = new Bitmap(50, 50);

            var bitmapBeforeResize = new Bitmap(100, 100);

            var bitmapAfterResize = bitmapBeforeResize.Scale(50);

            Assert.Equal(bitmapWithExpectedSize.Height, bitmapAfterResize.Height);
            Assert.Equal(bitmapWithExpectedSize.Width, bitmapAfterResize.Width);
        }
    }
}
