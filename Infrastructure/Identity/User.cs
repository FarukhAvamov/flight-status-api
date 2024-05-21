using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity;

public class User
{
    public int ID { get; set; }
    public string Username { get; set; }
    public string Password { get; set; } // Хранить хеш пароля
    public int RoleId { get; set; }
    public Role Role { get; set; }
}