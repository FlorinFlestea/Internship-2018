using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Registration.Models;
namespace Registration.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Registration(int id=0)
        {
            User userModel = new User("Alex Cernov","cernovalex1@gmail.com","test123");
            using (UserContext db = new UserContext())
            {
                db.Users.Add(userModel);
                db.SaveChanges();
            }
            return View(userModel);
        }
    }
}