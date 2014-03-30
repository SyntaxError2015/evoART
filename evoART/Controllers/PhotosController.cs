using System;
using System.Linq;
using System.Web.Mvc;
using evoART.DAL.DbContexts;
using evoART.Models.DbModels;
using evoART.Special;
using System.Collections.Generic;
using System.Web;
using System.IO;

namespace evoART.Controllers
{
    public class PhotosController : Controller
    {
        public ActionResult AddPhoto()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public string Upload(HttpPostedFileBase file)
        {
            
                    if (file != null && file.ContentLength > 0)
                    {
                        // extract only the fielname
                        var fileName = Path.GetFileName(file.FileName);
                        // TODO: need to define destination
                        var path = Path.Combine(Server.MapPath("~/Content/Photos"), fileName);
                        file.SaveAs(path);

                        return path;
                    }
                
            return "false";
        }



    }
}