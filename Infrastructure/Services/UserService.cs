using API.Rest.Example.Data;
using API.Rest.Example.Data.Models;
using API.Rest.Example.Infrastructure.Exception;
using API.Rest.Example.Infrastructure.ViewModel.User.Request;
using Core.Models.Manager.Exception;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Utilities.Helpers.Extensions.Extension;

namespace API.Rest.Example.Infrastructure.Service;

/// <summary>
/// Class responsability is to manage the user CRUD and log in.
/// </summary>
/// <param name="context">Data base context</param>
/// <param name="userManager">Microsoft Identity UserManager</param>
public class UserService(ExampleContext context, UserManager<User> userManager) : IUserService
{

    private readonly ExampleContext _db = context;
    private UserManager<User> _userManager = userManager;


    public async Task<User> CreateAsync(ICreateUserRequest request)
    {
        if (await _userManager.FindByEmailAsync(request.Email) != null)
            throw new Exception<RepeatedEmailException>(new RepeatedEmailException(request.Email));
        var user  = new User() {  UserName = request.Email.Split('@').First() + Guid.NewGuid().ToString(), Email = request.Email};
        var registrationResult = await _userManager.CreateAsync(user, request.Password);
        if (!registrationResult.Succeeded)
            throw new Exception<UnExpectedRegisterException>(new UnExpectedRegisterException());

        foreach (var role in request.Roles)
            await _userManager.AddToRoleAsync(user, role);

        return user;
    }

    public async Task<User> GetAsync(string id)
    {
        var response = await _userManager.FindByIdAsync(id);
        return response ?? throw new Exception<NotFoundInQueryException>(new NotFoundInQueryException("User", "ID", id));
    }

    public async Task<ICollection<string>> GetRolesAsync(User user)
    {
        var roles = await _userManager.GetRolesAsync(user);

        return roles ?? [];
    }

    public async Task<User> LogInAsync(string email, string password)
    {
        User user = await _userManager.FindByEmailAsync(email) ?? throw new Exception<InvalidLogInException>(new InvalidLogInException());
       
        var logged = await _userManager.CheckPasswordAsync(user, password);
        if (!logged)
            throw new Exception<InvalidLogInException>(new InvalidLogInException());

        return user;
    }

}