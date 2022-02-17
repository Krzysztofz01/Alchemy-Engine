using AlchemyEngine.Processing;
using System.Drawing;
using Xunit;

namespace AlchemyEngine.Test
{
    public class ColorBlenderTest
    {
        [Fact]
        public void ShouldBlendNormal()
        {
            var a = Color.FromArgb(255, 128, 128);
            var b = Color.FromArgb(0, 128, 64);

            var expected = Color.FromArgb(0, 128, 64);

            var actual = ColorBlender.Blend(a, b, BlendingMode.Normal);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ShouldBlendDarken()
        {
            var a = Color.FromArgb(255, 128, 128);
            var b = Color.FromArgb(0, 128, 64);

            var expected = Color.FromArgb(0, 128, 64);

            var actual = ColorBlender.Blend(a, b, BlendingMode.Darken);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ShouldBlendMultiply()
        {
            var a = Color.FromArgb(255, 128, 128);
            var b = Color.FromArgb(0, 128, 64);

            var expected = Color.FromArgb(0, 64, 32);

            var actual = ColorBlender.Blend(a, b, BlendingMode.Multiply);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ShouldBlendScreen()
        {
            var a = Color.FromArgb(255, 128, 128);
            var b = Color.FromArgb(0, 128, 64);

            var expected = Color.FromArgb(255, 191, 159);

            var actual = ColorBlender.Blend(a, b, BlendingMode.Screen);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ShouldBlendOverlay()
        {
            var a = Color.FromArgb(255, 128, 128);
            var b = Color.FromArgb(0, 128, 64);

            var expected = Color.FromArgb(255, 128, 64);

            var actual = ColorBlender.Blend(a, b, BlendingMode.Overlay);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ShouldBlendSoftLight()
        {
            var a = Color.FromArgb(255, 128, 128);
            var b = Color.FromArgb(0, 128, 64);

            var expected = Color.FromArgb(255, 128, 96);

            var actual = ColorBlender.Blend(a, b, BlendingMode.SoftLight);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ShouldBlendHardLight()
        {
            var a = Color.FromArgb(255, 128, 128);
            var b = Color.FromArgb(0, 128, 64);

            var expected = Color.FromArgb(0, 128, 64);

            var actual = ColorBlender.Blend(a, b, BlendingMode.HardLight);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ShouldBlendDifference()
        {
            var a = Color.FromArgb(255, 128, 128);
            var b = Color.FromArgb(0, 128, 64);

            var expected = Color.FromArgb(255, 0, 64);

            var actual = ColorBlender.Blend(a, b, BlendingMode.Difference);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ShouldBlendDivide()
        {
            var a = Color.FromArgb(255, 128, 128);
            var b = Color.FromArgb(0, 128, 64);

            var expected = Color.FromArgb(255, 255, 255);

            var actual = ColorBlender.Blend(a, b, BlendingMode.Divide);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ShouldBlendAddition()
        {
            var a = Color.FromArgb(255, 128, 128);
            var b = Color.FromArgb(0, 128, 64);

            var expected = Color.FromArgb(255, 255, 192);

            var actual = ColorBlender.Blend(a, b, BlendingMode.Addition);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ShouldBlendSubtract()
        {
            var a = Color.FromArgb(255, 128, 128);
            var b = Color.FromArgb(0, 128, 64);

            var expected = Color.FromArgb(255, 0, 64);

            var actual = ColorBlender.Blend(a, b, BlendingMode.Subtract);

            Assert.Equal(expected, actual);
        }
    }
}
