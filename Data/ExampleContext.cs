using API.Rest.Example.Data.Constants;
using API.Rest.Example.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace API.Rest.Example.Data;

public class ExampleContext : IdentityDbContext<User>
{
    public ExampleContext(DbContextOptions<ExampleContext> options) : base(options)
    {
    }


    public DbSet<Product> Products { get; set; }

 

    public async Task CreateRoles(RoleManager<IdentityRole> roleManager)
    {
        string[] roles = ApiConstants.USER_ROLES.Split(',');
        foreach (var role in roles) 
        {
            if (!(await roleManager.RoleExistsAsync(role)))
            {
                await roleManager.CreateAsync(new IdentityRole(role));
            }
        }
    }
}
