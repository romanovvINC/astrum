using Astrum.Appeal.Application.Services;
using Astrum.Appeal.Services;
using Astrum.Appeal.ViewModels;
using HotChocolate;
using HotChocolate.Types;
using Sakura.AspNetCore;

namespace Astrum.Appeal.GraphQL;

public class QueryAppeal
{
    [UsePaging(MaxPageSize = 20, IncludeTotalCount = true)]
    [UseSorting]
    [UseFiltering]
    public async Task<IPagedList<AppealFormResponse>> GetAppeals([Service] IAppealService appealService,
        CancellationToken cancellationToken)
    {
        var appeals = await appealService.GetAppeals(1, 100);
        return appeals.Data;
    }

    [UseSorting]
    [UseFiltering]
    public async Task<List<AppealCategoryForm>> GetCategories([Service] IAppealCategoryService appealService,
        CancellationToken cancellationToken)
    {
        return await appealService.GetAppealCategories();
    }

    public async Task<AppealFormResponse> GetAppealById([Service] IAppealService appealService, Guid id)
    {
        return await appealService.GetAppealById(id);
    }
}