using API.Rest.Example.Data.Models;

namespace API.Rest.Example.Infrastructure.ViewModel.Product.Response;

public class CreateProductResponse : IProduct
{
    public int Id { get; set; }
    public string? Description { get ; set ; }
    public string Name { get ; set ; }
    public double Price { get; set; }
}
