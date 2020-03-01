using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            //sort here
        }

        public void pickColor()
        {
            //return ,,n'' colors
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
