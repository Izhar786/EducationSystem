using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EducationSystem.ViewModels
{
    public class LoginViewModel
    {
        [Display(Name = "Username")]
        [Required(ErrorMessage = "{0} is required")]
        [StringLength(13, ErrorMessage = "{0} must be exact {1} characters", MinimumLength = 13)]
        [RegularExpression(@"^[0-9]{13}$", ErrorMessage ="{0} must be a valid CNIC / B-Form")]
        public string Username { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "{0} is required")]
        [StringLength(25, ErrorMessage = "{0} must be {2} - {1} characters", MinimumLength = 8)]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}
