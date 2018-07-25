using System;
using System.Web.Mvc;
using BusinessTripApplication.Models;
using BusinessTripApplication.Repository;
using BusinessTripApplication.ViewModels;

namespace BusinessTripApplication.Controllers
{
    [RequireHttps]
    public class UserController : Controller
    {

        IUserService UserService;
        private static readonly log4net.ILog Logger
       = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public UserController(IUserService repo)
        {
            UserService = repo;
        }

        public UserController()
        {
            UserService = new UserService(new UserRepository());
        }

        // GET: User
        [HttpGet]
        public ActionResult Registration()
        {
            RegistrationViewModel model = new RegistrationViewModel();
            return View(model);
        }

        public ActionResult LogIn(int id = 0)
        {
            return View();
        }
        public ActionResult Dashboard(int id = 0)
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Registration([Bind(Exclude = "IsEmailVerified,ActivationCode")] User user)
        {
            try
            {
                var model = new RegistrationViewModel(ModelState.IsValid, user,UserService);
                return View(model);
            }
            catch (Exception e)
            {
                Logger.Info(e.Message);
                return RedirectToRoute("~/Shared/Error");
            }
            
        }

        [HttpGet]
        public ActionResult VerifyAccount(string id)
        {
            bool result = UserService.VerifyAccount(id);
            ViewBag.Status = result;

            if(!result)
                ViewBag.Message = "Invalid Request";
            
            return View();
        }


    }
}