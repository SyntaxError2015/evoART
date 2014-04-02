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
            if (true==false)
            {
                //Delete the like

                return "R";
            }
            else
            {
                //Add the like
                return "A";
            }
        }

    }
}