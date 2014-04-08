using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Web.Mvc;
using evoART.DAL.UnitsOfWork;
using evoART.Models.DbModels;
using evoART.Models.ViewModels;
using evoART.Special;
using System.Web;
using System.IO;
using PhotoManipulator;

namespace evoART.Controllers
{
    public class PhotosController : Controller
    {
        public ActionResult NewPhoto()
        {
            return View();
        }

        [HttpPost]
        public string Upload(HttpPostedFileBase file, string photoId)
        {
            if (file != null && file.ContentLength > 0 && MySession.Current.UserDetails != null && MySession.Current.UserDetails.Role.RoleName == "Photographer")
            {
                var path = Path.Combine(Server.MapPath("~/Content/Temp"), photoId + ".jpg");
                file.SaveAs(path);

                return photoId;
            }

            return "F";
        }

        public ActionResult CreateNewPhoto()
        {
            if (MySession.Current.UserDetails == null)
                return PartialView("NewPhoto");

            var userAlbums =
                DatabaseWorkUnit.Instance.AlbumsRepository.GetAlbumsForUser(MySession.Current.UserDetails.UserId, MySession.Current.UserDetails.UserId);
            var photo = new PhotoModels.Photo
            {
                Album = userAlbums[0],
                ContentTag = DatabaseWorkUnit.Instance.ContentTagsRepository.GetContentTag("SFW")
            };

            var model = new PhotosModel { PhotoId = DatabaseWorkUnit.Instance.PhotosRepository.Insert(photo).ToString() };
            var t = userAlbums.Select(r => new SelectListItem { Text = r.AlbumName, Value = r.AlbumId.ToString() }).ToList();
            ViewData["Albums"] = t;

            var y = DatabaseWorkUnit.Instance.ContentTagsRepository.GetAllContentTags().Select(r => new SelectListItem { Text = r.ContentTagName, Value = r.ContentTagId.ToString() }).ToList();
            ViewData["ContentTags"] = y;

            return PartialView("NewPhoto", model);
        }

        public string SaveNewPhoto(PhotosModel model)
        {
            var photo = DatabaseWorkUnit.Instance.PhotosRepository.GetPhoto(new Guid(model.PhotoId));

            if (MySession.Current.UserDetails == null ||
                MySession.Current.UserDetails.UserId != photo.Album.UserAccount.UserId)
                return "F";

            Image img = Image.FromFile(Path.Combine(Server.MapPath("~/Content/Temp"), model.PhotoId + ".jpg"));

            ImageResizer.ResizeImage(img).Save(Path.Combine(Server.MapPath("~/Content/Photos"), model.PhotoId + ".jpg"), ImageFormat.Jpeg);
            ImageResizer.CreateThumbnail(img).Save(Path.Combine(Server.MapPath("~/Content/Thumbnails"), model.PhotoId + ".jpg"), ImageFormat.Jpeg);
            ImageResizer.CreateHexagonImage(img).Save(Path.Combine(Server.MapPath("~/Content/Hexagons"), model.PhotoId + ".png"), ImageFormat.Png);

            img.Dispose();
            try
            {
                System.IO.File.Delete(Path.Combine(Server.MapPath("~/Content/Temp"), model.PhotoId + ".jpg"));
            }
            catch { }

            photo.Album = DatabaseWorkUnit.Instance.AlbumsRepository.GetAlbum(new Guid(model.Album));
            photo.ContentTag = DatabaseWorkUnit.Instance.ContentTagsRepository.GetContentTag(new Guid(model.ContentTag));
            photo.PhotoName = model.NewPhotoName;
            photo.PhotoDescription = model.NewPhotoDescription;

            return DatabaseWorkUnit.Instance.PhotosRepository.Update(photo)
                ? "K"
                : "F";
        }

        public string EditPhoto(PhotoModel model)
        {
            var photo = DatabaseWorkUnit.Instance.PhotosRepository.GetPhoto(model.Photo.PhotoId);

            if (MySession.Current.UserDetails == null ||
               MySession.Current.UserDetails.UserId != photo.Album.UserAccount.UserId)
                return "F";

            photo.PhotoName = model.Photo.PhotoName;
            photo.PhotoDescription = model.Photo.PhotoDescription;
            photo.Album = DatabaseWorkUnit.Instance.AlbumsRepository.GetAlbum(new Guid(model.NewAlbum));
            photo.ContentTag =
                DatabaseWorkUnit.Instance.ContentTagsRepository.GetContentTag(new Guid(model.NewContentTag));

            return DatabaseWorkUnit.Instance.PhotosRepository.Update(photo) ? "K" : "F";
        }

        public string DeletePhoto(string id)
        {
            if (MySession.Current.UserDetails == null || MySession.Current.UserDetails.UserId !=
            DatabaseWorkUnit.Instance.PhotosRepository.GetPhoto(new Guid(id)).Album.UserAccount.UserId)
                return "F";

            try
            {
                System.IO.File.Delete(Path.Combine(Server.MapPath("~/Content/Temp"), id + ".jpg"));
            }
            catch { }

            try
            {
                System.IO.File.Delete(Path.Combine(Server.MapPath("~/Content/Thumbnails"), id + ".jpg"));
            }
            catch { }

            try
            {
                System.IO.File.Delete(Path.Combine(Server.MapPath("~/Content/Hexagons"), id + ".png"));
            }
            catch { }

            try
            {
                System.IO.File.Delete(Path.Combine(Server.MapPath("~/Content/Photos"), id + ".jpg"));
            }
            catch { }

            return DatabaseWorkUnit.Instance.PhotosRepository.Delete(new Guid(id)) ? "K" : "F";
        }

        public ActionResult Photo(string id, int asPartial = 0)
        {
            if (asPartial == 0 && MySession.Current.UserDetails == null)
                MySession.Current.UserDetails = new AccountController().GetUserDetails();

            try
            {
                if (id == null || !DatabaseWorkUnit.Instance.PhotosRepository.VerifyExists(new Guid(id)))
                    if (asPartial == 1) return PartialView("photo");
                    else return View("Photo");
            }
            catch
            {
                if (asPartial == 1) return PartialView("photo");

                return View("Photo");
            }

            //Increment views number 
            DatabaseWorkUnit.Instance.PhotosRepository.IncrementViews(new Guid(id));

            var photo = DatabaseWorkUnit.Instance.PhotosRepository.GetPhoto(new Guid(id));
            var model = new PhotoModel
            {
                Photo = photo,
                MyPhoto = MySession.Current.UserDetails != null && (photo.Album.UserAccount.UserId == MySession.Current.UserDetails.UserId ? true : false),

                Comments = DatabaseWorkUnit.Instance.CommentsRepository.GetCommentsForPhoto(photo.PhotoId),
                HasLiked = MySession.Current.UserDetails != null && DatabaseWorkUnit.Instance.LikesRepository.UserHasLikedPhoto(MySession.Current.UserDetails.UserId, photo.PhotoId),
                NextPhotoId = DatabaseWorkUnit.Instance.PhotosRepository.GetNextPhoto(photo),
                PreviousPhotoId = DatabaseWorkUnit.Instance.PhotosRepository.GetPreviousPhoto(photo)
            };

            //Prepare some things if it is my photo
            if (model.MyPhoto)
            {
                ViewData["Albums"] = DatabaseWorkUnit.Instance.AlbumsRepository
                .GetAlbumsForUser(MySession.Current.UserDetails.UserId, MySession.Current.UserDetails != null ? MySession.Current.UserDetails.UserId : Guid.Empty)
                .Select(r => new SelectListItem { Text = r.AlbumName, Value = r.AlbumId.ToString(), Selected = r.AlbumId == photo.Album.AlbumId }).ToList();
                ViewData["ContentTags"] = DatabaseWorkUnit.Instance.ContentTagsRepository.GetAllContentTags().Select(r => new SelectListItem { Text = r.ContentTagName, Value = r.ContentTagId.ToString(), Selected = r.ContentTagId == photo.ContentTag.ContentTagId }).ToList();
            }

            if (asPartial == 1) return PartialView("Photo", model);

            return View("Photo", model);
        }

    }
}