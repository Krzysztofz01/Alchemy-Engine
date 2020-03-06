using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using Wpf = System.Windows.Controls;

namespace Alchemy_Engine
{
    class AlchemyAnalyzer
    {
        private Bitmap bitmap;
        private List<Pixel> colorContainer;
        private int downscaleWidth;
        private int downscaleHeight;
        private int accuracy;

        public AlchemyAnalyzer(Bitmap bitmap, decimal imageScale, int accuracy)
        {
            this.bitmap = bitmap;
            this.accuracy = accuracy;

            colorContainer = new List<Pixel>();
            
            downscaleWidth = Decimal.ToInt32(bitmap.Width * (imageScale / 100));
            downscaleHeight = Decimal.ToInt32(bitmap.Height * (imageScale / 100));
        }

        public void generatePalette()
        {
            Bitmap scaledBitmap = new Bitmap(bitmap, downscaleWidth, downscaleHeight);

            Pixel currentCheck;
            for(int y=0; y < scaledBitmap.Height; y += accuracy)
            {
                for(int x=0; x < scaledBitmap.Width; x += accuracy)
                {
                    try
                    {
                        currentCheck = new Pixel(AlchemyConverter.colorToHex(bitmap.GetPixel(x, y)));

                        if (colorContainer.Count == 0)
                        {
                            colorContainer.Add(currentCheck);
                        }
                        else
                        {
                            foreach (Pixel element in colorContainer)
                            {
                                if (element.color == currentCheck.color)
                                {
                                    element.count++;
                                    currentCheck.exist = true;
                                    break;
                                }
                            }

                            if(!currentCheck.exist)
                            {
                                colorContainer.Add(currentCheck);
                            }

                        }
                    }
                    catch (ArgumentOutOfRangeException e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
            }

            colorBubbleSort();
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

        public void generatePalletePointer()
        {
            //Lock bitmaps bits in memory
            Rectangle rect = new Rectangle(0, 0, bitmap.Width, bitmap.Height);
            System.Drawing.Imaging.BitmapData bitmapData = bitmap.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite, bitmap.PixelFormat);

            //Get the memory address of the first line
            IntPtr ptr = bitmapData.Scan0;

            //Declare an array to hold bytes of the bitmap
            int bytes = bitmapData.Stride * bitmap.Height;
            int stride = bitmapData.Stride;
            byte[] rgbValues = new byte[bytes];

            Marshal.Copy(ptr, rgbValues, 0, bytes);
            Pixel currentCheck;

            for (int column = 0; column < bitmapData.Height; column++)
            {
                for (int row = 0; row < bitmapData.Width; row++)
                {
                    currentCheck = new Pixel(AlchemyConverter.rgbToHex(
                        (byte)(rgbValues[(column * stride) + (row * 3)]),
                        (byte)(rgbValues[(column * stride) + (row * 3) + 1]),
                        (byte)(rgbValues[(column * stride) + (row * 3) + 2])
                        ));

                    if (colorContainer.Count == 0)
                    {
                        colorContainer.Add(currentCheck);
                    }
                    else
                    {
                        foreach (Pixel element in colorContainer)
                        {
                            if (element.color == currentCheck.color)
                            {
                                element.count++;
                                currentCheck.exist = true;
                                break;
                            }
                        }

                        if (!currentCheck.exist)
                        {
                            colorContainer.Add(currentCheck);
                        }

                    }


                }
            }
            bitmap.UnlockBits(bitmapData);
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

                        if(noSimilarColors)
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

    class Pixel
    {
        public string color { get; }
        public bool exist { get; set; }
        public int count { get; set; }

        public Pixel(string color)
        {
            this.color = color;
            this.exist = false;
            this.count = 1;
        }
    }
}