//-----------------------------------------------------------------------
// <copyright file="OtherPage.xaml.cs" company="ambiguouscode.net">
//     Copyright (c) 2013 ambiguouscode.net All rights reserved.
// </copyright>
// <author>SandRock</author>
// <summary></summary>
//-----------------------------------------------------------------------

namespace SysTrayApp.Pages
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Linq;
    using System.Net;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;
    using System.Windows.Navigation;
    using Microsoft.Phone.Controls;
    using Microsoft.Phone.Shell;
    using SysTrayApp.Models;

    /// <summary>
    /// Page that allows the user to customize the system tray.
    /// </summary>
    public partial class OtherPage : PhoneApplicationPage, INotifyPropertyChanged
    {
        /// <summary>
        /// Gets the available colors.
        /// </summary>
        private readonly List<ColorModel> colors = new List<ColorModel>();

        /// <summary>
        /// Contains the selected foreground color.
        /// Use the property <see cref="SelectedForeground"/> instead.
        /// </summary>
        private ColorModel selectedForeground;

        /// <summary>
        /// Contains the selected background color.
        /// Use the property <see cref="SelectedBackground"/> instead.
        /// </summary>
        private ColorModel selectedBackground;

        /// <summary>
        /// The latest non-equales-to-1 opacity
        /// </summary>
        private double latestNonOneOpacity;

        private ObservableCollection<ColorModel> availableForegroundColors;

        private ObservableCollection<ColorModel> availableBackgroundColors;

        /// <summary>
        /// Initializes a new instance of the <see cref="OtherPage"/> class.
        /// </summary>
        public OtherPage()
        {
            this.InitializeComponent();

            this.colors.Add(new ColorModel("Black", Colors.Black));
            this.colors.Add(new ColorModel("White", Colors.White));
            this.colors.Add(new ColorModel("Transparent", Colors.Transparent));
            this.colors.Add(new ColorModel("Accent", (Color)this.Resources["PhoneAccentColor"]));
            this.colors.Add(new ColorModel("Red", Colors.Red));
            this.colors.Add(new ColorModel("Green", Colors.Green));
            this.colors.Add(new ColorModel("Blue", Colors.Blue));
            this.colors.Add(new ColorModel("LightGray", Colors.LightGray));
            this.colors.Add(new ColorModel("Gray", Colors.Gray));
            this.colors.Add(new ColorModel("DarkGray", Colors.DarkGray));
            this.availableBackgroundColors = new ObservableCollection<ColorModel>(this.colors.Select(c => new ColorModel(c.Name, c.Color)));
            this.availableForegroundColors = new ObservableCollection<ColorModel>(this.colors.Select(c => new ColorModel(c.Name, c.Color)));

            this.DataContext = this;

            if (SystemTray.Opacity != 1D)
            {
                latestNonOneOpacity = SystemTray.Opacity;
            }
        }

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Contains the available colors.
        /// Use the property <see cref="Colors"/> instead.
        /// </summary>
        public ObservableCollection<ColorModel> AvailableForegroundColors
        {
            get { return this.availableForegroundColors; }
        }

        /// <summary>
        /// Contains the available colors.
        /// Use the property <see cref="Colors"/> instead.
        /// </summary>
        public ObservableCollection<ColorModel> AvailableBackgroundColors
        {
            get { return this.availableBackgroundColors; }
        }

        /// <summary>
        /// Gets or sets the selected foreground color.
        /// </summary>
        public ColorModel SelectedForeground
        {
            get { return this.selectedForeground; }
            set
            {
                this.SetValue(ref this.selectedForeground, value, "SelectedForeground");
                foreach (var item in this.AvailableForegroundColors)
                {
                    item.IsSelected = value != null && item.Name == value.Name;
                }

                if (value != null)
                {
                    SystemTray.ForegroundColor = value.Color;
                }
            }
        }

        /// <summary>
        /// Gets or sets the selected background color.
        /// </summary>
        public ColorModel SelectedBackground
        {
            get { return this.selectedBackground; }
            set
            {
                this.SetValue(ref this.selectedBackground, value, "SelectedBackground");
                foreach (var item in this.AvailableBackgroundColors)
                {
                    item.IsSelected = value != null && item.Name == value.Name;
                }

                if (value != null)
                {
                    SystemTray.BackgroundColor = value.Color;
                }
            }
        }

        /// <summary>
        /// Called when a page becomes the active page in a frame.
        /// </summary>
        /// <param name="e">An object that contains the event data.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            //
            // configure UI
            this.OpacitySlider.Value = SystemTray.Opacity;
            this.OpacityCheckBox.IsChecked = this.OpacitySlider.Value == 1D;
            this.VisibilityCheckBox.IsChecked = SystemTray.IsVisible;

            foreach (var item in this.AvailableForegroundColors)
            {
                if (item.Color == SystemTray.ForegroundColor)
                {
                    this.ForegroundSelector.SelectedItem = item;
                    break;
                }
            }

            foreach (var item in this.AvailableBackgroundColors)
            {
                if (item.Color == SystemTray.BackgroundColor)
                {
                    this.BackgroundSelector.SelectedItem = item;
                    break;
                }
            }

            //
            // subscribe to control events
            this.OpacityCheckBox.Checked += this.OnCheckBoxCheckedChanged;
            this.OpacityCheckBox.Unchecked += this.OnCheckBoxCheckedChanged;
            this.OpacitySlider.ValueChanged += this.OnOpacitySliderValueChanged;
            this.ForegroundSelector.SelectionChanged += this.OnForegroundSelectorSelectionChanged;
            this.BackgroundSelector.SelectionChanged += this.OnBackgroundSelectorSelectionChanged;
            this.VisibilityCheckBox.Checked += this.OnVisibilityCheckBoxCheckedChanged;
            this.VisibilityCheckBox.Unchecked += this.OnVisibilityCheckBoxCheckedChanged;
        }

        /// <summary>
        /// Called when a page is no longer the active page in a frame.
        /// </summary>
        /// <param name="e">An object that contains the event data.</param>
        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);

            // subscribe to control events
            this.OpacityCheckBox.Checked -= this.OnCheckBoxCheckedChanged;
            this.OpacityCheckBox.Unchecked -= this.OnCheckBoxCheckedChanged;
            this.OpacitySlider.ValueChanged -= this.OnOpacitySliderValueChanged;
            this.ForegroundSelector.SelectionChanged -= this.OnForegroundSelectorSelectionChanged;
            this.BackgroundSelector.SelectionChanged -= this.OnBackgroundSelectorSelectionChanged;
            this.VisibilityCheckBox.Checked -= this.OnVisibilityCheckBoxCheckedChanged;
            this.VisibilityCheckBox.Unchecked -= this.OnVisibilityCheckBoxCheckedChanged;
        }

        /// <summary>
        /// Called when [check box checked changed].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void OnCheckBoxCheckedChanged(object sender, RoutedEventArgs e)
        {
            if (this.OpacityCheckBox.IsFocused)
            {
                if (this.OpacityCheckBox.IsChecked == true)
                {
                    if (SystemTray.Opacity != 1D)
                    {
                        this.latestNonOneOpacity = SystemTray.Opacity;
                    }

                    SystemTray.Opacity = 1;
                }
                else
                {
                    SystemTray.Opacity = this.latestNonOneOpacity;
                }

                this.OpacitySlider.Value = SystemTray.Opacity;
            }
        }

        /// <summary>
        /// Called when [opacity slider value changed].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        private void OnOpacitySliderValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            SystemTray.Opacity = this.OpacitySlider.Value;
            this.OpacityCheckBox.IsChecked = this.OpacitySlider.Value == 1D;

            if (SystemTray.Opacity != 1D)
            {
                this.latestNonOneOpacity = SystemTray.Opacity;
            }
        }

        /// <summary>
        /// Called when [background selector selection changed].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="SelectionChangedEventArgs"/> instance containing the event data.</param>
        private void OnBackgroundSelectorSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.SelectedBackground = this.BackgroundSelector.SelectedItem as ColorModel;
        }

        /// <summary>
        /// Called when [foreground selector selection changed].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="SelectionChangedEventArgs"/> instance containing the event data.</param>
        private void OnForegroundSelectorSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.SelectedForeground = this.ForegroundSelector.SelectedItem as ColorModel;
        }

        /// <summary>
        /// Called when [visibility check box checked changed].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void OnVisibilityCheckBoxCheckedChanged(object sender, RoutedEventArgs e)
        {
            SystemTray.IsVisible = this.VisibilityCheckBox.IsChecked == true;
        }

        #region INotifyPropertyChanged stuff

        /// <summary>
        /// Raises the property changed event.
        /// </summary>
        /// <param name="propertyName">Name of the property changed.</param>
        protected void RaisePropertyChanged(string propertyName)
        {
            var handler = this.PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Changes a property's value and notifies the view.
        /// </summary>
        /// <typeparam name="T">the property type</typeparam>
        /// <param name="property">a reference to a field</param>
        /// <param name="value">the new value</param>
        /// <param name="propertyName">the public property name for change notification</param>
        /// <returns>
        /// returns true if the new value is different from the old one
        /// </returns>
        protected bool SetValue<T>(ref T property, T value, string propertyName)
        {
            if (object.Equals(property, value))
            {
                return false;
            }

            property = value;
            this.RaisePropertyChanged(propertyName);
            return true;
        }

        #endregion
    }
}