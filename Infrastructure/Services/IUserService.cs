

using API.Rest.Example.Data.Models;
using API.Rest.Example.Infrastructure.ViewModel.User.Request;

namespace API.Rest.Example.Infrastructure.Service;

public interface IUserService
{
    Task<User> CreateAsync(ICreateUserRequest request);
    Task<User> GetAsync(string id);
    Task<User> LogInAsync(string email, string password);
    Task<ICollection<string>> GetRolesAsync(User user);
}