using Microsoft.AspNetCore.Identity;

namespace API.Entities;

public class User : IdentityUser
{
    public List<UserSection> UserSections { get; set; }
    public List<UserLesson> UserLessons { get; set; }
    public List<Payment> Payments { get; set; }
}