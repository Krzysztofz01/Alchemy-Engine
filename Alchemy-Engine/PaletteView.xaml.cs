using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Alchemy_Engine
{
    /// <summary>
    /// Interaction logic for PaletteView.xaml
    /// </summary>
    public partial class PaletteView : Window
    {
        private string filePath = null;
        public AnalyzerResults results;
        public PaletteView()
        {
            InitializeComponent();
        }

        private void btnSelectFileListener(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Multiselect = false;
            fileDialog.Filter = "Image files (*.jpg, *.jpeg, *.bmp, *.png) | *.jpg; *.jpeg; *.bmp; *.png";
            fileDialog.DefaultExt = ".jpg";

            if(fileDialog.ShowDialog() == true)
            {
                btnSampleImage.IsEnabled = true;
                this.filePath = fileDialog.FileName;
                btnSelectFile.Content = fileDialog.SafeFileName;
                imageHolder.Source = new BitmapImage(new Uri(this.filePath));
            }
        }

        private async void btnSampleImageListener(object sender, RoutedEventArgs e)
        {
            btnSampleImage.Content = "Please wait";
            this.results = await Task.Run(() => sampleProcess(this.filePath));
            if((this.results.image != null)&&(this.results.colors != null))
            {
                btnSaveImage.IsEnabled = true;
            }
            btnSampleImage.Content = "Finished";
        }

        private AnalyzerResults sampleProcess(string filePath)
        {
            if(filePath != null)
            {
                AlchemyAnalyzer analyzer = new AlchemyAnalyzer(new Bitmap(filePath), 25);
                analyzer.samplePaletteLock();
                return analyzer.getOutput();
            }
            return new AnalyzerResults(null, null);
        }

        private void btnSaveImageListener(object sender, RoutedEventArgs e)
        {
            Console.WriteLine(results.colors.ToString());
            //imageHolder.Source = AlchemyConverter.bitmapToBitmapSource(results.image);
            imageHolder.Source = AlchemyConverter.bitmapToBitmapSource(results.image);
            
            //Window exportPalette = new ExportView(analyzer.getPaletteImage(), analyzer.getPaletteLog());
            //exportPalette.Show();
        }
    }
}