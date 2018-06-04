using System;

namespace Godsend.Models
{
    public class Location
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string Address { get; set; }
    }
}