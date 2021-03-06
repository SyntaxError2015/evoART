﻿using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Web.Mvc;
using evoART.DAL.DbContexts;
using evoART.DAL.UnitsOfWork;
using evoART.Models.DbModels;
using evoART.Special;
using evoART.Models.ViewModels;
using System.Web;
using System.IO;
using PhotoManipulator;

namespace evoART.Controllers
{
    public class UsersController : Controller
    {
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

        public new ActionResult Profile(string id, int asPartial = 0)
        {
            if (asPartial == 0 && MySession.Current.UserDetails == null)
                MySession.Current.UserDetails = new AccountController().GetUserDetails();

            var model = new ProfileModel()
            {
               
            };

            if (DatabaseWorkUnit.Instance.UserAccountRepository.VerifyExists(id))
            {
                model.UserDetails = DatabaseWorkUnit.Instance.UserAccountRepository.GetUser(id);
                if (MySession.Current.UserDetails != null &&
                    model.UserDetails.UserName == MySession.Current.UserDetails.UserName)
                    model.MyProfile = true;
                else model.MyProfile = false;

                model.FacebookLinked = MySession.Current.UserDetails != null && DatabaseWorkUnit.Instance.OAuthLoginRepository.VerifyExists("Facebook", MySession.Current.UserDetails.UserId);

                if (MySession.Current.UserDetails != null)
                {
                    var t = DatabaseWorkUnit.Instance.RoleRepository.GetRoleNames().Select(r => new SelectListItem { Text = r, Value = r, Selected = r==MySession.Current.UserDetails.Role.RoleName}).ToList();
                    ViewData["Roles"] = t;
                } 
            }

            if (asPartial == 1) return PartialView(model);
            else
            {
                return View(model);
            }
        }

        public string EditProfile(ProfileModel model)
        {
            if (MySession.Current.UserDetails == null ||
                MySession.Current.UserDetails.UserId != model.UserDetails.UserId)
                return "F";

            var myProfile = DatabaseWorkUnit.Instance.UserAccountRepository.GetUser(MySession.Current.UserDetails.UserName);

            if ((model.UserDetails.UserName==null || model.UserDetails.UserName.Length < 4 || model.UserDetails.UserName.Length > 28 || !Char.IsLetter(model.UserDetails.UserName[0]) || DatabaseWorkUnit.Instance.UserAccountRepository.VerifyExists(model.UserDetails.UserName))&&myProfile.UserName!=model.UserDetails.UserName)
                return "U";

            if (!IsValidEmail(model.UserDetails.Email))
                return "E";

            myProfile.UserName = model.UserDetails.UserName;
            myProfile.FistName = model.UserDetails.FistName;
            myProfile.LastName = model.UserDetails.LastName;
            //myProfile.BirthDate = model.UserDetails.BirthDate;
            myProfile.Email = model.UserDetails.Email;
            myProfile.PhoneNumber = model.UserDetails.PhoneNumber;
            myProfile.Role = DatabaseWorkUnit.Instance.RoleRepository.GetRole(model.UserDetails.Role.RoleName);
            if (model.NewPassword!=null && model.NewPasswordRepeat != null && model.NewPassword.Length >= 6 && model.NewPassword == model.NewPasswordRepeat)
                myProfile.Password = Special.TokenGenerator.EncryptMD5(model.NewPassword);
            myProfile.UserName = model.UserDetails.UserName;

            return DatabaseWorkUnit.Instance.UserAccountRepository.Update(myProfile) ? "K" : "F";
        }

        [HttpPost]
        public ActionResult UploadProfilePic(HttpPostedFileBase file, string test = "")
        {
            if (file != null && file.ContentLength > 0 && MySession.Current.UserDetails != null )
            {
                var path = Path.Combine(Server.MapPath("~/Content/ProfilePictures"), MySession.Current.UserDetails.UserId + ".jpg");
                //file.SaveAs(path);

                Image image = Image.FromStream(file.InputStream);

                ImageResizer.ResizeImage(image, new Size(200, 200)).Save(path, ImageFormat.Jpeg);

                return Redirect("/Users/Profile/" + MySession.Current.UserDetails.UserName);
            }

            return Redirect("/Home/Index/");
        }
      
    }
}