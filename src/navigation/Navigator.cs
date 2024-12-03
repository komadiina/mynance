using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using mynance.src.auth;
using mynance.src.navigation.pages;

namespace mynance.src.navigation
{
    public class Navigator
    {
        private static Stack<Page> pages = new();

        // Implemented but will not be used.
        public static void Next(Page page)
        {
            pages.Push(page);
            App.Current.MainWindow.Content = page;
        }

        public static void Previous()
        {
            // Prevent accidentally navigating back to the login page, requires manually signing out
            if (AuthGate.CurrentUser != null && pages.ElementAt(pages.Count() - 1) is LoginPage) { return; }

            try {
                if (pages.Count > 1) {
                    // todo: leak?
                    pages.Pop();
                    App.Current.MainWindow.Content = pages.Peek();
                }
            } catch (InvalidOperationException ex)
            {
                Trace.WriteLine("Unable to navigate back.");
            }
        }

        public static void Reset()
        {
            pages.Clear();
            Next(new LoginPage());
        }
    }
}
