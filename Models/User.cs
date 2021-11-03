using Microsoft.AspNetCore.Identity;

namespace KievGyms.Models
{
    public class User : IdentityUser
    {
        public int Year { get; set; }
    }
}