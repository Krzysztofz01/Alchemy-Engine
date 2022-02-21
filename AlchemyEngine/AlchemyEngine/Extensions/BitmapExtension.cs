using AlchemyEngine.Processing;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace AlchemyEngine.Extensions
{
    public static class BitmapExtension
    {
        public static Bitmap Scale(this Bitmap bitmap, int percent)
        {
            if (percent > 100 || percent < 0)
                throw new ArgumentException("The percent value must be between 0 and 100.", nameof(percent));

            int width = (bitmap.Width * percent) / 100;
            int height = (bitmap.Height * percent) / 100;
            return new Bitmap(bitmap, new Size(width, height));
        }

        public static Bitmap Invert(this Bitmap bitmap)
        {
            BitmapData bitmapData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadWrite, bitmap.PixelFormat);

            int bytesPerPixel = Bitmap.GetPixelFormatSize(bitmap.PixelFormat) / 8;
            int byteCount = bitmapData.Stride * bitmap.Height;
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
                    int blue = 255 - pixels[currentLine + x];
                    int green = 255 - pixels[currentLine + x + 1];
                    int red = 255 - pixels[currentLine + x + 2];

                    pixels[currentLine + x] = (byte)blue;
                    pixels[currentLine + x + 1] = (byte)green;
                    pixels[currentLine + x + 2] = (byte)red;
                }
            }

            Marshal.Copy(pixels, 0, ptrFirstPixel, pixels.Length);
            bitmap.UnlockBits(bitmapData);
            return bitmap;
        }

        public static Bitmap Grayscale(this Bitmap bitmap)
        {
            BitmapData bitmapData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadWrite, bitmap.PixelFormat);

            int bytesPerPixel = Bitmap.GetPixelFormatSize(bitmap.PixelFormat) / 8;
            int byteCount = bitmapData.Stride * bitmap.Height;
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
                    int blue = pixels[currentLine + x];
                    int green = pixels[currentLine + x + 1];
                    int red = pixels[currentLine + x + 2];

                    byte value = (byte)(0.299d * red + 0.587d * green + 0.114d * blue);

                    pixels[currentLine + x] = value;
                    pixels[currentLine + x + 1] = value;
                    pixels[currentLine + x + 2] = value;
                }
            }

            Marshal.Copy(pixels, 0, ptrFirstPixel, pixels.Length);
            bitmap.UnlockBits(bitmapData);
            return bitmap;
        }

        public static Bitmap Brightness(this Bitmap bitmap, int value)
        {
            if (value > 100 || value < -100)
                throw new ArgumentException("The value must be between -100 and 100.", nameof(value));

            BitmapData bitmapData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadWrite, bitmap.PixelFormat);

            int bytesPerPixel = Bitmap.GetPixelFormatSize(bitmap.PixelFormat) / 8;
            int byteCount = bitmapData.Stride * bitmap.Height;
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
                    for (int com = 0; com < 3; com++)
                    {
                        int comVal = pixels[currentLine + x + com] + value;

                        pixels[currentLine + x + com] = (byte)Math.Max(0, Math.Min(255, comVal));
                    }
                }
            }

            Marshal.Copy(pixels, 0, ptrFirstPixel, pixels.Length);
            bitmap.UnlockBits(bitmapData);
            return bitmap;
        }

        public static Bitmap Contrast(this Bitmap bitmap, int value)
        {
            if (value > 100 || value < -100)
                throw new ArgumentException("The value must be between -100 and 100.", nameof(value));

            double dVal = Math.Pow((100.0d + value) / 100.0d, 2);

            BitmapData bitmapData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadWrite, bitmap.PixelFormat);

            int bytesPerPixel = Bitmap.GetPixelFormatSize(bitmap.PixelFormat) / 8;
            int byteCount = bitmapData.Stride * bitmap.Height;
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
                    for (int com = 0; com < 3; com++)
                    {
                        double comInterval = pixels[currentLine + x + com] / 255.0d;
                        comInterval -= 0.5d;
                        comInterval *= dVal;
                        comInterval += 0.5d;
                        comInterval *= 255.0d;

                        pixels[currentLine + x + com] = (byte)Math.Max(0, Math.Min(255, comInterval));
                    }
                }
            }

            Marshal.Copy(pixels, 0, ptrFirstPixel, pixels.Length);
            bitmap.UnlockBits(bitmapData);
            return bitmap;
        }

        public static Bitmap ChannelFilter(this Bitmap bitmap, Channel channel)
        {
            if ((int)channel > 2 || (int)channel < 0)
                throw new ArgumentException("Invalid channel.", nameof(channel));

            BitmapData bitmapData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadWrite, bitmap.PixelFormat);

            int bytesPerPixel = Bitmap.GetPixelFormatSize(bitmap.PixelFormat) / 8;
            int byteCount = bitmapData.Stride * bitmap.Height;
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
                    byte blue = pixels[currentLine + x];
                    byte green = pixels[currentLine + x + 1];
                    byte red = pixels[currentLine + x + 2];

                    pixels[currentLine + x] = (channel == Channel.Blue) ? blue : (byte)0;
                    pixels[currentLine + x + 1] = (channel == Channel.Green) ? green : (byte)0;
                    pixels[currentLine + x + 2] = (channel == Channel.Red) ? red : (byte)0;
                }
            }

            Marshal.Copy(pixels, 0, ptrFirstPixel, pixels.Length);
            bitmap.UnlockBits(bitmapData);
            return bitmap;
        }

        public static IEnumerable<Color> GetPallete(this Bitmap bitmap, PalleteGenerator palleteGenerator)
        {
            return palleteGenerator switch
            {
                PalleteGenerator.AdditionMethod => GeneratePalleteAdditionMethod(bitmap),
                PalleteGenerator.CubeMethod => GeneratePalleteCubeMethod(bitmap),
                _ => throw new ArgumentOutOfRangeException(nameof(palleteGenerator), "Invalid PalleteGenerator argument."),
            };
        }

        public static async Task<IEnumerable<Color>> GetPalleteAsync(this Bitmap bitmap, PalleteGenerator palleteGenerator, CancellationToken cancellationToken = default)
        {
            return palleteGenerator switch
            {
                PalleteGenerator.AdditionMethod => await Task.Run(() => GeneratePalleteAdditionMethod(bitmap), cancellationToken),
                PalleteGenerator.CubeMethod => await Task.Run(() => GeneratePalleteCubeMethod(bitmap), cancellationToken),
                _ => throw new ArgumentOutOfRangeException(nameof(palleteGenerator), "Invalid PalleteGenerator argument."),
            };
        }

        private static IEnumerable<Color> GeneratePalleteAdditionMethod(Bitmap bitmap)
        {
            var scaledBitmap = bitmap.Scale(50);

            BitmapData bitmapData = scaledBitmap
                .LockBits(new Rectangle(0, 0, scaledBitmap.Width, scaledBitmap.Height), ImageLockMode.ReadOnly, scaledBitmap.PixelFormat);

            int bytesPerPixel = Bitmap.GetPixelFormatSize(scaledBitmap.PixelFormat) / 8;
            int byteCount = bitmapData.Stride * scaledBitmap.Height;
            byte[] pixels = new byte[byteCount];
            IntPtr firstPixelPtr = bitmapData.Scan0;
            Marshal.Copy(firstPixelPtr, pixels, 0, pixels.Length);
            int heightInPixels = bitmapData.Height;
            int widthInBytes = bitmapData.Width;

            var colors = new Dictionary<Color, int>();
            for (int y = 0; y < heightInPixels; y++)
            {
                int currentLine = y * bitmapData.Stride;         
                for (int x = 0; x < widthInBytes; x++)
                {
                    var currentPixel = Color.FromArgb(
                        pixels[currentLine + x + 2],
                        pixels[currentLine + x + 1],
                        pixels[currentLine + x]);

                    if (!colors.ContainsKey(currentPixel))
                        colors.Add(currentPixel, 0);

                    colors[currentPixel]++;
                }
            }

            scaledBitmap.UnlockBits(bitmapData);

            colors.OrderByDescending(x => x.Value);

            var filteredPallete = new List<Color>();
            if (colors.Count == 0) return filteredPallete;

            filteredPallete.Add(colors.First().Key);
            foreach (var color in colors.Skip(1))
            {
                if (ColorComparer.Distance(color.Key, filteredPallete.Last()) > 45) filteredPallete.Add(color.Key);

                if (filteredPallete.Count == 10) break;
            }

            return filteredPallete;
        }

        private static IEnumerable<Color> GeneratePalleteCubeMethod(Bitmap bitmap)
        {
            var scaledBitmap = bitmap.Scale(50);

            BitmapData bitmapData = scaledBitmap
                .LockBits(new Rectangle(0, 0, scaledBitmap.Width, scaledBitmap.Height), ImageLockMode.ReadOnly, scaledBitmap.PixelFormat);

            int bytesPerPixel = Bitmap.GetPixelFormatSize(scaledBitmap.PixelFormat) / 8;
            int byteCount = bitmapData.Stride * scaledBitmap.Height;
            byte[] pixels = new byte[byteCount];
            IntPtr firstPixelPtr = bitmapData.Scan0;
            Marshal.Copy(firstPixelPtr, pixels, 0, pixels.Length);
            int heightInPixels = bitmapData.Height;
            int widthInBytes = bitmapData.Width;

            var colorMatrix = new List<Color>[3, 3, 3];
            for (int x = 0; x < 3; x++)
                for (int y = 0; y < 3; y++)
                    for (int z = 0; z < 3; z++)
                        colorMatrix[x, y, z] = new List<Color>();

            for (int y = 0; y < heightInPixels; y++)
            {
                int currentLine = y * bitmapData.Stride;
                for (int x = 0; x < widthInBytes; x += bytesPerPixel)
                {
                    var currentPixel = Color.FromArgb(
                        pixels[currentLine + x + 2],
                        pixels[currentLine + x + 1],
                        pixels[currentLine + x]);

                    int redIndex = (currentPixel.R > 170) ? 2 : (currentPixel.R < 85) ? 0 : 1;
                    int greenIndex = (currentPixel.G > 170) ? 2 : (currentPixel.G < 85) ? 0 : 1;
                    int blueIndex = (currentPixel.B > 170) ? 2 : (currentPixel.B < 85) ? 0 : 1;

                    colorMatrix[redIndex, greenIndex, blueIndex].Add(currentPixel);
                }
            }

            scaledBitmap.UnlockBits(bitmapData);

            var colors = new List<Color>();
            for (int r = 0; r < 3; r++)
            {
                for (int g = 0; g < 3; g++)
                {
                    for (int b = 0; b < 3; b++)
                    {
                        int colorCount = colorMatrix[r, g, b].Count;
                        if (colorCount == 0) continue;

                        int rSum = 0;
                        int gSum = 0;
                        int bSum = 0;
                        
                        foreach(var color in colorMatrix[r, g, b])
                        {
                            rSum += color.R;
                            gSum += color.G;
                            bSum += color.B;
                        }

                        colors.Add(Color.FromArgb(rSum / colorCount, gSum / colorCount, bSum / colorCount));
                    }
                }
            }

            return colors;
        }
    }

    public enum PalleteGenerator
    {
        CubeMethod,
        AdditionMethod
    }

    public enum Channel
    {
        Red = 0,
        Green = 1,
        Blue = 2
    }
}
