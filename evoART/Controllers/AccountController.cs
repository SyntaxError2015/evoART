using System;
using System.Linq;
using System.Web.Mvc;
using evoART.DAL.UnitsOfWork;
using evoART.Models.DbModels;
using evoART.Models.ViewModels;

namespace evoART.Controllers
{
    public class AccountController : Controller
    {

        public bool IsValidEmail(string email)
        {
            try
            {
                new System.Net.Mail.MailAddress(email);

                return true;
            }
            catch
            {
                return false;
            }
        }


        public string GetStatus()
        {
            return "Totul e OK la " + DateTime.Now.ToLongTimeString();
        }


        public string UpdateForm(string textBox1)
        {
            if (textBox1 != "Enter text")
            {
                return "Ai scris: \"" + textBox1 + "\" at " +
                    DateTime.Now.ToLongTimeString();
            }

            return String.Empty;
        }

        public string Login(LoginModel model)
        {

            if (!UserAccountsWorkUnit.Instance.UserAccountRepository.VerifyExists(model.UserName))
                return "U";

            if (UserAccountsWorkUnit.Instance.AccountValidationRepository.GetFailedLoginAttempts(model.UserName) > 10)
                return "E";

            if (!UserAccountsWorkUnit.Instance.UserAccountRepository.VerifyPassword(model.UserName, model.Password))
            {
                UserAccountsWorkUnit.Instance.AccountValidationRepository.IncrementFailedLoginAttempts(model.UserName);
                return "P";
            }
                
            //When login succeeds reset the failed logins
            UserAccountsWorkUnit.Instance.AccountValidationRepository.ResetLoginFailAttempts(model.UserName);

            return "K";
        }

        public string Register(RegisterModel model)
        {
            if (model.UserName==null || model.UserName.Length < 4 || model.UserName.Length > 28 || !Char.IsLetter(model.UserName[0]))
                return "U";
            if (UserAccountsWorkUnit.Instance.UserAccountRepository.VerifyExists(model.UserName))
                return "D";
            if (model.EmailAddress==null || !IsValidEmail(model.EmailAddress))
                return "E";
            if (model.Password==null || model.Password.Length < 6 || model.Password.Length > 28)
                return "P";
            if (model.PasswordConfirmation==null || model.Password != model.PasswordConfirmation)
                return "M";

            var newUser = new AccountModels.UserAccount
            {
                UserName = model.UserName,
                Email = model.EmailAddress,
                Password = model.Password,
                //Role = new AccountModels.Role() { RoleName = model.Role}
                Role = UserAccountsWorkUnit.Instance.RoleRepository.GetRole(model.Role)
            };

            // When creating a new user, the UnitOfWork automatically creates a validation entry in the
            // AccountValidations table for the user.
            // The password is also automatically encrypted in the model
            return UserAccountsWorkUnit.Instance.UserAccountRepository.Insert(newUser) ? "K" : "F";
        }

        public PartialViewResult RegisterTab()
        {
            var t = UserAccountsWorkUnit.Instance.RoleRepository.GetRoleNames().Select(r => new SelectListItem {Text = r, Value = r}).ToList();

            ViewData["Roles"] = t;

            return PartialView("Register");
        }

        public PartialViewResult LoginTab()
        {
            return PartialView("Login");
        }
            
    }
}
