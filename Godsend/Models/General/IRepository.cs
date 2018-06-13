namespace Godsend.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public interface IRepository<IEntity>
    {
        IEnumerable<IEntity> Entities { get; }

        IEnumerable<Information> EntitiesInfo { get; }

        IEntity GetEntity(Guid entityId);

        void SaveEntity(IEntity entity);

        void DeleteEntity(Guid entityId);

        bool IsFirst(IEntity entity);

        void Watch(IEntity entity);
    }
}
