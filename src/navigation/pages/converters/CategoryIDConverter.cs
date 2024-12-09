using mynance.src.localization;
using System.Globalization;
using System.Windows.Data;

namespace mynance.src.navigation.pages
{
	class CategoryIDConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			int val = (int)value;
			switch (val)
			{
				case 1: return LocalizationProvider.GetLocalizedValue<string>("CategoryBills");
				case 2: return LocalizationProvider.GetLocalizedValue<string>("CategoryGroceries");
				case 3: return LocalizationProvider.GetLocalizedValue<string>("CategorySubscriptions");
				case 4: return LocalizationProvider.GetLocalizedValue<string>("CategoryLoans");
				case 5: return LocalizationProvider.GetLocalizedValue<string>("CategoryOther");
				default: return "";
			}
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
