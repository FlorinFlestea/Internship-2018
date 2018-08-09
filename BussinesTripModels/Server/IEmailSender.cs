using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessTripApplication.Server
{
    public interface IEmailSender
    {
        void SendEmail(string email, string subject, string message);
    }
}
