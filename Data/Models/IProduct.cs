namespace API.Rest.Example.Data.Models;

public interface IProduct
{
    string? Description { get; set; }
    string Name { get; set; }
    double Price { get; set; }
}