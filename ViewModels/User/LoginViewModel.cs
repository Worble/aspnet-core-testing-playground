using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCore2Boilerplate.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Email Address or Username")]
        public string EmailAddressOrUsername { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public bool RememberMe { get; set; }

        public string ReturnUrl { get; set; }
    }
}
