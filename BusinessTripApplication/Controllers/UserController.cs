using System;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web.Mvc;
using BusinessTripApplication.Models;
using BusinessTripApplication.Repository;

namespace BusinessTripApplication.Controllers
{
    public class UserController : Controller
    {
        private UserRepository UserRepository = new UserRepository();
        // GET: User
        public ActionResult Registration(int id = 0)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Registration([Bind(Exclude = "IsEmailVerified,ActivationCode")] User user)
        {
            string returnedMessage = "";
            bool registrationStatus = false;

            if (ModelState.IsValid)
            {
                var isExist = UserRepository.EmailExists(user.Email);
                if (isExist)
                {
                    ModelState.AddModelError("EmailExist", "Email already exist");
                    return View(user);
                }

                User addedUser = UserRepository.Add(user);

                //Send Email to User
                SendVerificationLinkEmail(addedUser.Email, addedUser.ActivationCode.ToString());
                returnedMessage = "Registration successfully done. Account activation link " +
                            " has been sent to your email id:" + addedUser.Email;
                registrationStatus = true;

            }
            else
            {
                returnedMessage = "Invalid Request";
            }

            ViewBag.Message = returnedMessage;
            ViewBag.Status = registrationStatus;
            return View(user);
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