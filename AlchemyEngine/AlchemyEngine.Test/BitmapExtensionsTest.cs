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
            using var bitmapToResize = new Bitmap(10, 10);

            using var bitmapAfterResize = bitmapToResize.Scale(50);

            var expectedSize = 5;

            Assert.Equal(expectedSize, bitmapAfterResize.Height);
            Assert.Equal(expectedSize, bitmapAfterResize.Width);
        }

        [Fact]
        public void BitmapShouldInvert()
        {
            const int size = 10;

            using var bitmapBeforeInvert = new Bitmap(size, size);
            using var bitmapBeforeInvertGraphics = Graphics.FromImage(bitmapBeforeInvert);
            bitmapBeforeInvertGraphics.Clear(Color.FromArgb(255, 255, 255));

            using var bitmapToInvert = new Bitmap(size, size);
            using var bitmapToInvertGraphics = Graphics.FromImage(bitmapToInvert);
            bitmapToInvertGraphics.Clear(Color.FromArgb(0, 0, 0));

            using var invertedBitmap = bitmapToInvert.Invert();

            var expectedPixelColor = Color.FromArgb(255, 255, 255);

            for (int x = 0; x < size; x++)
            {
                for (int y = 0; y < size; y++)
                {
                    Assert.Equal(expectedPixelColor, invertedBitmap.GetPixel(x, y));
                }
            }
        }

        [Fact]
        public void BitmapShouldGrayscale()
        {
            const int size = 10;

            using var bitmapBeforeGrayscale = new Bitmap(size, size);
            using var bitmapBeforeGrayscaleGraphics = Graphics.FromImage(bitmapBeforeGrayscale);
            bitmapBeforeGrayscaleGraphics.Clear(Color.FromArgb(255, 255, 255));

            using var bitmapToGrayscale = new Bitmap(size, size);
            using var bitmapToGrayscaleGraphics = Graphics.FromImage(bitmapToGrayscale);
            bitmapToGrayscaleGraphics.Clear(Color.FromArgb(0, 0, 0));

            using var grayscaleBitmap = bitmapToGrayscale.Grayscale();

            for (int x = 0; x < size; x++)
            {
                for (int y = 0; y < size; y++)
                {
                    var pixelColor = grayscaleBitmap.GetPixel(x, y);

                    Assert.Equal(pixelColor.R, pixelColor.G);
                    Assert.Equal(pixelColor.R, pixelColor.B);
                    Assert.Equal(pixelColor.G, pixelColor.B);
                }
            }
        }

        [Fact]
        public void BitmapShouldGetPalleteUsingCubeMethod()
        {
            const int size = 10;
            
            Color expectedColorOne = Color.FromArgb(255, 128, 128);
            Color expectedColorTwo = Color.FromArgb(64, 128, 32);

            using var bitmap = new Bitmap(size, size);
            using var bitmapGraphics = Graphics.FromImage(bitmap);

            var firstRectangle = new Rectangle(0, 0, size, size / 2);
            bitmapGraphics.FillRectangle(new SolidBrush(expectedColorOne), firstRectangle);

            var secondRectangle = new Rectangle(0, size / 2, size, size / 2);
            bitmapGraphics.FillRectangle(new SolidBrush(expectedColorTwo), secondRectangle);

            var pallete = bitmap.GetPallete(PalleteGenerator.CubeMethod);

            Assert.Contains(expectedColorOne, pallete);
            Assert.Contains(expectedColorTwo, pallete);
        }

        [Fact]
        public void BitmapShouldGetPalleteUsingAdditionMethod()
        {
            const int size = 10;

            Color expectedColorOne = Color.FromArgb(255, 128, 128);
            Color expectedColorTwo = Color.FromArgb(64, 128, 32);

            using var bitmap = new Bitmap(size, size);
            using var bitmapGraphics = Graphics.FromImage(bitmap);

            var firstRectangle = new Rectangle(0, 0, size, size / 2);
            bitmapGraphics.FillRectangle(new SolidBrush(expectedColorOne), firstRectangle);

            var secondRectangle = new Rectangle(0, size / 2, size, size / 2);
            bitmapGraphics.FillRectangle(new SolidBrush(expectedColorTwo), secondRectangle);

            var pallete = bitmap.GetPallete(PalleteGenerator.AdditionMethod);

            Assert.Contains(expectedColorOne, pallete);
            Assert.Contains(expectedColorTwo, pallete);
        }
    }
}
