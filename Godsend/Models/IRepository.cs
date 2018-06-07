namespace Godsend.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public interface IRepository<IEntity>
    {
        IEntity GetEntity(Guid entityId);

        IEnumerable<IEntity> Entities { get; }

        void SaveEntity(IEntity entity);

        void DeleteEntity(Guid entityId);

        bool IsFirst(IEntity entity);
    }
}
