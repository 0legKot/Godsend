using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Godsend.Models
{
    public abstract class Product
    {
        public Guid Id { get; }
        public ProductInformation Info { get; set;}
    }

    public class DiscreteProduct : Product
    {

    }

    public class WeightedProduct : Product
    {

    }
}
