using Ardalis.Specification;

namespace Astrum.Market.Specifications.MarketProduct;

public class GetMarketProductsSpec : Specification<Aggregates.MarketProduct>
{
    public GetMarketProductsSpec(int page, int pageSize)
    {
        Query.Skip((page - 1) * pageSize).Take(pageSize);
    }
}