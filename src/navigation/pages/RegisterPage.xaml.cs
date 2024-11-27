using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace mynance.src.navigation.pages
{
    /// <summary>
    /// Interaction logic for RegisterPage.xaml
    /// </summary>
    public partial class RegisterPage : Page
    {
        public RegisterPage()
        {
            InitializeComponent();
        }

        private void BtnRegister_Click(object sender, RoutedEventArgs e) 
        {
            Register(tbUsername.Text, pbPassword.Password, PbPasswordConfirm.Password, tbFullname.Text);
        }

        private void Register_KeyDownEnter(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Enter) return;
            Register(tbUsername.Text, pbPassword.Password, PbPasswordConfirm.Password, tbFullname.Text);
        }

        private void Register(String username, String password, String passwordConfirm, String fullName)
        {
            Trace.WriteLine(username + ":" + password + ":" + passwordConfirm + ":" + fullName);
            lblStatusText.Content = "test";
            lblStatusText.Visibility = Visibility.Visible;
        }
    }
}
