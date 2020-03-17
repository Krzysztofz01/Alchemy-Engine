using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading.Tasks;

namespace Alchemy_Engine
{
    class AlchemyLumetri
    {
        private class ConvMatrix
        {
            public int tLeft = 0, tMid = 0, tRight = 0;
            public int mLeft = 0, Pixel = 1, mRight = 0;
            public int bLeft = 0, bMid = 0, bRight = 0;
            public int factor = 1;
            public int offset = 0;
               
            public ConvMatrix()
            {

            }

            public ConvMatrix(int nVal)
            {
                tLeft = tMid = tRight = mLeft = Pixel = mRight = bLeft = bMid = bRight = nVal;
            }
        }

        private Bitmap bitmap;
        private List<Bitmap> processingBackup;

        public AlchemyLumetri(Bitmap bitmap)
        {
            this.bitmap = bitmap;
            this.processingBackup = new List<Bitmap>() { bitmap };
        }

        //NEED TO FIX CONTRAST, CURRENTLY IT AFFECTS ONLY THE RED CHANNEL
        //I NEED ALSO TO ADD GAMMA AND COLOR CORRECTION

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

        private bool Conv3x3(Bitmap bitmap, ConvMatrix matrix)
        {
            if (matrix.factor == 0) return false;

            Bitmap bSrc = (Bitmap)bitmap.Clone();

            BitmapData bitmapData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            BitmapData bSrcData = bSrc.LockBits(new Rectangle(0, 0, bSrc.Width, bSrc.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            
            int stride = bitmapData.Stride;
            int stride2 = stride * 2;
            IntPtr Scan0 = bitmapData.Scan0;
            IntPtr srcScan0 = bSrcData.Scan0;

            unsafe
            {
                byte* p = (byte*)(void*)Scan0;
                byte* pSrc = (byte*)(void*)srcScan0;

                int nOffset = stride + 6 - bitmap.Width * 3;
                int nWidth = bitmap.Width - 2;
                int nHeight = bitmap.Height - 2;
                int nPixel;

                for(int y=0; y < nHeight; ++y)
                {
                    for(int x=0; x < nWidth; ++x)
                    {
                        nPixel = ((((pSrc[2] * matrix.tLeft) + (pSrc[5] * matrix.tMid) + (pSrc[8] * matrix.tRight) +
                                    (pSrc[2 + stride] * matrix.mLeft) + (pSrc[5 + stride] * matrix.Pixel) + (pSrc[8 + stride] * matrix.mRight) +
                                    (pSrc[2 + stride2] * matrix.bLeft) + (pSrc[5 + stride2] * matrix.bMid) + (pSrc[8 + stride2] * matrix.bRight)
                                 ) / matrix.factor ) + matrix.offset);

                        if (nPixel < 0) nPixel = 0;
                        if (nPixel > 255) nPixel = 255;
                        p[5 + stride] = (byte)nPixel;

                        nPixel = ((((pSrc[1] * matrix.tLeft) + (pSrc[4] * matrix.tMid) + (pSrc[7] * matrix.tRight) +
                                    (pSrc[1 + stride] * matrix.mLeft) + (pSrc[4 + stride] * matrix.Pixel) + (pSrc[7 + stride] * matrix.mRight) +
                                    (pSrc[1 + stride2] * matrix.bLeft) + (pSrc[4 + stride2] * matrix.bMid) + (pSrc[7 + stride2] * matrix.bRight)
                                 ) / matrix.factor) + matrix.offset);
                        
                        if (nPixel < 0) nPixel = 0;
                        if (nPixel > 255) nPixel = 255;
                        p[4 + stride] = (byte)nPixel;

                        nPixel = ((((pSrc[0] * matrix.tLeft) + (pSrc[3] * matrix.tMid) + (pSrc[6] * matrix.tRight) +
                                    (pSrc[0 + stride] * matrix.mLeft) + (pSrc[3 + stride] * matrix.Pixel) + (pSrc[6 + stride] * matrix.mRight) +
                                    (pSrc[0 + stride2] * matrix.bLeft) + (pSrc[3 + stride2] * matrix.bMid) + (pSrc[6 + stride2] * matrix.bRight)
                                 ) / matrix.factor) + matrix.offset);

                        if (nPixel < 0) nPixel = 0;
                        if (nPixel > 255) nPixel = 255;
                        p[3 + stride] = (byte)nPixel;

                        p += 3;
                        pSrc += 3;
                    }

                    p += nOffset;
                    pSrc += nOffset;
                }
            }

            bitmap.UnlockBits(bitmapData);
            bSrc.UnlockBits(bSrcData);
            
            return true;
        }

        //dafult 1
        public void smooth(int value)
        {
            Bitmap bmp = getItem();
            ConvMatrix m = new ConvMatrix(1);
            m.Pixel = value;
            m.factor = value + 8;

            if(this.Conv3x3(bmp, m))
            {
                this.processingBackup.Add(bmp);
            }
        }

        //default 4
        public void gaussianBlur(int value)
        {
            Bitmap bmp = getItem();
            ConvMatrix m = new ConvMatrix(1);
            m.Pixel = value;
            m.tMid = m.mLeft = m.mRight = m.bMid = 2;
            m.factor = value + 12;

            if(this.Conv3x3(bmp, m))
            {
                this.processingBackup.Add(bmp);
            }
        }

        //default 9
        public void meanRemoval(int value)
        {
            Bitmap bmp = getItem();
            ConvMatrix m = new ConvMatrix(-1);
            m.Pixel = value;
            m.factor = value - 8;

            if(this.Conv3x3(bmp, m))
            {
                this.processingBackup.Add(bmp);
            }
        }

        //default 11
        public void sharpen(int value)
        {
            Bitmap bmp = getItem();
            ConvMatrix m = new ConvMatrix(0);
            m.Pixel = value;
            m.tMid = m.mLeft = m.mRight = m.bMid = -2;
            m.factor = value - 8;

            if(this.Conv3x3(bmp, m))
            {
                this.processingBackup.Add(bmp);
            }
        }

        public void emboss()
        {
            Bitmap bmp = getItem();
            ConvMatrix m = new ConvMatrix(-1);
            m.tMid = m.mLeft = m.mRight = m.bMid = 0;
            m.Pixel = 4;
            m.offset = 127;

            if(this.Conv3x3(bmp, m))
            {
                this.processingBackup.Add(bmp);
            }
        }

        public void edgeDetection()
        {
            Bitmap bmp = getItem();
            ConvMatrix m = new ConvMatrix();
            m.tLeft = m.tMid = m.tRight = -1;
            m.mLeft = m.Pixel = m.mRight = 0;
            m.bLeft = m.bMid = m.bRight = 1;
            m.offset = 127;
             
            if(this.Conv3x3(bmp, m))
            {
                this.processingBackup.Add(bmp);
            }
        }
    }
}