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

        public ExportView(Bitmap palleteBitmap)
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
    }
}
