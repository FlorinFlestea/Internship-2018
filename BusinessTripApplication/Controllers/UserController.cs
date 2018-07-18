using System;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web.Helpers;
using System.Web.Mvc;
using BusinessTripApplication.Models;
using Microsoft.Ajax.Utilities;

namespace BusinessTripApplication.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Registration(int id = 0)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Registration([Bind(Exclude = "IsEmailVerified,ActivationCode")] User user)
        {
            if (ModelState.IsValid)
            {
                var isExist = EmailExists(user.Email);
                if (isExist)
                {
                    ViewBag.Message = "Email already in the database !";
                    ViewBag.Status = false;
                    return View(user);
                }

                user.ActivationCode = Guid.NewGuid();
                user.Password = Crypto.Hash(user.Password);
                user.IsEmailVerified = false;

                using (var dc = new UserContext())
                {
                    dc.Users.Add(user);
                    dc.SaveChanges();

                    //Send Email to User
                    SendVerificationLinkEmail(user.Email, user.ActivationCode.ToString());
                    ViewBag.Message = "Registration successfully done. Account activation link " +
                              " has been sent to your email id:" + user.Email;
                    ViewBag.Status = true;
                    return View(user);
                }
            }
            else
            {
                ViewBag.Message = "Invalid request";
                ViewBag.Status = false;
                return View(user);
            }

            ViewBag.Message = "";
            ViewBag.Status = false;
            return View(user);
        }

        [NonAction]
        public bool EmailExists(string email)
        {
            using (var db = new UserContext())
            {
                var exists = db.Users.FirstOrDefault(a => a.Email == email);
                return exists != null;
            }
        }

        [NonAction]
        public void SendVerificationLinkEmail(string emailID, string activationCode)
        {
            var verifyUrl = "/User/VerifyAccount/" + activationCode;
            var link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, verifyUrl);

            var fromEmail = new MailAddress("businesstripapplication@gmail.com", "Registration");
            var toEmail = new MailAddress(emailID);
            var fromEmailPassword = "ParolaTest1234"; // Replace with actual password
            string subject = "Your account is successfully created!";

            string body = "<br/><br/>We are excited to tell you that your account is" +
                          " successfully created. Please click on the below link to verify your account" +
                          " <br/><br/><a href='" + link + "'>" + link + "</a> ";

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromEmail.Address, fromEmailPassword)
            };

            using (var message = new MailMessage(fromEmail, toEmail)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            })
                smtp.Send(message);
        }


    }
}