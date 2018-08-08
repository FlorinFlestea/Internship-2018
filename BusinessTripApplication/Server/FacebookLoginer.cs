using System;
using System.Web.Helpers;
using System.Web.Security;
using BusinessTripApplication.Repository;
using BusinessTripModels.Models;
using Facebook;

namespace BusinessTripApplication.Server
{
    public class FacebookLoginer: IFacebookLoginer
    {
        public readonly FacebookClient FbClient = new FacebookClient();
        public String AppId = "1937273663237215";
        public String AppSecret = "66e4c89da64be8e1d96e29bfec738324";
        public Uri LoginUrl { get; set; }
        private Uri RedirectUri;

        public FacebookLoginer(Uri redirectUri)
        {
            RedirectUri = redirectUri;
            LoginUrl = FbClient.GetLoginUrl(SetLoginUrl(redirectUri));
        }

        public object SetLoginUrl(Uri redirectUri)
        {
            var loginUrl=new
            {
                client_id = AppId,
                client_secret = AppSecret,
                redirect_uri = redirectUri.AbsoluteUri,
                response_type = "code",
                scope = "email" // Add other permissions as needed
            };
            return loginUrl;
        }

        public void Response(IUserService service,dynamic result)
        {
            var accessToken = result.access_token;
            FbClient.AccessToken = accessToken;
           
            dynamic me = FbClient.Get("me?fields=first_name,middle_name,last_name,id,email");
            string email = me.email;
            string firstname = me.first_name;
            string middlename = me.middle_name;
            string lastname = me.last_name;
            RegisterUserIfNotPresent(service, firstname, email, accessToken);
            SetCookie(email);
        }

        private void SetCookie(string email)
        {
            FormsAuthentication.SetAuthCookie(email, false);
        }

        private void RegisterUserIfNotPresent(IUserService service, string firstname, string email, string accessToken)
        {
            bool emailExists;
            try
            {
                emailExists = service.EmailExists(email);
            }
            catch
            {
                throw;
            }

            if (emailExists)
            {
                return;
            }
            try
            {
                service.Add(new User(firstname, email, Crypto.Hash(accessToken)));
            }
            catch
            {
                throw;
            }
        }
    }
}