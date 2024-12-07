using System.Reflection;
using WPFLocalizeExtension.Extensions;

namespace mynance.src.localization
{
	public class LocalizationProvider
	{
		public static T GetLocalizedValue<T>(string key)
		{
			return LocExtension.GetLocalizedValue<T>(Assembly.GetCallingAssembly().GetName().Name + ":Locale:" + key);
		}
	}
}
