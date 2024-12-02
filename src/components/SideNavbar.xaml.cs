using mynance.src.auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
    }
}
