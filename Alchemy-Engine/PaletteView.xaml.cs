using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Drawing;
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
        private AlchemyAnalyzer analyzer = null;
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
            if(this.filePath != null)
            {
                analyzer = new AlchemyAnalyzer(new Bitmap(this.filePath), 25);
                analyzer.samplePaletteLock();
                /*List<Label> outputLabelArray = analyzer.getColors(5, true, 80);

                foreach(Label label in outputLabelArray)
                {
                    paletteGrid.Children.Add(label);
                }

                btnSaveImage.IsEnabled = true; 
                */
            }

            
        }

        private void btnSaveImageListener(object sender, RoutedEventArgs e)
        {
            if(this.analyzer != null)
            {
                Window exportPalette = new ExportView(analyzer.getPaletteImage(), analyzer.getPaletteLog());
                exportPalette.Show();
            }
        }
    }
}