using System;
using System.Web.Mvc;
using BusinessTripApplication.Models;
using BusinessTripApplication.Repository;
using BusinessTripApplication.ViewModels;

namespace BusinessTripApplication.Controllers
{
    public class UserController : Controller
    {

        IUserRepository UserRepository;

        public UserController(IUserRepository repo)
        {
            UserRepository = repo;
        }

        public UserController()
        {
            UserRepository = new UserRepository();
        }

        // GET: User
        [HttpGet]
        public ActionResult Registration()
        {
            RegistrationViewModel model = new RegistrationViewModel();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Registration([Bind(Exclude = "IsEmailVerified,ActivationCode")] User user)
        {
            try
            {
                RegistrationViewModel model = new RegistrationViewModel(ModelState.IsValid, user,UserRepository);
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
            bool result = UserRepository.VerifyAccount(id);
            ViewBag.Status = result;

            if(!result)
                ViewBag.Message = "Invalid Request";
            
            return View();
        }


    }
}