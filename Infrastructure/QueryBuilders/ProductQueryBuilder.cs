using API.Rest.Example.Data.Models;
using Core.Models.Manager.Builder;

namespace API.Rest.Example.Infrastructure.QueryBuilder;

public class ProductQueryBuilder : QueryBuilder<Product, int>, IProductQueryBuilder
{
    public ProductQueryBuilder(IQueryable<Product> initialQuery) : base(initialQuery)
    {
    }

    public void Test() { }
}
