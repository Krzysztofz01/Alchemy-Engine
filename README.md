# Alchemy-Engine 🧪
The Alchemy-Engine library is an extension of projects such as ColorShaper, ColorGenerator and GradientMaker. It is an independent, cross-platform library that facilitates image and color manipulation. It has many functions such as generating palettes, converting color spaces, compression etc. The library extends classes such as Bitmap and Color of the standard System.Drawing library with many new functions.

## Structures
Different color spaces allow for easier manipulation of colors and entire graphics. Each color space can be converted to another according to the following formula:

    //Creating a Rgb color 
    var color = new Rgb(255, 255, 255);
    //Converting the Rgb color to Cmyk
    var cmykColor = color.ToCmyk();

The library extends the `System.Drawing.Color` class with methods to convert this class to other color spaces.

**Available color spaces**

 - ✅ RGB
 - ❌ CMYK
 - ❌ HSL
 - ❌ HSV


## Extensions
The library extends the `System.Drawing.Bitmap` class with new functionalities.

 - ✅ `Scale(int percent)` - Scale the image by given percent.
 - ✅ `Invert()` - Invert the colors (negative effect).
 - ✅ `Grayscale()` - Make the image black and white.
 - ✅ `Brightness(int value)` - Change the brightness of the image by given value.
 - ✅ `Contrast(int value)` - Change the contrast of the image by given value.
 - ✅ `Channel(Channel channel)` - Subtract of the the three channels (red, green or blue).
 - ❌`Blur()`
 - ❌`GaussianBlur()`
 - ❌`MotionBlur()`
 - ❌`Soften()`
 - ❌`Sharpen()`
 - ❌`EgdeDetection()`
 - ❌`Emboss()`
 - ❌`HighPass()`
 - ❌`Tint(Color color)` - Apply a color filter on the whole image, you can create a "vintage look".

## Processing
**Palette**
Generate color palettes based on a given image or color.

 - ✅`GetGridPalleteValues(Bitmap image, int imageScale, int colorDifferenceThreshold)` - Get a collection of colors using the "grid" method.
 - ✅`GetGridPalleteImage(Bitmap image, int imageScale, int colorDifferenceThreshold)` - Get a bitmap with a palette using the "grid" method.
 - ✅`GetSumPalleteValues(Bitmap image, int imageScale)` - Get a collection of colors using the "sum" method.
 - ✅`GetSumPalleteImage(Bitmap image, int imageScale)` - Get a bitmap with a palette using the "sum" method.
 - ❌`GetMonochromaticValues(Color base)` - Get a collection of colors based on the given base color.
 - ❌`GetComplementaryValues(Color base)` - Get a collection of colors based on the given base color.

**Filter**
Color comparison algorithms.

 - ✅`GetDifference(Color A, Color B)` - Get the int value difference between colors.
 - ✅`IsDifferent(Color A, Color B, int threshold)` -  Get false if the color difference meets the given threshold.

**Compression**
Various algorithms that compress a given bitmap.

 - ✅`Pixelate(Bitmap bitmap, int pixelSize)` - Get pixelated version of given bitmap, based on given pixel size.

**Blending**
Mix colors based on the different methods given in the BlendingModes enum:

 - ❌`Normal`
 - ❌`Multiply`
 - ❌`Screen`
 - ❌`Overlay`
 - ❌`HardLight`
 - ❌`SoftLight`

**Glitch**
Add character to the image with various distortions and glitches.

 - ❌`PixelShift`
 - ❌`ChromaticShift`
 - ❌`Noise`
 - ❌`VhsOverlay`
