using API.Rest.Example.Data.Models;
using Core.API.Request.Response.Request;
using System.ComponentModel.DataAnnotations;

namespace API.Rest.Example.Infrastructure.ViewModel.Product.Request;

public class CreateProductRequest : BaseModelRequest, IProduct
{
    /// <summary>
    /// Name of the product
    /// </summary>
    [Required]
    [MaxLength(50)]
    public string Name { get; set; }
    /// <summary>
    /// Description of the product
    /// </summary>
    [MaxLength(500)]
    public string? Description { get; set; }
    /// <summary>
    /// Value of the product.
    /// </summary>
    [Range(1, double.MaxValue)]
    public double Price { get; set; }
}

