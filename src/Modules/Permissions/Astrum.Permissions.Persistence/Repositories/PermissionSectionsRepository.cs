using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ardalis.Specification;
using Astrum.Permissions.Domain.Aggregates;
using Astrum.Permissions.DomainServices.Repositories;
using Astrum.SharedLib.Persistence.Repositories;

namespace Astrum.Permissions.Persistence.Repositories
{
    public class PermissionSectionsRepository : EFRepository<PermissionSection, Guid, PermissionsDbContext>, IPermissionSectionsRepository
    {
        public PermissionSectionsRepository(PermissionsDbContext context, ISpecificationEvaluator? specificationEvaluator = null) : base(context, specificationEvaluator)
        {
        }
    }
}
