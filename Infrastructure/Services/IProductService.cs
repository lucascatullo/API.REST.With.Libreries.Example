using API.Rest.Example.Data.Models;
using Code.Models.Manager.Service;

namespace API.Rest.Example.Infrastructure.Service;

public interface IProductService : IBaseService<Product, int>
{
    Task<Product> CreateAsync(IProduct values);
}