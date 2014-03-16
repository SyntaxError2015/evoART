using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.DynamicData;

namespace evoART.Models
{
    public class AccountModels
    {
        [TableName("UserAccount")]
        public class UserAccount
        {
            [Key]
            [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
            public int UserId { get; set; }

            [Required]
            [DataType(DataType.Text)]
            public string UserName { get; set; }

            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }
            
            [Required]
            [DataType(DataType.EmailAddress)]
            public string Email { get; set; }

            [DataType(DataType.Text)]
            public string FistName { get; set; }

            [DataType(DataType.Text)]
            public string LastName { get; set; }

            [DataType(DataType.PhoneNumber)]
            public string PhoneNumber { get; set; }

            [DataType(DataType.DateTime)]
            public DateTime BirthDate { get; set; }

            public int RoleId { get; set; }
        }

        [TableName("AccountValidation")]
        public class AccountValidation
        {
            public int UserId { get; set; }

            public bool IsVerified { get; set; }

            [DataType(DataType.Text)]
            public string ValidationToken { get; set; }

            [DataType(DataType.DateTime)]
            public DateTime ValidationTokenExpireDate { get; set; }

            public int LoginFails { get; set; }
        }

        [TableName("Role")]
        public class Role
        {
            [Key]
            public int RoleId { get; set; }

            [DataType(DataType.Text)]
            public string RoleName { get; set; }
        }
    }
}