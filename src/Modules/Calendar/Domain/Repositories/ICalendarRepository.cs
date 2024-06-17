using Astrum.SharedLib.Application.Contracts.Persistence.Repositories;
using Astrum.SharedLib.Domain.Interfaces;

namespace Astrum.Calendar.Domain.Repositories
{
    public interface ICalendarRepository<TEntity> : IEntityRepository<TEntity, Guid>
        where TEntity : class, IEntity<Guid>
    {
    }
}
