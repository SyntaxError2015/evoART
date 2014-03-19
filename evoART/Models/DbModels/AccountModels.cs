using System;
using System.Collections.Generic;
using System.Web.DynamicData;

namespace evoART.Models.DbModels
{
    public class AccountModels
    {
        [TableName("UserAccount")]
        public class UserAccount
        {
            public int UserId { get; set; }

            public string UserName { get; set; }

            public string Password { get; set; }
            
            public string Email { get; set; }

            public string FistName { get; set; }

            public string LastName { get; set; }

            public string PhoneNumber { get; set; }

            public DateTime? BirthDate { get; set; }

            public virtual Role Role { get; set; }

            public virtual AccountValidation AccountValidation { get; set; }

            public virtual Session Session { get; set; }
        }

        [TableName("AccountValidation")]
        public class AccountValidation
        {
            public int AccountValidationId { get; set; }
            public bool IsVerified { get; set; }

            public string ValidationToken { get; set; }

            public DateTime ValidationTokenExpireDate { get; set; }

            public int LoginFails { get; set; }

            public virtual UserAccount UserAccount { get; set; }
        }

        [TableName("Role")]
        public class Role
        {
            public int RoleId { get; set; }

            public string RoleName { get; set; }

            public virtual ICollection<UserAccount> UserAccounts { get; set; }
        }

        [TableName("Session")]
        public class Session
        {
            public int SessionId { get; set; }

            public string SessionKey { get; set; }

            public virtual UserAccount UserAccount { get; set; }
        }
    }
}