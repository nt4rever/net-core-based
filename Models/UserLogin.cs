using System.ComponentModel.DataAnnotations;

namespace net_core_based.Models
{
    public class UserLogin
    {
        /// <summary>
        /// The user's username.
        /// </summary>
        /// <example>tan.le</example>
        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; } = string.Empty;

        /// <summary>
        /// The user's password.
        /// </summary>
        /// <example>Abcd1234@@</example>
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; } = string.Empty;
    }
}
