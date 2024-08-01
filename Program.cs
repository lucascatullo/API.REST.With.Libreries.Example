using API.Rest.Example.Data;
using API.Rest.Example.Data.Models;
using API.Rest.Example.Infrastructure.Extention;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Tools.JWT.Helper.Extension;
using Utilities.Helpers.Extensions.DefaultConfig;


var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.AddCustomServices();

builder.Services.AddDbContext<ExampleContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")), ServiceLifetime.Transient);

builder.AddIdentitySiteConfig();

builder.Services.AddSwaggerGen(c => SwaggerDefaultConfig.DefaultConfigFunction(c, Assembly.GetExecutingAssembly().GetName().Name!));
builder.Services.AddAuthentication().AuthenticateUsingJwt(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
using (var scope  = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ExampleContext>();
    await context.CreateRoles(scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>());
}

app.Run();

