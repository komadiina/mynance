﻿using mynance.src.auth;
using mynance.src.exceptions;
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
            try
            {
                AuthGate auth = new AuthGate();
                auth.RegisterUser(username, password, passwordConfirm, fullName);
            } catch (Exception ex)
            {
                if (ex is not AuthException) {
                    lblStatusText.Content = "Something went wrong - " + ex.Message;
                } 
                else {
                    lblStatusText.Content = ex.Message;
                }

                lblStatusText.Foreground = Brushes.MediumVioletRed;
                lblStatusText.Visibility = Visibility.Visible;
            
                return;
            }

            lblStatusText.Content = "Success! You can now go back and sign in.";
            lblStatusText.Foreground = Brushes.Green;
        }
    }
}
