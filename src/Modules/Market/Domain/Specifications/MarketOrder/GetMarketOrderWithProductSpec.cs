using Ardalis.Specification;

namespace Astrum.Market.Specifications.MarketOrder;

public class GetMarketOrderWithProductSpec : Specification<Aggregates.MarketOrder>
{
    public GetMarketOrderWithProductSpec(int page, int pageSize)
    {
        Query.Include(x => x.OrderProducts).ThenInclude(x => x.Product).Skip((page - 1) * pageSize).Take(pageSize);
    }
}