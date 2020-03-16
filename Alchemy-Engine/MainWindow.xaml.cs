using System.Windows;

namespace Alchemy_Engine
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            /* LUMETRI TEST SECTION
            var lumetri = new AlchemyLumetri(new System.Drawing.Bitmap(@"D:\Projekty\Grafika\Zdjęcia do testów\test.jpg"));
            lumetri.greyScale();
            AlchemyConverter.bitmapToJpeg(lumetri.getItem(), @"D:\Projekty\Grafika\Zdjęcia do testów", 100);
            */
        }

        private void btnPalleteListener(object sender, RoutedEventArgs e)
        {
            Window palleteGenerator = new PaletteView();
            palleteGenerator.Show();
        }

        private void btnConvertListener(object sender, RoutedEventArgs e)
        {
            Window exportMenu = new ExportView();
            exportMenu.Show();
        }

        private void btnGeneratorListener(object sender, RoutedEventArgs e)
        {
            Window solidGeneretor = new GeneratorView();
            solidGeneretor.Show();
        }
    }
}