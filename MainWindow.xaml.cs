using mynance.src.localization;
using mynance.src.navigation;
using mynance.src.navigation.pages;
using mynance.src.styles;
using System.Windows;

namespace mynance
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            ThemeHandler.SetDarkTheme();
            LocaleHandler.SetEnglish();
            Navigator.Next(new LoginPage());
        }
    }
}