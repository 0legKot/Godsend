﻿// <copyright file="ProductInformation.cs" company="Godsend Team">
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

    public class ProductInformation : Information, INotifyPropertyChanged
    {
        [NotMapped]
        private string title;

        public event PropertyChangedEventHandler PropertyChanged;

        public string Description { get; set; }

        [JsonProperty("title")]
        [NotMapped]
        public string Title
        {
            get => title;
            set
            {
                title = value;
                OnPropertyChanged();
            }
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        // public IEnumerable<ISupplier> Suppliers { get; set; }
    }
}