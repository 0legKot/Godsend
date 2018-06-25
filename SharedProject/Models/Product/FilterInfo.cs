using System;
using System.Collections.Generic;
using System.Text;

namespace Godsend.Models
{
    public class FilterInfo
    {
        public IEnumerable<DecimalPropertyInfo> DecimalProps { get; set; }

        public IEnumerable<StringPropertyInfo> StringProps { get; set; }

        public IEnumerable<IntPropertyInfo> IntProps { get; set; }
        public Guid SortingPropertyId { get; set; }
    }
}
