using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity;

public class Role
{
    public int ID { get; set; }
    public string Code { get; set; }
    public ICollection<User> Users { get; set; }
}