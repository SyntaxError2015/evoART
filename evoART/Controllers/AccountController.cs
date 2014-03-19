using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using evoART.Models;

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

        public string Login(evoART.Models.LoginModel.Login model)
        {

            if (model.UserName != "user") return "U";

            if (model.Password != "pass") return "P";

            return "K";
        }
    }
}
