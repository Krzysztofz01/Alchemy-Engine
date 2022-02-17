using AlchemyEngine.Processing;
using System.Drawing;
using Xunit;

namespace AlchemyEngine.Test
{
    public class ColorComparerTest
    {
        [Fact]
        public void ShouldReturnNoDifferenceForTheSameColor()
        {
            var a = Color.FromArgb(255, 128, 64);
            var b = Color.FromArgb(255, 128, 64);

            var expected = 0;

            var acutal = ColorComparer.Distance(a, b);

            Assert.Equal(expected, acutal);
        }

        [Fact]
        public void ShouldReturnCorrectDifferenceBetweenColors()
        {
            var a = Color.FromArgb(0, 0, 0);
            var b = Color.FromArgb(25, 0, 0);

            var expected = 25;

            var actual = ColorComparer.Distance(a, b);

            Assert.Equal(expected, actual);
        }
    }
}
