using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using evoART.Filters;
using evoART.Models;

namespace evoART.Controllers
{
    public class HomeController : Controller
    {
        //[InitializeDatabase]
        public ActionResult Index()
        {
            using (var db = new DatabaseWorkUnit())
            {
                //db.InitializeDatabaseTables();
            }


            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}