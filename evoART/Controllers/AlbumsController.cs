using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web;
using System.Web.Mvc;
using evoART.DAL.UnitsOfWork;
using evoART.Models.DbModels;
using evoART.Models.ViewModels;
using evoART.Special;

namespace evoART.Controllers
{
    public class AlbumsController : Controller
    {
        //
        // GET: /Albums/
        public ActionResult MyAlbums(int asPartial = 0)
        {
            if (asPartial == 0 && MySession.Current.UserDetails == null)
                MySession.Current.UserDetails = new AccountController().GetUserDetails();

            if (MySession.Current.UserDetails != null && MySession.Current.UserDetails.Role.RoleName == DatabaseWorkUnit.Instance.RoleRepository.GetRole("Photographer").RoleName)
            {
                PhotoModels.Album[] userAlbums = DatabaseWorkUnit.Instance.AlbumsRepository.GetAlbumsForUser(MySession.Current.UserDetails.UserId);
                var model = new AlbumsModel()
                {
                    Albums = userAlbums
                };
                if (asPartial == 1) return PartialView(model);
                else return View(model);
            }

            if (asPartial == 1) return PartialView();
            else return View();
        }




        public ActionResult Albums(string id, int asPartial = 0)
        {
            if (asPartial == 0 && MySession.Current.UserDetails == null)
                MySession.Current.UserDetails = new AccountController().GetUserDetails();

            if (!DatabaseWorkUnit.Instance.UserAccountRepository.VerifyExists(id) || DatabaseWorkUnit.Instance.UserAccountRepository.GetUser(id).Role.RoleName != DatabaseWorkUnit.Instance.RoleRepository.GetRole("Photographer").RoleName)
                return View("Albums");

            var userId = DatabaseWorkUnit.Instance.UserAccountRepository.GetUser(id).UserId;
            var userAlbums = DatabaseWorkUnit.Instance.AlbumsRepository.GetAlbumsForUser(userId);
            var model = new AlbumsModel()
            {
                Albums = userAlbums,
                AlbumsUser = id
            };

            if (asPartial == 1) return PartialView("Albums", model);
            else return View("Albums", model);
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
                Albums = userAlbums,
                AlbumsUser = id
            };

            return View("Album", model);

        }

        /// <summary>
        /// Create a new album
        /// </summary>
        /// <param name="model">The model for the new album</param>
        /// <returns></returns>
        public string Create(AlbumsModel model)
        {
            if (model.NewAlbumName == null || model.NewAlbumName.Length < 3)
                return "N";

            return MySession.Current.UserDetails != null && DatabaseWorkUnit.Instance.AlbumsRepository.Insert(
                new PhotoModels.Album() { AlbumName = model.NewAlbumName, AlbumDescription = model.NewAlbumDescription },
                MySession.Current.UserDetails.UserId)
                ? "K"
                : "F";
        }

        /// <summary>
        /// Delete an album
        /// </summary>
        /// <param name="id">The id of the album to be deleted</param>
        /// <returns></returns>
        public string Delete(string id)
        {
            if (MySession.Current.UserDetails.UserId !=
                DatabaseWorkUnit.Instance.AlbumsRepository.GetAlbum(new Guid(id)).UserAccount.UserId)
                return "F";
            return MySession.Current.UserDetails != null && DatabaseWorkUnit.Instance.AlbumsRepository.Delete(new Guid(id))
                ? "K"
                : "F";
        }
    }
}