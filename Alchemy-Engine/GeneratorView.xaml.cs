using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Alchemy_Engine
{
    /// <summary>
    /// Interaction logic for GeneratorView.xaml
    /// </summary>
    public partial class GeneratorView : Window
    {
        private Color firstColor = Color.FromRgb(255, 255, 255);
        private Color secondColor = Color.FromRgb(255, 255, 255);
        public GeneratorView()
        {
            InitializeComponent();

            cbType.Items.Add("Solid color");
            cbType.Items.Add("Linear gradient");
            cbType.SelectedIndex = 0;

            btnCopyFirst.Visibility = Visibility.Hidden;
            btnCopySecond.Visibility = Visibility.Hidden;
        }

        private void btnGenerateListener(object sender, RoutedEventArgs e)
        {
            btnCopyFirst.Visibility = Visibility.Hidden;
            btnCopySecond.Visibility = Visibility.Hidden;

            Label lbl = new Label();
            Grid.SetColumn(lbl, 1);
            Grid.SetRow(lbl, 1);

            switch(cbType.SelectedItem)
            {
                case "Solid color":
                    {
                        firstColor = AlchemyAlgorithm.getRandomBrushColor();
                        secondColor = Color.FromRgb(255, 255, 255);
                        lbl.Background = new SolidColorBrush(firstColor);

                        Grid.SetColumn(btnCopyFirst, 1);
                        btnCopyFirst.Content = AlchemyConverter.mediaColorToHex(firstColor);

                        btnCopyFirst.Visibility = Visibility.Visible;
                    }
                    break;
                case "Linear gradient":
                    {
                        firstColor = AlchemyAlgorithm.getRandomBrushColor();
                        secondColor = AlchemyAlgorithm.getRandomBrushColor();
                        lbl.Background = new LinearGradientBrush(firstColor, secondColor, 90.0);

                        Grid.SetColumn(btnCopyFirst, 0);
                        btnCopyFirst.Content = AlchemyConverter.mediaColorToHex(firstColor);
                        btnCopySecond.Content = AlchemyConverter.mediaColorToHex(firstColor);

                        btnCopyFirst.Visibility = Visibility.Visible;
                        btnCopySecond.Visibility = Visibility.Visible;
                    }
                    break;
                default: break;
            }
            mainGrid.Children.Add(lbl);
        }

        private void btnCopyFirstListener(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(AlchemyConverter.mediaColorToHex(firstColor));
        }

        private void btnCopySecondLIstener(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(AlchemyConverter.mediaColorToHex(secondColor));
        }
    }
}
