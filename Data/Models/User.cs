using Core.Models.Manager.Interface;
using Core.Models.Manager.Model;
using Microsoft.AspNetCore.Identity;

namespace API.Rest.Example.Data.Models;

public class User : IdentityUser, IBaseDbModel<string>
{
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    public DateTime ModifiedDate { get ; set ; } = DateTime.UtcNow;
    public bool LogicalDelete { get; set; } 
}
