using API.Rest.Example.Data.Models;

namespace API.Rest.Example.Infrastructure.ViewModels.Product.Requests;

public interface IEditProductRequest : IProduct
{
    int Id { get; set; }
}