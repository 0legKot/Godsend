namespace Godsend.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public interface IEntity
    {
        Information EntityInformation { get; set; }
    }
}
