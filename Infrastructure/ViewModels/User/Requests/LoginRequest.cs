using Core.API.Request.Response.Request;
using System.ComponentModel.DataAnnotations;

namespace API.Rest.Example.Infrastructure.ViewModels.User.Requests;

public class LoginRequest : BaseModelRequest
{
    [Required]
    [MaxLength(100)]
    public string Email { get; set; }
    [Required]
    [MaxLength(150)]
    public string Password { get; set; }
}
