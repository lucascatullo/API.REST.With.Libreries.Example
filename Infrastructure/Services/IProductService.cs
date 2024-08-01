using API.Rest.Example.Data.Models;
using API.Rest.Example.Infrastructure.ViewModels.Product.Requests;
using Code.Models.Manager.Service;

namespace API.Rest.Example.Infrastructure.Service;

public interface IProductService : IBaseService<Product, int>
{
    Task<Product> CreateAsync(IProduct values);
    Task<Product> EditAsync(IEditProductRequest values);
}