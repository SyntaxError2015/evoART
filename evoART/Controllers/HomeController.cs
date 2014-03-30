using System;
using System.Linq;
using System.Web.Mvc;
using evoART.DAL.DbContexts;
using evoART.Models.DbModels;
using evoART.Special;

namespace evoART.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Index(int asPartial = 0)
        {
            using (var db = new DatabaseContext())
            {
                db.UserAccounts.Count();
            }

            if (asPartial == 1) return PartialView();
            else
            {
                if (MySession.Current.UserDetails == null)
                    MySession.Current.UserDetails = new AccountController().GetUserDetails();
                return View();
            }
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }



        [Authorize]
        public ActionResult Contact(int asPartial = 0)
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [ActionName("MyAlbums")]
        public ActionResult MyAlbums(string id)
        {
            ViewBag.UserDetails = new AccountController().GetUserDetails();
            ViewBag.Message = "Your application description page.";
            ViewBag.test = id;
            return View();
        }
    }
}