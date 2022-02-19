using AlchemyEngine.Structures;
using AlchemyEngine.Extensions;
using System.Drawing;
using Xunit;

namespace AlchemyEngine.Test
{
    public class YuvTest
    {
        [Fact]
        public void ShouldUpdateLumaUsingDefault()
        {
            var yuv = new Yuv(1d, 0d, 0d);

            yuv.SetLuma(0d);

            var expected = 0d;

            Assert.Equal(expected, yuv.Luma);
        }

        [Fact]
        public void ShouldUpdateLumaUsingExpression()
        {
            var yuv = new Yuv(1d, 0d, 0d);

            yuv.SetLuma(v => v - 1d);

            var expected = 0d;

            Assert.Equal(expected, yuv.Luma);
        }

        [Fact]
        public void ShouldUpdateChUUsingDefault()
        {
            var yuv = new Yuv(1d, 0d, 0d);

            yuv.SetChrominanceU(0.1d);

            var expected = 0.1d;

            Assert.Equal(expected, yuv.ChrominanceU);
        }

        [Fact]
        public void ShouldUpdateChUUsingExpression()
        {
            var yuv = new Yuv(1d, 0d, 0d);

            yuv.SetChrominanceU(v => v + 0.1d);

            var expected = 0.1d;

            Assert.Equal(expected, yuv.ChrominanceU);
        }

        [Fact]
        public void ShouldUpdateChVUsingDefault()
        {
            var yuv = new Yuv(1d, 0d, 0d);

            yuv.SetChrominanceV(0.1d);

            var expected = 0.1d;

            Assert.Equal(expected, yuv.ChrominanceV);
        }

        [Fact]
        public void ShouldUpdateChVUsingExpression()
        {
            var yuv = new Yuv(1d, 0d, 0d);

            yuv.SetChrominanceV(v => v + 0.1d);

            var expected = 0.1d;

            Assert.Equal(expected, yuv.ChrominanceV);
        }

        [Fact]
        public void ConstBlackYuvShouldBeBlack()
        {
            var expected = Color.FromArgb(0, 0, 0);

            var actual = Yuv.Black.ToColor();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ConstBlackYuvShouldBeWhite()
        {
            var expected = Color.FromArgb(255, 255, 255);

            var actual = Yuv.White.ToColor();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void FlatRedYuvShouldBeRed()
        {
            var expected = Color.FromArgb(255, 0, 0);

            var actual = new Yuv(0.299d, -0.147d, 0.615d).ToColor();

            Assert.Equal(expected, actual);
        }
        
        [Fact]
        public void FlatGreenYuvShouldBeGreen()
        {
            var expected = Color.FromArgb(0, 255, 0);

            var actual = new Yuv(0.587d, -0.289d, -0.515d).ToColor();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void FlatBlueYuvShouldBeBlue()
        {
            var expected = Color.FromArgb(0, 0, 255);

            var actual = new Yuv(0.114d, 0.436d, -0.10001d).ToColor();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void YuvToYuvShouldApplyNoChanges()
        {
            var expected = new Yuv(0.12d, 0.21d, 0.30d);

            var actual = expected.ToYuv();

            Assert.Equal(expected, actual);
        }
    }
}
