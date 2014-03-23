using System;
using System.Linq;
using System.Web.Mvc;
using evoART.DAL.DbContexts;
using evoART.Special;

namespace evoART.Controllers
{
    public class HomeController : Controller
    {
        
        public ActionResult Index()
        {
            using (var db = new DatabaseContext())
            {
                db.UserAccounts.Count();
            }

            //new AccountController().SetCookie("test","bla",DateTime.Now.AddHours(5));

            var myCookie = new CookieHelper();
            myCookie.SetCookie("test", "bla", DateTime.Now.AddHours(10));

            ViewBag.UserDetails = new AccountController().GetUserDetails();

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [Authorize]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}