using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TestAPI.DAL.Entities
{
    public class User
    {
        [Required]
        public long Id { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Role { get; set; }
    }

    public enum Role
    {
        [Description("Admin")]
        Admin,
        [Description("Guest")]
        Guest
    }
}
