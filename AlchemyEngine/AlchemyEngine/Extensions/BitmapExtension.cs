﻿using AlchemyEngine.Processing;
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

        public static IEnumerable<Color> GetPallete(this Bitmap bitmap, PalleteGenerator palleteGenerator)
        {
            switch (palleteGenerator)
            {
                case PalleteGenerator.AdditionMethod: return GeneratePalleteAdditionMethod(bitmap);
                case PalleteGenerator.CubeMethod: return GeneratePalleteCubeMethod(bitmap);
                default: throw new ArgumentOutOfRangeException("Invalid PalleteGenerator argument.");
            }
        }

        public static async Task<IEnumerable<Color>> GetPalleteAsync(this Bitmap bitmap, PalleteGenerator palleteGenerator)
        {
            switch (palleteGenerator)
            {
                case PalleteGenerator.AdditionMethod: return await Task.Run(() => GeneratePalleteAdditionMethod(bitmap));
                case PalleteGenerator.CubeMethod: return await Task.Run(() => GeneratePalleteCubeMethod(bitmap));
                default: throw new ArgumentOutOfRangeException("Invalid PalleteGenerator argument.");
            }
        }

        public static async Task<IEnumerable<Color>> GetPalleteAsync(this Bitmap bitmap, PalleteGenerator palleteGenerator, CancellationToken cancellationToken)
        {
            switch (palleteGenerator)
            {
                case PalleteGenerator.AdditionMethod: return await Task.Run(() => GeneratePalleteAdditionMethod(bitmap), cancellationToken);
                case PalleteGenerator.CubeMethod: return await Task.Run(() => GeneratePalleteCubeMethod(bitmap), cancellationToken);
                default: throw new ArgumentOutOfRangeException("Invalid PalleteGenerator argument.");
            }
        }

        private static IEnumerable<Color> GeneratePalleteAdditionMethod(Bitmap bitmap)
        {
            //Data preparation step
            var scaledBitmap = bitmap.Scale(50);
            var colors = new Dictionary<Color, int>();

            BitmapData bitmapData = scaledBitmap
                .LockBits(new Rectangle(0, 0, scaledBitmap.Width, scaledBitmap.Height), ImageLockMode.ReadOnly, scaledBitmap.PixelFormat);

            int bytesPerPixel = Bitmap.GetPixelFormatSize(scaledBitmap.PixelFormat) / 8;
            int byteCount = bitmapData.Stride * scaledBitmap.Height;
            byte[] pixels = new byte[byteCount];
            IntPtr firstPixelPtr = bitmapData.Scan0;

            Marshal.Copy(firstPixelPtr, pixels, 0, pixels.Length);
            int heightInPixels = bitmapData.Height;
            int widthInPixels = bitmapData.Width;

            //Scaning step
            for(int y=0; y < heightInPixels; y++)
            {
                int currentLine = y * bitmapData.Stride;
                
                for(int x=0; x < widthInPixels; x++)
                {
                    //Pixel color access
                    var currentPixel = Color.FromArgb(pixels[currentLine + x + 2], pixels[currentLine + x + 1], pixels[currentLine + x]);

                    if (colors.ContainsKey(currentPixel))
                    {
                        colors[currentPixel]++;
                    }
                    else
                    {
                        colors.Add(currentPixel, 1);
                    }
                }
            }

            scaledBitmap.UnlockBits(bitmapData);

            //Sorting step
            colors.OrderByDescending(x => x.Value);

            //Filtering step
            var filteredPallete = new List<Color>();

            foreach(var color in colors)
            {
                if (!filteredPallete.Any()) filteredPallete.Add(color.Key);

                if (ColorComparer.Distance(color.Key, filteredPallete.Last()) > 45) filteredPallete.Add(color.Key);

                if (filteredPallete.Count == 10) break;
            }

            return filteredPallete;
        }

        private static IEnumerable<Color> GeneratePalleteCubeMethod(Bitmap bitmap)
        {
            //Local index definition function
            static int getIndex(int value)
            {
                if (value < 85) return 0;
                if (value > 170) return 2;
                return 1;
            }

            //Data preparation step
            var scaledBitmap = bitmap.Scale(50);
            var colorMatrix = new List<Color>[3, 3, 3];

            for (int xAxis = 0; xAxis < 3; xAxis++)
            {
                for (int yAxis = 0; yAxis < 3; yAxis++)
                {
                    for (int zAxis = 0; zAxis < 3; zAxis++)
                    {
                        colorMatrix[xAxis, yAxis, zAxis] = new List<Color>();
                    }
                }
            }

            BitmapData bitmapData = scaledBitmap
                .LockBits(new Rectangle(0, 0, scaledBitmap.Width, scaledBitmap.Height), ImageLockMode.ReadOnly, scaledBitmap.PixelFormat);

            int bytesPerPixel = Bitmap.GetPixelFormatSize(scaledBitmap.PixelFormat) / 8;
            int byteCount = bitmapData.Stride * scaledBitmap.Height;
            byte[] pixels = new byte[byteCount];
            IntPtr firstPixelPtr = bitmapData.Scan0;

            Marshal.Copy(firstPixelPtr, pixels, 0, pixels.Length);
            int heightInPixels = bitmapData.Height;
            int widthInPixels = bitmapData.Width;

            //Scaning step
            for (int y = 0; y < heightInPixels; y++)
            {
                int currentLine = y * bitmapData.Stride;

                for (int x = 0; x < widthInPixels; x++)
                {
                    //Pixel color access
                    var currentPixel = Color.FromArgb(pixels[currentLine + x + 2], pixels[currentLine + x + 1], pixels[currentLine + x]);

                    int redIndex = getIndex(currentPixel.R);
                    int greenIndex = getIndex(currentPixel.G);
                    int blueIndex = getIndex(currentPixel.B);

                    colorMatrix[redIndex, greenIndex, blueIndex].Add(currentPixel);
                }
            }

            scaledBitmap.UnlockBits(bitmapData);

            //Merge step
            var colors = new List<Color>();

            for (int r = 0; r < 3; r++)
            {
                for (int g = 0; g < 3; g++)
                {
                    for (int b = 0; b < 3; b++)
                    {
                        int rSum = 0;
                        int gSum = 0;
                        int bSum = 0;
                        int colorCount = colorMatrix[r, g, b].Count;

                        if (colorCount == 0) continue;

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
}