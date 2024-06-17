using Astrum.Market.ViewModels;
using Sakura.AspNetCore;

namespace Astrum.Market.Services;

public interface IMarketOrderService
{
    Task<SharedLib.Common.Results.Result<IPagedList<MarketOrderFormResponse>>> GetOrders(int page, int pageSize, Guid id);
    Task<SharedLib.Common.Results.Result<MarketOrderFormResponse>> GetOrder(Guid id);
    Task<SharedLib.Common.Results.Result<MarketOrderFormResponse>> Add(MarketOrderFormRequest order);
    Task<SharedLib.Common.Results.Result<MarketOrderFormResponse>> Remove(Guid id);
    Task<SharedLib.Common.Results.Result<MarketOrderFormResponse>> Update(Guid id, MarketOrderFormRequest order);
}