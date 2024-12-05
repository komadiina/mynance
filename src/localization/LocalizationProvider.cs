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

		public static String Translate(string key)
		{
			var locExtension = new LocExtension { Key = key };
			locExtension.ResolveLocalizedValue(out string uiString);
			return uiString ?? key;
		}
	}
}
