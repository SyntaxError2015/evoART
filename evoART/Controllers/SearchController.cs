using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using evoART.DAL.DbContexts;
using evoART.DAL.UnitsOfWork;
using evoART.Models.DbModels;
using evoART.Special;
using evoART.Models.ViewModels;

namespace evoART.Controllers
{
    public class SearchController : Controller
    {

        public ActionResult HashTag(string id, int asPartial = 0)
        {
            var model = new SearchModel();
            var searchTags = id.Split(' ');

            var photos = new List<PhotoModels.Photo>();

            foreach (var searchTag in searchTags)
            {
                try
                {
                    photos.AddRange(DatabaseWorkUnit.Instance.PhotosRepository.GetPhotosForHashTag(searchTag, 0, 30));
                }
                catch
                {
                    
                }
            }


            try
            {
                //photos = photos.Distinct().ToList();
                model.Photos = photos.ToArray();
            }
            catch
            {
                if (asPartial == 1) return PartialView("Results");
                else
                {
                    if (MySession.Current.UserDetails == null)
                        MySession.Current.UserDetails = new AccountController().GetUserDetails();
                    return View("Results");
                }
            }

           

            if (asPartial == 1) return PartialView("Results", model);
            else
            {
                if (MySession.Current.UserDetails == null)
                    MySession.Current.UserDetails = new AccountController().GetUserDetails();
                return View("Results", model);
            }
        }

       
    }
}