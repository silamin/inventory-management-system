using static Entities.Roles;

namespace Entities
{
    public class User
    {
        public int UserId { get; set; }
        public required string UserName { get; set; }
        public required string Password { get; set; }
        public required UserRole UserRole { get; set; }
        public List<Order> Orders { get; set; } = new List<Order>();


        public static User Create(string requestUserName, string requestPassword, UserRole requestUserRole)
        {
            return new User
            {
                UserName = requestUserName,
                Password = requestPassword,
                UserRole = requestUserRole,
            };
        }

    }
}
