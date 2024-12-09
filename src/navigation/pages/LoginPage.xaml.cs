using mynance.src.auth;
using mynance.src.exceptions;
using mynance.src.localization;
using mynance.src.styles;
using mynance.src.utilities;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace mynance.src.navigation.pages
{
	/// <summary>
	/// Interaction logic for LoginPage.xaml
	/// </summary>
	public partial class LoginPage : Page
	{
		public LoginPage()
		{
			Trace.WriteLine(DateTimeUtils.GetMonthExpiry());
			InitializeComponent();
			tbUsername.Focus();
		}

		private void BtnSignIn_Enter(object sender, KeyEventArgs e)
		{
			if (e.Key != Key.Enter) return;

			SetStatusText(HandleLogin(tbUsername.Text, pbPassword.Password));
		}

		private void BtnSignIn_Click(object sender, RoutedEventArgs e)
		{
			SetStatusText(HandleLogin(tbUsername.Text, pbPassword.Password));
			HandleRedirect(lblStatusText.Content as String);
		}

		private void pbPassword_Enter(object sender, KeyEventArgs e)
		{
			if (e.Key != Key.Enter) return;

			SetStatusText(HandleLogin(tbUsername.Text, pbPassword.Password));
			HandleRedirect(lblStatusText.Content as String);
		}

		private void BtnRegister_Enter(object sender, KeyEventArgs e)
		{
			if (e.Key != Key.Enter) return;
			Navigator.Next(new RegisterPage());

		}

		private void BtnRegister_Click(object sender, RoutedEventArgs e)
		{
			Navigator.Next(new RegisterPage());
		}

		private void SetStatusText(String message)
		{
			if (message != null)
			{
				lblStatusText.Visibility = Visibility.Visible;
				lblStatusText.Content = message;
			}
		}

		private String HandleLogin(String username, String password)
		{
			try
			{
				AuthGate auth = new AuthGate();
				auth.LoginUser(username, password);
			}
			catch (Exception ex)
			{
				lblStatusText.Foreground = Brushes.MediumVioletRed;

				if (ex is AuthException)
					return ex.Message;
				else return "Something went wrong.";
			}

			lblStatusText.Foreground = Brushes.Green;
			return "Success!";
		}

		private static void HandleRedirect(String message)
		{
			if (message == "Success!" && AuthGate.CurrentUser != null)
			{
				Navigator.Next(AuthGate.Role == 1 ? new LandingAdministrator() : new LandingUser());
			}
		}

		public void btnLightTheme_Click(object sender, RoutedEventArgs e) => ThemeHandler.SetLightTheme();
		public void btnDarkTheme_Click(object sender, RoutedEventArgs e) => ThemeHandler.SetDarkTheme();
		public void btnEnglish_Click(object sender, RoutedEventArgs e) => LocaleHandler.Instance.SetEnglish();
		public void btnSerbian_Click(object sender, RoutedEventArgs e) => LocaleHandler.Instance.SetSerbian();
	}
}
