using System.ComponentModel.DataAnnotations;

namespace net_core_based.Models
{
    public class UserLogin
    {
        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; } = string.Empty;
    }
}
