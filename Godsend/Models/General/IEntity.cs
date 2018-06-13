namespace Godsend.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public abstract class IEntity
    {
        public Guid Id { get; set; }
        public Information EntityInformation { get; set; }
        public virtual void SetIds() {
            Id = Guid.NewGuid();
            EntityInformation.Id = Guid.NewGuid();
        }
    }
}
