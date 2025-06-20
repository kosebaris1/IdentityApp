﻿using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace IdentityApp.ViewModels
{
    public class LoginViewModels 
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;

        public bool RememberMe { get; set; } = false;

    }
}
