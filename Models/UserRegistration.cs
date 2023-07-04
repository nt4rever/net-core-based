﻿using System.ComponentModel.DataAnnotations;

namespace net_core_based.Models
{
    public class UserRegistration
    {
        [Required(ErrorMessage = "Username is mandatory.")]
        public string Username { get; set; } = string.Empty;

        [EmailAddress]
        [Required(ErrorMessage = "Email is mandatory.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is mandatory.")]
        public string Password { get; set; } = string.Empty;
    }
}
