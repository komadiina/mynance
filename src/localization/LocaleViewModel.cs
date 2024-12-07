using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace mynance.src.localization
{
	public partial class LocaleViewModel : ObservableObject
	{
		[ObservableProperty]
		public String locale;

		[RelayCommand]
		public void CycleLocale()
		{
			switch (locale)
			{
				case "en-US":
					LocaleHandler.Instance.SetSerbian();
					break;
				case "sr-Latn-RS":
					LocaleHandler.Instance.SetEnglish();
					break;
			}
		}
	}
}
