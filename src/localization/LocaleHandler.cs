using System.Globalization;
using WPFLocalizeExtension.Engine;

namespace mynance.src.localization
{
	public class LocaleHandler
	{
		public String CurrentLocale;

		public static LocalizationProvider Provider = new();
		public static LocaleHandler Instance = new();

		public event EventHandler LocaleChanged;

		private LocaleHandler()
		{ }

		public void SetLocale(String cultureName)
		{
			LocalizeDictionary.Instance.SetCurrentThreadCulture = true;
			LocalizeDictionary.Instance.Culture = new CultureInfo(cultureName);
			CurrentLocale = cultureName;
			OnLocaleChanged(EventArgs.Empty);
		}

		public void SetEnglish()
		{
			LocalizeDictionary.Instance.SetCurrentThreadCulture = true;
			LocalizeDictionary.Instance.Culture = new CultureInfo("en-US");
			CurrentLocale = "en-US";
			OnLocaleChanged(EventArgs.Empty);
		}

		public void SetSerbian()
		{
			LocalizeDictionary.Instance.SetCurrentThreadCulture = true;
			LocalizeDictionary.Instance.Culture = new CultureInfo("sr-Latn-RS");
			CurrentLocale = "sr-Latn-RS";
			OnLocaleChanged(EventArgs.Empty);
		}

		protected virtual void OnLocaleChanged(EventArgs e)
		{
			LocaleChanged?.Invoke(this, e);
		}
	}
}
