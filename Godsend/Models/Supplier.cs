namespace Godsend.Models
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public abstract class Supplier: IEntity
    {
        public Guid Id { get; set; }

        public SupplierInformation Info { get; set; }

        [JsonIgnore]
        public Information EntityInformation { get => Info; set { Info = value as SupplierInformation; } }
    }

    public class SimpleSupplier : Supplier
    {
    }
}
