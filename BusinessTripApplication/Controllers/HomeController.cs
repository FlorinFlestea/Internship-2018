using System.Web.Mvc;

namespace BusinessTripApplication.Controllers
{
    [RequireHttps]
    public class HomeController : Controller
    {

        public ActionResult Index()
        {
            if (Request.IsAuthenticated)
            {
                return RedirectToAction("Index", "Trips");
            }
            return RedirectToAction("Login", "User");
        }

        public ActionResult About()
        {
            if (!Request.IsAuthenticated)
            {
                return RedirectToAction("Login", "User");
            }
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            if (!Request.IsAuthenticated)
            {
                return RedirectToAction("Login", "User"); ;
            }
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}