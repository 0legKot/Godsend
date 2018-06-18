// <copyright file="Product.cs" company="Godsend Team">
// Copyright (c) Godsend Team. All rights reserved.
// </copyright>

namespace Godsend.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Threading.Tasks;
    using Newtonsoft.Json;

    public abstract class Product : IEntity
    {
        //NO PALINDROMS
        const string separatist = "#/,%/#";
        public Guid Id { get; set; }

        public ProductInformation Info { get; set; }
        [JsonIgnore]
        public IEnumerable<StringWrapper> CharacteristicsList { get; set; } = new List<StringWrapper>();
        [NotMapped]
        public IDictionary<string,string> Characteristics {
            get => CharacteristicsList.Select(x => x.Value.Split(separatist)).ToDictionary(x=>x.FirstOrDefault(),y=>y.LastOrDefault());
            
        }
        public void AddCharacteristic(string name, string value) {
            name = name ?? "";
            value = value ?? "";
            name = name.Replace(separatist,"");
            value = value.Replace(separatist, "");
            List<StringWrapper> lst = new List<StringWrapper>();
            lst.Add(name + separatist + value);
            foreach (var item in CharacteristicsList)
                if (!item.Value.StartsWith(name))
                    lst.Add(item);
            CharacteristicsList = lst;
        }
        [JsonIgnore]
        public Information EntityInformation
        {
            get => Info;
            set => Info = value as ProductInformation;
        }
    }

    public class SimpleProduct : Product
    {
    }
    public class ProductTV : Product
    {
        ProductTV() {
            //Characteristics.Keys.Add("Diagonal");
            //Characteristics.Keys.Add("Matrix");
            //Characteristics.Keys.Add("Weight");
        }
    }

    public class ProductFruit : Product
    {
        //ProductFruit()
        //{
        //    Characteristics.Keys.Add("Vitamin A");
        //    Characteristics.Keys.Add("Vitamin B");
        //    Characteristics.Keys.Add("Vitamin C");
        //}
    }
    public class ProductWithSuppliers
    {
        public Product Product { get; set; }

        public IEnumerable<SupplierAndPrice> Suppliers { get; set; }
    }

    public class SupplierAndPrice
    {
        public Supplier Supplier { get; set; }

        public decimal Price { get; set; }
    }
}
