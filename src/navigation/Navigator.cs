﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace mynance.src.navigation
{
    public class Navigator
    {
        private static readonly Stack<Page> pages = new();

        // Implemented but will not be used.
        public static void Next(Page page)
        {
            pages.Push(page);
            App.Current.MainWindow.Content = page;
        }

        public static void Previous()
        {
            if (pages.Count > 0)
            {
                pages.Pop();
                App.Current.MainWindow.Content = pages.Peek();

            }
        }
    }
}
