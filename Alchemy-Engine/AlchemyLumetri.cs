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

        //Values from -100 - 100
        public void brightnessFilter(int value)
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
                        for(int px=0; px<3; px++)
                        {
                            int nVal = currentLine[x + px] + value;
                            if (nVal < 0) nVal = 0;
                            if (nVal > 255) nVal = 255;
                            currentLine[x + px] = (byte)nVal;
                        }
                    }
                });
                bmp.UnlockBits(bitmapData);
            }
            this.processingBackup.Add(bmp);
        }

        //Values from -100 - 100
        public void contrastFilter(int value)
        {
            if (value < -100) value = -100;
            if (value > 100) value = 100;
            double pixel = 0;
            double contrast = Math.Pow((100.0 + value) / 100.0, 2); 
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
                        for (int px = 0; px < 3; px++)
                        {
                            pixel = currentLine[x + px] / 255.0;
                            pixel -= 0.5;
                            pixel *= contrast;
                            pixel += 0.5;
                            pixel *= 255;
                            if (pixel < 0) pixel = 0;
                            if (pixel > 255) pixel = 255;
                            currentLine[x + px] = (byte)pixel;
                        }
                    }
                });
                bmp.UnlockBits(bitmapData);
            }
            this.processingBackup.Add(bmp);
        }
    }
}
