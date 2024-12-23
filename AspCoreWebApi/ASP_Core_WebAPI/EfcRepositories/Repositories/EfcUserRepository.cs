using Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using ASP_Core_WebAPI;
using ASP_Core_WebAPI.RepositoryContracts;
using static Entities.Roles;

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
        // Check if a user with the same username already exists
        bool userExists = await _ctx.Users.AnyAsync(u => u.UserName == user.UserName);
        if (userExists)
        {
            throw new ApplicationException($"A user with the username '{user.UserName}' already exists.");
        }

        // Add the new user
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

        // Update Password only if a new password is provided (not empty)
        if (!string.IsNullOrWhiteSpace(user.Password))
        {
            var passwordHasher = new PasswordHasher<User>();
            userToUpdate.Password = passwordHasher.HashPassword(userToUpdate, user.Password);
        }

        // Update UserRole only if it is provided and different
        if (user.UserRole != default && user.UserRole != userToUpdate.UserRole)
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


    public IQueryable<User> GetUsersByRole(UserRole userRole)
    {
        return _ctx.Users.Where(user => user.UserRole == userRole).AsQueryable();
    }
    /*
     * public IQueryable<User> GetAllUsers()
    {
        return _ctx.Users.AsQueryable();
    }
     * 
     */
    
}