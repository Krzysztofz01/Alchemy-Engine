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
            throw new NotImplementedException();
        }
    }

    public enum PalleteGenerator
    {
        CubeMethod,
        AdditionMethod
    }
}
