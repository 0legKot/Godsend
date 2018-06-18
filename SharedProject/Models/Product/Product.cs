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

    /// <summary>
    /// 
    /// </summary>
    public abstract class Product : IEntity
    {
        //NO PALINDROMS
        /// <summary>
        /// The separatist
        /// </summary>
        const string separatist = "#/,%/#";
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the information.
        /// </summary>
        /// <value>
        /// The information.
        /// </value>
        public ProductInformation Info { get; set; }
        /// <summary>
        /// Gets or sets the characteristics list.
        /// </summary>
        /// <value>
        /// The characteristics list.
        /// </value>
        [JsonIgnore]
        public IEnumerable<StringWrapper> CharacteristicsList { get; set; } = new List<StringWrapper>();
        /// <summary>
        /// Gets the characteristics.
        /// </summary>
        /// <value>
        /// The characteristics.
        /// </value>
        [NotMapped]
        public IDictionary<string,string> Characteristics {
            get => CharacteristicsList.Select(x => x.Value.Split(separatist)).ToDictionary(x=>x.FirstOrDefault(),y=>y.LastOrDefault());
            
        }
        /// <summary>
        /// Adds the characteristic.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
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
        /// <summary>
        /// Gets or sets the entity information.
        /// </summary>
        /// <value>
        /// The entity information.
        /// </value>
        [JsonIgnore]
        public Information EntityInformation
        {
            get => Info;
            set => Info = value as ProductInformation;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class SimpleProduct : Product
    {
    }
    /// <summary>
    /// 
    /// </summary>
    public class ProductTV : Product
    {
        /// <summary>
        /// Prevents a default instance of the <see cref="ProductTV"/> class from being created.
        /// </summary>
        ProductTV() {
            //Characteristics.Keys.Add("Diagonal");
            //Characteristics.Keys.Add("Matrix");
            //Characteristics.Keys.Add("Weight");
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class ProductFruit : Product
    {
        //ProductFruit()
        //{
        //    Characteristics.Keys.Add("Vitamin A");
        //    Characteristics.Keys.Add("Vitamin B");
        //    Characteristics.Keys.Add("Vitamin C");
        //}
    }
    /// <summary>
    /// 
    /// </summary>
    public class ProductWithSuppliers
    {
        /// <summary>
        /// Gets or sets the product.
        /// </summary>
        /// <value>
        /// The product.
        /// </value>
        public Product Product { get; set; }

        /// <summary>
        /// Gets or sets the suppliers.
        /// </summary>
        /// <value>
        /// The suppliers.
        /// </value>
        public IEnumerable<SupplierAndPrice> Suppliers { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class SupplierAndPrice
    {
        /// <summary>
        /// Gets or sets the supplier.
        /// </summary>
        /// <value>
        /// The supplier.
        /// </value>
        public Supplier Supplier { get; set; }

        /// <summary>
        /// Gets or sets the price.
        /// </summary>
        /// <value>
        /// The price.
        /// </value>
        public decimal Price { get; set; }
    }
}
