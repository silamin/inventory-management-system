using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SEP3_T3_ASP_Core_WebAPI.ApiContracts.UserDto;
using SEP3_T3_ASP_Core_WebAPI.RepositoryContracts;

namespace SEP3_T3_ASP_Core_WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController: ControllerBase
{
    private readonly IUserRepository userRepo;

    public UsersController(IUserRepository userRepo)
    {
        this.userRepo = userRepo;
    }

    // ********** CREATE Endpoints **********
    // POST: /Users
    [HttpPost]
    public async Task<ActionResult<GetUserDto>> AddUser([FromBody] UserCreateDto request)
    {
        try
        {
            // Create a new User instance
            User user = Entities.User.Create(request.UserName, request.Password, request.UserRole);

            // Save the user to the repository
            User created = await userRepo.AddUserAsync(user);

            // Map the created User to GetUserDto
            GetUserDto userDto = new GetUserDto
            {
                UserId = created.UserId,
                UserName = created.UserName,
                UserRole = created.UserRole,
            };

            // Return the created user DTO with status 201 (Created)
            return CreatedAtAction(nameof(AddUser), new { id = created.UserId }, userDto);
        }
        catch (ApplicationException ex)
        {
            // Return a 400 Bad Request with the error message
            return BadRequest(new { error = ex.Message });
        }
        catch (Exception ex)
        {
            // Return a 500 Internal Server Error for unexpected errors
            return StatusCode(500, $"An unexpected error occurred: {ex.Message}");
        }
    }


    // ********** UPDATE Endpoints **********
    // PUT: /Users/{id}
    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateUser([FromRoute] int id, [FromBody] UserDto request)
    {
        try
        {
            // Step 1: Map UserDto to User entity
            User user = new User
            {
                UserId = id, // Ensure the UserId is set properly
                UserName = request.UserName,
                Password = request.Password,
                UserRole = request.UserRole,
            };

            // Step 2: Pass the User entity to the repository (not the DTO)
            await userRepo.UpdateUserAsync(id, user);

            return NoContent();
        }
        catch (InvalidOperationException)
        {
            return NotFound($"User with ID {id} not found.");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, $"An error occurred: {e.Message}");
        }
    }


    // ********** Delete Endpoints **********
    // DELETE: /Users/{id}
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteUser([FromRoute] int id)
    {
        try
        {
            await userRepo.DeleteUserAsync(id);
            return NoContent();
        }
        catch (InvalidOperationException)
        {
            return NotFound($"User with ID {id} not found.");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, $"An error occurred: {e.Message}");
        }
    }

    // ********** GET Endpoints **********
    /**
     *     // GET: /Users
     * 
     * [HttpGet]
    public async Task<ActionResult<IEnumerable<GetUserDto>>> GetAllUsers()
    {
        try
        {
            // Fetch users and project them into GetUserDto objects to exclude the Password property
            List<GetUserDto> userDtos = await userRepo.GetAllUsers()
                .Select(user => new GetUserDto
                {
                    UserId = user.UserId,
                    UserName = user.UserName,
                    UserRole = user.UserRole,
                })
                .ToListAsync(); // Ensure asynchronous operation

            return Ok(userDtos);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, $"An error occurred: {e.Message}");
        }
    }

     * 
     */


    [HttpGet("role/{userRole}")]
    public async Task<ActionResult<IEnumerable<GetUserDto>>> GetUsersByRole(UserRole userRole)
    {
        try
        {
            // Use the repository method to fetch users by role
            List<GetUserDto> userDtos = await userRepo.GetUsersByRole(userRole)
                .Select(user => new GetUserDto
                {
                    UserId = user.UserId,
                    UserName = user.UserName,
                    UserRole = user.UserRole,
                })
                .ToListAsync();

            return Ok(userDtos);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, $"An error occurred: {e.Message}");
        }
    }


    [HttpGet("Type/{type}")]
    public async Task<ActionResult<IEnumerable<User>>> GetAllUsersByType([FromRoute] UserRole type)
    {
        try
        {
            List<User> dtos = await userRepo.GetAllUsersByRole(type)
                .ToListAsync();
            return Ok(dtos);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, $"An error occurred: {e.Message}");
        }
    }

}