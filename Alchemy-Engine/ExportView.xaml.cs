using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using winForm = System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Alchemy_Engine
{
    /// <summary>
    /// Interaction logic for ExportView.xaml
    /// </summary>
    public partial class ExportView : Window
    {
        private string filePath = null;
        private string destinationPath = null;
        private Bitmap bitmap = null;
        private List<string> sampledColors = null;
        public ExportView()
        {
            InitializeComponent();

            cbImageFormat.Items.Add("JPG (Photography and WWW)");
            cbImageFormat.Items.Add("PNG (Supports transparency)");
            cbImageFormat.Items.Add("BMP (Default bitmap format)");
            cbImageFormat.Items.Add("RAW (Raw text image data)");
            cbImageFormat.SelectedIndex = 0;
            
            btnExport.IsEnabled = false;
            btnSaveLocation.IsEnabled = false;
        }

        public ExportView(Bitmap bitmap, List<string> sampledColors)
        {
            InitializeComponent();

            cbImageFormat.Items.Add("JPG (Photography and WWW)");
            cbImageFormat.Items.Add("PNG (Supports transparency)");
            cbImageFormat.Items.Add("BMP (Default bitmap format)");
            cbImageFormat.Items.Add("JSON (Save palette to JSON file)");
            cbImageFormat.SelectedIndex = 0;

            btnExport.IsEnabled = false;
            btnSelectFile.IsEnabled = false;
            btnSelectFile.Content = "Palette Image";

            this.bitmap = bitmap;
            this.sampledColors = sampledColors;
        }

        private void btnSelectFileListener(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Multiselect = false;
            fileDialog.Filter = "Image files (*.jpg, *.jpeg, *.bmp, *.png) | *.jpg; *.jpeg; *.bmp; *.png";
            fileDialog.DefaultExt = ".jpg";

            if (fileDialog.ShowDialog() == true)
            {
                btnSaveLocation.IsEnabled = true;
                this.filePath = fileDialog.FileName;
                btnSelectFile.Content = "File: " + fileDialog.SafeFileName;
            }
        }

        private void btnSaveLocationListener(object sender, RoutedEventArgs e)
        {
            winForm.FolderBrowserDialog folderDialog = new winForm.FolderBrowserDialog();
            
            if(folderDialog.ShowDialog() == winForm.DialogResult.OK)
            {
                btnExport.IsEnabled = true;
                this.destinationPath = folderDialog.SelectedPath;
                btnSaveLocation.Content = "Destination: " + folderDialog.SelectedPath;
                Console.WriteLine(this.destinationPath);
            }
        }

        private void btnExportListener(object sender, RoutedEventArgs e)
        {
            if((this.destinationPath != null)&&((this.filePath != null)||(this.bitmap != null)))
            {
                switch(cbImageFormat.SelectedItem)
                {

                }
            }
        }

        private void cbImageFormatListener(object sender, SelectionChangedEventArgs e)
        {
            switch (cbImageFormat.SelectedItem)
            {
                case "JPG (Photography and WWW)":
                    {
                        slName.Content = "Jpeg quality: 100";
                        slSettings.IsEnabled = true;
                        slSettings.Minimum = 1;
                        slSettings.Maximum = 100;
                        slSettings.Value = 100;
                    }
                    break;
                case "PNG (Supports transparency)":
                    {
                        slName.Content = "Png transparency: default";
                        slSettings.IsEnabled = true;
                        slSettings.Minimum = 1;
                        slSettings.Maximum = 3;
                        slSettings.Value = 1;
                    }
                    break;
                case "BMP (Default bitmap format)":
                    {
                        slName.Content = "";
                        slSettings.IsEnabled = false;
                        slSettings.Minimum = 1;
                        slSettings.Maximum = 100;
                        slSettings.Value = 50;
                    }
                    break;
            }
        }

        private void slSettingsListener(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            switch (cbImageFormat.SelectedItem)
            {
                case "JPG (Photography and WWW)":
                    {
                        slName.Content = "Jpeg quality: " + ((int)slSettings.Value).ToString();
                    }
                    break;
                case "PNG (Supports transparency)":
                    {
                        switch(slSettings.Value)
                        {
                            case 1: slName.Content = "Png transparency: default"; break;
                            case 2: slName.Content = "Png transparency: black"; break;
                            case 3: slName.Content = "Png transparency: white"; break;
                        }   
                    }
                    break;
                default: break;
            }
        }
    }
}
