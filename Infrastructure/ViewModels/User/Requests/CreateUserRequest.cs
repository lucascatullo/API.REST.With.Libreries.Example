using API.Rest.Example.Data.Constants;
using Core.API.Request.Response.Request;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;
using Utilities.Helpers.Extensions.Extension;

namespace API.Rest.Example.Infrastructure.ViewModel.User.Request;

public class CreateUserRequest : BaseModelRequest, ICreateUserRequest
{
    /// <summary>
    /// User Email
    /// </summary>
    [Required]
    [MaxLength(100)]
    public string Email { get; set; }
    /// <summary>
    /// User password. Min lenght 8. at least 1 character and one number.
    /// </summary>
    [Required]
    [MaxLength(150)]
    public string Password { get; set; }
    /// <summary>
    /// User roles. Possible options: SUPER_ADMIN , ADMIN
    /// </summary>
    public string[] Roles { get; set; } = [];

    public override bool ModelIsValid(ModelStateDictionary modelState)
    {
        if (!Password.IsValidAsPassword()) 
            modelState.AddModelError("Password", "The password is not valid.");
        foreach(var role in Roles)
        {
            if (!ApiConstants.USER_ROLES.Contains(role))
                modelState.AddModelError("Role", $"The role {role} is invalid");
        }
        return base.ModelIsValid(modelState);
    }
}
