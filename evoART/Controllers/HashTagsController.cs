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
    public class HashTagsController : Controller
    {
        //[HttpPost]
        public ActionResult SuggestedHashTags(string photoId, string name)
        {
            try
            {
                var photoHashTags = DatabaseWorkUnit.Instance.PhotosRepository.GetPhoto(new Guid(photoId)).HashTags;

                var hashTags = DatabaseWorkUnit.Instance.HashTagsRepository.GetPopularHashTags(6, name);
                  
                if (photoHashTags!=null)
                    hashTags=hashTags.Except(photoHashTags).ToArray();
                        
                var model = new HashTagsModel()
                {
                    PhotoId = photoId,
                    HashTags = hashTags
                };

                return PartialView("HashTagsLabels", model);
            }
            catch (Exception)
            {
                return PartialView("HashTagsLabels");
            }
        }

        [HttpPost]
        public string AddHashTag(string photoId, string hashName)
        {
            var photo = DatabaseWorkUnit.Instance.PhotosRepository.GetPhoto(new Guid(photoId));

            if (MySession.Current.UserDetails == null ||
               MySession.Current.UserDetails.UserId != photo.Album.UserAccount.UserId)
                return "F";

            hashName=hashName.Replace("\n", "");
            hashName=hashName.Replace(" ", "");

            return DatabaseWorkUnit.Instance.HashTagsRepository.Insert(hashName, photo) ? "K" : "F";
        }

        [HttpPost]
        public string DeleteHashTag(string photoId, string hashName)
        {
            var photo = DatabaseWorkUnit.Instance.PhotosRepository.GetPhoto(new Guid(photoId));

            if (MySession.Current.UserDetails == null ||
               MySession.Current.UserDetails.UserId != photo.Album.UserAccount.UserId)
                return "F";

            return DatabaseWorkUnit.Instance.HashTagsRepository.DeleteHashTagForPhoto(hashName, photo) ? "K" : "F";
        }
    }
}