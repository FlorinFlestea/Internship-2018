using System;
using System.Web.Mvc;
using System.Web.Security;
using BusinessTripApplication.Models;
using BusinessTripApplication.Repository;
using BusinessTripApplication.ViewModels;

namespace BusinessTripApplication.Controllers
{
    [RequireHttps]
    public class UserController : Controller
    {

        IUserService UserService;

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

        [HttpGet]
        public ActionResult LogIn()
        {
            LogInViewModel model = new LogInViewModel();
            return View(model);
        }

        [Authorize]
        public ActionResult Dashboard()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogIn(string Email, string Password,bool RememberMe)
        {
            try
            {
                int response = -1;
                var model = new LogInViewModel(ModelState.IsValid, Email, Password, RememberMe, UserService, out response);
                if (response == 1)
                {
                    Response.Cookies.Add(model.cookie);
                    return RedirectToAction("Dashboard");//we want to load a new page with new url, not just rendering the view
                    //return View("Dashboard");
                }
                return View(model);
            }
            catch (Exception e)
            {
                return RedirectToRoute("~/Shared/Error");
            }
        }

        [Authorize]
        [HttpPost]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "User");
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