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

        public OrderBy OrderBy { get; set; } = OrderBy.Rating;
    }

    public class ProductFilterInfo
    {
        public IEnumerable<DecimalPropertyInfo> DecimalProps { get; set; }

        public IEnumerable<StringPropertyInfo> StringProps { get; set; }

        public IEnumerable<IntPropertyInfo> IntProps { get; set; }

        public Guid? SortingPropertyId { get; set; }

        public OrderBy OrderBy { get; set; } = OrderBy.Rating;

        public bool SortAscending { get; set; } = false;

        public Guid? CategoryId { get; set; }

        public string SearchTerm { get; set; }

        public int Quantity { get; set; }

        public int Page { get; set; }

        public int Skip => Quantity * (Page - 1);
    }
}
