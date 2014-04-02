using System;
using System.Linq;
using System.Web.Mvc;
using evoART.DAL.DbContexts;
using evoART.DAL.UnitsOfWork;
using evoART.Models.DbModels;
using evoART.Special;
using evoART.Models.ViewModels;

namespace evoART.Controllers
{
    public class LikesController : Controller
    {
        [HttpPost]
        public string ToggleLike(string photoId)
        {
            if (MySession.Current.UserDetails == null || photoId==null)
                return "F";

            //Check if has liked
            if (DatabaseWorkUnit.Instance.LikesRepository.UserHasLikedPhoto(MySession.Current.UserDetails.UserId,new Guid(photoId)))
            {
                //Delete the like

                return DatabaseWorkUnit.Instance.LikesRepository.Delete(new Guid(photoId),
                    MySession.Current.UserDetails.UserId)
                    ? "R"
                    : "F";
            }
            else
            {
                //Add the like
                return DatabaseWorkUnit.Instance.LikesRepository.Insert(new Guid(photoId),
                    MySession.Current.UserDetails.UserId)
                    ? "A"
                    : "F";
            }
        }

    }
}