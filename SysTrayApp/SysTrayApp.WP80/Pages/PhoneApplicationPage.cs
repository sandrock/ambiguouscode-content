//-----------------------------------------------------------------------
// <copyright file="PhoneApplicationPage.cs" company="ambiguouscode.net">
//     Copyright (c) 2013 ambiguouscode.net All rights reserved.
// </copyright>
// <author>SandRock</author>
// <summary></summary>
//-----------------------------------------------------------------------

namespace SysTrayApp.Pages
{
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;
    using System.Windows.Navigation;
    using Microsoft.Phone.Controls;
    using Microsoft.Phone.Shell;

    /// <summary>
    /// The app-specific <see cref="Microsoft.Phone.Controls.PhoneApplicationPage"/> control.
    /// </summary>
    public class PhoneApplicationPage : Microsoft.Phone.Controls.PhoneApplicationPage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PhoneApplicationPage"/> class.
        /// </summary>
        public PhoneApplicationPage()
        {
        }

        /// <summary>
        /// Gets a value indicating whether this instance is light theme.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is light theme; otherwise, <c>false</c>.
        /// </value>
        public bool IsLightTheme
        {
            get { return (Visibility)this.Resources["PhoneLightThemeVisibility"] == System.Windows.Visibility.Visible; }
        }

        /// <summary>
        /// Called when a page becomes the active page in a frame.
        /// </summary>
        /// <param name="e">An object that contains the event data.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            this.ApplyPageBackground();
            this.UpdateSystray();

            base.OnNavigatedTo(e);
        }

        /// <summary>
        /// Applies the page background.
        /// </summary>
        private void ApplyPageBackground()
        {
            var rootElement = (Panel)this.Content;
            if (this.IsLightTheme)
            {
                rootElement.Background = new SolidColorBrush(Color.FromArgb(0xff, 0xcc, 0xcc, 0xcc));
            }
            else
            {
                rootElement.Background = new SolidColorBrush(Color.FromArgb(0xff, 0x33, 0x33, 0x33));
            }
        }

        /// <summary>
        /// Called after the Orientation property has been changed.
        /// </summary>
        /// <param name="e">The event arguments.</param>
        protected override void OnOrientationChanged(OrientationChangedEventArgs e)
        {
            base.OnOrientationChanged(e);

            this.UpdateSystray();
        }

        /// <summary>
        /// Refreshes the System Tray display.
        /// </summary>
        protected virtual void UpdateSystray()
        {
            //
            // define systray preferences here
            //
            // use the application resources to apply ligh/dark theme values
            // set Opacity to 1 and the device will remove space from the app for the tray
            // set Opacity to 0-0.99999 and the device will show the tray on top of your app
            Color systrayForeground = (Color)this.Resources["PhoneForegroundColor"];
            Color systrayBackground = (Color)this.Resources["PhoneBackgroundColor"];
            double systrayOpacity = 1D;
            ////double systrayOpacity = .99999;
            ////double systrayOpacity = .5;
            ////double systrayOpacity = 0D;

            this.ConfigureSystray(systrayForeground, systrayBackground, systrayOpacity);
        }

        /// <summary>
        /// Configures the system tray.
        /// </summary>
        /// <param name="systrayForeground">The system tray foreground.</param>
        /// <param name="systrayBackground">The system tray background.</param>
        /// <param name="systrayOpacity">The system tray opacity.</param>
        protected void ConfigureSystray(Color systrayForeground, Color systrayBackground, double systrayOpacity)
        {
            // 
            // apply style if not done already
            SystemTray.ForegroundColor = systrayForeground;
            SystemTray.BackgroundColor = systrayBackground;
            SystemTray.Opacity = systrayOpacity;

            // 
            // adapt to orientation
            switch (this.Orientation)
            {
                case PageOrientation.Portrait:
                case PageOrientation.PortraitDown:
                case PageOrientation.PortraitUp:
                    // the systray is appreciated by users in portrait
                    // you may want to show it
                    if (!SystemTray.IsVisible)
                    {
                        SystemTray.IsVisible = true;
                    }

                    break;

                case PageOrientation.Landscape:
                case PageOrientation.LandscapeLeft:
                case PageOrientation.LandscapeRight:
                    // the systray is vertical in landscape
                    // you may want to hide it
                    if (SystemTray.IsVisible)
                    {
                        SystemTray.IsVisible = false;
                    }

                    break;
            }
        }
    }
}
