namespace Godsend.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public abstract class Product
    {
        public Guid Id { get; set; }

        public ProductInformation Info { get; set;}
    }

    public class DiscreteProduct : Product
    {
    }

    public class WeightedProduct : Product
    {
    }
}
