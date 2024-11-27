using mynance.src.models;
using mynance.src.navigation;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace mynance
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            EventManager.RegisterClassHandler(
                typeof(Window),
                Window.MouseDownEvent,
                new MouseButtonEventHandler(NavBack_MouseDown)
                );
            base.OnStartup(e);
        }

        private void NavBack_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.XButton1) Navigator.Previous();
            Trace.WriteLine(e.ChangedButton);
        }
    }
}
