using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web;
using System.Web.Mvc;
using evoART.DAL.UnitsOfWork;
using evoART.Models.DbModels;
using evoART.Models.ViewModels;

namespace evoART.Controllers
{
    public class AlbumsController : Controller
    {
        //
        // GET: /Albums/
        public ActionResult MyAlbums(int asPartial=0)
        {
            ViewBag.UserDetails = new AccountController().GetUserDetails();
            
            if (ViewBag.UserDetails != null)
            {
                PhotoModels.Album[] userAlbums = DatabaseWorkUnit.Instance.AlbumsRepository.GetAlbumsForUser(ViewBag.UserDetails.UserId);
                var model = new AlbumsModel()
                {
                    Albums = userAlbums
                };
                if (asPartial==1) return PartialView(model);
                else return View(model);
            }

            if (asPartial==1) return PartialView();
            else return View();
        }

        public ActionResult MyAlbums_p()
        {
            ViewBag.UserDetails = new AccountController().GetUserDetails();
            if (ViewBag.UserDetails != null)
            {
                PhotoModels.Album[] userAlbums = DatabaseWorkUnit.Instance.AlbumsRepository.GetAlbumsForUser(ViewBag.UserDetails.UserId);
                var model = new AlbumsModel()
                {
                    Albums = userAlbums
                };
                return PartialView("MyAlbums",model);
            }

            return PartialView("MyAlbums");
        }

        public string Create(AlbumsModel model)
        {
            var userDetails = new AccountController().GetUserDetails();

            if (model.NewAlbumName == null || model.NewAlbumName.Length < 3)
                return "N";

            return DatabaseWorkUnit.Instance.AlbumsRepository.Insert(
                new PhotoModels.Album() { AlbumName = model.NewAlbumName, AlbumDescription = model.NewAlbumDescription },
                userDetails.UserId)
                ? "K"
                : "F";
        }

        public ActionResult Albums(string id)
        {
            ViewBag.UserDetails = new AccountController().GetUserDetails();

            if (!DatabaseWorkUnit.Instance.UserAccountRepository.VerifyExists(id))
                return View("Albums");

            var userId = DatabaseWorkUnit.Instance.UserAccountRepository.GetUser(id).UserId;
            var userAlbums = DatabaseWorkUnit.Instance.AlbumsRepository.GetAlbumsForUser(userId);
            var model = new AlbumsModel()
            {
                Albums = userAlbums
            };
            
            return View("Albums",model);

        }

        public ActionResult Album(string id)
        {
            ViewBag.UserDetails = new AccountController().GetUserDetails();

            if (!DatabaseWorkUnit.Instance.UserAccountRepository.VerifyExists(id))
                return View("Album");

            var userId = DatabaseWorkUnit.Instance.UserAccountRepository.GetUser(id).UserId;
            var userAlbums = DatabaseWorkUnit.Instance.AlbumsRepository.GetAlbumsForUser(userId);
            var model = new AlbumsModel()
            {
                Albums = userAlbums
            };

            return View("Album", model);

        }
    }
}