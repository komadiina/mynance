using AdonisUI.Controls;
using mynance.src.localization;
using mynance.src.styles;
using System.Globalization;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WPFLocalizeExtension.Engine;

namespace mynance
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            //InitializeComponent();
            //Style = (Style)FindResource(typeof(Window));
            ThemeHandler.SetDarkTheme();
            LocaleHandler.SetEnglish();
        }

        public void btnLightTheme_Click(object sender, RoutedEventArgs e) => ThemeHandler.SetLightTheme();
        public void btnDarkTheme_Click(object sender, RoutedEventArgs e) => ThemeHandler.SetDarkTheme();
        public  void btnEnglish_Click(object sender, RoutedEventArgs e) => LocaleHandler.SetEnglish();
        public void btnSerbian_Click(object sender, RoutedEventArgs e) => LocaleHandler.SetSerbian();
    }
}