using System;
using System.Collections.Generic;
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
            public int Id { get; set; }

            public string UserName { get; set; }

            public string Password { get; set; }
            
            public string Email { get; set; }

            public string FistName { get; set; }

            public string LastName { get; set; }

            public string PhoneNumber { get; set; }

            public DateTime? BirthDate { get; set; }

            public virtual Role Role { get; set; }

            public virtual AccountValidation AccountValidation { get; set; }
        }

        [TableName("AccountValidation")]
        public class AccountValidation
        {
            public int Id { get; set; }

            public bool IsVerified { get; set; }

            [DataType(DataType.Text)]
            public string ValidationToken { get; set; }

            [DataType(DataType.DateTime)]
            public DateTime ValidationTokenExpireDate { get; set; }

            public int LoginFails { get; set; }

            public virtual UserAccount UserAccount { get; set; }
        }

        [TableName("Role")]
        public class Role
        {
            public int Id { get; set; }

            public string RoleName { get; set; }

            public virtual ICollection<UserAccount> UserAccounts { get; set; }
        }
    }
}