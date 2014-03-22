﻿using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.SessionState;
using evoART.DAL.UnitsOfWork;
using evoART.Models.DbModels;
using evoART.Models.ViewModels;
using System.Collections.Generic;
using System.Web.Security;
using evoART.Special;

namespace evoART.Controllers
{
    public class AccountController : Controller
    {
        private readonly HttpContext context;
        private readonly CookieHelper _myCookie;

        public AccountController()
        {
            context = System.Web.HttpContext.Current;
            _myCookie=new CookieHelper();
            
            //FormsAuthentication.SetAuthCookie();
            //FormsAuthentication.CookieMode
        }

        /// <summary>
        /// Test if the specified email is valid
        /// </summary>
        /// <param name="email">The email string to be tested</param>
        /// <returns>Whether the email is valid or not</returns>
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

        
        /// <summary>
        /// Log the user in
        /// </summary>
        /// <param name="model"></param>
        /// <returns>A string value used in javascript checking</returns>
        [HttpPost]
        public string Login(LoginModel model)
        {

            if (!DatabaseWorkUnit.Instance.UserAccountRepository.VerifyExists(model.UserName))
                return "U";

            if (DatabaseWorkUnit.Instance.AccountValidationRepository.GetFailedLoginAttempts(model.UserName) > 10)
                return "E";

            if (!DatabaseWorkUnit.Instance.UserAccountRepository.VerifyPassword(model.UserName, model.Password))
            {
                DatabaseWorkUnit.Instance.AccountValidationRepository.IncrementFailedLoginAttempts(model.UserName);
                return "P";
            }
                
            //When login succeeds reset the failed logins
            DatabaseWorkUnit.Instance.AccountValidationRepository.ResetLoginFailAttempts(model.UserName);

            //Make a new session for the user
            AccountModels.Session newSession = DatabaseWorkUnit.Instance.SessionRepository.Login(model.UserName);

            //Create the cookies for the session
            _myCookie.SetCookie("sessionId", newSession.SessionId.ToString(), DateTime.Now.AddMonths(6));
            _myCookie.SetCookie("sessionKey", newSession.SessionKey, DateTime.Now.AddMonths(6));

            return "K";
        }

        /// <summary>
        /// Register a new user
        /// </summary>
        /// <param name="model"></param>
        /// <returns>A string value used in javascript checking</returns>
        public string Register(RegisterModel model)
        {
            if (model.UserName==null || model.UserName.Length < 4 || model.UserName.Length > 28 || !Char.IsLetter(model.UserName[0]))
                return "U";
            if (DatabaseWorkUnit.Instance.UserAccountRepository.VerifyExists(model.UserName))
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
                Role = DatabaseWorkUnit.Instance.RoleRepository.GetRole(model.Role)
            };

            // When creating a new user, the UnitOfWork automatically creates a validation entry in the
            // AccountValidations table for the user.
            // The password is also automatically encrypted in the model
            return DatabaseWorkUnit.Instance.UserAccountRepository.Insert(newUser) ? "K" : "F";
        }

        public string GetUserName()
        {
            return "";
        }

        public AccountModels.UserAccount GetUserDetails()
        {
            if (_myCookie.GetCookie("sessionId") == "" || _myCookie.GetCookie("sessionKey") == "")
                return null;

            AccountModels.UserAccount user = DatabaseWorkUnit.Instance.SessionRepository.GetUser(
                new Guid(_myCookie.GetCookie("sessionId")), _myCookie.GetCookie("sessionKey"));

            return user;
        }

        public PartialViewResult RegisterTab()
        {
            var t = DatabaseWorkUnit.Instance.RoleRepository.GetRoleNames().Select(r => new SelectListItem {Text = r, Value = r}).ToList();

            ViewData["Roles"] = t;

            return PartialView("Register");
        }

        public PartialViewResult LoginTab()
        {
            return PartialView("Login");
        }


       
    }
}
