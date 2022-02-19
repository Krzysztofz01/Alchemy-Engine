
#  Alchemy-Engine 🧪

The Alchemy-Engine library is an extension of projects such as ColorShaper, ColorGenerator and GradientMaker. It is an independent, cross-platform library that facilitates image and color manipulation. It has many functions such as generating palettes, converting color spaces, compression etc. The library extends classes such as Bitmap and Color of the standard `System.Drawing` library with many new functions.

##  Structures

The library introduces the most used color spaces and the `System.Drawing` extension, converting between color spaces is very easy thanks to the Fluent API. The structures implement the `IConvertableColor` interface which allow for conversion between other colors.

```
//Creating a random color
var myColor = Color.GetRandom();

// Make changes using features of all color spaces    
// Example:
// Convert to hsl to update saturation, convert to cmyk to add cyan tint, convert back to rgb color
var updatedCmykColor = myColor
	.ToHsl()
	.SetSaturation(0.8d)
	.ToCmyk()
	.SetCyan(c => c + 0.2d)
	.ToColor();
```
 
**Available color spaces**
- ✅ RGB (Extension)
- ✅ CMYK
- ✅ HSL
- ❌ HSV
- ✅ YUV
- ⚠️ YCbCr

##  Extensions

The library extends the `System.Drawing.Bitmap` class with new functionalities.

- ✅ `Scale(int percent)` - Scale the image by given percent.
- ✅ `Invert()` - Invert the colors (negative effect).
- ✅ `Grayscale()` - Make the image black and white.
- ❌ `Brightness(int value)` - Change the brightness (Values between -100 and 100).
- ❌ `Contrast(int value)` - Change the contrast (Values between -100 and 100).
- ❌ `ChannelFilter(Channel)` - Extract the channel selected by the `Channel` enum.
- ✅ `GetPallete(PalleteGenerator)` / `GetPalleteAsync(PalleteGenerator)` - Creates a color palette based on a bitmap, you can choose from different `PalleteGenerator` graphics sampling methods.

```PalleteGenerator``` enum:
- ✅ `PalleteGenerator.CubeMethod`
- ✅ `PalleteGenerator.AdditionMethod`
- ❌ `PalleteGenerator.VerticalStripeMethod`
- ❌ `PalleteGenerator.HorizontalStripeMethod`

```Channel``` enum:
- ❌ `Channel.Red`
- ❌ `Channel.Green`
- ❌ `Channel.Blue`

##  Processing

**ColorComparer**

Color comparison algorithms. Get the double value difference between colors.

- ✅ ```ColorComparer.Distance(Color a, Color b)```
- ✅ ```ColorComparer.Distance(Color a, IConvertableColor b)```
- ✅ ```ColorComparer.Distance(IConvertableColor a, IConvertableColor b)```

**Compression**

**Blending**

Mix colors based on the different methods given in the ``BlendingMode`` enum:

- ✅ ```ColorBlender.Blend(Color a, Color b, BledingMode blendingMode)```
- ✅ ```ColorBlender.Blend(IConvertableColor a, IConvertableColor b, BlendingMode blendingMode)```

```BlendingMode``` enum:
- ✅ ```BlendingMode.Normal```
- ✅ ```BlendingMode.Darken```
- ✅ ```BlendingMode.Multiply```
- ✅ ```BlendingMode.Screen```
- ✅ ```BlendingMode.Overlay```
- ✅ ```BlendingMode.SoftLight```
- ✅ ```BlendingMode.HardLight```
- ✅ ```BlendingMode.Difference```
- ✅ ```BlendingMode.Divide```
- ✅ ```BlendingMode.Addition```
- ✅ ```BlendingMode.Subtract```      

**Glitch**

Add character to the image with various distortions and glitches.
- ❌`PixelShift`
- ❌`ChromaticShift`
- ❌`Noise`
- ❌`VhsOverlay`


## Support
The target fremework of Alchemy-Engine is .NET Standard 2.1, which offers many compatible versions except .NET Framework (Windows). Full specification of compatible versions can be found in the [documentation](https://docs.microsoft.com/en-us/dotnet/standard/net-standard?tabs=net-standard-2-0).