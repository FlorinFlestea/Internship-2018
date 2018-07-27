using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
                return View("Dashboard");
            }
            return View();
        }

        public ActionResult About()
        {
            if (!Request.IsAuthenticated)
            {
                return RedirectToAction("Login", "User"); ;
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