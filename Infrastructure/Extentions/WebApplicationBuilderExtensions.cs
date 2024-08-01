using API.Rest.Example.Data.Models;
using API.Rest.Example.Data;
using API.Rest.Example.Infrastructure.Service;
using Core.Main.Mapper;
using Microsoft.AspNetCore.Identity;
using Core.Mapper.ListMapper;
using API.Rest.Example.Infrastructure.Mappers;
using Tools.JWT.Helper.Helper;

namespace API.Rest.Example.Infrastructure.Extention;

public static class WebApplicationBuilderExtensions
{
    public static void AddCustomServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IProductService, ProductService>();
        builder.Services.AddScoped<IMapper, Mapper>();
        builder.Services.AddScoped<IUserService, UserService>();
        builder.Services.AddScoped<IUserMapper, UserMapper>();
        builder.Services.AddScoped<IJwtGenerator, JwtGenerator>();
    }

    public static void AddIdentitySiteConfig(this WebApplicationBuilder builder)
    {
        builder.Services.AddIdentity<User, IdentityRole>(o =>
        {
            o.Password.RequireDigit = false;
            o.Password.RequireLowercase = false;
            o.Password.RequireUppercase = false;
            o.Password.RequireNonAlphanumeric = false;
            o.Password.RequiredLength = 6;
        }).AddEntityFrameworkStores<ExampleContext>().AddDefaultTokenProviders();
    }
}
