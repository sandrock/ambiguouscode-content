//-----------------------------------------------------------------------
// <copyright file="MainPage.xaml.cs" company="ambiguouscode.net">
//     Copyright (c) 2013 ambiguouscode.net All rights reserved.
// </copyright>
// <author>SandRock</author>
// <summary></summary>
//-----------------------------------------------------------------------

namespace SysTrayApp.Pages
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using System.Windows.Media;
    using Microsoft.Phone.Controls;
    using Microsoft.Phone.Tasks;

    /// <summary>
    /// The start page.
    /// </summary>
    public partial class MainPage : PhoneApplicationPage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MainPage"/> class.
        /// </summary>
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button != null && button.Tag != null)
            {
                var task = new WebBrowserTask
                {
                    Uri = new Uri((string)button.Tag, UriKind.Absolute),
                };
                task.Show();
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button != null && button.Tag != null)
            {
                this.NavigationService.Navigate(new Uri((string)button.Tag, UriKind.Relative));
            }
        }
    }
}