using mynance.src.auth;
using mynance.src.localization;
using System.ComponentModel;

namespace mynance.src.navigation.pages
{
	class ProfilePageViewModel : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler? PropertyChanged;

		public string ProfilePage_Name { get { return AuthGate.CurrentUser.Username; } private set { } }
		public String ProfilePage_RegistrationDate
		{
			get { return LocalizationProvider.GetLocalizedValue<string>("ProfilePage_RegistrationDate"); }
		}
		public string ProfilePage_RegistrationDateValue
		{
			get { return AuthGate.CurrentUser.RegistrationDate.ToString(); }
			private set { }
		}

		public string ProfilePage_UsernameValue
		{
			get { return AuthGate.CurrentUser.Username; }
			private set { }
		}

		public string ProfilePage_FullnameValue
		{
			get { return AuthGate.CurrentUser.FullName; }
			private set { }
		}

		public string ProfilePage_LastSeenValue
		{
			get { return AuthGate.CurrentUser.LastActive.ToString(); }
			private set { }
		}

		public ProfilePageViewModel()
		{
			LocaleHandler.Instance.LocaleChanged += (sender, args) =>
			{
				OnPropertyChanged(nameof(ProfilePage_RegistrationDate));
			};
		}

		protected void OnPropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
