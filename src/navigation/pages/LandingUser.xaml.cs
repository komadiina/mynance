using mynance.src.localization;
using mynance.src.navigation.pages.controllers;
using mynance.src.styles;
using System.Windows;
using System.Windows.Controls;

namespace mynance.src.navigation.pages
{
	/// <summary>
	/// Interaction logic for LandingUser.xaml
	/// </summary>
	public partial class LandingUser : Page
	{
		private UserDashboardController Controller = new();
		protected LocalizedString PageDateIndicator = new("SideNavbar_Dashboard");

		public LandingUser()
		{
			InitializeComponent();
			//lblPageDateIndicator.Content = String.Format(
			//	"{0} | {1} {2}",
			//	LocalizationProvider.GetLocalizedValue<string>("SideNavbar_Dashboard"),
			//	LocalizationProvider.GetLocalizedValue<string>("ProfilePage_BalanceStatus"),
			//	DateTimeUtils.GetTodaysDate());

			lblPageDateIndicator.Content = PageDateIndicator.Value;

			lblBalanceAmount.Content = String.Format(
				"{0:.00} {1}",
				Controller.Budget.Amount,
				Controller.Currency.AlphabeticCode
				);
		}

		public void btnLightTheme_Click(object sender, RoutedEventArgs e) => ThemeHandler.SetLightTheme();
		public void btnDarkTheme_Click(object sender, RoutedEventArgs e) => ThemeHandler.SetDarkTheme();
		public void btnEnglish_Click(object sender, RoutedEventArgs e) => LocaleHandler.Instance.SetEnglish();
		public void btnSerbian_Click(object sender, RoutedEventArgs e) => LocaleHandler.Instance.SetSerbian();
	}
}
