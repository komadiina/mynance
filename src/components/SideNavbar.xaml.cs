using mynance.src.auth;
using mynance.src.localization;
using mynance.src.navigation;
using mynance.src.navigation.pages;
using mynance.src.styles;
using System.Windows;
using System.Windows.Controls;

namespace mynance.src.components
{
	/// <summary>
	/// Interaction logic for SideNavbar.xaml
	/// </summary>
	public partial class SideNavbar : UserControl
	{
		public SideNavbar()
		{
			InitializeComponent();
			UserFullname.Content = AuthGate.CurrentUser.FullName;

			btnThemeSwitch.Content = ThemeHandler.IsDarkMode
				? LocalizationProvider.GetLocalizedValue<string>("LightTheme")
				: LocalizationProvider.GetLocalizedValue<string>("DarkTheme");

			btnLocaleSwitch.Content = LocaleHandler.Instance.CurrentLocale == "en-US"
				? LocalizationProvider.GetLocalizedValue<string>("LocaleSerbian")
				: LocalizationProvider.GetLocalizedValue<string>("LocaleEnglish");

			NavCalendar.Visibility = AuthGate.Role == 1 ? Visibility.Hidden : Visibility.Visible;
			NavPayment.Visibility = AuthGate.Role == 1 ? Visibility.Hidden : Visibility.Visible;
		}

		private void updateButtons(String cultureCode)
		{
		}

		public void NavProfileSettings_Click(object sender, RoutedEventArgs e) => Navigator.Replace(new ProfilePage());
		public void NavCalendar_Click(object sender, RoutedEventArgs e) => Navigator.Replace(new HistoryPage());
		private void NavPayment_Click(object sender, RoutedEventArgs e) => Navigator.Replace(new PaymentPage());
		public void NavLogout_Click(object sender, RoutedEventArgs e)
		{
			AuthGate.Logout();
			Navigator.Reset();
		}

		private void NavDashboard_Click(object sender, RoutedEventArgs e)
		{
			Navigator.Replace(AuthGate.Role == 1 ? new LandingAdministrator() : new LandingUser());
		}

		public void btnLocaleSwitch_Click(object sender, RoutedEventArgs e)
		{
			if (LocaleHandler.Instance.CurrentLocale == "en-US")
			{
				LocaleHandler.Instance.SetSerbian();
				btnLocaleSwitch.Content = LocalizationProvider.GetLocalizedValue<string>("LocaleEnglish");
				btnThemeSwitch.Content = ThemeHandler.IsDarkMode
					? LocalizationProvider.GetLocalizedValue<string>("DarkTheme")
					: LocalizationProvider.GetLocalizedValue<string>("LightTheme");
			}
			else
			{
				LocaleHandler.Instance.SetEnglish();
				btnLocaleSwitch.Content = LocalizationProvider.GetLocalizedValue<string>("LocaleSerbian");
				btnThemeSwitch.Content = ThemeHandler.IsDarkMode
					? LocalizationProvider.GetLocalizedValue<string>("DarkTheme")
					: LocalizationProvider.GetLocalizedValue<string>("LightTheme");
			}
		}

		public void btnThemeSwitch_Click(object sender, RoutedEventArgs e)
		{
			if (ThemeHandler.IsDarkMode)
			{
				ThemeHandler.SetLightTheme();
				btnThemeSwitch.Content = LocalizationProvider.GetLocalizedValue<string>("DarkTheme");
			}
			else
			{
				ThemeHandler.SetDarkTheme();
				btnThemeSwitch.Content = LocalizationProvider.GetLocalizedValue<string>("LightTheme");
			}
		}
	}
}
