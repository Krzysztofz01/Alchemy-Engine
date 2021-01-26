using AlchemyEngine.Utility.Extensions;
using AlchemyEngine.Utility.Structures;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;

namespace AlchemyEngine.Utility.Processing
{
    public static class Palette
    {
        public static IEnumerable<RGB> GetGridPalleteValues(Bitmap image, int imageScale, int colorFilterThreshold)
        {
            image = image.Scale(imageScale);
            var pixelMap = new Dictionary<RGB, int>();

            BitmapData bitmapData = image.LockBits(new Rectangle(0, 0, image.Width, image.Height), ImageLockMode.ReadOnly, image.PixelFormat);
            int bytesPerPixel = Bitmap.GetPixelFormatSize(image.PixelFormat) / 8;
            int byteCount = bitmapData.Stride * image.Height;
            byte[] pixels = new byte[byteCount];
            IntPtr firstPixelPtr = bitmapData.Scan0;
            Marshal.Copy(firstPixelPtr, pixels, 0, pixels.Length);
            int heightInPixels = bitmapData.Height;
            int widthInPixels = bitmapData.Width;

            for(int y = 0; y < heightInPixels; y++)
            {
                int currentLine = y * bitmapData.Stride;
                for(int x = 0; x < widthInPixels; x = x + bytesPerPixel)
                {
                    var currentPixel = new RGB(pixels[currentLine + x + 2], pixels[currentLine + x + 1], pixels[currentLine + x]);

                    try
                    {
                        pixelMap.Add(currentPixel, 1);
                    }
                    catch(ArgumentException)
                    {
                        pixelMap[currentPixel]++;
                    }
                }
            }

            image.UnlockBits(bitmapData);

            var pixelPairList = pixelMap.Select(x => new Tuple<RGB, int>(x.Key, x.Value)).ToList();
            pixelPairList.Sort((a, b) => a.Item2.CompareTo(b.Item2));


            var filteredColors = new List<RGB>() { pixelPairList[0].Item1 };
            for(int i = 1; i < pixelPairList.Count && filteredColors.Count < 7; i++)
            {
                if (Filter.IsDifferent(filteredColors.Last(), pixelPairList[i].Item1, colorFilterThreshold))
                {
                    filteredColors.Add(pixelPairList[i].Item1);
                }

                if (filteredColors.Count == 7) break;
            }

            return filteredColors;
        }

        public static void GetGridPalleteImage(Bitmap image, int imageScale, int colorFilterThreshold, string fileSavePath)
        {
            var colors = GetGridPalleteValues(image, imageScale, colorFilterThreshold);
            var paletteImage = GenerateColorTiles(image, colors);

            fileSavePath = Path.Combine(fileSavePath, $@"{ Guid.NewGuid() }.jpg");
            paletteImage.Save(fileSavePath, ImageFormat.Jpeg);
        }

        public static IEnumerable<RGB> GetSumPalleteValues(Bitmap image, int imageScale)
        {
            image = image.Scale(imageScale);
            var colorMatrix = new List<RGB>[3, 3, 3];
            
            for(int xAxis = 0; xAxis < 3; xAxis++)
            {
                for(int yAxis = 0; yAxis < 3; yAxis++)
                {
                    for(int zAxis = 0; zAxis < 3; zAxis++)
                    {
                        colorMatrix[xAxis, yAxis, zAxis] = new List<RGB>();
                    }
                }
            }

            BitmapData bitmapData = image.LockBits(new Rectangle(0, 0, image.Width, image.Height), ImageLockMode.ReadOnly, image.PixelFormat);
            int bytesPerPixel = Bitmap.GetPixelFormatSize(image.PixelFormat) / 8;
            int byteCount = bitmapData.Stride * image.Height;
            byte[] pixels = new byte[byteCount];
            IntPtr firstPixelPtr = bitmapData.Scan0;
            Marshal.Copy(firstPixelPtr, pixels, 0, pixels.Length);
            int heightInPixels = bitmapData.Height;
            int widthInPixels = bitmapData.Width;

            for (int y = 0; y < heightInPixels; y++)
            {
                int currentLine = y * bitmapData.Stride;
                for (int x = 0; x < widthInPixels; x = x + bytesPerPixel)
                {
                    var currentPixel = new RGB(pixels[currentLine + x + 2], pixels[currentLine + x + 1], pixels[currentLine + x]);

                    int rIndex = 1;
                    if(currentPixel.R < 85) { rIndex = 0; }
                    if(currentPixel.R > 170) { rIndex = 2; }

                    int gIndex = 1;
                    if(currentPixel.G < 85) { gIndex = 0; }
                    if(currentPixel.G > 170) { gIndex = 2; }

                    int bIndex = 1;
                    if(currentPixel.B < 85) { bIndex = 0; }
                    if(currentPixel.B > 170) { bIndex = 2; }

                    colorMatrix[rIndex, gIndex, bIndex].Add(currentPixel);
                }
            }

            image.UnlockBits(bitmapData);

            var outputColors = new List<RGB>();

            for(int r = 0; r < 3; r++)
            {
                for(int g = 0; g < 3; g++)
                {
                    for(int b = 0; b < 3; b++)
                    {
                        int rSum = 0;
                        int gSum = 0;
                        int bSum = 0;
                        int colorCount = colorMatrix[r, g, b].Count;

                        foreach (var color in colorMatrix[r, g, b])
                        {
                            rSum += color.R;
                            gSum += color.G;
                            bSum += color.B;
                        }

                        if(rSum != 0 && gSum != 0 && bSum != 0)
                        {
                            outputColors.Add(new RGB((byte)(rSum / colorCount), (byte)(gSum / colorCount), (byte)(bSum / colorCount)));
                        }
                    }
                }
            }

            return outputColors;
        }

        public static void GetSumPalleteImage(Bitmap image, int imageScale, string fileSavePath)
        {
            var colors = GetSumPalleteValues(image, imageScale);
            var paletteImage = GenerateColorTiles(image, colors);

            fileSavePath = Path.Combine(fileSavePath, $@"{ Guid.NewGuid() }.jpg");
            paletteImage.Save(fileSavePath, ImageFormat.Jpeg);
        }

        private static Bitmap GenerateColorTiles(Bitmap image, IEnumerable<RGB> colors)
        {
            var colorList = colors.ToList();
            int tileWidth = image.Width / 5;
            int tileHeigh = image.Height / 8;
            int verticalTileMargin = tileHeigh / 8;
            int horizontalTileMargin = (image.Width / 2) - (tileWidth / 2);
            int verticalMarginSum = verticalTileMargin;

            for(int i=0; i < 7; i++)
            {
                using (var graphics = Graphics.FromImage(image))
                {
                    using (var brush = new SolidBrush(colorList[i].ToColor()))
                    {
                        graphics.FillRectangle(brush, horizontalTileMargin, verticalMarginSum, tileWidth, tileHeigh);
                        verticalMarginSum += tileHeigh + verticalTileMargin;
                    }
                }
            }
            return image;
        }

    }
}
