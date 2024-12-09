using mynance.src.localization;
using mynance.src.models.db;
using mynance.src.models.db.contexts;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace mynance.src.navigation.pages
{
	class HistoryPageViewModel : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler? PropertyChanged;
		public ObservableCollection<Payment> Payments { get; set; }

		public string Payments_Title
		{
			get { return LocalizationProvider.GetLocalizedValue<string>("Payments_Title"); }
		}

		public HistoryPageViewModel()
		{
			Payments = new();
			PaymentsContext ctx = new();

			foreach (var payment in ctx.Payments.AsQueryable())
			{
				Payments.Add(payment);
			}
		}

		protected void OnPropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
