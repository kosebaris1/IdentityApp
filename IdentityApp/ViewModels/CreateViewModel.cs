using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace IdentityApp.ViewModels
{
    public class CreateViewModel
    {
      
        [Required]
        [DisplayName("Full Name")]
        public string? FullName { get; set; }

        [Required(ErrorMessage = "E-Posta alanı boş olamaz")]
        [EmailAddress]
        [DisplayName("Email")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Şifre alanı boş olamaz")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string? Password { get; set; }

   
        [DataType(DataType.Password)]
        [DisplayName("Confirm Password")]
        [Compare(nameof(Password),ErrorMessage ="Parola Eşleşmiyor.")]
        public string? ConfirmPassword { get; set; }

    }
}
