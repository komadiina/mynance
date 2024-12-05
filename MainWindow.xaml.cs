using AdonisUI.Controls;
using mynance.src.localization;
using mynance.src.navigation;
using mynance.src.navigation.pages;
using mynance.src.styles;

namespace mynance
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : AdonisWindow
	{
		public MainWindow()
		{
			ThemeHandler.SetDarkTheme();
			LocaleHandler.Instance.SetEnglish();
			Navigator.Next(new LoginPage());
		}
	}
}