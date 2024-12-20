using Entities;
using System.Text.Json.Serialization;

namespace SEP3_T3_ASP_Core_WebAPI.ApiContracts.UserDto
{
    public class UserCreateDto
    {
        public string UserName { get; set; }
        public string Password { get; set; }

        public UserRole UserRole { get; set; }
    }
    public class GetUserDto
    {
        public int UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public UserRole UserRole { get; set; }
    }
    public class UserDto
    {
        public int UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public UserRole UserRole { get; set; }
    }
}
