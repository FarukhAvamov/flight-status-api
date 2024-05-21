using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Identity;

public class RegisterModel
{
    [Required]
    public string UserName { get; set; }
    [Required]
    public string Password { get; set; }
}