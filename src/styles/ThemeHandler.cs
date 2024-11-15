using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows;
using AdonisUI;

namespace mynance.src.styles
{
    public class ThemeHandler
    {
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
        }
    }
}
