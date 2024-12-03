using mynance.src.auth;
using mynance.src.navigation;
using mynance.src.navigation.pages;

using System.Windows;
using System.Windows.Controls;

namespace mynance.src.components
{
    /// <summary>
    /// Interaction logic for SideNavbar.xaml
    /// </summary>
    public partial class SideNavbar : UserControl
    {
        public SideNavbar()
        {
            InitializeComponent();
            UserFullname.Content = AuthGate.CurrentUser.FullName;
        }

        public void NavProfileSettings_Click(object sender, RoutedEventArgs e) => Navigator.Next(new ProfilePage());
        public void NavCalendar_Click(object sender, RoutedEventArgs e) => Navigator.Next(new CalendarPage());

        public void NavLogout_Click(object sender, RoutedEventArgs e)
        {
            AuthGate.Logout();
            Navigator.Reset();
        }

        private void NavDashboard_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
