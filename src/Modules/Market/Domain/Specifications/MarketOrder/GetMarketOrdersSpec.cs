using Ardalis.Specification;

namespace Astrum.Market.Specifications.MarketOrder;

public class GetMarketOrdersSpec : Specification<Aggregates.MarketOrder>
{
    public GetMarketOrdersSpec()
    {
        Query
            .Include(x => x.OrderProducts);
    }
}

public class GetMarketOrdersByUserIdSpec : Specification<Aggregates.MarketOrder>
{
    public GetMarketOrdersByUserIdSpec(Guid id)
    {
        Query
            .Include(x => x.OrderProducts).ThenInclude(x => x.Product)
            .Where(x => x.UserId == id || id == Guid.Empty);
    }
}