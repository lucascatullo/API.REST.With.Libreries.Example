using API.Rest.Example.Data.Models;
using Core.Mapper.ListMapper;

namespace API.Rest.Example.Infrastructure.Mappers;

public interface IUserMapper : IEspecializeMapper<User>
{
    IDictionary<string, object> BuildVM(User source, string token);
}