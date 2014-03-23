using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

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

        [DataType(DataType.Text)]
        [DisplayName("First name")]
        public string FirstName { get; set; }

        [DataType(DataType.Text)]
        [DisplayName("Last name")]
        public string LastName { get; set; }

        [DataType(DataType.Date)]
        [DisplayName("Birth Date")]
        public DateTime? BirthDate { get; set; }

        [DataType(DataType.PhoneNumber)]
        [DisplayName("Phone number")]
        public PhoneAttribute PhoneNumber { get; set; }

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