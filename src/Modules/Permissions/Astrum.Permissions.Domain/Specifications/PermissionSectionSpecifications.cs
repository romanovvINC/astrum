using Ardalis.Specification;
using Astrum.Permissions.Domain.Aggregates;

namespace Astrum.Permissions.Domain.Specifications
{
    public class GetPermissionsSectionsSpec : Specification<PermissionSection> { }

    public class GetPermissionSectionById : GetPermissionsSectionsSpec
    {
        public GetPermissionSectionById(Guid id)
        {
            Query.Where(section => section.Id == id);
        }
    }

    public class GetPermissionsSectionsByAccess : GetPermissionsSectionsSpec
    {
        public GetPermissionsSectionsByAccess(bool permission)
        {
            Query.Where(section => section.Permission == permission);
        }
    }
}
