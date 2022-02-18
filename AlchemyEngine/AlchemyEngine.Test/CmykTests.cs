using AlchemyEngine.Structures;
using System.Drawing;
using Xunit;

namespace AlchemyEngine.Test
{
    public class CmykTests
    {
        [Fact]
        public void ShouldUpdateCyanUsingDefault()
        {
            var cmyk = new Cmyk(.2, .2, .2, .2);

            cmyk.SetCyan(.4);

            var expected = .4;

            Assert.Equal(expected, cmyk.Cyan);
        }

        [Fact]
        public void ShouldUpdateCyanUsingExpression()
        {
            var cmyk = new Cmyk(.2, .2, .2, .2);

            cmyk.SetCyan(c => c + .2d);

            var expected = .4;

            Assert.Equal(expected, cmyk.Cyan);
        }

        [Fact]
        public void ShouldUpdateMagentaUsingDefault()
        {
            var cmyk = new Cmyk(.2, .2, .2, .2);

            cmyk.SetMagenta(.4);

            var expected = .4;

            Assert.Equal(expected, cmyk.Magenta);
        }

        [Fact]
        public void ShouldUpdateMagentaUsingExpression()
        {
            var cmyk = new Cmyk(.2, .2, .2, .2);

            cmyk.SetMagenta(c => c + .2d);

            var expected = .4;

            Assert.Equal(expected, cmyk.Magenta);
        }

        [Fact]
        public void ShouldUpdateYellowUsingDefault()
        {
            var cmyk = new Cmyk(.2, .2, .2, .2);

            cmyk.SetYellow(.4);

            var expected = .4;

            Assert.Equal(expected, cmyk.Yellow);
        }

        [Fact]
        public void ShouldUpdateYellowUsingExpression()
        {
            var cmyk = new Cmyk(.2, .2, .2, .2);

            cmyk.SetYellow(c => c + .2d);

            var expected = .4;

            Assert.Equal(expected, cmyk.Yellow);
        }

        [Fact]
        public void ShouldUpdateKeyUsingDefault()
        {
            var cmyk = new Cmyk(.2, .2, .2, .2);

            cmyk.SetKey(.4);

            var expected = .4;

            Assert.Equal(expected, cmyk.Key);
        }

        [Fact]
        public void ShouldUpdateKeyUsingExpression()
        {
            var cmyk = new Cmyk(.2, .2, .2, .2);

            cmyk.SetKey(c => c + .2d);

            var expected = .4;

            Assert.Equal(expected, cmyk.Key);
        }

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
