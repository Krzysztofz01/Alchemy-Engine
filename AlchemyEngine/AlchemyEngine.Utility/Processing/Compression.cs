using System.Drawing;

namespace AlchemyEngine.Utility.Processing
{
    public static class Compression
    {
        //Server conpression
        

        public static Bitmap Pixelate(Bitmap input, int pixelSize)
        {
            var pixelatedBitmap = new Bitmap(input.Width, input.Height);
            var rect = new Rectangle(0, 0, input.Width, input.Height);

            using (var graphics = Graphics.FromImage(pixelatedBitmap))
            {
                graphics.DrawImage(input, 
                    new Rectangle(0, 0, input.Width, input.Height),
                    new Rectangle(0, 0, input.Width, input.Height),
                    GraphicsUnit.Pixel);
            }

            for(int xx = rect.X; xx < rect.X + rect.Width && xx < input.Width; xx += pixelSize)
            {
                for(int yy = rect.Y; yy < rect.Y + rect.Height && yy < input.Height; yy += pixelSize)
                {
                    int offsetX = pixelSize / 2;
                    int offsetY = pixelSize / 2;

                    while (xx + offsetX >= input.Width) offsetX--;
                    while (yy + offsetY >= input.Height) offsetY--;

                    //inefficient way
                    var pixel = pixelatedBitmap.GetPixel(xx + offsetX, yy + offsetY);
                    
                    for(int x = xx; x < xx + pixelSize && x < input.Width; x++)
                    {
                        for(int y = yy; y < yy + pixelSize && y < input.Height; y++)
                        {
                            pixelatedBitmap.SetPixel(x, y, pixel);
                        }
                    }
                }
            }
            return pixelatedBitmap;
        }
    }
}
