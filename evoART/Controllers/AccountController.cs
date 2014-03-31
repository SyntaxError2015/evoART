using System;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Helpers;
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

        private readonly CookieHelper _myCookie;

        public AccountController()
        {
            _myCookie=new CookieHelper();
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

        /// <summary>
        /// Log out the currently logged in user
        /// </summary>
        /// <returns></returns>
        public string LogOut()
        {
            //Delete the cookies
            _myCookie.DeleteCookie("sessionId");
            _myCookie.DeleteCookie("sessionKey");

            //Delete the session from the server
            try
            {
                //Delete the session variable but remember the userName
                var userName = MySession.Current.UserDetails.UserName;
                MySession.Current.UserDetails = null;

                return DatabaseWorkUnit.Instance.SessionRepository.Logout(userName) ? "K" : "F";
            }
            catch (Exception)
            {
                return "F";
            } 
        }

        public AccountModels.UserAccount GetUserDetails()
        {
            if (_myCookie.GetCookie("sessionId") == "" || _myCookie.GetCookie("sessionKey") == "")
                return null;

            var user = DatabaseWorkUnit.Instance.SessionRepository.GetUser(
                new Guid(_myCookie.GetCookie("sessionId")), _myCookie.GetCookie("sessionKey"));

            if (user == null)
            {
                _myCookie.DeleteCookie("sessionId");
                _myCookie.DeleteCookie("sessionKey");
            }
                
            return user;
        }

        /// <summary>
        /// Login using external autentification
        /// </summary>
        /// <param name="providerName">Provider's name</param>
        /// <param name="userName">User name</param>
        /// <param name="id">The userId from the provider</param>
        /// <param name="userEmail">The email</param>
        /// <param name="token">The provider's token</param>
        /// <returns></returns>
        [HttpPost]
        public string AuthExternal(string providerName, string userName, string id, string userEmail, string token)
        {
            try
            {
                string fbpage =
                    new WebClient().DownloadString("https://graph.facebook.com/me?fields=id&access_token=" + token);
                string userId = fbpage.Substring(fbpage.IndexOf("id") + 5, 15);

                if (DatabaseWorkUnit.Instance.OAuthLoginRepository.VerifyExists(providerName, id))
                {
                    //get username from the oauth
                    userName = DatabaseWorkUnit.Instance.OAuthLoginRepository.GetUserNameForOAuth(providerName,id);

                    //Make a new session for the user
                    AccountModels.Session newSession = DatabaseWorkUnit.Instance.SessionRepository.Login(userName);

                    //Create the cookies for the session
                    _myCookie.SetCookie("sessionId", newSession.SessionId.ToString(), DateTime.Now.AddMonths(6));
                    _myCookie.SetCookie("sessionKey", newSession.SessionKey, DateTime.Now.AddMonths(6));

                    return "K";
                }
                else
                {
                    var newUser = new AccountModels.UserAccount
                    {
                        UserName = userName,
                        Email = userEmail,
                        Password = DateTime.Now.ToString(),
                        Role = DatabaseWorkUnit.Instance.RoleRepository.GetRole("Simple User")
                    };

                    if (!DatabaseWorkUnit.Instance.UserAccountRepository.Insert(newUser))
                        return "F";

                    //Create the external auth id
                    DatabaseWorkUnit.Instance.OAuthLoginRepository.Insert(userName, providerName, id);

                    //Make a new session for the user
                    AccountModels.Session newSession = DatabaseWorkUnit.Instance.SessionRepository.Login(userName);

                    //Create the cookies for the session
                    _myCookie.SetCookie("sessionId", newSession.SessionId.ToString(), DateTime.Now.AddMonths(6));
                    _myCookie.SetCookie("sessionKey", newSession.SessionKey, DateTime.Now.AddMonths(6));

                    return "K";
                }
            }
            catch 
            {
                return "F";
            }
        }

        /*The partialviews for login/register*/
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


        /*Prostii care vin sterse*/
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

    }
}
