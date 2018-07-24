using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace BusinessTripApplication.Tools
{
    public class InternetConnection
    {
        public static bool CheckForInternetConnection()
        {
            try
            {
                using (var client = new WebClient())
                using (client.OpenRead("https://www.google.ro/"))
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}