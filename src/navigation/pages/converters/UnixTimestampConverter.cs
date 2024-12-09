using System.Globalization;
using System.Windows.Data;

namespace mynance.src.navigation.pages
{
	public class UnixTimestampConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			long timestamp = (long)value;
			DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeMilliseconds(timestamp);
			return dateTimeOffset.ToString("dd/MM/yyyy HH:mm:ss");
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}