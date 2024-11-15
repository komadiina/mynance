using System.Globalization;
using WPFLocalizeExtension.Engine;

namespace mynance.src.localization
{
    public class LocaleHandler
    {
        public static void SetEnglish()
        {
            LocalizeDictionary.Instance.SetCurrentThreadCulture = true;
            LocalizeDictionary.Instance.Culture = new CultureInfo("en-US");   
        }

        public static void SetSerbian()
        {
            LocalizeDictionary.Instance.SetCurrentThreadCulture = true;
            LocalizeDictionary.Instance.Culture = new CultureInfo("sr-Latn-RS");
        }
    }
}
