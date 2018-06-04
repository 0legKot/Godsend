namespace Godsend.Models
{
    using System;

    public class Location
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string Address { get; set; }
    }
}
