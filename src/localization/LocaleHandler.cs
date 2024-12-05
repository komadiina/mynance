using System.Globalization;
using WPFLocalizeExtension.Engine;

namespace mynance.src.localization
{
	public class LocaleHandler
	{
		private String currentLocale;
		public String CurrentLocale
		{
			get { return currentLocale; }
			set { currentLocale = value; }
		}
		public static LocalizationProvider Provider = new();
		public static LocaleHandler Instance = new();

		private LocaleHandler()
		{ }

		public void SetLocale(String cultureName)
		{
			LocalizeDictionary.Instance.SetCurrentThreadCulture = true;
			LocalizeDictionary.Instance.Culture = new CultureInfo(cultureName);
			CurrentLocale = cultureName;
		}

		public void SetEnglish()
		{
			LocalizeDictionary.Instance.SetCurrentThreadCulture = true;
			LocalizeDictionary.Instance.Culture = new CultureInfo("en-US");
			CurrentLocale = "en-US";
		}

		public void SetSerbian()
		{
			LocalizeDictionary.Instance.SetCurrentThreadCulture = true;
			LocalizeDictionary.Instance.Culture = new CultureInfo("sr-Latn-RS");
			CurrentLocale = "sr-Latn-RS";
		}
	}
}
