using mynance.src.navigation;
using System.Windows;
using System.Windows.Controls;

namespace mynance
{
    /// <summary>
    /// Interaction logic for NavBackButton.xaml
    /// </summary>
    public partial class NavBackButton : UserControl
    {
        public NavBackButton()
        {
            InitializeComponent();
        }

        private void btnNavigateBackwardClick(Object sender, RoutedEventArgs e)
        {
            Navigator.Previous();
        }
    }
}
