using API.Rest.Example.Data.Models;
using API.Rest.Example.Infrastructure.ViewModel.User.Response;
using Core.Mapper.ListMapper;

namespace API.Rest.Example.Infrastructure.Mappers;

public class UserMapper : EspecializeMapper<User>, IUserMapper
{
    public override IDictionary<string, object> BuildVM(User source)
    {
        var response = new Dictionary<string, object>
        {
            { "user", CopyAndReturn<UserResponse>(source) }
        };
        return response;
    }

    public IDictionary<string, object> BuildVM(User source, string token)
    {
        var response = BuildVM(source);

        response.Add("token", token);
        return response;
    }
}
