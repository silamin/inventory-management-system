using Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using SEP3_T3_ASP_Core_WebAPI;
using SEP3_T3_ASP_Core_WebAPI.RepositoryContracts;

namespace EfcRepositories.Repositories;

public class EfcUserRepository: IUserRepository
{
    private readonly AppDbContext _ctx;
    private readonly IPasswordHasher<User> _passwordHasher;

    public EfcUserRepository(AppDbContext ctx, IPasswordHasher<User> passwordHasher)
    {
        this._ctx = ctx;
        this._passwordHasher = passwordHasher;
    }

    // Add a user to the database
    public async Task<User> GetUserById(int id)
    {
        try
        {
            Console.WriteLine($"Attempting to retrieve user with ID: {id}");

            // Use FirstOrDefaultAsync or FindAsync to search for the user by ID
            User? user = await _ctx.Users.FindAsync(id);

            if (user == null)
            {
                Console.WriteLine($"User with ID {id} not found.");
                throw new InvalidOperationException($"User with ID {id} not found.");
            }

            Console.WriteLine($"User with ID {id} retrieved successfully: {user}");
            return user;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error retrieving user with ID {id}: {ex.Message}");
            throw;
        }
    }


    public async Task<User> AddUserAsync(User user)
    {
        EntityEntry<User> entityEntry = await _ctx.Users.AddAsync(user);
        await _ctx.SaveChangesAsync();
        return entityEntry.Entity;
    }

    // Update a user in the database
    public async Task<User> UpdateUserAsync(int userId, User user)
    {
        // Retrieve the user to update
        User userToUpdate = await GetUserById(userId);

        if (userToUpdate == null)
        {
            throw new InvalidOperationException("User does not exist");
        }

        // Update UserName only if it's different and not empty
        if (!string.IsNullOrWhiteSpace(user.UserName) && user.UserName != userToUpdate.UserName)
        {
            userToUpdate.UserName = user.UserName;
        }

        // Update Password only if a new password is provided (not empty) and hash it
        if (!string.IsNullOrWhiteSpace(user.Password))
        {
            var passwordHasher = new PasswordHasher<User>();
            userToUpdate.Password = passwordHasher.HashPassword(userToUpdate, user.Password);
        }

        // Update UserRole (since role updates are straightforward)
        if (user.UserRole != userToUpdate.UserRole)
        {
            userToUpdate.UserRole = user.UserRole;
        }

        // Update the user in the context
        _ctx.Users.Update(userToUpdate);

        // Save the changes to the database
        await _ctx.SaveChangesAsync();

        return userToUpdate;
    }


    // Delete a user from the database
    public async Task<User> DeleteUserAsync(int id)
    {
        // Find the user by Id
        var user = await _ctx.Users.FindAsync(id);

        if (user == null)
        {
            throw new KeyNotFoundException($"User with ID {id} not found.");
        }

        _ctx.Users.Remove(user); // Remove the user from the context
        await _ctx.SaveChangesAsync(); // Save the changes to the database

        return user; // Return the deleted user
    }



    public IQueryable<User> GetAllUsers()
    {
        return _ctx.Users.AsQueryable();
    }


    public IQueryable<User> GetAllUsersByRole(UserRole type)
    {
        return _ctx.Users.Where(u => u.UserRole == type);
    }

    public Task<User?> GetUserByUsernameAndPasswordAsync(string? username, string? password)
    {
        throw new NotImplementedException();
    }

    // Get a single user from the database
    public async Task<User> GetSingleAsync(int userId)
    {
        return await _ctx.Users.SingleOrDefaultAsync(u => u.UserId == userId) ?? throw new InvalidOperationException();
    }
    

    //get user by username
    public Task<User?> GetUserByUsernameAsync(string requestUserName)
    {
        return  Task.FromResult(_ctx.Users.FirstOrDefault(u => u.UserName == requestUserName));
    }
    
    
}