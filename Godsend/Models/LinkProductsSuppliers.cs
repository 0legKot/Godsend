﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Godsend.Models
{
    public class LinkProductsSuppliers
    {
        public Supplier Supplier { get; set; }
        public Product Product { get; set; }
        public decimal Price { get; set; }
        // left in stock (gg)
    }
}