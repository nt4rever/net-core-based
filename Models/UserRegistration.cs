using System.ComponentModel.DataAnnotations;

namespace net_core_based.Models
{
    public class UserRegistration
    {
        /// <summary>
        /// The user's username.
        /// </summary>
        /// <example>tan.le</example>
        [Required(ErrorMessage = "Username is mandatory.")]
        public string Username { get; set; } = string.Empty;

        /// <summary>
        /// The user's email.
        /// </summary>
        /// <example>tan@example.com</example>
        [EmailAddress]
        [Required(ErrorMessage = "Email is mandatory.")]
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// The user's password.
        /// </summary>
        /// <example>Abcd1234@@</example>
        [Required(ErrorMessage = "Password is mandatory.")]
        public string Password { get; set; } = string.Empty;
    }
}
