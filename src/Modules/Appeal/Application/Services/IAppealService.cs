using Astrum.Appeal.ViewModels;
using Astrum.SharedLib.Common.Results;
using Sakura.AspNetCore;

namespace Astrum.Appeal.Services;

public interface IAppealService
{
    Task<SharedLib.Common.Results.Result<IPagedList<AppealFormResponse>>> GetAppeals(int page, int pageSize, CancellationToken cancellationToken = default);
    Task<SharedLib.Common.Results.Result<AppealFormResponse>> GetAppealById(Guid id, CancellationToken cancellationToken = default);
    Task<SharedLib.Common.Results.Result<AppealFormResponse>> UpdateAppeal(AppealForm appealForm, CancellationToken cancellationToken = default);
    Task<SharedLib.Common.Results.Result<AppealFormResponse>> CreateAppeal(AppealFormData appealForm, CancellationToken cancellationToken = default);
    Task<SharedLib.Common.Results.Result<AppealFormResponse>> DeleteAppeal(Guid id, CancellationToken cancellationToken = default);
}