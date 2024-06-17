using Astrum.SharedLib.Domain.Entities;

namespace Astrum.Permissions.Domain.Aggregates
{
    public class PermissionSection : AggregateRootBase<Guid>
    {
        public string TitleSection { get; set; }
        public bool Permission { get; set; }
    }
}
