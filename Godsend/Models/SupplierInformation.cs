namespace Godsend.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class SupplierInformation : Information
    {
        public Location Location { get; set; }

        // public IEnumerable<IProduct> Products { get; set; }
    }
}
