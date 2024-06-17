using Astrum.Permissions.Domain.Aggregates;
using Astrum.SharedLib.Application.Contracts.Persistence.Repositories;

namespace Astrum.Permissions.DomainServices.Repositories
{
    public interface IPermissionSectionsRepository : IEntityRepository<PermissionSection, Guid> { }
}
