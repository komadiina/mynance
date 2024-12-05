using mynance.src.mvvm;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Input;

namespace mynance.src.localization
{
	public class LocalizedString : INotifyPropertyChanged
	{
		private string key, value;

		public String Value
		{
			get { return value; }
			set { this.value = value; OnPropertyChanged(nameof(Value)); }
		}

		public String Key
		{
			get { return key; }
			set { key = value; UpdateValue(); }
		}
		public ICommand LocaleChangeCommand { get; set; }

		public event PropertyChangedEventHandler? PropertyChanged;

		public LocalizedString(String key)
		{
			Key = key;
			Value = LocalizationProvider.GetLocalizedValue<String>(key);
			LocaleChangeCommand = new RelayCommand(UpdateValue);
		}

		private void UpdateValue()
		{
			Value = LocalizationProvider.GetLocalizedValue<String>(Key);
			Trace.WriteLine("updated locale: " + Value);
		}

		protected void OnPropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
