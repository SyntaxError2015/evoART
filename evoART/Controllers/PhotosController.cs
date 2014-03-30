using System;
using System.Drawing;
using System.Drawing.Imaging;
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
using PhotoManipulator;

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

            var y = DatabaseWorkUnit.Instance.ContentTagsRepository.GetAllContentTags().Select(r => new SelectListItem { Text = r.ContentTagName, Value = r.ContentTagId.ToString() }).ToList();
            ViewData["ContentTags"] = y;

            return PartialView("NewPhoto", model);
        }

        public string SaveNewPhoto(PhotosModel model)
        {
            Image img = Image.FromFile(Path.Combine(Server.MapPath("~/Content/Temp"), model.PhotoId + ".jpg"));

            ImageResizer.ResizeImage(img).Save(Path.Combine(Server.MapPath("~/Content/Photos"), model.PhotoId + ".jpg"), ImageFormat.Jpeg);
            ImageResizer.CreateThumbnail(img).Save(Path.Combine(Server.MapPath("~/Content/Thumbnails"), model.PhotoId + ".jpg"), ImageFormat.Jpeg);
            ImageResizer.CreateHexagonImage(img).Save(Path.Combine(Server.MapPath("~/Content/Hexagons"), model.PhotoId + ".png"), ImageFormat.Png);

            var photo = DatabaseWorkUnit.Instance.PhotosRepository.GetPhoto(new Guid(model.PhotoId));

            photo.Album = DatabaseWorkUnit.Instance.AlbumsRepository.GetAlbum(new Guid(model.Album));
            photo.ContentTag = DatabaseWorkUnit.Instance.ContentTagsRepository.GetContentTag(new Guid(model.ContentTag));
            photo.PhotoName = model.NewPhotoName;
            photo.PhotoDescription = model.NewPhotoDescription;

            return DatabaseWorkUnit.Instance.PhotosRepository.Update(photo)
                ? "K"
                : "F";
        }

    }
}