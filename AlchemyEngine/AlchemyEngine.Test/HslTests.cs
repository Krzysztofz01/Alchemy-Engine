using AlchemyEngine.Structures;
using System.Drawing;
using Xunit;

namespace AlchemyEngine.Test
{
    public class HslTests
    {
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
