using AlchemyEngine.Utility.Structures;
using System.Drawing;
using Xunit;

namespace AlchemyEngine.Tests
{
    public class RGBTests
    {
        [Fact]
        public void RgbToHexShouldConvert()
        {
            HEX expected = new HEX("#FFFFFF");

            HEX actual = new RGB(255, 255, 255).ToHEX();

            Assert.Equal(expected.Value, actual.Value);
        }

        [Fact]
        public void RgbToCmykShouldConvert()
        {
            CMYK expected = new CMYK(0, 0, 0, 0);

            CMYK actual = new RGB(255, 255, 255).ToCMYK();

            Assert.Equal(expected.C, actual.C);
            Assert.Equal(expected.M, actual.M);
            Assert.Equal(expected.Y, actual.Y);
            Assert.Equal(expected.K, actual.K);
        }

        [Fact]
        public void RgbToHslShouldConvert()
        {
            HSL expected = new HSL(360, 0, 1);

            HSL actual = new RGB(255, 255, 255).ToHSL();

            Assert.Equal(expected.H, actual.H);
            Assert.Equal(expected.S, actual.S);
            Assert.Equal(expected.L, actual.L);
        }

        [Fact]
        public void RgbToColorShouldConvert()
        {
            Color expected = Color.FromArgb(255, 255, 255);

            Color actual = new RGB(255, 255, 255).ToColor();

            Assert.Equal(expected, actual);
        }
    }
}
