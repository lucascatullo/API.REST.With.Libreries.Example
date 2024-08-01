using API.Rest.Example.Data.Models;
using Core.Models.Manager.Interface;

namespace API.Rest.Example.Infrastructure.QueryBuilder;

public interface IProductQueryBuilder : IQueryBuilder<Product, int>
{
    void Test();
}