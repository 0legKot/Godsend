namespace Godsend.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Newtonsoft.Json;

    public abstract class Product : IEntity
    {
        public Guid Id { get; set; }

        public ProductInformation Info { get; set; }

        [JsonIgnore]
        public Information EntityInformation
        {
            get => Info;
            set => Info = value as ProductInformation;
        }
    }

    public class DiscreteProduct : Product
    {
    }

    public class WeightedProduct : Product
    {
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
