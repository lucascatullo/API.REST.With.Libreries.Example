namespace API.Rest.Example.Infrastructure.ViewModel.User.Request;

public interface ICreateUserRequest
{
    string Email { get; set; }
    string Password { get; set; }
    public string[] Roles { get; set; }
}