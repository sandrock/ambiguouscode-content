//-----------------------------------------------------------------------
// <copyright file="SpecialPage.xaml.cs" company="ambiguouscode.net">
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
    using System.Windows.Media;
    using System.Windows.Navigation;
    using Microsoft.Phone.Controls;
    using Microsoft.Phone.Shell;

    /// <summary>
    /// Page showing special usage of SystemTray with <see cref="SysTrayApp.Pages.PhoneApplicationPage"/>.
    /// </summary>
    public partial class SpecialPage : PhoneApplicationPage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SpecialPage"/> class.
        /// </summary>
        public SpecialPage()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Refreshes the System Tray display.
        /// </summary>
        protected override void UpdateSystray()
        {
            Color systrayForeground = (Color)this.Resources["PhoneAccentColor"];
            Color systrayBackground = (Color)this.Resources["PhoneBackgroundColor"];
            double systrayOpacity = 1D;

            this.ConfigureSystray(systrayForeground, systrayBackground, systrayOpacity);
        }
    }
}