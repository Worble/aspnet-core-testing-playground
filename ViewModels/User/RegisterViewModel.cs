using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCore2Boilerplate.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Username is required.")]
        public string Username { get; set; }

        [Display(Name = "Email Address")]
        [Required(ErrorMessage = "Email Address is Required.")]
        [EmailAddress(ErrorMessage = "Please use a valid Email Address.")]
        public string EmailAddress { get; set; }

        [Required(ErrorMessage = "Password is required.")] 
        [DataType("Password")]
        [MinLength(6, ErrorMessage = "Passwords require a minimum of 6 characters.")]
        public string Password { get; set; }

        [Display(Name = "Confirm your password")]
        [Required(ErrorMessage = "Please confirm your password."), DataType("Password")]
        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        public string PasswordVerify { get; set; }       
    }
}
