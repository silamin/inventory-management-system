using System.ComponentModel.DataAnnotations;

namespace SEP3_T3_ASP_Core_WebAPI.ApiContracts.AuthDtos;

public class LoginRequest
{
    public LoginRequest(string userName, string password)
    {
        UserName = userName;
        Password = password;
    }
    [Required]
    public string UserName { get; set; }

    [Required]
    public string Password { get; set; }
}