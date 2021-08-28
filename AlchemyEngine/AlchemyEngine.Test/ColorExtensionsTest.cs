using AlchemyEngine.Structures;
using AlchemyEngine.Extensions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AlchemyEngine.Test
{
    public class ColorExtensionsTest
    {
        [Fact]
        public void CustomWhiteRgbShouldBeWhiteCmyk()
        {
            var whiteCmyk = Cmyk.White;

            var whiteColor = Color.FromArgb(255, 255, 255);

            Assert.Equal(whiteCmyk, whiteColor.ToCmyk());
        }

        [Fact]
        public void CustomBlackRgbShouldBeBlackCmyk()
        {
            var blackCmyk = Cmyk.Black;

            var blackColor = Color.FromArgb(0, 0, 0);

            Assert.Equal(blackCmyk, blackColor.ToCmyk());
        }
        
        [Fact]
        public void CustomRedRgbShouldBeRedCmyk()
        {
            var redCmyk = new Cmyk(0, 1, 1, 0);

            var redColor = Color.FromArgb(255, 0, 0);

            Assert.Equal(redCmyk, redColor.ToCmyk());
        }
    }
}
