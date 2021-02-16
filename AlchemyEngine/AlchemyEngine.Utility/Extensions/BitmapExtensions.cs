using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace AlchemyEngine.Utility.Extensions
{
    public static class BitmapExtensions
    {
        /// <summary>
        /// Scale an image by a percentage
        /// </summary>
        /// <param name="bmp">Target</param>
        /// <param name="percent">Percent</param>
        /// <returns></returns>
        public static Bitmap Scale(this Bitmap bmp, int percent)
        {
            percent = Math.Min(Math.Max(percent, 0), 100);
            int width = (bmp.Width * percent) / 100;
            int height = (bmp.Height * percent) / 100;
            return new Bitmap(bmp, new Size(width, height));
        }

        /// <summary>
        /// Invert colors
        /// </summary>
        /// <param name="bmp">Target</param>
        /// <returns></returns>
        public static Bitmap Invert(this Bitmap bmp)
        {
            Bitmap image = new Bitmap(bmp, new Size(bmp.Width, bmp.Height));
            BitmapData bitmapData = image.LockBits(new Rectangle(0, 0, image.Width, image.Height), ImageLockMode.ReadWrite, image.PixelFormat);
            int bytesPerPixel = Bitmap.GetPixelFormatSize(image.PixelFormat) / 8;
            int byteCount = bitmapData.Stride * image.Height;
            byte[] pixels = new byte[byteCount];
            IntPtr ptrFirstPixel = bitmapData.Scan0;
            Marshal.Copy(ptrFirstPixel, pixels, 0, pixels.Length);
            int heightInPixels = bitmapData.Height;
            int widthInBytes = bitmapData.Width * bytesPerPixel;

            for(int y = 0; y < heightInPixels; y++)
            {
                int currentLine = y * bitmapData.Stride;
                for(int x = 0; x < widthInBytes; x += bytesPerPixel)
                {
                    pixels[currentLine + x + 2] = (byte)(255 - pixels[currentLine + x + 2]);
                    pixels[currentLine + x + 1] = (byte)(255 - pixels[currentLine + x + 1]);
                    pixels[currentLine + x] = (byte)(255 - pixels[currentLine + x]);
                }
            }

            Marshal.Copy(pixels, 0, ptrFirstPixel, pixels.Length);
            image.UnlockBits(bitmapData);
            return image;
        }

        /// <summary>
        /// Convert image colors to monochrome (shades of gray)
        /// </summary>
        /// <param name="bmp">Target</param>
        /// <returns></returns>
        public static Bitmap Grayscale(this Bitmap bmp)
        {
            Bitmap image = new Bitmap(bmp, new Size(bmp.Width, bmp.Height));
            BitmapData bitmapData = image.LockBits(new Rectangle(0, 0, image.Width, image.Height), ImageLockMode.ReadWrite, image.PixelFormat);
            int bytesPerPixel = Bitmap.GetPixelFormatSize(image.PixelFormat) / 8;
            int byteCount = bitmapData.Stride * image.Height;
            byte[] pixels = new byte[byteCount];
            IntPtr ptrFirstPixel = bitmapData.Scan0;
            Marshal.Copy(ptrFirstPixel, pixels, 0, pixels.Length);
            int heightInPixels = bitmapData.Height;
            int widthInBytes = bitmapData.Width * bytesPerPixel;

            byte red, green, blue;

            for (int y = 0; y < heightInPixels; y++)
            {
                int currentLine = y * bitmapData.Stride;
                for (int x = 0; x < widthInBytes; x += bytesPerPixel)
                {
                    red = pixels[currentLine + x + 2];
                    green = pixels[currentLine + x + 1];
                    blue = pixels[currentLine + x];

                    pixels[currentLine + x] = pixels[currentLine + x + 1] = pixels[currentLine + x + 2] = (byte)(.299 * red + .587 * green + .114 * blue);
                }
            }

            Marshal.Copy(pixels, 0, ptrFirstPixel, pixels.Length);
            image.UnlockBits(bitmapData);
            return image;
        }

        /// <summary>
        /// Change the brightness of the image
        /// </summary>
        /// <param name="bmp">Target</param>
        /// <param name="value">The highlight value, values ​​from -100 to 100</param>
        /// <returns></returns>
        public static Bitmap Brightness(this Bitmap bmp, int value)
        {
            if (value < -100 || value > 100) throw new Exception("Value needs to be in range batween -100 and 100");

            Bitmap image = new Bitmap(bmp, new Size(bmp.Width, bmp.Height));
            BitmapData bitmapData = image.LockBits(new Rectangle(0, 0, image.Width, image.Height), ImageLockMode.ReadWrite, image.PixelFormat);
            int bytesPerPixel = Bitmap.GetPixelFormatSize(image.PixelFormat) / 8;
            int byteCount = bitmapData.Stride * image.Height;
            byte[] pixels = new byte[byteCount];
            IntPtr ptrFirstPixel = bitmapData.Scan0;
            Marshal.Copy(ptrFirstPixel, pixels, 0, pixels.Length);
            int heightInPixels = bitmapData.Height;
            int widthInBytes = bitmapData.Width * bytesPerPixel;

            int nVal;

            for (int y = 0; y < heightInPixels; y++)
            {
                int currentLine = y * bitmapData.Stride;
                for (int x = 0; x < widthInBytes; x += bytesPerPixel)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        nVal = pixels[currentLine + x + i] + value;
                        if (nVal < 0) nVal = 0;
                        if (nVal > 255) nVal = 255;
                        pixels[currentLine + x + i] = (byte)nVal;
                    }
                }
            }

            Marshal.Copy(pixels, 0, ptrFirstPixel, pixels.Length);
            image.UnlockBits(bitmapData);
            return image;
        }

        /// <summary>
        /// Change the contrast of the image
        /// </summary>
        /// <param name="bmp">Target</param>
        /// <param name="value">The contrast value, values ​​from -100 to 100</param>
        /// <returns></returns>
        public static Bitmap Contrast(this Bitmap bmp, int value)
        {
            if (value < -100 || value > 100) throw new Exception("Value needs to be in range batween -100 and 100");

            Bitmap image = new Bitmap(bmp, new Size(bmp.Width, bmp.Height));
            BitmapData bitmapData = image.LockBits(new Rectangle(0, 0, image.Width, image.Height), ImageLockMode.ReadWrite, image.PixelFormat);
            int bytesPerPixel = Bitmap.GetPixelFormatSize(image.PixelFormat) / 8;
            int byteCount = bitmapData.Stride * image.Height;
            byte[] pixels = new byte[byteCount];
            IntPtr ptrFirstPixel = bitmapData.Scan0;
            Marshal.Copy(ptrFirstPixel, pixels, 0, pixels.Length);
            int heightInPixels = bitmapData.Height;
            int widthInBytes = bitmapData.Width * bytesPerPixel;

            double pxl;
            double contrast = (100.0 + value) / 100.0;
            contrast *= contrast;

            for (int y = 0; y < heightInPixels; y++)
            {
                int currentLine = y * bitmapData.Stride;
                for (int x = 0; x < widthInBytes; x += bytesPerPixel)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        pxl = pixels[currentLine + x + i] / 255.0;
                        pxl -= 0.5;
                        pxl *= contrast;
                        pxl += 0.5;
                        pxl *= 255;

                        if (pxl < 0) pxl = 0;
                        if (pxl > 255) pxl = 255;
                        pixels[currentLine + x + i] = (byte)pxl;
                    }
                }
            }

            Marshal.Copy(pixels, 0, ptrFirstPixel, pixels.Length);
            image.UnlockBits(bitmapData);
            return image;
        }

        /// <summary>
        /// Extracting one of the channels
        /// </summary>
        /// <param name="bmp">Target</param>
        /// <param name="channel">Choose one of the three channels based on enuma Channels</param>
        /// <returns></returns>
        public static Bitmap Channel(this Bitmap bmp, Channels channel)
        {
            Bitmap image = new Bitmap(bmp, new Size(bmp.Width, bmp.Height));
            BitmapData bitmapData = image.LockBits(new Rectangle(0, 0, image.Width, image.Height), ImageLockMode.ReadWrite, image.PixelFormat);
            int bytesPerPixel = Bitmap.GetPixelFormatSize(image.PixelFormat) / 8;
            int byteCount = bitmapData.Stride * image.Height;
            byte[] pixels = new byte[byteCount];
            IntPtr ptrFirstPixel = bitmapData.Scan0;
            Marshal.Copy(ptrFirstPixel, pixels, 0, pixels.Length);
            int heightInPixels = bitmapData.Height;
            int widthInBytes = bitmapData.Width * bytesPerPixel;

            for (int y = 0; y < heightInPixels; y++)
            {
                int currentLine = y * bitmapData.Stride;
                for (int x = 0; x < widthInBytes; x += bytesPerPixel)
                {
                    if (channel == Channels.Blue)
                    {
                        pixels[currentLine + x + 2] = 0;
                        pixels[currentLine + x + 1] = 0;
                    }

                    if (channel == Channels.Green)
                    {
                        pixels[currentLine + x + 2] = 0;
                        pixels[currentLine + x] = 0;
                    }

                    if (channel == Channels.Red)
                    {
                        pixels[currentLine + x + 1] = 0;
                        pixels[currentLine + x] = 0;
                    }
                }
            }

            Marshal.Copy(pixels, 0, ptrFirstPixel, pixels.Length);
            image.UnlockBits(bitmapData);
            return image;
        }

        private static Bitmap ApplyConvolutionFilter(Bitmap sourceBitmap, ConvFilterSettings filter)
        {
            BitmapData sourceData = sourceBitmap.LockBits(
                new Rectangle(0, 0, sourceBitmap.Width, sourceBitmap.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);

            int bufferSize = sourceData.Stride * sourceData.Height;
            byte[] pixelBuffer = new byte[bufferSize];
            byte[] resultBuffer = new byte[bufferSize];

            Marshal.Copy(sourceData.Scan0, pixelBuffer, 0, pixelBuffer.Length);
            sourceBitmap.UnlockBits(sourceData);

            double blue;
            double green;
            double red;

            int filterWidth = filter.Matrix.GetLength(1);
            int filterHeight = filter.Matrix.GetLength(0);

            int filterOffset = (filterWidth - 1) / 2;
            int calcOffset;

            int byteOffset;

            for(int offsetY = filterOffset; offsetY < sourceBitmap.Height - filterOffset; offsetY++)
            {
                for(int offsetX = filterOffset; offsetX < sourceBitmap.Width - filterOffset; offsetX++)
                {
                    blue = green = red = 0;
                    byteOffset = offsetY * sourceData.Stride + offsetX * 4;

                    for(int filterY = -filterOffset; filterY <= filterOffset; filterY++)
                    {
                        for(int filterX = -filterOffset; filterX <= filterOffset; filterX++)
                        {
                            calcOffset = byteOffset + (filterX * 4) + (filterY * sourceData.Stride);

                            blue += (double)(pixelBuffer[calcOffset]) * filter.Matrix[filterY + filterOffset, filterX + filterOffset];

                            green += (double)(pixelBuffer[calcOffset + 1]) * filter.Matrix[filterY + filterOffset, filterX + filterOffset];

                            red += (double)(pixelBuffer[calcOffset + 2]) * filter.Matrix[filterY + filterOffset, filterX + filterOffset];
                        }
                    }

                    blue = filter.Factor * blue + filter.Bias;
                    if (blue > 255) blue = 255;
                    if (blue < 0) blue = 0;

                    green = filter.Factor * green + filter.Bias;
                    if (green > 255) green = 255;
                    if (green < 0) green = 0;

                    red = filter.Factor * red + filter.Bias;
                    if (red > 255) red = 255;
                    if (red < 0) red = 0;


                    resultBuffer[byteOffset] = (byte)(blue);
                    resultBuffer[byteOffset + 1] = (byte)(green);
                    resultBuffer[byteOffset + 2] = (byte)(red);
                    resultBuffer[byteOffset + 3] = 255;
                }
            }

            Bitmap resultBitmap = new Bitmap(sourceBitmap.Width, sourceBitmap.Height);

            BitmapData resultData = resultBitmap.LockBits(
                new Rectangle(0, 0, resultBitmap.Width, resultBitmap.Height), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);

            Marshal.Copy(resultBuffer, 0, resultData.Scan0, resultBuffer.Length);
            resultBitmap.UnlockBits(resultData);

            return resultBitmap;
        }

        public static Bitmap Blur(this Bitmap bmp, Kernel kernel)
        {
            if(kernel == Kernel.Conv3x3)
            {
                var filterSettings = new ConvFilterSettings();
                filterSettings.Factor = 1.0;
                filterSettings.Bias = 0.0;
                filterSettings.Matrix =
                    new double[,] { { 0.0, 0.2, 0.0 },
                                    { 0.2, 0.2, 0.2 },
                                    { 0.0, 0.2, 0.2 }, };

                return ApplyConvolutionFilter(bmp, filterSettings);
            }
            else if(kernel == Kernel.Conv5x5)
            {
                var filterSettings = new ConvFilterSettings();
                filterSettings.Factor = 1.0 / 13.0;
                filterSettings.Bias = 0.0;
                filterSettings.Matrix =
                    new double[,] { { 0, 0, 1, 0, 0, },
                                    { 0, 1, 1, 1, 0, },
                                    { 1, 1, 1, 1, 1, },
                                    { 0, 1, 1, 1, 0, },
                                    { 0, 0, 1, 0, 0, }, };

                return ApplyConvolutionFilter(bmp, filterSettings);
            }
            else if(kernel == Kernel.Conv7x7)
            {
                throw new NotImplementedException();
            }

            return null;
        }

        public static Bitmap GaussianBlur(this Bitmap bmp, Kernel kernel)
        {
            if(kernel == Kernel.Conv3x3)
            {
                var filterSettings = new ConvFilterSettings();
                filterSettings.Factor = 1.0 / 16.0;
                filterSettings.Bias = 0.0;
                filterSettings.Matrix =
                    new double[,] { { 1, 2, 1, },
                                    { 2, 4, 2, },
                                    { 1, 2, 1, }, };

                return ApplyConvolutionFilter(bmp, filterSettings);
            }
            else if(kernel == Kernel.Conv5x5)
            {
                var filterSettings = new ConvFilterSettings();
                filterSettings.Factor = 1.0 / 159.0;
                filterSettings.Bias = 0.0;
                filterSettings.Matrix =
                    new double[,] { { 2, 04, 05, 04, 2, },
                                    { 4, 09, 12, 09, 4, },
                                    { 5, 12, 15, 12, 5, },
                                    { 4, 09, 12, 09, 4, },
                                    { 2, 04, 05, 04, 2, }, };

                return ApplyConvolutionFilter(bmp, filterSettings);
            }
            else if(kernel == Kernel.Conv7x7)
            {
                throw new NotImplementedException();
            }
            return null;
        }

        public static Bitmap MotionBlur(this Bitmap bmp)
        {
            var filterSettings = new ConvFilterSettings();
            filterSettings.Factor = 1.0 / 18.0;
            filterSettings.Bias = 0.0;
            filterSettings.Matrix =
                new double[,] { { 1, 0, 0, 0, 0, 0, 0, 0, 1, },
                                { 0, 1, 0, 0, 0, 0, 0, 1, 0, },
                                { 0, 0, 1, 0, 0, 0, 1, 0, 0, },
                                { 0, 0, 0, 1, 0, 1, 0, 0, 0, },
                                { 0, 0, 0, 0, 1, 0, 0, 0, 0, },
                                { 0, 0, 0, 1, 0, 1, 0, 0, 0, },
                                { 0, 0, 1, 0, 0, 0, 1, 0, 0, },
                                { 0, 1, 0, 0, 0, 0, 0, 1, 0, },
                                { 1, 0, 0, 0, 0, 0, 0, 0, 1, }, };

            return ApplyConvolutionFilter(bmp, filterSettings);
        }

        public static Bitmap Soften(this Bitmap bmp, Kernel kernel)
        {
            if (kernel == Kernel.Conv3x3)
            {
                var filterSettings = new ConvFilterSettings();
                filterSettings.Factor = 1.0 / 8.0;
                filterSettings.Bias = 0.0;
                filterSettings.Matrix =
                    new double[,] { { 1, 1, 1, },
                                    { 1, 1, 1, },
                                    { 1, 1, 1, }, };

                return ApplyConvolutionFilter(bmp, filterSettings);
            }
            else if (kernel == Kernel.Conv5x5)
            {
                throw new NotImplementedException();
            }
            else if (kernel == Kernel.Conv7x7)
            {
                throw new NotImplementedException();
            }
            return null;
        }

        public static Bitmap Sharpen(this Bitmap bmp, Kernel kernel)
        {
            if (kernel == Kernel.Conv3x3)
            {
                var filterSettings = new ConvFilterSettings();
                filterSettings.Factor = 1.0 / 3.0;
                filterSettings.Bias = 0.0;
                filterSettings.Matrix =
                    new double[,] { {  0, -2,  0, },
                                    { -2, 11, -2, },
                                    {  0, -2,  0, }, };

                return ApplyConvolutionFilter(bmp, filterSettings);
            }
            else if (kernel == Kernel.Conv5x5)
            {
                var filterSettings = new ConvFilterSettings();
                filterSettings.Factor = 1.0 / 3.0;
                filterSettings.Bias = 0.0;
                filterSettings.Matrix =
                    new double[,] { { -1, -1, -1, -1, -1, },
                                    { -1,  2,  2,  2, -1, },
                                    { -1,  2,  8,  2,  1, },
                                    { -1,  2,  2,  2, -1, },
                                    { -1, -1, -1, -1, -1, }, };

                return ApplyConvolutionFilter(bmp, filterSettings);
            }
            else if (kernel == Kernel.Conv7x7)
            {
                throw new NotImplementedException();
            }
            return null;
        }

        public static Bitmap EdgeDetection(this Bitmap bmp)
        {
            var filterSettings = new ConvFilterSettings();
            filterSettings.Factor = 1.0;
            filterSettings.Bias = 0.0;
            filterSettings.Matrix =
                new double[,] { { -1, -1, -1, },
                                { -1,  8, -1, },
                                { -1, -1, -1, }, };

            return ApplyConvolutionFilter(bmp, filterSettings);
        }

        public static Bitmap Emboss(this Bitmap bmp)
        {
            var filterSettings = new ConvFilterSettings();
            filterSettings.Factor = 1.0;
            filterSettings.Bias = 128.0;
            filterSettings.Matrix =
                new double[,] { { 2,  0,  0, },
                                { 0, -1,  0, },
                                { 0,  0, -1, }, };

            return ApplyConvolutionFilter(bmp, filterSettings);
        }

        public static Bitmap HighPass(this Bitmap bmp)
        {
            var filterSettings = new ConvFilterSettings();
            filterSettings.Factor = 1.0 / 16.0;
            filterSettings.Bias = 128.0;
            filterSettings.Matrix =
                new double[,] { { -1, -2, -1, },
                                { -2, 12, -2, },
                                { -1, -2, -1, } };

            return ApplyConvolutionFilter(bmp, filterSettings);
        }
    }

    public class ConvFilterSettings
    {
        public double Factor { get; set; }
        public double Bias { get; set; }
        public double[,] Matrix { get; set; }
    }

    public enum Channels
    {
        Red,
        Green,
        Blue
    }

    public enum Kernel
    {
        Conv3x3,
        Conv5x5,
        Conv7x7
    }
}
