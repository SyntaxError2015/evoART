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
    public class CommentsController : Controller
    {
        /// <summary>
        /// Add a new comment for the photo
        /// </summary>
        /// <param name="photoId">The photo id</param>
        /// <param name="comment">The comment to be added</param>
        /// <returns></returns>
        [HttpPost]
        public string AddComment(string photoId, string comment)
        {
            if (MySession.Current.UserDetails == null || photoId==null || comment==null || comment.Length<2)
                return "F";

            return DatabaseWorkUnit.Instance.CommentsRepository.Insert(new Guid(photoId),
                MySession.Current.UserDetails.UserId,
                comment)
                ? "K"
                : "F";
        }

        /// <summary>
        /// Delete a comment
        /// </summary>
        /// <param name="id">The id of the comment to be deleted</param>
        /// <returns></returns>
        public string DeleteComment(string id)
        {
            if (MySession.Current.UserDetails == null || id == null)
                return "F";

            //Check if it is my own comment
            if (DatabaseWorkUnit.Instance.CommentsRepository.GetComment(new Guid(id)).UserAccount.UserId !=
                MySession.Current.UserDetails.UserId)
                return "F";

            return DatabaseWorkUnit.Instance.CommentsRepository.Delete(new Guid(id)) ? "K" : "F";
        }
    }
}