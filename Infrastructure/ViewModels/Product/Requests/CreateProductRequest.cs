using API.Rest.Example.Data.Models;
using Core.API.Request.Response.Request;
using System.ComponentModel.DataAnnotations;

namespace API.Rest.Example.Infrastructure.ViewModel.Product.Request;

public class CreateProductRequest : BaseModelRequest, IProduct
{
    [Required]
    [MaxLength(50)]
    public string Name { get; set; }

    [MaxLength(500)]
    public string? Description { get; set; }

    [Range(1, double.MaxValue)]
    public double Price { get; set; }
}

