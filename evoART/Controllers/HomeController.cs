using System.Web.Mvc;
using evoART.DAL.UnitsOfWork;
using evoART.Special;
using evoART.Models.ViewModels;

namespace evoART.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Index(int asPartial = 0)
        {

            var model = new IndexModel()
            {
                TopPhotos = DatabaseWorkUnit.Instance.PhotosRepository.GetPopularPhotos(0, 19)
            };

            if (asPartial == 1) return PartialView(model);
            
            if (MySession.Current.UserDetails == null)
                MySession.Current.UserDetails = new AccountController().GetUserDetails();
            
            return View(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }



        [Authorize]
        public ActionResult Contact(int asPartial = 0)
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [ActionName("MyAlbums")]
        public ActionResult MyAlbums(string id)
        {
            ViewBag.UserDetails = new AccountController().GetUserDetails();
            ViewBag.Message = "Your application description page.";
            ViewBag.test = id;
            return View();
        }
    }
}