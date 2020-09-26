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
        private static extern int DeleteObject(IntPtr o);
        
        /* === COLORS === */

        /// <summary>
        /// Convert Bitmap object into BitmapSource object
        /// </summary>
        /// <param name="bitmap">Input Drawing Bitmap object</param>
        /// <returns>BitmapSource object</returns>
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

        /// <summary>
        /// Convert Drawing Color object into hex string
        /// </summary>
        /// <param name="color">Color object</param>
        /// <returns>Hex string</returns>
        public static string colorToHex(Color color)
        {
            return "#" + color.R.ToString("X2") + color.G.ToString("X2") + color.B.ToString("X2");
        }

        /// <summary>
        /// Convert RGB int values into hex string
        /// </summary>
        /// <param name="R">Red int</param>
        /// <param name="G">Green int</param>
        /// <param name="B">Blue int</param>
        /// <returns>Hex string</returns>
        public static string colorToHex(int R, int G, int B)
        {
            return "#" + R.ToString("X2") + G.ToString("X2") + B.ToString("X2");
        }

        /// <summary>
        /// Convert WinMedia Color object int hex string
        /// </summary>
        /// <param name="color">WinMedia Color object</param>
        /// <returns>Hex string</returns>
        public static string colorToHex(System.Windows.Media.Color color)
        {
            return "#" + color.R.ToString("X2") + color.G.ToString("X2") + color.B.ToString("X2");
        }

        /// <summary>
        /// Convert RGB (in that order as in memory -> BGR) byte values into hex values
        /// </summary>
        /// <param name="b">Blue byte</param>
        /// <param name="g">Green byte</param>
        /// <param name="r">Red byte</param>
        /// <returns>Hex string</returns>
        public static string colorToHex(byte b, byte g, byte r)
        {
            return "#" + r.ToString("X2") + g.ToString("X2") + b.ToString("X2");
        }

        /// <summary>
        /// Convert hex string color into Color object
        /// </summary>
        /// <param name="color">Hex string</param>
        /// <returns>Color object</returns>
        public static Color hexToColor(string color)
        {
            return ColorTranslator.FromHtml(color);
        }

        /// <summary>
        /// Convert Drawing.Color object into HSL struct
        /// </summary>
        /// <param name="color">Color object</param>
        /// <returns>HSL struct</returns>
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

        /// <summary>
        /// Convert HSL struct into Drawing.Color object
        /// </summary>
        /// <param name="hsl">HSL struct</param>
        /// <returns>Color object</returns>
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

        /* === FILE === */
        
        /// <summary>
        /// Save bitmap as JPEG file
        /// </summary>
        /// <param name="bitmap">Bitmap object</param>
        /// <param name="destinationPath">File path</param>
        /// <param name="quality">JPEG compression</param>
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

        /// <summary>
        /// Convert * image file into JPEG file
        /// </summary>
        /// <param name="filePath">Source path</param>
        /// <param name="destinationPath">Save path</param>
        /// <param name="quality">JPEG compression</param>
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

        /// <summary>
        /// Save bitmap as PNG file
        /// </summary>
        /// <param name="bitmap">Bitmap object</param>
        /// <param name="destinationPath">File path</param>
        /// <param name="transparency">Transparency settings (1 = default, 2 = on white, 3 = on black)</param>
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

        /// <summary>
        /// Convert * image file into PNG file
        /// </summary>
        /// <param name="filePath">Source file path</param>
        /// <param name="destinationPath">Save file path</param>
        /// <param name="transparency">Transparency settings (1 = default, 2 = on white, 3 = on black)</param>
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

        /// <summary>
        /// Save bitmap as BMP file
        /// </summary>
        /// <param name="bitmap">Bitmap object</param>
        /// <param name="destination">File path</param>
        public static void bitmapToBmp(Bitmap bitmap, string destination)
        {
            bitmap.Save(destination + "\\convertedImage.bmp", ImageFormat.Bmp);
        }

        /// <summary>
        /// Convert * image into BMP file
        /// </summary>
        /// <param name="filePath">Source file path</param>
        /// <param name="destination">Save file path</param>
        public static void imageToBmp(string filePath, string destination)
        {
            Bitmap bitmap = (Bitmap)Image.FromFile(filePath);
            bitmap.Save(destination + "\\convertedImage.bmp");
        }

        /// <summary>
        /// Convert list of colors into JSON file
        /// </summary>
        /// <param name="array">List of colors</param>
        /// <param name="destination">File path</param>
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
