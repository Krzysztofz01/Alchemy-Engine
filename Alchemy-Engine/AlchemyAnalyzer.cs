using System;
using System.Collections.Generic;
using System.Drawing;

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

        public List<string> getColors(int amount, bool applyFilter, int filterThreshold = 0)
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
                return outputArray;
            }
            return null;
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
