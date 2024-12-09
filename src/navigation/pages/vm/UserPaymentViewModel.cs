using mynance.src.localization;
using System.ComponentModel;

namespace mynance.src.navigation.pages
{
	class UserPaymentViewModel : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler? PropertyChanged;

		public string Dashboard_MakePaymentOutgoing
		{
			get { return LocalizationProvider.GetLocalizedValue<string>("Dashboard_MakePaymentOutgoing"); }
		}
		public string Dashboard_MakePaymentIncoming
		{
			get { return LocalizationProvider.GetLocalizedValue<string>("Dashboard_MakePaymentIncoming"); }
		}
		public string Dashboard_MakePaymentOutgoingButtonText
		{
			get { return LocalizationProvider.GetLocalizedValue<string>("Dashboard_MakePaymentOutgoingButtonText"); }
		}
		public string Dashboard_MakePaymentIncomingButtonText
		{
			get { return LocalizationProvider.GetLocalizedValue<string>("Dashboard_MakePaymentIncomingButtonText"); }
		}

		public UserPaymentViewModel()
		{
			LocaleHandler.Instance.LocaleChanged += (sender, args) =>
			{
				OnPropertyChanged(nameof(Dashboard_MakePaymentOutgoing));
				OnPropertyChanged(nameof(Dashboard_MakePaymentIncoming));
				OnPropertyChanged(nameof(Dashboard_MakePaymentOutgoingButtonText));
				OnPropertyChanged(nameof(Dashboard_MakePaymentIncomingButtonText));
			};
		}
		protected void OnPropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
