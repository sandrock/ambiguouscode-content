//-----------------------------------------------------------------------
// <copyright file="ColorModel.cs" company="ambiguouscode.net">
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
    using System.Windows.Media;

    /// <summary>
    /// Represents a user-selectable color.
    /// </summary>
    public class ColorModel : NotifyPropertyChanged
    {
        /// <summary>
        /// Contains a value indicating whether the element is selected.
        /// Use the property <see cref="IsSelected"/> instead.
        /// </summary>
        private bool isSelected;

        /// <summary>
        /// Initializes a new instance of the <see cref="ColorModel"/> class.
        /// </summary>
        public ColorModel()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ColorModel"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="color">The color.</param>
        public ColorModel(string name, Color color)
        {
            this.Name = name;
            this.Color = color;
        }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the color.
        /// </summary>
        /// <value>
        /// The color.
        /// </value>
        public Color Color { get; set; }

        /// <summary>
        /// Gets the brush.
        /// </summary>
        /// <value>
        /// The brush.
        /// </value>
        public SolidColorBrush Brush
        {
            get { return new SolidColorBrush(this.Color); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the element is selected.
        /// </summary>
        public bool IsSelected
        {
            get { return this.isSelected; }
            set { this.SetValue(ref this.isSelected, value, "IsSelected"); }
        }
    }
}
