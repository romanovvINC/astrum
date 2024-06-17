using Astrum.Permissions.Application.Models.CreateModels;
using Astrum.Permissions.Application.Models.UpdateModels;
using Astrum.Permissions.Application.Models.ViewModels;
using Astrum.SharedLib.Common.Results;

namespace Astrum.Permissions.Application.Services
{
    public interface IPermissionSectionService
    {
        Task<Result<List<PermissionSectionView>>> GetPermissionsSections(CancellationToken cancellationToken = default);
        Task<Result<List<PermissionSectionView>>> GetPermissionsSectionsByAccess(bool permission, CancellationToken cancellationToken = default);
        Task<Result<PermissionSectionView>> GetPermissionSectionById(Guid id, CancellationToken cancellationToken = default);
        Task<Result<PermissionSectionView>> CreatePermissionSection(PermissionSectionCreateRequest permissionSectionCreateRequest, CancellationToken cancellationToken = default);
        Task<Result<PermissionSectionView>> UpdatePermissionSection(Guid id, PermissionSectionUpdateRequest permissionSectionUpdateRequest,CancellationToken cancellationToken = default);
        Task<Result<PermissionSectionView>> DeletePermissionSection(Guid id, CancellationToken cancellationToken = default);

    }
}
