using AlchemyEngine.Structures;
using System.Drawing;
using Xunit;

namespace AlchemyEngine.Test
{
    public class CmykTests
    {
        [Fact]
        public void ConstWhiteCmykShouldBeWhite()
        {
            var expected = Color.FromArgb(255, 255, 255);

            var actual = Cmyk.White.ToColor();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ConstBlackCmykShouldBeBlack()
        {
            var expected = Color.FromArgb(0, 0, 0);

            var actual = Cmyk.Black.ToColor();

            Assert.Equal(expected, actual);
        }
        
        [Fact]
        public void FlatRedCmykShouldBeFlatRed()
        {
            var expected = Color.FromArgb(255, 0, 0);

            var actual = new Cmyk(0, 1, 1, 0).ToColor();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void FlatGreenCmykShouldBeFlatGreen()
        {
            var expected = Color.FromArgb(0, 255, 0);

            var actual = new Cmyk(1, 0, 1, 0).ToColor();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void FlatBlueCmykShouldBeFlatBlue()
        {
            var expected = Color.FromArgb(0, 0, 255);

            var actual = new Cmyk(1, 1, 0, 0).ToColor();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void CmykToCmykShoudlApplyNoChanges()
        {
            var expected = new Cmyk(1, 1, 0, 0);

            var actual = expected.ToCmyk();

            Assert.Equal(expected, actual);
        }
    }
}
