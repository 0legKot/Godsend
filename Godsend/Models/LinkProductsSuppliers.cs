namespace Godsend.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class LinkProductsSuppliers
    {
        public Guid Id { get; set; }

        public Guid SupplierId { get; set; }

        public Supplier Supplier { get; set; }

        public Guid ProductId { get; set; }

        public Product Product { get; set; }

        public decimal Price { get; set; }

        // left in stock (gg)
    }
}
