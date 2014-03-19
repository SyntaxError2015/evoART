using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using evoART.Models;
using evoART.Models.ViewModels;

namespace evoART.Controllers
{
    public class AccountController : Controller
    {

        public string Register()
        {
            return "hahahaa";
        }



        public string GetStatus()
        {
            return "Totul e OK la " + DateTime.Now.ToLongTimeString();
        }


        public string UpdateForm(string textBox1)
        {
            if (textBox1 != "Enter text")
            {
                return "Ai scris: \"" + textBox1.ToString() + "\" at " +
                    DateTime.Now.ToLongTimeString();
            }

            return String.Empty;
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model)
        {
            if (true)
            {
                return Redirect("/");
            }

            // If we got this far, something failed, redisplay form

            ModelState.AddModelError("", "The user name or password provided is incorrect.");
            return View(model);
        }
    }
}
