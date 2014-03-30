using System;
using System.Linq;
using System.Web.Mvc;
using evoART.DAL.DbContexts;
using evoART.DAL.UnitsOfWork;
using evoART.Models.DbModels;
using evoART.Models.ViewModels;
using evoART.Special;
using System.Collections.Generic;
using System.Web;
using System.IO;

namespace evoART.Controllers
{
    public class PhotosController : Controller
    {
        public ActionResult NewPhoto()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [HttpPost]
        public string Upload(HttpPostedFileBase file, string photoId)
        {
            //string photoId = new Guid().ToString();

            if (file != null && file.ContentLength > 0 && MySession.Current.UserDetails != null && MySession.Current.UserDetails.Role.RoleName=="Photographer")
            {
                // extract only the fielname
                var fileName = Path.GetFileName(file.FileName);


                // the destination
                var path = Path.Combine(Server.MapPath("~/Content/Temp"), photoId+".jpg");
                file.SaveAs(path);

                return photoId;
            }

            return "F";
        }

        public ActionResult CreateNewPhoto()
        {
            var userAlbums =
                DatabaseWorkUnit.Instance.AlbumsRepository.GetAlbumsForUser(MySession.Current.UserDetails.UserId);
            var photo = new PhotoModels.Photo()
            {
                Album = userAlbums[0],
                ContentTag = DatabaseWorkUnit.Instance.ContentTagsRepository.GetContentTag("SFW")
            };


            var model = new PhotosModel {PhotoId = DatabaseWorkUnit.Instance.PhotosRepository.Insert(photo).ToString()};
            var t = userAlbums.Select(r => new SelectListItem { Text = r.AlbumName, Value = r.AlbumId.ToString() }).ToList();
            ViewData["Albums"] = t;

            return PartialView("NewPhoto", photo);
        }

        public string SaveNewPhoto(PhotosModel model)
        {
            DatabaseWorkUnit.Instance.PhotosRepository.Update(new PhotoModels.Photo()
            {
                Album=DatabaseWorkUnit.Instance.AlbumsRepository.
            })
            return "F";
        }

    }
}