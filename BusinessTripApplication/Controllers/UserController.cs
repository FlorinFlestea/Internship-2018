using System;
using System.Web.Mvc;
using System.Web.Security;
using BusinessTripModels;
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
            if (Request.IsAuthenticated)
            {
                return RedirectToAction("PermissionDenied");
            }
            RegistrationViewModel model = new RegistrationViewModel();
            return View(model);
        }

        [HttpGet]
        public ActionResult LogIn()
        {
            if (Request.IsAuthenticated)
            {
                return RedirectToAction("PermissionDenied");
            }
            LogInViewModel model = new LogInViewModel();
            return View(model);
        }

        [Authorize]
        public ActionResult Dashboard()
        {
            return View();
        }

        [Authorize]
        public ActionResult PermissionDenied()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogIn(string email, string password, bool rememberMe)
        {
            try
            {
                var model = new LogInViewModel(ModelState.IsValid, email, password, rememberMe, UserService, out var response);
                if (response == 1)
                {
                    Response.Cookies.Add(model.Cookie);
                    return RedirectToAction("Dashboard");//we want to load a new page with new url, not just rendering the view
                }
                return View(model);
            }
            catch (System.Exception e)
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
                var model = new RegistrationViewModel(ModelState.IsValid, user, UserService);
                return View(model);
            }
            catch (System.Exception e)
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

            if (!result)
                ViewBag.Message = "Invalid Request";

            else
            {
                Guid guid = new Guid(id);
                LogInViewModel loginVM = new LogInViewModel(UserService, guid);
                Response.Cookies.Add(loginVM.Cookie);
            }
            return View();
        }


    }
}
