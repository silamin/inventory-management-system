

using Entities;

namespace ASP_Core_WebAPI.RepositoryContracts;

public interface IAuthRepository
{
    Task<User> LoginAsync(string username, string password);

}