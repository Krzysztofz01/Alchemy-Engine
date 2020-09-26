using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading.Tasks;
using Wpf = System.Windows.Controls;

namespace Alchemy_Engine
{
    class AlchemyAnalyzer
    {
        private Bitmap bitmap;
        private Bitmap originalSizedBitmap;
        private List<Pixel> colorContainer;
        
        public AlchemyAnalyzer(Bitmap bitmap)
        {
            this.bitmap = bitmap;
            this.originalSizedBitmap = bitmap;
            this.colorContainer = new List<Pixel>();
        }

        public AlchemyAnalyzer(Bitmap bitmap, int downScale)
        {
            this.bitmap = new Bitmap(bitmap, (int)(bitmap.Width * downScale / 100), (int)(bitmap.Height * downScale / 100));
            this.originalSizedBitmap = bitmap;
            this.colorContainer = new List<Pixel>();
        }

        //For now the only working method
        public void samplePaletteLock()
        {
            unsafe
            {
                BitmapData bitmapData = this.bitmap.LockBits(new Rectangle(0, 0, this.bitmap.Width, this.bitmap.Height), ImageLockMode.ReadOnly, this.bitmap.PixelFormat);
                int bytesPerPixel = Bitmap.GetPixelFormatSize(this.bitmap.PixelFormat) / 8;
                int heightInPixel = bitmapData.Height;
                int widthInBytes = bitmapData.Width * bytesPerPixel;
                byte* ptrFirstPixel = (byte*)bitmapData.Scan0;
                Pixel currentPixel = null;

                for(int y=0; y < heightInPixel; y++)
                {
                    byte* currentLine = ptrFirstPixel + (y * bitmapData.Stride);
                    for(int x=0; x<widthInBytes; x = x + bytesPerPixel)
                    {
                        currentPixel = new Pixel(currentLine[x], currentLine[x + 1], currentLine[x + 2]);

                        if(this.colorContainer.Count > 0)
                        {
                            for (int l = 0; l < this.colorContainer.Count; l++)
                            {
                                if(currentPixel.color == this.colorContainer[l].color)
                                {
                                    currentPixel.exist = true;
                                    this.colorContainer[l].count++;
                                    break;
                                }
                            }
                        }

                        if(!currentPixel.exist)
                        {
                            this.colorContainer.Add(currentPixel);
                        }
                    }
                }
                this.bitmap.UnlockBits(bitmapData);
                colorBubbleSort();
            }
        }


        //Multi thread method (not working), research about parallel collections
        public void samplePaletteLockParallel()
        {
            unsafe
            {
                BitmapData bitmapData = this.bitmap.LockBits(new Rectangle(0, 0, this.bitmap.Width, this.bitmap.Height), ImageLockMode.ReadOnly, this.bitmap.PixelFormat);
                int bytesPerPixel = Bitmap.GetPixelFormatSize(this.bitmap.PixelFormat) / 8;
                int heightInPixel = bitmapData.Height;
                int widthInBytes = bitmapData.Width * bytesPerPixel;
                byte* ptrFirstPixel = (byte*)bitmapData.Scan0;
                Pixel currentPixel = null;

                Parallel.For(0, heightInPixel, y => {
                    byte* currentLine = ptrFirstPixel + (y * bitmapData.Stride);
                    for(int x=0; x < widthInBytes; x = x + bytesPerPixel)
                    {
                        var cos = new Pixel(currentLine[x], currentLine[x + 1], currentLine[x + 2]);
                        Console.WriteLine(cos.color);
                    }
                });
                this.bitmap.UnlockBits(bitmapData);
            }
        }

        private void colorCountSort()
        {
            //https://www.w3resource.com/csharp-exercises/searching-and-sorting-algorithm/searching-and-sorting-algorithm-exercise-4.php
        }

        

        private void colorBubbleSort()
        {
            for (int i = 0; i < colorContainer.Count - 1; i++)
            {
                for (int j = 0; j < colorContainer.Count - i - 1; j++)
                {
                    if (colorContainer[j].count > colorContainer[j + 1].count)
                    {
                        Pixel tmp = colorContainer[j];
                        colorContainer[j] = colorContainer[j + 1];
                        colorContainer[j + 1] = tmp;
                    }
                }
            }
        }

        public AnalyzerResults getOutput()
        {
            int filterThreshold = 150;

            List<string> outputArray = new List<string>() { colorContainer[0].color };
            AlchemyFilter filter = new AlchemyFilter();
            bool filterCheck = true;

            while (outputArray.Count < 5)
            {
                Console.WriteLine("colorCt count less than 5");
                for (int i = 0; i < colorContainer.Count; i++)
                {
                    filterCheck = true;
                    for (int j = 0; j < outputArray.Count; j++)
                    {
                        if (filter.filterColorDifference(
                            AlchemyConverter.hexToColor(colorContainer[i].color),
                            AlchemyConverter.hexToColor(outputArray[j]), filterThreshold))
                        {
                            filterCheck = false;
                            break;
                        }

                    }

                    if (filterCheck)
                    {
                        Console.WriteLine("Add element to array output");
                        outputArray.Add(colorContainer[i].color);
                    }

                    if (outputArray.Count == 5)
                    {
                        break;
                    }
                }
                filterThreshold -= 5;
            }

            foreach(string color in outputArray)
            {
                Console.WriteLine(color);
            }
            Console.WriteLine(colorContainer.Count.ToString() + "  " + outputArray.Count.ToString());

            //Bitmap generate section
            Bitmap outputImage = this.originalSizedBitmap;

            int rectWidth = outputImage.Width / 5;
            int rectHeight = outputImage.Height / 6;
            int rectMargin = rectHeight / 6;
            int center = (outputImage.Width / 2) - (rectWidth / 2);
            int yOffset = rectMargin;

            foreach(string color in outputArray)
            {
                using(Graphics graphics = Graphics.FromImage(outputImage))
                {
                    using(SolidBrush brush = new SolidBrush(AlchemyConverter.hexToColor(color)))
                    {
                        graphics.FillRectangle(brush, center, yOffset, rectWidth, rectHeight);
                        yOffset += rectHeight + rectMargin;
                    }
                }
            }

            return new AnalyzerResults(outputImage, outputArray);
        }

        public static List<Wpf.Label> colorsToLabels(List<string> colors)
        {
            //Generate WPF Labels
            List<Wpf.Label> outputLabels = new List<Wpf.Label>();
            int rowOffset = 1;
            foreach (string color in colors)
            {
                Wpf.Label lbl = new Wpf.Label();
                Wpf.Grid.SetColumn(lbl, 2);
                Wpf.Grid.SetRow(lbl, rowOffset);
                Color tmpColor = AlchemyConverter.hexToColor(color);
                lbl.Background = new System.Windows.Media.SolidColorBrush(
                    System.Windows.Media.Color.FromRgb(tmpColor.R, tmpColor.G, tmpColor.B));
                outputLabels.Add(lbl);
                rowOffset += 2;
            }
            return outputLabels;
        }

        public List<Wpf.Label> getColors(int amount, bool applyFilter, int filterThreshold = 0)
        {
            if(colorContainer.Count >= amount)
            {
                List<string> outputArray = new List<string>();
                if(applyFilter)
                {
                    AlchemyFilter filter = new AlchemyFilter();
                    outputArray.Add(colorContainer[0].color);
                    bool noSimilarColors = true;
                    for (int i = 0; i < colorContainer.Count; i++)
                    {
                        for (int j = 0; j < outputArray.Count; j++)
                        {
                            if(filter.filterColorDifference(
                                AlchemyConverter.hexToColor(outputArray[j]),
                                AlchemyConverter.hexToColor(colorContainer[i].color),
                                filterThreshold))
                            {
                                noSimilarColors = false;
                                break;
                            }   
                        }

                        if(!noSimilarColors)
                        {
                            outputArray.Add(colorContainer[i].color);
                            if(outputArray.Count == amount)
                            {
                                break;
                            }
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < amount; i++)
                    {
                        outputArray.Add(colorContainer[i].color);
                    }
                }

                //Convert hex color informations to Grid labels
                List<Wpf.Label> outputLabels = new List<Wpf.Label>();
                int rowOffset = 1;
                foreach(string color in outputArray)
                {
                    Wpf.Label lbl = new Wpf.Label();
                    Wpf.Grid.SetColumn(lbl, 2);
                    Wpf.Grid.SetRow(lbl, rowOffset);
                    Color tmpColor = AlchemyConverter.hexToColor(color);
                    lbl.Background = new System.Windows.Media.SolidColorBrush(
                        System.Windows.Media.Color.FromRgb(tmpColor.R, tmpColor.G, tmpColor.B));
                    outputLabels.Add(lbl);
                    rowOffset += 2;
                }
                return outputLabels;    
            }
            return null;
        }

        public Bitmap getPaletteImage()
        {
            return null;
        }

        public List<string> getPaletteLog()
        {
            List<string> hexColors = new List<string>();
            foreach(Pixel pixel in colorContainer)
            {
                hexColors.Add(pixel.color);
            }
            return hexColors;
        }
    }

    public struct AnalyzerResults
    {
        public Bitmap image { get; private set; }
        public List<string> colors { get; private set; }
        public AnalyzerResults(Bitmap image, List<string> colors)
        {
            this.image = image;
            this.colors = colors;
        }
    }

    class Pixel
    {
        public string color { get; }
        public bool exist { get; set; }
        public int count { get; set; }

        public Pixel(byte b, byte g, byte r)
        {
            this.color = "#" + r.ToString("X2") + g.ToString("X2") + b.ToString("X2");
            this.exist = false;
            this.count = 1;
        }
    }

    
}