using System;
using System.Linq;
using evoART.DAL.UnitsOfWork;
using evoART.Models.DbModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace evoART.UnitTests
{
    //[TestClass]
    public class UserAccountTests
    {
        private static readonly AccountModels.UserAccount user0 = new AccountModels.UserAccount
        {
            UserName = "user0",
            Email = "user0@u.user",
            Password = "user0"
        };

        private static readonly AccountModels.UserAccount user1 = new AccountModels.UserAccount
        {
            UserName = "user1",
            Email = "user1@u.user",
            Password = "user1"
        };

        private static readonly AccountModels.UserAccount user2 = new AccountModels.UserAccount
        {
            UserName = "user2",
            Email = "user2@u.user",
            Password = "user2"
        };

        private static readonly AccountModels.UserAccount user3 = new AccountModels.UserAccount
        {
            UserName = "user3",
            Email = "user3@u.user",
            Password = "user3"
        };

        [TestMethod]
        public void UserAccountsTest()
        {
            var users = new AccountModels.UserAccount[4];
            users[0] = user0;
            users[1] = user1;
            users[2] = user2;
            users[3] = user3;

            foreach (var user in users)
            {
                user.Role = DatabaseWorkUnit.Instance.RoleRepository.GetRole("Photographer");

                if (!DatabaseWorkUnit.Instance.UserAccountRepository.Insert(user))
                    throw new Exception("User account creation failed");
            }

            user0.UserName = "MOFO";
            user2.PhoneNumber = "666";

            if (users.Any(t => !DatabaseWorkUnit.Instance.UserAccountRepository.Update(t)))
            {
                throw new Exception("User account update failed");
            }

            if (!DatabaseWorkUnit.Instance.UserAccountRepository.Delete(user3.UserName))
                throw new Exception("User account delete failed");
        }
    }
}
