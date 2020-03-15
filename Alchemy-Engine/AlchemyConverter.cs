using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Media.Imaging;

namespace Alchemy_Engine
{
    class AlchemyConverter
    {
        [DllImport("gdi32")]
        static extern int DeleteObject(IntPtr o);
        
        public static BitmapSource bitmapToBitmapSource(Bitmap bitmap)
        {
            IntPtr ip = bitmap.GetHbitmap();
            BitmapSource bs = null;
            try
            {
                bs = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(ip,
                   IntPtr.Zero, Int32Rect.Empty,
                   System.Windows.Media.Imaging.BitmapSizeOptions.FromEmptyOptions());
            }
            finally
            {
                DeleteObject(ip);
            }

            return bs;
        }

        //Convert Color struct to Hex string
        public static string colorToHex(Color color)
        {
            return "#" + color.R.ToString("X2") + color.G.ToString("X2") + color.B.ToString("X2");
        }

        public static string mediaColorToHex(System.Windows.Media.Color color)
        {
            return "#" + color.R.ToString("X2") + color.G.ToString("X2") + color.B.ToString("X2");
        }

        public static string rgbToHex(byte b, byte g, byte r)
        {
            return "#" + r.ToString("X2") + g.ToString("X2") + b.ToString("X2");
        }

        //Convert Hex string to Color object
        public static Color hexToColor(string color)
        {
            return ColorTranslator.FromHtml(color);
        }

        //Convert Color struct to HSL struct (RGB to HSL)
        public static HSL colorToHsl(Color color)
        {
            double h = 0.0;
            double s = 0.0;
            double l = 0.0;

            double r = color.R / 255.0;
            double g = color.G / 255.0;
            double b = color.B / 255.0;

            double min = Math.Min(r, Math.Min(g, b));
            double max = Math.Max(r, Math.Max(g, b));

            l = (min + max) / 2.0;

            if (min == max)
            {
                h = 0.0;
                s = 0.0;
            }
            else
            {
                if (l <= 0.5)
                {
                    s = (max - min) / (max + min);
                }
                else
                {
                    s = ((max - min) / (2.0 - max - min));
                }

                if (max == r)
                {
                    h = (g - b) / (max - min);
                }
                else if (max == g)
                {
                    h = 2.0 + (b - r) / (max - min);
                }
                else if (max == b)
                {
                    h = 4.0 + (r - g) / (max - min);
                }

                h *= 60.0;
                if (h < 0)
                {
                    h += 360.0;
                }
            }

            return new HSL(h, s, l);
        }

        //Convert HSL struct to Color struct (HSL to RGB)
        public static Color hslToColor(HSL hsl)
        {
            double p2 = 0.0;
            double doubleR = 0.0;
            double doubleG = 0.0;
            double doubleB = 0.0;

            if(hsl.L <= 0.5)
            {
                p2 = hsl.L * (1 + hsl.S);
            }
            else
            {
                p2 = hsl.L + hsl.S - (hsl.L * hsl.S);
            }

            double p1 = 2 * hsl.L - p2;

            if(hsl.S == 0)
            {
                doubleR = hsl.L;
                doubleG = hsl.L;
                doubleB = hsl.L;
            }
            else
            {
                doubleR = qqhToRgb(p1, p2, hsl.H + 120);
                doubleG = qqhToRgb(p1, p2, hsl.H);
                doubleB = qqhToRgb(p1, p2, hsl.H - 120);
            }

            return Color.FromArgb(
                (int)(doubleR * 255.0),
                (int)(doubleG * 255.0),
                (int)(doubleB * 255.0));
        }

        private static double qqhToRgb(double q1, double q2, double hue)
        {
            if(hue > 360)
            {
                hue -= 360;
            }
            else if(hue < 0)
            {
                hue += 360;
            }

            if(hue < 60)
            {
                return q1 + (q2 - q1) * hue / 60;
            }

            if(hue < 180)
            {
                return q2;
            }

            if(hue < 240)
            {
                return q1 + (q2 - q1) * (240 - hue) / 60;
            }
            return q1;
        }

        //FILE 
        
        public static void bitmapToJpeg(Bitmap bitmap, string destinationPath, int quality)
        {
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();
            ImageCodecInfo ici = null;

            foreach(ImageCodecInfo codec in codecs)
            {
                if(codec.MimeType == "image/jpeg")
                {
                    ici = codec;
                }
            }

            EncoderParameters ep = new EncoderParameters();
            ep.Param[0] = new EncoderParameter(Encoder.Quality, (long)quality);
            bitmap.Save(destinationPath + "\\bitmapPalette.jpg", ici, ep);
        }

        public static void imageToJpeg(string filePath, string destinationPath, int quality)
        {
            Bitmap bitmap = (Bitmap)Image.FromFile(filePath);
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();
            ImageCodecInfo ici = null;
            
            foreach (ImageCodecInfo codec in codecs)
            {
                if (codec.MimeType == "image/jpeg")
                {
                    ici = codec;
                }
            }

            EncoderParameters ep = new EncoderParameters();
            ep.Param[0] = new EncoderParameter(Encoder.Quality, (long)quality);
            bitmap.Save(destinationPath + "\\convertedImage.jpg", ici, ep);
        }

        public static void bitmapToPng(Bitmap bitmap, string destinationPath, int transparency)
        {
            switch (transparency)
            {
                case 1:
                    {
                        bitmap.Save(destinationPath + "\\covertedImage.png", ImageFormat.Png);
                    }
                    break;
                case 2:
                    {
                        bitmap.MakeTransparent(Color.White);
                        bitmap.Save(destinationPath + "\\covertedImage.png", ImageFormat.Png);
                    }
                    break;
                case 3:
                    {
                        bitmap.MakeTransparent(Color.Black);
                        bitmap.Save(destinationPath + "\\covertedImage.png", ImageFormat.Png);
                    }
                    break;
            }
        }

        public static void imageToPng(string filePath, string destinationPath, int transparency)
        {
            Bitmap bitmap = (Bitmap)Image.FromFile(filePath);
            switch (transparency)
            {
                case 1:
                    {
                        bitmap.Save(destinationPath + "\\covertedImage.png", ImageFormat.Png);
                    } break;
                case 2: 
                    {
                        bitmap.MakeTransparent(Color.White);
                        bitmap.Save(destinationPath + "\\covertedImage.png", ImageFormat.Png);
                    } break;
                case 3:
                    {
                        bitmap.MakeTransparent(Color.Black);
                        bitmap.Save(destinationPath + "\\covertedImage.png", ImageFormat.Png);
                    } break;
            }
        }

        public static void bitmapToBmp(Bitmap bitmap, string destination)
        {
            bitmap.Save(destination + "\\convertedImage.bmp", ImageFormat.Bmp);
        }

        public static void imageToBmp(string filePath, string destination)
        {
            Bitmap bitmap = (Bitmap)Image.FromFile(filePath);
            bitmap.Save(destination + "\\convertedImage.bmp");
        }

        public static void colorContainerToJson(List<string> array, string destination)
        {
            File.WriteAllText(destination + "\\paletteLog.json", JsonConvert.SerializeObject(array, Formatting.Indented));
        }
    }

    struct HSL
    {
        public double H { get; set; }
        public double S { get; set; }
        public double L { get; set; }
        public HSL(double hue, double saturation, double luminance)
        {
            H = hue;
            S = saturation;
            L = luminance;
        }
    }
}
