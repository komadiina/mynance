using AdonisUI;
using System.Windows;
using System.Windows.Media;

namespace mynance.src.styles
{
	public class ThemeHandler
	{
		public static bool IsDarkMode;

		public static void SetTheme(bool darkMode)
		{
			if (darkMode) { SetDarkTheme(); }
			else { SetLightTheme(); }
		}

		public static void SetDarkTheme()
		{
			var rd = new ResourceDictionary();

			rd.Add("TextColor", (SolidColorBrush)new BrushConverter().ConvertFrom("#fefefe"));
			//rd.Add("AccentColor", (SolidColorBrush)new BrushConverter().ConvertFrom("#ffc300"));
			//rd.Add("BackgroundColor", (SolidColorBrush)new BrushConverter().ConvertFrom("#000814"));
			//rd.Add("ElevatedColor", (SolidColorBrush)new BrushConverter().ConvertFrom("#001d3d"));
			//rd.Add("ElevatedHighColor", (SolidColorBrush)new BrushConverter().ConvertFrom("#003566"));

			Application.Current.Resources.MergedDictionaries.Add(rd);

			AdonisUI.ResourceLocator.SetColorScheme(Application.Current.Resources, ResourceLocator.DarkColorScheme);
			IsDarkMode = true;
		}

		public static void SetLightTheme()
		{
			var rd = new ResourceDictionary();

			rd.Add("TextColor", (SolidColorBrush)new BrushConverter().ConvertFrom("#121212"));
			//rd.Add("AccentColor", (SolidColorBrush)new BrushConverter().ConvertFrom("#ffc300"));
			//rd.Add("BackgroundColor", (SolidColorBrush)new BrushConverter().ConvertFrom("#ffd60a"));

			//rd.Add("ElevatedColor", (SolidColorBrush)new BrushConverter().ConvertFrom("#003566"));
			//rd.Add("ElevatedHighColor", (SolidColorBrush)new BrushConverter().ConvertFrom("#0f4576"));

			Application.Current.Resources.MergedDictionaries.Add(rd);
			AdonisUI.ResourceLocator.SetColorScheme(Application.Current.Resources, ResourceLocator.LightColorScheme);
			IsDarkMode = false;
		}
	}
}
