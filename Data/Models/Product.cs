using Core.Models.Manager.Model;
using System.ComponentModel.DataAnnotations;

namespace API.Rest.Example.Data.Models;

public class Product : BaseModel<int>, IProduct
{
    [Required]
    [MaxLength(50)]
    public string Name { get; set; }

    [MaxLength(500)]
    public string? Description { get; set; }

    [Range(1, double.MaxValue)]
    public double Price { get; set; }
}

