using Astrum.Market.Services;
using Astrum.Market.ViewModels;
using HotChocolate;
using HotChocolate.Types;
using Sakura.AspNetCore;

namespace Astrum.Market.GraphQL;

public class QueryMarket
{
    public async Task<BasketForm> GetBasket([Service] IBasketService basketService, Guid owner)
    {
        return await basketService.GetBasket(owner);
    }

    [UsePaging(MaxPageSize = 20, IncludeTotalCount = true)]
    [UseSorting]
    [UseFiltering]
    public async Task<IPagedList<MarketOrderFormResponse>> GetOrderList([Service] IMarketOrderService marketOrderService)
    {
        return (await marketOrderService.GetOrders(1, 100, Guid.Empty)).Data;
    }

    public async Task<MarketOrderFormResponse> GetOrder([Service] IMarketOrderService marketOrderService, Guid id)
    {
        return await marketOrderService.GetOrder(id);
    }

    [UsePaging(MaxPageSize = 20, IncludeTotalCount = true)]
    [UseSorting]
    [UseFiltering]
    public async Task<IPagedList<MarketProductFormResponse>> GetProductList([Service] IProductService productService)
    {
        return (await productService.GetProducts(1, 100)).Data;
    }

    public async Task<MarketProductFormResponse> GetProduct([Service] IProductService productService, Guid id)
    {
        return await productService.GetProduct(id);
    }
}