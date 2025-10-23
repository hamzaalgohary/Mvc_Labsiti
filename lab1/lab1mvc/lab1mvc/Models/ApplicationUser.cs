using Microsoft.AspNetCore.Identity;

namespace lab1mvc.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }
    }
}

