using AlchemyEngine.Structures;
using System.Drawing;

namespace AlchemyEngine.Abstraction
{
    public interface IConvertableColor
    {
        Color ToColor();
        Cmyk ToCmyk();
        Hsl ToHsl();
        YCbCr ToYCbCr();
        Yuv ToYuv();
    }
}
