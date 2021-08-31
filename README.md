
#  Alchemy-Engine 🧪

The Alchemy-Engine library is an extension of projects such as ColorShaper, ColorGenerator and GradientMaker. It is an independent, cross-platform library that facilitates image and color manipulation. It has many functions such as generating palettes, converting color spaces, compression etc. The library extends classes such as Bitmap and Color of the standard `System.Drawing` library with many new functions.

##  Structures

The library introduces the most used color spaces and the `System.Drawing` extension, converting between color spaces is very easy thanks to the Fluent API. The structures implement the `IConvertableColor` interfaces  which allow for conversion between other colors and `IRandomColor` which adds the random color method.

  

    //Creating a random color
	var myColor = Color.GetRandom();
    
    //Converting to hsl, updating the saturation and converting to cmyk
    var updatedCmykColor = myColor
	    .ToHsl()
	    .SetSaturation(0.8f);
	    .ToCmyk();
 
**Available color spaces**

  

- ✅ RGB (Extension)

- ✅ CMYK

- ✅ HSL

- ❌ HSV
- ⚠️YCbCr

  
  

##  Extensions

The library extends the `System.Drawing.Bitmap` class with new functionalities.

  

- ✅ `Scale(int percent)` - Scale the image by given percent.

- ❌ `Invert()` - Invert the colors (negative effect).

- ❌ `Grayscale()` - Make the image black and white.

-  ✅ `GetPallete(PalleteGenerator)` / `GetPalleteAsync(PalleteGenerator)` - Creates a color palette based on a bitmap, you can choose from different PalleteGenerator graphics sampling methods.

  
  ### PalleteGenerators
  - ✅ `PalleteGenerator.CubeMethod`
  - ✅ `PalleteGenerator.AdditionMethod`
  - ❌ `PalleteGenerator.VerticalStripMethod`
  - ❌ `PalleteGenerator.HorizontalStripMethod`

##  Processing

**ColorComparer**

Color comparison algorithms.

- ✅`Distance(T A, T B)` - Get the float value difference between colors.

**Compression**

  

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