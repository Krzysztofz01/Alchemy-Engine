using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace AlchemyEngine.Utility.Extensions
{
    public static class BitmapExtensions
    {
        public static Bitmap Scale(this Bitmap bmp, int percent)
        {
            int width = (bmp.Width * percent) / 100;
            int height = (bmp.Height * percent) / 100;
            return new Bitmap(bmp, new Size(width, height));
        }

        public static Bitmap Invert(this Bitmap bmp)
        {
            Bitmap image = bmp;
            BitmapData bmpData = image.LockBits(new Rectangle(0, 0, image.Width, image.Height), ImageLockMode.ReadWrite, image.PixelFormat);
            int bytesPerPixel = Bitmap.GetPixelFormatSize(image.PixelFormat) / 8;
            int byteCount = bmpData.Stride * image.Height;
            byte[] pixels = new byte[byteCount];
            IntPtr firstPixelPtr = bmpData.Scan0;
            Marshal.Copy(firstPixelPtr, pixels, 0, pixels.Length);
            int heightInPixels = bmpData.Height;
            int widthInPixels = bmpData.Width;

            for(int y = 0; y < heightInPixels; y++)
            {
                int currentLine = y * bmpData.Stride;
                for(int x = 0; x < widthInPixels; x += bytesPerPixel)
                {
                    pixels[currentLine + x + 2] = (byte)(255 - pixels[currentLine + x + 2]);
                    pixels[currentLine + x + 1] = (byte)(255 - pixels[currentLine + x + 1]);
                    pixels[currentLine + x] = (byte)(255 - pixels[currentLine + x]);
                }
            }

            image.UnlockBits(bmpData);
            return image;
        }

        public static Bitmap Grayscale(this Bitmap bmp)
        {
            Bitmap image = bmp;
            BitmapData bmpData = image.LockBits(new Rectangle(0, 0, image.Width, image.Height), ImageLockMode.ReadWrite, image.PixelFormat);
            int bytesPerPixel = Bitmap.GetPixelFormatSize(image.PixelFormat) / 8;
            int byteCount = bmpData.Stride * image.Height;
            byte[] pixels = new byte[byteCount];
            IntPtr firstPixelPtr = bmpData.Scan0;
            Marshal.Copy(firstPixelPtr, pixels, 0, pixels.Length);
            int heightInPixels = bmpData.Height;
            int widthInPixels = bmpData.Width;

            byte red, green, blue;
            for (int y = 0; y < heightInPixels; y++)
            {
                int currentLine = y * bmpData.Stride;
                for (int x = 0; x < widthInPixels; x += bytesPerPixel)
                {
                    red = pixels[currentLine + x + 2];
                    green = pixels[currentLine + x + 1];
                    blue = pixels[currentLine + x];

                    pixels[currentLine + x] = pixels[currentLine + x + 1] = pixels[currentLine + x + 2] = (byte)(.299 * red + .587 * green + .114 * blue);
                }
            }

            image.UnlockBits(bmpData);
            return image;
        }

        public static Bitmap Brightness(this Bitmap bmp, int value)
        {
            Bitmap image = bmp;
            BitmapData bmpData = image.LockBits(new Rectangle(0, 0, image.Width, image.Height), ImageLockMode.ReadWrite, image.PixelFormat);
            int bytesPerPixel = Bitmap.GetPixelFormatSize(image.PixelFormat) / 8;
            int byteCount = bmpData.Stride * image.Height;
            byte[] pixels = new byte[byteCount];
            IntPtr firstPixelPtr = bmpData.Scan0;
            Marshal.Copy(firstPixelPtr, pixels, 0, pixels.Length);
            int heightInPixels = bmpData.Height;
            int widthInPixels = bmpData.Width;

            int nVal;
            for (int y = 0; y < heightInPixels; y++)
            {
                int currentLine = y * bmpData.Stride;
                for (int x = 0; x < widthInPixels; x += bytesPerPixel)
                {
                    for(int i = 0; i < 3; i++)
                    {
                        nVal = pixels[currentLine + x + i] + value;
                        if (nVal < 0) nVal = 0;
                        if (nVal > 255) nVal = 255;
                        pixels[currentLine + x + i] = (byte)nVal;
                    }
                }
            }

            image.UnlockBits(bmpData);
            return image;
        }

        public static Bitmap Contrast(this Bitmap bmp, int value)
        {
            if (value < -100 || value > 100) throw new Exception("Value needs to be in range batween -100 and 100");

            Bitmap image = bmp;
            BitmapData bmpData = image.LockBits(new Rectangle(0, 0, image.Width, image.Height), ImageLockMode.ReadWrite, image.PixelFormat);
            int bytesPerPixel = Bitmap.GetPixelFormatSize(image.PixelFormat) / 8;
            int byteCount = bmpData.Stride * image.Height;
            byte[] pixels = new byte[byteCount];
            IntPtr firstPixelPtr = bmpData.Scan0;
            Marshal.Copy(firstPixelPtr, pixels, 0, pixels.Length);
            int heightInPixels = bmpData.Height;
            int widthInPixels = bmpData.Width;

            double pxl = 0;
            double contrast = (100.0 + value) / 100.0;
            for (int y = 0; y < heightInPixels; y++)
            {
                int currentLine = y * bmpData.Stride;
                for (int x = 0; x < widthInPixels; x += bytesPerPixel)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        pxl = pixels[currentLine + x + i] / 255.0;
                        pxl -= 0.5;
                        pxl *= contrast;
                        pxl *= 255;

                        if (pxl < 0) pxl = 0;
                        if (pxl > 255) pxl = 255;
                        pixels[currentLine + x + i] = (byte)pxl;
                    }
                }
            }

            image.UnlockBits(bmpData);
            return image;
        }

        public static Bitmap Channel(this Bitmap bmp, Channels channel)
        {
            Bitmap image = bmp;
            BitmapData bmpData = image.LockBits(new Rectangle(0, 0, image.Width, image.Height), ImageLockMode.ReadWrite, image.PixelFormat);
            int bytesPerPixel = Bitmap.GetPixelFormatSize(image.PixelFormat) / 8;
            int byteCount = bmpData.Stride * image.Height;
            byte[] pixels = new byte[byteCount];
            IntPtr firstPixelPtr = bmpData.Scan0;
            Marshal.Copy(firstPixelPtr, pixels, 0, pixels.Length);
            int heightInPixels = bmpData.Height;
            int widthInPixels = bmpData.Width;

            for (int y = 0; y < heightInPixels; y++)
            {
                int currentLine = y * bmpData.Stride;
                for (int x = 0; x < widthInPixels; x += bytesPerPixel)
                {
                    if(channel == Channels.Blue)
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

            image.UnlockBits(bmpData);
            return image;
        }

        private static Bitmap Conv3x3(Bitmap bmp, ConvMatrix m)
        {
            if (m.Factor == 0) throw new Exception("Matrix Factor is set to 0");

            var b = bmp;
            var bSrc = (Bitmap)bmp.Clone();
            BitmapData bmData = b.LockBits(new Rectangle(0, 0, b.Width, b.Height), ImageLockMode.ReadWrite, b.PixelFormat);
            BitmapData bmSrc = bSrc.LockBits(new Rectangle(0, 0, bSrc.Width, bSrc.Height), ImageLockMode.ReadWrite, bSrc.PixelFormat);

            int stride = bmData.Stride;
            int stride2 = stride * 2;
            IntPtr Scan0 = bmData.Scan0;
            IntPtr SrcScan0 = bmSrc.Scan0;
            int byteCount = stride * b.Height;
            byte[] p = new byte[byteCount];
            Marshal.Copy(Scan0, p, 0, p.Length);
            int srcByteCount = stride * bSrc.Height;
            byte[] pSrc = new byte[srcByteCount];
            Marshal.Copy(SrcScan0, pSrc, 0, pSrc.Length);
            int nOffset = stride - b.Width * 3;
            int nWidth = b.Width - 2;
            int nHeight = b.Height - 2;
            int baseIndex = 0;

            int nPixel;

            for(int y = 0; y < nHeight; ++y)
            {
                for(int x = 0; x < nWidth; ++x)
                {
                    nPixel = ((((pSrc[baseIndex + 2] * m.TopLeft) +
                    (pSrc[baseIndex + 5] * m.TopMid) +
                    (pSrc[baseIndex + 8] * m.TopRight) +
                    (pSrc[baseIndex + 2 + stride] * m.MidLeft) +
                    (pSrc[baseIndex + 5 + stride] * m.Pixel) +
                    (pSrc[baseIndex + 8 + stride] * m.MidRight) +
                    (pSrc[baseIndex + 2 + stride2] * m.BotLeft) +
                    (pSrc[baseIndex + 5 + stride2] * m.BotMid) +
                    (pSrc[baseIndex + 8 + stride2] * m.BotRight))
                    / m.Factor) + m.Offset);

                    if (nPixel < 0) nPixel = 0;
                    if (nPixel > 255) nPixel = 255;
                    p[baseIndex + 5 + stride] = (byte)nPixel;

                    nPixel = ((((pSrc[baseIndex + 1] * m.TopLeft) +
                        (pSrc[baseIndex + 4] * m.TopMid) +
                        (pSrc[baseIndex + 7] * m.TopRight) +
                        (pSrc[baseIndex + 1 + stride] * m.MidLeft) +
                        (pSrc[baseIndex + 4 + stride] * m.Pixel) +
                        (pSrc[baseIndex + 7 + stride] * m.MidRight) +
                        (pSrc[baseIndex + 1 + stride2] * m.BotLeft) +
                        (pSrc[baseIndex + 4 + stride2] * m.BotMid) +
                        (pSrc[baseIndex + 7 + stride2] * m.BotRight))
                        / m.Factor) + m.Offset);

                    if (nPixel < 0) nPixel = 0;
                    if (nPixel > 255) nPixel = 255;
                    p[baseIndex + 4 + stride] = (byte)nPixel;

                    nPixel = ((((pSrc[baseIndex + 0] * m.TopLeft) +
                                   (pSrc[baseIndex + 3] * m.TopMid) +
                                   (pSrc[baseIndex + 6] * m.TopRight) +
                                   (pSrc[baseIndex + 0 + stride] * m.MidLeft) +
                                   (pSrc[baseIndex + 3 + stride] * m.Pixel) +
                                   (pSrc[baseIndex + 6 + stride] * m.MidRight) +
                                   (pSrc[baseIndex + 0 + stride2] * m.BotLeft) +
                                   (pSrc[baseIndex + 3 + stride2] * m.BotMid) +
                                   (pSrc[baseIndex + 6 + stride2] * m.BotRight))
                        / m.Factor) + m.Offset);

                    if (nPixel < 0) nPixel = 0;
                    if (nPixel > 255) nPixel = 255;
                    p[baseIndex + 3 + stride] = (byte)nPixel;

                    baseIndex += 3;
                }
                baseIndex += nOffset;
            }

            return b;
        }

        public static Bitmap Smoothing(this Bitmap bmp, int weight)
        {
            var matrix = new ConvMatrix();
            matrix.SetAll(1);
            matrix.Pixel = weight;
            matrix.Factor = weight + 8;

            return Conv3x3(bmp, matrix);
        }
    }

    public class ConvMatrix
    {
        public int TopLeft = 0, TopMid = 0, TopRight = 0;
        public int MidLeft = 0, Pixel = 1, MidRight = 0;
        public int BotLeft = 0, BotMid = 0, BotRight = 0;
        public int Factor = 1;
        public int Offset = 0;

        public void SetAll(int value)
        {
            TopLeft = TopMid = TopRight = MidLeft = Pixel = MidRight = BotLeft = BotMid = BotRight = value;
        }
    }

    public enum Channels
    {
        Red,
        Green,
        Blue
    }
}
