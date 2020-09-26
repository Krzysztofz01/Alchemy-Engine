using Microsoft.Win32;
using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace Alchemy_Engine
{
    /// <summary>
    /// Interaction logic for LumetriView.xaml
    /// </summary>
    public partial class LumetriView : Window
    {
        private AlchemyLumetri lumetri = null;
        private string filePath = null;
        
        public LumetriView()
        {
            InitializeComponent();

            selectFilter.Items.Add("Grey scale");
            selectFilter.Items.Add("Negative");
            selectFilter.Items.Add("Brightness");
            

            selectFilter.IsEnabled = false;
            setting1Name.Visibility = Visibility.Hidden;
            setting1Value.Visibility = Visibility.Hidden;
            setting2Name.Visibility = Visibility.Hidden;
            setting2Value.Visibility = Visibility.Hidden;
            setting3Name.Visibility = Visibility.Hidden;
            setting3Value.Visibility = Visibility.Hidden;
            setting4Name.Visibility = Visibility.Hidden;
            setting4Value.Visibility = Visibility.Hidden;
            applyChanges.Visibility = Visibility.Hidden;
            navBack.IsEnabled = false;
            navForward.IsEnabled = false;
            exportImage.IsEnabled = false;
        }

        private void btnSelectImageListener(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Multiselect = false;
            fileDialog.Filter = "Image files (*.jpg, *.jpeg, *.bmp, *.png) | *.jpg; *.jpeg; *.bmp; *.png";
            fileDialog.DefaultExt = ".jpg";

            if (fileDialog.ShowDialog() == true)
            {
                this.filePath = fileDialog.FileName;
                this.lumetri = new AlchemyLumetri(new Bitmap(this.filePath));
                imageHolder.Source = new BitmapImage(new Uri(this.filePath));

                selectFilter.IsEnabled = true;
                navBack.IsEnabled = true;
                navForward.IsEnabled = true;
                exportImage.IsEnabled = true;
            }
        }

        private void applyFilterToImage(object selectedFilter, double[] settings)
        {
            switch(selectedFilter)
            {
                case "Grey scale": this.lumetri.greyScale(); break;
                case "Negative": this.lumetri.invertColors(); break;
                case "Brightness": this.lumetri.brightnessFilter((int)settings[0]); break;
                default: break;
            }
        }

        private async void btnApplyChangesListener(object sender, RoutedEventArgs e)
        {
            object selectedFilter = selectFilter.SelectedItem;
            double[] settings = { setting1Value.Value, setting2Value.Value, setting3Value.Value, setting4Value.Value };
            await Task.Run(() => applyFilterToImage(selectedFilter, settings));
            imageHolder.Source = AlchemyConverter.bitmapToBitmapSource(lumetri.getItem());
        }

        private void cbSelectionChangedListener(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            setting1Name.Visibility = Visibility.Hidden;
            setting1Value.Visibility = Visibility.Hidden;
            setting2Name.Visibility = Visibility.Hidden;
            setting2Value.Visibility = Visibility.Hidden;
            setting3Name.Visibility = Visibility.Hidden;
            setting3Value.Visibility = Visibility.Hidden;
            setting4Name.Visibility = Visibility.Hidden;
            setting4Value.Visibility = Visibility.Hidden;
            applyChanges.Visibility = Visibility.Hidden;

            switch (selectFilter.SelectedItem)
            {
                case "Grey scale": 
                    {
                        applyChanges.Visibility = Visibility.Visible;
                    }; break;
                case "Negative": 
                    {
                        applyChanges.Visibility = Visibility.Visible;
                    }; break;
                case "Brightness":
                    {
                        setting1Value.Visibility = Visibility.Visible;
                        setting1Value.Minimum = -100;
                        setting1Value.Maximum = 100;
                        setting2Name.Content = "Brightness: ";
                        applyChanges.Visibility = Visibility.Visible;
                    }; break;
            }
        }

        private void btnNavBackListener(object sender, RoutedEventArgs e)
        {
            imageHolder.Source = new BitmapImage(new Uri(this.filePath));
            //imageHolder.Source = AlchemyConverter.bitmapToBitmapSource(lumetri.getItemOffset(-1));
        }

        private void btnNavForwardListener(object sender, RoutedEventArgs e)
        {
            imageHolder.Source = AlchemyConverter.bitmapToBitmapSource(lumetri.getItemOffset(-1));
        }
    }
}
