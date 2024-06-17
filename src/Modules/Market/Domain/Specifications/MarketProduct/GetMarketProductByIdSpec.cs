using Ardalis.Specification;

namespace Astrum.Market.Specifications.MarketProduct;

public class GetMarketProductByIdSpec : Specification<Aggregates.MarketProduct>
{
    public GetMarketProductByIdSpec(Guid id)
    {
        Query
            .Where(x => x.Id == id);
    }
}

public class GetMarketProductsByIdsSpec : Specification<Aggregates.MarketProduct>
{
    public GetMarketProductsByIdsSpec(IEnumerable<Guid> ids)
    {
        Query
            .Where(x => ids.Contains(x.Id));
    }
}