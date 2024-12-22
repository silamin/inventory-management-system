using Entities;
using static Entities.Roles;

namespace SEP3_T3_ASP_Core_WebAPI.RepositoryContracts;

public interface IUserRepository
{
    Task<User> GetUserById(int id); 
    Task<User> AddUserAsync(User user);
    Task<User> UpdateUserAsync(int userId, User user);
    Task<User> DeleteUserAsync(int id);
    //IQueryable<User> GetAllUsers();
    IQueryable<User> GetUsersByRole(UserRole userRole);

}