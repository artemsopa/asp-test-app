using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestAPI.WEB.Models.ViewModels
{
    public class AuthViewModel
    {
        [Required(ErrorMessage = "There is no login here")]
        [StringLength(30, MinimumLength = 5)]
        public string Login { get; set; }

        [Required(ErrorMessage = "There is no password here")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
