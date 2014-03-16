using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.DynamicData;

namespace evoART.Models
{
    public class AccountModels
    {
        [TableName("Users")]
        public class Users
        {
            [Key]
            public int UserId { get; set; }

            [Required]
            public string UserName { get; set; }
            
            [Required]
            [DataType(DataType.EmailAddress)]
            public string Email { get; set; }

            public string FistName { get; set; }

            public string LastName { get; set; }

            [DataType(DataType.PhoneNumber)]
            public string PhoneNumber { get; set; }

            [DataType(DataType.DateTime)]
            public string BirthDate { get; set; }

            public int RoleId { get; set; }
        }

        [TableName("AccountValidation")]
        public class AccountValidation
        {
            
        }

        [TableName("Roles")]
        public class Roles
        {
            [Key]
            public int RoleId { get; set; }

            public string RoleName { get; set; }
        }
    }
}