using Godsend.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Godsend.Models
{
    public class ImagePaths
    {
        public Guid Id { get; set; }
        public string Preview { get; set; }
        public IEnumerable<StringWrapper> Images { get; set; }
    }
}
