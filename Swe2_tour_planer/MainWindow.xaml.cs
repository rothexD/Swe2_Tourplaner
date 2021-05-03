using System.Windows;
using Swe2_tour_planer.ViewModels;

[assembly: log4net.Config.XmlConfigurator(Watch = true)]
namespace Swe2_tour_planer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            DataContext = new MainViewModel();
            InitializeComponent();       
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void TextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {

        }

        private void TabControl_SelectionChanged()
        {

        }
    }
}
