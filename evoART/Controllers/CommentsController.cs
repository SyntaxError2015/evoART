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
        [HttpPost]
        public string AddComment(string photoId, string comment)
        {
            if (MySession.Current.UserDetails == null || photoId==null || comment==null)
                return "F";

            return "K";
        }

        public string DeleteComment(string id)
        {
            if (MySession.Current.UserDetails == null || id == null)
                return "F";

            //Check if it is my own comment

            
            return "K";
        }
    }
}