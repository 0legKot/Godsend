// <copyright file="ProductInformation.cs" company="Godsend Team">
// Copyright (c) Godsend Team. All rights reserved.
// </copyright>

namespace Godsend.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Threading.Tasks;
    using Newtonsoft.Json;

    /// <summary>
    ///
    /// </summary>
    public class ProductInformation : Information // , INotifyPropertyChanged
    {
        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; set; }

        ////[NotMapped]
        ////private string title;

        ////public event PropertyChangedEventHandler PropertyChanged;

        ////[JsonProperty("title")]
        ////[NotMapped]
        ////public string Title
        ////{
        ////    get => title;
        ////    set
        ////    {
        ////        title = value;
        ////        //OnPropertyChanged();
        ////    }
        ////}

        ////protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        ////{
        ////    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        ////}
    }
}
