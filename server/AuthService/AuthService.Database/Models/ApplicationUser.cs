using Microsoft.AspNetCore.Identity;

namespace AuthService.Database.Models
{
    /// <summary>
    /// расширение пользователя 
    /// </summary>
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }

        public string Surname { get; set; }

        public string? RefreshToken { get; set; }

        public DateTime RefreshTokenExpiryTime { get; set; }
    }
}
