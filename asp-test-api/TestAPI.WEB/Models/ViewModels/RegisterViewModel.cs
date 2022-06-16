using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestAPI.WEB.Models.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "There is no login here")]
        [StringLength(30, MinimumLength = 5, ErrorMessage = "Invalid length")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "There is no email here")]
        [EmailAddress(ErrorMessage = "There is no email here")]
        public string Email { get; set; }

        [Required(ErrorMessage = "There is no password here")]
        [DataType(DataType.Password)]
        [StringLength(30, MinimumLength = 5, ErrorMessage = "Invalid length")]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "Passwords are mismatch")]
        [DataType(DataType.Password)]
        public string PasswordConfirm { get; set; }

        public string Role { get; set; }
    }
}
