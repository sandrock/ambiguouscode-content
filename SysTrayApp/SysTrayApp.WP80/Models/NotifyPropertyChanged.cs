//-----------------------------------------------------------------------
// <copyright file="NotifyPropertyChanged.cs" company="ambiguouscode.net">
//     Copyright (c) 2013 ambiguouscode.net All rights reserved.
// </copyright>
// <author>SandRock</author>
// <summary></summary>
//-----------------------------------------------------------------------

namespace SysTrayApp.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Basic implementation of <see cref="INotifyPropertyChanged"/>.
    /// </summary>
    public class NotifyPropertyChanged : INotifyPropertyChanged
    {
        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Raises the property changed event.
        /// </summary>
        /// <param name="propertyName">Name of the property changed.</param>
        protected void RaisePropertyChanged(string propertyName)
        {
            var handler = this.PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
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
    }
}
