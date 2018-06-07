namespace Godsend.Models
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public abstract class Product :IEntity
    {
        public Guid Id { get; set; }

        public ProductInformation Info { get; set; }

        [JsonIgnore]
        public Information EntityInformation { get => Info; set { Info = value as ProductInformation; } }
    }

    public class DiscreteProduct : Product
    {
    }

    public class WeightedProduct : Product
    {
    }
}
