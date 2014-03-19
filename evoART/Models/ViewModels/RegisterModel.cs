using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace evoART.Models.ViewModels
{
    public class RegisterModel
    {
        [Required]
        [DataType(DataType.Text)]
        [DisplayName("Username")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [DisplayName("Email address")]
        public string EmailAddress { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [DisplayName("Password")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [DisplayName("Confirm password")]
        public string PasswordConfirmation { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [DisplayName("What do you want to be?")]
        public string Role { get; set; }
    }
}