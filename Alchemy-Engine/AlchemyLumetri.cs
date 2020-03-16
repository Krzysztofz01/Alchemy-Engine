using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading.Tasks;

namespace Alchemy_Engine
{
    class AlchemyLumetri
    {
        private Bitmap bitmap;
        private List<Bitmap> processingBackup;

        public AlchemyLumetri(Bitmap bitmap)
        {
            this.bitmap = bitmap;
            this.processingBackup = new List<Bitmap>() { bitmap };
        }

        public Bitmap getItem()
        {
            return processingBackup[processingBackup.Count - 1];
        }

        public void invertColors()
        {
            Bitmap bmp = getItem();
            unsafe
            {
                BitmapData bitmapData = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadWrite, bmp.PixelFormat);
                int bytesPerPixel = Bitmap.GetPixelFormatSize(bmp.PixelFormat) / 8;
                int heightInPixel = bitmapData.Height;
                int widthInBytes = bitmapData.Width * bytesPerPixel;
                byte* ptrFirstPixel = (byte*)bitmapData.Scan0;
                
                Parallel.For(0, heightInPixel, y => {
                    byte* currentLine = ptrFirstPixel + (y * bitmapData.Stride);
                    for (int x = 0; x < widthInBytes; x += bytesPerPixel)
                    {
                        currentLine[x] = (byte)(255 - currentLine[x]);
                        currentLine[x + 1] = (byte)(255 - currentLine[x + 1]);
                        currentLine[x + 2] = (byte)(255 - currentLine[x + 2]);
                    }
                });
                bmp.UnlockBits(bitmapData);
            }
            this.processingBackup.Add(bmp);
        }

        public void greyScale()
        {
            Bitmap bmp = getItem();
            unsafe
            {
                BitmapData bitmapData = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadWrite, bmp.PixelFormat);
                int bytesPerPixel = Bitmap.GetPixelFormatSize(bmp.PixelFormat) / 8;
                int heightInPixel = bitmapData.Height;
                int widthInBytes = bitmapData.Width * bytesPerPixel;
                byte* ptrFirstPixel = (byte*)bitmapData.Scan0;

                Parallel.For(0, heightInPixel, y => {
                    byte* currentLine = ptrFirstPixel + (y * bitmapData.Stride);
                    for (int x = 0; x < widthInBytes; x += bytesPerPixel)
                    {
                        currentLine[x] = currentLine[x + 1] = currentLine[x + 2] = (byte)(.299 * currentLine[x + 2] + .587 * currentLine[x + 1] + .114 * currentLine[x]);
                    }
                });
                bmp.UnlockBits(bitmapData);
            }
            this.processingBackup.Add(bmp);
        }
    }
}
